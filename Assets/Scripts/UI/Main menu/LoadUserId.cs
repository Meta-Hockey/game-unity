using Near;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_menu
{
    public class LoadUserId : MonoBehaviour
    {
        [SerializeField] private Text accountId;

        private void Awake()
        {
            accountId.text = "Welcome: " + NearPersistentManager.Instance.GetAccountId() + " !";
        }
    }
}