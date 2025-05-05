using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public MainMenuManager mainMenu;
    private ButtonNavigator buttonNavigator; // Ссылка на компонент GameState

    [Header("из меню в игру")]
    public Animator startGame;
    public GameObject startGamePanel;

    [System.Obsolete]
    private void Start()
    {
        buttonNavigator = FindObjectOfType<ButtonNavigator>();
    }

    public void LoadLevel(int levelIndex)
    {
        if (buttonNavigator != null)
        {
            buttonNavigator.Played = false;
            buttonNavigator.SaveVolume(); // Устанавливаем Played в true и сохраняем состояние
        }

        startGamePanel.SetActive(true);
        startGame.Play("StartGamePannel");

        // Используем корутину для ожидания завершения анимации
        StartCoroutine(WaitForAnimationAndLoadScene(levelIndex));
    }

    private IEnumerator WaitForAnimationAndLoadScene(int levelIndex)
    {
        // Ждем завершения анимации
        while (!startGame.GetCurrentAnimatorStateInfo(0).IsName("StartGamePannel") ||
               startGame.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; // Ждем один кадр
        }

        SceneManager.LoadScene(levelIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        mainMenu.reloading = 0;
        Application.Quit();
    }
}
