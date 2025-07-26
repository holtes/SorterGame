using UnityEngine;
using TMPro;

namespace _Project.Develop.Runtime.Presentation.InfoBars.Views
{
    public class HPBarView : MonoBehaviour
    {
        [SerializeField] private Transform _hpFill;
        [SerializeField] private TMP_Text _hpValueTxt;
        [SerializeField] private ParticleSystem _lowHPVFX;

        private int _startHP;

        public void InitHP(int startHP)
        {
            _startHP = startHP;
            _hpValueTxt.text = startHP.ToString();
        }

        public void SetHealth(int value)
        {
            _hpValueTxt.text = value.ToString();

            var fillScale = _hpFill.localScale;
            fillScale.y = (float)value / _startHP;
            _hpFill.localScale = fillScale;
        }

        public void PlayLowHPVFX()
        {
            _lowHPVFX.Play();
        }

        public void StopLowHPVFX()
        {
            _lowHPVFX.Stop();
        }
    }
}