using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBoost : MonoBehaviour
{
    [SerializeField]
    float bounceForce;
    [SerializeField]
    Vector2 bounceDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Time.deltaTime * new Vector2(bounceDirection.x, bounceDirection.y) * bounceForce, ForceMode2D.Impulse);
        }
    }
}
