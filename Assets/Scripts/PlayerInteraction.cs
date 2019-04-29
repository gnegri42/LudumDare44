using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    // To define in Private at the End
    public List<GameObject> potion;
    public GameObject   currentObj = null;
    public Sprite       objSprite;
    public bool        hasFlask;

    [Header("UI")]
    // UI Elements
    public Sprite fullBottle;
    public Image        ingredientImage;
    public Text     ingredientText;

    void Start()
    {
        //Creating a List to receive potion
        ingredientText.text = "";
        hasFlask = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFlask)
        {
            ingredientImage.sprite = fullBottle;
            ingredientText.text = "Potion";
        }
    }
}
