using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScenesLoaderHandler : MonoBehaviour
{
    const string LOADING_SCENE = "LoadingScreen";
    static string sceneFrom;
    static string sceneToLoad;

    public Slider fillSlider;
    public float minimumTime = 5f;

    public string textLoading = "Loading";
    public string textContinue = "Press \"Space\" to continue";
    public TextMeshProUGUI textProgres;
    
    // ----------------------------------

    float timeLoading;
    float loadingProgress;

    float timer = 0f;
    bool continueScene = false;

    AsyncOperation operation;

    // ----------------------------------

    public static void LoadScene(string scene)
    {
        sceneFrom = SceneManager.GetActiveScene().name;
        sceneToLoad = scene;
        SceneManager.LoadScene(LOADING_SCENE, LoadSceneMode.Additive);
    }

    private IEnumerator Start()
    {
        yield return SceneManager.UnloadSceneAsync(sceneFrom);
        operation = SceneManager.LoadSceneAsync(sceneToLoad);
        
        loadingProgress = 0;
        timeLoading = 0;

        yield return null;
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            timeLoading += Time.deltaTime;
            loadingProgress = operation.progress + 0.1f;
            loadingProgress = loadingProgress * timeLoading / minimumTime;

            if (loadingProgress >= 1)
            {
                ChangeText();
                break;
            }

            UpdateBar(loadingProgress);
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && continueScene == true)
        {
            AkSoundEngine.PostEvent("loading_spacebar", gameObject);

            operation.allowSceneActivation = true;
        }
    }

    // --------------------------------

    void UpdateBar(float value)
    {
        float progress = Mathf.Clamp01(value / 0.9f);
        fillSlider.value = progress;

        timer += Time.deltaTime;

        if (timer <= 1)
        {
            textProgres.text = textLoading + ".";
        }
        else if (timer > 1 && timer <= 2)
        {
            textProgres.text = textLoading + "..";
        }
        else if (timer > 2 && timer <= 3)
        {
            textProgres.text = textLoading + "...";
        }
        else if (timer > 3)
        {
            textProgres.text = textLoading;
            timer = 0f;
        }
    }

    void ChangeText()
    {
        textProgres.text = textContinue;

        continueScene = true;
    }
}
