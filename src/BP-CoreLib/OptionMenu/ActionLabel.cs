using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPCoreLib.Interfaces;
using BrokeProtocol.API;
using BrokeProtocol.Collections;
using BrokeProtocol.Entities;
using BrokeProtocol.Required;

namespace BPCoreLib.OptionMenu
{
    public class ActionLabel : IActionLabel
    {
        public string Name { get; }
        public Action<ShPlayer, string> Action { get; }

        public ActionLabel(string name, Action<ShPlayer, string> action)
        {
            Name = name;
            Action = action;
        }
    }

    internal class OptionMenu
    {
        public static Dictionary<string, OptionMenu> Menus = new Dictionary<string, OptionMenu>();

        private readonly string _title;
        public Dictionary<string, ActionLabel> Actions { get; private set; }
        private readonly LabelID[] _options;
        private readonly string _menuId;

        public OptionMenu(string title, IEnumerable<LabelID> labels, IEnumerable<ActionLabel> actions)
        {
            _title = title;
            _options = labels.ToArray();
            _menuId = Guid.NewGuid().ToString();
            Menus.Add(_menuId, this);
            Actions = new Dictionary<string, ActionLabel>();
            foreach (var action in actions)
            {
                Actions.Add(Guid.NewGuid().ToString(), action);
            }
        }

        internal void SendToPlayer(ShPlayer player)
        {
            var actions = new LabelID[Actions.Count];
            for (var i = 0; i < actions.Length; i++)
            {
                var action = Actions.ElementAt(i);
                actions[i++] = new LabelID(action.Value.Name, action.Key);
            }
            player.svPlayer.SendOptionMenu(_title, player.ID, _menuId, _options, actions);
        }
    }

    public static class Extension
    {
        public static void SendOptionMenu(this ShPlayer player, string title, IEnumerable<LabelID> labels, IEnumerable<ActionLabel> actions)
        {
            (new OptionMenu(title, labels, actions)).SendToPlayer(player);
        }
    }

    public class OnAction : IScript
    {
        [Target(GameSourceEvent.PlayerOptionAction, ExecutionMode.Event)]
        public void OnOptionAction(ShPlayer player, int targetID, string menuID, string optionID, string actionID)
        {
            if (!OptionMenu.Menus.ContainsKey(menuID))
            {
                return;
            }

            var menu = OptionMenu.Menus[menuID];

            menu.Actions[actionID].Action.Invoke(player, optionID);
        }
    }
}