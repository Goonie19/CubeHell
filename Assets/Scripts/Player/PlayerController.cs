using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

        private Vector2 _input;
        private Rigidbody _rb;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            
        }
    }
}

