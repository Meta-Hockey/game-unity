using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_menu.UIPopups
{
    public class UIPopupOpponent : UIPopup
    {

        [SerializeField] private Text opponentId;
        [SerializeField] private Text price;
        [SerializeField] private Text defensive;
        [SerializeField] private Text attack;
        [SerializeField] private Text goalie;
        
        public void LoadData(OpponentBet bet)
        {
            opponentId.text = bet.OpponentId.text;
            price.text = "Choose a bet: " + bet.Price.text + "N";
            defensive.text = bet.Defensive.text;
            attack.text = bet.Attack.text;
            goalie.text = bet.Goalie.text;
        }

    }
}