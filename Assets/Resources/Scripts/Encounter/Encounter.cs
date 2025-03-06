using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public struct Fighter{
        public Fighter(Player team, Player opp, Unit unit){
            this.team = team;
            this.opp = opp;
            this.unit = unit;
        }
        public Player team;
        public Player opp;
        public Unit unit;        
    }
    // Start is called before the first frame update
    public Player player;
    public Player enemy;
    List<Fighter> aliveUnits;
    List<Fighter> allUnits;
    int roundCount=0;
    Unit actingUnit;
    Command actingCommand;
    int walker = 0;
    bool isOver;
    void Start()
    {
        foreach(Unit unit in player.GetAliveUnits()){
            allUnits.Add(new Fighter(player, enemy, unit));
        }
        foreach(Unit unit in enemy.GetAliveUnits()){
            allUnits.Add(new Fighter(enemy, player, unit));
        }   
    }
    // Update is called once per frame
    void Update()
    {
        //Wait until action is completed before going to the next
        if((!(actingUnit is null) && actingUnit.isActing) || isOver){
            return;
        }
        //have the unit at the top of the order act until the list is empty, then fill up the order again 
        if(aliveUnits == null || aliveUnits.Count == walker){
            StartRound();
            walker = 0;
        }
        else{
            //
            Fighter u = aliveUnits[walker];
            actingUnit = u.unit;
            actingUnit.isActing = true;
            actingUnit.Act(u.team.units, u.opp.units);
            walker++;
            isOver = isEncounterOver();
        }
    }

    //O(NlogN) where is the total number of alive units across both sides
    //fully its O(NlogN + 2N)
    void StartRound(){
        Debug.Log("New round");
        //increment the round counter
        roundCount++;
        //get the units fromt the enemy and the player(modify later to be the alive units)
        aliveUnits = new List<Fighter>();
        foreach(Unit unit in player.GetAliveUnits()){
            aliveUnits.Add(new Fighter(player, enemy, unit));
        }
        foreach(Unit unit in enemy.GetAliveUnits()){
            aliveUnits.Add(new Fighter(enemy, player, unit));
        }
        //sort them
        mergeSort(aliveUnits, 0, aliveUnits.Count-1);
    }
    //sort the alive units with a merge sort
    // O(N logN), won't be dealing with any cases longer than 6 but still difference between, 36 and 15, multiplied over # of rounds
    //l is the left start and r is the right end
    void mergeSort(List<Fighter> units, int l, int r){
        if(l < r){
            int m = (l+r)/2;

            //sort the halves
            mergeSort(units, l, m);
            mergeSort(units, m+1, r);

            //merge the sorted halves
            merge(units, l, m, r);
        }
    }

    void merge(List<Fighter> units, int l, int m, int r){

        List<Fighter> left = new List<Fighter>();
        List<Fighter> right = new List<Fighter>();

        for(int i = l; i < m+1; i++){
            left.Add(units[i]);
        }
        for(int j = m+1; j <= r; j++){
            right.Add(units[j]);
        }

        int il = 0;
        int ir = 0;
        int index = l;
        //be sure to come back to move the stats inside
        while(il < left.Count && ir < right.Count){
            if(left[il].unit.stats["speed"] >= right[ir].unit.stats["speed"]){
                units[index] = left[il];
                il++;
            }
            else {
                units[index] = right[ir];
                ir++;
            }
            index++;
        }

        while(il < left.Count){
            units[index] = left[il];
            index++;
            il++;
        }
        while(ir < right.Count){
            units[index] = right[ir];
            index++;
            ir++;
        }
    }

    bool isEncounterOver(){
        if(player.GetAliveUnits().Count > 0 && enemy.GetAliveUnits().Count > 0){
            return false;
        }
        else{
            Debug.Log("Encounter is over after " + roundCount +  " rounds");
            return true;
        }
    }
}
