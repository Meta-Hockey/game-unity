using UnityEngine;
using UnityEngine.UI;

namespace UI.ManageTeam.Buttons
{
    public class LineButton : MonoBehaviour
    {
        [SerializeField] private SwitchColorButtonsView view;

        public Image image;
        public Text text;


        public void ChangeActiveButton()
        {
            view.ChangeActiveButton(this);
        }
    }
}