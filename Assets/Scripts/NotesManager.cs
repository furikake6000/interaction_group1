using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note
{
    public NoteType type;
    public float startTime;
    public int phase;
    public bool enabled;

    public Note(NoteType type){
        this.type = type;
        this.startTime = Time.time;
        this.phase = 0;
        this.enabled = true;
    }

    public void Update(Dictionary<NoteType, AudioSource> sounds, Dictionary<NoteType, float> intervals){
        // リズム音を鳴らす
        if(Time.time >= startTime + intervals[type] * phase) {
            switch (type) {
                case NoteType.Left:
                case NoteType.Right:
                    if(phase < 2){
                        sounds[type].Play();
                    }
                    break;
                case NoteType.Top:
                    if(phase < 1){
                        sounds[type].Play();
                    }
                    break;
                case NoteType.Zigzag:
                    if(phase == 0 || phase == 2){
                        sounds[NoteType.Left].Play();
                    }else if(phase == 1 || phase == 3){
                        sounds[NoteType.Right].Play();
                    }
                    break;
            }
            phase++;
        }

        switch (type) {
            case NoteType.Left:
            case NoteType.Right:
                if(phase > 3){
                    enabled = false;
                }
                break;
            case NoteType.Top:
                if(phase > 2){
                    enabled = false;
                }
                break;
            case NoteType.Zigzag:
                if(phase > 8){
                    enabled = false;
                }
                break;
        }
    }

    public bool CheckForShake(float timeMargin, Dictionary<NoteType, float> intervals) {
        // 判定
        float slack = 999.0f;
        float elapsedTime = Time.time - startTime;

        switch (type) {
            case NoteType.Left:
            case NoteType.Right:
                slack = Mathf.Abs(elapsedTime - intervals[type] * 2);
                break;
            case NoteType.Top:
                slack = Mathf.Abs(elapsedTime - intervals[type] * 1);
                break;
            case NoteType.Zigzag:
                slack = Mathf.Min(new float[4] {
                    Mathf.Abs(elapsedTime - intervals[type] * 4),
                    Mathf.Abs(elapsedTime - intervals[type] * 5),
                    Mathf.Abs(elapsedTime - intervals[type] * 6),
                    Mathf.Abs(elapsedTime - intervals[type] * 7)
                });
                break;
        }

        if (slack < timeMargin){
            Debug.Log("OK! " + slack + " < " + timeMargin);
            return true;
        }
        Debug.Log("NG " + slack + " > " + timeMargin);
        return false;
    }
}
public enum NoteType
{
    Left,
    Right,
    Zigzag,
    Top
}

public class NotesManager : MonoBehaviour
{

    [SerializeField] float interval = 1f;
    [SerializeField] AudioSource leftPingSound, rightPingSound, topPingSound;
    [SerializeField] AudioSource hitAudio, missAudio;

    [SerializeField] float timeMargin = 0.3f;

    float elapsedTime;

    List<Note> notes = new List<Note>();

    Dictionary<NoteType, AudioSource> sounds;

    Dictionary<NoteType, float> intervals = new Dictionary<NoteType, float>(){
        {NoteType.Left, 0.5f},
        {NoteType.Right, 0.5f},
        {NoteType.Zigzag, 0.5f},
        {NoteType.Top, 1.0f}
    };

    public int missCounter, hitCounter;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
        sounds = new Dictionary<NoteType, AudioSource>(){
            {NoteType.Left, leftPingSound},
            {NoteType.Right, rightPingSound},
            {NoteType.Top, topPingSound}
        };
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Note note in notes){
            note.Update(sounds, intervals);
        }
        notes.RemoveAll(n => n.enabled == false);
    }

    public void Shake(float x, Vector3 v){
        bool isHit = false;
        foreach(Note note in notes){
            if (note.CheckForShake(timeMargin, intervals)){
                isHit = true;
            }
        }

        // 音を鳴らす
        if (isHit) {
            hitAudio.Play();
            hitCounter++;
        } else {
            missAudio.Play();
            missCounter++;
        }
    }
    public void Shake(){
        Shake(1.0f, Vector3.up);
    }

    public void SetTempo(float bpm) {
        float secondsPerBeat = 60.0f / (float)bpm;
        intervals = new Dictionary<NoteType, float>(){
            {NoteType.Left, secondsPerBeat},
            {NoteType.Right, secondsPerBeat},
            {NoteType.Zigzag, secondsPerBeat},
            {NoteType.Top, secondsPerBeat * 2.0f}
        };
    }

    public void AddNote(NoteType type){
        notes.Add(new Note(type));
    }

    public void ResetCounter() {
        missCounter = 0;
        hitCounter = 0;
    }
}
