using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Color Color = Color.white;
    private Image _image;
    public GameObject Player;
    public Vector3 StartPosition;
    private PlayerStatus status;




    public void Awake()
    {
        _image = GetComponent<Image>();
        status = GameObject.Find("ThirdPersonController").GetComponent<PlayerStatus>();
    }

    public void StartFade()
    {
        StartCoroutine("FadeAlpha");
    }
    public void StartFadeEnd()
    {
        StartCoroutine("FadeEnd");
    }
    // Update is called once per frame
    public IEnumerator FadeAlpha()
    {
        if (!_image)
        {
            _image = GetComponent<Image>();

        }
        var initialColor = Color;

        initialColor.a = 0;
        while (initialColor.a < 1)
        {
            initialColor.a += 0.5f * Time.smoothDeltaTime;
            _image.color = initialColor;
            yield return null;
        }

        status.Hp = 100;
        status.Stamina = 100;
        Player.transform.position = StartPosition;
        initialColor.a = 1;
        while (initialColor.a > 0)
        {
            initialColor.a -= 0.5f *Time.smoothDeltaTime;
            _image.color = initialColor;
            yield return null;
        }
        gameObject.SetActive(false);
        
        


    }
    public IEnumerator FadeEnd()
    {
        if (!_image)
        {
            _image = GetComponent<Image>();

        }
        var initialColor = Color.red;

        initialColor.a = 0;
        while (initialColor.a < 1)
        {
            initialColor.a += 0.5f * Time.smoothDeltaTime;
            _image.color = initialColor;
            yield return null;
        }

        
        


    }
}