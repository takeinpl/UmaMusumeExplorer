using UmamusumeData.Enums;
using UmamusumeData.Tables;

namespace UmamusumeData.Utility
{
    public static class SkillHelpers
    {
        public static int GetSkillIcon(SkillData skillData)
        {
            if (skillData.IconId != 0) return skillData.IconId;

            int abilityTypePart = 0;
            int rarityPart = 0;

            SkillTargetType targetType = (SkillTargetType)skillData.TargetType11;
            SkillAbilityType abilityType = (SkillAbilityType)skillData.AbilityType11;
            int rarity = skillData.Rarity;

            float abilityValue = skillData.FloatAbilityValue11 * 0.0001f;
            bool isTargetOwnHorse = IsSkillTargetOwnHorse(targetType);
            bool isDebuff = IsDebuff(abilityType, abilityValue);

            bool isDemerit = IsSkillDemerit(abilityType, abilityValue);
            isDemerit = isDemerit && isTargetOwnHorse;

            if (!isDebuff && isTargetOwnHorse)
            {
                switch (abilityType)
                {
                    case SkillAbilityType.Speed:
                        abilityTypePart = 1001;
                        break;
                    case SkillAbilityType.Stamina:
                        abilityTypePart = 1002;
                        break;
                    case SkillAbilityType.Power:
                        abilityTypePart = 1003;
                        break;
                    case SkillAbilityType.Guts:
                        abilityTypePart = 1004;
                        break;
                    case SkillAbilityType.Wiz:
                    case SkillAbilityType.TemptationPer:
                        abilityTypePart = 1005;
                        break;
                    case SkillAbilityType.RunningStyleExOonige:
                    case SkillAbilityType.ForceOvertakeIn:
                    case SkillAbilityType.ForceOvertakeOut:
                    case SkillAbilityType.TemptationEndTime:
                    case SkillAbilityType.HpRateDemerit:
                    case SkillAbilityType.NOUSE_14:
                    case SkillAbilityType.NOUSE:
                    case SkillAbilityType.NOUSE_3:
                    case SkillAbilityType.NOUSE_21:
                    case SkillAbilityType.NOUSE_8:
                    case SkillAbilityType.NOUSE_2:
                    case SkillAbilityType.NOUSE_4:
                    case SkillAbilityType.NOUSE_7:
                    case SkillAbilityType.NOUSE_5:
                    case SkillAbilityType.PushPer:
                        return 0;
                    case SkillAbilityType.HpDecRate:
                    case SkillAbilityType.HpRate:
                        abilityTypePart = 2002;
                        break;
                    case SkillAbilityType.VisibleDistance:
                        abilityTypePart = 2009;
                        break;
                    case SkillAbilityType.StartDash:
                    case SkillAbilityType.StartDelayFix:
                        abilityTypePart = 2006;
                        break;
                    case SkillAbilityType.CurrentSpeed:
                    case SkillAbilityType.CurrentSpeedWithNaturalDeceleration:
                    case SkillAbilityType.TargetSpeed:
                        abilityTypePart = 2001;
                        break;
                    case SkillAbilityType.LaneMoveSpeed:
                        abilityTypePart = 2005;
                        break;
                    case SkillAbilityType.Accel:
                        abilityTypePart = 2004;
                        break;
                    case SkillAbilityType.AllStatus:
                        abilityTypePart = 1006;
                        break;
                    default:
                        if (abilityType != SkillAbilityType.AddExPower)
                            return 0;
                        abilityTypePart = 2035;
                        break;
                }
            }
            else
            {
                switch (abilityType)
                {
                    case SkillAbilityType.HpDecRate:
                    case SkillAbilityType.HpRate:
                        abilityTypePart = 3005;
                        break;
                    case SkillAbilityType.VisibleDistance:
                        abilityTypePart = 3007;
                        break;
                    case SkillAbilityType.StartDash:
                    case SkillAbilityType.ForceOvertakeIn:
                    case SkillAbilityType.ForceOvertakeOut:
                    case SkillAbilityType.TemptationEndTime:
                        abilityTypePart = 3004;
                        break;
                    default:
                        if (((int)abilityType - 21 < 2) || abilityType == SkillAbilityType.TargetSpeed)
                            abilityTypePart = 3001;
                        else if (abilityType == SkillAbilityType.LaneMoveSpeed)
                            abilityTypePart = 3003;
                        else
                        {
                            if (abilityType == SkillAbilityType.TemptationPer)
                                return 0;
                            if (abilityType == SkillAbilityType.PushPer)
                                return 0;
                            if (abilityType != SkillAbilityType.Accel)
                                return 0;

                            abilityTypePart = 3002;
                        }
                        break;
                }
            }

            if (!isDemerit)
            {
                switch (rarity)
                {
                    case 1:
                        rarityPart = 1;
                        break;
                    case 2:
                        rarityPart = 2;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        rarityPart = 3;
                        break;
                    case 6:
                        rarityPart = 6;
                        break;
                    default:
                        return 0;
                }
            }
            else if (rarity == 1)
                rarityPart = 4;
            else
            {
                if (rarity != 2)
                    return 0;

                rarityPart = 5;
            }

            return rarityPart + abilityTypePart * 10;
        }

