using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public List<Sprite> cardSprites; // 모든 카드 이미지를 담는 리스트
    public int numberOfCardsToPick; // 랜덤으로 뽑을 카드의 개수

    void Start()
    {
        PickRandomCards(numberOfCardsToPick); // 원하는 개수만큼 카드를 뽑습니다.
    }

    void PickRandomCards(int numberOfCards)
    {
        List<Sprite> pickedCards = new List<Sprite>(); // 뽑힌 카드를 담을 리스트

        for (int i = 0; i < numberOfCards; i++)
        {
            int randomIndex = Random.Range(0, cardSprites.Count); // 랜덤으로 인덱스를 선택합니다.
            Sprite pickedCard = cardSprites[randomIndex]; // 선택된 인덱스의 카드를 가져옵니다.
            pickedCards.Add(pickedCard); // 뽑힌 카드 리스트에 추가합니다.
            pickedCards.Add(pickedCard);

            // 뽑힌 카드를 리스트에서 제거하여 중복 선택을 방지합니다.
            cardSprites.RemoveAt(randomIndex);
        }

        // 뽑힌 카드 리스트를 출력합니다.
        foreach (Sprite card in pickedCards)
        {
            Debug.Log("Picked Card: " + card.name);
        }
    }
}
