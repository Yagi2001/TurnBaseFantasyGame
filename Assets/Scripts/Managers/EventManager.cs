using System;

public static class EventManager
{
    public static event Action TurnCompleted;
    public static event Action GameStarted;

    public static void OnTurnCompleted()
    {
        TurnCompleted?.Invoke();
    }

    public static void OnGameStarted()
    {
        GameStarted?.Invoke();
    }
}
