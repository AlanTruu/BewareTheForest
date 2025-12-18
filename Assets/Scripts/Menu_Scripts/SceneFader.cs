using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public AudioSource audioSource;
    public float startVolume;

    void Start()
    {
        if(audioSource != null)
        {
            startVolume = audioSource.volume;
        }
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        if (audioSource != null)
        {
            audioSource.volume = 0f;
        }

        while (t > 0)
        {
            t -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, t);

            if (audioSource != null)
            {
                audioSource.volume = Mathf.Lerp(0f, startVolume, 1f - t);
            }

            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, t);

            if (audioSource != null)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            }

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
