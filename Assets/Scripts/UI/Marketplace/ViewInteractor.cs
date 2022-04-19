using UnityEngine;

namespace UI.Marketplace
{
    public abstract class ViewInteractor : MonoBehaviour
    {
        public virtual void ChangeView(Transform view) {}
    }
}