        public static bool IsSkillTargetOwnHorse(SkillTargetType skillTargetType)
        {
            if (skillTargetType != SkillTargetType.Self &&
                skillTargetType != SkillTargetType.TeamMember &&
                skillTargetType != SkillTargetType.CharaId)
                return false;

            return true;
        }

        public static bool IsDebuff(SkillAbilityType skillAbilityType, float abilityValue)
        {
            if (skillAbilityType != SkillAbilityType.None)
            {
                bool abilityValueFlag = abilityValue < 0.0f;
                switch (skillAbilityType)
                {
                    case SkillAbilityType.StartDash:
                    case SkillAbilityType.DebuffAbilityValueMultiply:
                    case SkillAbilityType.DebuffAbilityValueMultiplyOtherActivate:
                        return abilityValue > 1.0f;
                    case SkillAbilityType.TemptationEndTime:
                    case SkillAbilityType.StartDelayFix:
                        return true;
                    case SkillAbilityType.TemptationPer:
                        return !abilityValueFlag;
                    case SkillAbilityType.UpgradeSkillTimeMultiply:
                        return abilityValue < 1.0f;
                    case SkillAbilityType.Speed:
                    case SkillAbilityType.Stamina:
                    case SkillAbilityType.Power:
                    case SkillAbilityType.Guts:
                    case SkillAbilityType.Wiz:
                    case SkillAbilityType.VisibleDistance:
                    case SkillAbilityType.HpRate:
                    case SkillAbilityType.CurrentSpeed:
                    case SkillAbilityType.CurrentSpeedWithNaturalDeceleration:
                    case SkillAbilityType.TargetSpeed:
                    case SkillAbilityType.LaneMoveSpeed:
                    case SkillAbilityType.PushPer:
                    case SkillAbilityType.Accel:
                    case SkillAbilityType.AllStatus:
                    case SkillAbilityType.AddExPower:
                        return abilityValueFlag;
                    default:
                        return false;
                }
            }

            return false;
        }

        public static bool IsSkillDemerit(SkillAbilityType skillAbilityType, float abilityValue)
        {
            bool abilityValueFlag = abilityValue < 0.0f;
            switch (skillAbilityType)
            {
                case SkillAbilityType.Speed:
                case SkillAbilityType.Stamina:
                case SkillAbilityType.Power:
                case SkillAbilityType.Guts:
                case SkillAbilityType.Wiz:
                case SkillAbilityType.VisibleDistance:
                case SkillAbilityType.HpRate:
                case SkillAbilityType.CurrentSpeed:
                case SkillAbilityType.CurrentSpeedWithNaturalDeceleration:
                case SkillAbilityType.TargetSpeed:
                case SkillAbilityType.LaneMoveSpeed:
                case SkillAbilityType.PushPer:
                    return abilityValueFlag;
                case SkillAbilityType.StartDash:
                case SkillAbilityType.DebuffAbilityValueMultiply:
                case SkillAbilityType.DebuffAbilityValueMultiplyOtherActivate:
                    return abilityValue > 1.0f;
                case SkillAbilityType.TemptationEndTime:
                case SkillAbilityType.StartDelayFix:
                    return true;
                case SkillAbilityType.TemptationPer:
                    return !abilityValueFlag;
                case SkillAbilityType.UpgradeSkillTimeMultiply:
                    return abilityValue < 1.0f;
                default:
                    return false;
            }
        }
    }
}
