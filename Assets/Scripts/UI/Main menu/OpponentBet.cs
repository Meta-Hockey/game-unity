using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_menu
{
    public class OpponentBet : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text price;
        [SerializeField] private Text opponentId;
        [SerializeField] private Text defensive;
        [SerializeField] private Text attack;
        [SerializeField] private Text goalie;
        [SerializeField] private Button selectButton;
        [SerializeField] private Color activeColor;

        public Image Image => image;
        public Text Price => price;
        public Text OpponentId => opponentId;
        public Text Defensive => defensive;
        public Text Attack => attack;
        public Text Goalie => goalie;
        public Button SelectButton => selectButton;
        public Color ActiveColor => activeColor;
    }
}