using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterMenu : MonoBehaviour
{
    //resources
    public Unit unit;
    public List<Unit> ally;
    public List<Unit> enemy;
    public GameObject cmdButton;

    //for navigation
    [SerializeField] List<GameObject> submenus;
    int currMenu = 0;
    int selectedCommandType;
    Command selectedCommand;
    List<Unit> selectedTargets;
    //KeyCode[] arrows = {KeyCode.LeftArrow,KeyCode.UpArrow,KeyCode.RightArrow,KeyCode.DownArrow};

    // Update is called once per frame

    public void SetCommandType(int type){
        selectedCommandType = type;
        SwitchSubmenu(true);
    }

    public void SetCommand(Command command){
        selectedCommand = command;
        SwitchSubmenu(true);
    }

    public void Action(Unit selectedUnit){
        selectedTargets = new List<Unit>{selectedUnit};
        selectedCommand.SetTargets(selectedTargets);
        selectedCommand.action();
        unit.isActing = false;
        Debug.Log(unit + " used " + selectedCommand + " on " + string.Join(", ", selectedTargets));
        Destroy(gameObject);
    }

    //if not forward, then this is being called to go backwards
    public void SwitchSubmenu(bool forward){
        submenus[currMenu].SetActive(false);
        currMenu = forward ? currMenu+1 : currMenu-1;
        submenus[currMenu].SetActive(true);
        PopulateMenu();
    }

    public void PopulateMenu(){
        switch (currMenu){
            case 1:
                GoToSubmenu(selectedCommandType);
                break;
            case 2:
                GoToTargetMenu(selectedCommand);
                break;
        }
    }

    //
    public void GoToSubmenu(int type){
        //instantiate the menu here
        //so we'd pass in the command type likely and use that to filter
        List<Command> cmdSubmenu = unit.cmdList.FindAll(
            delegate(Command command){
                return (command.commandType == (CommandType)type);
            }
        );

        int count = 0;

        foreach (Command cmd in cmdSubmenu){
            GameObject commandButton = Instantiate(cmdButton, offset(count), Quaternion.identity);
            commandButton.GetComponentInChildren<Text>().text = cmd.name;
            Command com = commandButton.AddComponent(cmd.GetType()) as Command;
            com.source = cmd.source;

            commandButton.GetComponentInChildren<Button>().onClick.AddListener(() => SetCommand(com));
            commandButton.transform.SetParent(submenus[currMenu].transform, false);
            count++;
        }
        /*
        menu.GetComponent<EncounterSubMenu>().ConfirmButton = (int)type;
        menu.GetComponent<EncounterSubMenu>().BackButton = ((int)type + 2) % 4;
        menu.transform.SetParent(this.transform.parent);
        */
    }

    public void GoToTargetMenu(Command command){
        List<Unit> pTargets = unit.GetValidTargets(command, ally, enemy);
        int count = 0;
        foreach (Unit target in pTargets){
            GameObject targetButton = Instantiate(cmdButton, offset(count), Quaternion.identity);
            targetButton.GetComponentInChildren<Button>().onClick.AddListener(() => Action(target));
            targetButton.GetComponentInChildren<Text>().text = "" + target;
            targetButton.transform.SetParent(submenus[currMenu].transform, false);
            count++;
        }
    } 

    Vector2 offset(int num){
        var xOffset = cmdButton.GetComponent<RectTransform>().rect.width*2;
        var yOffset = 20;
        if (false){
            return new Vector2(0, num*yOffset);
        }
        else{
            //Menu will be displayed horizontally instead
            return new Vector2(-160 + num*xOffset, 0);
        }
    }
}
