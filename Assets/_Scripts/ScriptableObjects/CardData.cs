using _Scripts.GlobalSettings;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Data/CardData", order = 0)]
    public class CardData : ScriptableObject
    {
        public string _cardName;
        public CardsSettings.CardRank _cardRank;
        public Sprite _cardSprite;
        [Range(0, 1)] public float _dropChance;
    }
}