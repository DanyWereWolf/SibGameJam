using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    private Health health;
    private Animator animator;

    public SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer
    public Color damageColor = Color.red; // Цвет, который будет применяться при получении урона
    public float flashDuration = 0.1f; // Время, на которое цвет будет изменен

    private Color originalColor; // Исходный цвет спрайт
    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    private void Start()
    {
        // Сохраняем исходный цвет спрайта
        originalColor = spriteRenderer.color;
    }
    private void Update()
    {
        if (health.isAlive == false)
        {
            Debug.Log("Враг повержен");
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
        // Запускаем корутину для изменения цвета
        StartCoroutine(FlashDamageColor());
    }
    private IEnumerator FlashDamageColor()
    {
        // Меняем цвет на красный
        spriteRenderer.color = damageColor;

        // Ждем указанное время
        yield return new WaitForSeconds(flashDuration);

        // Возвращаем исходный цвет
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
