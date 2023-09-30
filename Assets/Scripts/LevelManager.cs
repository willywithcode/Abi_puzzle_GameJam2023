using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Transform> listLevel;
    private Transform _currentLevel;
    private int _level;
    public Transform CurrentLevel => _currentLevel;
    public int Level => _level;
    private void Start()
    {
        _currentLevel=  Instantiate(listLevel[0]);
        _level = 0;
        _currentLevel.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }
    public void InstantiateLevel(int i)
    {
        _level= i;
        _currentLevel = Instantiate(listLevel[i]);
        _currentLevel.gameObject.SetActive(true);
    }
    public void GetNextLevel()
    {
            Destroy(_currentLevel.gameObject);
            if (_level == 4)
            {
                _currentLevel = Instantiate(listLevel[0]);
                _level = 0;
            }
            else
            {
                _currentLevel = Instantiate(listLevel[_level + 1]);
                _level++;
            }
    }
    public void RePlayLevel()
    {
        
            Destroy(_currentLevel.gameObject);
            _currentLevel = Instantiate(listLevel[_level]);
    }
}
