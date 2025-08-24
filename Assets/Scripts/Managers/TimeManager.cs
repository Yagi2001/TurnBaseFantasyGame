using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimerText;
    [SerializeField] private TextMeshProUGUI turnTimerText;

    //Later on can be moved to a S.O data
    [SerializeField] private int gameTime = 300;
    [SerializeField] private int turnTime = 30;

    private Coroutine _gameTimerCoroutine;
    private Coroutine _turnTimerCoroutine;

    private void Awake()
    {
        EventManager.GameStarted += StartGameTimer;
        EventManager.TurnCompleted += StartTourTimer;
    }

    private void OnDestroy()
    {
        EventManager.GameStarted -= StartGameTimer;
        EventManager.TurnCompleted -= StartTourTimer;
    }

    private void StartTourTimer()
    {
        if (_turnTimerCoroutine != null)
            StopCoroutine( _turnTimerCoroutine );

        _turnTimerCoroutine = StartCoroutine( TourTimer() );
    }

    private void StartGameTimer()
    {
        if (_gameTimerCoroutine != null)
            StopCoroutine( _gameTimerCoroutine );

        _gameTimerCoroutine = StartCoroutine( GameTimer() );
    }

    private IEnumerator GameTimer()
    {
        int remainingTime = gameTime;
        StartTourTimer();

        while (remainingTime > 0)
        {
            int minutes = remainingTime / 60;
            int seconds = remainingTime % 60;

            gameTimerText.text = $"Game Time : {minutes}:{seconds}";

            yield return new WaitForSeconds( 1f );
            remainingTime--;
        }

        gameTimerText.text = "Game Over!";
    }

    private IEnumerator TourTimer()
    {
        int remainingTime = turnTime;

        while (remainingTime > 0)
        {
            turnTimerText.text = "Turn time : " + remainingTime.ToString();
            yield return new WaitForSeconds( 1f );
            remainingTime--;
        }
    }
}
