using System;
using UnityEngine;

namespace CodeBase.View.UI.Elements
{
    public class MovementUpCurve : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _speed;

        private float _elapsedTime;
        
        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            var step = Time.deltaTime * _speed.Evaluate(_elapsedTime);
            var movement = Vector3.up * step;
            transform.Translate(movement, Space.World);
        }
    }
}