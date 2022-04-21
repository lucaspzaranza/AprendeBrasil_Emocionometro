using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    /// <summary>
    /// If the width is greater > than the height, we game will
    /// choose the desktop orientation. Otherwise, mobile will be chosed.
    /// </summary>
    public bool IsDesktop => Screen.width > Screen.height;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {

        if (Screen.width > Screen.height)
            SceneManager.LoadScene(1); // Desktop
        else
            SceneManager.LoadScene(2); // Mobile
    }
}
