using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Shooters;
using UnityEngine.UI;

namespace Player
{
    public class AvoidEffect : MonoBehaviour
    {

        public Material AvoidMaterial;

        private float _avoidAdding;

        private void Start()
        {
            _avoidAdding = GetComponentInParent<PlayerController>().avoidAdding;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Bullet>())
            {
                if(GameManager.Instance.State == State.Avoiding)
                {
                    PlayDangerEffect();
                    
                }
            }
                
        }

        private void OnTriggerExit(Collider other)
        {
            if (GameManager.Instance.State == State.Avoiding)
            {
                GameManager.Instance.AddToAvoidBar(_avoidAdding);
                GameManager.Instance.AvoidEffect();
            }
        }

        void PlayDangerEffect()
        {
            Sequence avoidSeq = DOTween.Sequence();

            avoidSeq.Append(AvoidMaterial.DOFade(.3f, 0.2f).SetEase(Ease.InQuad));
            avoidSeq.Append(AvoidMaterial.DOFade(0, 0.2f).SetEase(Ease.InQuad));

            avoidSeq.Play();
        }

       


    }
}

