using TMPro;
using UnityEngine;

public class GemsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private BaseShop _shop;

    private void OnEnable()
    {
        _shop.GemAmountChanged += UpdateValue;
    }

    private void OnDisable()
    {
        _shop.GemAmountChanged -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        _text.text = value.ToString();
    }
}
