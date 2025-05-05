using System.Collections;
using UnityEngine;

public class SlowDownObject : MonoBehaviour
{
    [Header("������ ��� ����������")]
    public GameObject objectToSlowDown; // ������ �� ������, ������� ����� ���������

    [Header("��������� ����������")]
    public float slowDownFactor = 0.5f; // �� ������� ��������� ������
    public float slowDownDuration = 2f; // ������������ ����������

    private Rigidbody2D rb;
    private bool isSlowed = false;

    private void Awake()
    {
        if (objectToSlowDown != null)
        {
            rb = objectToSlowDown.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogWarning("objectToSlowDown �� �������� � ����������!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // �������� �� ������ ��� �������
        {
            if (!isSlowed && rb != null)
            {
                StartCoroutine(SlowDown());
            }
        }
    }

    private IEnumerator SlowDown()
    {
        isSlowed = true;

        // ��������� ������� ��������
        Vector2 originalVelocity = rb.linearVelocity;

        // ��������� ������
        rb.linearVelocity *= slowDownFactor;

        // ���� ��������� �����
        yield return new WaitForSeconds(slowDownDuration);

        // ���������� ������������ ��������
        rb.linearVelocity = originalVelocity;

        isSlowed = false;
    }
}
