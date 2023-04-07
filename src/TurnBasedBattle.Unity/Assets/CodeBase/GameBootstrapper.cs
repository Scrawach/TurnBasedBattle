using UnityEngine;

namespace CodeBase
{
    public class GameBootstrapper : MonoBehaviour
    {
        private async void Start()
        {
            var battle = new Battle();
            await battle.Process();
        }
    }
}
