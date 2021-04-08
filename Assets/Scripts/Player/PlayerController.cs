using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Shooters;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

        public float Speed;
        public Material AvoidMaterial;


        private Vector2 _input;
        private Rigidbody _rb;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            _rb.velocity = new Vector3(_input.x, 0, _input.y).normalized * Speed;
        }

        // Update is called once per frame
        void Update()
        {
            _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Bullet>())
                PlayAvoidEffect();
        }

        void PlayAvoidEffect()
        {
            Sequence avoidSeq = DOTween.Sequence();

            avoidSeq.Append(AvoidMaterial.DOFade(1, 0.2f).SetEase(Ease.InQuad));
            avoidSeq.Append(AvoidMaterial.DOFade(0, 0.2f).SetEase(Ease.InQuad));

            avoidSeq.Play();
        }

    }
}

