using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7;
    public float jumpForce = 7;
    public Text scoreText;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded = false;
    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateScoreText();
    }

    void Update()
    {
        float richtung = Input.GetAxis("Horizontal");
        Move(richtung);
        Jump();
    }

    void Move(float direction)
    {
        float moveAmount = direction * moveSpeed * Time.deltaTime;
        transform.Translate(moveAmount, 0, 0);

        anim.SetBool("isRunning", Mathf.Abs(direction) > 0);

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bronze"))
        {
            score += 1;
            Destroy(other.gameObject);
            UpdateScoreText();
        }
        else if (other.gameObject.CompareTag("silver"))
        {
            score += 2;
            Destroy(other.gameObject);
            UpdateScoreText();
        }
        else if (other.gameObject.CompareTag("gold"))
        {
            score += 3;
            Destroy(other.gameObject);
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    //GithubTest
    //githubTestno.2
}
