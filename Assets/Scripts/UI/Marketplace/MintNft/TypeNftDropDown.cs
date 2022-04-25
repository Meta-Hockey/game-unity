using UnityEngine;

namespace UI.Marketplace.MintNft
{
    public class TypeNftDropDown : MonoBehaviour
    {
        [SerializeField] private MintNftView view;

        public void OnChangeType(int id)
        {
            view.SwitchType(id);
        }
    }
}