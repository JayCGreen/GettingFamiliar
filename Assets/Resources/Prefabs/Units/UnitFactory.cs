using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UnitFactory : MonoBehaviour
{
    public abstract Unit CreateUnit();

    public GameObject Spawn(Unit unit, Dictionary<string, int> stats, List<Command> cmds){
        //So I think in future versions we'll pass a prefab that corresponds to the unit, but for now just make an empty one
        GameObject unitObject = new GameObject();
        Unit u = unitObject.AddComponent(unit.GetType()) as Unit;
        u.SetCommandList(cmds);
        u.InitStats(stats);
        return unitObject;
    }
}
