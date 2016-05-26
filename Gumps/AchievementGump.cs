using Scripts.Mythik.Mobiles;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;

namespace Scripts.Mythik.Systems.Achievements.Gumps
{
    class AchievementGump : Gump
    {
        private int m_curTotal;
        private Dictionary<int, AcheiveData> m_curAchieves;

        public AchievementGump(Dictionary<int, AcheiveData> achieves, int total,int category = 0) : base(25, 25)
        {
            
            m_curAchieves = achieves;
            m_curTotal = total;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(11, 15, 758, 575, 2600);
            this.AddBackground(57, 92, 666, 478, 9250);
            this.AddBackground(321, 104, 386, 453, 9270);
            this.AddBackground(72, 104, 245, 453, 9270);
            this.AddBackground(72, 34, 635, 53, 9270);
            this.AddBackground(327, 0, 133, 41, 9200);
            this.AddLabel(292, 52, 68, @"Mythik Achievment System");
            this.AddLabel(360, 11, 82, total + @" Points");
            this.AddBackground(341, 522, 353, 26, 9200);


            for(int i = 0;i < AchievmentSystem.Categories.Count; i++)
            {
                int x = 90;
                if (AchievmentSystem.Categories[i].Parent != 0 && i != category)
                    continue;
                else
                {
                    x += 20;
                }
                int bgID = 9200;
                if(i == category)
                    bgID = 5120;
                this.AddBackground(x, 123 + (i * 31), 18810 / x, 25, bgID);
               
                if (i == category) // selected
                    this.AddImage(x + 12, 129 + (i * 31), 1210);
                else
                    this.AddButton(102, 129 + (i * 31), 1209, 1210, 5000 + i, GumpButtonType.Reply, 0);
                this.AddLabel(x + 32, 125 + (i * 31), 0, AchievmentSystem.Categories[i].Name);
            }
            int cnt = 0; 
            foreach( var ac in AchievmentSystem.Achievements)
            {
                if (ac.CategoryID == category)
                {
                    if(achieves.ContainsKey(ac.ID))
                    {
                        AddAchieve(ac, cnt, achieves[ac.ID]);
                    }
                    else {
                        AddAchieve(ac, cnt,null);
                    }
                    cnt++;
                }                
            }
        }

        private void AddAchieve(Achievement ac, int i, AcheiveData acheiveData)
        {
            int index = i % 4;
            if(index == 0)
            {
                this.AddButton(658, 524, 4005, 4006, 0, GumpButtonType.Page, (i / 4) + 1);
                AddPage((i / 4) + 1);
                this.AddLabel(484, 526, 32, "Page " + ((i / 4) + 1));
                this.AddButton(345, 524, 4014, 4015, 0, GumpButtonType.Page, i/4);
            }
            int bg = 9350;
            if (acheiveData?.CompletedOn != null)
                bg = 9300;
            this.AddBackground(340, 122 + (index * 100), 347, 97, bg);
            this.AddLabel(414, 131 + (index * 100), 49, ac.Title);
            if(ac.ItemIcon > 0)
                this.AddItem(357, 147 + (index * 100), ac.ItemIcon);
            this.AddImageTiled(416, 203 + (index * 100), 95, 9, 9750);

            var step = 95.0 / ac.CompletionTotal;
            var progress = 0;
            if (acheiveData?.CompletedOn != null)
                progress = acheiveData.Progress;

            this.AddImageTiled(416, 203 + (index * 100), (int)(progress * step), 9, 9752);
            this.AddHtml(413, 152 + (index * 100), 194, 47,ac.Desc, (bool)true, (bool)true);
            if (acheiveData?.CompletedOn != null)
                this.AddLabel(566, 127 + (index * 100), 32, acheiveData.CompletedOn.ToShortDateString());

            if(ac.CompletionTotal > 1)
                this.AddLabel(522, 196 + (index * 100), 0, progress + @" / " + ac.CompletionTotal);

            this.AddBackground(628, 149 + (index * 100), 48, 48, 9200);
            this.AddLabel(648, 163 + (index * 100), 32, ac.RewardPoints.ToString());

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
            if (info.ButtonID == 0)
                return;
            var btn = info.ButtonID - 5000;
            if (btn >= 0 && btn < AchievmentSystem.Categories.Count)
                sender.Mobile.SendGump(new AchievementGump(m_curAchieves, m_curTotal, btn));
        }


    }
}

