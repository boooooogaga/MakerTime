using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    public bool[] drum = new bool[6];
    public int currentBullet = 0;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        reload();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Shot");
        }        if(Input.GetMouseButtonDown(2))
        {
            reload();
        }        if(Input.GetMouseButtonDown(1))
        {
            spin();
        }
    }

    public void Shot()
    {
        if (drum[currentBullet])
        {
            fire();
            drum[currentBullet] = false;
            currentBullet++;
            if (currentBullet >= 6)
            {
                currentBullet = 0;
            }
        }
        else
        {
            blank();
            currentBullet++;
            if (currentBullet >= 6)
            {
                currentBullet = 0;
            }
        }
    }
    public void fire()
    {   
        Debug.Log("Fire");
        anim.ResetTrigger("Fire");
        anim.SetTrigger("Fire");   
    }
    public void blank()
    {
        Debug.Log("Blank");
        anim.ResetTrigger("Blank");
        anim.SetTrigger("Blank");  
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
    public void PlaySound(int num)
    {
        AudioSource.PlayClipAtPoint(sounds[num], transform.position, 5f);
    }
}
