using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWeb : MonoBehaviour
{
    [SerializeField]
    float speedDivider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("PlayerController Before MoveForce: " + collision.gameObject.GetComponent<Rigidbody2D>().velocity);
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity / speedDivider;
        print("PlayerController After MoveForce: " + collision.gameObject.GetComponent<Rigidbody2D>().velocity);
    }
}
