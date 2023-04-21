using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Wheel[] wheels;
    public Boss[] bosses;
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private AudioSource knifeHit;


    [Header(header: "Wheel settings")]
    [SerializeField] private Transform wheelSpawnPosition;
    [Range(0, 1)] [SerializeField] private float wheelScale;

    [Header(header: "knife settings")]
    [SerializeField] private Transform knifespawnPosition;
    [Range(0, 11)] [SerializeField] private float knifeScale;

    private string bossName;
    private Wheel currentWheel;
    private Knife currentKnife;

    public int TotalSpawnKnife { get; set; }
    public string BossName => bossName;
    

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

        InitializedGame();
    }

  

    private void InitializedGame()
    {
        GameManager.Instance.IsGameOver = false;
        GameManager.Instance.Score = 0;
        GameManager.Instance.Stage = 1;

        SetupGame();
    }
    private void Update()
    {
        if (currentKnife == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !currentKnife.IsReleased)
        {
            knifeHit.Play();
            currentKnife.FireKnife();
            StartCoroutine(GenerateKnife());
        }
    }

    private void SetupGame()
    {
        SpawnWheel();

        TotalSpawnKnife = 0;
        StartCoroutine(GenerateKnife());
    }

    private void SpawnWheel()
    {
        GameObject tmpWheel = new GameObject();
        if (GameManager.Instance.Stage % 5 == 0)
        {
            Boss newBoss = bosses[Random.Range(0, bosses.Length)];
            tmpWheel = Instantiate(newBoss.bossPrefab, wheelSpawnPosition.position, Quaternion.identity,wheelSpawnPosition).gameObject;
            bossName = "Boss" + newBoss.bossName;
        }
        else
        {
            tmpWheel = Instantiate(wheels[GameManager.Instance.Stage - 1], wheelSpawnPosition.position,
             Quaternion.identity, wheelSpawnPosition).gameObject;
        }

        float wheelScaleInScreen = GameManager.Instance.ScreenWidth * wheelScale / tmpWheel.GetComponent<SpriteRenderer>().bounds.size.x;
        tmpWheel.transform.localScale = Vector3.one * wheelScaleInScreen;
        currentWheel = tmpWheel.GetComponent<Wheel>();

    }

    private IEnumerator GenerateKnife()
    {
        yield return new WaitUntil(() => knifespawnPosition.childCount == 0);
        if (currentWheel.AvailableKnifes > TotalSpawnKnife && !GameManager.Instance.IsGameOver)
        {
            TotalSpawnKnife++;
            GameObject tmpknife = new GameObject();

            if (GameManager.Instance.SelectedKnifePrefab == null)
            {
                tmpknife = Instantiate(knifePrefab, knifespawnPosition.position, Quaternion.identity, knifespawnPosition).gameObject;

            }
            else
            {
                tmpknife = Instantiate(GameManager.Instance.SelectedKnifePrefab, knifespawnPosition.position, Quaternion.identity, knifespawnPosition).gameObject;
            }

            float knifeScaleInScreen = GameManager.Instance.ScreenHeight * knifeScale / tmpknife.GetComponent<SpriteRenderer>().bounds.size.y;
            tmpknife.transform.localScale = Vector3.one * knifeScaleInScreen;
            currentKnife = tmpknife.GetComponent<Knife>();
        }
    }

    public UnityEngine.Events.UnityEvent levelchange;

    public void NextLevel()
    {
        if (currentWheel != null)
        {
            currentWheel.DestoryKnife();
        }

        if (GameManager.Instance.Stage % 5 == 0)
        {
            GameManager.Instance.Stage++;
            StartCoroutine(BossDefeated());
            
        }
        else
        {
            GameManager.Instance.Stage++;
            if (GameManager.Instance.Stage % 5 == 0)
            {
                StartCoroutine(BossFight());
            }
            else
            {
                Invoke(nameof(SetupGame), 0.3f);
                levelchange.Invoke();
            }
        }
        

    }

    private IEnumerator BossFight()
    {
        StartCoroutine(UiManager.Instance.BossStart());
        yield return new WaitForSeconds(2f);
        SetupGame();
    }
    private IEnumerator BossDefeated()
    {
        StartCoroutine(UiManager.Instance.BossDefeated());
        yield return new WaitForSeconds(2f);
        SetupGame();
    }
}

[Serializable]
public class Boss
{
    public GameObject bossPrefab;
    public string bossName;
}
   


