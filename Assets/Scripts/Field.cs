using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Transform _spawnTransfrom;


    public bool IsEmpty { get; set; }
    public Transform SpawnTransform => _spawnTransfrom;
    public Content CurrentContent { get; set; }

    private void Awake()
    {
        IsEmpty = true;
    }
}
