using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchFactory : UnitFactory
{
    //Well here we could actually just make new GameObjects and attach the appropriate components
    //issues come when we et into images right
    public override Unit CreateUnit(){
        GameObject witch = Spawn(unit: new WitchUnit(), stats:setStats(0), cmds: setCmdList(0)) as GameObject;
        return witch.GetComponent<WitchUnit>();
    }

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
            {"speed", 20},
        };
    }

    //commands based off of the subtype
    //at the moment, variation not added yet, that's a later thing
    public List<Command> setCmdList(int type){
        List<Command> cmdList = new List<Command>();
        cmdList.Add(new WitchHitCmd());
        return cmdList;
    }
}
