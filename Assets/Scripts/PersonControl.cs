using UnityEngine;

public class PersonControl : MonoBehaviour
{
    private static readonly int Outfit = Animator.StringToHash("Outfit");
    private static readonly int Vel = Animator.StringToHash("Vel");
    private Rigidbody2D _body;

    [HideInInspector]
    public Vector3 newPos;
    
    [SerializeField] private float moveTime;
    private float _moveCounter;

    [HideInInspector] public bool abducting = false;
    
    [SerializeField] private float speed;

    private SpriteRenderer _renderer;

    private Transform _playerTrans;

    //TODO: Make faster
    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = Color.HSVToRGB(Random.Range(0f, 0.15f), Random.Range(0.1f, 0.5f), Random.Range(1, 0.25f));
        GetComponent<Animator>().SetInteger(Outfit, Random.Range(0, 8));
        _body = GetComponent<Rigidbody2D>();
        newPos = transform.position + ((Vector3)Random.insideUnitCircle)/4f;
        _playerTrans = FindObjectOfType<AlienControl>().transform;
    }

    //TODO: Make faster
    // Update is called once per frame
    private void Update()
    {
        var position = transform.position;
        var dist = Vector3.SqrMagnitude(newPos - position);

        if (!abducting)
        {
            var newTime = _moveCounter + Time.deltaTime;
            if (newTime < moveTime)
            {
                _moveCounter = newTime;
            }

            if (!(_moveCounter > moveTime) && !(dist < 0.1f)) return;
            newPos = transform.position + (Vector3)Random.insideUnitCircle;
            _moveCounter -= moveTime;
        }
        else
        {
            newPos = _playerTrans.position;
            speed += 10 * Time.deltaTime;

            if ((dist < 0.1f))
            {
                AlienControl.abductees += 1;
                Heat.heat += 0.01f;
                Destroy(gameObject);
            }
        }
        
        var newVel = (newPos - position).normalized * speed;
        _body.velocity = newVel;
        _renderer.flipX = newVel.x > 0;
    }
}
