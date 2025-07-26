using UnityEngine;

namespace _Project.Develop.Runtime.Presentation.Sorter.Views
{
    public class SorterSlotView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _slotRenderer;
        [SerializeField] private SpriteRenderer _slotFrameRenderer;
        [SerializeField] private ParticleSystem _correctSortVFX;

        public void SetSprite(Sprite sprite, Sprite frameSprite)
        {
            _slotRenderer.sprite = sprite;
            _slotFrameRenderer.sprite = frameSprite;
        }

        public void SetColor(Color color)
        {
            _slotRenderer.color = color;
        }

        public void PlayCorrectVFX()
        {
            _correctSortVFX.Play();
        }
    }
}