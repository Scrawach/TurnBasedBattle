using CodeBase.View.UI;
using UnityEngine;

namespace CodeBase.View.Factory
{
    public interface IUIFactory
    {
        BubbleText CreateDamageBubble(Vector3 at, int damage);
        BubbleText CreateHealBubble(Vector3 at, int heal);
    }
}