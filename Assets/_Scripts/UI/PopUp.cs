using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cardName;
        [SerializeField] private Image _image;

        public void ShowPanel(Card card)
        {
            gameObject.SetActive(true);
            var cardData = card.GetData();
            _cardName.text = cardData._cardName;
            _image.sprite = cardData._cardSprite;
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
        }
    }
}