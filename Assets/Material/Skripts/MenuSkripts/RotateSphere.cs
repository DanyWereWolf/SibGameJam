using UnityEngine;

public class RotateSphere : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool oppositely = false;
    void Update()
    {
        if (oppositely == false)
        {
            // Вращение вокруг оси Z
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (oppositely == true)
        {
            // Вращение вокруг оси Z
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

    }
}
