using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
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
    [SerializeField] private GameObject F22;
    [SerializeField] private GameObject Su;
    [SerializeField] private GameObject Swept;
    [SerializeField] private GameObject B52;
    [SerializeField] private GameObject Nuker;

    private AlienControl ac;
    private Transform player;

    private void Start()
    {
        ac = FindObjectOfType<AlienControl>();
        player = ac.transform;
        player.position = GetSpawn().position;
        StartCoroutine(nameof(SpawnThings));
    }

    private Transform GetSpawn()
    {
        while (true)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (Vector3.SqrMagnitude(point.position - player.position) < 144)
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
                SpawnThis(Nuker);
                SpawnThis(F22);
                SpawnThis(Swept);
                SpawnThis(Swept);
                SpawnThis(Su);
                SpawnThis(B52);
            }
            else if (_heat > 4)
            {
                SpawnThis(F22);
                SpawnThis(Swept);
                SpawnThis(Swept);
                SpawnThis(Su);
                SpawnThis(B52);
            }
            else if (_heat > 3)
            {
                SpawnThis(F22);
                SpawnThis(Swept);
                SpawnThis(Swept);
                SpawnThis(Su);
            }
            else if (_heat > 2)
            {
                SpawnThis(F22);
                SpawnThis(F22);
                SpawnThis(Swept);
                SpawnThis(Swept);
                SpawnThis(Swept);
                SpawnThis(heli);
                SpawnThis(heli);
            }
            else if (_heat > 1)
            {
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(F22);
            }
            else
            {
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(heli);
                SpawnThis(heli);
            }
            yield return new WaitForSeconds(10 / Mathf.Sqrt(Mathf.Max(_heat, 1)));
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
        abductees.text = "People abducted: " + ac.abductees;
    }
}
