using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    public Player1Controller player1;
    public Player2Controller player2;
    public TimerManager timermanager;
    public AudioSource bgmSource;

    IEnumerator Start()
    {
        // 最初は動けない
        player1.enabled = false;
        player2.enabled = false;

        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownText.text = "READY";
        yield return new WaitForSeconds(0.7f);

        countdownText.text = "FIGHT!";
        yield return new WaitForSeconds(0.7f);

        countdownText.gameObject.SetActive(false);

        // 操作開始
        player1.enabled = true;
        player2.enabled = true;
        timermanager.isRunning = true;
        bgmSource.Play();
    }
}