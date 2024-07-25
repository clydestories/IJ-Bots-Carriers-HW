using TMPro;
using UnityEngine;

public class GemsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.GemAmountChanged += UpdateValue;
    }

    private void OnDisable()
    {
        _base.GemAmountChanged -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        _text.text = value.ToString();
    }
}
