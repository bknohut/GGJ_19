using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> GroundObjects;
    public Transform Sponge;
    public Transform Broom;
    public CharacterController CharacterController;

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
    public void OnRetry()
    {
        Sponge.gameObject.SetActive(true);
        Broom.gameObject.SetActive(true);
        Sponge.GetComponent<Collider2D>().enabled = true;
        Broom.GetComponent<Collider2D>().enabled = true;
        UIManager.instance.Hud.OnRetry();

        foreach (Transform t in GroundObjects)
        {
            for( int i = 0; i < t.childCount; i++)
            {
                t.GetChild(i).GetComponent<Ground>().OnRetry();
            }
        }
        Sponge.position = new Vector3(2, 2, 0);
        Broom.position = new Vector3(-5, -3, 0);
        CharacterController.transform.position = new Vector3(0, 0, 0);
        CharacterController.equipment = CharacterController.Equipment.NONE;
    }
}
