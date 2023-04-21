using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = System.Random;



public class RewardController : MonoBehaviour
{
    public static RewardController Instance;

    [SerializeField] private int hourstorbeward = 6;
    [SerializeField] private int minutesTorReward;
    [SerializeField] private int secondsTorReward = 10;

    private int minReward =20;
    private int maxReward =60;

    private const string NEXT_REWARD = "RewardTime";
    public DateTime NextRewardTime => GetNextRewardTime();

   
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

   
        
    public bool CanReward()
    {
        return NextRewardTime <= DateTime.Now;
    }
    
    
    ///</summary>
    public int getRandomReward()
    {
        return UnityEngine.Random.Range(minReward, maxReward);
    }
        
    
    public void ResetRewardTine()
    {
        DateTime nextReward = DateTime.Now.Add(new TimeSpan(hourstorbeward, secondsTorReward,secondsTorReward));
        SaveNextRewardTime(nextReward);
    }

    private void SaveNextRewardTime(DateTime time)
    {
        PlayerPrefs.SetString(NEXT_REWARD, time.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private DateTime GetNextRewardTime()
    {
        string nextReward = PlayerPrefs.GetString(NEXT_REWARD, string.Empty);

        if (!string.IsNullOrEmpty(nextReward))
        {
            return DateTime.FromBinary(Convert.ToInt64(nextReward));
        }
        else
        {
            return DateTime.Now;
        }
    }



}
