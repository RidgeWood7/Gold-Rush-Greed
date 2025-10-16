using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DayNightManager : MonoBehaviour
{
    void Start() => StartCoroutine(DayTime());

    public UnityEvent nightTimeCue;
    public UnityEvent dayTimeCue;
    public GameObject Warning;
    private int _30SecWarning = 150; // In seconds
    private int _last30Sec = 30; // In seconds

    public void quickNight()
    {
        StopCoroutine(DayTime());
        nightTimeCue.Invoke();
        SceneManager.LoadScene(2);
    }

    IEnumerator DayTime()
    {
        dayTimeCue.Invoke();
        yield return new WaitForSeconds(_30SecWarning);
        if (Warning != null)
            Warning.SetActive(true);
        yield return new WaitForSeconds(_last30Sec);
        nightTimeCue.Invoke();
        SceneManager.LoadScene(2);
    }
}
