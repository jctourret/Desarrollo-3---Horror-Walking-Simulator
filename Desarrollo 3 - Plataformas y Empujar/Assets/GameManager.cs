using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
