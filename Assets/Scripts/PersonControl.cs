using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonControl : MonoBehaviour
{
    private static readonly int Outfit = Animator.StringToHash("Outfit");
    private static readonly int Vel = Animator.StringToHash("Vel");
    private Animator _anim;
    private Rigidbody2D _body;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0f, 0.15f), Random.Range(0.1f, 0.5f), Random.Range(1, 0.25f));
        GetComponent<Animator>().SetInteger(Outfit, Random.Range(0, 8));
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat(Vel, _body.velocity.magnitude);
    }
}
