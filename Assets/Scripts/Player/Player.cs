using UnityEngine;
using Unity.Mathematics;
using static UnityEngine.Debug;

public class Player : MonoBehaviour
{
    private float _healthPoints;
    public float HealthPoints {get => _healthPoints;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _healthPoints = 50f;
    }

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;
        math.ceil(_healthPoints);
        Log($"Enemy took {damage} damage, and he has {_healthPoints} HP left");
        if (_healthPoints <= 0)
        {
            Log("Object Destroy");
            Destroy(gameObject);
        }
    }
}
