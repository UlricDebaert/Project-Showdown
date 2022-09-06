using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public static bool musicOn;

    public UnityEvent resumeEvent;

    public void Resume()
    {
        Time.timeScale = 1.0f;
        resumeEvent.Invoke();
    }

    public void setMusicOn(bool toggleValue)
    {
        musicOn = toggleValue;
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
