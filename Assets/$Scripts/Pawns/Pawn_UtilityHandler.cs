/************************************************************************************
 *  Pawn Utility class
 *      -this class is instantiated with every pawn and is used 
 *       to track the untilities and calculate actions based on them
*************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_UtilityHandler
{
    Pawn owner;                                                                                     //Pawn which owns this utility handler
    private sbyte energy;                                                                           //current energy amout
    public sbyte getEnergy
    {
        get
        {
            return energy;
        }
    }

    private sbyte sleepEnergyThreshold;                                                             //point of energy at which pawn must 'sleep'

    private sbyte restedEnergyThreshold;                                                            //point of energy at which pawn may 'awaken'

    private const byte maxEnergy = 100;                                                             //max potential energy this pawn can have, const for now [TEMP]
    public static byte getMaxEnergy
    {
        get
        {
            return maxEnergy;
        }
    }

    public Pawn_UtilityHandler(Pawn owner)                                                          //Constructor
    {                                                                                               //sets or calculates variables
        this.owner = owner;
        float t = (maxEnergy / 2) + Random.Range(0, 50);
        energy = (sbyte)t;
        sleepEnergyThreshold = 25;                                                                      
        restedEnergyThreshold = 75;
    }

    public void Tick()                                                                              //Tick is called 1/sec by pawn                                          
    {
        TickEnergy();                                                                               //Check for energy adjustments

        if(owner.getSetHasJob)
        {
            return;
        }
        else
        {
            switch (owner.pawnState)
            {
                case PawnState.Idle:
                    CalculateNextAction();
                    break;
                case PawnState.Work:
                    break;
                case PawnState.Rest:
                    break;
            }
        }
    }

    private void TickEnergy()
    {
        //set semi-random amount based on current activity                                          
        //add, subtract it based on curr activity

        sbyte energyLossOrGain = 0;
        switch (owner.pawnState)
        {                                                                                           //if pawn resting
            case PawnState.Rest:                                                                    //regain energy
                energyLossOrGain += 10;                                                             //if fully rested
                if (energy >= restedEnergyThreshold) { CalculateNextAction(); }                     //wake up
                break;

            case PawnState.Work:                                                                    //if pawn working, tire
                if (owner.pawnType == PawnType.Hauler) { energyLossOrGain -= 15; }                  //tire more if hauling
                else { energyLossOrGain -= 10; }
                break;

            case PawnState.Idle:                                                                    //if pawn idle, tire slowly
                energyLossOrGain -= 5;
                break;
        }

        energy += energyLossOrGain;                                                                 //apply energy loss/gain
    }

    public void CalculateNextAction()
    {
        //Evaluate Utilities
        //choose best suited job based on utility, 
        ParticleSystem.EmissionModule em = owner.ptl.emission;
        if (owner.pawnState == PawnState.Rest && energy > restedEnergyThreshold)                    //wake up pawn if fully rested
        {
            owner.pawnState = PawnState.Idle;
            em.enabled = false;
        }
        else if (owner.pawnState != PawnState.Rest && energy < sleepEnergyThreshold)                //Rest pawn if exhausted
        {
            owner.pawnState = PawnState.Rest;
            em.enabled = true;
        }
        if (owner.pawnState == PawnState.Idle || owner.pawnState == PawnState.Work)                 //if available to work
        {
            owner.getJobHandler.LookForJob();                                                       //Job Handler to decide whether to take job or not
        }
    }
}