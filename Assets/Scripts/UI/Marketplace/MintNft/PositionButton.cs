using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.MintNft
{
    public class PositionButton : MonoBehaviour
    {
        [SerializeField] private MintNftView view;
        
        [SerializeField] public Image image;
        [SerializeField] public Text text;

        public void ChangePosition(string position)
        {
            view.ChoosePlayerPosition(position, this);
        }
    }
}