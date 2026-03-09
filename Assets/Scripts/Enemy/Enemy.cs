using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private float _timerSinceLastAttack;
    private float _healthPoints;
    private float _secondPerAttack;
    public float HealthPoints {get => _healthPoints;}
    public float SecondPerAttack {get => _secondPerAttack; set => _secondPerAttack = value;}
    public float TimerSinceLastAttack {get => _timerSinceLastAttack; set => _timerSinceLastAttack = value;} 


    private void Start()
    {
        _timerSinceLastAttack = 0f;
        _healthPoints = 100f;
        _secondPerAttack = 1.5f;
    }

    private void Update()
    {
        _timerSinceLastAttack += Time.deltaTime;
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
