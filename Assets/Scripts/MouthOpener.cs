using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthOpener : MonoBehaviour
{
    public GameObject MouthClose, MouthOpen;

    // Start is called before the first frame update
    void Start()
    {
        MouthClose.SetActive(true);
        MouthOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MouthClose.SetActive(false);
        MouthOpen.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MouthClose.SetActive(true);
        MouthOpen.SetActive(false);
    }

}
