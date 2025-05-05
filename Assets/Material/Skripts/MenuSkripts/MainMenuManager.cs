using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public ButtonNavigator buttonNavigator;
    public SceneLoader sceneLoader;
    [Header("��������� �������� ����")]
    public Animator loadMainMenu;
    public GameObject loadPanel;
    [Header(" �������� ���� �� ����")]
    public GameObject loadPanelGame;
    public Animator animPanelGame;

    public int reloading = 0;

    void Start()
    {
        // ������������ �� ����
        if (reloading == 0)
        {
            loadPanelGame.SetActive(false);
            loadPanel.SetActive(true);
            loadMainMenu.Play("LoadMainMenu");
        }
        else if (reloading == 1)
        {
            loadPanel.SetActive(false);
            loadPanelGame.SetActive(true);
        }
        
    }
    void Update()
    {
        if (animPanelGame.GetCurrentAnimatorStateInfo(0).IsName("LoadMenu") &&
            animPanelGame.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            loadPanelGame.SetActive(false);
        }
        // ���������, ��������� �� �������� �������� �������� ����
        if (loadMainMenu.GetCurrentAnimatorStateInfo(0).IsName("LoadMainMenu") &&
            loadMainMenu.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            loadPanel.SetActive(false);
            reloading = 1; // ������������� reloading � 1 ����� ��������� �����
        }
       
    }
    private void OnDisable()
    {
        // ��������� �������� reloading ��� ���������� �����
        PlayerPrefs.SetInt("Reloading", reloading);
    }

    private void OnEnable()
    {
        // ��������� �������� reloading ��� �������� �����
        reloading = PlayerPrefs.GetInt("Reloading", 0);
    }
  
}
