using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;

    private void Start()
    {
        cam = CameraBehaviour.GetCamera();
    }

    private void LateUpdate()
    {
        if (cam != null)
        {
            Vector3 adjustedCam = cam.transform.position;
            adjustedCam.y = gameObject.transform.position.y;
            this.transform.LookAt(adjustedCam);
        }
    }
}
