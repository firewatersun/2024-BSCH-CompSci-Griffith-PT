using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        DetectPlayer,
        Chasing,
        AggroIdle,
    }

    public State enemyAIState;
    public float moveSpeed; //speed of the enemy when patrolling

    public float maxSpeed;

    public float chaseSpeed; //speed of the enemy when chasing the player

    private float speed; //current speed of the enemy

    public float detectedPlayerTime; //time the enemy will stay in detect mode before beginning chasing player

    public float aggroTime; //used if player is out of detection radius - enemy will stay in aggro mode for this time, and can immediately resume chasing before going back to idle

    public bool playerDetected; //if the player is detected

    public bool aggro; //if the enemy is in an aggro state
    private Rigidbody2D _myRb;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAIState = State.Idle;
        _myRb = GetComponent<Rigidbody2D>(); // look for a component called Rigidbody2D and assign it to myRb
    }

    // Update is called once per frame
    void Update()
    {
        _myRb.velocity = new Vector2(speed, _myRb.velocity.y);

        
        switch (enemyAIState)
        {
            case State.Idle:
                speed = 0;
                //do nothing
                break;
            case State.Patrol:
                speed = moveSpeed;
                //move the enemy
                break;
            case State.DetectPlayer:
                speed = 0;
                //when player is detected, start a timer to chase the player
                break;
            case State.Chasing:
                //chases the player
                speed = chaseSpeed;
                break;
            case State.AggroIdle:
                //stays in aggro mode for a set time before going back to idle
                speed = 0;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            if (aggro == false)
            {
                StopCoroutine("DetectTimer"); //need to stop the Coroutine in case it was previously started e.g. if the player quickly enters and exits the detection radius
                StartCoroutine("DetectTimer");
            }
            if (aggro == true)
            {
                playerDetected = true;
                enemyAIState = State.Chasing;
            }
        }

    }
    
    IEnumerator DetectTimer()
    {
        enemyAIState = State.DetectPlayer;
        yield return new WaitForSeconds(detectedPlayerTime);
        if (playerDetected == true)
        {
            aggro = true;
            enemyAIState = State.Chasing;
        }
        if (playerDetected == false)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            if (aggro == true)
            {
                StopCoroutine("AggroTimer");
                StartCoroutine("AggroTimer");
            }
        }
    }
    
    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTime);
        if (playerDetected == false & aggro == false)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }
        if (playerDetected == false & aggro == true)
        {
            enemyAIState = State.AggroIdle;
        }
        yield return new WaitForSeconds(aggroTime*2);
        if (playerDetected == false)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }

    }
}
