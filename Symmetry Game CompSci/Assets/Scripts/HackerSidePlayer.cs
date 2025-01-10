using UnityEngine;

public class HackerSidePlayer : MonoBehaviour
{
    private float scaleFactor;

    public PlayerController playerController;
    public Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        scaleFactor = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = mainCamera.WorldToScreenPoint(playerController.transform.position);
        transform.position = (new Vector2(position.x + 2000, position.y)) * scaleFactor;
    }
}