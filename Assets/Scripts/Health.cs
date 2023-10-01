using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _health;
    [SerializeField] private GameObject[] spawnOnDeath;
    [SerializeField] private float heatValue;

    private Heat _heat;

    private void Start()
    {
        _health = maxHealth;
    }

    public void Damage(float damage)
    {
        _health -= damage;
        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
        if (_health >= 0) return;
        var transform1 = transform;
        foreach (var g in spawnOnDeath)
        {
            Instantiate(g, transform1.position, transform1.rotation);
        }

        Heat.HeatValue += heatValue;
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return _health;
    }
}
