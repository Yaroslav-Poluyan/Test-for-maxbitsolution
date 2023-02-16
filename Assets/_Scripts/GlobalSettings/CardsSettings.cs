using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.GlobalSettings
{
    public class CardsSettings : MonoBehaviour
    {
        public static CardsSettings Instance { get; private set; }

        public List<RankSettings> CardRanks => _cardRanks;

        [SerializeField] private List<RankSettings> _cardRanks = new()
        {
            new RankSettings()
            {
                _cardRank = CardRank.Common,
                _cardColor = Color.white,
            },
            new RankSettings()
            {
                _cardRank = CardRank.Uncommon,
                _cardColor = Color.green,
            },
            new RankSettings()
            {
                _cardRank = CardRank.Rare,
                _cardColor = Color.blue,
            },
            new RankSettings()
            {
                _cardRank = CardRank.Epic,
                _cardColor = Color.magenta,
            },
            new RankSettings()
            {
                _cardRank = CardRank.Legendary,
                _cardColor = Color.yellow,
            },
        };

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < CardRanks.Count; i++)
            {
                for (int j = 0; j < CardRanks.Count; j++)
                {
                    if (i == j) continue;
                    if (CardRanks[i]._cardRank == CardRanks[j]._cardRank)
                    {
                        Debug.LogError($"There are duplicates in the list of card ranks: {CardRanks[i]._cardRank}");
                    }
                }
            }
        }
#endif
        [Serializable]
        public struct RankSettings
        {
            public CardRank _cardRank;
            public Color _cardColor;
            public Sprite _cardBackground;
        }

        public enum CardRank
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary,
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}