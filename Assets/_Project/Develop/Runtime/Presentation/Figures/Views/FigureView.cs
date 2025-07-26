using _Project.Develop.Runtime.Core.Services;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.Figures.Views
{
    public class FigureView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private ParticleSystem _lineImpactVFX;
        [SerializeField] private GameObject _explosionVFXPrefab;

        private Transform _sceneOrigin;
        private TimeService _timeService;

        [Inject]
        private void Construct(TimeService timeService, Transform sceneOrigin)
        {
            _timeService = timeService;
            _sceneOrigin = sceneOrigin;
        }

        public void Init(Sprite figureSprite, Color figureColor)
        {
            _renderer.sprite = figureSprite;
            _renderer.color = figureColor;
        }

        public void MoveToTarget(Vector3 target, float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * _timeService.DeltaTime);
        }

        public void PlayImpactVFX()
        {
            _lineImpactVFX.Play();
        }

        public void StopImpactVFX()
        {
            _lineImpactVFX.Stop();
        }

        public void PlayExplosionVFX()
        {
            var explosionVFXObj = Instantiate(_explosionVFXPrefab, transform.position, Quaternion.identity, _sceneOrigin);
            var explosionVFX = explosionVFXObj.GetComponent<ParticleSystem>();
            explosionVFX.Play();
        }
    }
}