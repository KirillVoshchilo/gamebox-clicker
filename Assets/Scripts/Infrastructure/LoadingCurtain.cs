using System.Collections;
using UnityEngine;

namespace GameBoxClicker.Infrastructure
{
    public class LoadingCurtain:MonoBehaviour
    {
        public CanvasGroup Curtain;
        private WaitForSeconds _waiter;

        private WaitForSeconds Waiter
        {
            get
            { 
               if (_waiter==null) _waiter = new WaitForSeconds(0.03f);
                return _waiter;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }
        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return Waiter;
            }
            gameObject.SetActive(true);
        }
    }
    
}