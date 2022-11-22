using GameBoxClicker.Infrastructure.Services;
using UnityEngine;

namespace GameBoxClicker.Services.Input
{
    public interface IInputService : IService
    {
        public Vector2 PointerPosition { get; set; }
        public bool HoldClick{ get; set; }
}
}