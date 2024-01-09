using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitySceneReload : MonoBehaviour
{
    private void OnGUI() 
    {
        if (GUI.Button(new Rect(50,50,50,50), "Reload"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
