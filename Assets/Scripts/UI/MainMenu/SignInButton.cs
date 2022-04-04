using System;
using UnityEngine;

namespace UI.MainMenu
{
    public class SignInButton : MonoBehaviour
    {
        public async void RequestSignIn()
        {
            await NearPersistentManager.Instance.WalletAccount.RequestSignIn(
                "uriyyuriy.testnet",
                "Nft hockey",
                new Uri("nearclientunity://testnet.near.org/success"),
                new Uri("nearclientunity://testnet.near.org/fail"),
                new Uri("nearclientios://testnet.near.org")
            );
        }
    }
}