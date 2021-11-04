using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timer = 30;

    [SerializeField]
    private Animator bodyAnimator;
    
    enum GameState
    {
        Prepare,
        Running,
        Stop
    }
    
    private GameState gameState = GameState.Prepare;
    
    public float Timer
    {
        get => timer;
        set
        {
            timer = value;
            TimerText.text = timer.ToString(CultureInfo.CurrentCulture) + "s";
        }
    }

    [SerializeField] private TMP_Text TimerText; 
    
    IEnumerator StartGameTimer()
    {
        gameState = GameState.Running;
        while (Timer > 0)
        {
            Timer--;
            yield return new WaitForSeconds(1);
        }
        StopGame();
    }

    void StopGame()
    {
        gameState = GameState.Stop;
        SetStats();
        bodyAnimator.SetTrigger("GameOver");
    }
}

