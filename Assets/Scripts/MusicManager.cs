﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int bpm;
    public string resource;
    public float length;
    public NoteData[] notes;
}
[System.Serializable]
public class NoteData
{
    public float time;
    public string type;
    public bool enabled;

    static Dictionary<string, NoteType> noteTypeDic = new Dictionary<string, NoteType>(){
        {"left", NoteType.Left},
        {"right", NoteType.Right},
        {"zigzag", NoteType.Zigzag},
        {"top", NoteType.Top}
    };

    public NoteData(){
        enabled = false;
    }

    public NoteType Type()
    {
        return noteTypeDic[type];
    }
}

public class MusicManager : MonoBehaviour
{
    [SerializeField] float bpm;

    NotesManager notesManager;
    AudioSource music;
    ScoreData scoreData;

    // Start is called before the first frame update
    void Start()
    {
        notesManager = GetComponent<NotesManager>();
        music = GetComponent<AudioSource>();

        // LoadScoreFile("Tutorial1");
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreData == null) return;

        float beatTime = music.time * bpm / 60.0f;

        foreach (NoteData nd in scoreData.notes){
            if(nd.enabled == false && beatTime >= nd.time){
                notesManager.AddNote(nd.Type());
                nd.enabled = true;
            }
        }

        if (beatTime > scoreData.length){
            music.Stop();
        }
    }

    public void LoadScoreFile(string scoreFileName){
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load("Scores/" + scoreFileName, typeof(TextAsset) )as TextAsset;
        string scoreDataText = textasset.text; 

        scoreData = JsonUtility.FromJson<ScoreData>(scoreDataText);

        notesManager.SetTempo(scoreData.bpm);
        music.clip = Resources.Load("Music/" + scoreData.resource, typeof(AudioClip)) as AudioClip;

        music.Play();
    }

    // Seconds per beat
    public float Spb(){
        return 60.0f / (float)bpm;
    }

    public bool isPlay() {
        return music.isPlaying;
    }
}
