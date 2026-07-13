using TMPro;
using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour
{
    public float interval = 0.5f;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}