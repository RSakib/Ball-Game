using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class hideCursor : MonoBehaviour
{
    private BallControls ballControls;

    // Start is called before the first frame update
    void Awake()
    {
        ballControls = new BallControls();
        ballControls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ballControls.Ball.Free_Cursor.performed += free_cursor;
    }

    void free_cursor(InputAction.CallbackContext context)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
