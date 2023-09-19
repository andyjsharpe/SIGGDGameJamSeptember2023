using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform _playerTrans;
    private Rigidbody2D _body;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private int damage;
    [SerializeField] private int splashDamage;
    [SerializeField] private GameObject spawnOnHit;

    [SerializeField] private float missileRadius;
    [SerializeField] private float blastRadius;
    
    [SerializeField] private float trackRatio;
    
    [SerializeField] private float timer;
    
    private Heat _heat;

    // Start is called before the first frame update
    private void Start()
    {
        _playerTrans = FindObjectOfType<AlienControl>().transform;
        _body = GetComponent<Rigidbody2D>();
        _heat = FindObjectOfType<Heat>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            BlowUp();
        }
        
        var transform1 = transform;
        var dir = _playerTrans.position - transform1.position;
        var sqrDist = dir.sqrMagnitude;
        var angle = Vector3.SignedAngle(transform1.up, dir, Vector3.forward);
        var ratio = Mathf.Abs(angle)/180;

        if (sqrDist < missileRadius * missileRadius)
        {
            BlowUp();
        }

        _body.AddForce((1 - ratio) * speed * Time.deltaTime * transform1.up, ForceMode2D.Force);
        if (ratio < trackRatio)
        {
            transform.Rotate(Vector3.forward, angle * turnSpeed * Time.deltaTime);
        }
    }

    private void BlowUp()
    {
        var outs = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (var hel in outs)
        {
            var health = hel.GetComponent<Health>();
            if (health == null || hel.GetComponent<Plane>() != null) continue;
            health.Damage(splashDamage);
        }
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.transform.GetComponent<Health>();
        if (health != null && other.transform.GetComponent<Plane>() == null)
        {
            health.Damage(damage);
        }
        BlowUp();
    }
    
    private void OnDestroy()
    {
        Instantiate(spawnOnHit, transform.position, Quaternion.identity);
    }
}
