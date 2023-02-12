using System.Linq;
using BrokeProtocol.Entities;
using BrokeProtocol.GameSource;
using BrokeProtocol.Managers;
using BrokeProtocol.Required;

namespace BPCoreLib.ExtensionMethods
{
    public static class SvPlayerExtensions
    {
        public static void Replenish(this SvPlayer player)
        {
            player.UpdateStatsDelta(1f, 1f, 1f);
        }
        
        public static void Damage(this SvPlayer player, float amount)
        {
            player.Damage(DamageIndex.Null, amount);
        }

        public static bool IsJobGroup(this SvPlayer player, GroupIndex group)
        {
            return ((MyJobInfo)player.job.info).groupIndex == group;
        }
        
        public static bool IsPrisoner(this SvPlayer player)
        {
            return player.IsJobGroup(GroupIndex.Prisoner);
        }
        
        public static bool IsWearing(this SvPlayer player, string id)
        {
            if (!SceneManager.Instance.TryGetEntity(id, out ShWearable entity))
            {
                return false;
            }

            return player.IsWearing(entity);
        }

        public static bool IsWearing(this SvPlayer player, ShWearable wearable)
        {
            ShWearable currentWearable = player.player.curWearables[(int) wearable.type];
            if (!currentWearable)
            {
                return false;
            }

            return currentWearable.index == wearable.index;
        }

        public static bool IsInTrigger(this SvPlayer player, string goName)
        {
            return player.currentTriggers.Any(x => x.go.name == goName);
        }
        
        public static bool IsInTriggerByData(this SvPlayer player, string data)
        {
            return player.currentTriggers.Any(x => x.data == data);
        }
    }
}
