using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private GameObject spawnOnHit;
    
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.transform.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(spawnOnHit, transform.position, Quaternion.identity);
    }
}
