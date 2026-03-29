using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Налаштування руху")]
    public float forwardSpeed = 7f;   // Швидкість вперед
    public float sideSpeed = 10f;     // Швидкість вліво-вправо (збільшена для різкості)
    public float jumpForce = 6f;      // Сила стрибка

    [Header("Налаштування прискорення (n секунд)")]
    public float boostMultiplier = 2f; 
    public float maxBoostTime = 3f;   // n секунд прискорення
    private float currentBoostTime;
    private bool isBoosting = false;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isFinished = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBoostTime = maxBoostTime;
    }

    void Update()
    {
        if (isFinished) return;

        // 5. Стрибок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // 7. Логіка прискорення на Shift
        if (Input.GetKey(KeyCode.LeftShift) && currentBoostTime > 0)
        {
            isBoosting = true;
            currentBoostTime -= Time.deltaTime;
        }
        else
        {
            isBoosting = false;
        }

        // 6. Повернення на старт при падінні в яму
        if (transform.position.y < -5f)
        {
            RestartLevel();
        }
    }

    void FixedUpdate()
    {
        if (isFinished) return;

        // Визначаємо швидкість
        float currentForwardSpeed = isBoosting ? forwardSpeed * boostMultiplier : forwardSpeed;

        // 2. Отримуємо різкий ввід (A/D)
        float horizontal = Input.GetAxisRaw("Horizontal");

        // 1. Рух через Velocity (щоб гравець падав у ями природньо)
        Vector3 newVelocity = new Vector3(horizontal * sideSpeed, rb.linearVelocity.y, currentForwardSpeed);
        rb.linearVelocity = newVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Перевірка чи ми на землі (тег Ground)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // 3. Зіткнення з перешкодою (тег Obstacle)
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            RestartLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 4. Фініш (об'єкт з тегом Finish та галочкою Is Trigger)
        if (other.CompareTag("Finish"))
        {
            isFinished = true;
            rb.linearVelocity = Vector3.zero;
            Debug.Log("Рівень пройдено!");
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}