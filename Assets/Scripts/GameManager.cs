using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> Players = new List<GameObject>();

    private void Awake()
    {
        Application.runInBackground = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        if ( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
}
