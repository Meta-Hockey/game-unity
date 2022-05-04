using Near;
using UnityEngine;

namespace UI.Main_menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private SignInView signInView;

        private void Start()
        {
            if (NearPersistentManager.Instance.WalletAccount.IsSignedIn())
            {
                mainMenuView.gameObject.SetActive(true);
                signInView.gameObject.SetActive(false);
            }
            else
            {
                mainMenuView.gameObject.SetActive(false);
                signInView.gameObject.SetActive(true);
            }
        }
    }
}