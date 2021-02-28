using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokeProtocol.API;
using BrokeProtocol.Client.UI;
using BrokeProtocol.Entities;

namespace BPCoreLib.Menus
{
    public class EventHandler
    {
        [Target(GameSourceEvent.PlayerOptionAction, ExecutionMode.Event)]
        public void OnOptionAction(ShPlayer player, int targetID, string menuID, string optionID, string actionID)
        {
            if (!PlayerExtension.OptionMenus.ContainsKey(menuID))
            {
                return;
            }

            var menu = PlayerExtension.OptionMenus[menuID];

            menu.Actions[actionID].Action.Invoke(player, optionID);
        }

        [Target(GameSourceEvent.PlayerSubmitInput, ExecutionMode.Event)]
        public void OnSubmitInput(ShPlayer player, int targetID, string menuID, string input)
        {
            if (!PlayerExtension.InputMenus.ContainsKey(menuID))
            {
                return;
            }

            PlayerExtension.InputMenus[menuID].Invoke(player, input);
        }
    }
}