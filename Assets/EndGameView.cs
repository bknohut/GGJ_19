using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndGameView : MonoBehaviour
{
    private static EndGameView _instance;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _popup;
    private static int _score;

    private void Awake()
    {
        _instance = this;
    }
    public static void IncreaseScore()
    {   
        if( ++_score == 21)
        {
            TimeUp();
        }
    }
    public static void ResetScore()
    {
        _score = 0;
    }
    public static void TimeUp()
    {   
        if( _instance._popup.activeSelf)
        {
            return;
        }
        Time.timeScale = 0;
        _instance._scoreText.text = $"{_score}/21";
        _instance._popup.SetActive(true);
    }
}
