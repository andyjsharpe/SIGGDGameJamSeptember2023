using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AutoCuller : MonoBehaviour
{
    private ShadowCaster2D _caster;
    private bool _isCaster;
    private Light2D _light;
    private bool _isLight;
    private Collider2D _collider;
    private bool _isCollider;
    
    // Start is called before the first frame update
    private void Start()
    {
        _caster = GetComponent<ShadowCaster2D>();
        _isCaster = _caster != null;
        _light = GetComponentInChildren<Light2D>();
        _isLight = _light != null;
        _collider = GetComponent<Collider2D>();
        _isCollider = _collider != null;
        
        OnOff(false);
    }

    private void OnBecameInvisible()
    {
        OnOff(false);
    }

    private void OnBecameVisible()
    {
        OnOff(true);
    }

    private void OnOff(bool b)
    {
        if (_isCaster)
        {
            _caster.enabled = b;
        }
        if (_isLight)
        {
            _light.enabled = b;
        }
        if (_isCollider)
        {
            _collider.enabled = b;
        }
    }

    /*
    // Update is called once per frame
    private void Update()
    {
        //OnOff((_playerTrans.position - transform.position).sqrMagnitude < 144);
    }
    */
}
