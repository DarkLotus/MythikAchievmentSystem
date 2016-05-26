using Scripts.Mythik.Mobiles;
using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Achievements
{
    public class Achievement
    {

        public Achievement(int id, int catid, int itemIcon,bool hiddenTillComplete,string title, string desc, short rewardPoints,int total, params Type[] rewards)
        {
            ID = id;
            CategoryID = catid;
            Title = title;
            Desc = desc;
            RewardPoints = rewardPoints;
            CompletionTotal = total;
            RewardItems = rewards;
            HiddenTillComplete = hiddenTillComplete;
            ItemIcon = itemIcon;
        }

        /// <summary>
        /// Achievement ID must be Unique
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID of this acheivments category
        /// </summary>
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int ItemIcon { get; set; }
        /// <summary>
        /// Number of Points rewarded for completion
        /// </summary>
        public short RewardPoints { get; set; }
        public Type[] RewardItems { get; set; }
        public int CompletionTotal { get; set; }
        public bool HiddenTillComplete { get; private set; }
    }



    public class DiscoveryAchievement : Achievement
    {
        private string m_Region;
        public DiscoveryAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, string title, string desc, short RewardPoints, string region, params Type[] rewards)
            :base(id,catid, itemIcon, hiddenTillComplete,title,desc,RewardPoints,1,rewards)
        {
            m_Region = region;
            CompletionTotal = 1;
            EventSink.OnEnterRegion += EventSink_OnEnterRegion;
        }

        private void EventSink_OnEnterRegion(OnEnterRegionEventArgs e)
        {
            if (e.Region == null || e.From == null)
                return;
            var player = e.From as PlayerMobile;
            if(e.Region.Name.Contains(m_Region) && player != null)
            {
                AchievmentSystem.SetAchievementStatus(player, this, 1);

            }
        }
    }

    public class HunterAchievement : Achievement
    {
        private Type m_Mobile;
        public HunterAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, int total, string title, string desc, short RewardPoints, Type targets, params Type[] rewards)
            :base(id,catid, itemIcon, hiddenTillComplete,title,desc,RewardPoints,total,rewards)
        {
            m_Mobile = targets;
            EventSink.OnKilledBy += EventSink_OnKilledBy;
        }

        private void EventSink_OnKilledBy(OnKilledByEventArgs e)
        {
            var player = e.KilledBy as PlayerMobile;
            if(player != null && e.Killed.GetType() == m_Mobile) {
                AchievmentSystem.SetAchievementStatus(player, this,1);
            }
        }

        
    }

    class HarvestAchievement : Achievement
    {
        private Type m_Item;
        public HarvestAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, int total, string title, string desc, short RewardPoints, Type targets, params Type[] rewards)
            : base(id, catid,itemIcon, hiddenTillComplete, title, desc, RewardPoints, total, rewards)
        {
            m_Item = targets;
            EventSink.ResourceHarvestSuccess += EventSink_ResourceHarvestSuccess;
        }

        private void EventSink_ResourceHarvestSuccess(ResourceHarvestSuccessEventArgs e)
        {
            var player = e.Harvester as PlayerMobile;
            if(e.Resource.GetType() == m_Item)
            {
                AchievmentSystem.SetAchievementStatus(player, this, e.Resource.Amount);
            }
        }
    }



    class SkillProgressAchievement : Achievement
    {
        private Type m_Item;
        public SkillProgressAchievement(int id, int catid, int itemIcon, bool hiddenTillComplete, int total, string title, string desc, short RewardPoints, Type targets, params Type[] rewards)
            : base(id, catid, itemIcon, hiddenTillComplete, title, desc, RewardPoints, total, rewards)
        {
            m_Item = targets;
            
        }

        private void EventSink_ResourceHarvestSuccess(ResourceHarvestSuccessEventArgs e)
        {
            var player = e.Harvester as PlayerMobile;
            if (e.Resource.GetType() == m_Item)
            {
                AchievmentSystem.SetAchievementStatus(player, this, e.Resource.Amount);
            }
        }
    }

    class AcheiveData
    {
        //public int ID { get; set; }
        public int Progress { get; set; }
        public DateTime CompletedOn { get; set; }

        public AcheiveData()
        {

        }
        public AcheiveData(GenericReader reader)
        {
            Deserialize(reader);
        }
        public void Serialize(GenericWriter writer)
        {
            writer.Write(1); // version
            writer.Write(Progress);
            writer.Write(CompletedOn);

        }
        public void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();
            Progress = reader.ReadInt();
            CompletedOn = reader.ReadDateTime();
        }

    }
}
