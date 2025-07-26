using UnityEngine;

namespace _Project.Develop.Runtime.Presentation.SpawnZone.Views
{
    public class SpawnZoneView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _electricityVFX;

        public void PlayVFX()
        {
            _electricityVFX.Play();
        }

        public void StopVFX()
        {
            _electricityVFX.Stop();
        }
    }
}