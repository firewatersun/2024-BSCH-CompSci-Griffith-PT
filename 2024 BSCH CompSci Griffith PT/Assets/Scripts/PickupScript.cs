using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float scoreValue;
    public GameManagerScript gameManager;

    public GameObject collectedEffect;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>(); //finds the game manager object and gets the GameManagerScript component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) //checks if the gameobject that collided with the pickup is the player
        {
            gameManager.AddScore(scoreValue); //runs the AddScore method from the GameManagerScript, passing the scoreValue as an argument
            Instantiate(collectedEffect, transform.position, transform.rotation); //creates the collectedEffect at the position and rotation of the pickup
            Destroy(gameObject); //destroys self
        }
    }
}
