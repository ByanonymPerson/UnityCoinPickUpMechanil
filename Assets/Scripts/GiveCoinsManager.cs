using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class GiveCoinsManager : MonoBehaviour
{
    [SerializeField] private GameObject PileOfCoinParent;
    [SerializeField] private TextMeshProUGUI Counter;
    [SerializeField] private Vector3[] _InitialPos;
    [SerializeField] private Quaternion[] _InitialRotation;
    [SerializeField] private int _CoinNo;

    private void Start()
    {
        _InitialPos = new Vector3[_CoinNo];
        _InitialRotation = new Quaternion[_CoinNo];
        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            _InitialPos[i] = PileOfCoinParent.transform.GetChild(i).position;
            _InitialRotation[i] = PileOfCoinParent.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).position = _InitialPos[i];
            PileOfCoinParent.transform.GetChild(i).rotation = _InitialRotation[i];

        }
    }


    public void GivePileOfCoin(int noCoin)
    {
        Reset();

        var delay = 0f;

        PileOfCoinParent.SetActive(true);

        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
              
            PileOfCoinParent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(319f, 909f), 1f).SetDelay(delay).SetEase(Ease.InBack);
         
            PileOfCoinParent.transform.GetChild(i).DORotate(Vector3.zero,0.5f).SetDelay(delay+0.5f).SetEase(Ease.Flash).OnComplete(CountCoinsComplete);

            PileOfCoinParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.8f).SetEase(Ease.OutBack);

            delay += 0.2f;
        }

       
    }

     void CountCoinsComplete()
    {
        PlayerPrefs.SetInt("CountsCoin", PlayerPrefs.GetInt("CountsCoin") + 1);

        Counter.text = PlayerPrefs.GetInt("CountsCoin").ToString();
    }
    IEnumerable CountCoins(int coinNo)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        var timer = 0f; 

        for(int i = 0; i< coinNo; i++)
        {
            PlayerPrefs.SetInt("CountsCoin", PlayerPrefs.GetInt("CountsCoin") + 1);

            Counter.text = PlayerPrefs.GetInt("CountsCoin").ToString();

            timer += 0.05f;

            yield return new WaitForSecondsRealtime(timer);
        }
    }

}


