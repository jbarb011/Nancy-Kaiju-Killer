using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    //VARIABLES
    public int min_damage = 5;              //Minimum Damage Output to Enemy
    public float min_kb = 200f;             //Minimum kickback
    public bool hitting = false;            //whether or not attack is done
    public float hit_timer = 0f;            //where attackbox duration is kept
    public float hit_cooldown = 0.2f;       //attackbox duration

    //FSM VARIABLES
    public float Charge_duration = 0.6f;
    public float Rapid_duration = 0.6f;
    public float fsm_timer = 0f;
    public int NumPressed = 0;
    public int NumRapid = 3;

    //Shared Variables
    public int damage_output;
    public float kb_output;



    //FSM STATES
    public enum PunchStates
    {
        Waiting,
        Pressed,
        Not_Pressed
    };

    public PunchStates pstate;
    
    //UNITY ATTACHERS
    public Collider2D hittrigger;           //Locate attackbox used
    public GameObject source;

    private void Awake()
    {
        // set collider to off
        hittrigger.enabled = false;
        pstate = PunchStates.Waiting;
        damage_output = min_damage;
        kb_output = min_kb;
    }

    private void Update()
    {
        Hit_FSM();

        // if hitting
        if (hitting)
        {
            // subtract timer
            if(hit_timer > 0)
            {
                hit_timer -= Time.deltaTime;
            }
            // if timers up reset everything
            else
            {
                hitting = false;
                hittrigger.enabled = false;
            }
        }
    }

    private void Hit_FSM()
    {
        //WAITING
        if (pstate == PunchStates.Waiting)
        {
            if (Input.GetButtonDown("Z"))
            {
                pstate = PunchStates.Pressed;
                NumPressed++;
            }
        }

        //PRESSED 
        else if (pstate == PunchStates.Pressed)
        {
            if (Input.GetButton("Z"))
            {
                fsm_timer += Time.deltaTime;
            }

            if (Input.GetButtonUp("Z"))
            {
                if (fsm_timer >= Charge_duration)
                {
                    Debug.Log("Charge Punch");
                    pstate = PunchStates.Waiting;
                    NumPressed = 0;
                    hit(fsm_timer*5, true);
                }
                else if (NumPressed >= NumRapid)
                {
                    Debug.Log("Rapid Punch Finisher");
                    pstate = PunchStates.Waiting;
                    NumPressed = 0;
                    hit(3.0f, true);
                }
                else
                {
                    Debug.Log(NumPressed);
                    pstate = PunchStates.Not_Pressed;
                    hit(1f, false);
                }

                fsm_timer = 0;

            }
        }

        //NOT PRESSED BUT STILL IN COMBO MODE
        else if(pstate == PunchStates.Not_Pressed)
        {
            if (Input.GetButtonDown("Z"))
            {
                NumPressed++;
                pstate = PunchStates.Pressed;
            }
            else
            {
                if(fsm_timer < Rapid_duration)
                {
                    fsm_timer += Time.deltaTime;
                }
                else
                {
                    pstate = PunchStates.Waiting;
                    NumPressed = 0;

                    Debug.Log(fsm_timer);
                    fsm_timer = 0;
                }
            }
        }

        else
        {
            pstate = PunchStates.Waiting;
        }
    }

    private void hit(float Multiplier, bool kickback_on)
    {
        int dmgMult = (int)Multiplier;
        Debug.Log(Multiplier);
        damage_output = min_damage * dmgMult;

        if (kickback_on)
        {
            kb_output = min_kb * Multiplier;
        }
        else
        {
            kb_output = 0;
        }

        hittrigger.enabled = true;
        hit_timer = hit_cooldown;
        hitting = true;
    }
    
}