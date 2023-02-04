using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    UIManager ui;

    [SerializeField]
    float maxTime;
    float currentTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ui = UIManager.instance;
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
    }

    void DecreaseTime()
    {
        currentTime -= Time.deltaTime;
        ui.DepleteWaterBar(currentTime, maxTime);

        if(currentTime <= 0)
        {
            currentTime = 0;
            Time.timeScale = 0;
            ui.GameOver();
        }

        if (currentTime >=  maxTime)
        {
            currentTime = maxTime;
        }
    }

    public void AddTime(float amountTime)
    {
        currentTime += amountTime;
    }

    public void GameWon()
    {
        print("GAME WON");
        Time.timeScale = 0;
        //ui.VictoryScreen();
    }
}
