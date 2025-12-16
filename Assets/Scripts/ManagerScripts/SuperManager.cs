using UnityEngine;

public class SuperManager : MonoBehaviour
{
    public static GraphicManager gui_manager;
    public static GameObject player;

    //More managers below...

    void Awake()
    {
        gui_manager = GetComponent<GraphicManager>();
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
