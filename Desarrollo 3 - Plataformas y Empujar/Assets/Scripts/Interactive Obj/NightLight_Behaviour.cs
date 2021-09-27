using UnityEngine;

public class NightLight_Behaviour : MonoBehaviour
{
    [SerializeField] Color normalColorMaterial = Color.yellow;
    [SerializeField] Color collapsingColorMaterial = Color.red;
    [SerializeField] Light pointLight;

    bool isActive = false;
    Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        material.SetColor("_EmissionColor", normalColorMaterial * 0);
        pointLight.intensity = 0;
    }

    //==========================================

    public void SetIntensityOfLight(bool isCollapsing, float value)
    {
        if (isCollapsing)
        {
            material.SetColor("_EmissionColor", collapsingColorMaterial * value);
            pointLight.color = collapsingColorMaterial;
        }
        else
        {
            material.SetColor("_EmissionColor", normalColorMaterial * value);
            pointLight.color = normalColorMaterial;
        }

        pointLight.intensity = value;
    }
}