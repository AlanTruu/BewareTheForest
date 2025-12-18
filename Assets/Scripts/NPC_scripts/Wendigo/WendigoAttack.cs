using UnityEngine;

public class WendigoAttack : IState
{
    private Wendigo _wendigo;
    private Animator _animator;
    private GameObject player;

    public float attack_cd = 2f;
    public float attack_counter = 0f;
    public float attack_radius = 1f;
    public float attack_range = 6f;
    public float attack_enter_range = 5f;
    public float attack_exit_range = 6.5f; // MUST be larger

    public WendigoAttack(Wendigo wendigo)
    {
        _wendigo = wendigo;
        _animator = wendigo.animator;
        player = SuperManager.player;
    }


    public void Tick()
    {
        float distance = Vector3.Distance(
            _wendigo.transform.position,
            player.transform.position
        );

        if (distance > attack_exit_range)
        {
            _wendigo.switch_state(_wendigo.wendigo_chase);
            return;
        }

        attack_counter -= Time.deltaTime;

        if (attack_counter <= 0f)
        {
            attack();
            attack_counter = attack_cd;
        }
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void attack()
    {
        _animator.SetTrigger("attack");
        Vector3 origin = _wendigo.transform.position;
        Vector3 direction = _wendigo.transform.forward;

        Debug.Log("attacking...");


        RaycastHit[] hits = Physics.SphereCastAll(origin, attack_radius, direction, attack_range, _wendigo.detectable_layer);

        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(origin, hit.point, Color.yellow);
            Debug.DrawRay(hit.point, hit.normal, Color.cyan);

            ILife life = hit.collider.GetComponent<ILife>();

            if (life != null)
            {
                life.take_damage(100f, _wendigo.transform);
            }
        }

    }

}
