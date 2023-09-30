using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private Transform currentMovePlatform;
    private float speed = 4f;
    private Transform map;
    private Vector2 posTouchDown;
    private Vector2 posTouchUp;
    [SerializeField] private bool _isFirstTough = true;
    private bool _canAttach = false;
    private Rigidbody2D rb;
    private Transform tf;
    private Tween move;
    private List<Tween> listTweenMove= new List<Tween>();
    private int countStep = 0;
    private float countTime = 0;


    float angle;
    RaycastHit2D hit;
    void Start()
    {
        tf = transform;
        rb = GetComponent<Rigidbody2D>();
        map = LevelManager.Instance.CurrentLevel;
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
            Debug.Log(1);
            _isFirstTough = false;
            countTime = 0;
            posTouchUp = Input.mousePosition;
            if (posTouchUp.x - posTouchDown.x < 0)
            {
                angle += 90;
                int currentStep  = countStep;
                listTweenMove.Add( map.DOLocalRotateQuaternion(Quaternion.Euler(map.rotation.x, map.rotation.y, angle), .7f)
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
                tf.SetParent(LevelManager.Instance.CurrentLevel);
                listTweenMove.Add(map.DOLocalRotateQuaternion(Quaternion.Euler(map.rotation.x, map.rotation.y, angle), .7f)
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
        _canAttach = true;
        tf.SetParent(LevelManager.Instance.CurrentLevel);
        hit = Physics2D.Raycast(transform.position - new Vector3(0, 1, 0) * 0.01f, Vector2.down, Mathf.Infinity, mapLayer);
        if (!hit.IsUnityNull())
        {
            move = tf.DOMoveY(hit.transform.position.y, Mathf.Abs(tf.position.y- hit.transform.position.y)/speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                
                if (hit.transform.CompareTag("MoveInstant"))
                {
                    tf.position = hit.transform.GetComponent<MoveInstant>().pair.transform.position;
                    RaycastHit2D secondHit = Physics2D.Raycast(transform.position - new Vector3(0,1,0) * 0.01f, Vector2.down, Mathf.Infinity, mapLayer);
                    if (!secondHit.IsUnityNull())
                    {
                        move = tf.DOMoveY(hit.transform.position.y, Mathf.Abs(tf.position.y - hit.transform.position.y) / speed).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            listTweenMove.Clear();
                            countStep = 0;
                            _isFirstTough = true;
                            _canAttach = false;
                        });
                    }
                }
                else
                {
                    listTweenMove.Clear();
                    countStep = 0;
                    _isFirstTough = true;
                    _canAttach = false;
                }
            });
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
        {
            _canAttach = false;
            tf.position += new Vector3(0, 0.1f, 0);
            listTweenMove.Clear();
            countStep = 0;
            _isFirstTough = true;
            move.Kill();
        }
    }
}