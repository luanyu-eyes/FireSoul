using System;
using UnityEngine;

namespace Script.Camera
{
    public class CameraMove : MonoBehaviour
    {
        private GameObject _target;
        public float moveSpeed;

        private Vector3 _distance;
        void Start()
        {
            var list = GameObject.FindGameObjectWithTag("Player");
            _target = list;
            _distance = transform.position - _target.transform.position;
        }

        void LateUpdate()
        {
            if (_target == null)
            {
                var list = GameObject.FindGameObjectWithTag("Player");
                if(list)
                {
                    _target = list;
                }
            }else
            {
                transform.position = Vector3.Lerp(transform.position, _distance + _target.transform.position,
                    moveSpeed * Time.deltaTime);
            }
        }
    }
}
