using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce;
    private Vector2 moveDirection;

    [Header("DirectionHelper Settings")]
    [SerializeField] private GameObject arrowPointer;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private GameObject forceIndicator;
    [SerializeField] private GameObject forceVFX;

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
            arrowPointer.SetActive(true);
            forceIndicator.SetActive(true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            arrowPointer.SetActive(false);
            forceIndicator.SetActive(false);

            moveDirection = arrowPointer.transform.up;
            HandleMovement(moveDirection);
        }
    }

    private void HandleDirectionHelper()
    {
        if (Input.GetButton("Fire1"))
        {
            //ARROW POINTER
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rotationAngle = Mathf.Clamp(mousePosition.x * rotationMultiplier, -90f, 90f);

            arrowPointer.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

            //FORCE INDICATOR
           
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        rb.AddForce(direction * moveForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
