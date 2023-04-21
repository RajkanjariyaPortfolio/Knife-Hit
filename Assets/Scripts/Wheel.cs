using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private int availableKnifes;

    [SerializeField] private Sprite firstWheel;
    [SerializeField] private Sprite secondWheel;
    [SerializeField] private Sprite thirdWheel;

    [SerializeField] private bool isBoss;

    [Header(header: "Prefabs")]
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject knifePrefab;

    [Header(header: "Settings")]
    [SerializeField] private float rotationz;

    public List<Level> levels;

    [HideInInspector]
    public List<Knife> Knifes;

    private int levelIndex;

    public int AvailableKnifes => availableKnifes; 

    // Start is called before the first frame update
    void Start()
    {
        if (isBoss)
        {
            if(GameManager.Instance.Stage < 5)
            {
                GetComponent<SpriteRenderer>().sprite = firstWheel;
            }
            else if (GameManager.Instance.Stage < 5 && GameManager.Instance.Stage < 10)
            {
                GetComponent<SpriteRenderer>().sprite = secondWheel;
            }
            else if (GameManager.Instance.Stage > 10)
            {
                GetComponent<SpriteRenderer>().sprite = thirdWheel;
            }
        }

        
       
        levelIndex = UnityEngine.Random.Range(0, levels.Count);

        if(levels[levelIndex].appleChance > UnityEngine.Random.value)
        {
            SpawnApple(); 
        }

        SpawnKnifes();
    }

    private void Update()
    {
        RotateWheel();
    }

    private void RotateWheel()
    {
        transform.Rotate(0f, 0f, rotationz * Time.deltaTime);
    }
    
    private void SpawnKnifes()
    {
        foreach(float knifeAngle in levels[levelIndex].knifeAngleFromWheel)
        {
            GameObject knifeTmp = Instantiate(knifePrefab);
            knifeTmp.transform.SetParent(transform);

            SetRotationFromWheel(transform, knifeTmp.transform, knifeAngle, 0.20f, 180f);
            knifeTmp.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    private void SpawnApple()
    {
        foreach (float appleAngle in levels[levelIndex].appleAngleFromWheel)
        {
            GameObject appleTmp = Instantiate(applePrefab);
            appleTmp.transform.SetParent(transform);

            SetRotationFromWheel(transform, appleTmp.transform, appleAngle, 0.25f, 0f);
            appleTmp.transform.localScale =  Vector3.one;
        }
    }
   
    public void SetRotationFromWheel(Transform wheel,Transform objectToplace, float angle, float spaceFromObject, float objectRotaion)
    {
        Vector2 offset = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (wheel.GetComponent<CircleCollider2D>().radius + spaceFromObject);
        objectToplace.localPosition = (Vector2)wheel.localPosition + offset;
        objectToplace.localRotation = Quaternion.Euler(0, 0, -angle + objectRotaion);
        
    }

    public void KnifeHit(Knife Knife)
    {
        Knife.rb.isKinematic = true;
        Knife.rb.velocity = Vector2.zero;
        Knife.transform.SetParent(transform);
        Knife.Hit = true;
         Knifes.Add(Knife);

        if (Knifes.Count >= AvailableKnifes)
        {
            LevelManager.Instance.NextLevel();
        }
        GameManager.Instance.Score++;

       
    }

    public void DestoryKnife()
    {
        foreach (var knife in Knifes)
        {
            Destroy(knife.gameObject);
        }

        Destroy(gameObject);
    }

}

[Serializable]
public class Level
{
    [Range(0, 1)] [SerializeField] public float appleChance;

    public List<float> appleAngleFromWheel = new List<float>();
    public List<float> knifeAngleFromWheel = new List<float>();

}


