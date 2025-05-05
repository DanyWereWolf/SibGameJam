using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab; // Префаб эффектора взрыва
    public float explosionRadius = 5f; // Радиус взрыва
    public float explosionForce = 10f; // Сила взрыва
    public float explosionDuration = 0.5f; // Время жизни эффектора

    public void TriggerExplosion(Vector2 position)
    {
        // Создание эффектора взрыва
        GameObject explosionEffect = Instantiate(explosionPrefab, position, Quaternion.identity);

        // Получение всех объектов в радиусе взрыва
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            // Проверка, что объект имеет Rigidbody2D
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Применение силы к объекту
                Vector2 direction = (collider.transform.position - (Vector3)position).normalized; // Приведение position к Vector3
                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }

            // Удаление ящиков или объектов при взрыве
            if (collider.CompareTag("Destructible"))
            {
                Destroy(collider.gameObject);
            }
        }

        // Удаление эффектора через некоторое время
        Destroy(explosionEffect, explosionDuration);
    }
}
