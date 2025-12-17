using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private AudioSource audio_source;
    [SerializeField] AudioClip walk_grass;
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = walk_grass;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            audio_source.enabled = true;

            audio_source.pitch = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        }
        else
        {
            audio_source.enabled = false;
        }
    }
}
