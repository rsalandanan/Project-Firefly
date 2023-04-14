using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float projectileSpeed;
    private float _despawnTime = 4f;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody2D.velocity = Vector2.right * projectileSpeed;
        Destroy(gameObject,_despawnTime);
    }
}
