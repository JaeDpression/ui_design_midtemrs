using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFader : MonoBehaviour
{
    public Image image;  // The UI image you want to fade
    public Button fadeButton;  // The button to trigger the fade
    public float fadeDuration = 1.0f;  // Duration of the fade animation

    private bool isFaded = false;  // Flag to track the fade state

    void Start()
    {
        // Add listener to the fade button
        fadeButton.onClick.AddListener(ToggleFade);
    }

    void ToggleFade()
    {
        // Stop any running coroutines to avoid conflicts
        StopAllCoroutines();

        // Start fading based on current state
        if (isFaded)
        {
            StartCoroutine(FadeImage(0f, 1f));  // Fade in (to full opacity)
        }
        else
        {
            StartCoroutine(FadeImage(1f, 0f));  // Fade out (to transparent)
        }

        // Toggle the fade flag
        isFaded = !isFaded;
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        // Get the current color of the image
        Color currentColor = image.color;
        float time = 0f;

        // Smoothly interpolate between the start and end alpha over time
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the final alpha is set correctly
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, endAlpha);
    }
}
