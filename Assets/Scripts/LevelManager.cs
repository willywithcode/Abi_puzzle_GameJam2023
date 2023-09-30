using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform _currentLevel;
    public Transform CurrentLevel => _currentLevel;

}
