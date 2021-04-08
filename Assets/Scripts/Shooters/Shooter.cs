using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooters
{
    public class Shooter : MonoBehaviour
    {
        [Header("Bullets to shoot")]
        public List<GameObject> pool;

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
                Shoot();
        }

        public void Shoot()
        {
            GameObject bullet = GetFreeObject();
            bullet.transform.position = transform.position;
            bullet.transform.LookAt(Player);
            
            bullet.SetActive(true);

            _timer = Random.Range(RangeTimeBetweenShots.x, RangeTimeBetweenShots.y);
        }

        public GameObject GetFreeObject() 
        { 
            return pool.Find(item => item.activeInHierarchy == false);
        }
    }
}

