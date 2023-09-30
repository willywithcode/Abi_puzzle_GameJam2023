using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject map;
    // Start is called before the first frame update
    public void onButtonPlayClick() {
        this.gameObject.SetActive(false);
        map.SetActive(true);
    }
}
