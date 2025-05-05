using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FollowPlayer : MonoBehaviour
{
    private EventInstance musicEventCristal;
    public GameObject player; // ��������, �� ������� ����� ��������� ������
    public float followDistance = 2f; // ����������, �� ������� ������ ����� ��������� �� ����������
    public float followSpeed = 2f; // �������� ����������

    public bool isFollowing = false; // ����, �����������, ������� �� ������ �� �������

    public void Start()
    {
        musicEventCristal = RuntimeManager.CreateInstance("event:/Cristal");
        Vector3 position = transform.position;
        musicEventCristal.set3DAttributes(RuntimeUtils.To3DAttributes(position));
    }
    private void Update()
    {
        // ���� ������ ������� �� �������, ��������� ��� �������
        if (isFollowing && player != null)
        {
            Vector3 targetPosition = player.transform.position + Vector3.up * followDistance;
            // ������� �������� � ������� �������
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �������� ��������� �� ������� ��� �������
            isFollowing = true;
            musicEventCristal.start();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ������������� ����������, ���� ����� ������� �������
            isFollowing = false;
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
