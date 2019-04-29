using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Reference to Animator component attached to Witch_Anim
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Bool for Interaction Animation
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("isInteracting", true);
        }
        else
        {
            anim.SetBool("isInteracting", false);
        }
    }
}
