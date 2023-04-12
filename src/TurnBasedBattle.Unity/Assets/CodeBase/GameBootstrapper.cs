using UnityEngine;

namespace CodeBase
{
    public class GameBootstrapper : MonoBehaviour
    {
        private BattleView _battleView;
        
        private async void Start()
        {
            _battleView = new BattleView();
            await _battleView.Process();
        }

        private void OnDestroy() =>
            _battleView.Dispose();
    }
}
