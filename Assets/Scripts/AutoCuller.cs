using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AutoCuller : MonoBehaviour
{
    private Transform _playerTrans;
    private ShadowCaster2D _caster;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTrans = FindObjectOfType<AlienControl>().transform;
        _caster = GetComponent<ShadowCaster2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
        }
    }
}
