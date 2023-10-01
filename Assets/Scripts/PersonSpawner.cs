using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject person;

    private int _personCount;

    private void Start()
    {
        SpawnPerson(1f/10);
    }

    private void Update()
    {
        SpawnPerson(1f/500);
    }

    private void SpawnPerson(float chance)
    {
        var val = 1 - Time.deltaTime * chance;
        if (!(Random.value > val)) return;
        Instantiate(person, transform.position, Quaternion.identity);
        _personCount++;
        if (_personCount > 3)
        {
            Destroy(this);
        }
    }
}
