using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("EnemyProjectile"))
      {
         Debug.Log("hit");
         Destroy(col.gameObject);
      }
   }
}
