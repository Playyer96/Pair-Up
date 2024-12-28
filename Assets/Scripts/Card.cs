using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PairUp
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image iconImage;

        public Sprite hiddenIconSprite;
        public Sprite iconSprite;

        public bool isSelected;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnCardClick);
        }

        public void OnCardClick()
        {
            EventManager.InvokeCardSelected(this); // Notify the event manager
        }

        public void SetIconSprite(Sprite sprite)
        {
            iconSprite = sprite;
        }

        public void Show()
        {
            transform.DORotate(new Vector3(0f, 180f, 0f), 0.2f)
                .SetEase(Ease.OutBack)
                .OnUpdate(() =>
                {
                    if (transform.rotation.eulerAngles.y >= 90f && iconImage.sprite != iconSprite)
                    {
                        iconImage.sprite = iconSprite;
                    }
                });
            isSelected = true;
        }

        public void Hide()
        {
            transform.DORotate(Vector3.zero, 0.2f)
                .SetEase(Ease.OutBack)
                .OnUpdate(() =>
                {
                    if (transform.rotation.eulerAngles.y >= 90f && iconImage.sprite != hiddenIconSprite)
                    {
                        iconImage.sprite = hiddenIconSprite;
                    }
                });
            isSelected = false;
        }
    }
}