using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplanationManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("PlayScene");
        }
    }
}