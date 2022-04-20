using UnityEngine;

namespace UI.Marketplace
{
    public abstract class ViewInteractor : MonoBehaviour
    {
        public virtual void ChangeView(Transform view) {}
        public virtual void LoadNftCards(IViewNftCards viewNftCards) {}
    }
}