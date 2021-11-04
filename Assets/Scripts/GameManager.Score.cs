using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public partial class GameManager
{
   [SerializeField] private TMP_Text scoreText;

   [SerializeField] private TMP_Text maxKpsText;
   [SerializeField] private TMP_Text accText;
   [SerializeField] private TMP_Text ratioText;

   [SerializeField]
   private int score;

   private int _goods;
   private int _bads;

   public int Score
   {
      get => score;
      set
      {
         score = value;
         scoreText.text = score.ToString();
      }
   }
   [SerializeField] private TMP_Text kpsText;
   
   [SerializeField]
   private int kps;
   
   [SerializeField]
   private int maxKps = 0;

   public int Kps
   {
      get => kps;
      set
      {
         kps = value;
         maxKps = Math.Max(kps, maxKps);
         kpsText.text = kps.ToString();
         srcGood.pitch = Math.Min(2f, 1f + kps / 100f);
      }
   }

   private IEnumerator AddKpsThread()
   {
      Kps += 1;
      StartCoroutine(AddScore());
      yield return new WaitForSeconds(1);
      Kps -= 1;
   }

   private IEnumerator AddScore()
   {
      _goods++;
      for (int i = 0; i < kps; i++)
      {
         Score += 1;
         yield return new WaitForSeconds(Time.deltaTime);
      }
   }

   private IEnumerator SubScore()
   {
      _bads++;
      for (int i = 0; i < kps; i++)
      {
         Score -= 1;
         yield return new WaitForSeconds(Time.deltaTime);
      }
   }

   private void SetStats()
   {
      maxKpsText.text = maxKps.ToString();
      accText.text = (100 * _goods / (_goods + _bads)).ToString("##0") + "%";
      ratioText.text = _goods + "/" + (_goods + _bads);
   }
}
