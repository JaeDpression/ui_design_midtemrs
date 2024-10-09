using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFlipperWithAnimation : MonoBehaviour
{
    public RectTransform imageRect;  // The UI image to flip
    public Button flipButton;        // The button to trigger the flip
    public float flipDuration = 0.5f; // Duration of the flip animation

    private bool isFlipped = false;  // Flag to track flip state

    void Start()
    {
        // Add listener to the flip button
        flipButton.onClick.AddListener(ToggleFlip);
    }

    void ToggleFlip()
    {
        // Stop any running coroutines to avoid conflicts
        StopAllCoroutines();

        if (isFlipped)
        {
            // If flipped, flip back to normal with animation
            StartCoroutine(FlipImage(0f, 1f));
        }
        else
        {
            // If not flipped, flip vertically and disappear
            StartCoroutine(FlipImage(1f, 0f));
        }

        // Toggle the flip state
        isFlipped = !isFlipped;
    }

    IEnumerator FlipImage(float startScale, float endScale)
    {
        float time = 0f;

        // Smoothly interpolate between the start and end scale over time
        while (time < flipDuration)
        {
            float scale = Mathf.Lerp(startScale, endScale, time / flipDuration);
            imageRect.localScale = new Vector3(imageRect.localScale.x, scale, imageRect.localScale.z);
            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the final scale is set correctly
        imageRect.localScale = new Vector3(imageRect.localScale.x, endScale, imageRect.localScale.z);
    }
}
