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

        private Slider _avoidBar;


        private void Start()
        {
            _avoidBar = GetComponentInParent<PlayerController>().AvoidSlider;
            _avoidAdding = GetComponentInParent<PlayerController>().avoidAdding;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Bullet>())
            {
                if(GameManager.Instance.State == State.Avoiding)
                {
                    PlayAvoidEffect();
                    
                }
                Debug.Log(GameManager.Instance.State);
            }
                
        }

        private void OnTriggerExit(Collider other)
        {
            if (GameManager.Instance.State == State.Avoiding)
                Avoid();
        }

        void PlayAvoidEffect()
        {
            Sequence avoidSeq = DOTween.Sequence();

            avoidSeq.Append(AvoidMaterial.DOFade(.3f, 0.2f).SetEase(Ease.InQuad));
            avoidSeq.Append(AvoidMaterial.DOFade(0, 0.2f).SetEase(Ease.InQuad));

            avoidSeq.Play();
        }

        void Avoid()
        {
            _avoidBar.value += _avoidAdding;
            if (_avoidBar.value >= _avoidBar.maxValue)
                GameManager.Instance.ChangeState(State.Eating);
        }


    }
}

