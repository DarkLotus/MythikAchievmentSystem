using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Achievements.Gumps
{
    class AchievementObtainedGump : Gump
    {
        private Achievement ach;

        public AchievementObtainedGump(Achievement ach):base(25,25)
        {
            this.ach = ach;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(39, 38, 350, 100, 9270);
            this.AddAlphaRegion(48, 45, 332, 86);
            if(ach.ItemIcon > 0)
                this.AddItem(29, 48, ach.ItemIcon);
            this.AddLabel(121, 55, 49, ach.Title);
            this.AddHtml(120, 80, 167, 42, ach.Desc, (bool)true, (bool)true);
            this.AddLabel(275, 51, 61, @"COMPLETE");
            this.AddBackground(320, 72, 44, 47, 9200);
            this.AddLabel(337, 87, 0, ach.RewardPoints.ToString());
        }
    }
}
