using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageScalerWithZoom : MonoBehaviour
{
    public RectTransform imageRect;  // The image you want to scale
    public Button scaleButton;       // The button to toggle the scale (small/normal)
    public Button zoomButton;        // The button for zoom effect (make bigger)
    public float scaleDuration = 0.5f;  // Duration of the scale animation
    public float zoomFactor = 1.5f;  // Factor by which to zoom in

    private bool isScaledDown = false;  // Flag to track if image is small
    private bool isZoomedIn = false;    // Flag to track if image is zoomed in
    private Vector3 originalScale;      // Store the original scale of the image
    private Vector3 zoomedScale;        // Store the zoomed-in scale of the image

    void Start()
    {
        // Store the original scale and calculate the zoomed-in scale
        originalScale = imageRect.localScale;
        zoomedScale = originalScale * zoomFactor;  // Increase size by zoomFactor

        // Add listeners to the buttons
        scaleButton.onClick.AddListener(ToggleScale);
        zoomButton.onClick.AddListener(ZoomImage);
    }

    void ToggleScale()
    {
        // Stop any running coroutine to avoid conflicts
        StopAllCoroutines();

        if (isScaledDown)
        {
            // If scaled down, scale back up to the original size
            StartCoroutine(ScaleImage(originalScale));
        }
        else
        {
            // If not scaled down, scale down to zero (make the image disappear)
            StartCoroutine(ScaleImage(Vector3.zero));
        }

        // Toggle the flag
        isScaledDown = !isScaledDown;
    }

    void ZoomImage()
    {
        // Stop any running coroutine to avoid conflicts
        StopAllCoroutines();

        if (isZoomedIn)
        {
            // If zoomed in, scale back to original size
            StartCoroutine(ScaleImage(originalScale));
        }
        else
        {
            // If not zoomed in, scale up to the zoomed-in size
            StartCoroutine(ScaleImage(zoomedScale));
        }

        // Toggle the zoom flag
        isZoomedIn = !isZoomedIn;
    }

    IEnumerator ScaleImage(Vector3 target)
    {
        // Get the current scale of the image
        Vector3 currentScale = imageRect.localScale;
        float time = 0f;

        // Smoothly interpolate between the current scale and the target scale over time
        while (time < scaleDuration)
        {
            imageRect.localScale = Vector3.Lerp(currentScale, target, time / scaleDuration);
            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the final scale is set correctly
        imageRect.localScale = target;
    }
}
