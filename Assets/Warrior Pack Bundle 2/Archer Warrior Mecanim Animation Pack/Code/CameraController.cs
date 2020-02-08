using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject cameraTarget;
    public float rotateSpeed;
    float rotate;
    public float offsetDistance;
    public float offsetHeight;
    public float smoothing;
    Vector3 offset;
    bool following = true;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
        offset = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        rotate = Input.GetAxis("Mouse X");
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    rotate--;
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    rotate--;
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    rotate++;
        //}
        offset = Quaternion.AngleAxis(rotate * rotateSpeed, Vector3.up) * offset;
        transform.position = cameraTarget.transform.position + offset;
        transform.position = new Vector3(Mathf.Lerp(lastPosition.x, cameraTarget.transform.position.x + offset.x, smoothing * Time.deltaTime),
        Mathf.Lerp(lastPosition.y, cameraTarget.transform.position.y + offset.y, smoothing * Time.deltaTime),
        Mathf.Lerp(lastPosition.z, cameraTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
        transform.LookAt(cameraTarget.transform.position);
        transform.rotation = Quaternion.Euler(20, transform.rotation.eulerAngles.y, 0);
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
    }
}