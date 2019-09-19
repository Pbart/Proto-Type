using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip TapSound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        TouchEvents.OnSingleTouch += PlayTapSound;        
    }

    private void PlayTapSound(Touch touch)
    {
        source.PlayOneShot(TapSound, 0.2f);
    }
}
