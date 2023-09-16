using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class AlienControl : MonoBehaviour
{
    private Vector3 _dir = Vector3.zero;
    [SerializeField] private float force;
    private Rigidbody2D _rigid;
    private Camera _cam;
    [SerializeField] private GameObject projectile;
    private CircleCollider2D _beam;
    private Light2D _beamLight;

    private bool _firing = false;
    private bool _abducting = false;
    [SerializeField] private float fireRate;
    private float _shotTime = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _cam = GetComponentInChildren<Camera>();
        _beam = GetComponentInChildren<CircleCollider2D>();
        _beamLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _rigid.AddForce(_dir * force);
        TryFire();
        TryAbduct();
    }
    
    public void OnMove(InputValue value)
    {
        _dir = value.Get<Vector2>();
    }
    
    public void OnFire(InputValue value)
    {
        _firing = !_firing;
    }

    private void TryFire()
    {
        var newTime = _shotTime + Time.deltaTime;
        if (newTime < fireRate)
        {
            _shotTime = newTime;
        }
        if (_firing)
        {
            var mousePos = GetMouseWorldPos();
            var position = transform.position;
            Instantiate(projectile, position, Quaternion.LookRotation(mousePos - position, Vector3.forward));
            _shotTime -= fireRate;
        }
    }
    
    public void OnAltFire(InputValue value)
    {
        _abducting = !_abducting;
        _beamLight.enabled = !_beamLight.enabled ;
    }

    private void TryAbduct()
    {
        if (_abducting)
        {
            //do abducting
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        return _cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _cam.transform.position.z));
    }
}
