using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
