using UnityEngine;
using UnityEngine.Events;

namespace GameBoxClicker
{
    public class MergeContent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private UnityEvent<MergeContent, MergeContent> _onMerge;
        private float _radius;

        public Field Field { get; set; }

        public string ID => _id;

        private void Awake()
        {
            _radius = _collider.radius;
        }

        public void CheckForPotentialMerge()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius * 0.5f, Vector3.down, 0.01f);
            int count = hits.Length;
            for (int k = 0; k < count; k++)
            {
                GameObject gameObject = hits[k].collider.gameObject;
                if (gameObject.TryGetComponent(out MergeContent target))
                {
                    if (target.ID == this.ID&& target!= this)
                    {
                        _onMerge?.Invoke(target, this);
                        return;
                    }
                }
            }
        }
    }
}