using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{

    [SerializeField] private AudioSource neighAudio;
    [SerializeField] private AudioClip loud;
    [SerializeField] private AudioClip notLoud;


    public void changeToMainScript()
    {
        SceneManager.LoadScene(1);
    }


    public void exitGame()
    {
        Application.Quit();
    }


    public void neigh()
    {
        if (Random.Range(0, 4) == 0)
        {
            neighAudio.clip = loud;
        }
        else
        {
            neighAudio.clip = notLoud;
        }
        neighAudio.Play();
    }





}
