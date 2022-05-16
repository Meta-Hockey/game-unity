using System.Collections.Generic;
using UnityEngine;

namespace UI.ManageTeam.Buttons
{
    public class SwitchColorButtonsView : MonoBehaviour
    {
        [SerializeField] private List<LineButton> buttons;
        
        [SerializeField] private Color activeColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color defaultColorText;
        [SerializeField] private Color activeColorText;

        public void ChangeActiveButton(LineButton setButton)
        {
            foreach (var button in buttons)
            {
                button.image.color = defaultColor;
                button.text.color = defaultColorText;
            }

            setButton.image.color = activeColor;
            setButton.text.color = activeColorText;
        }
    }
}