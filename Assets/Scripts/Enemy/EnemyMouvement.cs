using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMouvement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] public float walkSpeed = 8f;
    [SerializeField] public float rotationSpeed = 10f;
    private Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        GoToPlayerRange();
    }

    private void GoToPlayerRange()
    {
        
    }
}
