using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

abstract public class Command : MonoBehaviour
{

    public CommandType commandType;

    public Unit source;
    public List<Unit> targets;
    public TargetType targetType;

    //public int commandType;
    //have a command type, for both parsing of the main characters as well as enemy
    public string name;
    public string description;

    public abstract bool action();

    //instead of target type, have target condition
    //eh keep the type
    public abstract List<Unit> TargetCondition(List<Unit> units);

    public void attackTarget(int dmg){
        //if we're going to add a defense stat, that would go here
        foreach (Unit target in targets){
            if (target.stats["shield"] > 0){
                target.stats["shield"] = Math.Max(target.stats["shield"] - dmg, 0);
            }
            else{
                target.stats["stamina"] = Math.Max(target.stats["stamina"] - dmg, 0);
            }
        }
    }

    public void healTarget(int heals){
        foreach (Unit target in targets){
            target.stats["stamina"] = Math.Min(target.stats["stamina"] + heals, target.maxStats["stamina"]);
        }
    }
    //inflinct status ailment;
    public void buffTarget(){

    }

    public void SetTargets(List<Unit> targets){
        this.targets = targets;
    }

    public override string ToString(){
        return name;
    }

}
