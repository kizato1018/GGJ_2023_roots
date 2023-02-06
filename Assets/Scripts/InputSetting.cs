using UnityEngine;
using System.Collections.Generic;
using Rewired;
using UnityEngine.UI;

public class InputSetting : MonoBehaviour
{
    public GameObject CheckInputPage;
    public List<Toggle> playerKeyboardToggles;
    public List<Toggle> playerJotstickToggles;
    public ToggleGroup p1InputStateToggleGroup;


    void Start()
    {
        Rewired.Player player3 = ReInput.players.GetPlayer(2);
        Rewired.Player player4 = ReInput.players.GetPlayer(3);
        playerJotstickToggles[2].isOn = player3.controllers.joystickCount > 0;
        playerJotstickToggles[3].isOn = player4.controllers.joystickCount > 0;
    }

    private void Update()
    {
        if (!ReInput.isReady) return;
        //AssignJoysticksToPlayers();
    }

    private void AssignJoysticksToPlayers()
    {

        // Check all joysticks for a button press and assign it tp
        // the first Player foudn without a joystick
        IList<Joystick> joysticks = ReInput.controllers.Joysticks;
        for (int i = 0; i < joysticks.Count; i++)
        {

            Joystick joystick = joysticks[i];
            if (ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id)) continue; // joystick is already assigned to a Player

            // Chec if a button was pressed on the joystick
            if (joystick.GetAnyButtonDown())
            {

                // Find the next Player without a Joystick
                Rewired.Player player = FindPlayerWithoutJoystick();
                if (player == null) return; // no free joysticks

                // Assign the joystick to this Player
                player.controllers.AddController(joystick, false);
            }
        }

        // If all players have joysticks, enable joystick auto-assignment
        // so controllers are re-assigned correctly when a joystick is disconnected
        // and re-connected and disable this script
        if (DoAllPlayersHaveJoysticks())
        {
            ReInput.configuration.autoAssignJoysticks = true;
            this.enabled = false; // disable this script
        }
    }

    // Searches all Players to find the next Player without a Joystick assigned
    private Rewired.Player FindPlayerWithoutJoystick()
    {
        IList<Rewired.Player> players = ReInput.players.Players;
        for (int i = 0; i < players.Count; i++)
        {
            //if (i == 0)
            //{
            //    p1InputState.isOn = players[i].controllers.hasKeyboard;
            //}
            if (players[i].controllers.joystickCount > 0) continue;
            if (players[i].controllers.hasKeyboard) continue;
            //if (players[i].controllers.hasKeyboard) players[i].controllers.hasKeyboard = false;
            return players[i];
        }
        return null;
    }

    private bool DoAllPlayersHaveJoysticks()
    {
        return FindPlayerWithoutJoystick() == null;
    }

    public void OpenSettingPage()
    {
        GameManager.instance.Pause();
        CheckInputPage.SetActive(true);
    }

    public void CloseSettingPage()
    {
        GameManager.instance.Resume();
        CheckInputPage.SetActive(false);
    }

    public void AssignJoystickToPlayer1(bool value)
    {
        assignJoystickToPlayer(0, value);
    }
    public void AssignJoystickToPlayer2(bool value)
    {
        assignJoystickToPlayer(1, value);
    }
    public void AssignJoystickToPlayer3(bool value)
    {
        assignJoystickToPlayer(2, value);
    }
    public void AssignJoystickToPlayer4(bool value)
    {
        assignJoystickToPlayer(3, value);
    }

    private void assignJoystickToPlayer(int playerID, bool value)
    {
        if (value)
            assignJoystickToPlayerWithID(playerID);
        else
            removeJoystickToPlayerWithID(playerID);
        Debug.Log("assignJoystickToPlayer id = " + playerID + " isOn = " + value);
    }

    public void AssignKeyboardToPlayer1(bool value)
    {
        assignKeyboardToPlayer(0, value);
    }
    public void AssignKeyboardToPlayer2(bool value)
    {
        assignKeyboardToPlayer(1, value);
    }
    public void AssignKeyboardToPlayer3(bool value)
    {
        assignKeyboardToPlayer(2, value);
    }
    public void AssignKeyboardToPlayer4(bool value)
    {
        assignKeyboardToPlayer(3, value);
    }

    private void assignKeyboardToPlayer(int playerID, bool value)
    {
        if (value)
            assignKeyboardToPlayerWithID(playerID);
        else
            removeKeyboardToPlayerWithID(playerID);
        Debug.Log("assignKeyboardToPlayer id = " + playerID + " value = " + value + " isOn = " + playerKeyboardToggles[playerID].isOn);
    }

    private bool assignJoystickToPlayerWithID(int playerID)
    {
        IList<Joystick> joysticks = ReInput.controllers.Joysticks;
        for (int i = 0; i < joysticks.Count; i++)
        {
            Joystick joystick = joysticks[i];
            if (ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id)) continue; // joystick is already assigned to a Player

            // Find the next Player without a Joystick
            Rewired.Player player = ReInput.players.GetPlayer(playerID);
            if (player == null) return false; // no player
            if (player.controllers.joystickCount > 0) return false;
            // Assign the joystick to this Player
            player.controllers.AddController(joystick, false);
            //player.controllers.hasKeyboard = false;
            return true;
        }
        return false;
    }

    private bool assignKeyboardToPlayerWithID(int playerID)
    {
        Rewired.Player player = ReInput.players.GetPlayer(playerID);
        if (player == null) return false; // no free joysticks
        //player.controllers.ClearControllersOfType(ControllerType.Joystick);
        player.controllers.hasKeyboard = true;
        return true;

    }

    private bool removeJoystickToPlayerWithID(int playerID)
    {
        Rewired.Player player = ReInput.players.GetPlayer(playerID);
        if (player == null) return false; // no free joysticks
        player.controllers.ClearControllersOfType(ControllerType.Joystick);
        return true;
    }

    private bool removeKeyboardToPlayerWithID(int playerID)
    {
        Rewired.Player player = ReInput.players.GetPlayer(playerID);
        if (player == null) return false; // no free joysticks
        //player.controllers.ClearControllersOfType(ControllerType.Joystick);
        player.controllers.hasKeyboard = false;
        return true;

    }
}
