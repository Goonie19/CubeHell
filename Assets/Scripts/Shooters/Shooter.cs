using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooters
{
    public class Shooter : MonoBehaviour
    {

        public List<GameObject> pool;

        public float TimeBetweenShots;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject GetFreeObject() 
        { 
            return pool.Find(item => item.activeInHierarchy == false);
        }
    }
}

