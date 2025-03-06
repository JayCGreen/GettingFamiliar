using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    //location of the prefab 
    private string preFab;
    public bool isActing;
    //at some point, consider switching to an enum for the key
    public Dictionary<string, int> stats = new Dictionary<string, int>{
        {"shield", 0},
        {"stamina", 0},
        {"will", 0},
        {"power", 0},
        {"speed", 0},
    };

    public Dictionary<string, int> maxStats = new Dictionary<string, int>{
        {"shield", 0},
        {"stamina", 0},
        {"will", 0},
        {"power", 0},
        {"speed", 0},
    };
    public List<Command> cmdList;

    //Every unit has stats and will act
    abstract public List<Unit> SetTarget(List<Unit> unit);
    abstract public Command SetCommand(List<Unit> ally, List<Unit> enemy);

    public virtual List<Command> GetCommandList(int submenu){
        return cmdList;
    }

    public void SetStats(int shield, int stamina, int will, int power, int speed){
        stats["shield"] = shield;
        stats["stamina"] = stamina;
        stats["will"] = will;
        stats["power"] = power;
        stats["speed"] = speed;
    }

    //might switch to a struct later, in which case I'll want this
    public void InitStats(int shield, int stamina, int will, int power, int speed){
        maxStats["shield"] = shield;
        maxStats["stamina"] = stamina;
        maxStats["will"] = will;
        maxStats["power"] = power;
        maxStats["speed"] = speed;

        stats["shield"] = maxStats["shield"];
        stats["stamina"] = maxStats["stamina"]; 
        stats["will"] = maxStats["will"]; 
        stats["power"] = maxStats["spower"]; 
        stats["speed"] = maxStats["speed"] ;
    }

    public void InitStats(Dictionary<string, int> stats){
        maxStats = stats;
        this.stats = maxStats;
    }

    public void SetCommandList(List<Command> list){
        foreach(Command command in list){
            command.source = this;
        }
        cmdList = list; 
    }

    public List<Unit> GetValidTargets(Command command, List<Unit> ally, List<Unit> enemy) {
        //See if targets an ally, enemy or self;
        TargetType targetType = command.targetType;
        List<Unit> targets;
        //default is self
        switch(targetType){
            case TargetType.Ally:
                targets = ally;
                break;
            case TargetType.Enemy:
                targets = enemy;
                break;
            default:
                targets = new List<Unit>();
                targets.Add(this);
                break;
        }
        //get the Command's list conditions
        return command.TargetCondition(targets);
    }

    virtual public void Act(List<Unit> ally, List<Unit> enemy){
        isActing = true;
        //put a random gen here maybe (weighted for some units)
        int selection = 0;
        Command command= SetCommand(ally, enemy);
        //check target type to see what the potential targets would be
        List<Unit> pTargets = GetValidTargets(command, ally, enemy);
        //Would probably have a switch statement to determine whether to put in enemy or ally
        List<Unit> targets = SetTarget(pTargets);
        command.SetTargets(targets);
        //execute the command
        command.action();

        Debug.Log(this + " used " + command + " on " + string.Join(", ", targets));
        isActing = false;
    }

}
