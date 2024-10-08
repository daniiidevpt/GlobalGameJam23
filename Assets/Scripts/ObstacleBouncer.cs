using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBouncer : MonoBehaviour
{
    [SerializeField]
    float bounceForce;
    [SerializeField]
    Vector2 bounceDirection;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("bounce");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Time.deltaTime * new Vector2(bounceDirection.x, bounceDirection.y) * bounceForce, ForceMode2D.Impulse);
        }
    }
}
