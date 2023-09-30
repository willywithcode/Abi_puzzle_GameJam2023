using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _currentGameState;
    public GameState CurrentGameState => _currentGameState;
    public void StartGame()
    {
        _currentGameState = GameState.Menu;
    }
    public void ChangeState(GameState gamestate)
    {
        _currentGameState = gamestate;
    }
}
public enum GameState
{
    Menu,
    GamePlay
}
