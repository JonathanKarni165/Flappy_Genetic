using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// scales time, also includs some scene management
/// </summary>
public class TimeScaler : MonoBehaviour
{
    public float timeScale;

    //used for game starting and -> set time scale to 3
    private bool started;

    private void Start()
    {
        started = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);

            //destroy all training options related scripts
            //reset the training
            FindObjectOfType<DDOL>().DestroyOnNextLoad();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            timeScale = 3f;
            started = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
            timeScale = (timeScale + 1) % 10;

        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            timeScale = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            timeScale = 3;
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            timeScale = 4;
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            timeScale = 5;
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            timeScale = 6;
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            timeScale = 7;
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            timeScale = 8;
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            timeScale = 9;
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            timeScale = 10;
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            timeScale = 12;

        if (Input.GetKeyDown(KeyCode.F))
            timeScale = 5;
        if (Input.GetKeyDown(KeyCode.U))
            timeScale = 10;
        Time.timeScale = timeScale;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
