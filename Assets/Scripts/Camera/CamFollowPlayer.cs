using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] GameObject player;
    [SerializeField] float smoothSpeed = 0.125f; // Adjust this value for desired smoothness

    Vector3 offset;

    void Start()
    {
        cam = GetComponent<Transform>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        offset = cam.position - new Vector3(player.transform.position.x,player.transform.position.y - 1,player.transform.position.z);
    }

    void FixedUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(cam.position, desiredPosition, smoothSpeed);
        cam.position = smoothedPosition;
    }
}
