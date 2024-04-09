using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavMeshAI : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public NavMeshAgent agent;
    public float currentVelocity;
    private bool aggro;
    public bool destinationReached;
    public float destinationThreshold;
    public Transform[] patrolPoints;
    public float aggroTimer;

    public float patrolSpeed;

    public float aggroSpeed;

    public GameObject aggroUI;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        aggro = false;
        destinationReached = true;
        aggroUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the velocity of the enemy
        currentVelocity = agent.velocity.magnitude;
        // Set the speed of the animator to the velocity of the enemy
        animator.SetFloat("velocity", currentVelocity);
        
        if (aggro == true)
        {
            aggroUI.SetActive(true);
            // Move towards the player using navmesh
            agent.destination = player.position;
        }
        
        if (aggro == false && destinationReached == true)
        {
            aggroUI.SetActive(false);
            destinationReached = false;
            agent.speed = patrolSpeed;
            // Move towards the patrol points using navmesh
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }
        
        // Check if the enemy has reached the destination
        if (Vector3.Distance(transform.position, agent.destination) < destinationThreshold)
        {
            destinationReached = true;
        }

    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggro = true;
            agent.speed = aggroSpeed;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            agent.destination = player.position; //this is the last seen player position (since OnTriggerExit only runs once)
            StopCoroutine("AggroTimer");
            StartCoroutine("AggroTimer");
        }
    }
    
    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTimer);
        aggro = false;
        destinationReached = true;
    }
}
