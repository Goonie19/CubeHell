using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Shooters
{
    public class Bullet : MonoBehaviour
    {
        [Header("Rotation Parameter")]
        public float RotationSpeed;

        [Header("Disapearing distance from center")]
        public float DisappearingDistance;

        private Transform _bulletCube;
        private ParticleSystem _dieParticles;
        private float _bulletSpeed;

        void Awake()
        {
            _bulletCube = transform.GetChild(0);
            _dieParticles = GetComponentInChildren<ParticleSystem>();
            _bulletSpeed = GameManager.Instance.bulletSpeed;
        }

        private void OnEnable()
        {
            if (!_bulletCube.gameObject.activeInHierarchy)
                _bulletCube.gameObject.SetActive(true);

        }

        void Update()
        {
            if(_bulletCube.gameObject.activeInHierarchy)
                transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);

            _bulletCube.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, new Vector3(0, transform.position.y, 0)) > DisappearingDistance)
                gameObject.SetActive(false);
        }

        #region BULLET IMPACTS
        public void Break()
        {
            _dieParticles.Play();
            _bulletCube.gameObject.SetActive(false);
            StartCoroutine(Die());
        }

        public void EatBullet()
        {
            gameObject.SetActive(false);
        }

        IEnumerator Die()
        {
            yield return new WaitForSeconds(0.2f);

            gameObject.SetActive(false);
        }
        #endregion
    }
}

