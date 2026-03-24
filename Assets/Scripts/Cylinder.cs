using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float angle = 30f;
    public float speed = 2f;

    void Update()
    {
        float currentAngle = angle * Mathf.Sin(Time.time * speed);
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
