using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float jumpForce;
   private Rigidbody2D _rigidbody2D;
   private BoxCollider2D _boxCollider2D;
   [SerializeField] private LayerMask canJump;

   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _boxCollider2D = GetComponent<BoxCollider2D>();
   }

   private void Update()
   {
      if (Input.GetKey(KeyCode.Mouse0) && IsGrounded())
      {
         _rigidbody2D.AddForce(Vector2.up *jumpForce);
      }
   }

   private bool IsGrounded()
   {
      var bounds = _boxCollider2D.bounds;
      return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 1f, canJump);
   }
}
