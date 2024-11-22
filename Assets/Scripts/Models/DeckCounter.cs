using Assets.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCounter : MonoBehaviour
{
    public TMP_Text text;
    private CardDeck cardDeck;

    private void Start()
    {
        cardDeck = CardDeck.GetDeck();
        text.text = cardDeck.GetDeckCount().ToString();
    }

    private void Update()
    {
        text.text = cardDeck.GetDeckCount().ToString();
    }
}