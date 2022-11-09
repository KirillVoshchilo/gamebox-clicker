using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameBoxClicker
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Transform _spawnTransfrom;

        public bool IsEmpty { get; set; }
        public Transform SpawnTransform => _spawnTransfrom;
        public MergeContent CurrentContent { get; set; }

        public void ClearTheField()
        {
            IsEmpty = true;
            if (CurrentContent != null) Addressables.ReleaseInstance(CurrentContent.gameObject);
            CurrentContent = null;
        }

    }
}