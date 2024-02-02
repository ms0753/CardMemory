using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryBtn : MonoBehaviour
{
    public GameObject uiPrefab;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        // UI 프리팹을 생성하고 Canvas 하위에 배치합니다.
        GameObject newUI = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);
        newUI.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }
}
