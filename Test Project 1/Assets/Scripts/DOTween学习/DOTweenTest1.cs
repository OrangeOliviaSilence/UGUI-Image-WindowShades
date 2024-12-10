using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenTest1 : MonoBehaviour
{
    void Awake()
    {
        DOTween.Init();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.DOKill();
            //transform.DOMove(endValue: 10 * Vector3.forward, duration: 1, snapping: false);
            //transform.DOJump(endValue: transform.position + Vector3.right*2, jumpPower: 1, numJumps: 10, duration: 5, snapping: false).SetEase(Ease.Linear);
            //transform.DOPunchPosition(punch: Vector3.zero, duration: 2, vibrato: 1, elasticity: 1).SetEase(Ease.Linear);
            //DOTween.To(getter: () => transform.position, setter: val => transform.position = val, endValue: transform.position + Vector3.right, duration: 1).SetEase(Ease.Linear);
            transform.DOPath(
				path: new Vector3[] {Vector3.right, Vector3.forward, Vector3.up},
                duration: 1).
                SetEase(Ease.Linear);
        }
    }
}
