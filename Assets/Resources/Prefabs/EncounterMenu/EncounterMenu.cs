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

    //for keyboard nav
    List<KeyCode> arrows = new List<KeyCode>{KeyCode.LeftArrow,KeyCode.UpArrow,KeyCode.RightArrow,KeyCode.DownArrow};
    KeyCode confirmButton;
    KeyCode backButton;
    int highlighted = 0;
    List<Command> cmdSubmenu;
    List<Unit> targetSubmenu;
    bool vertical;

    // Update is called once per frame
    void Update(){
        if (currMenu > 0){
            if (Input.GetKeyDown(confirmButton)){
            switch (currMenu){
                case 1:
                    SetCommand(cmdSubmenu[highlighted]);
                    break;
                case 2:
                    SetTarget(targetSubmenu[highlighted]);
                    break;
                }
                highlighted = 0;
            }
            else if (Input.GetKeyDown(backButton)){
                //if not in the first menu, this button will go back
                if (currMenu > 0){
                    highlighted = 0;
                    SwitchSubmenu(forward: false);
                }
            } 
            else{
                navigateMenu();
            }
        } else{
          //first menu navigation
          int index = arrows.FindIndex(0, arrows.Count, delegate(KeyCode keycode){
            return Input.GetKeyDown(keycode);
          });
          if(index >= 0){
            SetCommandType(index);
          }
        }
    }

    public void SetCommandType(int type){
        selectedCommandType = type;
        if (type%2 == 0){
            vertical = true;
        }
        else{
            //Menu will be displayed horizontally instead
            vertical = false;
        }
        confirmButton = arrows[type];
        backButton = arrows[type + 2 % 4];
        SwitchSubmenu(true);
    }

    public void SetCommand(Command command){
        selectedCommand = command;
        SwitchSubmenu(true);
    }

    public void SetTarget(Unit selectedUnit){
        selectedTargets = new List<Unit>{selectedUnit};
        selectedCommand.SetTargets(selectedTargets);
        Action();
    }

    public void Action(){
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
        cmdSubmenu = unit.cmdList.FindAll(
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
    }

    public void GoToTargetMenu(Command command){
        targetSubmenu = unit.GetValidTargets(command, ally, enemy);
        int count = 0;
        foreach (Unit target in targetSubmenu){
            GameObject targetButton = Instantiate(cmdButton, offset(count), Quaternion.identity);
            targetButton.GetComponentInChildren<Button>().onClick.AddListener(() => SetTarget(target));
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

    void navigateMenu(){
        int count = currMenu == 1 ? cmdSubmenu.Count : targetSubmenu.Count;
        if(vertical){
            //use up and down keys to navigate the menu
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                highlighted = mod(highlighted + 1, count);
                Debug.Log("highlight is now " + highlighted);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)){
                highlighted = mod(highlighted - 1, count);
                Debug.Log("highlight is now " + highlighted);
            }
        }else {
            //use left and right kets to navigate
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                highlighted = mod(highlighted + 1, count);
                Debug.Log("highlight is now " + highlighted);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                highlighted = mod(highlighted - 1, count);
                Debug.Log("highlight is now " + highlighted);
            }
        }
    }

    int mod(int number, int modulus){
        int div = number / modulus;
        if (number < 0) div = div - 1;
        return (number - (modulus)*div);
    }

}
