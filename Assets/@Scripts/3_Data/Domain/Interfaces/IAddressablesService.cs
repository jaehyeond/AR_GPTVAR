using System.Threading.Tasks;
using UnityEngine;

namespace AREnterprise.Domain.Interfaces
{
    public interface IAddressablesService
    {
        // Addressables에서 GameObject 프리팹을 비동기로 로드
        Task<GameObject> LoadPrefabAsync(string key);
    }
} 