using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Player Player = Player.Default;
    private List<GameObject> CardsInHand = new List<GameObject>();
    private Vector3 HandPosition;

    private void Start()
    {
        HandPosition = GetHandTransformation();
    }

    private void Update()
    {
        TransformCardsInHand();
    }

    public void AddCardToHand(GameObject card)
    {
        CardsInHand.Add(card);
        TransformCardsInHand();
    }

    public void RemoveCardFromHand(GameObject card)
    {
        CardsInHand.Remove(card);
        TransformCardsInHand();
    }

    private Vector3 GetHandTransformation()
    {
        var Hand = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("PlayerHand")).FirstOrDefault();
        return Hand.transform.position;
    }

    private void TransformCardsInHand()
    {
        var positions = CalculateCardsPosition(CardsInHand.Count);
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            var position = HandPosition;
            position.x += positions[i];
            CardsInHand[i].transform.position = position;
            CardsInHand[i].GetComponent<Renderer>().sortingOrder = i;
        }
    }

    private static float[] CalculateCardsPosition(int cardCount)
    {
        float[] cardsPosition = new float[cardCount];
        float cardDistance = (cardCount > 10) ? 20f / cardCount : 2f;
        float positionOffset = -(cardDistance * (cardCount - 1)) / 2;
        for (int i = 0; i < cardCount; i++)
        {
            cardsPosition[i] = positionOffset + (cardDistance * i);
        }
        return cardsPosition;
    }
}