using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ExplanationManager : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current != null &&
            Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            SceneManager.LoadScene("PlayScene");
        }
    }
}