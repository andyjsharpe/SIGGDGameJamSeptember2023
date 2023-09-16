using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonControl : MonoBehaviour
{
    private static readonly int Outfit = Animator.StringToHash("Outfit");
    private static readonly int Vel = Animator.StringToHash("Vel");
    private Animator _anim;
    private Rigidbody2D _body;

    private Vector3 _newPos;
    [HideInInspector] public bool panicked;
    [SerializeField] private float moveTime;
    private float _moveCounter;
    
    [SerializeField] private float speed;

    private GridInitiator _gridInitiator;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0f, 0.15f), Random.Range(0.1f, 0.5f), Random.Range(1, 0.25f));
        GetComponent<Animator>().SetInteger(Outfit, Random.Range(0, 8));
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _gridInitiator = FindObjectOfType<GridInitiator>();
        _newPos = transform.position + (Vector3)Random.insideUnitCircle/4f;
    }

    // Update is called once per frame
    private void Update()
    {
        var position = transform.position;
        var dist = Vector3.Distance(_newPos, position);
        
        var newTime = _moveCounter + Time.deltaTime;
        if (newTime < moveTime)
        {
            _moveCounter = newTime;
        }

        if (!panicked && dist < 0.1f)
        {
            _newPos = _gridInitiator.SecondNearestIntersection(position);
        } else if (panicked && _moveCounter > moveTime)
        {
            _newPos = transform.position + (Vector3)Random.insideUnitCircle;
        }

        _body.velocity = (_newPos - position).normalized * Mathf.Min(speed, dist);
        _anim.SetFloat(Vel, _body.velocity.magnitude);
    }
}
