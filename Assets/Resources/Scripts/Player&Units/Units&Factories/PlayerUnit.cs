using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class PlayerUnit : Unit
{
    public GameObject cmdMenu;
    public bool test = false;
    GameObject canvas;

    [SerializeField]List<Command> atkList;
    [SerializeField]List<Command> willList;
    [SerializeField]List<Command> itemList;
    [SerializeField]List<Command> speciallList;

    void Start(){
        canvas = GameObject.Find("UICanvas");
    }
    
    //Getters and Setters
    void Update(){
        if (isActing){
            //put input reader here to invoke the thing

        }
    }
    override public List<Command> GetCommandList(int submenu){
        switch (submenu){
            case 0:
                return willList;
            case 1:
                return atkList;           
            case 2:
                return itemList;
            case 3:
                return speciallList;
            //prob should error handle here
            default:
                return null;
        }
    }

    public override string ToString(){
        return "Demi";
    }

    //and then we'll create the menus for targetting here
    //Something about a Coroutine and yielding? We'll see later I suppose
    override public List<Unit> SetTarget(List<Unit> potentialTargets){
        List<Unit> t = new List<Unit>();
        t.Add(potentialTargets[0]);
        return t;
    }

    //here is where we create the menus and submenus
    override public Command SetCommand(List<Unit> ally, List<Unit> enemy){
        int selection = 0;
        //a start, next step is to actually get the menu layout in the prefab
        GameObject menu = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/EncounterMenu/EncounterMenu"));
        //menu.GetComponent<EncounterMenu>().unit = this;
        
        Debug.Log("Made it hurr");
        return cmdList[selection];
    }

    public void SetTest(){
        Debug.Log("wanka");
        test= true;
    }

    
    //version specifically for playable chareacter
    override public void Act(List<Unit> ally, List<Unit> enemy){
        isActing = true;
        //put a random gen here maybe (weighted for some units)
        int selection = 0;
        GameObject menu = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/EncounterMenu/EncounterMenu"));
        menu.transform.GetComponent<EncounterMenu>().unit = this;
        menu.transform.GetComponent<EncounterMenu>().ally = ally;
        menu.transform.GetComponent<EncounterMenu>().enemy = enemy;
        menu.transform.SetParent(canvas.transform);
    }


    IEnumerator Chill(){
        yield return new WaitForSeconds(2);
        Debug.Log("Eazy does it");
    }
}
