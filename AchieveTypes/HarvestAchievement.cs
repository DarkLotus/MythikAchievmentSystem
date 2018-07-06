using Server;
using Server.Mobiles;
using System;

namespace Scripts.Mythik.Systems.Achievements
{
    public class HarvestAchievement : BaseAchievement
    {
        private Type m_Item;
        public HarvestAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, BaseAchievement prereq, int total, string title, string desc, short RewardPoints, Type targets, params Type[] rewards)
            : base(id, catid, itemIcon, hiddenTillComplete, prereq, title, desc, RewardPoints, total, rewards)
        {
            m_Item = targets;
            EventSink.ResourceHarvestSuccess += EventSink_ResourceHarvestSuccess;
        }

        private void EventSink_ResourceHarvestSuccess(ResourceHarvestSuccessEventArgs e)
        {
            var player = e.Harvester as PlayerMobile;
            if (e.Resource.GetType() == m_Item)
            {
                AchievementSystem.SetAchievementStatus(player, this, e.Resource.Amount);
            }
        }
    }
}
