using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ChargeUI : MonoBehaviour
{
    [Header("UI")]
    public Slider chargeSlider;
    public TMP_Text maxText;

    [Header("チャージ設定")]
    public float maxChargeTime = 3f;

    private float chargeTime = 0f;

    void Start()
    {
        chargeSlider.minValue = 0f;
        chargeSlider.maxValue = 1f;
        chargeSlider.value = 0f;

        maxText.gameObject.SetActive(false);
    }

    void Update()
    {
        // FIGHT前はチャージできない
        if (!CountdownManager.gameStarted)
        {
            return;
        }

        // P1コントローラーのYボタンを押している間チャージ
        if (Gamepad.all.Count > 1 &&
            Gamepad.all[0].yButton.isPressed)
        {
            chargeTime += Time.deltaTime;

            // 最大値を超えないようにする
            if (chargeTime >= maxChargeTime)
            {
                chargeTime = maxChargeTime;

                // 最大チャージ
                chargeSlider.value = 1f;
                maxText.gameObject.SetActive(true);
            }
            else
            {
                // 最大未満
                chargeSlider.value =
                    chargeTime / maxChargeTime;

                maxText.gameObject.SetActive(false);
            }
        }
        else
        {
            // ボタンを離したらリセット
            chargeTime = 0f;
            chargeSlider.value = 0f;
            maxText.gameObject.SetActive(false);
        }
    }
}