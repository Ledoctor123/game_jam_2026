using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using static UnityEngine.Debug;
using System.Collections;

public class Player : MonoBehaviour
{
    private float _timerSinceLastAttack;
    private float _healthPoints;
    private float _secondPerAttack;
    private const float _AttackDuration = 1f; 
    public float HealthPoints {get => _healthPoints;}
    public float SecondPerAttack {get => _secondPerAttack; set => _secondPerAttack = value;}
    public float TimerSinceLastAttack {get => _timerSinceLastAttack; set => _timerSinceLastAttack = value;} 


    // Input Actions
    private InputAction _attackAction;
    // States
    private bool _isAttacking;
    public bool IsAttacking {get => _isAttacking; set => _isAttacking = value;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _timerSinceLastAttack = 0f;
        _healthPoints = 50f;
        _secondPerAttack = 1.5f;

        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {   
        _timerSinceLastAttack += Time.deltaTime;
        
        if (_attackAction != null && _attackAction.WasPressedThisFrame())
        {
            _isAttacking = true;
            StartCoroutine(AttackDuration());
        }
    }

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;
        _healthPoints = math.ceil(_healthPoints);
        Log($"Enemy took {damage} damage, and he has {_healthPoints} HP left");
        if (_healthPoints <= 0)
        {
            Log("Object Destroy");
            Destroy(gameObject);
        }
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(_AttackDuration);
        _isAttacking = false;
    }
}
