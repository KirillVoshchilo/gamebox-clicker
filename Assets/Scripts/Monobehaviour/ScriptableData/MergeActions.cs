using GameBoxClicker.AppEvents;
using UnityEngine;

namespace GameBoxClicker.ScriptableData
{
    [CreateAssetMenu(fileName = "new MergeActions", menuName = "MergeActions")]
    public class MergeActions : ScriptableObject
    {
        [SerializeField] private MergeContent[] _mergeContent;
        [SerializeField] private ScriptableFloatEvent _onLastMerge;
        [SerializeField] private float _onLastMergeEarn;

        public void OrdinaryMerge(MergeContent target, MergeContent current)
        {
            GameObject gameObject = Instantiate(ChooseNextLevelContent(target).gameObject, target.transform.position, target.transform.rotation, target.Field.SpawnTransform);
            MergeContent newContent = gameObject.GetComponent<MergeContent>();
            newContent.Field = target.Field;
            newContent.Field.CurrentContent = newContent;
            current.Field.ClearTheField();
            Destroy(current.gameObject);
            Destroy(target.gameObject);
        }
        public void LastMerge(MergeContent target, MergeContent current)
        {
            current.Field.ClearTheField();
            _onLastMerge.Raise(_onLastMergeEarn);
            Destroy(current.gameObject);
            Destroy(target.gameObject);
        }

        private MergeContent ChooseNextLevelContent(MergeContent content)
        {
            int count = _mergeContent.Length;
            for (int k = 0; k < count; k++)
            {
                if (content.ID == _mergeContent[k].ID) return _mergeContent[k + 1];
            }
            return null;
        }
    }
}