using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform playerPosition;
    private float xOffset;
    private float yOffset;
    private float zOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        //get gameobject's position relative to player position
        xOffset = transform.position.x - playerPosition.position.x;
        yOffset = transform.position.y - playerPosition.position.y;
        zOffset = transform.position.z - playerPosition.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //maintain gameobject positon relative to playerPosition
        transform.position = new Vector3(playerPosition.position.x + xOffset, playerPosition.position.y + yOffset, playerPosition.position.z + zOffset);
    }
}
