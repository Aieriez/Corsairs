using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Animator gameMenu, configurationPainel;
    public GameObject backgroundBar, tutorial;
    public Button replay, goToMenu;
    public TextMeshProUGUI enemyCount, pointCount, totalPoints, tNum, eNum;
    [SerializeField] private Button play, config;
    public int playTime, spawnEnemiesTime;
    [SerializeField] private Slider downTime , enemyNumbers;

    void Awake()
    {
		if(instance == null)
		{
			instance = this;
            DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}

        if(!PlayerPrefs.HasKey("Time"))
        {
            PlayerPrefs.SetInt("Time", 1);
            playTime = PlayerPrefs.GetInt("Time");
        }
        if(!PlayerPrefs.HasKey("Enemy"))
        {
            PlayerPrefs.SetInt("Enemy", 5);
            spawnEnemiesTime = PlayerPrefs.GetInt("Enemy");
        }

        SceneManager.sceneLoaded += Load;

    }
    
    void Load(Scene scene, LoadSceneMode mode)
    {
        DataLoanding();
    }

    void DataLoanding()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            tNum = GameObject.Find("TimeNumber").GetComponent<TextMeshProUGUI>();
            eNum = GameObject.Find("EnemyNumber").GetComponent<TextMeshProUGUI>();
            downTime = GameObject.Find("TimeSlider").GetComponent<Slider>();
            enemyNumbers = GameObject.Find("EnemySlider").GetComponent<Slider>();
            eNum.text = enemyNumbers.value.ToString();
            tNum.text = downTime.value.ToString();
            configurationPainel = GameObject.Find("ConfigPanel").GetComponent<Animator>();
            play = GameObject.Find("PlayBtn").GetComponent<Button>();
            config = GameObject.Find("ConfigBtn").GetComponent<Button>();
            play.onClick.AddListener(Play);
            config.onClick.AddListener(ShowConfig);
            tutorial = GameObject.Find("Tutorial");

        }
        else
        {
            backgroundBar = GameObject.Find("BackgroundBar");
            gameMenu = GameObject.Find("EndGamePanel").GetComponent<Animator>();
            replay = GameObject.Find("Replay").GetComponent<Button>();
            goToMenu = GameObject.Find("GoToMenu").GetComponent<Button>();
            totalPoints = GameObject.Find("EndPonits").GetComponent<TextMeshProUGUI>();
            pointCount = GameObject.Find("PointsCount").GetComponent<TextMeshProUGUI>();
            enemyCount = GameObject.Find("EnemiesCount").GetComponent<TextMeshProUGUI>();
            playTime = PlayerPrefs.GetInt("Time");
            spawnEnemiesTime = PlayerPrefs.GetInt("Enemy");
            
            goToMenu.onClick.AddListener(BackToMenu);
            replay.onClick.AddListener(Replay);
        }
    }
    
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);   
    }

    public void ExitButton()
    {
        configurationPainel.Play("ConfigPanelReverse");
        tutorial.SetActive(true);
    }

    public void ShowConfig()
    {
        configurationPainel.Play("ConfigPanel");
        tutorial.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void OnChangeTime()
    {   
        playTime = (int)downTime.value;
        tNum.text =  downTime.value.ToString();
        PlayerPrefs.SetInt("Time", playTime);
    }
    public void OnEnemyNumberChange()
    {   
        spawnEnemiesTime = (int)enemyNumbers.value;
        eNum.text = enemyNumbers.value.ToString();
        PlayerPrefs.SetInt("Enemy", spawnEnemiesTime);
    }
}
