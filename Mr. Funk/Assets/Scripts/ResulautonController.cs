using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResulautonController : MonoBehaviour
{
    public GameObject funkFeed;
    public Texture camTexture;

    private Vector2 setRes;
    // Update is called once per frame
    void Update()
    {
        if (setRes.x != GetRes().x || setRes.y != GetRes().y)
        {
            Vector2 res = GetRes();
            camTexture.height = Screen.height;
            camTexture.width = Screen.width;
        }
    }

    private Vector2 GetRes()
    {
        Vector2 screenResolution = Vector2.zero;
        screenResolution.x = Screen.width;
        screenResolution.x = Screen.height;
        return screenResolution;
    }
}
