using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private Transform map;
    Vector2 posTouchDown;
    Vector2 posTouchUp;
    bool _cantough = false;
    Transform tf;
    Tween move;
    void Start()
    {
        tf = transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            posTouchDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && !_cantough)
        {
            _cantough = true;
            posTouchUp = Input.mousePosition;
            if (posTouchUp.x - posTouchDown.x < 0)
            {
                map.DOLocalRotateQuaternion(Quaternion.Euler(map.rotation.x, map.rotation.y, map.eulerAngles.z + 90), 1)
                    .OnComplete(MoveToBottom);
            }
            else if (posTouchUp.x - posTouchDown.x > 0)
            {
                map.DOLocalRotateQuaternion(Quaternion.Euler(map.rotation.x, map.rotation.y, map.eulerAngles.z - 90), 1)
                    .OnComplete(MoveToBottom);
            }
        }
    }
    public void MoveToBottom()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,Mathf.Infinity, mapLayer);
        if (!hit.IsUnityNull())
        {
            move = tf.DOMoveY(hit.transform.position.y, 1).OnComplete(() =>
            {
                _cantough = false;
            });
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Map"))
        {
            tf.position += new Vector3(0, 0.05f, 0);
            move.Kill();
            _cantough = false;
        }
    }
}
