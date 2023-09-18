using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShrinkLight : MonoBehaviour
{
    [SerializeField] private float lifespan;

    private float _counter = 0;
    
    private float _startSize;

    private Light2D _light2D;

    private void Start()
    {
        _light2D = GetComponent<Light2D>();
        _startSize = _light2D.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter > lifespan)
        {
            Destroy(gameObject);
        }
        _light2D.intensity = Mathf.Lerp(_startSize, 0, _counter / lifespan);
    }
}
