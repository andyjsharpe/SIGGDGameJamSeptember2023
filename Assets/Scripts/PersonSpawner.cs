using UnityEngine;
using Random = UnityEngine.Random;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject person;

    private int _personCount;

    private void Update()
    {
        var val = 1 - Time.deltaTime / 500;
        if (!(Random.value > val)) return;
        Instantiate(person, transform.position, Quaternion.identity);
        _personCount++;
        if (_personCount > 1)
        {
            Destroy(this);
        }
    }
}
