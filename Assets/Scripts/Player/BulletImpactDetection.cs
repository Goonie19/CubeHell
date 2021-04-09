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

        private Slider _avoidSlider;

        // Start is called before the first frame update
        void Start()
        {
            _avoidSlider = GetComponentInParent<PlayerController>().AvoidSlider;
            _avoidAdding = GetComponentInParent<PlayerController>().avoidAdding;

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                if(GameManager.Instance.State == State.Avoiding)
                {
                    other.gameObject.GetComponent<Bullet>().Break();
                    HitEffect();
                } 

            }
        }

        void HitEffect()
        {
            _avoidSlider.value -= _avoidAdding;
        }
    }
}

