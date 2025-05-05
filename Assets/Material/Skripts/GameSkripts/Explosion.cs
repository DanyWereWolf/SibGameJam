using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab; // ������ ��������� ������
    public float explosionRadius = 5f; // ������ ������
    public float explosionForce = 10f; // ���� ������
    public float explosionDuration = 0.5f; // ����� ����� ���������

    public void TriggerExplosion(Vector2 position)
    {
        // �������� ��������� ������
        GameObject explosionEffect = Instantiate(explosionPrefab, position, Quaternion.identity);

        // ��������� ���� �������� � ������� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            // ��������, ��� ������ ����� Rigidbody2D
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ���������� ���� � �������
                Vector2 direction = (collider.transform.position - (Vector3)position).normalized; // ���������� position � Vector3
                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }

            // �������� ������ ��� �������� ��� ������
            if (collider.CompareTag("Destructible"))
            {
                Destroy(collider.gameObject);
            }
        }

        // �������� ��������� ����� ��������� �����
        Destroy(explosionEffect, explosionDuration);
    }
}
