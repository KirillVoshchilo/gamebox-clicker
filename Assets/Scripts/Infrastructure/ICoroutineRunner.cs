using System.Collections;
using UnityEngine;

namespace GameBoxClicker.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}