using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    private Health health;
    private Animator animator;

    public SpriteRenderer spriteRenderer; // ������ �� ��������� SpriteRenderer
    public Color damageColor = Color.red; // ����, ������� ����� ����������� ��� ��������� �����
    public float flashDuration = 0.1f; // �����, �� ������� ���� ����� �������

    private Color originalColor; // �������� ���� ������
    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    private void Start()
    {
        // ��������� �������� ���� �������
        originalColor = spriteRenderer.color;
    }
    private void Update()
    {
        if (health.isAlive == false)
        {
            Debug.Log("���� ��������");
            animator.SetBool("Die", true);
            StartCoroutine(DestroyObject());
            gameObject.GetComponent<EnamyController>().enabled = false;
        }
    }
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    public void TakeDamage()
    {
        // ��������� �������� ��� ��������� �����
        StartCoroutine(FlashDamageColor());
    }
    private IEnumerator FlashDamageColor()
    {
        // ������ ���� �� �������
        spriteRenderer.color = damageColor;

        // ���� ��������� �����
        yield return new WaitForSeconds(flashDuration);

        // ���������� �������� ����
        spriteRenderer.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Players"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
    
}
