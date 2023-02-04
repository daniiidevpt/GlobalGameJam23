using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWater : MonoBehaviour
{
    GameManager gm;

    [SerializeField]
    float timeAmount;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.AddTime(timeAmount);
        }
    }
}
