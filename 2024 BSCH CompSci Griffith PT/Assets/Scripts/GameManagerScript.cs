using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public float health;

    public float score;
    public Transform spawnPoint;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Start").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
}
