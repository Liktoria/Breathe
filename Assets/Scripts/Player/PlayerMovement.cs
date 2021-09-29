using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityY;
    [SerializeField] private Transform isGroundedChecker;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rememberGroundedFor;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private int defaultJumpCount = 1;
    [SerializeField] private Sprite walkingSprite;
    [SerializeField] private Sprite standingSprite;
    private bool isFalling = false;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float lastTimeGrounded;
    private int jumpCounter;
    private bool jumping = false;
    private SpriteRenderer characterRenderer;
    private int walkingDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = new Vector2(0, gravityY);
        characterRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.GetInstance().gamePaused)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            MoveCharacter();
            Jump();
            AdjustFalling();
            if (transform.position.y < -18)
            {
                LevelManager.GetInstance().currentPlayerHealth = 0;
                GetComponent<PlayerHealth>().TakeHit();
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void MoveCharacter()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * movementSpeed;
        if (!GetComponent<Shooting>().isShooting)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (characterRenderer.sprite != walkingSprite)
                {
                    characterRenderer.sprite = walkingSprite;
                }
                if (characterRenderer.flipX)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player Character/Turn", GetComponent<Transform>().position);
                }
                characterRenderer.flipX = false;
                walkingDirection = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (characterRenderer.sprite != walkingSprite)
                {
                    characterRenderer.sprite = walkingSprite;
                }
                if (!characterRenderer.flipX)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player Character/Turn", GetComponent<Transform>().position);
                }
                characterRenderer.flipX = true;
                walkingDirection = -1;
            }
            else
            {
                if (characterRenderer.sprite != standingSprite)
                {
                    characterRenderer.sprite = standingSprite;
                    if (walkingDirection < 0)
                    {
                        characterRenderer.flipX = true;
                    }
                }
            }
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (CheckIfGrounded() || Time.time - lastTimeGrounded <= rememberGroundedFor || jumpCounter > 0)
            {
                jumping = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player Character/Jump", GetComponent<Transform>().position);
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Roanoke Barks/Jump Emote", GetComponent<Transform>().position);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCounter--;
            }
        }
    }

    private bool CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, groundRadius, groundLayer);

        if (collider != null)
        {
            if (isFalling)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Roanoke Barks/Landing Emote", GetComponent<Transform>().position);
                isFalling = false;
            }
            isGrounded = true;
            jumpCounter = defaultJumpCount;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
        return isGrounded;
    }

    void AdjustFalling()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;            
            if (!isFalling && !CheckIfGrounded())
            {
                isFalling = true;
            }
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
