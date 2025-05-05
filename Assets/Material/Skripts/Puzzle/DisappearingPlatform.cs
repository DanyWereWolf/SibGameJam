using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public GameObject Platform;
    public float disappearTime = 2f; // �����, ����� ������� ��������� ��������
    public float appearTime = 2f;     // �����, ����� ������� ��������� ����� ����������

    private void Start()
    {
        StartCoroutine(ManagePlatform());
    }

    private IEnumerator ManagePlatform()
    {
        while (true) // ����������� ���� ��� ���������� ��������
        {
            // ������������ ���������
            yield return new WaitForSeconds(disappearTime); // ����, ���� ��������� �� ��������
            Platform.SetActive(false); // �������� ���������

            // �������� ����� ���������� ���������
            yield return new WaitForSeconds(appearTime); // ����, ���� ��������� �� ��������
            Platform.SetActive(true); // ���������� ���������
        }
    }
}
