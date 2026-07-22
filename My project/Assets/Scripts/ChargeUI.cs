using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargeUI : MonoBehaviour
{
    [Header("UI")]
    public Slider chargeSlider;
    public TMP_Text maxText;

    [Header("ゲージ色")]
    public Image fillImage;
    public Color chargingColor = Color.white;
    public Color maxColor = Color.red;

    [Header("チャージシステム")]
    public ChargeSystem chargeSystem;

    void Start()
    {
        chargeSlider.minValue = 0f;
        chargeSlider.maxValue = 1f;
        chargeSlider.value = 0f;

        maxText.gameObject.SetActive(false);
    }

    void Update()
    {
        chargeSlider.value = chargeSystem.ChargeRate;

        bool isMax = chargeSystem.IsMax;

        maxText.gameObject.SetActive(isMax);

        // MAXかどうかでゲージ色を変更
        if (isMax)
        {
            fillImage.color = maxColor;
        }
        else
        {
            fillImage.color = chargingColor;
        }
    }
}