using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class GameManager
{
    [SerializeField]
    private AudioSource srcGood;
    [SerializeField]
    private AudioSource srcBad;
    
    private Image GetArrowImage()
    {
        return CurrentArrow().GetComponentInChildren<Image>();
    }

    private IEnumerator SetArrowGood()
    {
        CurrentArrow().GetComponent<Animator>().SetTrigger("Good");
        StartCoroutine(AddKpsThread());
        srcGood.Play();
        yield break;
    }

    private IEnumerator SetArrowBad()
    {        
        CurrentArrow().GetComponent<Animator>().SetTrigger("Bad");
        StartCoroutine(SubScore());
        srcBad.Play();
        yield break;
    }

    private void SetArrowCurrent()
    {
        GetArrowImage().color = Color.gray;
    }
    
}
