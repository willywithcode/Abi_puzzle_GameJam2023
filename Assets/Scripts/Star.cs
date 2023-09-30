using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public void CallNextLevel()
    {
        Debug.Log(1); 
        gameObject.SetActive(false);
        LevelManager.Instance.GetNextLevel();
    }
}
