using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Exit : MonoBehaviour
{
   public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Quit();
        }
    }
}
