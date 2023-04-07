using UnityEngine;

namespace CodeBase.View.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start() =>
            _mainCamera = Camera.main;

        private void LateUpdate()
        {
            var rotation = _mainCamera.transform.localRotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}