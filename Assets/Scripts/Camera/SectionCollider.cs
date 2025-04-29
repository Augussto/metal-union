using Unity.VisualScripting;
using UnityEngine;

public class SectionCollider : MonoBehaviour
{
    [SerializeField] bool hasPassed;
    [SerializeField] Vector3 currentPos, nextPos;
    private Vector3 targetPos;
    [SerializeField] private CameraMovement cam;

    void Start()
    {
        cam = FindObjectOfType<CameraMovement>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!hasPassed)
            {
                cam.targetPos = nextPos;
                hasPassed = true;
            }
            else 
            {
                cam.targetPos = currentPos;
                hasPassed = false;
            }
        }
    }
}
