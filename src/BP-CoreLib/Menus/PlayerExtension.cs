using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPCoreLib.Interfaces;
using BPCoreLib.Serializable;
using BrokeProtocol.Entities;
using BrokeProtocol.Required;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace BPCoreLib.Menus
{
    public static class PlayerExtension
    {
        internal static Dictionary<string, Action<string>> InputMenus { get; } = new Dictionary<string, Action<string>>();

        internal static Dictionary<string, OptionMenu> OptionMenus { get; } = new Dictionary<string, OptionMenu>();

        public static void SendOptionMenu(this ShPlayer player, string title, IEnumerable<LabelID> labels, IEnumerable<IActionLabel> actions)
        {
            (new OptionMenu(title, labels, actions)).SendToPlayer(player);
        }

        public static void SendInputMenu(this ShPlayer player, string title, Action<string> action, InputField.ContentType type = InputField.ContentType.Standard)
        {
            var menuId = Guid.NewGuid().ToString();
            player.svPlayer.SendInputMenu(title, player.ID, menuId, type);
            InputMenus.Add(menuId, action);
        }
    }
}