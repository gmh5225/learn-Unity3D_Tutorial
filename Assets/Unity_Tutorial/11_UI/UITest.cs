/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2022																																  *
* Emmanuel Badier <emmanuel.badier@gmail.com>																										  *
* 																																					  *
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),  *
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,  *
* and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:		  *
* 																																					  *
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.					  *
* 																																					  *
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 																							  *
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 		  *
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.							  *
******************************************************************************************************************************************************/

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
		// Switch to next color in the UIColor enum.
		_currentColor = ++_currentColor % _colorsCount;
		UIColor colorEnum = (UIColor)_currentColor;
		Color newColor = Color.white; // default
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
		newColor.a = alpha_Slider.value; // with the the same alpha value as the current slider value.
		color_Image.color = newColor;
	}

	// V1 : register in the Unity event "On Value Changed" in the Slider's inspector.
	public void SetAlpha(float pNewAlpha)
	{
		// We cannot change the alpha attribute directly,
		// because accessing property color_Image.color returns a copy.
		//color_Image.color.a = pNewAlpha;

		// So we need to get the current image color.
		Color color = color_Image.color;
		// then modify its alpha value.
		color.a = pNewAlpha;
		// then set the modified color to the image.
		color_Image.color = color;
	}
}
