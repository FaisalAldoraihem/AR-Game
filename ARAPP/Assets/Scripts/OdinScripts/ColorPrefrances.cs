using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ColorPrefrances : MonoBehaviour
{
    [ColorPalette("Floral")]
    [ShowInInspector]
    private Color textcolor { get { return _textColor; } set { UpdateColor(value); } }
    private Color _textColor;
    private TextMeshProUGUI text;
    private void UpdateColor(Color value)
    {
        if (text == null)
            text = this.GetComponent<TextMeshProUGUI>();

        text.color = value;
    }

}
