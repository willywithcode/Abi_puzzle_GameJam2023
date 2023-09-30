using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Transform> listLevel;
    private Transform _currentLevel;
    public Transform CurrentLevel => _currentLevel;
    private void Start()
    {
        _currentLevel=  Instantiate(listLevel[0]);
        _currentLevel.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
