using UnityEngine;

namespace UI.Marketplace
{
    public abstract class ViewInteractor : MonoBehaviour
    {
        public MarketplaceController MarketplaceController;

        private void Awake()
        {
            MarketplaceController = new MarketplaceController();
        }

        public virtual void ChangeView(Transform view) {}
    }
}