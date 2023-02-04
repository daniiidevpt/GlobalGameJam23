using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    [Header("Water Bar")]
    [SerializeField]
    Image imageWaterBarFill;

    private void Awake()
    {
        if(instance == null)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DepleteWaterBar(float currentTime, float maxTime)
    {
        imageWaterBarFill.fillAmount = currentTime / maxTime;
    }

    public void GameOver()
    {
        print("GAME OVER UI");
    }

}
