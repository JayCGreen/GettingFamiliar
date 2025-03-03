using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [System.Serializable] public struct Comp{
        public UnitFactory typeSpawner;
        public int amount;
        public int level;
        //can add variants here too but like later
    }
    public List<Comp> composition;
    public List<Unit> units = new List<Unit>();

    // Start is called before the first frame update
    //Create the unit and then push the returned GameObject into the players unit list
    //So each encounter will have a list of UnitFactories instead of outright units.
    //On the Encounter's start, the unit's get populated
    void Start()
    {
        for (int i = 0; i < composition.Count; i++){
            Comp comp = composition[i];
            for (int j = 0; j < comp.amount; j++){
                units.Add(composition[i].typeSpawner.CreateUnit());
            }
        }
        //The units can know their enemy why not
    }

    public List<Unit> GetAliveUnits(){
        //will add logic later
        List<Unit> standingUnits = new List<Unit>();
        foreach (Unit unit in units){
            if (unit.stats["stamina"] > 0 || unit.stats["shield"] > 0){
                standingUnits.Add(unit);
            }
        }
        return standingUnits;
    }
}
