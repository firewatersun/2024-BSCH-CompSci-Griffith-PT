using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    public float currentVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the velocity of the player
        currentVelocity = agent.velocity.magnitude;
        //set the speed of the animator to the velocity of the player
        animator.SetFloat("velocity", currentVelocity);
        
        //move towards the mouseclick using navmesh
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
    }
}
