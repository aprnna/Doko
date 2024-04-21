using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MenuItem
{
    public string name;
    public GameObject Hover;
    public GameObject Check;
    public GameObject Instruction;
}
public class RecycleMenu : MonoBehaviour
{
    public MenuItem[] MenuItems;
    // Start is called before the first frame update
    void Start()
    {
        MenuItems[0].Check.SetActive(false);
        MenuItems[0].Hover.SetActive(true);
        MenuItems[0].Instruction.SetActive(true);
        for (int idx = 1; idx < MenuItems.Length; idx++)
        {
            MenuItems[idx].Check.SetActive(false);
            MenuItems[idx].Hover.SetActive(false);
            MenuItems[idx].Instruction.SetActive(false);
        }
    }
    
    public void NextStep(int currentStep)
    {
        currentStep--;
        if (currentStep == MenuItems.Length-1)
        {
            MenuItems[currentStep].Hover.SetActive(false);
            MenuItems[currentStep].Check.SetActive(true);
        }
        else
        {
            MenuItems[currentStep].Check.SetActive(true);
            MenuItems[currentStep].Hover.SetActive(false);
            MenuItems[currentStep].Instruction.SetActive(false);
            MenuItems[currentStep + 1].Hover.SetActive(true);
            MenuItems[currentStep + 1].Instruction.SetActive(true);
        }

    }
    
    public bool AllStepDone()
    {
        int count = 0;
        for(int idx = 0; idx < MenuItems.Length; idx++)
        {
            if (MenuItems[idx].Check.activeSelf) count++;
        }
        if (count == MenuItems.Length) return true;
        return false;
    }
}
