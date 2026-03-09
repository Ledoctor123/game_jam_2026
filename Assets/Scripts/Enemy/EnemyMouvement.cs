using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] public float walkSpeed = 8f;
    [SerializeField] public float rotationSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void FixedUpdate()
    {
        GoToPlayerRange();
    }

    private void GoToPlayerRange()
    {
        
    }
}
