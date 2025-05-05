using UnityEngine;

public class SwingingImage : MonoBehaviour
{
    public float swingAmount = 0.5f; // ������������ ���� �����������
    public float swingSpeed = 2f;     // �������� �����������

    private Vector3 initialPosition;

    private void Start()
    {
        // ��������� ��������� ������� �������
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // ��������� ����� ��������� �� ������ ���������
        float newX = initialPosition.x + Mathf.Sin(Time.time * swingSpeed) * swingAmount;
        transform.localPosition = new Vector3(newX, initialPosition.y, initialPosition.z);
    }
}

