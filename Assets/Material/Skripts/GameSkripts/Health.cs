using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    public bool isAlive;
    public static Health instance;

    public void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }
    public void UpdateHealth()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckIsAlive();
    }
    public void CheckIsAlive()
    {
        if (currentHealth > 0)
            isAlive = true;
        else
            isAlive = false;
    }
}
