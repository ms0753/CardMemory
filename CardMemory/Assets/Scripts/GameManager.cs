using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public TMP_Text timeTxt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endPopup;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    float time = 30.0f;

    private void Awake()
    {
        gameManager = this;
    }

    void Start()
    {
        // 각 스테이지에 맞는 카드 배치를 수행합니다.
        if (stage1)
        {
            Stage1Generate(2, 2);
        }
        else if (stage2)
        {
            Stage2Generate(2, 4);
        }
        else if (stage3)
        {
            Stage3Generate(3, 6);
        }
        
        /*
        int[] trumps = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12 };
        trumps = trumps.OrderBy(item => UnityEngine.Random.Range(-1.0f, 1.0f)).ToArray();
        

        for (int i = 0; i < 4; i++)
        {
            string trumpName = "clubs" + trumps[i].ToString();
            Image cardImage = newCard.transform.Find("front").GetComponent<Image>();
            Sprite sprite = Resources.Load<Sprite>(trumpName);
            
            if (sprite != null) // 해당 이미지가 올바로 로드되었는지 확인
            {
                cardImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("Failed to load sprite: " + trumpName);
            }
            
        }
        */
    }

    void Stage1Generate(int rows, int columns)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        float cardWidth = cardRectTransform.rect.width;
        float cardHeight = cardRectTransform.rect.height;

        int[] trumps = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12 };


        // 생성할 위치 계산
        Vector3 spawnPosition = new Vector3(-100, 50, 0); // 중앙 위치
        Vector3 offset = new Vector3(cardWidth * 2.0f, -cardHeight * 2.0f, 0); // 카드 간격

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCard = Instantiate(card);
                RectTransform newCardRectTransform = newCard.GetComponent<RectTransform>();
                newCardRectTransform.SetParent(GameObject.Find("cards").transform, false);
                newCardRectTransform.localPosition = spawnPosition + new Vector3(offset.x * j, offset.y * i, 0);

                string cardName = "cards" + trumps[i].ToString();
                newCard.transform.Find("front").GetComponent<Image>().sprite = Resources.Load<Sprite>(cardName);
            }
        }
    }

    void Stage2Generate(int rows, int columns)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        float cardWidth = cardRectTransform.rect.width;
        float cardHeight = cardRectTransform.rect.height;

        Vector3 spawnPosition = new Vector3(-300, 50, 0);
        Vector3 offset = new Vector3(cardWidth * 2.0f, -cardHeight * 2.0f, 0);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCard = Instantiate(card);
                RectTransform newCardRectTransform = newCard.GetComponent<RectTransform>();
                newCardRectTransform.SetParent(GameObject.Find("cards").transform, false);
                newCardRectTransform.localPosition = spawnPosition + new Vector3(offset.x * j, offset.y * i, 0);
            }
        }
    }

    void Stage3Generate(int rows, int columns)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        float cardWidth = cardRectTransform.rect.width;
        float cardHeight = cardRectTransform.rect.height;

        int[] trumps = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12 };
        for (int i = 0; i < trumps.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, trumps.Length);
            int temp = trumps[randomIndex];
            trumps[randomIndex] = trumps[i];
            trumps[i] = temp;
        }

        Vector3 spawnPosition = new Vector3(-400, 100, 0);
        Vector3 offset = new Vector3(cardWidth * 2.0f, -cardHeight * 2.0f, 0);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCard = Instantiate(card);
                RectTransform newCardRectTransform = newCard.GetComponent<RectTransform>();
                newCardRectTransform.SetParent(GameObject.Find("cards").transform, false);
                newCardRectTransform.localPosition = spawnPosition + new Vector3(offset.x * j, offset.y * i, 0);
                string cardName = "cards" + trumps[i].ToString();
                newCard.transform.Find("front").GetComponent<Image>().sprite = Resources.Load<Sprite>(cardName);
            }
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
            if (cardsLeft == 2)
            {
                endPopup.SetActive(true);
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

    public void checkMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<Image>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<Image>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<Card>().destroyCard();
            secondCard.GetComponent<Card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            Debug.Log(cardsLeft);
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