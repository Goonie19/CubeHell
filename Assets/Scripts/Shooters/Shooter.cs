using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooters
{
    public class Shooter : MonoBehaviour
    {
        [Header("Bullets to shoot")]
        public List<GameObject> pool;
        public Material avoidMaterial;
        public Material eatingMaterial;

        [Header("Range of time between shots")]
        public Vector2 RangeTimeBetweenShots;

        [Header("Player reference")]
        public Transform Player;

        private float _timer;
        // Start is called before the first frame update
        void Start()
        {
            _timer = Random.Range(RangeTimeBetweenShots.x, RangeTimeBetweenShots.y);
        }

        // Update is called once per frame
        void Update()
        {
            if (_timer > 0)
                _timer -= Time.deltaTime;
            else
                if(GameManager.Instance.State == State.Avoiding || GameManager.Instance.State == State.Eating)
                    Shoot();
        }

        #region SHOOTING METHODS

        public void Shoot()
        {
            GameObject bullet = GetFreeObject();
            bullet.transform.position = transform.position;

            //If player has to eat bullets, it stops shooting directly to player
            if (GameManager.Instance.State == State.Avoiding)
            {
                bullet.transform.LookAt(Player);
                bullet.transform.GetComponentInChildren<Renderer>().material = avoidMaterial;
            }
            else if (GameManager.Instance.State == State.Eating)
            {
                bullet.transform.rotation = transform.rotation;
                bullet.transform.GetComponentInChildren<Renderer>().material = eatingMaterial;
            }

            bullet.SetActive(true);

            _timer = Random.Range(RangeTimeBetweenShots.x, RangeTimeBetweenShots.y);
        }

        public GameObject GetFreeObject() 
        { 
            return pool.Find(item => item.activeInHierarchy == false);
        }

        #endregion
    }
}

