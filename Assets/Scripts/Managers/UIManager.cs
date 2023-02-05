using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public void DepleteWaterBar(float currentTime, float maxTime)
    {
        imageWaterBarFill.fillAmount = currentTime / maxTime;
    }

    public void GameOver()
    {
        ChangeScene("MainMenu");
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public IEnumerator VicotoryScene()
    {
        yield return new WaitForSeconds(5f);

        GameOver();
    }
}
