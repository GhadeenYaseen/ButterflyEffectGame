using System.Collections;
using TMPro;
using UnityEngine;

public class TimeOutManager : MonoBehaviour
{
    public static TimeOutManager instance {get; private set;}

    [SerializeField] private TextMeshProUGUI timerText;

    [Tooltip("In minutes")]
    [SerializeField] private float timeBeforeLose;

    private float _currentTimeLeft;

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        
        _currentTimeLeft = timeBeforeLose * 60f;
    }

    public void StartCountDown()
    {
        StartCoroutine(BeginCountDown());
    }

    private IEnumerator BeginCountDown()
    {
        while (_currentTimeLeft > 0f)
            {
                int mins = Mathf.FloorToInt(_currentTimeLeft / 60f);
                int secs = Mathf.FloorToInt(_currentTimeLeft % 60f);

                timerText.text = $"{mins:00}:{secs:00}";

                yield return null;
                _currentTimeLeft -= Time.deltaTime;
            }

        timerText.text = "00:00";
        UIManager.instance.GameOver();
        Debug.LogError("GAME OVER FOO");
    }
     
}
