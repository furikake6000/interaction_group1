using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int bpm;
    public NoteData notes;
}
[System.Serializable]
public class NoteData
{
    public float time;
    public string type;

    static Dictionary<string, NoteType> noteTypeDic = new Dictionary<string, NoteType>(){
        {"left", NoteType.Left},
        {"right", NoteType.Right},
        {"zigzag", NoteType.Zigzag},
        {"top", NoteType.Top}
    };

    public NoteType Type()
    {
        return noteTypeDic[type];
    }
}

public class MusicManager : MonoBehaviour
{
    [SerializeField] float bpm;
    [SerializeField] string scoreFileName;

    NotesManager notesManager;
    AudioSource music;
    ScoreData scoreData;

    // Start is called before the first frame update
    void Start()
    {
        notesManager = GetComponent<NotesManager>();
        music = GetComponent<AudioSource>();

        LoadScoreFile();

        notesManager.SetTempo(bpm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadScoreFile(){
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load("Scores/" + scoreFileName, typeof(TextAsset) )as TextAsset;
        string scoreDataText = textasset.text; 

        scoreData = JsonUtility.FromJson<ScoreData>(scoreDataText);
        Debug.Log(scoreDataText);
        Debug.Log(scoreData.bpm);
    }

    // Seconds per beat
    public float Spb(){
        return 60.0f / (float)bpm;
    }
}
