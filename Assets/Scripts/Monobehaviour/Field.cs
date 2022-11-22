using UnityEngine;

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
            CurrentContent = null;
        }
    }
}