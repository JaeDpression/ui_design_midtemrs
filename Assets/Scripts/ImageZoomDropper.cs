using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageZoomDropper : MonoBehaviour
{
    public RectTransform imageRect;  // The UI image to drop and zoom in
    public Button dropButton;        // The button to trigger the drop
    public float dropDuration = 0.5f;  // Duration of the drop and zoom animation
    public float offscreenOffset = 500f;  // How far off-screen the image starts
    public float startScale = 0.1f;      // Starting scale for the zoom effect

    private Vector3 originalPosition;   // The original position of the image
    private Vector3 originalScale;      // The original scale of the image
    private bool isDropped = false;     // Flag to track drop state

    void Start()
    {
        // Store the original position and scale of the image
        originalPosition = imageRect.anchoredPosition;
        originalScale = imageRect.localScale;

        // Add listener to the drop button
        dropButton.onClick.AddListener(ToggleDrop);
    }

    void ToggleDrop()
    {
        // Stop any running coroutines to avoid conflicts
        StopAllCoroutines();

        if (isDropped)
        {
            // Move the image back to the top and shrink it
            StartCoroutine(MoveAndScaleImage(originalPosition.y, originalPosition.y + offscreenOffset, originalScale, Vector3.one * startScale));
        }
        else
        {
            // Move the image from the top and zoom it in
            StartCoroutine(MoveAndScaleImage(originalPosition.y + offscreenOffset, originalPosition.y, Vector3.one * startScale, originalScale));
        }

        // Toggle the drop state
        isDropped = !isDropped;
    }

    IEnumerator MoveAndScaleImage(float startY, float endY, Vector3 startScale, Vector3 endScale)
    {
        float time = 0f;
        Vector2 startPos = imageRect.anchoredPosition;

        // Smoothly interpolate between the start and end position and scale over time
        while (time < dropDuration)
        {
            float newY = Mathf.Lerp(startY, endY, time / dropDuration);
            Vector3 newScale = Vector3.Lerp(startScale, endScale, time / dropDuration);
            imageRect.anchoredPosition = new Vector2(startPos.x, newY);
            imageRect.localScale = newScale;
            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the final position and scale are set correctly
        imageRect.anchoredPosition = new Vector2(startPos.x, endY);
        imageRect.localScale = endScale;
    }
}
