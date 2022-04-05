using System;
using Runtime;
using UnityEngine;

namespace UI.Sign_In
{
    public class SignInButton : MonoBehaviour
    {
        public async void RequestSignIn()
        {
            await NearPersistentManager.Instance.WalletAccount.RequestSignIn(
                NearPersistentManager.Instance._gameContactId,
                "Nft hockey",
                new Uri("nearclientunity://testnet.near.org/success"),
                new Uri("nearclientunity://testnet.near.org/fail"),
                new Uri("nearclientios://testnet.near.org")
            );
        }
    }
}