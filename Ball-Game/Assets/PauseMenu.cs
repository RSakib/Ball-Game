using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObj;
    public static bool isPaused = false;
    public BallControls inputSystem;

    void Awake()
    {
        inputSystem = new BallControls();
        inputSystem.Enable();
        inputSystem.Ball.PauseGame.performed += P_Decide;
        Resume();
    }

    void P_Decide(InputAction.CallbackContext context)
    {
        if (isPaused == false)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        PauseMenuObj.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        PauseMenuObj.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GamePadToggle(bool toggle)
    {

    }
}
