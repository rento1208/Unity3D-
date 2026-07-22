using UnityEngine;

public class ChargeSystem : MonoBehaviour
{
    [Header("チャージ設定")]
    public float maxChargeTime = 3f;

    private float chargeTime = 0f;

    public bool IsMax
    {
        get
        {
            return chargeTime >= maxChargeTime;
        }
    }

    public float ChargeRate
    {
        get
        {
            return chargeTime / maxChargeTime;
        }
    }

    public void AddCharge(float amount)
    {
        chargeTime += amount;

        if (chargeTime > maxChargeTime)
        {
            chargeTime = maxChargeTime;
        }
    }

    public float GetKnockbackPower()
    {
        if (IsMax)
        {
            return 1200f;
        }
        else
        {
            return 800f;
        }
    }

    public void ResetCharge()
    {
        chargeTime = 0f;
    }
}