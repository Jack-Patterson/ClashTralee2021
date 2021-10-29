using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour, IHealth
{
    // I referenced the Cannon script when writing as I wan't 100% certain on how to write some of this. Also did so for consistency between the scripts in cases such as variable names, timing, dps etc
    enum building_states { Idle, Attack, Death }
    int DPS = 110; // COC Archer Tower does slightly less damage but is faster
    float attack_time_interval = 0.5f;
    float Range = 7.0f;
    float attack_timer;
    building_states my_state = building_states.Idle;

    private int MHP = 1000, CHP = 1000, _level = 0;
    CharacterScript current_target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (my_state)
        {

            case building_states.Idle:

                if ((current_target) && within_attack_range(current_target))
                {
                    my_state = building_states.Attack;
                    attack_timer = 0;
                }

                break;

            case building_states.Attack:

                if (attack_timer <= 0)
                {
                    current_target.takeDamage((int)((float)DPS * attack_time_interval));
                    attack_timer = attack_time_interval;

                  while(within_attack_range(current_target))
                    {
                        Vector3 from_me_to_Character = current_target.transform.position - transform.position;
                        Vector3 direction = from_me_to_Character.normalized;
                        transform.forward = direction;
                    }
                }

                attack_timer -= Time.deltaTime;

                break;


            case building_states.Death:


                break;



        }





        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            current_target = FindObjectOfType<CharacterScript>();
            if (current_target)
            {
                assign_target(current_target);

            }
        }
    }

    public void assign_target(CharacterScript current_target)
    {
        if (my_state == building_states.Idle)
        {
            Vector3 from_me_to_Character = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_Character.normalized;
            transform.forward = direction;
        }
    }

    private bool within_attack_range(CharacterScript current_target)
    {
        return (Vector3.Distance(transform.position, current_target.transform.position) < Range);
    
    }

    public void repair(int v)
    {
        
    }

    public void takeDamage(int v)
    {
        
    }
}
