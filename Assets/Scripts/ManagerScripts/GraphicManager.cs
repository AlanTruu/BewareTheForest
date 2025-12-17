using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    //This class should hold most if not all Canvases.


    //Reference to player canvas which holds the health bar and stamina bar
    //As well as the health/stamina bar references for easy access
    [SerializeField] public Canvas player_canvas;
    [SerializeField] public Slider health_slider;
    [SerializeField] public Slider stamina_slider;
    [SerializeField] public Canvas death_canvas;
    [SerializeField] public ScreenFade screen_fade;

    void Awake()
    {
        death_canvas.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
