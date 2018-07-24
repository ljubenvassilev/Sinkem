using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player;
    public float distance = 10f;
    public float height = 5f;
    public float cameraSpeed = 10f;
    public float rotationSpeed = 10f;
    public Vector3 lookOffset = new Vector3(0, 1, 0);

    void FixedUpdate()
    {
        if (player)
        {
            Vector3 lookPosition = player.position + this.lookOffset;
            Vector3 relativePosition = lookPosition - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation,
                Time.deltaTime * this.rotationSpeed * 0.1f);
            Vector3 targetPosition = player.transform.position + player.transform.up * this.height -
                                     player.transform.forward * this.distance;

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition,
                Time.deltaTime * this.cameraSpeed * 0.1f);
            
        }
    }
}