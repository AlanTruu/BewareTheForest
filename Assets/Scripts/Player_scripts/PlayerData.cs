using UnityEngine;
using UnityEngine.UI;
using EasyPeasyFirstPersonController;
using System.Collections;

public class PlayerData : MonoBehaviour, ILife
{
    private float max_health = 100f;
    private float health = 100f;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private float hunger = 100f;
    public float Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public float max_stamina = 5f;

    //References
    private Slider health_slider;
    private Slider stamina_slider;
    private FirstPersonController fps_controller;
    private CharacterController controller;
    private Canvas death_canvas;
    private ScreenFade death_fade;
    private AudioSource audio_source;
    [SerializeField] AudioClip hurt_1;


    //Logic
    private bool is_dead = false;

    void Start()
    {
        health_slider = SuperManager.gui_manager.health_slider;
        stamina_slider = SuperManager.gui_manager.stamina_slider;
        death_canvas = SuperManager.gui_manager.death_canvas;
        death_fade = SuperManager.gui_manager.screen_fade;

        fps_controller = GetComponent<FirstPersonController>();
        controller = GetComponent<CharacterController>();
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        health_slider.value = health;
        stamina_slider.value = fps_controller.stamina;
    }

    //reduce health and set the value of the health slider
    public void take_damage(float dmg, Transform source = null)
    {

        health -= dmg;
        health_slider.value = health;
        audio_source.PlayOneShot(hurt_1);

        if (health <= 0 && !is_dead)
        {
            is_dead = true;
            die();
        }
    }

    public void die()
    {
        //death_canvas.gameObject.SetActive(true);
        death_fade.fade_to_black();
        StartCoroutine(death_sequence(new Vector3(0, 0, 0))); //Insert position of camp/etc/whatever here
    }

    //Coroutine to delay player's respawn
    IEnumerator death_sequence(Vector3 spawn_location)
    {
        yield return new WaitForSeconds(1.5f);
        respawn(spawn_location);
    }

    //Should reset any of the player's stats, teleports player to respawn point
    public void respawn(Vector3 respawn_point)
    {
        //DO respawn stuff here
        health = max_health;


        controller.enabled = false;
        transform.position = respawn_point;
        controller.enabled = true;
        death_fade.fade_from_black();
        //death_canvas.gameObject.SetActive(false);
    }
}
