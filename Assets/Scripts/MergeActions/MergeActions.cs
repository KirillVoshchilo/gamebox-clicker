using GameBoxClicker;
using GameBoxClicker.AppEvents;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameBoxClicker.MergeActions
{
    [CreateAssetMenu(fileName = "new MergeActions", menuName = "MergeActions")]
    public class MergeActions : ScriptableObject
    {
        [SerializeField] private AssetReference[] _mergeContentAssets;
        [SerializeField] private ScriptableIntEvent _onLastMerge;
        [SerializeField] private int _onLastMergeEarn;

        public void OrdinaryMerge(MergeContent target, MergeContent current)
        {
            AsyncOperationHandle<GameObject> handle = Addressables
                .InstantiateAsync(ChooseNextLevelContent(target.ObjetReference), target.transform.position, target.transform.rotation, target.Field.SpawnTransform);
            handle.Completed += (AsyncOperationHandle<GameObject> handle) =>
            {
                MergeContent newContent = handle.Result.GetComponent<MergeContent>();
                newContent.Field = target.Field;
                newContent.Field.CurrentContent = newContent;
            };
            current.Field.ClearTheField();
            Addressables.ReleaseInstance(target.gameObject);
            Addressables.ReleaseInstance(current.gameObject);
        }
        public void LastMerge(MergeContent target, MergeContent current)
        {
            current.Field.ClearTheField();
            Addressables.ReleaseInstance(target.gameObject);
            Addressables.ReleaseInstance(current.gameObject);
            _onLastMerge.Raise(_onLastMergeEarn);
        }

        private AssetReference ChooseNextLevelContent(string currentReference)
        {
            int count = _mergeContentAssets.Length;
            for (int k = 0; k < count; k++)
            {
                if (currentReference == _mergeContentAssets[k].AssetGUID) return _mergeContentAssets[k + 1];
            }
            return null;
        }
    }
}