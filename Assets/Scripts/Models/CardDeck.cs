using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class CardDeck : MonoBehaviour
    {
        public List<GameObject> Cards;
        private List<GameObject> Deck;

        public void Start()
        {
            Deck = Shuffle(Cards);
        }

        public void Update()
        {
            if (Deck.Count <= 0)
            {
                Deck = Shuffle(Cards);
            }
        }

        public GameObject DrawCard()
        {
            if (Deck.Count > 0)
            {
                var firstCard = Deck[0];
                Deck.RemoveAt(0);
                return firstCard;
            }
            Deck = Shuffle(Cards);
            return DrawCard();
        }

        private static List<GameObject> Shuffle(List<GameObject> cards)
        {
            var shuffledCards = new List<GameObject>(cards);
            for (int i = 0; i < cards.Count; i++)
            {
                var random = Random.Range(0, cards.Count - 1);
                (shuffledCards[random], shuffledCards[i]) = (shuffledCards[i], shuffledCards[random]);
            }
            return shuffledCards;
        }

        public static CardDeck GetDeck()
        {
            var allObjects = FindObjectsOfType<GameObject>();
            return allObjects
                .Where(x => x.CompareTag("CardDeck")).First().GetComponent<CardDeck>();
        }

        public int GetDeckCount()
        {
            if (Deck is null)
            {
                return 0;
            }
            return Deck.Count;
        }
    }
}