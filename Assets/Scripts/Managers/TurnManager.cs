using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    [SerializeField] private Team[] activeTeams;

    private int _currentTeamIndex;
    private TeamColor _playableTeamColor;
    private Transform _playableCharacter;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy( gameObject );
            return;
        }

        Instance = this;
        EventManager.TurnCompleted += OnTurnCompleted;
        //This will be moved to gameplay start
        SetColorOfPlayableTeam( activeTeams[_currentTeamIndex].teamColor );
        AdjustPlayableCharacter();
    }

    private void Start()
    {
        EventManager.OnGameStarted();
    }

    private void OnDestroy()
    {
        EventManager.TurnCompleted -= OnTurnCompleted;
    }

    public TeamColor GetColorOfPlayableTeam()
    {
        return _playableTeamColor;
    }

    public Transform GetCurrentPlayableCharacter()
    {
        return _playableCharacter;
    }

    private void SetColorOfPlayableTeam(TeamColor teamColor)
    {
        _playableTeamColor = teamColor;
    }

    private void AdjustPlayableCharacter()
    {
        var indexOfPlayableCharacter = activeTeams[_currentTeamIndex].GetIndexOfCurrentPlayableCharacter();
        _playableCharacter = activeTeams[_currentTeamIndex].characters[indexOfPlayableCharacter];
    }

    private void OnTurnCompleted()
    {
        activeTeams[_currentTeamIndex].OnTurnComplete();
        _currentTeamIndex++;

        if (_currentTeamIndex >= activeTeams.Length)
            _currentTeamIndex = 0;

        SetColorOfPlayableTeam( activeTeams[_currentTeamIndex].teamColor );
        AdjustPlayableCharacter();
    }

    ///TEST Methods
    public void TestTurnComplete()
    {
        OnTurnCompleted();
        EventManager.OnTurnCompleted();
    }
}

