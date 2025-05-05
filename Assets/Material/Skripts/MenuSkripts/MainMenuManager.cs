using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public ButtonNavigator buttonNavigator;
    public SceneLoader sceneLoader;
    [Header("Начальная загрузка меню")]
    public Animator loadMainMenu;
    public GameObject loadPanel;
    [Header(" загрузка меню из игры")]
    public GameObject loadPanelGame;
    public Animator animPanelGame;

    public int reloading = 0;

    void Start()
    {
        // Перезагрузки не было
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
        // Проверяем, завершена ли анимация загрузки главного меню
        if (loadMainMenu.GetCurrentAnimatorStateInfo(0).IsName("LoadMainMenu") &&
            loadMainMenu.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            loadPanel.SetActive(false);
            reloading = 1; // Устанавливаем reloading в 1 перед загрузкой сцены
        }
       
    }
    private void OnDisable()
    {
        // Сохранить значение reloading при выключении сцены
        PlayerPrefs.SetInt("Reloading", reloading);
    }

    private void OnEnable()
    {
        // Загрузить значение reloading при загрузке сцены
        reloading = PlayerPrefs.GetInt("Reloading", 0);
    }
  
}
