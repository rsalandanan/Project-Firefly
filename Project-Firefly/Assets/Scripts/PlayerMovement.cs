using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float jumpForce;
   private Rigidbody2D _rigidbody2D;
   private BoxCollider2D _boxCollider2D;
   [SerializeField] private LayerMask canJump;
   public GameObject projectilePrefab;
   public Transform projectileSpawn;
   private float _timer;
   public float shootTime;

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
      if (Input.GetKey(KeyCode.Mouse0) && IsGrounded())
      {
         _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
      }
   }

   private void Attack()
   { 
      _timer += Time.deltaTime;
      if (Input.GetKey(KeyCode.Mouse1) &&  _timer >= shootTime)
      {
         _timer = 0;
         Instantiate(projectilePrefab, projectileSpawn.transform.position, projectilePrefab.transform.rotation);
      }
      
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
      else if (Input.GetMouseButtonDown(1))
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
