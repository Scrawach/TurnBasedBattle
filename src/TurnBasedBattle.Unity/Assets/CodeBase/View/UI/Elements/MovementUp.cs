using UnityEngine;

namespace CodeBase.View.UI.Elements
{
    public class MovementUp : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void Update()
        {
            var step = Time.deltaTime * _speed;
            var movement = Vector3.up * step;
            transform.Translate(movement, Space.World);
        }
    }
}