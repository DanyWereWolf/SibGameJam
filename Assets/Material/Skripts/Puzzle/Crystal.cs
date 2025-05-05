using UnityEngine;

public class Crystal : MonoBehaviour
{
    public PlayerMovment playerMovment;
    public GameObject Cristal;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovment.Cristal ++;
            Destroy(gameObject, 0.3f);
        }
    }
}
