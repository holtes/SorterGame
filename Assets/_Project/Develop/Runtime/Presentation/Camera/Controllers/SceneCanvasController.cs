using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.CameraCanvas.Controllers
{
    public class SceneCanvasController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private Camera _gameCamera;

        [Inject]
        private void Construct(Camera gameCamera)
        {
            _gameCamera = gameCamera;
        }

        private void Awake()
        {
            _canvas.worldCamera = _gameCamera;
        }
    }
}