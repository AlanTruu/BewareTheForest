using UnityEngine;
using UnityEngine.UI;
using EasyPeasyFirstPersonController;

public class PlayerData : MonoBehaviour, ILife
{
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

    void Start()
    {
        health_slider = SuperManager.gui_manager.health_slider;
        stamina_slider = SuperManager.gui_manager.stamina_slider;
        fps_controller = GetComponent<FirstPersonController>();
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
    }

    public void die()
    {

    }
}
