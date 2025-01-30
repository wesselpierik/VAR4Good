using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    // public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    private Color fadeColor;
    private Image image;

    private bool fadeActive = true;

    void Awake()
    {
        image = GetComponentInChildren<Image>();
        if (image == null)
        {
            Debug.LogError("No Image component found in children of FadeUI");
            return;
        }
        fadeColor = image.color;
        // if (fadeOnStart) FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
        fadeActive = false;
    }

    public void FadeOut()
    {
        Fade(0, 1);
        fadeActive = true;
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;

        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            image.color = newColor;
            timer += Time.deltaTime;
            yield return null;
        }
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        image.color = newColor2;
    }
}
