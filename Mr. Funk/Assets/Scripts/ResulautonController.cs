using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResulautonController : MonoBehaviour
{
    public GameObject funkFeed;
    public Texture camTexture;

    private Vector2 setRes;
    // Update is called once per frame
    private void Awake()
    {
        SetRes();
        camTexture.height = Screen.height;
        camTexture.width = Screen.width;
    }

    void Update()
    {
        if (setRes.x != Screen.width || setRes.y != Screen.height)
        {
            SetRes();
        }
    }

    private void SetRes()
    {
        setRes = new Vector2(Screen.width, Screen.height);
        
        funkFeed.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        funkFeed.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
    }
}
