using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonNavigator : MonoBehaviour
{
    [Header("������")]
    public GameObject panelOptions;
    public GameObject panelButtons;

    [Header("�������")]
    public Slider volumeSlider;
    private const string VolumeKey = "VolumeLevel";
    private const string PlayedKey = "Played";

    [Header("���� ����� �����")]
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
        // ���������, ���������� �� ���������� ��������
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float volume = PlayerPrefs.GetFloat(VolumeKey);
            volumeSlider.value = volume; // ������������� �������� ��������
        }
        else
        {
            volumeSlider.value = 1.0f; // ������������� �������� �� ���������, ���� ��� ����������� ��������
        }

        // ���������, ���������� �� ���������� ��������
        if (PlayerPrefs.HasKey(PlayedKey))
        {
            Played = PlayerPrefs.GetInt(PlayedKey) == 1; // ��������� �������� � ����������� � bool
        }
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value); // ��������� ������� �������� ��������
        PlayerPrefs.SetInt(PlayedKey, Played ? 1 : 0); // ��������� �������� ��� int (1 ��� true, 0 ��� false)
        PlayerPrefs.Save(); // ��������� ���������
    }
}
