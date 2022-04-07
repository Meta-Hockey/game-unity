using Near;
using UnityEngine;

namespace UI.Sign_In
{
    public class SignInButton : MonoBehaviour
    {
        public async void RequestSignIn()
        {
            await NearPersistentManager.Instance.SignIn();
        }
    }
}