using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class UIPopupResult : MonoBehaviour
    {
        [SerializeField] private Text resultText;
        [SerializeField] private Text ownScoreText;
        [SerializeField] private Text opponentScoreText;

        public void ShowResult(uint ownScore, uint opponentScore)
        {
            if (ownScore >= opponentScore)
            {
                resultText.text = "You win with a score of:";
            }
            else
            {
                resultText.text = "You lost with a score of:";
            }

            ownScoreText.text = ownScore < 10 ? "0" + ownScore : ownScore.ToString();
            opponentScoreText.text = opponentScore < 10 ? "0" + opponentScore : opponentScore.ToString();
            
            gameObject.SetActive(true);
        }
    }
}