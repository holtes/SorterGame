using UnityEngine;
using TMPro;

namespace _Project.Develop.Runtime.Presentation.InfoBars.Views
{
    public class ScoreBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreValueTxt;
        [SerializeField] private ParticleSystem _gainScoreVFX;

        public void InitScore(int startScore)
        {
            _scoreValueTxt.text = startScore.ToString();
        }

        public void SetScore(int value)
        {
            _scoreValueTxt.text = value.ToString();
            _gainScoreVFX.Play();
        }

        public void PlayGainScoreVFX()
        {
            _gainScoreVFX.Play();
        }
    }
}