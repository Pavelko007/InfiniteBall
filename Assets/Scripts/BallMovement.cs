using UnityEngine;

namespace InfiniteBall
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private float horSpeed = 5;
        [SerializeField] private float vertSpeed = 1;

        private Rigidbody2D rb;
        private bool isGrounded;
        private float horAxis;
        private const int VertSpeedMult = 100;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = horAxis * horSpeed;
            rb.velocity = newVelocity;
        }

        void Update()
        {
            horAxis = Input.GetAxis("Horizontal"); 

            if (Input.GetButtonDown("Jump") &&
                isGrounded)
            {
                rb.AddForce(Vector2.up * vertSpeed * rb.mass * VertSpeedMult);
            }
        }

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            isGrounded = true;
        }

        void OnCollisionExit2D(Collision2D collision2D)
        {
            isGrounded = false;
        }
    }
}
