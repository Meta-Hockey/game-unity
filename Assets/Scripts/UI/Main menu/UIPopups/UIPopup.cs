using UnityEngine;

namespace UI.Main_menu.UIPopups
{
    public abstract class UIPopup : MonoBehaviour
    {
        [SerializeField] protected MainMenuView mainMenuView;

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