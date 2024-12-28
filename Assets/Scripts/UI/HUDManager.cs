using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace PairUp.UI
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI winLoseText;
        [SerializeField] private Slider timerSlider;

        private Coroutine _timerCoroutine;

        private void OnEnable()
        {
            EventManager.OnLevelStarted += HandleLevelStarted;
            EventManager.OnLevelWon += HandleLevelWon;
            EventManager.OnTimeEnded += HandleTimeEnded;
        }

        private void OnDisable()
        {
            EventManager.OnLevelStarted -= HandleLevelStarted;
            EventManager.OnLevelWon -= HandleLevelWon;
            EventManager.OnTimeEnded -= HandleTimeEnded;
        }

        private void Start()
        {
            timerSlider.maxValue = 1f;
            winLoseText.gameObject.SetActive(false);
        }

        private void HandleLevelStarted(int level)
        {
            ShowLevelText($"Level {level}");
            StartTimer(5f); // Example: Set 30 seconds for the timer
        }

        private void HandleLevelWon()
        {
            StopTimer();
            ShowWinLoseText("Congratulations!", true);
        }

        private void HandleTimeEnded()
        {
            StopTimer();
            ShowWinLoseText("Time's Up!", false);
            EventManager.InvokeOnGameOver();
        }

        public void ShowLevelText(string text)
        {
            levelText.text = text;
        }

        public void ShowWinLoseText(string text, bool isWin = true)
        {
            winLoseText.text = text;
            winLoseText.gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(winLoseText.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack))
                .Append(winLoseText.transform.DOScale(1f, 0.2f).SetEase(Ease.OutSine))
                .AppendInterval(0.2f)
                .Append(winLoseText.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InOutSine))
                .Append(winLoseText.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack))
                .OnComplete(() =>
                {
                    winLoseText.gameObject.SetActive(false);
                    if (isWin)
                    {
                        EventManager.InvokeStartNextLevel();
                    }
                    else
                    {
                        EventManager.InvokeTimeEnded();
                    }
                });
        }

        private void StartTimer(float duration)
        {
            timerSlider.value = 1f;

            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _timerCoroutine = StartCoroutine(UpdateTimer(duration));
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
        }

        private IEnumerator UpdateTimer(float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                UpdateTimerSlider(1 - (elapsedTime / duration));
                yield return null;
            }

            // Timer ends here
            UpdateTimerSlider(0);
            EventManager.InvokeTimeEnded();
        }

        private void UpdateTimerSlider(float value)
        {
            if (timerSlider != null)
            {
                timerSlider.value = value;
            }
        }
    }
}