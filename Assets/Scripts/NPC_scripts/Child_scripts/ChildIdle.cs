using UnityEngine;

public class ChildIdle : IState
{
    private CryingChild _child;
    private GameObject _player;
    private float rotation_speed = 180f;

    public ChildIdle(CryingChild child)
    {
        _child = child;
        _player = SuperManager.player;
    }

    public void Tick()
    {
        if (_player)
        {
            Vector3 pos = _player.transform.position - _child.transform.position;

            pos.y = 0;

            Quaternion target = Quaternion.LookRotation(pos);

            if (Quaternion.Angle(_child.transform.rotation, target) > 2f)
            {
                _child.transform.rotation = Quaternion.RotateTowards(_child.transform.rotation, target, rotation_speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(_player.transform.position, _child.transform.position) < 2f)
            {
                Debug.Log("Pressed E!");
                _child.switch_state(_child.child_follow);
            }

        }
    }
    public void OnEnter()
    {

    }
    public void OnExit()
    {

    }
}
