using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargeUI2 : MonoBehaviour
{
    [Header("UI")]
    public Slider chargeSlider;
    public TMP_Text maxText;

    [Header("ゲージ色")]
    public Image fillImage;
    public Color chargingColor = Color.white;
    public Color maxColor = Color.red;

    [Header("接続するChargeSystem")]
    public ChargeSystem2 chargeSystem;

    void Start()
    {
        chargeSlider.minValue = 0f;
        chargeSlider.maxValue = 1f;
        chargeSlider.value = 0f;

        maxText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (chargeSystem == null) return;

        chargeSlider.value = chargeSystem.ChargeRate;

        bool isMax = chargeSystem.IsMax;

        maxText.gameObject.SetActive(isMax);

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