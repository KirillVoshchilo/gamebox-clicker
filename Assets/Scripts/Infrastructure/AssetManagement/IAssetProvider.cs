using GameBoxClicker.Infrastructure.Services;
using UnityEngine;

namespace GameBoxClicker.Infrastructure.AssetManagement
{
    public interface IAssetProvider:IService
    {
        GameObject Instantiate(string path,Vector3 at);
        GameObject Instantiate(string path);
    }
}