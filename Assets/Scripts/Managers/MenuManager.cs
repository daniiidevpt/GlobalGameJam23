using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    AudioManager am;

    [SerializeField]
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        am = AudioManager.instance;
        am.Play("Title");
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
