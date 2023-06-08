using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DecimalConverter : MonoBehaviour
{
    private int HexToDec(string hex)                    //Take string arg for hex value
    {
        int dec = System.Convert.ToInt32(hex, 16);      //Convert hex to int with base 16
        return dec;
    }

    private string DecToHex(int value)
    {
        return value.ToString("X2");
    }

    private string FloatNormalizedToHex(float value)            //Receive value between 0 and 1
    {
        return DecToHex(Mathf.RoundToInt(value * 255f));        //Multiply it by 255, round to an int and send it to dectohex function
    }

    private float HexToFloatNormalized(string hex)              //Convert to float
    {
        return HexToDec(hex) /255f;
    }

    public Color GetColorFromString(string hexString)                       //Type color, take argument as a string and convert the substring values into a float
    {
        float red = HexToFloatNormalized(hexString.Substring(0, 2));        //Red starts on 0 and has 2 characters
        float green = HexToFloatNormalized(hexString.Substring(2, 2));      //green starts on 2 and has 2 chars
        float blue = HexToFloatNormalized(hexString.Substring(4, 2));       //blue starts on 4 and has 2 chars
        float alpha = 1.0f;                                                 //Default alpha value to 1.0f
        if (hexString.Length >= 8)                                          //If the length is 8 or more then change the alpha value at point 6
        {
            alpha = HexToFloatNormalized(hexString.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }

    public string GetStringFromColor(Color color, bool useAlpha = false)    //Add alpha value to string - https://gist.github.com/lopspower/03fb1cc0ac9f32ef38f4
    {
        string red = FloatNormalizedToHex(color.r);                         //Set hex alpha dependent on percentage of transparency
        string green = FloatNormalizedToHex(color.g);
        string blue = FloatNormalizedToHex(color.b);
        if (!useAlpha)
        {
            return red + green + blue;
        }
        else
        {
            string alpha = FloatNormalizedToHex(color.a);
            return red + green + blue + alpha;
        }
    }
}
