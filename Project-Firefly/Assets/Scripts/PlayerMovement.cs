
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float jumpForce;
   private Rigidbody2D _rigidbody2D;
   private BoxCollider2D _boxCollider2D;
   [SerializeField] private LayerMask canJump;
   [SerializeField] private LayerMask enemies;
   private float _timer;
   public float shootTime;
   private bool _isAttacking;
   public GameObject attackPoint;
   public float attackRadius;
   

   private Animator _animator;
   private static readonly int State = Animator.StringToHash("state");
   private enum CharacterState {Running,Jumping, Falling, Attacking}
   

   private void Awake()
   {
      Cursor.visible = false;
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _boxCollider2D = GetComponent<BoxCollider2D>();
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      Jump();
      Attack();
      CharacterAnimation();
   }

   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
      {
         _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
      }
   }

   private void Attack()
   {
      
      _timer += Time.deltaTime;
      if (Input.GetKey(KeyCode.Mouse0) &&  _timer >= shootTime)
      {
         Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, enemies);
         foreach (Collider2D enemyGameObject in enemy)
         {
            Destroy(enemyGameObject.gameObject);
            Debug.Log("You hit an enemy");
         }
         _isAttacking = true;
         _timer = 0;
         Debug.Log("attack");
      }
      else
      {
         _isAttacking = false;
      }
   }

   private void OnDrawGizmos()
   {
      Gizmos.DrawWireSphere(attackPoint.transform.position,attackRadius);
   }

   private bool IsGrounded()
   {
      var bounds = _boxCollider2D.bounds;
      return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0.1f, canJump);
   }

   private void CharacterAnimation()
   {
      CharacterState state;
      if (_rigidbody2D.velocity.y > .1f)
      {
         state = CharacterState.Jumping;
      }
      else if (_rigidbody2D.velocity.y < -.1f)
      {
         state = CharacterState.Falling;
      }
      else if (_isAttacking)
      {
         state = CharacterState.Attacking;
      }
      else
      {
         state = CharacterState.Running;
      }
      _animator.SetInteger(State,(int)state);
   }
}
