using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmamusumeData.Enums
{
    public enum SkillTargetType
    {
        Self = 1,
        All,
        AllOtherSelf,
        Visible,
        RandomOtherSelf,
        Order,
        OrderInfront,
        OrderBehind,
        SelfInfront,
        SelfBehind,
        TeamMember,
        Near,
        SelfAndBlockFront,
        BlockSide,
        NearInfront,
        NearBehind,
        RunningStyle,
        RunningStyleOtherSelf,
        SelfInfrontTemptation,
        SelfBehindTemptation,
        RunningStyleTemptationOtherSelf,
        CharaId,
        ActivateHealSkill,
        SkillTargetTeamMemberRandom,
        SkillTargetOtherTeamMemberRandom
    }
}
