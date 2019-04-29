using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class Caldron : MonoBehaviour
{
    // To define in Private at the End
    public bool isWaiting;
    public List<GameObject> prepList;
    public GameObject flask;
    public Sprite cookingCaldron;
    public Sprite emptyFlask;

    private Color objColor;
    private SpriteRenderer sr;
    private bool isReady;
    private Sprite caldronSprite;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        caldronSprite = sr.sprite;
        //prepList = new List<preparation.GetComponent<Recipes>().recipe>;
        isWaiting = false;
        isReady = false;
        objColor = sr.color;
    }

    // if cooking is not on, check if player is on the caldron
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isWaiting)
        {
            if (!isReady)
                sr.color = Color.blue;
            else
                sr.color = Color.yellow;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player interact with something
        if (Input.GetButtonDown("Interact"))
        {
            if (collision.GetComponent<PlayerInteraction>().currentObj && !isWaiting && !isReady)
            {
                // Add object to the list and delete from player inventory
                prepList.Add(collision.GetComponent<PlayerInteraction>().currentObj);
                collision.GetComponent<PlayerInteraction>().currentObj = null;
                collision.GetComponent<PlayerInteraction>().objSprite = null;

                // UI
                collision.GetComponent<PlayerInteraction>().ingredientImage.sprite = null;
                collision.GetComponent<PlayerInteraction>().ingredientText.text = "";

                /*
                // TEST POUR VOIR CE QU'IL Y A DANS LE CHAUDRON
                int x = 0;
                foreach (var ing in prepList)
                {
                    Debug.Log(x + " " + ing);
                    x++;
                }
                */
            }
            // Check if preparation is Ready and player has empty hands
            if (isReady && !collision.GetComponent<PlayerInteraction>().currentObj)
            {
                // Copy preparation in Player hands and clear caldron list
                CopyLists(prepList, collision.GetComponent<PlayerInteraction>().potion);
                prepList.Clear();
                collision.GetComponent<PlayerInteraction>().ingredientImage.sprite = emptyFlask;
                collision.GetComponent<PlayerInteraction>().ingredientText.text = "preparation";
                /* Display what's cooked
                int x = 0;
                foreach (var ing in collision.GetComponent<PlayerInteraction>().potion)
                {
                    Debug.Log(x + " " + ing);
                    x++;
                }
                */
                isReady = false;
                sr.color = Color.blue;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!isWaiting)
            sr.color = objColor;
    }

    // Coroutine to deactivate caldron interaction when cooking
   public IEnumerator Cook()
    {
        Debug.Log("Begin Cooking");
        sr.sprite = cookingCaldron;
        yield return new WaitForSeconds(5);
        isWaiting = false;
        isReady = true;
        flask.SetActive(true);
        sr.sprite = caldronSprite;
        sr.color = Color.yellow;
        Debug.Log("Cooking Over !");
    }

    private void CopyLists(List<GameObject> prepList, List<GameObject> potionList)
    {
        int x = 0;
        foreach (var ing in prepList)
        {
            potionList.Add(ing);
            x++;
        }
    }
}
