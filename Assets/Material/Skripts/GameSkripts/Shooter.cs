using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireSpeed;
    [SerializeField] private Transform firePoint;

    public void Shoot(float direction)
    {
        GameObject currentBullet = Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
        Rigidbody2D currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();
        
        if (direction > 0)
        {
            currentBulletVelocity.linearVelocity = new Vector2(fireSpeed * 1, currentBulletVelocity.linearVelocity.y);
        }
        else
        {
            currentBulletVelocity.linearVelocity = new Vector2(fireSpeed * -1, currentBulletVelocity.linearVelocity.y);
        }
    }
}
