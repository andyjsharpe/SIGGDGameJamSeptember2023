using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AutoCuller : MonoBehaviour
{
    private Transform _playerTrans;
    
    private ShadowCaster2D _caster;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private Health _health;
    private PersonControl _personControl;
    private Rigidbody2D _rigidbody2D;

    //TODO: Make faster
    // Start is called before the first frame update
    private void Start()
    {
        _playerTrans = FindObjectOfType<AlienControl>().transform;
        
        _caster = GetComponent<ShadowCaster2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _health = GetComponent<Health>();
        _personControl = GetComponent<PersonControl>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        //OnOff(false);
    }

    private void OnBecameVisible()
    {
        //OnOff(true);
    }

    private void OnOff(bool b)
    {
        if (_caster != null)
        {
            _caster.enabled = b;
        }
        if (_renderer != null)
        {
            _renderer.enabled = b;
        }
        if (_animator != null)
        {
            _animator.enabled = b;
        }
        if (_polygonCollider2D != null)
        {
            _polygonCollider2D.enabled = b;
        }
        if (_health != null)
        {
            _health.enabled = b;
        }
        if (_boxCollider2D != null)
        {
            _boxCollider2D.enabled = b;
        }
        if (_personControl != null)
        {
            _personControl.enabled = b;
        }

        if (_rigidbody2D != null)
        {
            _rigidbody2D.simulated = b;
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        //OnOff((_playerTrans.position - transform.position).sqrMagnitude < 144);
    }
}
