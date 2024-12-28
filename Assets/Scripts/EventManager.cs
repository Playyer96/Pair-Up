using System;

namespace MemoryGame
{
    public static class EventManager
    {
        public static event Action<Card> OnCardSelected;
        public static event Action OnLevelWon;
        public static event Action<int> OnLevelStarted;
        public static event Action OnStartNextLevel;
        public static event Action OnTimeEnded;
        public static event Action OnRestartLevel;
        public static event Action OnGameOver;

        public static void InvokeCardSelected(Card card) => OnCardSelected?.Invoke(card);

        public static void InvokeLevelWon() => OnLevelWon?.Invoke();

        public static void InvokeLevelStarted(int level) => OnLevelStarted?.Invoke(level);

        public static void InvokeStartNextLevel() => OnStartNextLevel?.Invoke();

        public static void InvokeTimeEnded() => OnTimeEnded?.Invoke();

        public static void InvokeOnRestartLevel() => OnRestartLevel?.Invoke();

        public static void InvokeOnGameOver() => OnGameOver?.Invoke();
    }
}