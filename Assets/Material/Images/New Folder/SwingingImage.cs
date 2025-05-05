using UnityEngine;

public class SwingingImage : MonoBehaviour
{
    public float swingAmount = 0.5f; // Максимальный угол покачивания
    public float swingSpeed = 2f;     // Скорость покачивания

    private Vector3 initialPosition;

    private void Start()
    {
        // Сохраняем начальную позицию объекта
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // Вычисляем новое положение на основе синусоиды
        float newX = initialPosition.x + Mathf.Sin(Time.time * swingSpeed) * swingAmount;
        transform.localPosition = new Vector3(newX, initialPosition.y, initialPosition.z);
    }
}

