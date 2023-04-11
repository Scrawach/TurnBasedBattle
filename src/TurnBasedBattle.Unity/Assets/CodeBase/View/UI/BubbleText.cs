using TMPro;
using UnityEngine;

namespace CodeBase.View.UI
{
    public class BubbleText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _content;
        
        public void Construct(string content, Color color)
        {
            _content.text = content;
            _content.color = color;
        }
    }
}