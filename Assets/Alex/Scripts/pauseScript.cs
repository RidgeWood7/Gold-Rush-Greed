using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class pauseScript : MonoBehaviour
{


    public TMPro.TMP_Text tipsText;

    public List<string> toolTips;

    private bool pauseOpened = false;

    public Canvas pauseCanvas;
    public Canvas dialogueCanvas;
    public dialogueScript dialogueCanvasScript;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueCanvasScript = GameObject.Find("DialogueCanvas").transform.GetChild(0).GetComponent<dialogueScript>();
       
        
        tipsText.text = toolTips[Random.Range(0, toolTips.Count)];
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseOpened)
            {

                dialogueCanvasScript.setText();
                openPauseMenu();

            }
            else
            {
                closePauseMenu();
            }

        }

    }

    private void openPauseMenu()
    {
        dialogueCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
        pauseOpened = true;
        tipsText.text = toolTips[Random.Range(0, toolTips.Count)];
        pauseCanvas.gameObject.SetActive(true);
    }

    public void closePauseMenu()
    {
        Time.timeScale = 1f;
        
        pauseOpened = false;
        pauseCanvas.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(true);
    }
}
