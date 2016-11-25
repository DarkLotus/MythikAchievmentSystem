#define STOREONITEM

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
    //page limit?
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
                Categories.Add(new AchievementCategory(4, 1, "Points of Interest"));

            Categories.Add(new AchievementCategory(1000, 0, "Crafting"));
            Categories.Add(new AchievementCategory(2000, 0, "Resource Gathering"));
            Categories.Add(new AchievementCategory(3000, 0, "Hunting"));
            Categories.Add(new AchievementCategory(4000, 0, "Character Development"));
            Categories.Add(new AchievementCategory(5000, 0, "Other"));


            Achievements.Add(new DiscoveryAchievement(0, 2, 0x14EB, false, null, "Cove!", "Discover the Cove Township", 5, "Cove"));
            Achievements.Add(new DiscoveryAchievement(1, 2, 0x14EB, false, null, "Britain!", "Discover the City Britain", 5, "Britain"));
            Achievements.Add(new DiscoveryAchievement(2, 2, 0x14EB, false, null, "Minoc!", "Discover the Minoc Township", 5, "Minoc"));
            Achievements.Add(new DiscoveryAchievement(3, 2, 0x14EB, false, null, "Ocllo!", "Discover the Ocllo Township", 5, "Ocllo"));
            Achievements.Add(new DiscoveryAchievement(4, 2, 0x14EB, false, null, "Trinsic!", "Discover the City of Trinsic", 5, "Trinsic"));
            Achievements.Add(new DiscoveryAchievement(5, 2, 0x14EB, false, null, "Vesper!", "Discover the City of Vesper", 5, "Vesper"));
            Achievements.Add(new DiscoveryAchievement(6, 2, 0x14EB, false, null, "Yew!", "Discover the Yew Township", 5, "Yew"));
            Achievements.Add(new DiscoveryAchievement(7, 2, 0x14EB, false, null, "Wind!", "Discover the City of Wind", 5, "Wind"));
            Achievements.Add(new DiscoveryAchievement(8, 2, 0x14EB, false, null, "Serpent's Hold!", "Discover the City of Serpent's Hold", 5, "Serpent's Hold"));
            Achievements.Add(new DiscoveryAchievement(9, 2, 0x14EB, false, null, "Skara Brae!", "Discover the Island of Skara Brae", 5, "Skara Brae"));
            Achievements.Add(new DiscoveryAchievement(10, 2, 0x14EB, false, null, "Nujel'm!", "Discover the Island of Nujel'm", 5, "Nujel'm"));
            Achievements.Add(new DiscoveryAchievement(11, 2, 0x14EB, false, null, "Moonglow!", "Discover the City of Moonglow", 5, "Moonglow"));
            Achievements.Add(new DiscoveryAchievement(12, 2, 0x14EB, false, null, "Magincia!", "Discover the City of Magincia", 5, "Magincia"));
            Achievements.Add(new DiscoveryAchievement(13, 2, 0x14EB, false, null, "Buccaneer's Den!", "Discover the Secrets of Buccaneer's Den", 5, "Buccaneer's Den"));

            Achievements.Add(new DiscoveryAchievement(25, 3, 0x14EB, false, null, "Covetous!", "Discover the dungeon of Covetous", 5, "Covetous"));
            Achievements.Add(new DiscoveryAchievement(26, 3, 0x14EB, false, null, "Deceit!", "Discover the dungeon of Deceit", 5, "Deceit"));
            Achievements.Add(new DiscoveryAchievement(27, 3, 0x14EB, false, null, "Despise!", "Discover the dungeon of Despise", 5, "Despise"));
            Achievements.Add(new DiscoveryAchievement(28, 3, 0x14EB, false, null, "Destard!", "Discover the dungeon of Destard", 5, "Destard"));
            Achievements.Add(new DiscoveryAchievement(29, 3, 0x14EB, false, null, "Hythloth!", "Discover the dungeon of Hythloth", 5, "Hythloth"));
            Achievements.Add(new DiscoveryAchievement(30, 3, 0x14EB, false, null, "Wrong!", "Discover the dungeon of Wrong", 5, "Wrong"));
            Achievements.Add(new DiscoveryAchievement(31, 3, 0x14EB, false, null, "Shame!", "Discover the dungeon of Shame", 5, "Shame"));

            Achievements.Add(new DiscoveryAchievement(100, 4, 0x14EB, false, null, "Cotton!", "Discover A Cotton Field in Moonglow", 5, "A Cotton Field in Moonglow"));
            Achievements.Add(new DiscoveryAchievement(101, 4, 0x14EB, false, null, "Carrots!", "Discover A Carrot Field in Skara Brae", 5, "A Carrot Field in Skara Brae"));

            //these two show examples of adding a reward or multiple rewards
            var achieve = new HarvestAchievement(500, 2000, 0x0E85, false, null, 500, "500 Iron Ore", "Mine 500 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer));
            Achievements.Add(achieve);
            Achievements.Add(new HarvestAchievement(501, 2000, 0x0E85, false, achieve, 5000, "5000 Iron Ore", "Mine 5000 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer),typeof(TinkerTools),typeof(HatOfTheMagi)));

            Achievements.Add(new HunterAchievement(1000, 3000, 0x25D1, false, null, 5, "Dog Slayer", "Slay 5 Dogs", 5, typeof(Dog)));
            Achievements.Add(new HunterAchievement(1001, 3000, 0x25D1, false, null, 50, "Dragon Slayer", "Slay 50 Dragon", 5, typeof(Dragon)));




            CommandSystem.Register("feats", AccessLevel.Player, new CommandEventHandler(OpenGumpCommand));

        }

        public static void OpenGump(Mobile from, Mobile target)
        {
            if (from == null || target == null)
                return;
            if (target as PlayerMobile != null)
            {
#if STOREONITEM
                var player = target as PlayerMobile;
           if (!AchievementSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AchievementSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AchieveData>());
            var achieves = AchievementSystemMemoryStone.GetInstance().Achievements[player.Serial];
                var total = AchievementSystemMemoryStone.GetInstance().GetPlayerPointsTotal(player);
#else
                var achieves = (target as MythikPlayerMobile).Achievements;
                var total = (target as MythikPlayerMobile).AchievementPointsTotal;
#endif
                from.SendGump(new AchievementGump(achieves, total));
            }
        }
        [Usage("feats"), Aliases("ach", "achievement", "achs", "achievements")]
        [Description("Opens the Achievements gump")]
        private static void OpenGumpCommand(CommandEventArgs e)
        {
            OpenGump(e.Mobile, e.Mobile); 
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
                achieves[ach.ID].CompletedOn = DateTime.UtcNow;
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
