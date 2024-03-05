using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //using the TextMeshPro namespace
public class GameUIScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public GameManagerScript gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>(); //finds the game manager object and gets the GameManagerScript component
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameManager.score; //sets the text of the scoreText to the score value from the GameManagerScript
        healthText.text = "Health: " + gameManager.health; //sets the text of the healthText to the health value from the GameManagerScript
    }
}
