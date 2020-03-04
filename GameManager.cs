using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private bool onPause = false;
    public Animator animator;
    public List<string> hints;
    public string rareHint;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            animator.SetTrigger("Loading");
            GameObject.Find("HintBox").GetComponent<Text>().text = hints[Random.Range(0, hints.Count)];
            SceneManager.LoadSceneAsync(StaticValues.idScene);
        }
        else
        {
            animator.SetTrigger("Loaded");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        onPause = !onPause;
        StopAllCoroutines();
        StartCoroutine(pauseEnumerator());
    }

    IEnumerator pauseEnumerator(){
        if (onPause)
        {
            for (int i = 0; i < 20; ++i)
            {
                Time.timeScale = (Time.timeScale - 0.05f) <= 0f ? 0f : Time.timeScale - 0.05f;
                if (Time.timeScale == 0)
                    break;
                yield return new WaitForSeconds(0.025f);
            }
        }
        else 
        if (!onPause)
        {
            for (int i = 0; i < 20; ++i)
            {
                Time.timeScale = (Time.timeScale + 0.05f) >= 1f ? 1f : Time.timeScale + 0.05f;
                if (Time.timeScale == 1)
                    break;
                yield return new WaitForSeconds(0.025f);
            }
        }
    }
    
}
