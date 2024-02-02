using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text timeTxt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endPopup;
    public GameObject nextPopup;
    public List<Sprite> cardSprites; // 모든 카드 이미지를 담는 리스트
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    float time = 30.0f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (stage1)
        {
            CardGenerate(2, 2, 2);
        }
        else if (stage2)
        {
            CardGenerate(2, 4, 4);
        }
        else if (stage3)
        {
            CardGenerate(3, 6, 9);
        }
    }

    void CardGenerate(int rows, int columns, int numberOfCards)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        float cardWidth = cardRectTransform.rect.width;
        float cardHeight = cardRectTransform.rect.height;

        List<Sprite> pickedCards = new List<Sprite>(); // 뽑힌 카드를 담을 리스트

        for (int i = 0; i < numberOfCards; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardSprites.Count); // 랜덤으로 인덱스를 선택합니다.
            Sprite pickedCard = cardSprites[randomIndex]; // 선택된 인덱스의 카드를 가져옵니다.
            pickedCards.Add(pickedCard); // 뽑힌 카드 리스트에 추가합니다.
            pickedCards.Add(pickedCard);

            // 뽑힌 카드를 리스트에서 제거하여 중복 선택을 방지합니다.
            cardSprites.RemoveAt(randomIndex);
        }

        // 뽑힌 카드를 섞습니다.
        pickedCards = pickedCards.OrderBy(x => UnityEngine.Random.value).ToList();

        // 생성할 위치 계산
        Vector3 spawnPosition = new Vector3(-400, 50, 0); // 중앙 위치
        Vector3 offset = new Vector3(cardWidth * 2.0f, -cardHeight * 2.0f, 0); // 카드 간격

        // 카드를 생성하고 배치합니다.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCard = Instantiate(card);
                RectTransform newCardRectTransform = newCard.GetComponent<RectTransform>();
                newCardRectTransform.SetParent(GameObject.Find("cards").transform, false);
                newCardRectTransform.localPosition = spawnPosition + new Vector3(offset.x * j, offset.y * i, 0);

                // 뽑힌 카드를 리스트에서 가져와서 이미지를 설정합니다.
                Sprite cardSprite = pickedCards[i * columns + j];
                newCard.transform.Find("front").GetComponent<Image>().sprite = cardSprite;
            }
        }
        foreach (Sprite card in pickedCards)
        {
            Debug.Log("Picked Card: " + card.name);
        }
    }

    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time <= 0.0f)
        {
            endPopup.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<Image>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<Image>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<Card>().destroyCard();
            secondCard.GetComponent<Card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 0)
            {
                nextPopup.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.GetComponent<Card>().closeCard();
            secondCard.GetComponent<Card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

}