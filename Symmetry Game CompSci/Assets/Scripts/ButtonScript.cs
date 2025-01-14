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

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private Image image;
    private Color originalColor;
    private bool isHovered = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalColor = image.color;

        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        bool isMouseOver = rectTransform.rect.Contains(localMousePosition);

        if (isMouseOver && !isHovered)
        {
            Debug.Log("Mouse entered button area.");
            isHovered = true;
            StopAllCoroutines();
            StartCoroutine(AnimateButton(originalScale * hoverScale, originalPosition + new Vector3(-moveDistance, 0, 0)));
        }
        else if (!isMouseOver && isHovered)
        {
            Debug.Log("Mouse exited button area.");
            isHovered = false;
            StopAllCoroutines();
            StartCoroutine(AnimateButton(originalScale, originalPosition));
        }

        if (isMouseOver && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button clicked.");
            StartCoroutine(FlashAndLoadScene());
        }
    }

    private IEnumerator AnimateButton(Vector3 targetScale, Vector3 targetPosition)
    {
        Debug.Log("Animating button to position: " + targetPosition);
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float curveValue = animationCurve.Evaluate(t);
            transform.localScale = Vector3.LerpUnclamped(startingScale, targetScale, curveValue);
            transform.position = Vector3.LerpUnclamped(startingPosition, targetPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.position = targetPosition;
        Debug.Log("Button animation completed. New position: " + transform.position);
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