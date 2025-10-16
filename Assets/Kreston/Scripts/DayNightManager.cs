using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DayNightManager : MonoBehaviour
{
    void Start() => StartCoroutine(DayTime());

    public UnityEvent nightTimeCue;
    public UnityEvent dayTimeCue;
    [SerializeField] private bool _isNightTime;
    [SerializeField] private int _timeInDay = 180; // In seconds

    IEnumerator DayTime()
    {
        dayTimeCue.Invoke();
        _isNightTime = false;
        yield return new WaitForSeconds(_timeInDay);
        nightTimeCue.Invoke();
    }
}
