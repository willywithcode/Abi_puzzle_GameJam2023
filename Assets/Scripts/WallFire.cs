using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFire : MonoBehaviour
{
    public void LossGame()
    {
        LevelManager.Instance.RePlayLevel();
    }
}
