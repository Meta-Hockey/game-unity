using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_menu
{
    public class LoadInfo : MonoBehaviour
    {
        [SerializeField] private Text accountId;

        private void Awake()
        {
            accountId.text = "Welcome: " + NearPersistentManager.Instance.WalletAccount.GetAccountId() + " !";
        }
    }
}