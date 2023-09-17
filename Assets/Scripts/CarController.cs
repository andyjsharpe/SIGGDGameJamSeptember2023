using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Sprite[] carSprites;
    
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = carSprites[Random.Range(0, carSprites.Length)];
    }
}
