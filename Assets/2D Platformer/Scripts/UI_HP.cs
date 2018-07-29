using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HP : MonoBehaviour
{
    public int HP = 0;                   // The player's score.


    private BasicController basicController;    // Reference to the player control script.


    void Awake()
    {
        // Setting up the reference.
        basicController = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicController>();
    }


    void Update()
    {
        // Set the score text.
        GetComponent<GUIText>().text = "HP: " + 100;
    }
}