using UnityEngine;

public class Dagger : MonoBehaviour
{
    public float force;
    public Rigidbody2D rb;
    
    void Start()
    {
        Destroy(this.gameObject, 2);
    }
    public void AddForce(int dir)
    {
        rb.AddForce(new Vector2(dir, 0), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
