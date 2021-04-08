using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooters
{
    public class Bullet : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float Speed;

        [Header("Rotation Parameters")]
        public float RotationSpeed;

        private Transform _bulletCube;

        void Start()
        {
            _bulletCube = transform.GetChild(0);    
        }

        void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            _bulletCube.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }
    }
}

