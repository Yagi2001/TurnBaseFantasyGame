using UnityEngine;

[System.Serializable]
public class Team
{
    public TeamColor teamColor;
    public int teamSize;
    public Transform[] characters;
    private int _indexOfCurrentPlayableCharacter;

    public int GetIndexOfCurrentPlayableCharacter()
    {
        return _indexOfCurrentPlayableCharacter;
    }

    public void OnTurnComplete()
    {
        if (_indexOfCurrentPlayableCharacter + 1 < characters.Length)
            _indexOfCurrentPlayableCharacter++;
        else
            _indexOfCurrentPlayableCharacter = 0;
    }
}

public enum TeamColor
{
    Blue,
    Red
}