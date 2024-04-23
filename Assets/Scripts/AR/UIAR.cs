using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIAR : MonoBehaviour
{
    [SerializeField]
    private Slider uiSlider;

    [SerializeField] private GameObject finishPanel;

    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        uiSlider.value = 0;
        uiSlider.maxValue = _gm.trashLimit;
    }

    // Update is called once per frame
    void Update()
    {   
        uiSlider.value = _gm.trashCount;
        if (uiSlider.value == uiSlider.maxValue)
        {
            finishPanel.SetActive(true);
        }
    }
}
