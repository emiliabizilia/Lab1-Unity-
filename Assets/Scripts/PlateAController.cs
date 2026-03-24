using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * direction * speed * Time.deltaTime);

        if (Vector3.Distance(startPos, transform.position) >= distance)
        {
            direction *= -1;
        }
    }
}
