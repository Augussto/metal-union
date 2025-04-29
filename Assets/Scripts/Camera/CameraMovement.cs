using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool followPlayer;
    public Vector3 targetPos;
    private PlayerMovement player;
    [SerializeField] float cameraY;

    void Start()
    {
        targetPos = new Vector3(-3, -2.5f, -10);
        player = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        if (followPlayer)
        {
            if (player.isGrounded)
            {
                targetPos = new Vector3(player.transform.position.x, player.transform.position.y + cameraY, -10f);
            }
            else
            {
                targetPos = new Vector3(player.transform.position.x, targetPos.y, targetPos.z);
            }
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
