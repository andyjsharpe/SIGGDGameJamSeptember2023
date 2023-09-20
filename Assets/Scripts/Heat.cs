using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Heat : MonoBehaviour
{
    public static float HeatValue;
    [SerializeField] private Slider[] heatSliders;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI abductees;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject heli;
    [SerializeField] private GameObject f22;
    [SerializeField] private GameObject su;
    [SerializeField] private GameObject swept;
    [SerializeField] private GameObject b52;
    [SerializeField] private GameObject bomber;
    
    [SerializeField] private Slider health;

    private AlienControl _ac;
    private Transform _player;
    private Health _playerHealth;

    private float levelStart;

    public void Start()
    {
        _ac = FindObjectOfType<AlienControl>();
        _player = _ac.transform;
        _player.position = GetSpawn().position;
        _playerHealth = _player.GetComponent<Health>();
        StartCoroutine(nameof(SpawnThings));
        Heat.HeatValue = 0;
        heatSliders[0].value = 0;
        heatSliders[1].value = 0;
        heatSliders[2].value = 0;
        heatSliders[3].value = 0;
        heatSliders[4].value = 0;
        levelStart = Time.time;
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
        SpawnThis(heli);
        SpawnThis(heli);
        SpawnThis(heli);
        SpawnThis(heli);
        yield return new WaitForSeconds(10);
        while (true)
        {
            switch (Mathf.Sqrt(Heat.HeatValue))
            {
                case > 5:
                    if (Random.value > 0.5)
                    {
                        SpawnThis(Random.value > 0.75f ? b52 : bomber);
                    }
                    if (Random.value > 0.5)
                    {
                        SpawnThis(f22);
                    }
                    if (Random.value > 0.5)
                    {
                        SpawnThis(swept);
                    }
                    if (Random.value > 0.5)
                    {
                        SpawnThis(su);
                    }
                    break;
                case > 4:
                    if (Random.value > 0.5)
                    {
                        SpawnThis(b52);
                    }
                    if (Random.value > 0.5)
                    {
                        SpawnThis(f22);
                    }
                    if (Random.value > 0.5)
                    {
                        SpawnThis(swept);
                    }
                    if (Random.value > 0.5)
                    {
                        SpawnThis(su);
                    }
                    break;
                case > 3:
                    SpawnThis(swept);
                    SpawnThis(su);
                    break;
                case > 2:
                    SpawnThis(f22);
                    SpawnThis(swept);
                    break;
                case > 1:
                    SpawnThis(heli);
                    SpawnThis(f22);
                    break;
                default:
                    SpawnThis(heli);
                    SpawnThis(heli);
                    break;
            }

            yield return new WaitForSeconds(20 / Mathf.Sqrt(Mathf.Max(Heat.HeatValue, 1)));
        }
    }

    private void Update()
    {
        var correctedHeat = Mathf.Sqrt(Heat.HeatValue);
        timer.text = "Time Survived: " + Time.time.ToString("F2");
        abductees.text = "People abducted: " + AlienControl.Abductees;
        PlayerPrefs.SetFloat("Time", Time.time - levelStart);
        PlayerPrefs.SetInt("Abductees", AlienControl.Abductees);
        PlayerPrefs.SetFloat("Heat", correctedHeat);
        health.value = _playerHealth.GetHealth() / 1000f;
        
        heatSliders[0].value = Mathf.Max(0, Mathf.Min(1, correctedHeat));
        heatSliders[1].value = Mathf.Max(0, Mathf.Min(2, correctedHeat) - 1);
        heatSliders[2].value = Mathf.Max(0, Mathf.Min(3, correctedHeat) - 2);
        heatSliders[3].value = Mathf.Max(0, Mathf.Min(4, correctedHeat) - 3);
        heatSliders[4].value = Mathf.Max(0, Mathf.Min(5, correctedHeat) - 4);
    }
}
