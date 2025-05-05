using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float damage;
    private Health health;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Players"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
