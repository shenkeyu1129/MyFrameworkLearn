using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float playerSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v).normalized;

        rb.MovePosition(transform.position + dir * playerSpeed * Time.fixedDeltaTime);
    }
}
