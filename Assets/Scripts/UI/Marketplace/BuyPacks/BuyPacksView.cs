using UnityEngine;

namespace UI.Marketplace.BuyPacks
{
    public class BuyPacksView : MonoBehaviour
    {
        [SerializeField] private ViewInteractor viewInteractor;

        public void LoadPacks()
        {
            viewInteractor.ChangeView(transform);
        }
    }
}