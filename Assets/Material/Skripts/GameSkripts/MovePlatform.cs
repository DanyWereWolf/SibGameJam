using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public SliderJoint2D sliderJoint;
    public float motorSpeed = 1f;

    private void Start()
    {
        UpdateMotorSpeed(motorSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ElevatorStopper"))
        {
            Debug.Log("ห่๔๒");
            motorSpeed = -motorSpeed;
            UpdateMotorSpeed(motorSpeed);
        }
    }
  
    private void UpdateMotorSpeed(float speed)
    {
        JointMotor2D motor = sliderJoint.motor;
        motor.motorSpeed = speed;
        sliderJoint.motor = motor;
    }
}
