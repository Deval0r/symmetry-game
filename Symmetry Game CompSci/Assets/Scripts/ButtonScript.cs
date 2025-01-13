using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverScale = 1.2f;
    public float moveDistance = 10f;
    public float animationDuration = 0.2f;
    public string sceneToLoad;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isHovered = false;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        StopAllCoroutines();
        StartCoroutine(AnimateButton(transform.localScale * hoverScale, originalPosition + new Vector3(-moveDistance, 0, 0)));
        MoveOtherButtons(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
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
        Vector3 startingScale = transform.localScale;
        Vector3 startingPosition = transform.localPosition;

        while (elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / animationDuration);
            transform.localPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.localPosition = targetPosition;
    }

    private void MoveOtherButtons(bool moveAway)
    {
        foreach (Transform sibling in transform.parent)
        {
            if (sibling != transform)
            {
                Vector3 targetPosition = sibling.localPosition;
                if (moveAway)
                {
                    targetPosition += (sibling.localPosition - transform.localPosition).normalized * moveDistance;
                }
                else
                {
                    targetPosition = sibling.GetComponent<ButtonScript>().originalPosition;
                }
                StartCoroutine(AnimateSibling(sibling, targetPosition));
            }
        }
    }

    private IEnumerator AnimateSibling(Transform sibling, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = sibling.localPosition;

        while (elapsedTime < animationDuration)
        {
            sibling.localPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sibling.localPosition = targetPosition;
    }

    private IEnumerator FlashAndLoadScene()
    {
        Color originalColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = originalColor;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}