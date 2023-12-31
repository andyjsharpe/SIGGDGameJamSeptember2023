using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class AlienControl : MonoBehaviour
{
    private Vector3 _dir = Vector3.zero;
    [SerializeField] private float force;
    private Rigidbody2D _rigid;
    private Camera _cam;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Light2D beamLight;

    private bool _firing;
    private bool _abducting;
    [SerializeField] private float fireRate;
    private float _shotTime;
    
    public static int Abductees = 0;

    public static Health Health;

    // Start is called before the first frame update
    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _cam = GetComponentInChildren<Camera>();
        beamLight.enabled = false;
        Health = GetComponent<Health>();
        AlienControl.Abductees = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        _rigid.AddForce(force*force*Time.deltaTime*_dir);
        TryFire();
        TryAbduct();
        Health.Damage(-Time.deltaTime);
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
        if (_shotTime < fireRate)
        {
            _shotTime = newTime;
        }
        if (_firing && _shotTime > fireRate)
        {
            var mousePos = GetMouseWorldPos();
            var position = transform.position;
            var dir = mousePos - position;
            Instantiate(projectile, position + dir.normalized * 1f, Quaternion.LookRotation(dir, Vector3.forward));
            _shotTime -= fireRate;
        }
    }
    
    public void OnAltFire(InputValue value)
    {
        _abducting = !_abducting;
        beamLight.enabled = !beamLight.enabled ;
    }

    private void TryAbduct()
    {
        if (!_abducting) return;
        //do abducting
        var outs = Physics2D.OverlapCapsuleAll((Vector2)transform.position - Vector2.up * 2, new Vector2(1,2), CapsuleDirection2D.Vertical, 0);
        foreach (var coll in outs)
        {
            var pc = coll.GetComponent<PersonControl>();
            if (pc == null || pc.abducting) continue;
            pc.abducting = true;
            pc.newPos = transform.position;
            pc.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        return _cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _cam.transform.position.z));
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(2);
    }
}
