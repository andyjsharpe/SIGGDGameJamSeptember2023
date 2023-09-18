using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject person;

    private void Update()
    {
        var val = 1 - Time.deltaTime / 10;
        if (!(Random.value > val)) return;
        Instantiate(person, transform.position, Quaternion.identity);
    }
}
