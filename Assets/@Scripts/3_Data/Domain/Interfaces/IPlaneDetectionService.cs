using System;
using UnityEngine.XR.ARFoundation;

namespace AREnterprise.Domain.Interfaces
{
    public interface IPlaneDetectionService
    {
        // 평면 검출 시작
        void StartDetection();
        // 평면 검출 중지
        void StopDetection();
        // 평면 변경 이벤트 (추가된 평면 정보 등)
        event Action<ARPlanesChangedEventArgs> PlanesChanged;
    }
} 