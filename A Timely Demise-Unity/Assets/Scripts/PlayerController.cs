/****
 * Created By: Jacob Sharp
 * Date Created: April 18, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: April 24, 2022
 * 
 * Description: Manage player movement/interactions
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public bool dead = false;
    // references to components
    private Rigidbody rb;
    private Collider col;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audio;
    private TimeRecorder recorder;
    public LayerMask ground;

    // variables for input
    [HideInInspector] public float moveInput;
    [HideInInspector] public bool jumpInput, hasJumped;
    [HideInInspector] public bool controlsEnabled = true;

    // variables for movement
    private Vector3 moveChange;
    [SerializeField] private float moveSpeed, jumpSpeed;
    public bool currentlyGrounded;
    public int lowerBound = -40;

    // variables for managing death
    [SerializeField] private float deathDelay;
    private float deathDelayTimer;
    private bool deathActive = false;

    [SerializeField] private int health; // player health

    // assign sound effects
    public AudioClip jumpSound;
    public AudioClip deathSound;

    private GameManager gameMan;


    private void Awake()
    {
        // get player components
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        recorder = GetComponent<TimeRecorder>();

        gameMan = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        // controls.Player.Quit.performed += _ => gameMan.quitGame();
    }

    private void Update()
    {
        // Read input if controls are enabled
        if (controlsEnabled)
        {
            moveInput = Input.GetAxis("Horizontal");
            if (!jumpInput) jumpInput = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
            if (!hasJumped) hasJumped = jumpInput; // used for time recorder

            // restart if R is pressed
            if (Input.GetKeyDown(KeyCode.R)) { dead = true; gameMan.GetComponent<AudioSource>().PlayOneShot(deathSound); gameMan.RestartLevel(); }
        }
    }

    void FixedUpdate()
    {
        // manage movement when the player isn't dead
        if (!deathActive)
        {
            move();
            jump();
            checkBounds();
        }

        // check to see if the player is dead/dying
        die();
        // if (isGrounded()) animator.SetBool("Grounded", true);
        // else animator.SetBool("Grounded", false);
    }

    private void move()
    {
        // Move the player
        moveChange = Vector3.zero;
        moveChange.x = moveInput * moveSpeed * Time.fixedDeltaTime;
        if (moveChange.x != 0 && rb.SweepTest(Vector3.right * Mathf.Sign(moveChange.x), out RaycastHit hit, Mathf.Abs(moveChange.x), QueryTriggerInteraction.Ignore)) // check for wall collisions
        {
            moveChange = Vector3.zero;
        }
        transform.position += moveChange;

        // Animation
        if (moveInput != 0) animator.SetBool("Walk", true);
        else animator.SetBool("Walk", false);

        // Sprite Flip
        if (moveInput > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void jump()
    {
        // jump if the player is on the ground and presses w/space
        if (jumpInput && isGrounded() && !deathActive)
        {
            jumpInput = false;
            audio.PlayOneShot(jumpSound);
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            // animator.SetTrigger("Jump");
        }
    }

    private bool isGrounded()
    {
        // return whether the player is on the ground
        currentlyGrounded = (Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.05f)
            || Physics.Raycast(transform.position - new Vector3(col.bounds.extents.x, 0, 0), Vector3.down, col.bounds.extents.y + 0.05f) 
            || Physics.Raycast(transform.position + new Vector3(col.bounds.extents.x, 0, 0), Vector3.down, col.bounds.extents.y + 0.05f));
        return currentlyGrounded;
    }

    private bool collideWalls() // OUTDATED
    {
        // return whether the player is colliding with walls
        Vector2 topLeft = transform.position;
        topLeft.x -= col.bounds.extents.x;
        topLeft.y += col.bounds.extents.y - 0.1f;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y - 0.1f;

        return Physics2D.OverlapArea(topLeft, bottomRight, ground);
    }

    private void checkBounds()
    {
        // restart the room when the player falls off the level
        if (transform.position.y < lowerBound)
        {
            gameMan.GetComponent<AudioSource>().PlayOneShot(deathSound);
            dead = true; gameMan.RestartLevel();
        }
    }

    public void takeDamage(int damage)
    {
        // reduce health
        health -= damage;
        // animator.SetTrigger("Hurt");

        // kill player if health reaches zero
        if (health <= 0)
        {
            // animator.SetTrigger("Dead");
            deathDelayTimer = deathDelay;
            deathActive = true;
        }

    }

    public void die()
    {
        // continue player death process and reset room if timer is up
        if (deathActive)
        {
            if (deathDelayTimer > 0) deathDelayTimer -= Time.fixedDeltaTime;
            gameMan.GetComponent<AudioSource>().PlayOneShot(deathSound);
            gameMan.RestartLevel();
        }
    }
}