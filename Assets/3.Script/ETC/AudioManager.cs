using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audio;

    [Header("UI")]
    [SerializeField] AudioClip uiMoveItem;

    [Header("ETC")]
    [SerializeField] AudioClip itemCollector;

    static public AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void MoveItem()
    {
        audio.PlayOneShot(uiMoveItem);
    }
}
