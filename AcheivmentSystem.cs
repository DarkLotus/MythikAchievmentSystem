//#define STOREONITEM

using Server;
using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Misc;

#if STOREONITEM
#else
using Scripts.Mythik.Mobiles;
#endif

using Scripts.Mythik.Systems.Achievements.Gumps;

namespace Scripts.Mythik.Systems.Achievements
{
    //TODO
    //subcategories X
    //page limit?
    // Achievement prereq achieve before showing X
    //TODO Skill gain achieves needs event X
    //TODO ITEM crafted event sink X
    //TODO SKILL USE 
    //TODO HousePlaced event sink
    /*thought of eating a lemon (and other foods), consume pots,
     *  craft a home, 
     *  own home (more for larger homes), 
     *  loot x amount of gold, 
     *  find a uni, 
     *  kill each mob in the game,
     *   enter an event,
     *    tame all tamables,
     *     use a max powerscroll (or skill stone), 
     *     ride each type of mount
     */
    public class AchievmentSystem
    {
        public class AchievementCategory
        {
            public int ID { get; set; }
            public int Parent { get; set; }
            public string Name;


            public AchievementCategory(int id, int parent, string v3)
            {
                ID = id;
                Parent = parent;
                Name = v3;
            }
        }
        public static List<BaseAchievement> Achievements = new List<BaseAchievement>();
        public static List<AchievementCategory> Categories = new List<AchievementCategory>();

        public static void Initialize()
        {
            Categories.Add(new AchievementCategory(1, 0, "Exploration"));
                Categories.Add(new AchievementCategory(2, 1, "Towns"));
                Categories.Add(new AchievementCategory(3, 1, "Dungeons"));

            Categories.Add(new AchievementCategory(4, 0, "Crafting"));
            Categories.Add(new AchievementCategory(5, 0, "Resource Gathering"));
            Categories.Add(new AchievementCategory(6, 0, "Hunting"));
            Categories.Add(new AchievementCategory(7, 0, "Character Development"));
            Categories.Add(new AchievementCategory(8, 0, "Other"));

            Achievements.Add(new DiscoveryAchievement(0, 2, 0x96E, false, null, "Minoc!", "Discover Minoc Township", 5, "Minoc"));
            Achievements.Add(new DiscoveryAchievement(1, 2, 0x96E, false, null, "Yew!", "Discover the Yew Township", 5, "Yew"));
            Achievements.Add(new DiscoveryAchievement(2, 2, 0x96E, false, null, "Trinsic!", "Discover the Trinsic Township", 5, "Trinsic"));
            Achievements.Add(new DiscoveryAchievement(3, 2, 0x96E, false, null, "Cove!", "Discover the Cove Township", 5, "Cove"));
            Achievements.Add(new DiscoveryAchievement(4, 3, 0x96E, false, null, "Wrong!", "Discover the dungeon of Wrong", 5, "Wrong"));
            Achievements.Add(new DiscoveryAchievement(5, 3, 0x96E, false, null, "Shame!", "Discover the dungeon of Shame", 5, "Shame"));

            var achieve = new HarvestAchievement(100, 5, 0, false, null, 500, "500 Iron Ore", "Mine 500 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer));
            Achievements.Add(achieve);
            Achievements.Add(new HarvestAchievement(100, 5, 0, false, achieve, 500, "50000 Iron Ore", "Mine 500 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer)));

            Achievements.Add(new HunterAchievement(300, 6, 0x25D1, false, null, 5, "Dog Slayer", "Slay 5 Dogs", 5, typeof(Dog)));
            Achievements.Add(new HunterAchievement(301, 6, 0x25D1, false, null, 50, "Dragon Slayer", "Slay 50 Dragon", 5, typeof(Dragon)));




            CommandSystem.Register("feats", AccessLevel.Player, new CommandEventHandler(OpenGump));

        }

        private static void OpenGump(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if(player != null)
            {
#if STOREONITEM
           if (!AchievementSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AchievementSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AchieveData>());
            var achieves = AchievementSystemMemoryStone.GetInstance().Achievements[player.Serial];
                var total = AchievementSystemMemoryStone.GetInstance().GetPlayerPointsTotal(player);
#else
                var achieves = (player as MythikPlayerMobile).Achievements;
                var total = (player as MythikPlayerMobile).AchievementPointsTotal;
#endif
                e.Mobile.SendGump(new AchievementGump(achieves, total));
            }
            
        }

        internal static void SetAchievementStatus(PlayerMobile player, BaseAchievement ach, int progress)
        {
#if STOREONITEM
           if (!AchievementSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AchievementSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AchieveData>());
            var achieves = AchievementSystemMemoryStone.GetInstance().Achievements[player.Serial]; 
#else
            var achieves = (player as MythikPlayerMobile).Achievements;
#endif
            if (achieves.ContainsKey(ach.ID))
            {
                if (achieves[ach.ID].Progress >= ach.CompletionTotal)
                    return;
                achieves[ach.ID].Progress += progress;
            }
            else
            {
                achieves.Add(ach.ID, new AchieveData() { Progress = progress });
            }

            if (achieves[ach.ID].Progress >= ach.CompletionTotal)
            {
                player.SendGump(new AchievementObtainedGump(ach),false);
                achieves[ach.ID].CompletedOn = DateTime.Now;
#if STOREONITEM
                AchievementSystemMemoryStone.GetInstance().AddPoints(player,ach.RewardPoints);
#else
                (player as MythikPlayerMobile).AchievementPointsTotal += ach.RewardPoints;
#endif
                if (ach.RewardItems != null && ach.RewardItems.Length > 0)
                {
                    try
                    {
                        player.SendAsciiMessage("You have recieved an award for completing this achievment!");
                        var item = (Item)Activator.CreateInstance(ach.RewardItems[0]);
                        if (!WeightOverloading.IsOverloaded(player))
                            player.Backpack.DropItem(item);
                        else
                            player.BankBox.DropItem(item);
                    }
                    catch { }
                }
            }
        }


    }
}
