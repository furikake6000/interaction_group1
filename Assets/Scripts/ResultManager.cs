using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject scorePerfectObj = null;
    public GameObject scoreGoodObj = null;
    public GameObject scoreMissObj = null;
    // Start is called before the first frame update
    void Start()
    {
        int scorePerfect = AccelInput.getPerfect();
        int scoreGood = AccelInput.getGood();
        int scoreMiss = AccelInput.getMiss();
        Text scorePerfectText = scorePerfectObj.GetComponent<Text> ();
        Text scoreGoodText = scoreGoodObj.GetComponent<Text> ();
        Text scoreMissText = scoreMissObj.GetComponent<Text> ();
        scorePerfectText.text = scorePerfect.ToString();
        scoreGoodText.text = scoreGood.ToString();
        scoreMissText.text = scoreMiss.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
