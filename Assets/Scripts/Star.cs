using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public void CallNextLevel()
    {
        gameObject.SetActive(false);
        LevelManager.Instance.GetNextLevel();
    }
}
