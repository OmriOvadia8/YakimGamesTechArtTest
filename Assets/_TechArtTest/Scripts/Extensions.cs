using UnityEngine;

public static class Extensions
{
    public static Color ColorFromHSV(Vector4 colorCode)
    {
        Color color = Color.HSVToRGB(colorCode.x / 360f, colorCode.y / 100f, colorCode.z / 100f);
        color.a = colorCode.w;
        return color;
    }
}
