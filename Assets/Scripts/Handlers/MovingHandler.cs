using Assets.Scripts.Models;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
    public class MovingHandler : MonoBehaviour
    {
        private int _topCardOrder = 0;
        private const int maxLayer = int.MaxValue - 1000;
        private GameObject movableObject = null;
        private Vector3 mousePosition;
        private Vector3 mouseOffset;

        private CardDeck Deck = null;

        // Use this for initialization
        void Start()
        {
            _topCardOrder = GetTopSortingOrder();
            Deck = GetDeck();
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) == true)
            {
                var targetsHit = Physics2D.OverlapPointAll(mousePosition);
                var cardHit = GetTopCardFromColiders(targetsHit);
                var deckHit = GetDeckFromColiders(targetsHit);
                var handHit = GetHandFromColiders(targetsHit);
                if (cardHit == null && deckHit != null)
                {
                    var newCardPosition = mousePosition;
                    newCardPosition.z = 0;
                    var newCard = Instantiate(Deck.DrawCard(), newCardPosition, Quaternion.identity);
                    cardHit = newCard.GetComponent<Collider2D>();
                }
                if (cardHit != null)
                {
                    if (handHit != null)
                    {
                        handHit.RemoveCardFromHand(cardHit.gameObject);
                    }
                    ResetLayersUponIntegerOverfolat();
                    _topCardOrder = cardHit.GetComponent<Renderer>().MoveToTopLayer(_topCardOrder);
                    movableObject = cardHit.transform.gameObject;
                    mouseOffset = movableObject.transform.position - mousePosition;
                }
            }

            if (movableObject != null)
            {
                movableObject.transform.position = mousePosition + mouseOffset;
            }

            if (Input.GetMouseButtonUp(0) == true && movableObject != null)
            {
                var targetsHit = Physics2D.OverlapPointAll(mousePosition);
                var handHit = GetHandFromColiders(targetsHit);
                if (handHit != null)
                {
                    handHit.AddCardToHand(movableObject);
                }
                movableObject = null;
            }
        }

        private static Collider2D GetTopCardFromColiders(Collider2D[] coliders)
        {
            return coliders
                .Where(x => x.transform.gameObject.CompareTag("Card"))
                .OrderByDescending(x => x.transform.gameObject.GetComponent<Renderer>().sortingOrder)
                .FirstOrDefault();
        }

        private static Collider2D GetDeckFromColiders(Collider2D[] coliders)
        {
            return coliders
                .Where(x => x.transform.gameObject.CompareTag("CardDeck"))
                .OrderByDescending(x => x.transform.gameObject.GetComponent<Renderer>().sortingOrder)
                .FirstOrDefault();
        }

        private static PlayerHand GetHandFromColiders(Collider2D[] coliders)
        {
            var playerHand = coliders
                .Where(x => x.transform.gameObject.CompareTag("PlayerHand"))
                .FirstOrDefault();
            if (playerHand != null)
            {
                return playerHand.gameObject.GetComponent<PlayerHand>();
            }
            return null;
        }

        private CardDeck GetDeck()
        {
            return CardDeck.GetDeck();
        }

        private int GetTopSortingOrder()
        {
            var allObjects = FindObjectsOfType<GameObject>();
            return allObjects
                .Where(x => x.CompareTag("Card"))
                .OrderByDescending(x => x.transform.gameObject.GetComponent<Renderer>().sortingOrder)
                .FirstOrDefault()?
                .GetComponent<Renderer>().sortingOrder ?? 0;
        }

        private void ResetLayersUponIntegerOverfolat()
        {
            if (_topCardOrder >= maxLayer)
            {
                var allCards = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Card")).ToList();
                allCards.ForEach(x => x.transform.gameObject.GetComponent<Renderer>().sortingOrder = 0);
                _topCardOrder = 0;
            }
        }
    }
}