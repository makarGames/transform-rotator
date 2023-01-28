#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utils.TransformRotating
{
    public enum Axis
    {
        Back,
        Down,
        Forward,
        Left,
        Right,
        Up,
        Zero
    };

    [ExecuteAlways]
    public class TransformRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 300f;
        [SerializeField] private Axis _rotationAxis = Axis.Up;
        [SerializeField] private bool _rotateByStart = false;
        
        private static readonly Dictionary<Axis, Vector3> EulerByAxis = new Dictionary<Axis, Vector3>()
        {
            { Axis.Back, Vector3.back },
            { Axis.Down, Vector3.down },
            { Axis.Forward, Vector3.forward },
            { Axis.Left, Vector3.left },
            { Axis.Right, Vector3.right },
            { Axis.Up, Vector3.up },
            { Axis.Zero, Vector3.zero },
        };
        
        private Transform _cashedTransform;
        private bool _isRotating;
        
        private void Awake()
        {
            _cashedTransform = transform;
            _isRotating = _rotateByStart;
        }

        private void FixedUpdate()
        {
            if (_isRotating)
            {
                _cashedTransform.Rotate(EulerByAxis[_rotationAxis], Time.fixedDeltaTime * _rotationSpeed);
            }
        }

        private void EnableRotate(bool enable)
        {
            _isRotating = enable;
        }

        public void SetRotationSpeed(float speed)
        {
            _rotationSpeed = speed;
        }

#if UNITY_EDITOR
        public void StartRotate()
        {
            if (Application.isEditor && !_isRotating)
            {
                EditorApplication.update += FixedUpdate;
            }

            EnableRotate(true);
        }

        public void StopRotate()
        {
            if (Application.isEditor)
            {
                EditorApplication.update -= FixedUpdate;
            }

            EnableRotate(false);
        }
#endif
    }
}