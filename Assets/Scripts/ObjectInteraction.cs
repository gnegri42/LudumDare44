using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    // To define in Private at the End
    //public Sprite ingredientSprite;
    public GameObject ingredient;

    private Color objColor;
    private SpriteRenderer sr;
    private SpriteRenderer ingredientSr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ingredientSr = ingredient.GetComponent<SpriteRenderer>();
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
        if (Input.GetButtonDown("Interact") && !collision.GetComponent<PlayerInteraction>().currentObj && collision.GetComponent<PlayerInteraction>().potion.Count == 0)
        {   //Do something with the object
            collision.GetComponent<PlayerInteraction>().currentObj = ingredient;
            collision.GetComponent<PlayerInteraction>().objSprite = ingredientSr.sprite;

            // UI
            collision.GetComponent<PlayerInteraction>().ingredientImage.sprite = ingredientSr.sprite;
            collision.GetComponent<PlayerInteraction>().ingredientText.text = ingredient.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = objColor;
    }
}
