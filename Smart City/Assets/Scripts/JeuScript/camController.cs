using UnityEngine;

public class camController : MonoBehaviour
{
    /*
    [SerializeField] float speed = 5f;
    */

    Camera cam;

    public float speedScroll = 30f;
    public float dragSpeed = 1f;
    private Vector3 dragOrigin;


    private void Start()
    {
        cam = Camera.main;
    }
    // Permet de gérer l'affichage des scènes
    void Update()
    {
        /*
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.up * zInput + transform.right * xInput;

        transform.position += dir * speed * Time.deltaTime;
        */

        if(Input.GetAxis("Mouse ScrollWheel") != 0f && cam.fieldOfView - Input.GetAxis("Mouse ScrollWheel") * speedScroll < 110)
        {
            cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * speedScroll;
        }

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(1)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed * -1, pos.y * dragSpeed * -1, 0);
 
        transform.Translate(move, Space.World);  
    }
}
