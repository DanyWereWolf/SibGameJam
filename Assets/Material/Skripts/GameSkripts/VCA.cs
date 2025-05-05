using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class VCASlider : MonoBehaviour
{
    
    [SerializeField] private FMOD.Studio.EventInstance vcaEvent;

    
    [Header("Слайдер")]
    [SerializeField] public Slider volumeSlider;
    private const string VolumeKey = "VolumeLevel";
    private const string PlayedKey = "Played";

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        LoadVolume();
    }
    void Update()
    {

    }
    public void LoadVolume()
    {
        // Проверяем, существует ли сохранённое значение
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float volume = PlayerPrefs.GetFloat(VolumeKey);
            volumeSlider.value = volume; // Устанавливаем значение слайдера
        }
        else
        {
            volumeSlider.value = 1.0f; // Устанавливаем значение по умолчанию, если нет сохранённого значения
        }
    }
    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0f, 1f);
        RuntimeManager.GetVCA("vca:/VCAmaster").setVolume(volume);
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value); // Сохраняем текущее значение слайдера
        PlayerPrefs.Save(); // Сохраняем изменения
    }
}