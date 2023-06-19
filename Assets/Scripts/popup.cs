using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    public GameObject PopUp;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnmMouseDown()
    {
        PopUp.SetActive (active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
