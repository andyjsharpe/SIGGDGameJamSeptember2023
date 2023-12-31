using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    private Transform _playerTrans;
    private Rigidbody2D _body;
    [SerializeField] private float speed;

    [SerializeField] private int splashDamage;
    [SerializeField] private GameObject spawnOnHit;
    
    [FormerlySerializedAs("BlastRadius")] [SerializeField] private float blastRadius;
    
    [SerializeField] private float timer;

    private Light2D flickerLight;
    private float maxTime;

    // Start is called before the first frame update
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.identity;
        flickerLight = GetComponent<Light2D>();
        maxTime = timer;
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

        var deltaT = maxTime - timer;
        var timeRatio = deltaT / maxTime;
        var timeValue = Mathf.Sin(2 * Mathf.PI * Mathf.Pow(3*timeRatio,2));
        flickerLight.enabled = timeValue > 0;
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
        BlowUp();
    }
    
    private void OnDestroy()
    {
        Instantiate(spawnOnHit, transform.position, Quaternion.identity);
    }
}
