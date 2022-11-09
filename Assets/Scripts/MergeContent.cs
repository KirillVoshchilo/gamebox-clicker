using GameBoxClicker.AppEvents;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameBoxClicker
{
    public class MergeContent : MonoBehaviour
    {
        [SerializeField] private AssetReference _upgradeTo;
        [SerializeField] private AssetReference _thisObjectReference;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private ScriptableMergeEvent _onMergAction;
        private float _radius;

        public Field Field { get; set; }
        public string ObjetReference => _thisObjectReference.AssetGUID;

        private void Awake()
        {
            _radius = _collider.radius;
        }

        public void CheckForPotentialMerge()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, Vector3.down, 0.01f);
            int count = hits.Length;
            for (int k = 0; k < count; k++)
            {
                GameObject gameObject = hits[k].collider.gameObject;
                if (gameObject.TryGetComponent(out MergeContent content))
                {
                    if (content != this && content.ObjetReference == ObjetReference)
                    {
                        MergWith(content);
                        return;
                    }
                }
            }
        }

        private void MergWith(MergeContent content)
        {
            AsyncOperationHandle<GameObject> handle = Addressables
                .InstantiateAsync(_upgradeTo, content.transform.position, content.transform.rotation, content.Field.SpawnTransform);
            handle.Completed += (AsyncOperationHandle<GameObject> handle) =>
              {
                  Addressables.ReleaseInstance(content.gameObject);
                  MergeContent newContent = handle.Result.GetComponent<MergeContent>();
                  newContent.Field = content.Field;
                  content.Field.CurrentContent = newContent;
                  _onMergAction.Raise(content, this);
              };
            this.Field.CurrentContent = null;
            Addressables.ReleaseInstance(this.gameObject);
        }
    }
}