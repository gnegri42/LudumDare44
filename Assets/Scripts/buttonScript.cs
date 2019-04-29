using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public GameObject caldron;

    private Color objColor;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objColor = sr.color;
        sr.color = Color.blue;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the Caldron is available and player interacting with the button
        if (Input.GetButtonDown("Interact") && !caldron.GetComponent<Caldron>().isWaiting)
        {
            caldron.GetComponent<Caldron>().isWaiting = true;
            StartCoroutine(caldron.GetComponent<Caldron>().Cook());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = objColor;
    }
}
