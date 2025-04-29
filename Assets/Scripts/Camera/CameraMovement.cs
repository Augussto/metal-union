using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool followPlayer;
    public Vector3 targetPos;
    private PlayerMovement player;

    void Start()
    {
        targetPos = new Vector3(-3, -2.5f, -10);
        player = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        if (followPlayer)
        {
            targetPos = player.transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
        }
        else
        {
            if (transform.position != targetPos)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
            }
        }
    }
}
