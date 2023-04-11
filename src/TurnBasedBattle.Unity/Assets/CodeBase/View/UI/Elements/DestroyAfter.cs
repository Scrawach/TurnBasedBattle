using UnityEngine;

namespace CodeBase.View.UI.Elements
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] private float _timeInSeconds;

        private void Start() =>
            Destroy(gameObject, _timeInSeconds);
    }
}