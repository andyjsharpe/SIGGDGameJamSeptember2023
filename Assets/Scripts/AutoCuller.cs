using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AutoCuller : MonoBehaviour
{
    private ShadowCaster2D _caster;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private Health _health;

    //TODO: Make faster
    // Start is called before the first frame update
    private void Start()
    {
        /*
        _caster = GetComponent<ShadowCaster2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _health = GetComponent<Health>();
        */
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
    }

    /*
    // Update is called once per frame
    private void Update()
    {
        if ((_playerTrans.position - transform.position).sqrMagnitude > 144)
        {
            if (_caster is not null)
            {
                _caster.enabled = false;
            }
            if (_renderer is not null)
            {
                _renderer.enabled = false;
            }
            if (_animator is not null)
            {
                _animator.enabled = false;
            }
            if (_polygonCollider2D is not null)
            {
                _polygonCollider2D.enabled = false;
            }
            if (_health is not null)
            {
                _health.enabled = false;
            }
            if (_boxCollider2D is not null)
            {
                _boxCollider2D.enabled = false;
            }
        }
        else
        {
            if (_caster is not null)
            {
                _caster.enabled = true;
            }
            if (_renderer is not null)
            {
                _renderer.enabled = true;
            }
            if (_animator is not null)
            {
                _animator.enabled = true;
            }
            if (_polygonCollider2D is not null)
            {
                _polygonCollider2D.enabled = true;
            }
            if (_health is not null)
            {
                _health.enabled = true;
            }
            if (_boxCollider2D is not null)
            {
                _boxCollider2D.enabled = true;
            }
        }
    }
    */
}
