using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Shooters;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

        public float Speed;
        public float avoidAdding;

        public AudioClip Hit, Eat, Avoid;
        public AudioSource Asource;

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

    }
}

