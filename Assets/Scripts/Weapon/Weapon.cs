using UnityEngine;
using static UnityEngine.Debug;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{
    private float _damage = 15f;

    private Player _playerOwner;
    private Enemy _enemyOwner;

    [Header("Materials")]
    [SerializeField] private Material _weaponMaterial;
    [SerializeField] private Material _weaponAttackingMaterial;

    private Renderer _renderer;
    private bool _lastIsAttacking;

    void Awake()
    {
        _playerOwner = GetComponentInParent<Player>();
        _enemyOwner = GetComponentInParent<Enemy>();
        _renderer = GetComponent<Renderer>() ?? GetComponentInChildren<Renderer>();

        if (_playerOwner == null && _enemyOwner == null)
        {
            LogError($"[Weapon] Aucun Player/Enemy trouvé dans les parents de {name}");
            return;
        }

        if (_playerOwner != null) Log("[Weapon] Owner = Player");
        if (_enemyOwner != null) Log("[Weapon] Owner = Enemy");
    }

    void Update()
    {
        if (_playerOwner == null || _renderer == null) return;

        bool isAttacking = _playerOwner.IsAttacking;
        if (isAttacking == _lastIsAttacking) return;

        _lastIsAttacking = isAttacking;
        _renderer.material = isAttacking ? _weaponAttackingMaterial : _weaponMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        // Player -> Enemy
        if (_playerOwner != null && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null &&
                _playerOwner.TimerSinceLastAttack >= _playerOwner.SecondPerAttack &&
                _playerOwner.IsAttacking)
            {
                _playerOwner.TimerSinceLastAttack = 0f;
                enemy.TakeDamage(_damage);
            }
        }

        // Enemy -> Player
        if (_enemyOwner != null && other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();
            if (player != null && _enemyOwner.TimerSinceLastAttack >= _enemyOwner.SecondPerAttack)
            {
                _enemyOwner.TimerSinceLastAttack = 0f;
                player.TakeDamage(_damage);
            }
        }
    }
}