using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinUnit : Unit
{

    public override string ToString(){
        return "Big Gob";
    }

    override public List<Unit> SetTarget(List<Unit> potentialTargets){
        //put the enemy behaivors for the commands
        List<Unit> t = new List<Unit>();
        t.Add(potentialTargets[0]);
        return t;
    }

    override public Command SetCommand(List<Unit> ally, List<Unit> enemy){
        //has the layout so can put behaivors here
        int selection = 0;
        return cmdList[selection];
    }
}
