using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterSubMenu : MonoBehaviour
{
    //if not vertical, then build horizontal
    bool vertical;
    [SerializeField] Unit unit;
    public List<Command> cmdList;
    [SerializeField] GameObject cmdButton;
    public GameObject mainMenu;
    public int ConfirmButton;
    public int BackButton;

    //public Button b1;
    KeyCode[] arrows = {KeyCode.LeftArrow,KeyCode.UpArrow,KeyCode.RightArrow,KeyCode.DownArrow};
    List<GameObject> buttonList = new List<GameObject>();
    int highlightedCmd = 0;
    [SerializeField] Command selectedCommand;
    
    // Start is called before the first frame update
    void Start()
    {
        if (ConfirmButton%2 == 0){
            vertical = true;
        }
        else{
            //Menu will be displayed horizontally instead
            vertical = false;
        }

        //on start go through each cmd and make a button corresponding to it
        // at one point it'll be a list of commands, but that's later down the line
        //b1.onClick.AddListener(OnCommandClick);
        //cmdButton.GetComponent<Button>().onClick.AddListener(()=>OnCommandClick());
        int count = 0;
        foreach (var cmd in cmdList){
            Debug.Log("WE here with " + cmd);
            GameObject command = Instantiate(cmdButton, offset(count), Quaternion.identity);
            Command com = command.AddComponent(cmd.GetType()) as Command;
            com.source = cmd.source;
            command.GetComponentInChildren<Button>().onClick.AddListener(() => SelectCommand(com));
            command.transform.SetParent(this.transform, false);
            count++;
        }
        //
        
    }

    public void OnCommandClick(){
        Debug.Log("Hey this is a button");
    }

    Vector2 offset(int num){
        var xOffset = 100;
        var yOffset = 20;
        if (ConfirmButton%2 == 0){
            return new Vector2(0, num*yOffset);
        }
        else{
            //Menu will be displayed horizontally instead
            return new Vector2(num*xOffset, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(arrows[ConfirmButton])){
            Debug.Log("Test Success, Button Confirmed" + cmdList[highlightedCmd]);
            //cmdList[hightlighted].execute or whatever it is I'll call doing the action
            //the button variant would be more work actually
            mainMenu.GetComponent<EncounterMenu>().unit.isActing=false;
            Debug.Log(mainMenu.GetComponent<EncounterMenu>().unit.isActing);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(arrows[BackButton])){
            Debug.Log("Going Back to Menu");
            mainMenu.SetActive(true);
            Destroy(gameObject);
        } 
        else{
            navigateMenu();
        }
    }

    void navigateMenu(){
        if(vertical){
            //use up and down keys to navigate the menu
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                highlightedCmd = mod(highlightedCmd + 1, cmdList.Count);
                Debug.Log("highlight is now " + highlightedCmd);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)){
                highlightedCmd = mod(highlightedCmd - 1, cmdList.Count);
                Debug.Log("highlight is now " + highlightedCmd);
            }
        }else {
            //use left and right kets to navigate
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                highlightedCmd = mod(highlightedCmd + 1, cmdList.Count);
                Debug.Log("highlight is now " + highlightedCmd);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                highlightedCmd = mod(highlightedCmd - 1, cmdList.Count);
                Debug.Log("highlight is now " + highlightedCmd);
            }
        }
    }

    int mod(int number, int modulus){
        int div = number / modulus;
        if (number < 0) div = div - 1;
        return (number - (modulus)*div);
    }

    void SelectCommand(Command command){
        selectedCommand = command;
        //Likely what we'd want to do is deal with the targetting 
        //Need to bring in the enemies and allies
        //Feels like a hassle to track them this deep but oh well
        
    }

    void SelectTarget(List<Unit> targets){
        selectedCommand.SetTargets(targets);
    }
}
