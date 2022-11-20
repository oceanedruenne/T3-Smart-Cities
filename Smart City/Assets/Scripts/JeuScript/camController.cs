using UnityEngine;

public class camController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float speedScroll = 30f;
    Camera cam;


    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.up * zInput + transform.right * xInput;

        transform.position += dir * speed * Time.deltaTime;

        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * speedScroll;
        }
    }
}
