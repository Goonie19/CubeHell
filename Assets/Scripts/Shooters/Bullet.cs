using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Shooters
{
    public class Bullet : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float Speed;

        [Header("Rotation Parameters")]
        public float RotationSpeed;

        [Header("Disapearing distance from center")]
        public float DisappearingDistance;

        private Transform _bulletCube;

        void Start()
        {
            _bulletCube = transform.GetChild(0);

        }

        void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            _bulletCube.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, new Vector3(0, transform.position.y, 0)) > DisappearingDistance)
                gameObject.SetActive(false);
        }
    }
}

