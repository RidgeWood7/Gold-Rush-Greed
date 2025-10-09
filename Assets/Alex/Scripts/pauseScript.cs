using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class pauseScript : MonoBehaviour
{


    public TMPro.TMP_Text tipsText;

    public List<string> toolTips;

    private bool pauseOpened = true;

    public Canvas pauseCanvas;
    public Canvas dialogueCanvas;
    private float waitTime = 1f;
    private float coolDown = 0;

    





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tipsText.text = toolTips[Random.Range(0, toolTips.Count)];
    }

    void Update()
    {

        if (!pauseOpened)
        {
            coolDown += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("1");
            if (!pauseOpened)
            {

                if (coolDown > 0.25f)
                {
                    coolDown = 0.0f;
                    dialogueCanvas.gameObject.SetActive(false);
                    Time.timeScale = 0f;
                    Debug.Log("2");
                    pauseOpened = true;
                    tipsText.text = toolTips[Random.Range(0, toolTips.Count)];
                    pauseCanvas.gameObject.SetActive(true);
                }
            }
            else
            {
                closePauseMenu();
            }

        }
    }


   

    public void closePauseMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("3");
        pauseOpened = false;
        pauseCanvas.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(true);
    }






}
