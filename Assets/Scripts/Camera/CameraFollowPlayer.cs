using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        if (player != null)
        {
            this.transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
