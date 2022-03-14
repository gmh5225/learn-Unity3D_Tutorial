using System;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    public enum ColorEnum { White, Red, Green, Blue, Black }

    public Button changeColor_Button;
    public Slider alpha_Slider;
    public Image color_Image;
    
    private int _currentColor = 0;

	// V2 : Use callbacks
	//private void Awake()
	//{
	//	changeColor_Button.onClick.AddListener(ChangeColor);
	//	alpha_Slider.onValueChanged.AddListener(AlphaChanged);
	//}

	// V1 : Use Unity event "On Click" in the button's inspector.
	public void ChangeColor()
	{
        // Next color
        _currentColor = ++_currentColor % Enum.GetValues(typeof(ColorEnum)).Length;
        ColorEnum colorEnum = (ColorEnum)_currentColor;
        Color newColor = Color.white;
        switch (colorEnum)
		{
        //case ColorEnum.White: newColor = Color.white; break;
        case ColorEnum.Red: newColor = Color.red; break;
        case ColorEnum.Green: newColor = Color.green; break;
        case ColorEnum.Blue: newColor = Color.blue; break;
        case ColorEnum.Black: newColor = Color.black; break;
        }

        // Apply Color.
        newColor.a = alpha_Slider.value;
        color_Image.color = newColor;
    }

    // V1 : Use Unity event "On Value Changed" in the slider's inspector.
    public void AlphaChanged(float pNewAlpha)
	{
        Color color = color_Image.color;
        color.a = pNewAlpha;
        color_Image.color = color;
    }
}
