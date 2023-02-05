using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gm;
    UIManager ui;

    private void Start()
    {
        gm = GameManager.instance;
        ui = UIManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().PlayVictory();
            CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
            cam.m_Lens.OrthographicSize = 3f;

            ui.StartCoroutine(ui.VicotoryScene());
        }
    }
}
