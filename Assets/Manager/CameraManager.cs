using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    Vector2 view;
    public float distance = 10;
    Vector3 cameraCenter;

    // Start is called before the first frame update
    void Start()
    {
        view = new Vector2(20, 0);
        Cursor.lockState = CursorLockMode.Locked;
        cameraCenter = cam.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dT = Time.deltaTime;

        Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        view += delta;
        Quaternion currentView = Quaternion.Euler(-view.y * 10, view.x * 10, 0);

        cameraCenter += (target.position - cameraCenter) * 5.0f * dT;
        cam.transform.position = cameraCenter - currentView * Vector3.forward * distance;
        cam.transform.rotation = currentView;
    }
}
