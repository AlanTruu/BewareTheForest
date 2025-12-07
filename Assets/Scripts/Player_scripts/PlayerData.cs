using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    private float health = 100f;
    private Slider health_slider;
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

    private float stamina;
    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    void Start()
    {
        health_slider = SuperManager.gui_manager.player_canvas.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //reduce health and set the value of the health slider
    public void take_damage(float dmg, string source)
    {
        
        health -= dmg;
        health_slider.value = health;
    }
}
