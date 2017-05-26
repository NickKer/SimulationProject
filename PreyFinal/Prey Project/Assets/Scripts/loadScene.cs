using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {

    public Button mybutton;


    public void onClick()
    {

        SceneManager.LoadScene("game");

    }


}