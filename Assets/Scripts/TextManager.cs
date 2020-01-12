using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] Text t;
    [SerializeField] Text tapToNextText;

    string[] texts = new string[]{
        "SWINGへようこそ！",
        "SWINGは\nリズムにノリながら\nテニスをプレイする\nリズムゲームです",
        "ここでは\nゲームの遊び方を\n説明します",
        "スマホを\n左右に振ってみよう"
    };

    int state = -1;
    bool waitForInput = false;
    // 遷移してはいけないstateのリスト
    List<int> pauseStates = new List<int>(){
        3
    };

    // Start is called before the first frame update
    void Start()
    {
        SetNextText();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForInput && GetTouchState() && !pauseStates.Contains(state)) {
            GotoNextText();
        }
    }

    // GotoNextText() -> SetNextText() -> WaitForNextInput()
    void GotoNextText() {
        waitForInput = false;
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f,
            "to", 0.0f,
            "time", 0.5f,
            "onupdate", "SetTextAlpha",
            "oncomplete", "SetNextText"
        ));
    }
    void SetNextText() {
        state++;
        t.text = texts[state];

        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0.0f,
            "to", 1.0f,
            "time", 0.5f,
            "onupdate", "SetTextAlpha",
            "oncomplete", "WaitForNextInput"
        ));
    }
    void WaitForNextInput() {
        waitForInput = true;
    }
    void SetTextAlpha(float value) {
        t.color = new Color(t.color.r, t.color.g, t.color.b, value);
    }

    bool GetTouchState() {
        // エディターの場合
        if (Application.isEditor) {
            if (Input.GetMouseButtonDown(0)) {
                return true;
            }
            return false;
        }

        // 実機の場合
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                return true;
            }
        }
        return false;
    }
}
