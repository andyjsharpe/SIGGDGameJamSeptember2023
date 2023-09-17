using UnityEngine;

public class bomb : MonoBehaviour
{
    private Transform _playerTrans;
    private Rigidbody2D _body;
    [SerializeField] private float speed;

    [SerializeField] private int splashDamage;
    [SerializeField] private GameObject spawnOnHit;
    
    [SerializeField] private float BlastRadius;
    
    [SerializeField] private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            BlowUp();
        }
        
        _body.AddForce(Vector3.down * speed);
    }

    private void BlowUp()
    {
        foreach (var hel in FindObjectsOfType<Health>())
        {
            if (Vector3.SqrMagnitude(hel.transform.position - transform.position) < BlastRadius)
            {
                hel.Damage(splashDamage);
            }
        }
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        BlowUp();
    }
    
    private void OnDestroy()
    {
        Instantiate(spawnOnHit, transform.position, Quaternion.identity);
    }
}
