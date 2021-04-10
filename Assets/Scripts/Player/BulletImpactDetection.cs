using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Shooters;
using UnityEngine.UI;

namespace Player
{
    public class BulletImpactDetection : MonoBehaviour
    {

        private float _avoidAdding;
        private float _eatingAdding;
        

        private Slider _avoidSlider;
        private GameObject _playerCube;


        // Start is called before the first frame update
        void Start()
        {
            _avoidAdding = GetComponentInParent<PlayerController>().avoidAdding;
            _playerCube = GetComponentInParent<PlayerController>().gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.GetComponentInParent<Bullet>())
            {

                if(GameManager.Instance.State == State.Avoiding)
                {

                    other.gameObject.GetComponent<Bullet>().Break();
                    HitEffect();
                } else if(GameManager.Instance.State == State.Eating)
                {
                    if (GameManager.Instance.desiredSize > transform.parent.localScale.x)
                    {

                        EatEffect();
                        other.GetComponent<Bullet>().EatBullet();
                    } else
                        GameManager.Instance.ChangeState(State.End);

                } 


            }
        }



        void HitEffect()
        {
            GameManager.Instance.AddToAvoidBar(-_avoidAdding);
            GameManager.Instance.HitEffect();
        }

        void EatEffect()
        {
            Sequence CubeSeq = DOTween.Sequence();

            CubeSeq.Append(_playerCube.transform.DOScale(_playerCube.transform.localScale * 1.01f, 0.2f).SetEase(Ease.InQuad));

            CubeSeq.Play();
        }

        
    }
}


