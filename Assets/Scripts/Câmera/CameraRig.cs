using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CameraRig : MonoBehaviour
{
    public Transform y_axix;
    public Transform x_axis;
    public float moveTime;

  
    public void AlignTo(Transform target)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(y_axix.DOMove(target.position, 0.75f));
        seq.Join(y_axix.DORotate(new Vector3 (0f, target.rotation.eulerAngles.y, 0f), 0.75f));
        seq.Join(x_axis.DOLocalRotate(new Vector3(target.rotation.eulerAngles.x, 0f, 0f), 0.75f));
    }
}
