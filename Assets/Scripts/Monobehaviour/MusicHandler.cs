using GameBoxClicker.AppEvents;
using System;
using System.Collections;
using UnityEngine;

namespace GameBoxClicker
{
    public class MusicHandler : MonoBehaviour, IStartNewGame, IPauseHandler, IEndGame
    {
        [SerializeField] private AudioClip[] _mainMenuTheme;
        [SerializeField] private AudioClip[] _gameThemes;
        [SerializeField] private ScriptableEvent _onContinueGame;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onStartNewGame;
        [SerializeField] private ScriptableEvent _onEndGame;
        [SerializeField] private AudioSource _audioSource;

        private event Action OnEndOfClip;
        private WaitForSeconds _checkerWaiter;
        private AudioClip[] _currentPlayList;
        private Coroutine _musicCheckerRoutine;

        private void Awake()
        {
            _currentPlayList = _mainMenuTheme;
            _checkerWaiter = new WaitForSeconds(1f);
            PlayClip(RandomClip(_currentPlayList));
            _onContinueGame.RegisterListener(ContinueGame);
            _onPauseGame.RegisterListener(PauseGame);
            _onStartNewGame.RegisterListener(StartNewGame);
            _onEndGame.RegisterListener(EndGame);
            _musicCheckerRoutine = StartCoroutine(ClipCompletionChecker());
            OnEndOfClip += ChangeClip;
        }
        private void OnDestroy()
        {
            _onContinueGame.UnregisterListener(ContinueGame);
            _onPauseGame.UnregisterListener(PauseGame);
            _onStartNewGame.UnregisterListener(StartNewGame);
            _onEndGame.UnregisterListener(EndGame);
            OnEndOfClip -= ChangeClip;
        }

        public void ContinueGame()
        {
            _audioSource.UnPause();
            _musicCheckerRoutine = StartCoroutine(ClipCompletionChecker());
        }
        public void EndGame()
        {
            ChangePlayList(_mainMenuTheme);
            PlayClip(RandomClip(_currentPlayList));
            _musicCheckerRoutine = StartCoroutine(ClipCompletionChecker());
        }
        public void PauseGame()
        {
            _audioSource.Pause();
            StopCoroutine(_musicCheckerRoutine);
        }
        public void StartNewGame()
        {
            ChangePlayList(_gameThemes);
            PlayClip(RandomClip(_currentPlayList));
        }

        private void ChangePlayList(AudioClip[] playeList)
        {
            _audioSource.Stop();
            _currentPlayList = playeList;
        }
        private void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        private AudioClip RandomClip(AudioClip[] clips)
        {
            int value = UnityEngine.Random.Range(0, clips.Length - 1);
            return clips[value];
        }
        private IEnumerator ClipCompletionChecker()
        {
            yield return _checkerWaiter;
            if (_audioSource.time == 0)
            {
                OnEndOfClip.Invoke();
            }
            _musicCheckerRoutine = StartCoroutine(ClipCompletionChecker());
        }
        private void ChangeClip()
        {
            PlayClip(RandomClip(_currentPlayList));
        }
    }
}