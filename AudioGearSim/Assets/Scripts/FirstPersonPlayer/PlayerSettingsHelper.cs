using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsHelper : MonoBehaviour
{
    public float mouseSensitivity = 3f;
    public float jumpHeight = 12f;
    public float speed = 12f;
    public float gravity = 20f;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerSettings();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void InitializePlayerSettings()
    {
        PlayerSettings.mouseSensitivity = mouseSensitivity;
        PlayerSettings.jumpHeight = jumpHeight;
        PlayerSettings.speed = speed;
        PlayerSettings.gravity = -gravity;
    }
}
