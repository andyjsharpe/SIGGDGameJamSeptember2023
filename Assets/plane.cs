using UnityEngine;

public class plane : MonoBehaviour
{
    private Transform _playerTrans;
    private Rigidbody2D _body;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private float attackDist;

    [SerializeField] private GameObject projectile;
    
    [SerializeField] private float fireRate;
    private float _shotTime = 0;
    
    [SerializeField] private float fireRatio;
    
    [SerializeField] private float spawnOffset;
    
    // Start is called before the first frame update
    private void Start()
    {
        _playerTrans = FindObjectOfType<AlienControl>().transform;
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var transform1 = transform;
        var dir = _playerTrans.position - transform1.position;
        var sqrDist = dir.sqrMagnitude;
        var angle = Vector3.SignedAngle(transform1.up, dir, Vector3.forward);
        var ratio = Mathf.Abs(angle)/180;

        var ts = turnSpeed;
        var s = speed;
        
        if (sqrDist < attackDist * attackDist && ratio < fireRatio)
        {
            TryFire(dir);
            ts /= 2;
            s *= 10;
        }

        _body.AddForce((1 - ratio) * s * Time.deltaTime * transform1.up, ForceMode2D.Force);
        transform.Rotate(Vector3.forward, angle * ts * Time.deltaTime);
    }
    
    private void TryFire(Vector3 pathDir)
    {
        var newTime = _shotTime + Time.deltaTime;
        if (_shotTime < fireRate)
        {
            _shotTime = newTime;
        }
        if (_shotTime > fireRate)
        {
            var transform1 = transform;
            var dir = transform1.up;
            Instantiate(projectile, transform1.position + dir.normalized * spawnOffset, transform1.rotation);
            _shotTime -= fireRate;
        }
    }
}
