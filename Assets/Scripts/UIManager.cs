using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    public void EnterThegame()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        LevelManager.Instance.CurrentLevel.gameObject.SetActive(true);
    }
    public void EnterLevel(int i)
    {
        Destroy(LevelManager.Instance.CurrentLevel.gameObject);
        LevelManager.Instance.InstantiateLevel(i);

    }
    
}
