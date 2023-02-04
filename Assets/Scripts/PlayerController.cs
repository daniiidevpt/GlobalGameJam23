using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce;
    [SerializeField] private float glidingForce;
    [SerializeField] private bool isInAir;
    private Vector2 moveDirection;
    private Vector2 glidingDirection;

    [Header("Grounded Settings")]
    [SerializeField] private Transform checkPosition;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    [Header("DirectionHelper Settings")]
    [SerializeField] private GameObject arrowPointer;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float rotationMin;
    [SerializeField] private float rotationMax;

    [Header("Force Indicator")]
    [SerializeField] private GameObject forceIndicator;
    [SerializeField] private GameObject forceVFX;
    [SerializeField] private float forceIndicatorMultiplier;

    Rigidbody2D rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        HandleClick();
        HandleDirectionHelper();
        HandleGlide();

        isInAir = IsGrounded() ? false : true;
    }

    private void HandleClick()
    {
        if (Input.GetButtonDown("Fire1") && !isInAir)
        {
            Debug.Log("Showing Helper");

            arrowPointer.SetActive(true);
            forceIndicator.SetActive(true);
        }

        if (Input.GetButtonUp("Fire1") && !isInAir)
        {
            Debug.Log("Hiding Helper");

            arrowPointer.SetActive(false);
            forceIndicator.SetActive(false);

            moveDirection = arrowPointer.transform.up;
            HandleMovement(moveDirection);
        }
    }

    private void HandleDirectionHelper()
    {
        if (Input.GetButton("Fire1") && !isInAir)
        {
            Debug.Log("Definig Force");

            //ARROW POINTER
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rotationAngle = Mathf.Clamp(mousePosition.x * rotationMultiplier, rotationMin, rotationMax);
            arrowPointer.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

            if (rotationAngle > 0f)
                glidingDirection = -transform.right;
            else
                glidingDirection = transform.right;

            //FORCE INDICATOR
            forceVFX.transform.localScale = new Vector3(Mathf.PingPong(Time.time * forceIndicatorMultiplier, 1.5f), 0.1f, 1f);
            float forceIndicator = forceVFX.transform.localScale.x;

            if (forceIndicator < 0.5f)
                moveForce = 1500;
            else if (forceIndicator < 1f)
                moveForce = 3000;
            else if (forceIndicator < 1.5f)
                moveForce = 5000;
        }
    }

    private void HandleGlide()
    {
        if (Input.GetButton("Fire2") && isInAir)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, -glidingForce);
            Debug.Log("Gliding");
        }
        else if(isInAir)
        {
            rb.gravityScale = 2f;
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        rb.AddForce(direction * moveForce * Time.deltaTime, ForceMode2D.Impulse);
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
}
