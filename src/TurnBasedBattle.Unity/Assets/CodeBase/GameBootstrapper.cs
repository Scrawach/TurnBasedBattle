using System;
using UnityEngine;

namespace CodeBase
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Battle _battle;
        
        private async void Start()
        {
            _battle = new Battle();
            await _battle.Process();
        }

        private void OnDestroy()
        {
            Debug.Log("DESTORY");
            _battle.Dispose();
        }
    }
}
