using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [Header (header: "UI Settings")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text stageText;
    [SerializeField] private GameObject stageContainer;
    [SerializeField] private Color stagecompletedColor;
    [SerializeField] private Color stageNormalColor;

    public List<Image> stageIcons;

    [Header (header:"UI BOSS")]
    [SerializeField] private GameObject bossFight;
    [SerializeField] private GameObject boasDefeated;

    [Header(header: "Gemeover UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverScore;
    [SerializeField] private Text gameOverStage;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Update()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        gameOverScore.text = GameManager.Instance.Score.ToString();

        stageText.text = "Stage" + GameManager.Instance.Stage;
        gameOverStage.text = "Stage" + GameManager.Instance.Stage;
        
        UpdateUI();
    }

    public IEnumerator BossStart()
    {
        bossFight.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossFight.SetActive(false);
    }
    public IEnumerator BossDefeated()
    {
        boasDefeated.SetActive(true);
        yield return new WaitForSeconds(1f);
        boasDefeated.SetActive(false);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        stageContainer.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    private void UpdateUI()
    {
        if(GameManager.Instance.Stage % 5 == 0)
        {
            foreach (var icon in stageIcons)
            {
                icon.gameObject.SetActive(false);

                stageIcons[stageIcons.Count - 1].color = Color.white;
                stageText.text = "Boss" + LevelManager.Instance.BossName;
            }
        }
        else
        {
            for(int i = 0; i < stageIcons.Count; i++)
            {
                stageIcons[i].gameObject.SetActive(true);
                stageIcons[i].color = GameManager.Instance.Stage % 5 <= i ? Color.white : stagecompletedColor;
            }
        }
    }

}
