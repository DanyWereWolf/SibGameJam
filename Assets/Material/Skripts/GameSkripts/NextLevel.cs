using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel Instance { get; private set; }
    public GameManager gameManager;
    public GameObject nextLoadLevel;

    //private void Awake()
    //{
    //    // Проверяем, есть ли уже экземпляр LevelController
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новой сцены
    //    }
    //    else
    //    {
    //        Destroy(gameObject); // Удаляем дубликаты
    //    }
    //}

    void Start()
    {
        Vector3 position = transform.position;
        nextLoadLevel.SetActive(false);
    }
    public void _LevelLoad()
    {
        gameManager.EndGame();
    }
   
    private IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextLoadLevel.SetActive(true);
            StartCoroutine(nextLevel());
            _LevelLoad();
            
        }
    }
}
