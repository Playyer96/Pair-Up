using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PairUp
{
    public class CardsController : MonoBehaviour
    {
        [SerializeField] private Card cardPrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Canvas gameCanvas;

        private List<Sprite> _spritePairs;
        private Card _firstCardSelected;
        private Card _secondCardSelected;
        private int _matchCounts;

        [SerializeField] private int level = 1;
        [SerializeField] private int pairsPerLevel = 1;
        [SerializeField] private int maxPairsPerLevel = 10;

        private void OnEnable()
        {
            EventManager.OnCardSelected += SetSelected;
            EventManager.OnLevelWon += HandleLevelWon;
            EventManager.OnStartNextLevel += HandleStartNextLevel;
            EventManager.OnRestartLevel += HandleRestartLevel;
        }

        private void OnDisable()
        {
            EventManager.OnCardSelected -= SetSelected;
            EventManager.OnLevelWon -= HandleLevelWon;
            EventManager.OnStartNextLevel -= HandleStartNextLevel;
            EventManager.OnRestartLevel -= HandleRestartLevel;
        }

        private void Start()
        {
            _matchCounts = 0;
            PrepareSprites();
            CreateCards();
            AdjustGridLayout();
            EventManager.InvokeLevelStarted(level);
        }

        private void StartNextLevel()
        {
            level++;
            _matchCounts = 0;
            PrepareSprites();
            CreateCards();
            AdjustGridLayout();
            EventManager.InvokeLevelStarted(level);
        }

        private void PrepareSprites()
        {
            _spritePairs = new List<Sprite>();
            int pairsToUse = Mathf.Min(level * pairsPerLevel, Mathf.Min(sprites.Length, 18));

            for (int i = 0; i < pairsToUse; i++)
            {
                _spritePairs.Add(sprites[i]);
                _spritePairs.Add(sprites[i]);
            }

            ShuffleSprites(_spritePairs);
        }

        private void CreateCards()
        {
            // Remove any existing children in the GridLayoutGroup
            foreach (Transform child in gridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }

            // Instantiate the cards, but ensure no more than 36 cards are created
            int maxCards = 36;
            for (int i = 0; i < Mathf.Min(_spritePairs.Count, maxCards); i++)
            {
                Card card = Instantiate(cardPrefab, gridLayoutGroup.transform);
                card.SetIconSprite(_spritePairs[i]);
            }

            // Adjust the grid's cell sizes after cards are created
            AdjustGridLayout();
        }

        private void AdjustGridLayout()
        {
            int maxGridSize = 6;

            // gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            // gridLayoutGroup.constraintCount = maxGridSize;

            RectTransform gridRectTransform = gridLayoutGroup.GetComponent<RectTransform>();
            float gridWidth = gridRectTransform.rect.width;
            float gridHeight = gridRectTransform.rect.height;

            float availableWidth = gridWidth - 2;
            float availableHeight = gridHeight - 2;

            float requiredWidth = availableWidth / maxGridSize;
            float requiredHeight = availableHeight / maxGridSize;

            float newCellSize = Mathf.Min(requiredWidth, requiredHeight);

            gridLayoutGroup.cellSize = new Vector2(newCellSize, newCellSize);
        }

        private void ShuffleSprites(List<Sprite> spriteList)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                int randomIndex = Random.Range(0, i + 1);
                (spriteList[i], spriteList[randomIndex]) = (spriteList[randomIndex], spriteList[i]);
            }
        }

        private void SetSelected(Card card)
        {
            if (!card.isSelected)
            {
                card.Show();

                if (_firstCardSelected == null)
                {
                    _firstCardSelected = card;
                    return;
                }

                if (_secondCardSelected == null)
                {
                    _secondCardSelected = card;
                    StartCoroutine(CheckMatching(_firstCardSelected, _secondCardSelected));
                    _firstCardSelected = null;
                    _secondCardSelected = null;
                }
            }
        }

        private IEnumerator CheckMatching(Card a, Card b)
        {
            yield return new WaitForSeconds(0.3f);

            if (a.iconSprite == b.iconSprite)
            {
                _matchCounts++;
                if (_matchCounts >= _spritePairs.Count / 2)
                {
                    EventManager.InvokeLevelWon();

                    Sequence sequence = DOTween.Sequence();
                    sequence
                        .Append(gridLayoutGroup.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetEase(Ease.OutBack))
                        .Append(gridLayoutGroup.transform.DOScale(Vector3.one, 0.1f));
                }
            }
            else
            {
                a.Hide();
                b.Hide();
            }
        }

        private void HandleLevelWon()
        {
            Debug.Log("You win this level!");
        }

        private void HandleStartNextLevel()
        {
            StartNextLevel();
        }

        private void HandleRestartLevel()
        {
            _matchCounts = 0;
            PrepareSprites();
            CreateCards();
            AdjustGridLayout();
            EventManager.InvokeLevelStarted(level);
        }
    }
}