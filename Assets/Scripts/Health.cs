using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] spawnOnDeath;
    [SerializeField] private float heatValue;

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
            FindObjectOfType<Heat>().AddHeat(heatValue);
            Destroy(gameObject);
        }
    }
}
