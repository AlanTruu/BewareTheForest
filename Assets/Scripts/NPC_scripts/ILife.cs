using UnityEngine;

public interface ILife 
{

    public void take_damage(float damage, Transform source = null) 
    {
        
    }

    public void die()
    {
        
    }

    public bool is_alive()
    {
        return true;
    }

}
