using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFlipInFromRight : MonoBehaviour
{
    public RectTransform imageRect;  // The UI image to flip in
    public Button flipButton;        // The button to trigger the flip
    public float flipDuration = 0.5f;  // Duration of the flip and zoom animation
    public float offscreenOffset = 500f;  // How far off-screen the image starts
    public float startScale = 0.1f;      // Starting scale for the zoom effect

    private Vector3 originalPosition;   // The original position of the image
    private Vector3 originalScale;      // The original scale of the image
    private bool isFlipped = false;     // Flag to track flip state

    void Start()
    {
        // Store the original position and scale of the image
        originalPosition = imageRect.anchoredPosition;
        originalScale = imageRect.localScale;

        // Add listener to the flip button
        flipButton.onClick.AddListener(ToggleFlip);
    }

    void ToggleFlip()
    {
        // Stop any running coroutines to avoid conflicts
        StopAllCoroutines();

        if (isFlipped)
        {
            // Move the image back to its original position and scale
            StartCoroutine(MoveAndScaleImage(originalPosition.x + offscreenOffset, originalPosition.x, Vector3.one * startScale, originalScale));
        }
        else
        {
            // Move the image from off-screen to its original position while scaling it
            StartCoroutine(MoveAndScaleImage(originalPosition.x, originalPosition.x + offscreenOffset, originalScale, Vector3.one * startScale));
        }

        // Toggle the flip state
        isFlipped = !isFlipped;
    }

    IEnumerator MoveAndScaleImage(float startX, float endX, Vector3 startScale, Vector3 endScale)
    {
        float time = 0f;
        Vector2 startPos = imageRect.anchoredPosition;

        // Smoothly interpolate between the start and end position and scale over time
        while (time < flipDuration)
        {
            float newX = Mathf.Lerp(startX, endX, time / flipDuration);
            Vector3 newScale = Vector3.Lerp(startScale, endScale, time / flipDuration);
            imageRect.anchoredPosition = new Vector2(newX, startPos.y);
            imageRect.localScale = newScale;
            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the final position and scale are set correctly
        imageRect.anchoredPosition = new Vector2(endX, startPos.y);
        imageRect.localScale = endScale;
    }
}
