using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenu : MonoBehaviour {

    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 fromVector2Mouse = new Vector2(0.5f, 1.0f);
    private Vector2 centerCircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2Mouse;

    public int menuItems;
    public int currentMenuItem;
    public int oldMenuItem;

	// Use this for initialization
	void Start () {
        menuItems = buttons.Count;

        foreach (MenuButton button in buttons) {
            button.sceneImage.color = button.normalColor;
        }

        currentMenuItem = 0;
        oldMenuItem = 0;
	}
	
	// Update is called once per frame
	void Update () {
        getCurrentMenuItem();
        if (Input.GetButtonDown("Fire1")) {
            buttonAction();
        }
	}

    public void getCurrentMenuItem() {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2Mouse = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);

        float angle = (Mathf.Atan2(fromVector2Mouse.y - centerCircle.y, fromVector2Mouse.x - centerCircle.x) - Mathf.Atan2(toVector2Mouse.y - centerCircle.y, toVector2Mouse.x - centerCircle.x)) * Mathf.Rad2Deg;

        if (angle < 0) {
            angle += 360;
        }

        currentMenuItem = (int)(angle / (360 / menuItems));

        if (currentMenuItem != oldMenuItem) {
            buttons[oldMenuItem].sceneImage.color = buttons[oldMenuItem].normalColor;
            oldMenuItem = currentMenuItem;
            buttons[currentMenuItem].sceneImage.color = buttons[currentMenuItem].highlightColor;
        }
    }

    public void buttonAction() {
        buttons[currentMenuItem].sceneImage.color = buttons[currentMenuItem].pressedColor;
        if (currentMenuItem == 0) {
            Debug.Log("Button 0 pressed!");
        } else if (currentMenuItem == 1) {
            Debug.Log("Button 1 pressed!");
        } else if (currentMenuItem == 2) {
            Debug.Log("Button 2 pressed!");
        } else if (currentMenuItem == 3) {
            Debug.Log("Button 3 pressed!");
        }
    }
}

[System.Serializable]
public class MenuButton {

    public string name;
    public Image sceneImage;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.grey;
    public Color pressedColor = Color.gray;
}