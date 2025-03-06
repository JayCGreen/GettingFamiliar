using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : UnitFactory
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Unit CreateUnit(){
        //instatiation would go here but not there yet so we'll just return the string of it
        GameObject player = Spawn(unit: new PlayerUnit(), stats:setStats(0), cmds: setCmdList(0));
        return player.GetComponent<PlayerUnit>();
    }

    public Dictionary<string, int> setStats(int level){
        // stat = base + bonus + g * (n-1) * (0.7025 + 0.0175 * (n-1))
        //constants don't matter, just that its quadratic and therefor will hit a threshold
        //although linear isnt the worse
        //could also just call it real with the variants
        return new Dictionary<string, int>{
            {"shield", 30},
            {"stamina", 20},
            {"will", 20},
            {"power", 20},
            {"speed", 25},
        };
    }
    
    public List<Command> setCmdList(int type){
        List<Command> cmdList = new List<Command>();
        cmdList.Add(new DoubleTapCmd());
        return cmdList;
    }
}
