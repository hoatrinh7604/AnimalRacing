using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private int playerColor = 0;
    [SerializeField] Image colorImage;
    private int playerNextColor = 0;
    [SerializeField] Image colorNextImage;
    private UIController uiController;

    private float time;
    [SerializeField] float timeToChangeColor;
    [SerializeField] float timeOfGame;

    public bool isStartGame;
    [SerializeField] SpawCar spawCarController;
    [SerializeField] HandleRanking rankingController;
    private List<GameObject> listCars;
    public int[] listRanking;
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
        listCars = spawCarController.Spaw();
        rankingController.UpdateList(listRanking);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartGame)
        {
            time += Time.deltaTime;
            UpdateTime();
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void UpdateTime()
    {
        uiController.UpdateScore(time);
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(int index)
    {
        if(index == playerColor)
        {
            UpdateScore();
        }
        else
        {
            GameOver();
        }
    }

    public void StartRunning()
    {
        isStartGame = true;
        for(int i = 0; i< listCars.Count; i++)
        {
            listCars[i].GetComponent<CarController>().StartRunning(true);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
        Reset();
    }

    public void UpdateScore()
    {
        time++;
        score++;
        uiController.UpdateScore(score);
        if (score > highscore)
        {
            highscore = score;
            uiController.UpdateHighScore(highscore);
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    public void UpdateColor()
    {
        colorImage.color = template[playerColor];
        colorNextImage.color = template[playerNextColor];
    }

    public void ChangeColor()
    {
        playerColor = playerNextColor;
        playerNextColor = Random.Range(0, template.Length);
        while(playerNextColor == playerColor)
        {
            playerNextColor = Random.Range(0, template.Length);
        }
        UpdateColor();
    }

    public void ReachEnd(int idCar)
    {
        //listRanking[currentIndex++] = idCar;
        for(int i = 0; i< listRanking.Length; i++)
        {
            if(listRanking[i] == idCar)
            {
                int temp = listRanking[currentIndex];
                listRanking[currentIndex] = idCar;
                listRanking[i] = temp;
                currentIndex++;
                break;
            }
        }

        rankingController.UpdateList(listRanking);

        if(currentIndex == listRanking.Length)
        {
            uiController.UpdateTopWin(rankingController.GetSprite(listRanking[0]), rankingController.GetSprite(listRanking[1]), rankingController.GetSprite(listRanking[2]));
            uiController.TopWin();
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;

        playerNextColor = Random.Range(0, template.Length);
        ChangeColor();
        time = 0;
        SetSlider();
        score = 0;
        isStartGame = false;
        highscore = PlayerPrefs.GetInt("highscore");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);
        currentIndex = 0;
        listRanking = null;
    }

}
