﻿using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Achievements
{

    public class ItemCraftedAchievement : BaseAchievement
    {
        private Type m_Item;
        public ItemCraftedAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, BaseAchievement prereq, int total, string title, string desc, short RewardPoints, Type item, params Type[] rewards)
            : base(id, catid, itemIcon, hiddenTillComplete, prereq, title, desc, RewardPoints, total, rewards)
        {
            m_Item = item;
            EventSink.CraftSuccess += EventSink_CraftSuccess; ;
        }

        private void EventSink_CraftSuccess(CraftSuccessEventArgs e)
        {
            if(e.Crafter is PlayerMobile && e.CraftedItem.GetType() == m_Item)
            {
                AchievmentSystem.SetAchievementStatus(e.Crafter as PlayerMobile, this, e.CraftedItem.Amount);
            }
        }

       
    }
}
