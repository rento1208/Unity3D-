using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ResultManager : MonoBehaviour
{
    public TMP_Text leftResult;
    public TMP_Text rightResult;

    public TMP_Text p1Kill;
    public TMP_Text p2Kill;

    void Start()
    {
        Debug.Log("leftResult = " + leftResult);
        Debug.Log("rightResult = " + rightResult);
        Debug.Log("p1Kill = " + p1Kill);
        Debug.Log("p2Kill = " + p2Kill);
        Debug.Log("winner = " + ScoreManager.winner);

        p1Kill.text = "Score : " + ScoreManager.p1Score;
        p2Kill.text = "Score : " + ScoreManager.p2Score;

        if (ScoreManager.winner == "1P WIN!")
        {
            leftResult.text = "Winner";
            rightResult.text = "Loser";

            leftResult.color = Color.red;
            rightResult.color = Color.blue;
        }
        else if (ScoreManager.winner == "2P WIN!")
        {
            leftResult.text = "Loser";
            rightResult.text = "Winner";

            leftResult.color = Color.blue;
            rightResult.color = Color.red;
        }
        else
        {
            leftResult.text = "DRAW";
            rightResult.text = "DRAW";

            leftResult.color = Color.white;
            rightResult.color = Color.white;
        }
    }

    void Update()
    {
        // コントローラーのAボタン（PSなら×ボタン）
        if (Gamepad.current != null &&
            Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            ScoreManager.ResetScore();
            SceneManager.LoadScene("TitleScene");
        }
    }
}