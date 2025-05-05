using UnityEngine;
using UnityEngine.Playables;

public class CheckCrystal : MonoBehaviour
{
    public PlayerMovment playerMovment;

    public int CristalNecessary;
    public PlayableDirector playableDirector;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerMovment.Cristal == CristalNecessary)
            {
                playableDirector.Play();
                Destroy(gameObject);
            }
        }
    }

}
