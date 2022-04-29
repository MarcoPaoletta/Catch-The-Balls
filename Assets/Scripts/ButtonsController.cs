using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public static Color backgroundColorSelected; 

    public void BlueButton()
    {
        backgroundColorSelected = new Color(255, 255, 255, 1);
        SceneManager.LoadScene(1);
    }

    public void RedButton()
    {
        backgroundColorSelected = new Color(255, 0, 0, 1);
        SceneManager.LoadScene(1);
    }

    public void OpenSourceCodeButton()
    {
        Application.OpenURL("https://github.com/MarcoPaoletta/Catch-The-Balls");
    }
}
