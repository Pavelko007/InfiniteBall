using UnityEngine;

namespace InfiniteBall
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private float horSpeed = 5;
        [SerializeField] public float JumpHeight = 1;

        private Rigidbody2D rb;
        private bool isGrounded;
        private float horAxis;
        private float impulse;
        private const int VertSpeedMult = 100;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            impulse = Mathf.Sqrt(2f * -Physics2D.gravity.y * JumpHeight) / rb.mass;
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
                rb.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
            }
        }

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            SetGrounded(collision2D, true);
        }

        void OnCollisionExit2D(Collision2D collision2D)
        {
            SetGrounded(collision2D, false);
        }

        private void SetGrounded(Collision2D collision2D, bool grounded)
        {
            if (collision2D.gameObject.tag != "platform") return;
            isGrounded = grounded;
        }
    }
}
