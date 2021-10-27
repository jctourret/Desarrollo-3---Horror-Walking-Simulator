using UnityEngine;
using System.Collections;

public class NightLight_Behaviour : MonoBehaviour
{
    [SerializeField] private Color normalColorMaterial = Color.yellow;
    [SerializeField] private Color collapsingColorMaterial = Color.red;
    [SerializeField] private Light pointLight;

    [SerializeField] private float minDelay = 5f;
    [SerializeField] private float maxDelay = 10f;
    private float delay = 0f;


    private Material material;
    private bool flashing = false;
    private float timer = 0f;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;

        material.SetColor("_EmissionColor", normalColorMaterial * 0);
        pointLight.intensity = 0;

        delay = Random.Range(minDelay, maxDelay);
    }

    //==========================================

    public void SetIntensityOfLight(bool isCollapsing, float value)
    {
        if (flashing == false)
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

            if (timer > delay)
            {
                timer = 0f;
                StartCoroutine(FlashingLights());
            }
            else
            {
                timer += Time.deltaTime;
            }
        }        
    }

    IEnumerator FlashingLights()
    {
        float time = 0f;

        flashing = true;

        while (time < 0.6f)
        {
            time += Time.deltaTime;

            pointLight.intensity = Mathf.PingPong(Time.time, 6);

            yield return null;
        }

        flashing = false;

    }
}