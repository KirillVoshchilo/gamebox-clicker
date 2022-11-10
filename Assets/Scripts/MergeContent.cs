using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace GameBoxClicker
{
    public class MergeContent : MonoBehaviour
    {
        [SerializeField] private AssetReference _thisObjectReference;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private UnityEvent<MergeContent, MergeContent> _onMerge;
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
                        _onMerge?.Invoke(content, this);
                        return;
                    }
                }
            }
        }
    }
}