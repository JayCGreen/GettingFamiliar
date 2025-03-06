using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchHitCmd : Command
{
    public WitchHitCmd(){
        name = "Witch Hit";
        description = "A sinle hit attack";
        targetType = TargetType.Enemy;
        commandType = CommandType.Atk;
    }
    
    public override bool action(){
        //put the calculation here in the action, one less method to write
        //plus this allows for different effects to do different numbers
        int val = 2;
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
