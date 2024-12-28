using UnityEngine;

namespace MemoryGame
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EventManager.OnLevelWon += HandleLevelWon;
            EventManager.OnTimeEnded += HandleTimeEnded;
        }

        private void UnsubscribeEvents()
        {
            EventManager.OnLevelWon -= HandleLevelWon;
            EventManager.OnTimeEnded -= HandleTimeEnded;
        }

        private void HandleLevelWon()
        {
            Debug.Log("Level completed. Starting next level.");
            EventManager.InvokeStartNextLevel();
        }

        private void HandleTimeEnded()
        {
            Debug.Log("Time's up. Restarting level.");
            RestartLevel();
        }

        private void RestartLevel()
        {
            EventManager.InvokeOnRestartLevel();
        }
    }
}