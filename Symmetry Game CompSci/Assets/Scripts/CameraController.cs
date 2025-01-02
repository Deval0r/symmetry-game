using UnityEngine;

public class CameraController : MonoBehaviour
{

    public TopMovement topMovementScript;
    public CarMovement carMovementScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topMovementScript = FindObjectOfType<TopMovement>();
        carMovementScript = FindObjectOfType<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carMovementScript.isPlayerInside)
        {
           Vector3 targetPosition = new Vector3(carMovementScript.transform.position.x, carMovementScript.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
        }
        else
        {
            Vector3 targetPosition = new Vector3(topMovementScript.transform.position.x, topMovementScript.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
        }
    }
}
