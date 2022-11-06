using UnityEngine;

namespace GameBoxClicker
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Transform _spawnTransfrom;


        public bool IsEmpty { get; set; }
        public Transform SpawnTransform => _spawnTransfrom;
        public MergeContent CurrentContent { get; set; }

        private void Awake()
        {
            IsEmpty = true;
        }
    }
}