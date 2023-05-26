using System;
using UnityEngine;

namespace Script.Scene
{
    public class BackGroundMove : MonoBehaviour
    {
        public Vector2 moveSpeed;

        private Transform _cameraTransform;
        private Vector3 _lastPosition;

        private void Start()
        {
            if (UnityEngine.Camera.main != null) _cameraTransform = UnityEngine.Camera.main.transform;
            _lastPosition = _cameraTransform.position;
        }

        public void LateUpdate()
        {
            var deltaPosition = _cameraTransform.position - _lastPosition;
            transform.position +=
                new Vector3(deltaPosition.x * moveSpeed.x, deltaPosition.y * moveSpeed.y, deltaPosition.z);
            _lastPosition = _cameraTransform.position;
        }
    }
}
