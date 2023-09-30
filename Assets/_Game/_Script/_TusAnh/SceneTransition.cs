using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator _animatorSceneChange;
    

    public void Start()
    {
     
    }
    public void enableTransition()
    {
        _animatorSceneChange.SetTrigger("Trigger");
        StartCoroutine(disabeTime());
    }

    private IEnumerator disabeTime()
    {
        yield return new WaitForSeconds(4f);
        _animatorSceneChange.ResetTrigger("Trigger");
    }
}
