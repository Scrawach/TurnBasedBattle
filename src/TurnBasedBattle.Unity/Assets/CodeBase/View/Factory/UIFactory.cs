using CodeBase.View.AssetManagement;
using CodeBase.View.UI;
using UnityEngine;

namespace CodeBase.View.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;

        public UIFactory(IAssets assets) =>
            _assets = assets;
        
        public BubbleText CreateDamageBubble(Vector3 at, int damage) =>
            CreateBubbleText(at, $"-{damage}", Color.red);

        public BubbleText CreateHealBubble(Vector3 at, int heal) =>
            CreateBubbleText(at, $"+{heal}", Color.green);

        private BubbleText CreateBubbleText(Vector3 at, string content, Color color)
        {
            var prefab = _assets.Instantiate<BubbleText>(AssetPath.UI.BubbleText, at);
            prefab.Construct(content, color);
            return prefab;
        }
    }
}