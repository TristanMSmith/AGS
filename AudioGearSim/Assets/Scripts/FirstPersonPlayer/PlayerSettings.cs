using System;
using UnityEngine;

[Serializable]
public static class PlayerSettings
{
    public static float mouseSensitivity { get; set; } = 3f;
    public static float jumpHeight { get; set; } = 12f;
    public static float gravity { get; set; } = -12f;
    public static float speed { get; set; } = 20f;

    public static int CurrentDisplay => GetCurrentDisplay();



    static int GetCurrentDisplay()
    {
        return (int)Display.RelativeMouseAt(Input.mousePosition).z;
    }
}