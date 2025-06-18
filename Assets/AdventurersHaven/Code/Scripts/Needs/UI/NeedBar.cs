using UnityEngine;
using UnityEngine.UI;

public class NeedBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void SetFillAmount(float value)
    {
        _fillImage.fillAmount = value;
    }
}
