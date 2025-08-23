using System;

public static class EventManager
{
    public static event Action<TeamColor> TurnCompleted;

    public static void OnTurnCompleted(TeamColor teamColor)
    {
        TurnCompleted?.Invoke(teamColor);
    }
}
