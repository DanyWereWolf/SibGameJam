using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer
    public Color damageColor = Color.red; // Цвет, который будет применяться при получении урона
    public float flashDuration = 0.5f; // Время, на которое цвет будет изменен

    private Color originalColor; // Исходный цвет спрайта

    private Health health;
    private Animator animator;
    public Image helBar;
    public float healFloat;
    private PlayerInput playerInput;
    private PlayerMovment playerMovment;

    public GameObject losPannel;
    private EventInstance musicEventDeath;

    private void Awake()
    {
        playerMovment = GetComponent<PlayerMovment>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    private void Start()
    {
        musicEventDeath = RuntimeManager.CreateInstance("event:/Death");
        Vector3 position = transform.position;
        musicEventDeath.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        // Сохраняем исходный цвет спрайта
        originalColor = spriteRenderer.color;
    }
    // Метод для получения урона
    public void TakeDamage()
    {
        // Запускаем корутину для изменения цвета
        StartCoroutine(FlashDamageColor());
    }

    public void Update()
    {
        healFloat = health.currentHealth / health.maxHealth;
        helBar.fillAmount = healFloat;

        if (health.isAlive == false)
        {

            playerMovment.enabled = false;
            playerInput.enabled = false;
            animator.SetBool("death", true);
            StartCoroutine(Lose());
            Debug.Log("Наш герой погиб");
        }
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
    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(1f);
        float direction = Input.GetAxis("Horizontal");
        playerMovment.UpdateSound(direction);
        musicEventDeath.start();
        losPannel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damageable"))
        {
            TakeDamage();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            health.currentHealth = 0;
            playerMovment.enabled = false;
            playerInput.enabled = false;
            animator.SetBool("death", true);
            StartCoroutine(Lose());
            Debug.Log("Наш герой погиб");
        }
    }
}