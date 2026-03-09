using UnityEngine;

public class CameraMouvement : MonoBehaviour
{
    private Transform _playerTransform;
    private const float _YAxis = 32f;
    private float _xAxis;
    private float _zAxis;

    private void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = players[0];
        _playerTransform = player.transform;
    }

    private void LateUpdate()
    {
        _xAxis = _playerTransform.position.x - 47.5f;
        _zAxis = _playerTransform.position.z - 24.5f;
        transform.position = new Vector3(_xAxis, _YAxis, _zAxis);
    }
}
