using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private TypeMove typeMove;
    [SerializeField] private float distanceMove;
    private Transform tf;
    private void Start()
    {
        tf = transform;
        if (typeMove == TypeMove.horizontal) tf.DOLocalMoveX(tf.localPosition.x + distanceMove, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        if (typeMove == TypeMove.vertical) tf.DOLocalMoveY(tf.localPosition.y + distanceMove, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    public void AtaachFlatform(Transform player)
    {
        player.SetParent(tf);
    }
    public Transform TF => tf;
}
public enum TypeMove
{
    vertical,
    horizontal
}
