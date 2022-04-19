using Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Main_menu
{
    public class TradeCardsButton : MonoBehaviour
    {
        public void TradeCards()
        {
            Game.LoadMarketplace();
        }
    }
}