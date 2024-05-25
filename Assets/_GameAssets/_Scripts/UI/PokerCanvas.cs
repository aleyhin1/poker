using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PokerCanvas : MonoSingleton<PokerCanvas>
{
    [Header("Win Info Panel Fields")]
    [Space]
    [SerializeField] private GameObject _winInfoPanel;
    [SerializeField] private TextMeshProUGUI _handInfoText;
    [SerializeField] private List<Image> _winningHandCards;
    private Vector2 _winningHandCardSize;
    private StringBuilder _handInfoString = new StringBuilder(100);

    private void Awake()
    {
        _winningHandCardSize = _winningHandCards[0].GetComponent<RectTransform>().sizeDelta;
    }

    public void ChangeVisibilityWinInfoPanel(bool isValue)
    {
        _winInfoPanel.SetActive(isValue);
    }

    public void ShowWinInfo((Dictionary<Player, (HandRank, CardRank[], List<Card>)>, CardRank?) winInfo)
    {
        ChangeVisibilityWinInfoPanel(true);

        (HandRank, CardRank[], List<Card>) handInfo = winInfo.Item1.Values.FirstOrDefault();
        HandRank handRank = handInfo.Item1;
        CardRank[] cardRanks = handInfo.Item2;
        CardRank? kicker = winInfo.Item2;
        List<Card> cards = handInfo.Item3;

        SetHandInfoText(handRank, cards, cardRanks, kicker);
    }

    private void SetHandInfoText(HandRank handRank, List<Card> cards, CardRank[] cardRanks, CardRank? kicker)
    {
        _handInfoString.Clear();

        SetHandRankText(handRank);
        SetCardRankText(cardRanks);
        SetKickerText(kicker);
        SetWinningHandCards(cards);

        _handInfoText.text = _handInfoString.ToString();
    }

    private void SetHandRankText(HandRank handRank)
    {
        _handInfoString.Append(handRank.ToString() + " ");
    }

    private void SetCardRankText(CardRank[] cardRanks)
    {
        _handInfoString.Append("of ");

        for (int i = 0; i < 2; i++)
        {
            if (i < cardRanks.Length)
            {
                _handInfoString.Append(cardRanks[i].ToString() + " ");
            }
            if (cardRanks.Length > 1 && i == 0)
            {
                _handInfoString.Append("and ");
            }

        }
    }

    private void SetKickerText(CardRank? kicker)
    {
        if (kicker != null)
        {
            _handInfoString.Append("with the kicker of " + kicker.ToString());
        }
    }

    private void SetWinningHandCards(List<Card> winningCards)
    {
        int index = 0;
        foreach (Image image in _winningHandCards)
        {
            Card card = winningCards[index];
            if (card != null)
            {
                image.GetComponent<RectTransform>().sizeDelta = _winningHandCardSize;
                image.sprite = card.Front;
            }
            else
            {
                image.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            }
            index++;
        }
    }
}
