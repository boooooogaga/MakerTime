using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject DeathScreen;
    [SerializeField] public TMP_Text hpText;
    [SerializeField] public CharacterControl player;
    [SerializeField] public Animation hpAnim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = player.health.ToString();
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
    }
}
