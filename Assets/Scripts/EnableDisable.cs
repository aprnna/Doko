using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisable : MonoBehaviour
{
    public GameObject DecorItems;
    public GameObject Hover;
    public bool isEnable = true;
    // Start is called before the first frame update
    public void ButtonClicked()
    {
        if (Hover.gameObject.activeSelf)
        {
            isEnable = !isEnable;
            DecorItems.SetActive(isEnable);
        }
    }
}
