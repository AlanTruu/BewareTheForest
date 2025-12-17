//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image fade_image;
    [SerializeField] private float fade_duration = 1f;


    void Awake()
    {
        fade_image.color = new Color(0, 0, 0, 0);
        fade_image.gameObject.SetActive(true);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //For when player dies, their screen shall fade to black
    public void fade_to_black()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    //For when player revives or wakes, theirs screen shall fade from black
    public void fade_from_black()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    //Coroutine to Fade the screen, changes alpha value of this
    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color c = fade_image.color;

        while (t < fade_duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / fade_duration);
            fade_image.color = c;
            yield return null;
        }

        //Snap the image color at the end
        c.a = to;
        fade_image.color = c;
    }
}
