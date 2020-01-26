using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting
{
    public abstract class CameraFollowToPerson : MonoBehaviour
    {
        [SerializeField]
        private Transform _targetTransform;
        [SerializeField]
        private Transform _rightEndPointTransform;
        [SerializeField]
        private Transform _leftEndPointTransform;

        private Transform _transform;
        private Camera _camera;

        protected float _horizontalExtent;

        private void Awake()
        {
            InitComponent();
        }
        private void Update()
        {
            if (_targetTransform != null)
            {
                _transform.position = new Vector3(
                Mathf.Clamp(_targetTransform.position.x,
                    _leftEndPointTransform.position.x + _horizontalExtent,
                    _rightEndPointTransform.position.x - _horizontalExtent),
                _transform.position.y,
                _transform.position.z);
            }
             
        }
        public void SetTansforms(Transform targetTansform, Transform leftEndPoint, Transform rightEndPoint)
        {
            _targetTransform = targetTansform;
            _leftEndPointTransform = leftEndPoint;
            _rightEndPointTransform = rightEndPoint;
        }
        private void InitComponent()   
        {
            _transform = GetComponent<Transform>();
            _camera = GetComponent<Camera>();
            _horizontalExtent = _camera.orthographicSize * Screen.width / Screen.height;
            InitCamera();
        }
        protected abstract void InitCamera();
             

    }
}

