using UnityEngine;
using System.Collections;

public class cutsceneScript : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Text infoText;
    [SerializeField] private TMPro.TMP_Text bottomText;
    [SerializeField] private UnityEngine.UI.Image background;
    [SerializeField] private GameObject _defenseGame;

    private static bool s_infoPlayed = false;
    private bool _exitQueued = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!s_infoPlayed)
        {
            StartCoroutine(fadeObject(infoText, "in", .3f));
        }
        StartCoroutine(fadeObject(bottomText, "in", .3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !_exitQueued)
        {
            _exitQueued = true;
            
            StartCoroutine(fadeObject(background, "out", 1f));
            StartCoroutine(fadeObject(bottomText, "out", 2f));
            if (!s_infoPlayed)
            {
                StartCoroutine(fadeObject(infoText, "out", 2f));
            }
            _defenseGame.SetActive(true);
            
             
        }
    }


    private IEnumerator fadeObject(Object obj, string direction, float rate)
    {


        //Sets startValue to 0f if direction equals in, if it doesnt it sets it to 1f
        float alphaValue = direction == "in" ? 0f : 1f;

        //Sets endValue to  the opposite of previous line
        float alphaEndValue = direction == "in" ? 1f : 0f;

        //Checks if the object type is text then makes a text variable for readabliltiy
        if (obj is TMPro.TMP_Text text)
        {
            //Sets original color so text color stays the same
            Color originalColor = text.color;


            //Has a loop that runs every frame till the alpha value of the text is basically the end value
            while (!Mathf.Approximately(alphaValue, alphaEndValue))
            {
                if (_exitQueued && Mathf.Approximately(alphaEndValue, 1f))
                {
                    yield break;
                }
                alphaValue = Mathf.MoveTowards(alphaValue, alphaEndValue, rate * Time.deltaTime);
                text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
                yield return null;
            }

            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaEndValue);

        }
        else if (obj is UnityEngine.UI.Image image)
        {
            Color originalColor = image.color;
            while (!Mathf.Approximately(alphaValue, alphaEndValue))
            {
                if (_exitQueued && Mathf.Approximately(alphaEndValue, 1f))
                {
                    yield break;
                }
                alphaValue = Mathf.MoveTowards(alphaValue, alphaEndValue, rate * Time.deltaTime);
                image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
                yield return null;
            }

            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaEndValue);
        }
        else
        {
            Debug.LogWarning("Unspported object type to fade");
        }
    }


   

}
