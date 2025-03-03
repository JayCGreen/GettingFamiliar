using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowClick : MonoBehaviour
{
    // Start is called before the first frame update
    private enum direction{
        Left,
        Right,
        Up,
        Down,
    };
    [SerializeField ] private direction keyDirection;
    private Button menuButton;
    void Start()
    {
        menuButton = this.gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (isArrowKeyDown()){
            Debug.Log("button hit " + this.gameObject);
            FadeToColor(menuButton.colors.pressedColor, menuButton.colors.fadeDuration);
            menuButton.onClick.Invoke();
        }
        else if (isArrowKeyUp()){
            FadeToColor(menuButton.colors.normalColor, menuButton.colors.fadeDuration);
        }
    }

    void FadeToColor(Color color, float dur){
        Graphic graphic = this.gameObject.GetComponent<Graphic>();
        graphic.CrossFadeColor(color, dur, true, true);

    }

    bool isArrowKeyDown(){
        switch (keyDirection)
        {
            case direction.Left:
                return (Input.GetKeyDown(KeyCode.LeftArrow));
            case direction.Right:
                return (Input.GetKeyDown(KeyCode.RightArrow));
            case direction.Up:
                return (Input.GetKeyDown(KeyCode.UpArrow));
            case direction.Down:
                return (Input.GetKeyDown(KeyCode.DownArrow));
            default:
                return false;
        }
    }

    bool isArrowKeyUp(){
        switch (keyDirection)
        {
            case direction.Left:
                return (Input.GetKeyUp(KeyCode.LeftArrow));
            case direction.Right:
                return (Input.GetKeyUp(KeyCode.RightArrow));
            case direction.Up:
                return (Input.GetKeyUp(KeyCode.UpArrow));
            case direction.Down:
                return (Input.GetKeyUp(KeyCode.DownArrow));
            default:
                return true;
        }
    }
}
