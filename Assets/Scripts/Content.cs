using UnityEngine;

public class Content : MonoBehaviour
{
    [SerializeField] private Content _upgradeTo;
    [SerializeField] private Content _upgradeFrom;
    [SerializeField] private float _earnPerSecond;
    [SerializeField] private float _earnPerClick;
}

