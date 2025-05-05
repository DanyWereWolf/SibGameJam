using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance;
    private EventInstance musicEventAmbient;
    private EventInstance musicEventOnClick;
    private bool isMusicPlaying = false; // ���� ��� ��������, ������ �� ������

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

        // ��������� ������ ������ ���� ��� ��� �� ������
        if (!isMusicPlaying)
        {
            musicEventAmbient.start();
            isMusicPlaying = true; // ������������� ����, ��� ������ ������
        }

        Vector3 position = transform.position;
        musicEventAmbient.set3DAttributes(RuntimeUtils.To3DAttributes(position));
    }
    public void OnClick()
    {
        musicEventOnClick.start();
    }
}
