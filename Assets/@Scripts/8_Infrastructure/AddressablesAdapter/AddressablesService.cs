using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AREnterprise.Domain.Interfaces;

namespace AREnterprise.Infrastructure.AddressablesAdapter
{
    public class AddressablesService : IAddressablesService
    {
        public async Task<GameObject> LoadPrefabAsync(string key)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(key);
            await handle.Task;
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                return handle.Result;

            Debug.LogError($"[AddressablesService] Failed to load asset with key: {key}");
            return null;
        }
    }
} 