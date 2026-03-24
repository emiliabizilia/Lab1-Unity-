using UnityEngine;

public class SpiralMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateSpeed = 50f;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}