using UnityEngine;

namespace UI.MainMenu
{
    public class SignOutButton : MonoBehaviour
    {
        public void SignOut()
        {
            NearPersistentManager.Instance.WalletAccount.SignOut();
        }
    }
}