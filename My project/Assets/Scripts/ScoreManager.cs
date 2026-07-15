using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // スコア
    public static int p1Score = 0;
    public static int p2Score = 0;

    // 勝者
    public static string winner = "";

    // UI
    public TMP_Text p1Text;
    public TMP_Text p2Text;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        p1Text.text = "Score: " + p1Score;
        p2Text.text = "Score: " + p2Score;
    }

    // スコアリセット
    public static void ResetScore()
    {
        p1Score = 0;
        p2Score = 0;
        winner = "";
    }

    // 勝敗判定
    public static void JudgeWinner()
    {
        if (p1Score > p2Score)
        {
            winner = "1P WIN!";
        }
        else if (p2Score > p1Score)
        {
            winner = "2P WIN!";
        }
        else
        {
            winner = "DRAW!";
        }
    }
}