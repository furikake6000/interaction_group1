using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [SerializeField] Text t;
    [SerializeField] Text tapToNextText;
    [SerializeField] MusicManager musicManager;
    [SerializeField] NotesManager notesManager;
    public AudioClip welcome;
    AudioSource audioSource;
    public AudioClip[] voices = new AudioClip[19];
    
    string[] texts = new string[]{
        "SWINGへようこそ！",
        "SWINGは\nリズムにノリながら\nテニスをプレイする\nリズムゲームです",
        "ここでは\nゲームの遊び方を\n説明します",
        "スマホを\n左右に振ってみよう",
        "いい感じ！",
        "続けて、\nリズムに合わせて\n振ってみよう",
        "カン、カン、(シュッ)\nのタイミングで\n左に振ってみよう",
        "もう一度！\nリズムに合わせて！",
        "いい感じ！",
        "次は右\nいってみよう！",
        "ポン、ポン、(シュッ)\nのタイミングで\n右に振ってみよう",
        "もう一度！\nリズムに合わせて！",
        "いい感じ！",
        "次は左右に\n連続で来るぞ！",
        "カン、ポン、カン、ポン\n(左、右、左、右)\nのタイミングで\n左右に振ってみよう",
        "もう一度！\n落ち着いて！",
        "いい感じ！",
        "それでは本番\n行ってみましょう！",
        ""
    };

    int state = -1;
    bool waitForInput = false;
    // 遷移してはいけないstateのリスト
    List<int> pauseStates = new List<int>(){
        3, 6, 10, 14, 18
    };

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetNextText();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForInput)
        {
            if (!pauseStates.Contains(state) && GetTouchState())
            {
                switch (state)
                {
                    case 7:
                        GotoText(5);
                        break;
                    case 11:
                        GotoText(9);
                        break;
                    case 15:
                        GotoText(13);
                        break;
                }
                GotoNextText();
            }

            switch (state)
            {
                case 3:
                    if (notesManager.missCounter >= 3)
                    {
                        GotoNextText();
                    }
                    break;
                case 6:
                    if (!musicManager.isPlay())
                    {
                        if (notesManager.hitCounter >= 2)
                        {
                            GotoText(8);
                        }
                        else
                        {
                            GotoNextText();
                        }
                    }
                    break;
                case 10:
                    if (!musicManager.isPlay())
                    {
                        if (notesManager.hitCounter >= 2)
                        {
                            GotoText(12);
                        }
                        else
                        {
                            GotoNextText();
                        }
                    }
                    break;
                case 14:
                    if (!musicManager.isPlay())
                    {
                        if (notesManager.hitCounter >= 8)
                        {
                            GotoText(16);
                        }
                        else
                        {
                            GotoNextText();
                        }
                    }
                    break;
              case 18:
                  if (!musicManager.isPlay()) {
                      SceneManager.LoadScene("ResultScene");
                      AccelInput.goodCounter = notesManager.hitCounter;
                  }
                  break;
            }
        }
    }

    // GotoNextText() -> SetNextText() -> WaitForNextInput()
    void GotoNextText()
    {
        waitForInput = false;
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f,
            "to", 0.0f,
            "time", 0.5f,
            "onupdate", "SetTextAlpha",
            "oncomplete", "SetNextText"
        ));
    }
    void SetNextText()
    {
        state++;
        t.text = texts[state];


        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0.0f,
            "to", 1.0f,
            "time", 0.5f,
            "onupdate", "SetTextAlpha",
            "oncomplete", "WaitForNextInput"
        ));
        audioSource.PlayOneShot(voices[state]);
    }
    void WaitForNextInput()
    {
        waitForInput = true;

        switch (state)
        {
            case 3:
                notesManager.ResetCounter();
                break;
            case 6:
                musicManager.LoadScoreFile("Tutorial1");
                break;
            case 10:
                musicManager.LoadScoreFile("Tutorial2");
                break;
            case 14:
                musicManager.LoadScoreFile("Tutorial3");
                break;
            case 18:
                musicManager.LoadScoreFile("Jumper");
                break;
        }
    }
    public void GotoText(int gotoState)
    {
        state = gotoState - 1;
        GotoNextText();
    }
    void SetTextAlpha(float value)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, value);
    }

    bool GetTouchState()
    {
        // エディターの場合
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }

        // 実機の場合
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
}
