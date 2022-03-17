using System;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
	public enum UIColor { White, Black, Red, Green, Blue, Yellow }

	public Button changeColor_Button;
	public Slider alpha_Slider;
	public Image color_Image;

	private int _currentColor = 0;
	private int _colorsCount = 0;

	private void Awake()
	{
		// Get the number of values in UIColor enum.
		_colorsCount = Enum.GetValues(typeof(UIColor)).Length;
		// V2 : register to Button/Slider callbacks from script.
		changeColor_Button.onClick.AddListener(ChangeColor); // ChangeColor() will be called when the button is clicked.
		alpha_Slider.onValueChanged.AddListener(SetAlpha); // SetAlpha() will be called when the slider value change.
	}

	// V1 : register in the Unity event "On Click" in the Button's inspector.
	public void ChangeColor()
	{
		// Next color
		_currentColor = ++_currentColor % _colorsCount;
		UIColor colorEnum = (UIColor)_currentColor;
		Color newColor = Color.white;
		switch (colorEnum)
		{
			//case ColorEnum.White: newColor = Color.white; break;
			case UIColor.Black: newColor = Color.black; break;
			case UIColor.Red: newColor = Color.red; break;
			case UIColor.Green: newColor = Color.green; break;
			case UIColor.Blue: newColor = Color.blue; break;
			case UIColor.Yellow: newColor = Color.yellow; break;
		}

		// Apply new color.
		newColor.a = alpha_Slider.value; // with the the same alpha value as the slider value.
		color_Image.color = newColor;
	}

	// V1 : register in the Unity event "On Value Changed" in the Slider's inspector.
	public void SetAlpha(float pNewAlpha)
	{
		Color color = color_Image.color;
		color.a = pNewAlpha;
		color_Image.color = color;
	}
}
