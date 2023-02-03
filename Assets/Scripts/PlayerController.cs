using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce;
    private Vector2 moveDirection;
    private Vector2 mousePositionDown;
    private Vector2 mousePositionUp;

    [Header("DirectionHelper Settings")]
    [SerializeField] private GameObject arrowPointer;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private GameObject forceIndicator;

    Rigidbody2D rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        HandleClick();
        HandleDirectionHelper();
    }

    private void HandleClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mousePositionDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Click Pressed: " + mousePositionDown.x + " | " + mousePositionDown.y);
            
            arrowPointer.SetActive(true);
            forceIndicator.SetActive(true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            mousePositionUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Click Released: " + mousePositionUp.x + " | " + mousePositionUp.y);

            arrowPointer.SetActive(false);
            forceIndicator.SetActive(false);

            moveDirection = mousePositionDown - mousePositionUp;
            HandleMovement(moveDirection);
        }
    }

    private void HandleDirectionHelper()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            arrowPointer.transform.rotation = Quaternion.Euler(0f, 0f, mousePosition.x * rotationMultiplier);
            //NEED TO CLAMP ROTATION
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        rb.AddForce(direction * moveForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
