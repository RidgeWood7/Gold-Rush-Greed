using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DayNightManager : MonoBehaviour
{
    void Start() => StartCoroutine(FirstDayTime());

    public UnityEvent nightTimeCue;
    public UnityEvent dayTimeCue;
    [SerializeField] private bool _isNightTime;
    [SerializeField] private int _timeInDayOne = 240; // In seconds
    [SerializeField] private int _timeInDay = 180; // In seconds
    [SerializeField] private int _timeInNight = 180; // In seconds

    IEnumerator FirstDayTime()
    {
        dayTimeCue.Invoke();
        _isNightTime = false;
        yield return new WaitForSeconds(_timeInDayOne);
        StartCoroutine(NightTime());
    }
    IEnumerator DayTime()
    {
        dayTimeCue.Invoke();
        _isNightTime = false;
        yield return new WaitForSeconds(_timeInDay);
        StartCoroutine(NightTime());
    }
    IEnumerator NightTime()
    {
        nightTimeCue.Invoke();
        _isNightTime = true;
        yield return new WaitForSeconds(_timeInNight);
        StartCoroutine(DayTime());
    }
}
