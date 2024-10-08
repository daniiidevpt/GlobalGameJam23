using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    AudioManager am;

    [Header("Movement Settings")]
    [SerializeField] private float moveForce;
    [SerializeField] private float glidingForce;
    [SerializeField] private bool isInAir;
    private Vector2 moveDirection;

    [Header("Grounded Settings")]
    [SerializeField] private Transform checkPosition;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    private bool alreadyHitGround;

    [Header("DirectionHelper Settings")]
    [SerializeField] private GameObject arrowPointer;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float rotationMin;
    [SerializeField] private float rotationMax;

    [Header("Force Indicator")]
    [SerializeField] private GameObject forceIndicator;
    [SerializeField] private Image forceVFX;
    [SerializeField] private float forceIndicatorMultiplier;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    Transform currentCheckpointPos;
    [SerializeField]
    Transform initialPos;

    private void Start()
    {
       am = AudioManager.instance;
       rb = GetComponent<Rigidbody2D>(); 
       anim = GetComponentInChildren<Animator>();
       sr = anim.gameObject.GetComponent<SpriteRenderer>();
       currentCheckpointPos = initialPos;
    }

    private void Update()
    {
        HandleClick();
        HandleDirectionHelper();
        HandleGlide();

        isInAir = IsGrounded() ? false : true;

        if (isInAir)
        {
            alreadyHitGround = false;
        }
    }

    private void HandleClick()
    {
        if (Input.GetButtonDown("Fire1") && !isInAir)
        {
            Debug.Log("Showing Helper");

            arrowPointer.SetActive(true);
            forceIndicator.SetActive(true);
            am.Play("PlayerHoldToThrow", gameObject);
        }

        if (Input.GetButtonUp("Fire1") && !isInAir)
        {
            Debug.Log("Hiding Helper");

            arrowPointer.SetActive(false);
            forceIndicator.SetActive(false);
            am.Play("PlayerThrown", gameObject);
            anim.SetBool("gliding", true);

            moveDirection = arrowPointer.transform.up;
            HandleMovement(moveDirection);
        }
    }

    private void HandleDirectionHelper()
    {
        if (Input.GetButton("Fire1") && !isInAir)
        {
            Debug.Log("Defining Force");

            //ARROW POINTER
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePosition - arrowPointer.transform.position;

            arrowPointer.transform.rotation = Quaternion.LookRotation(arrowPointer.transform.forward, rotation);

            Vector3 euler = arrowPointer.transform.eulerAngles;
            if (euler.z > 180) euler.z = euler.z - 360;
            euler.z = Mathf.Clamp(euler.z, rotationMin, rotationMax);
            arrowPointer.transform.eulerAngles = euler;

            if(euler.z > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }

            //FORCE INDICATOR
            forceVFX.fillAmount = Mathf.PingPong(Time.time * forceIndicatorMultiplier, 1f);

            if (forceVFX.fillAmount < 0.2f)
                moveForce = 8f;
            else if (forceVFX.fillAmount < 0.5f)
                moveForce = 10f;
            else if (forceVFX.fillAmount < 0.8f)
                moveForce = 15f;
            else if (forceVFX.fillAmount < 1f)
                moveForce = 20f;
        }
    }

    private void HandleGlide()
    {
        if(Input.GetButtonDown("Fire2") && isInAir)
        {
            am.Play("PlayerGliding");
        }

        if (Input.GetButton("Fire2") && isInAir)
        {
            Debug.Log("Gliding");
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, -glidingForce);
        }
        else if(isInAir)
        {
            rb.gravityScale = 2f;
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        rb.AddForce(direction * moveForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(checkPosition.position, checkRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkPosition.position, checkRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3 && !alreadyHitGround)
        {
            anim.SetBool("gliding", false);
            am.Play("PlayerHitGround");
            alreadyHitGround = true;
        }
    }

    public void LoadCheckpoint()
    {
        //am.FadeOutSound("PlayerGliding");
        //am.Stop("PlayerHitGround");
        //am.Stop("PlayerThrown");
        am.Play("PlayerHurt");
        gameObject.transform.position = currentCheckpointPos.position;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("loadCheckpoint");
    }

    public void SaveCheckpoint(Transform savePos)
    {
        currentCheckpointPos = savePos;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("checkpoint");
    }

    public void PlayVictory()
    {
        anim.SetTrigger("victory");
    }
}
