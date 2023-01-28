using UnityEngine;
using System.Collections.Generic;

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
        [SerializeField] private UpdateType _updateType;

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
        }

        private void Start()
        {
            if (_rotateByStart)
            {
                StartRotate();
            }
        }

        private void FixedUpdate()
        {
            if (_isRotating && _updateType == UpdateType.FixedUpdate)
            {
                _cashedTransform.Rotate(EulerByAxis[_rotationAxis], Time.fixedDeltaTime * _rotationSpeed);
            }
        }

        private void Update()
        {
            if (_isRotating && _updateType == UpdateType.Update)
            {
                _cashedTransform.Rotate(EulerByAxis[_rotationAxis], Time.deltaTime * _rotationSpeed);
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

        private void OnDisable()
        {
            StopRotate();
        }
        
        public void StartRotate()
        {
            EnableRotate(true);
        }

        public void StopRotate()
        {
            EnableRotate(false);
        }
    }

    internal enum UpdateType
    {
        FixedUpdate,
        Update
    }
}