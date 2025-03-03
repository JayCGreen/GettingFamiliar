using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapCmd : Command
{
    public DoubleTapCmd(){
        name = "Double Tap";
        description = "A two hit attack";
        targetType = TargetType.Enemy;
        commandType = CommandType.Atk;
    }
    
    public override bool action(){
        //put the calculation here in the action, one less method to write
        //plus this allows for different effects to do different numbers
        int val = 10;
        attackTarget(val);
        attackTarget(val);
        return true;
    }

    public override List<Unit> TargetCondition(List<Unit> units){
        //put the calculation here in the action, one less method to write
        //plus this allows for different effects to do different numbers
        return units.FindAll(
            delegate(Unit u){
                return u.stats["stamina"] > 0;
            });
    }
}
