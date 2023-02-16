using System.Collections.Generic;
using _Scripts.ScriptableObjects;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Scroller
{
    public class ScrollerVisualPart : MonoBehaviour
    {
        [SerializeField] private PopUp _popUp;
        [SerializeField] private AnimationCurve _speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _cardsOffset = .5f;
        private readonly List<Card> _cards = new();
        private float _currentSpeed;
        private float _translationRemaining;
        private float _targetTranslation;
        private float _cardWidth;
        private int _cardWinnerIdx;
        private float _cardQueueLength;
        private Card _winnerCard;

        private void Update()
        {
            MoveCards();
            if (_targetTranslation > 0)
            {
                var t = (_targetTranslation - _translationRemaining) / _targetTranslation;
                _currentSpeed = _speedCurve.Evaluate(t) * _maxSpeed;
            }
        }

        public void StartScroll(List<CardData> generatedData, int scroll, int winnerCardIdx)
        {
            ClearAll();
            _popUp.HidePanel();
            _cardWidth = _cardPrefab.GetCardWidth + _cardsOffset;
            var centralIdx = generatedData.Count / 2;
            var startPosition = transform.position + Vector3.left * centralIdx * _cardWidth;
            for (var index = 0; index < generatedData.Count; index++)
            {
                var data = generatedData[index];
                var pos = startPosition + Vector3.right * index * _cardWidth;
                var card = Instantiate(_cardPrefab, pos, Quaternion.identity, transform);
                card.InitCard(data);
                _cards.Add(card);
                if (index == winnerCardIdx)
                {
                    _winnerCard = card;
                    card.SetWinnerText();
                }
            }

            _leftBorder.transform.position = startPosition;
            _rightBorder.transform.position = startPosition + Vector3.right * generatedData.Count * _cardWidth;
            _cardQueueLength = _rightBorder.position.x - _leftBorder.position.x;
            _targetTranslation = scroll * _cardWidth;
            _translationRemaining = _targetTranslation;
        }

        private void ClearAll()
        {
            if (_cards.Count == 0) return;
            foreach (var card in _cards)
            {
                Destroy(card.gameObject);
            }

            _cards.Clear();
        }

        private void MoveCards()
        {
            if (_translationRemaining <= 0) return;
            var currentTranslation = Mathf.Min(_currentSpeed * Time.deltaTime, _translationRemaining);
            foreach (var card in _cards)
            {
                card.transform.position += Vector3.left * currentTranslation;
                var distanceToLeftBorder = card.transform.position.x - _leftBorder.position.x;
                if (distanceToLeftBorder < 0)
                {
                    var cardPosition = card.transform.position;
                    cardPosition.x = _rightBorder.position.x + distanceToLeftBorder % _cardQueueLength;
                    card.transform.position = cardPosition;
                }
            }

            _translationRemaining -= currentTranslation;
            if (_translationRemaining <= 0)
            {
                _targetTranslation = 0;
                _translationRemaining = 0;
                _popUp.ShowPanel(_winnerCard);
            }
        }
    }
}