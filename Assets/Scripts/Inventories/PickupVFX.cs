using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RPG.Inventories
{
    public class PickupVFX : MonoBehaviour
    {
        private void Start()
        {
            float startPosY = transform.position.y;
            transform.DOMoveY(startPosY + .15f, 1f).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);
            transform.DORotate(new Vector3(0, 10f, 0f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
