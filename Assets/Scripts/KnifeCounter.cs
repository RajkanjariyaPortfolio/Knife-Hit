using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeCounter : MonoBehaviour
{
    
    public GameObject knifeIcon;
    public Color activeColor;
    public Color deactiveColor;
    public static KnifeCounter intance;

    List<GameObject> iconList;

    private void Start()
    {
        setUpCounter(5);   
    }
    public void setUpCounter(int totalKnife)
    {
       

        for (int i = 0; i < totalKnife; i++)
        {
            GameObject temp = Instantiate<GameObject>(knifeIcon, transform,true);
            temp.GetComponent<Image>().color = activeColor;
            iconList.Add(temp);
        }
    }
    public void setHitedKnife(int val)
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            iconList[i].GetComponent<Image>().color = Color.black;
        }
    }
    

    
   Stack<GameObject> icons = new Stack<GameObject>();

    /*
    private void Start()
    {
        
    }

    private void Awake()
    {
        var knife = GetComponentsInChildren<Image>(true);
        var rev = new Stack<GameObject>();
        foreach (var k in knife)
        {
             rev.Push(k.gameObject);
        }
        while (rev.Count != 0)
        {
            icons.Push(rev.Pop());

        }


    }

    public void ResetLevel()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            icons.Peek().GetComponent<Image>().color = Color.red;
            icons.Pop();
        }
    }
    


    */


}



