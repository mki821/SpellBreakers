using UnityEngine;
using UnityEngine.UI;

public class StatusUI : HUDUI
{
    [SerializeField] private Image _healthFillAmount;

    public void SetHealth(float percent)
    {
        _healthFillAmount.fillAmount = Mathf.Clamp(percent, 0.0f, 1.0f);
    }
}
