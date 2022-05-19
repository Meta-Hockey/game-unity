using UnityEngine;

namespace UI.Main_menu.UIPopups
{
    public class UIPopup : MonoBehaviour
    {
        [SerializeField] private MainMenuView mainMenuView;

        public void Show()
        {
            mainMenuView.ShowPopup(transform);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}