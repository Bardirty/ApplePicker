using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int apples = 0;
    public int goldenApples = 0;
    public int appleLimit = 10;
    [SerializeField][Range(0, 100)] private float sensetivity = 20;
    [Range(0, 1000)] public float speed = 75;
    [SerializeField][Range(0, 1000)] private float jumpForce = 250;
    [SerializeField] private TextMeshProUGUI appleCountInfo = null;
    [SerializeField] private TextMeshProUGUI goldenAppleCountInfo = null;
    public int isPaused = 1;
    private Camera mainCamera;
    private Rigidbody rb;
    private float rotationY = 0f;
    private bool isGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        float jump = isGrounded ? Input.GetAxis("Jump") : 0f;
        rb.velocity = (transform.forward * moveZ + transform.right * moveX +
            transform.up * (rb.velocity.y + jump * jumpForce)) * isPaused;
        float rotationX = Input.GetAxis("Mouse X") * Time.deltaTime * sensetivity * isPaused;
        transform.Rotate(0, rotationX, 0);
        rotationY += -Input.GetAxis("Mouse Y") * Time.deltaTime * sensetivity * isPaused;
        mainCamera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rotationY, -90f, 90f), mainCamera.transform.localRotation.y, mainCamera.transform.localRotation.z);
        appleCountInfo.text = $"Apples: {apples} / {appleLimit}";
        goldenAppleCountInfo.text = $"Golden Apples: {goldenApples}";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = true;
        else if (apples < appleLimit && collision.transform.CompareTag("Apple"))
        {
            apples++;
            Destroy(collision.gameObject);
        }
        else if(collision.transform.CompareTag("GoldenApple"))
        {
            goldenApples++;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = false;
    }
}