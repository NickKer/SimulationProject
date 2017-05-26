using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Button myButton;
    public Dropdown myDropdown;

    void Start()
    {
        myDropdown.onValueChanged.AddListener(delegate
        {
            myDropdownValueChangedHandler(myDropdown);
        });
                
    }
    void Destroy()
    {
        myDropdown.onValueChanged.RemoveAllListeners();
    }

    private void myDropdownValueChangedHandler(Dropdown target)
    {
        Debug.Log("selected: " + target.value);
		difficultyHolder.difficulty = myDropdown.value;
    }

    public void SetDropdownIndex(int index)
    {
        myDropdown.value = index;

    }

    
    void loadGameScene()
    {


        SceneManager.LoadScene("game");
        
    }
}



