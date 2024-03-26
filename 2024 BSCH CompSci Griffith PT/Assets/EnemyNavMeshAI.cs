using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavMeshAI : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the player using navmesh
         GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.position;
    }
}
