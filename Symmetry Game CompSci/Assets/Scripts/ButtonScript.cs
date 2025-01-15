using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public float hoverScale = 1.2f;
    public float moveDistance = 100f; // Increased move distance
    public float animationDuration = 1.0f; // Increased duration for smoother animation
    public string sceneToLoad;
    public AnimationCurve animationCurve; // Animation curve for smooth, wavy effect
    public AudioClip hoverSound; // Assignable hover sound
    public string buttonText = "New Button Text"; // Assignable button text
    public Vector2 textOffset = new Vector2(-50, 0); // Offset for the text position
    public bool isExitButton = false; // Flag to indicate if this is the exit button

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Vector3 targetScale;
    private Vector3 targetPosition;
    private RectTransform rectTransform;
    private Image image;
    private TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component
    private Color originalColor;
    private bool isHovered = false;
    private AudioSource audioSource;
    private float hoverSoundCooldown = 0.2f; // Cooldown time in seconds
    private float lastHoverSoundTime = 0f;
    private float hoverDebounceTime = 0.1f; // Debounce time in seconds
    private float lastHoverChangeTime = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        textComponent = GetComponentInChildren<TextMeshProUGUI>(); // Get the TextMeshProUGUI component in the children
        originalColor = image.color;

        originalScale = transform.localScale;
        originalPosition = transform.position;
        targetScale = originalScale;
        targetPosition = originalPosition;

        // Add an AudioSource component if it doesn't already exist
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Set the button text
        if (textComponent != null)
        {
            textComponent.text = buttonText;

            // Offset the text position
            RectTransform textRectTransform = textComponent.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition += textOffset;
        }
    }

    void Update()
    {
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        bool isMouseOver = rectTransform.rect.Contains(localMousePosition);

        if (Time.time - lastHoverChangeTime > hoverDebounceTime)
        {
            if (isMouseOver && !isHovered)
            {
                Debug.Log("Mouse entered button area.");
                isHovered = true;
                lastHoverChangeTime = Time.time;
                targetScale = originalScale * hoverScale;
                targetPosition = originalPosition + new Vector3(-moveDistance, 0, 0);
                MoveOtherButtons(true);

                // Play hover sound with cooldown
                if (hoverSound != null && Time.time - lastHoverSoundTime > hoverSoundCooldown)
                {
                    audioSource.clip = hoverSound;
                    audioSource.Play();
                    lastHoverSoundTime = Time.time;
                }
            }
            else if (!isMouseOver && isHovered)
            {
                Debug.Log("Mouse exited button area.");
                isHovered = false;
                lastHoverChangeTime = Time.time;
                targetScale = originalScale;
                targetPosition = originalPosition;
                MoveOtherButtons(false);
            }
        }

        if (isMouseOver && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button clicked.");
            if (isExitButton)
            {
                FindObjectOfType<EndGame>().EndTheGame();
            }
            else
            {
                StartCoroutine(FlashAndLoadScene());
            }
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