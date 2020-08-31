using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealth : MonoBehaviour
{
    [Header("stuff")]
    public RectTransform bossHearts;
    public GameObject[] hearts;

    private BossBattle bossHealth;
    // Start is called before the first frame update
    void Start()
    {
        bossHearts.anchoredPosition = new Vector2(40f, 0);
        bossHealth = FindObjectOfType<BossBattle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.bossBattle)
        {
            bossHealth = FindObjectOfType<BossBattle>();
            Move_Boss_Hearts();

            if(bossHealth.health == 3)
            {
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
            }
            else if(bossHealth.health == 2)
            {
                hearts[0].SetActive(false);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
            }
            else if(bossHealth.health == 1)
            {
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(true);
            }
            else
            {
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
            }
           
        }
        else
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
            bossHearts.anchoredPosition = Vector2.Lerp(bossHearts.anchoredPosition, new Vector2(40f, 0), 5f * Time.deltaTime);
        }
    }
    public void Move_Boss_Hearts()
    {
        bossHearts.anchoredPosition = Vector2.Lerp(bossHearts.anchoredPosition, new Vector2(-37.5f, 0), 5f * Time.deltaTime);
    }
}
