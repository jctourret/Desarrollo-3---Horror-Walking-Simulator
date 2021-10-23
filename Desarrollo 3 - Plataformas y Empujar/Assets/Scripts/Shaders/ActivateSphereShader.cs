using UnityEngine;

public class ActivateSphereShader : MonoBehaviour
{
    [SerializeField] private LayerMask myLayerMask;
    [SerializeField] private float speedToActivate = 1f;
    [SerializeField] private float maxScale = 6f;
    
    private Camera cam;
    private float scale = 0f;

    private void Start()
    {
        cam = CameraBehaviour.GetCamera();

        scale = this.transform.localScale.x;
    }

    private void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, this.transform.position - cam.transform.position, out hit, Mathf.Infinity, myLayerMask))
        {
            if(hit.collider.transform.tag == "ShaderSphere")
            {
                if(scale > 0)
                {
                    scale -= Time.deltaTime * speedToActivate;

                    this.transform.localScale = new Vector3(scale, scale, scale);
                }


                if (scale < 0)
                {
                    scale = 0f;
                }
            }
            else
            {
                if (scale < maxScale)
                {
                    scale += Time.deltaTime * speedToActivate;

                    this.transform.localScale = new Vector3(scale, scale, scale);
                }

                if (scale > maxScale)
                {
                    scale = maxScale;
                }
            }
        }
    }
}
