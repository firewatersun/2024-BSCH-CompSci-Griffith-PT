using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move towards the mouseclick using navmesh
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hit.point;
            }
        }
    }
}
