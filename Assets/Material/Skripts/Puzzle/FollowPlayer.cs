using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FollowPlayer : MonoBehaviour
{
    private EventInstance musicEventCristal;
    public GameObject player; // Персонаж, за которым будет следовать объект
    public float followDistance = 2f; // Расстояние, на котором объект будет следовать за персонажем
    public float followSpeed = 2f; // Скорость следования

    public bool isFollowing = false; // Флаг, указывающий, следует ли объект за игроком

    public void Start()
    {
        musicEventCristal = RuntimeManager.CreateInstance("event:/Cristal");
        Vector3 position = transform.position;
        musicEventCristal.set3DAttributes(RuntimeUtils.To3DAttributes(position));
    }
    private void Update()
    {
        // Если объект следует за игроком, обновляем его позицию
        if (isFollowing && player != null)
        {
            Vector3 targetPosition = player.transform.position + Vector3.up * followDistance;
            // Плавное движение к целевой позиции
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Начинаем следовать за игроком при касании
            isFollowing = true;
            musicEventCristal.start();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Останавливаем следование, если игрок покинул триггер
            isFollowing = false;
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
