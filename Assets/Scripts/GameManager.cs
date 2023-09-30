using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _currentGameState;
    public GameState CurrentGameState => _currentGameState;
    public GameObject Transition;
    public GameObject GameSystem;
    public void StartGame()
    {
        _currentGameState = GameState.Menu;
     
    }
    public void ChangeState(GameState gamestate)
    {
        _currentGameState = gamestate;
     
    }
    public void GoToGame()
    {
        Transition.gameObject.SetActive(true);
        StartCoroutine(LoadGame());
    }
    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2f);
        GameSystem.gameObject.SetActive(true);
    }
    public void TurnOn()
    {
        Transition.gameObject.SetActive(true);

    }
}



public enum GameState
{
    Menu,
    GamePlay
}
