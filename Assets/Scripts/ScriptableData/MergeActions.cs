using GameBoxClicker.AppEvents;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameBoxClicker.ScriptableData
{
    [CreateAssetMenu(fileName = "new MergeActions", menuName = "MergeActions")]
    public class MergeActions : ScriptableObject
    {
        [SerializeField] private AssetReference[] _mergeContentAssets;
        [SerializeField] private ScriptableIntEvent _onLastMerge;
        [SerializeField] private int _onLastMergeEarn;

        public void OrdinaryMerge(MergeContent target, MergeContent current)
        {
            Task task = AddressablesPreloader.LoadFromReference(ChooseNextLevelContent(target.ObjetReference), (GameObject obj) =>
              {
                  Destroy(current.gameObject);
                  Destroy(target.gameObject);
                  current.Field.ClearTheField();
                  GameObject gameObject = Instantiate(obj, target.transform.position, target.transform.rotation, target.Field.SpawnTransform);
                  MergeContent newContent = gameObject.GetComponent<MergeContent>();
                  newContent.Field = target.Field;
                  newContent.Field.CurrentContent = newContent;
              });

        }
        public void LastMerge(MergeContent target, MergeContent current)
        {
            current.Field.ClearTheField();
            _onLastMerge.Raise(_onLastMergeEarn);
            Destroy(current.gameObject);
            Destroy(target.gameObject);
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