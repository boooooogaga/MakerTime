using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public bool[] drum = new bool[6];
    public int currentBullet = 0;
    // Start is called before the first frame update
    void Start()
    {
        reload();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Shot())
            {
                fire();
            }
            else
            {
                blank();
            }
        }        if(Input.GetMouseButtonDown(2))
        {
            reload();
        }        if(Input.GetMouseButtonDown(1))
        {
            spin();
        }
    }

    public bool Shot()
    {
        if (drum[currentBullet])
        {
            return true;
        }
        else
        {
            currentBullet++;
            if (currentBullet >= 6)
            {
                currentBullet = 0;
            }
            return false;
        }
    }
    public void fire()
    {
        Debug.Log("Fire");
    }
    public void blank()
    {
        Debug.Log("Blank");
    }
    public void reload()
    {
        for (int i = 0; i < drum.Length; i++)
        {
            drum[i] = Random.Range(0, 2) == 0 ? false : true;
        }
    }
    public void spin()
    {
        currentBullet = Random.Range(0, 6);
    }
}
