using UnityEngine;

public class MobEscapeGround : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed, 0, 0);
    }
}
