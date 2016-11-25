using Server.ContextMenus;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Achievements
{
    public class AchievementMenuEntry : ContextMenuEntry
    {
        private PlayerMobile _from;
        private PlayerMobile _target;

        public AchievementMenuEntry(PlayerMobile from,PlayerMobile target)
            : base(6252, -1) // View Achievements // 3006252
        {
            _from = from;
            _target = target;
        }

        public override void OnClick()
        {
            AchievmentSystem.OpenGump(_from, _target);
        }
    }

}
