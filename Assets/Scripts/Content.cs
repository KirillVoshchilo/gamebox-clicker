using UnityEngine;

namespace GameBoxClicker
{
    public class Content : MonoBehaviour
    {
        [SerializeField] private Content _upgradeTo;
        [SerializeField] private Content _upgradeFrom;
    }
}