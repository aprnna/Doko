using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] public TMP_Text coinText;
    private PlayerManager _pm;

    void Start()
    {
        _pm = PlayerManager.Instance;
        coinText.text = _pm.Coin.ToString();
    }

    private void Update()
    {
        coinText.text = _pm.Coin.ToString();
    }
}
