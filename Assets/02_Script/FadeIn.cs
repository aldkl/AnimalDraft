using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour

{

    public float FadeInTime = 2f; // Fade효과 재생시간
    public float FadeOutTime = 2f; // Fade효과 재생시간

    Image fadeImg;

    float start;

    float end;

    float time = 0f;

    bool isPlaying = false;

    public bool PlayIn;
    public bool PlayOut;
    public bool Loop;

    void Awake()
    {

        fadeImg = GetComponent<Image>();
    }
    private void Update()
    {
        if(Loop)
        {
            start = 1f;

            end = 0f;
            if(fadeImg.color.a == 0)
            {
                StartCoroutine("fadeInplay");
            }
            else if(fadeImg.color.a == 1)
            {
                StartCoroutine("fadeoutplay");
            }
        }
        else
        {
            if (PlayIn)
            {
                start = 1f;

                end = 0f;
                StartCoroutine("fadeInplay");
                PlayIn = false;
            }
            if (PlayOut)
            {
                start = 1f;

                end = 0f;
                StartCoroutine("fadeoutplay");
                PlayOut = false;
            }
        }
    }

    IEnumerator fadeInplay()
    {

        isPlaying = true;



        Color fadecolor = fadeImg.color;

        time = 0f;

        fadecolor.a = Mathf.Lerp(end, start, time);



        while (fadecolor.a < 1)
        {

            time += Time.deltaTime / FadeInTime;

            fadecolor.a = Mathf.Lerp(end, start, time);

            fadeImg.color = fadecolor;

            yield return null;

        }

        isPlaying = false;
    }

    IEnumerator fadeoutplay()

    {

        isPlaying = true;



        Color fadecolor = fadeImg.color;

        time = 0f;

        fadecolor.a = Mathf.Lerp(start, end, time);



        while (fadecolor.a > 0f)

        {

            time += Time.deltaTime / FadeOutTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;

        }

        isPlaying = false;
    }
}