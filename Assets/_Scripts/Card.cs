using _Scripts.GlobalSettings;
using _Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _cardName;
        [SerializeField] private TextMeshPro _cardRank;
        [SerializeField] private SpriteRenderer _cardImage;
        [SerializeField] private SpriteRenderer _cardBackground;
        private CardData _cardData;
        public float GetCardWidth => _cardBackground.localBounds.size.x;


        //размер взять с спрайт рендерера bounds
        public void InitCard(CardData cardData)
        {
            var rankSettings = CardsSettings.Instance.CardRanks.Find(x => x._cardRank == cardData._cardRank);
            _cardName.text = cardData._cardName;
            _cardRank.text = cardData._cardRank.ToString();
            _cardRank.color = rankSettings._cardColor;
            _cardImage.sprite = cardData._cardSprite;
            _cardBackground.sprite = rankSettings._cardBackground;
            _cardData = cardData;
        }

        public void SetWinnerText()
        {
            _cardBackground.material.color = Color.red;
        }

        public CardData GetData()
        {
            return _cardData;
        }
    }
}