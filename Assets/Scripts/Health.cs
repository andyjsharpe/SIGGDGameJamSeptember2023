using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] spawnOnDeath;
    [SerializeField] private float heatValue;

    private Heat _heat;

    //TODO: Make faster
    private void Start()
    {
        _heat = FindObjectOfType<Heat>();
    }

    public void Damage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth < 0)
        {
            var transform1 = transform;
            foreach (var g in spawnOnDeath)
            {
                Instantiate(g, transform1.position, transform1.rotation);
            }
            _heat.AddHeat(heatValue);
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return maxHealth;
    }
}
