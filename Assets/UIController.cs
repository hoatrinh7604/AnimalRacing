using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject confirm;

    [SerializeField] GameObject topWin;
    [SerializeField] Image top1;
    [SerializeField] Image top2;
    [SerializeField] Image top3;

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highscore;

    [SerializeField] GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        confirm.SetActive(false);
        topWin.SetActive(false);
    }

    public void SetSlider(float value)
    {
        slider.GetComponent<SliderController>().SetSlider(value);
    }

    public void UpdateSlider(float value)
    {
        slider.GetComponent<SliderController>().UpdateSlider(value);
    }

    public void StartRunning()
    {
        GamePlayController.Instance.StartRunning();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void TopWin()
    {
        topWin.SetActive(true);
    }

    public void UpdateTopWin(Sprite top1, Sprite top2, Sprite top3)
    {
        this.top1.sprite = top1;
        this.top2.sprite = top2;
        this.top3.sprite = top3;
    }

    public void Confirm(bool isShow)
    {
        Time.timeScale = (isShow) ? 0 : 1;
        confirm.SetActive(isShow);
    }

    public void BackToMenu()
    {
        GetComponent<SceneController>().BackToMenu();
    }

    public void Restart()
    {
        GetComponent<SceneController>().Restart();
    }

    public void Quit()
    {
        GetComponent<SceneController>().Quit();
    }

    public void UpdateScore(float score)
    {
        this.score.text = FloatToMin(score);
    }

    public string FloatToMin(float value)
    {
        int min = (int)value / 60;
        return min + "m" + ((int)value - min * 60) + "s";
    }

    public void UpdateHighScore(int score)
    {
        this.highscore.text = score.ToString();
    }
}
