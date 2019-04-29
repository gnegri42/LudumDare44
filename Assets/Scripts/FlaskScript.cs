using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    private Color objColor;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objColor = sr.color;
        sr.color = Color.blue;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player interact with something
        if (Input.GetButtonDown("Interact") && !collision.GetComponent<PlayerInteraction>().currentObj && collision.GetComponent<PlayerInteraction>().potion.Count > 0)
        {
            collision.GetComponent<PlayerInteraction>().hasFlask = true;
            sr.color = objColor;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = objColor;
    }
}
