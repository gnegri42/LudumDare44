using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsScript : MonoBehaviour
{
    private Color objColor;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objColor = sr.color;
        sr.color = Color.blue;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player interact with something
        if (Input.GetButtonDown("Interact"))
        {
            ClearInventory(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = objColor;
    }

    // Retirer tous les éléments de l'inventaire du joueur
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
    /*
     * FUNCTION TO EMPTY ARRAY
    private void ClearArray(GameObject[] array)
    {
        int length = array.Length;
        for (int x = 0; x < length; x++)
        {
            array[x] = null;
        }
    }
    */
}
