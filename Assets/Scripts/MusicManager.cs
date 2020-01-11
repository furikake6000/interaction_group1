using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] float bpm;
    [SerializeField] string scoreFileName;

    NotesManager notesManager;
    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        notesManager = GetComponent<NotesManager>();
        music = GetComponent<AudioSource>();

        notesManager.SetTempo(bpm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Seconds per beat
    public float Spb(){
        return 60.0f / (float)bpm;
    }
}
