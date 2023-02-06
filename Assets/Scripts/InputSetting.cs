using UnityEngine;
using System.Collections.Generic;
using Rewired;
using UnityEngine.UI;

public class InputSetting : MonoBehaviour
{
    public GameObject CheckInputPage;
    public Toggle p1InputState;


    void Start()
    {

    }

    private void Update()
    {
        if (!ReInput.isReady) return;
        AssignJoysticksToPlayers();
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
                Rewired.Player player = FindPlayerWithoutJoystickAndKeyBoard();
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
    private Rewired.Player FindPlayerWithoutJoystickAndKeyBoard()
    {
        IList<Rewired.Player> players = ReInput.players.Players;
        for (int i = 0; i < players.Count; i++)
        {
            if (i == 0)
            {
                p1InputState.isOn = players[i].controllers.hasKeyboard;
            }
            if (players[i].controllers.joystickCount > 0) continue;
            //if (players[i].controllers.hasKeyboard) players[i].controllers.hasKeyboard = false;
            return players[i];
        }
        return null;
    }

    private bool DoAllPlayersHaveJoysticks()
    {
        return FindPlayerWithoutJoystickAndKeyBoard() == null;
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
}
