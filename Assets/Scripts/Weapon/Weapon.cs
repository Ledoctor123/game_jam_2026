using UnityEngine;
using static UnityEngine.Debug;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{
    private float _damage = 15f;
    private Component _ownerScript;

    void Awake()
    {
        _ownerScript = (Component)GetComponentInParent<Player>() ?? GetComponentInParent<Enemy>();

        if (_ownerScript == null)
        {
            LogError($"[Weapon] Aucun Player/Enemy trouvé dans les parents de {name}");
            return;
        }

        Log($"[Weapon] Owner script: {_ownerScript.GetType().Name} | Tag: {_ownerScript.gameObject.tag}");
    }

    void OnTriggerEnter(Collider other)
    {
        if (_ownerScript == null) return;

        // Case: Player Hits Enemy
        if (_ownerScript is Player && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }

        // Case: Enemy Hits Player
        if (_ownerScript is Enemy && other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }
    }
}