using System;
using UnityEngine.XR.ARFoundation;
using AREnterprise.Domain.Interfaces;

namespace AREnterprise.Infrastructure.ARFoundationAdapter
{
    public class PlaneDetectionService : IPlaneDetectionService
    {
        private ARPlaneManager _planeManager;
        public event Action<ARPlanesChangedEventArgs> PlanesChanged;

        public PlaneDetectionService(ARPlaneManager planeManager)
        {
            _planeManager = planeManager;
            _planeManager.planesChanged += OnPlanesChanged;
        }

        public void StartDetection()
        {
            _planeManager.enabled = true;
        }

        public void StopDetection()
        {
            _planeManager.enabled = false;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            PlanesChanged?.Invoke(args);
        }
    }
} 