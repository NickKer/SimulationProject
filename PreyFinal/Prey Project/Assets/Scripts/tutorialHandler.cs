using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialHandler : MonoBehaviour
{

    public Image goalTutorial;
    public Image controlsTutorial;
    public Button continuelevel;
    bool goalComplete = false;
    bool movementcomplete = false;


    void Start()
    {
        continuelevel.enabled = false;
        goalTutorial.enabled = false;
        controlsTutorial.enabled = false;
        if (staticData.tutorialComplete == false)
        {

            goalTutorial.enabled = true;
            controlsTutorial.enabled = true;
            continuelevel.enabled = true;
        }

    }

    public void onPress()
    {

        if (goalComplete == false)
        {
            goalTutorial.enabled = false;
            goalComplete = true;
        }

        else if (goalComplete == true && movementcomplete == false)
        {
            controlsTutorial.enabled = false;
            staticData.tutorialComplete = true;
            movementcomplete = true;

        }

    }
    
        //this function is kill, doesn't destroy UI button
    public void destroyButton()
    {

        if (goalComplete == true && movementcomplete == true)
        {
            Destroy(continuelevel);
        }
    }

}
