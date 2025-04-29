using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using AREnterprise.Application.Interactors;
using AREnterprise.Domain.UseCases;

namespace AREnterprise.SceneControllers
{
    [RequireComponent(typeof(ARPlaneManager), typeof(ARAnchorManager))]
    public class ARSceneController : MonoBehaviour
    {
        [Header("Addressable Prefab Key")]
        [SerializeField] private string terrainPrefabKey = "TerrainPrefab";

        private ARPlaneManager _planeManager;
        private ARAnchorManager _anchorManager;

        // Interactor/UseCase 주입 (DI 컨테이너에서 할당)
        public StartPlaneDetectionInteractor PlaneDetectionInteractor { get; set; }
        public PlacePrefabUseCase PrefabUseCase { get; set; }

        private bool _isDetecting;

        void Awake()
        {
            _planeManager = GetComponent<ARPlaneManager>();
            _anchorManager = GetComponent<ARAnchorManager>();
        }

        void OnEnable()
        {
            // 평면 검출 시작
            PlaneDetectionInteractor.PlanesDetected += OnPlanesChanged;
            PlaneDetectionInteractor.Execute();
            _isDetecting = true;
        }

        void OnDisable()
        {
            PlaneDetectionInteractor.PlanesDetected -= OnPlanesChanged;
            PlaneDetectionInteractor.Stop();
            _isDetecting = false;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            if (!_isDetecting || args.added.Count == 0)
                return;

            // 첫 번째 검출된 평면에만 배치
            var plane = args.added[0];
            _isDetecting = false;
            InstantiateOnPlane(plane);
        }

        private async void InstantiateOnPlane(ARPlane plane)
        {
            // Addressable 로드
            var prefab = await PrefabUseCase.LoadPrefabAsync(terrainPrefabKey);
            if (prefab == null)
            {
                Debug.LogError("Failed to load terrain prefab.");
                return;
            }

            // AR 세션에 앵커 생성
            var anchor = _anchorManager.AddAnchor(plane.centerPose);
            if (anchor == null)
            {
                Debug.LogError("Failed to create AR anchor.");
                return;
            }

            // 실제 인스턴스 생성
            Instantiate(prefab, anchor.transform.position, anchor.transform.rotation);
        }
    }
} 