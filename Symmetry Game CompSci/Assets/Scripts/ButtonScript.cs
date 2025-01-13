using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverScale = 1.2f;
    public float moveDistance = 10f;
    public float animationDuration = 0.2f;
    public string sceneToLoad;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private Image image;
    private Color originalColor;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalColor = image.color;

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateButton(originalScale * hoverScale, originalPosition + new Vector3(-moveDistance, 0, 0)));
        MoveOtherButtons(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateButton(originalScale, originalPosition));
        MoveOtherButtons(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(FlashAndLoadScene());
    }

    private IEnumerator AnimateButton(Vector3 targetScale, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingScale = rectTransform.localScale;
        Vector3 startingPosition = rectTransform.anchoredPosition;

        while (elapsedTime < animationDuration)
        {
            rectTransform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / animationDuration);
            rectTransform.anchoredPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;
        rectTransform.anchoredPosition = targetPosition;
    }

    private void MoveOtherButtons(bool moveAway)
    {
        foreach (Transform sibling in transform.parent)
        {
            if (sibling != transform)
            {
                RectTransform siblingRect = sibling.GetComponent<RectTransform>();
                ButtonScript siblingScript = sibling.GetComponent<ButtonScript>();
                if (siblingRect != null && siblingScript != null)
                {
                    Vector3 targetPosition = siblingRect.anchoredPosition;
                    if (moveAway)
                    {
                        Vector3 direction = (siblingRect.anchoredPosition - rectTransform.anchoredPosition).normalized;
                        targetPosition += direction * moveDistance;
                    }
                    else
                    {
                        targetPosition = siblingScript.originalPosition;
                    }
                    StartCoroutine(AnimateSibling(siblingRect, targetPosition));
                }
            }
        }
    }

    private IEnumerator AnimateSibling(RectTransform siblingRect, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = siblingRect.anchoredPosition;

        while (elapsedTime < animationDuration)
        {
            siblingRect.anchoredPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        siblingRect.anchoredPosition = targetPosition;
    }

    private IEnumerator FlashAndLoadScene()
    {
        image.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        image.color = originalColor;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
