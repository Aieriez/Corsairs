using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController timer;
    [SerializeField]
    public float timeDuration;
    public float stopwatch;
    [SerializeField]
    private TextMeshProUGUI timerTxt;
    [SerializeField]
    WaitForSeconds time = new WaitForSeconds(0.5f);
    private float blinkTimer;
    private float blinkDuration = 1f;

    void Start()
    {
        timeDuration = UIManager.instance.playTime * 60;
        GetComponent<TextMeshProUGUI>();
        ResetTimer();   
    }

    // Update is called once per frame
    void Update()
    {
        if(stopwatch > 0)
        {
            stopwatch -= Time.deltaTime;
            DisplayTimer(stopwatch);
            if (stopwatch < 20)
            {
                TimerBlink();           
            }
        }
        else
        {
            stopwatch = 0;
            DisplayTimer(stopwatch);
            if(!GameManager.instance.endGame)
            {
                GameManager.instance.endGame = true;
            } 
        }    
    }

    private void ResetTimer()
    {
        stopwatch = timeDuration;
    }

    private void DisplayTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}:{1:00}", minutes, seconds);
        timerTxt.text = currentTime;
    }

    void TimerBlink()
    {
        if (blinkTimer <= 0)
        {
            blinkTimer = blinkDuration;  
        }
        else if (blinkTimer >= blinkDuration / 2)
        {
            blinkTimer -= Time.deltaTime;
            timerTxt.color = Color.red;
            gameObject.transform.localScale = new Vector3 (1.1f,1.1f,0);
        } else
        {
            blinkTimer -= Time.deltaTime;
            timerTxt.color = Color.white;
            gameObject.transform.localScale = new Vector3 (1f,1f,0);
        }   
    }

}
