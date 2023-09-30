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
    private Vector2 posTouchDown;
    private Vector2 posTouchUp;
    bool _isFirstTough = true;
    Transform tf;
    Tween move;
    List<Tween> listTweenMove= new List<Tween>();
    int countStep = 0;
    float countTime = 0;


    float angle;
    void Start()
    {
        tf = transform;
        Debug.Log(map);
        angle = map.eulerAngles.z;
    }
    void Update()
    {
        countTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            posTouchDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (countTime > .7f && !_isFirstTough) return;
            countTime = 0;
            posTouchUp = Input.mousePosition;
            if (posTouchUp.x - posTouchDown.x < 0)
            {
                angle += 90;
                int currentStep  = countStep;
                listTweenMove.Add( map.DOLocalRotateQuaternion(Quaternion.Euler(map.rotation.x, map.rotation.y, angle), 1)
                    .OnComplete(() =>
                    {
                        if (currentStep != listTweenMove.Count - 1) listTweenMove[currentStep + 1].Play();
                        else MoveToBottom();
                    }));
                listTweenMove[currentStep].Pause();
                if (countStep == 0) listTweenMove[0].Play();
                countStep++;
            }
            else if (posTouchUp.x - posTouchDown.x > 0)
            {
                angle -= 90;
                int currentStep = countStep;
                listTweenMove.Add(map.DOLocalRotateQuaternion(Quaternion.Euler(map.rotation.x, map.rotation.y, angle), 1)
                    .OnComplete(() =>
                    {
                        if (currentStep != listTweenMove.Count - 1) listTweenMove[currentStep + 1].Play();
                        else MoveToBottom();
                    }));
                listTweenMove[currentStep].Pause();
                if (countStep == 0) listTweenMove[0].Play();
                countStep++;
            }
        }
    }
    public void MoveToBottom()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, mapLayer);
        if (!hit.IsUnityNull())
        {
            move = tf.DOMoveY(hit.transform.position.y, 1).OnComplete(() =>
            {
                
                if (hit.transform.CompareTag("MoveInstant"))
                {
                    tf.position = hit.transform.GetComponent<MoveInstant>().pair.transform.position;
                    RaycastHit2D secondHit = Physics2D.Raycast(transform.position - new Vector3(0,1,0) * 0.03f, Vector2.down, Mathf.Infinity, mapLayer);
                    if (!secondHit.IsUnityNull())
                    {
                        move = tf.DOMoveY(hit.transform.position.y, 1).OnComplete(() =>
                        {
                            listTweenMove.Clear();
                            countStep = 0;
                            _isFirstTough = true;
                        });
                    }
                }
                else
                {
                    listTweenMove.Clear();
                    countStep = 0;
                    _isFirstTough = true;
                }
            });
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
        {
            tf.position += new Vector3(0, 0.05f, 0);
            listTweenMove.Clear();
            countStep = 0;
            _isFirstTough = true;
            move.Kill();
        }
    }
}