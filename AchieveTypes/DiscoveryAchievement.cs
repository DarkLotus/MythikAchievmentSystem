﻿using Server;
using Server.Mobiles;
using System;


namespace Scripts.Mythik.Systems.Achievements
{
    public class DiscoveryAchievement : BaseAchievement
    {
        private string m_Region;
        public DiscoveryAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, BaseAchievement prereq, string title, string desc, short RewardPoints, string region, params Type[] rewards)
            : base(id, catid, itemIcon, hiddenTillComplete, prereq, title, desc, RewardPoints, 1, rewards)
        {
            m_Region = region;
            CompletionTotal = 1;
            EventSink.OnEnterRegion += EventSink_OnEnterRegion;
        }

        private void EventSink_OnEnterRegion(OnEnterRegionEventArgs e)
        {
            if (e == null || e.Region == null || e.From == null || e.Region.Name == null)
                return;
            var player = e.From as PlayerMobile;
            if (e.Region.Name.Contains(m_Region) && player != null)
            {
                AchievmentSystem.SetAchievementStatus(player, this, 1);

            }
        }
    }

}
