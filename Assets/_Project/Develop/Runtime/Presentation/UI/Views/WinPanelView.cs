using UnityEngine;
using TMPro;

namespace _Project.Develop.Runtime.Presentation.UI.Views
{
    public class WinPanelView : EndGameView
    {
        [SerializeField] private TMP_Text _scoreTxt;


        public void SetScore(int value)
        {
            _scoreTxt.text = value.ToString();
        }
    }
}