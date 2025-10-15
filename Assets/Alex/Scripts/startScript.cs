using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{
   

    public void changeToMainScript()
    {
        SceneManager.LoadScene(1);
    }


    public void exitGame()
    {
        Application.Quit();
    }



}
