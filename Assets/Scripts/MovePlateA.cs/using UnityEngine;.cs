using UnityEngine;

public class MovePlateA : MonoBehaviour
{
    public Vector3 pointB = new Vector3(5, 0, 0); // кінець відрізка
    public float speed = 2f;
    private Vector3 startPos;
    private bool goingToB = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (goingToB)
        {
            transform.Translate((pointB - startPos).normalized * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB) < 0.1f)
                goingToB = false;
        }
        else
        {
            transform.Translate((startPos - pointB).normalized * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPos) < 0.1f)
                goingToB = true;
        }
    }
}