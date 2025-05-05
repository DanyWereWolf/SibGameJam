using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance;
    private EventInstance musicEventAmbient;
    private EventInstance musicEventOnClick;
    private bool isMusicPlaying = false; // Флаг для проверки, играет ли музыка

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartMusic();
    }
    public void StartMusic()
    {
        musicEventAmbient = RuntimeManager.CreateInstance("event:/Ambient");
        musicEventOnClick = RuntimeManager.CreateInstance("event:/Click");

        // Запускаем музыку только если она еще не играет
        if (!isMusicPlaying)
        {
            musicEventAmbient.start();
            isMusicPlaying = true; // Устанавливаем флаг, что музыка играет
        }

        Vector3 position = transform.position;
        musicEventAmbient.set3DAttributes(RuntimeUtils.To3DAttributes(position));
    }
    public void OnClick()
    {
        musicEventOnClick.start();
    }
}
