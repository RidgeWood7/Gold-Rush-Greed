using UnityEngine;
using System.Collections.Generic;


public class pauseScript : MonoBehaviour
{

    public TMPro.TMP_Text tipsText;

    public List<string> toolTips;

    private bool pauseOpened = true;

    public Canvas pauseCanvas; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tipsText.text = toolTips[Random.Range(0, toolTips.Count)];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("1");
            if (!pauseOpened)
            {
                Debug.Log("2");
                pauseOpened = true;
                tipsText.text = toolTips[Random.Range(0, toolTips.Count)];
                pauseCanvas.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("3");
                pauseOpened = false;
                pauseCanvas.gameObject.SetActive(false);
            }

        }
    }






}
