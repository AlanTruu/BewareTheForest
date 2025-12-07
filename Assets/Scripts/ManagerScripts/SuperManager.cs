using UnityEngine;

public class SuperManager : MonoBehaviour
{
    public static GraphicManager gui_manager;
    
    //More managers below...
    void Start()
    {
        gui_manager = GetComponent<GraphicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
