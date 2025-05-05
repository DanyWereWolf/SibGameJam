using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public GameObject Platform;
    public float disappearTime = 2f; // Время, через которое платформа исчезает
    public float appearTime = 2f;     // Время, через которое платформа снова появляется

    private void Start()
    {
        StartCoroutine(ManagePlatform());
    }

    private IEnumerator ManagePlatform()
    {
        while (true) // Бесконечный цикл для повторения процесса
        {
            // Исчезновение платформы
            yield return new WaitForSeconds(disappearTime); // Ждем, пока платформа не исчезнет
            Platform.SetActive(false); // Скрываем платформу

            // Ожидание перед появлением платформы
            yield return new WaitForSeconds(appearTime); // Ждем, пока платформа не появится
            Platform.SetActive(true); // Показываем платформу
        }
    }
}
