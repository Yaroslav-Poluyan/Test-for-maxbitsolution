using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.GlobalSettings;
using _Scripts.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Scroller
{
    public class ScrollerGenerator : MonoBehaviour
    {
        [SerializeField] private ScrollerVisualPart _scrollerVisualPart;
        [SerializeField] private int _cardsCount = 20;
        [SerializeField] private List<CardData> _cardsData = new();
        [SerializeField] private int _targetScroll;
        [SerializeField] private int _loopsCount = 2;


        public void ButtonPressed()
        {
            var generatedData = GenerateCardsPreset();
            var winnerCard = ReturnWinnerCardIdx(generatedData);
            _targetScroll = (_cardsCount - _cardsCount / 2) + winnerCard + _loopsCount * _cardsCount;
            _scrollerVisualPart.StartScroll(generatedData, _targetScroll, winnerCard);
        }
        //

        private int ReturnWinnerCardIdx(List<CardData> generatedData)
        {
            var value = Random.Range(0f, 1f);
            var sortedCardsData = generatedData.OrderBy(x => x._dropChance).ToList();
            for (var i = 0; i < sortedCardsData.Count; i++)
            {
                if (value <= sortedCardsData[i]._dropChance)
                {
                    var cardsWithSameChance =
                        sortedCardsData.Where(x => x._dropChance == sortedCardsData[i]._dropChance).ToList();
                    var winnerCard = cardsWithSameChance.Count > 1
                        ? cardsWithSameChance[Random.Range(0, cardsWithSameChance.Count)]
                        : sortedCardsData[i];
                    var idx = generatedData.IndexOf(winnerCard);
                    return idx;
                }
            }

            return sortedCardsData.Count - 1;
        }

        private List<CardData> GenerateCardsPreset()
        {
            var generatedCardsPreset = new List<CardData>();
            for (var i = 0; i < _cardsCount; i++)
            {
                generatedCardsPreset.Add(_cardsData[Random.Range(0, _cardsData.Count)]);
            }

            return generatedCardsPreset;
        }

        public void RandomizeCardsData()
        {
            foreach (var cardData in _cardsData)
            {
                cardData._dropChance = Random.Range(0f, 1f);
                var enumLength = Enum.GetNames(typeof(CardsSettings.CardRank)).Length;
                cardData._cardRank = (CardsSettings.CardRank) Random.Range(0, enumLength);
            }
        }
    }
}