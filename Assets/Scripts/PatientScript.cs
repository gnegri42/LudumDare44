using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientScript : MonoBehaviour
{
    public List<Recipes> recipes;
    public List<Sprite> sprites;
    public List<GameObject> ingredientList;
    public List<Image> icons;

    private List<Sprite> ingredientsSprites;
    private bool inCharge = false;
    private Color objColor;
    private SpriteRenderer sr;
    private Recipes choosedRecipe;
    private Sprite choosedSprite;
    private Vector3 basicPosition;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // Get one random recipe
        choosedRecipe = ChooseRecipeInList(recipes);
        // Extract list of ingredients
        ingredientList = choosedRecipe.GetComponent<Recipes>().recipe;
        // Cache Ingredients Sprites
        ingredientsSprites = choosedRecipe.GetComponent<Recipes>().ingredientsSprites;
        // Choose one Sprite
        choosedSprite = ChooseSpriteInList(sprites);
        sr.sprite = choosedSprite;
        ChangeIngredientIcons(icons, ingredientsSprites);
    }

    // Check if player go to see the patient
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the patient has not been activated before put one color. Else, another
        if (!inCharge)
        {
            objColor = sr.color;
            sr.color = Color.blue;
        }
        if (inCharge)
        {
            objColor = sr.color;
            sr.color = Color.yellow;
        }
    }

    // Check if player interact with patient, if not already did
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player interact with something
        if (Input.GetButtonDown("Interact") && !inCharge)
        {
            inCharge = true;
            sr.color = Color.yellow;
        }
        if (Input.GetButtonDown("Interact") && inCharge &&
            (collision.GetComponent<PlayerInteraction>().currentObj || collision.GetComponent<PlayerInteraction>().potion.Count > 0))
        {
            // Si la potion correspond bien à celle choisie
            if (CompareLists<GameObject>(collision.GetComponent<PlayerInteraction>().potion, ingredientList) && collision.GetComponent<PlayerInteraction>().hasFlask)
            {
                // Increase points
                GameManager.score += 10;
                ClearInventory(collision);
                StartCoroutine(ChangePatient());
                Debug.Log("Réussi"); 
            }
            else
            {
                // Diminuer les points, whatever
                ClearInventory(collision);
                Debug.Log("Mauvaise recette");
                StartCoroutine(ChangePatient());
                inCharge = false;
            }
            
        }
    }

    // Get color back to the previous one
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objColor != null)
            sr.color = objColor;
    }

    // Coroutine to change patient
    private IEnumerator ChangePatient()
    {
        basicPosition = transform.position;
        transform.position = new Vector3(50f, 0, 0);
        EmptyIngredientIcons(icons, ingredientsSprites);
        yield return new WaitForSeconds(5);
        // Find new recipe
        choosedRecipe = ChooseRecipeInList(recipes);
        // Change Sprite
        choosedSprite = ChooseSpriteInList(sprites);
        ChangeIngredientIcons(icons, ingredientsSprites);
        sr.sprite = choosedSprite;
        inCharge = false;
        transform.position = basicPosition;
    }

    // Function to disable ingredient icons
    private void EmptyIngredientIcons(List<Image> icons, List<Sprite> ingredients)
    {
        int x = 0;

        foreach (var ing in ingredients)
        {
            icons[x].enabled = false;
            x++;
        }
    }

    // Function to change ingredient Icons
    private void ChangeIngredientIcons(List<Image> icons, List<Sprite> ingredients)
    {
        /*
        icons[0].sprite = ingredients[0];
        icons[0].enabled = true;
        icons[1].sprite = ingredients[1];
        icons[1].enabled = true;
        */
        int x = 0;

        foreach (var ing in ingredients)
        {
            icons[x].sprite = ing;
            icons[x].enabled = true;
            x++;
        }
    }

    // Remove all elements from player inventory
    private void ClearInventory(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>().currentObj)
        {
            collision.GetComponent<PlayerInteraction>().currentObj = null;
            collision.GetComponent<PlayerInteraction>().objSprite = null;
            // UI
            collision.GetComponent<PlayerInteraction>().ingredientImage.sprite = null;
            collision.GetComponent<PlayerInteraction>().ingredientText.text = "";
        }
        if (collision.GetComponent<PlayerInteraction>().potion.Count > 0)
            collision.GetComponent<PlayerInteraction>().potion.Clear();
        if (collision.GetComponent<PlayerInteraction>().hasFlask)
        {
            collision.GetComponent<PlayerInteraction>().hasFlask = false;
            // UI
            collision.GetComponent<PlayerInteraction>().ingredientImage.sprite = null;
            collision.GetComponent<PlayerInteraction>().ingredientText.text = "";
        }
    }

    // Function to return one recipe beetween many
    private Recipes ChooseRecipeInList(List<Recipes> recipesList)
    {
        int x = 0;

        x = Random.Range(0, recipesList.Count);
        return (recipesList[x]);
    }

    // Function to return one Sprite beetween many
    private Sprite ChooseSpriteInList(List<Sprite> spritesList)
    {
        int x = 0;

        x = Random.Range(0, spritesList.Count);
        return (spritesList[x]);
    }


    #region CompareList
    // Comparer deux listes pour voir si elles sont identiques
    private bool CompareLists<T>(List<T> aListA, List<T> aListB)
    {
        int x = 0;
        foreach (var ing1 in aListA)
        {
            Debug.Log("Liste A : " + ing1);
            x++;
        }
        x = 0;
        foreach (var ing2 in aListB)
        {
            Debug.Log("Liste B : " + ing2);
            x++;
        }
        if (aListA == null || aListB == null || aListA.Count != aListB.Count)
            return false;
        if (aListA.Count == 0)
            return true;
        Dictionary<T, int> lookUp = new Dictionary<T, int>();
        // create index for the first list
        for (int i = 0; i < aListA.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListA[i], out count))
            {
                lookUp.Add(aListA[i], 1);
                continue;
            }
            lookUp[aListA[i]] = count + 1;
        }
        for (int i = 0; i < aListB.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListB[i], out count))
            {
                // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(aListB[i]);
            else
                lookUp[aListB[i]] = count;
        }
        // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        return lookUp.Count == 0;
    }
    #endregion
}
