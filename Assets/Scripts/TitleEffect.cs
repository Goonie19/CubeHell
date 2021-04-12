using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakePosition(0.2f, 5, 20).SetLoops(-1).SetEase(Ease.Linear).Play();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
