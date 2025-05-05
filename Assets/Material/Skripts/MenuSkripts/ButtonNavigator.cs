using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonNavigator : MonoBehaviour
{
    [Header("Панели")]
    public GameObject panelOptions;
    public GameObject panelButtons;

    [Header("Слайдер")]
    public Slider volumeSlider;
    private const string VolumeKey = "VolumeLevel";
    private const string PlayedKey = "Played";

    [Header("Если игрок играл")]
    public Text PlayedText;
    public bool Played;

    void Start()
    {
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

        // Проверяем, существует ли сохранённое значение
        if (PlayerPrefs.HasKey(PlayedKey))
        {
            Played = PlayerPrefs.GetInt(PlayedKey) == 1; // Загружаем значение и преобразуем в bool
        }
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value); // Сохраняем текущее значение слайдера
        PlayerPrefs.SetInt(PlayedKey, Played ? 1 : 0); // Сохраняем значение как int (1 для true, 0 для false)
        PlayerPrefs.Save(); // Сохраняем изменения
    }
}
