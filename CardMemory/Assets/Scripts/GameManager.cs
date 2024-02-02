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
    public List<Sprite> cardSprites; // ��� ī�� �̹����� ��� ����Ʈ
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

        List<Sprite> pickedCards = new List<Sprite>(); // ���� ī�带 ���� ����Ʈ

        for (int i = 0; i < numberOfCards; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardSprites.Count); // �������� �ε����� �����մϴ�.
            Sprite pickedCard = cardSprites[randomIndex]; // ���õ� �ε����� ī�带 �����ɴϴ�.
            pickedCards.Add(pickedCard); // ���� ī�� ����Ʈ�� �߰��մϴ�.
            pickedCards.Add(pickedCard);

            // ���� ī�带 ����Ʈ���� �����Ͽ� �ߺ� ������ �����մϴ�.
            cardSprites.RemoveAt(randomIndex);
        }

        // ���� ī�带 �����ϴ�.
        pickedCards = pickedCards.OrderBy(x => UnityEngine.Random.value).ToList();

        // ������ ��ġ ���
        Vector3 spawnPosition = new Vector3(-400, 50, 0); // �߾� ��ġ
        Vector3 offset = new Vector3(cardWidth * 2.0f, -cardHeight * 2.0f, 0); // ī�� ����

        // ī�带 �����ϰ� ��ġ�մϴ�.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCard = Instantiate(card);
                RectTransform newCardRectTransform = newCard.GetComponent<RectTransform>();
                newCardRectTransform.SetParent(GameObject.Find("cards").transform, false);
                newCardRectTransform.localPosition = spawnPosition + new Vector3(offset.x * j, offset.y * i, 0);

                // ���� ī�带 ����Ʈ���� �����ͼ� �̹����� �����մϴ�.
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