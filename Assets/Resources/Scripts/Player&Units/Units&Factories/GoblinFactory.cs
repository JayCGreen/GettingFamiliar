using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinFactory : UnitFactory
{

    public override Unit CreateUnit(){
        GameObject goblin = Spawn(unit: new GoblinUnit(), stats:setStats(0), cmds: setCmdList(0));
        return goblin.GetComponent<GoblinUnit>();
    }

    //Here we would also handle the stat - level relationship, maybe with level as a parameter of the 
    //level and amount, instead of handling the loop being in the
    //loop being up a level allows for easy pushing though, questions for later
    // could also pull an ff and just use variants too
    public Dictionary<string, int> setStats(int level){
        // stat = base + bonus + g * (n-1) * (0.7025 + 0.0175 * (n-1))
        //constants don't matter, just that its quadratic and therefor will hit a threshold
        //although linear isnt the worse
        //could also just call it real with the variants
        return new Dictionary<string, int>{
            {"shield", 0},
            {"stamina", 20},
            {"will", 20},
            {"power", 20},
            {"speed", 15},
        };
    }

    public List<Command> setCmdList(int type){
        List<Command> cmdList = new List<Command>();
        cmdList.Add(new GoblinHitCmd());
        return cmdList;
    }
}
