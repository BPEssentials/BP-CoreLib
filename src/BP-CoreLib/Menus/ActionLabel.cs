using BPCoreLib.Interfaces;
using BrokeProtocol.Entities;
using BrokeProtocol.Required;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPCoreLib.Menus
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
        private readonly string _title;
        public Dictionary<string, IActionLabel> Actions { get; private set; }
        private readonly LabelID[] _options;
        private readonly string _menuId;

        public OptionMenu(string title, IEnumerable<LabelID> labels, IEnumerable<IActionLabel> actions)
        {
            _title = title;
            _options = labels.ToArray();
            _menuId = Guid.NewGuid().ToString();
            PlayerExtension.OptionMenus.Add(_menuId, this);
            Actions = new Dictionary<string, IActionLabel>();
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
                actions[i] = new LabelID(action.Value.Name, action.Key);
            }
            player.svPlayer.SendOptionMenu(_title, player.ID, _menuId, _options, actions);
        }
    }
}