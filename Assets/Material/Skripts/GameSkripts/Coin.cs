using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private EventInstance musicEventCoin;

    private void Start()
    {
        musicEventCoin = RuntimeManager.CreateInstance("event:/Coin");
        Vector3 position = transform.position;
        musicEventCoin.set3DAttributes(RuntimeUtils.To3DAttributes(position));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            musicEventCoin.start();
            Destroy(gameObject);
        }
    }

}
