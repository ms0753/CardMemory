using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public List<Sprite> cardSprites; // ��� ī�� �̹����� ��� ����Ʈ
    public int numberOfCardsToPick; // �������� ���� ī���� ����

    void Start()
    {
        PickRandomCards(numberOfCardsToPick); // ���ϴ� ������ŭ ī�带 �̽��ϴ�.
    }

    void PickRandomCards(int numberOfCards)
    {
        List<Sprite> pickedCards = new List<Sprite>(); // ���� ī�带 ���� ����Ʈ

        for (int i = 0; i < numberOfCards; i++)
        {
            int randomIndex = Random.Range(0, cardSprites.Count); // �������� �ε����� �����մϴ�.
            Sprite pickedCard = cardSprites[randomIndex]; // ���õ� �ε����� ī�带 �����ɴϴ�.
            pickedCards.Add(pickedCard); // ���� ī�� ����Ʈ�� �߰��մϴ�.
            pickedCards.Add(pickedCard);

            // ���� ī�带 ����Ʈ���� �����Ͽ� �ߺ� ������ �����մϴ�.
            cardSprites.RemoveAt(randomIndex);
        }

        // ���� ī�� ����Ʈ�� ����մϴ�.
        foreach (Sprite card in pickedCards)
        {
            Debug.Log("Picked Card: " + card.name);
        }
    }
}
