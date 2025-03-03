using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchUnit : Unit
{
    public override string ToString(){
        return "Witchy Witchy";
    }

    //Will return a list of units
    override public List<Unit> SetTarget(List<Unit> potentialTargets){
        //put the enemy behaivors for the commands
        //trait parsing could go here as well, maybe weighted probability distribution
        List<Unit> t = new List<Unit>();
        t.Add(potentialTargets[0]);
        return t;
    }

    override public Command SetCommand(List<Unit> ally, List<Unit> enemy){
        //has the layout so can put behaivors here
        //trait parsing could go here as well, maybe weighted probability distribution
        int selection = 0;
        return cmdList[selection];
    }
}
