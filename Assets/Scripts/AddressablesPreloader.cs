using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameBoxClicker
{
    public class AddressablesPreloader
    {
        private static Dictionary<AssetReference, GameObject> s_preloadedObjects = new ();

        public static async Task LoadFromReference(AssetReference reference, Action<GameObject> onCompleted)
        {
            if (string.IsNullOrEmpty(reference.AssetGUID)) return;
            if (s_preloadedObjects.ContainsKey(reference)) onCompleted?.Invoke(s_preloadedObjects[reference]);
            else
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(reference);
                await handle.Task;
                s_preloadedObjects.Add(reference, handle.Result);
                onCompleted?.Invoke(s_preloadedObjects[reference]);
            }
        }
        public static void ReleaseFromReference(AssetReference reference)
        {
            if (s_preloadedObjects.ContainsKey(reference))
            {
                Addressables.Release(s_preloadedObjects[reference]);
                s_preloadedObjects.Remove(reference);
            }
        }
    }
}