using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerMovement.RecieveCamera += GetCamera;
        CallCameraTrigger.RecieveCamera += GetCamera;
        EnemyAI.RecieveCamera += GetCamera;
    }

    private void OnDisable()
    {
        PlayerMovement.RecieveCamera -= GetCamera;
        CallCameraTrigger.RecieveCamera -= GetCamera;
        EnemyAI.RecieveCamera -= GetCamera;
    }

    Camera GetCamera()
    {
        return gameObject.GetComponent<Camera>();
    }
}
