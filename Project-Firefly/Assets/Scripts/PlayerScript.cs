using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerScript : MonoBehaviour
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
   
   public int hpPoint = 3;
   public TextMeshProUGUI hpUI;
   private bool _gotHit;
   private bool _isDead;
   

   private Animator _animator;
   private static readonly int State = Animator.StringToHash("state");
   private enum CharacterState {Running,Jumping, Falling, Attacking,GettingHit,Dead}

   public GameManager gameManager;
   

   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _boxCollider2D = GetComponent<BoxCollider2D>();
      _animator = GetComponent<Animator>();
      hpUI.text = "HEALTH: " + hpPoint;
   }

   private void Update()
   {
      Jump();
      Attack();
      CharacterAnimation();
   }

   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !_isDead)
      {
         _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
      }
   }

   private void Attack()
   {
      _timer += Time.deltaTime;
      if (Input.GetKey(KeyCode.Mouse0) &&  _timer >= shootTime && !_isDead)
      {
         Debug.Log("Attack!");
         _timer = 0;
         _isAttacking = true;
         Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, enemies);
         foreach (Collider2D enemyGameObject in enemy)
         {
            Destroy(enemyGameObject.gameObject, 0.1f);
            gameManager.killCount += 1;
         }
      }
      else
      {
         _isAttacking = false;
      }
   }

   private IEnumerator OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("EnemyProjectile"))
      {
         hpPoint--;
         hpUI.text = "HP: " + hpPoint;
         if (hpPoint == 0)
         {
            _isDead = true;
            yield return new WaitForSeconds(0.1f);
            
         }
         else
         {
            _gotHit = true;
            yield return new WaitForSeconds(0.1f);
            _gotHit = false;
         }
      }

      if (col.gameObject.CompareTag("Potion"))
      {
         hpPoint++;
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
      if (_rigidbody2D.velocity.y > .1f && !_isAttacking &&!_gotHit &&!_isDead)
      {
         state = CharacterState.Jumping;
      }
      else if (_rigidbody2D.velocity.y < -.1f && !_isAttacking &&!_gotHit &&!_isDead)
      {
         state = CharacterState.Falling;
      }
      else if (_isAttacking)
      {
         state = CharacterState.Attacking;
      }
      else if (_gotHit)
      {
         state = CharacterState.GettingHit;
      }
      else if ((_isDead))
      {
         state = CharacterState.Dead;
      }
      else
      {
         state = CharacterState.Running;
      }
      _animator.SetInteger(State,(int)state);
   }
}
