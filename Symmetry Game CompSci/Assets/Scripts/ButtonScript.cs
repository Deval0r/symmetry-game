using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public float hoverScale = 1.2f;
    public float moveDistance = 100f; // Increased move distance
    public float animationDuration = 1.0f; // Increased duration for smoother animation
    public string sceneToLoad;
    public AnimationCurve animationCurve; // Animation curve for smooth, wavy effect
    public AudioClip hoverSound; // Assignable hover sound

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Vector3 targetScale;
    private Vector3 targetPosition;
    private RectTransform rectTransform;
    private Image image;
    private Color originalColor;
    private bool isHovered = false;
    private AudioSource audioSource;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalColor = image.color;

        originalScale = transform.localScale;
        originalPosition = transform.position;
        targetScale = originalScale;
        targetPosition = originalPosition;

        // Add an AudioSource component if it doesn't already exist
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        bool isMouseOver = rectTransform.rect.Contains(localMousePosition);

        if (isMouseOver && !isHovered)
        {
            Debug.Log("Mouse entered button area.");
            isHovered = true;
            targetScale = originalScale * hoverScale;
            targetPosition = originalPosition + new Vector3(-moveDistance, 0, 0);
            MoveOtherButtons(true);

            // Play hover sound
            if (hoverSound != null)
            {
                audioSource.clip = hoverSound;
                audioSource.Play();
            }
        }
        else if (!isMouseOver && isHovered)
        {
            Debug.Log("Mouse exited button area.");
            isHovered = false;
            targetScale = originalScale;
            targetPosition = originalPosition;
            MoveOtherButtons(false);
        }

        if (isMouseOver && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button clicked.");
            StartCoroutine(FlashAndLoadScene());
        }

        // Smoothly interpolate to the target scale and position
        float t = Time.deltaTime / animationDuration;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t);
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
    }

    private void MoveOtherButtons(bool moveAway)
    {
        foreach (Transform sibling in transform.parent)
        {
            if (sibling != transform)
            {
                ButtonScript siblingScript = sibling.GetComponent<ButtonScript>();
                if (siblingScript != null)
                {
                    if (moveAway)
                    {
                        siblingScript.targetPosition = siblingScript.originalPosition + (sibling.position - transform.position).normalized * moveDistance * 0.5f;
                    }
                    else
                    {
                        siblingScript.targetPosition = siblingScript.originalPosition;
                    }
                }
            }
        }
    }

    private IEnumerator FlashAndLoadScene()
    {
        Debug.Log("Flashing button and loading scene.");
        Color originalColor = image.color;
        image.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        image.color = originalColor;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}