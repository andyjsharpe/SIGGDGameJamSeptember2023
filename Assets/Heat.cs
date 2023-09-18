using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Heat : MonoBehaviour
{
    private float _heat = 0;
    [SerializeField] private Slider[] heatSliders;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI abductees;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject heli;
    [FormerlySerializedAs("F22")] [SerializeField] private GameObject f22;
    [FormerlySerializedAs("Su")] [SerializeField] private GameObject su;
    [FormerlySerializedAs("Swept")] [SerializeField] private GameObject swept;
    [FormerlySerializedAs("B52")] [SerializeField] private GameObject b52;
    [FormerlySerializedAs("Nuker")] [SerializeField] private GameObject nuker;
    
    [SerializeField] private Slider health;

    private AlienControl _ac;
    private Transform _player;
    private Health _playerHealth;

    private void Start()
    {
        _ac = FindObjectOfType<AlienControl>();
        _player = _ac.transform;
        _player.position = GetSpawn().position;
        _playerHealth = _player.GetComponent<Health>();
        StartCoroutine(nameof(SpawnThings));
    }

    private Transform GetSpawn()
    {
        while (true)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (Vector3.SqrMagnitude(point.position - _player.position) < 144)
            {
                continue;
            }

            return point;
        }
    }

    private void SpawnThis(GameObject g)
    {
        Instantiate(g, GetSpawn().position, Quaternion.identity);
    }

    public IEnumerator SpawnThings()
    {
        while (true)
        {
            if (_heat > 5)
            {
                SpawnThis(nuker);
                SpawnThis(f22);
                SpawnThis(swept);
                SpawnThis(su);
                SpawnThis(b52);
            }
            else if (_heat > 4)
            {
                SpawnThis(f22);
                SpawnThis(swept);
                SpawnThis(su);
                SpawnThis(b52);
            }
            else if (_heat > 3)
            {
                SpawnThis(f22);
                SpawnThis(swept);
                SpawnThis(su);
            }
            else if (_heat > 2)
            {
                SpawnThis(f22);
                SpawnThis(swept);
                SpawnThis(heli);
            }
            else if (_heat > 1)
            {
                SpawnThis(heli);
                SpawnThis(f22);
            }
            else
            {
                SpawnThis(heli);
            }
            yield return new WaitForSeconds(20 / Mathf.Sqrt(Mathf.Max(_heat, 1)));
        }
    }

    public void AddHeat(float heatDelta)
    {
        _heat += heatDelta;
        heatSliders[0].value = Mathf.Max(0, Mathf.Min(1, _heat));
        heatSliders[1].value = Mathf.Max(0, Mathf.Min(2, _heat) - 1);
        heatSliders[2].value = Mathf.Max(0, Mathf.Min(3, _heat) - 2);
        heatSliders[3].value = Mathf.Max(0, Mathf.Min(4, _heat) - 3);
        heatSliders[4].value = Mathf.Max(0, Mathf.Min(5, _heat) - 4);
    }

    private void Update()
    {
        timer.text = "Time Survived: " + Time.time.ToString("F2");
        abductees.text = "People abducted: " + _ac.abductees;
        PlayerPrefs.SetFloat("Time", Time.time);
        PlayerPrefs.SetInt("Abductees", _ac.abductees);
        PlayerPrefs.SetFloat("Heat", _heat);
        health.value = ((float)_playerHealth.GetHealth()) / 1000f;
    }
}
