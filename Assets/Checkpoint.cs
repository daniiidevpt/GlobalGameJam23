using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool alreadyPassed;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !alreadyPassed)
        {
            collision.gameObject.GetComponent<PlayerController>().SaveCheckpoint(transform);
            alreadyPassed = true;
        }
    }
    
}
