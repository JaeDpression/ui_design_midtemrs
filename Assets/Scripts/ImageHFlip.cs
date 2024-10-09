using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageHFlip : MonoBehaviour
{
    public RectTransform imageRect;  // The UI image to flip
    public Button flipButton;         // The button to trigger the flip
    public float flipDuration = 0.5f; // Duration of the flip animation

    private bool isFlipped = false;   // Flag to track flip state

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
            StartCoroutine(FlipImage(90f, 0f));
        }
        else
        {
            // If not flipped, rotate 90 degrees
            StartCoroutine(FlipImage(0f, 90f));
        }

        // Toggle the flip state
        isFlipped = !isFlipped;
    }

    IEnumerator FlipImage(float startAngle, float endAngle)
    {
        float time = 0f;

        // Smoothly interpolate between the start and end angle over time
        while (time < flipDuration)
        {
            // Calculate the angle at this time
            float angle = Mathf.Lerp(startAngle, endAngle, time / flipDuration);
            imageRect.localRotation = Quaternion.Euler(0f, angle, 0f);

            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the final rotation is set correctly
        imageRect.localRotation = Quaternion.Euler(0f, endAngle, 0f);
    }
}
