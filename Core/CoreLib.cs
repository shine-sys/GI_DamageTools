namespace Core
{
    public class DamageCalcLib
    {
        public static double GetTotalAttack(double baseAttack, double additionalAttack)
        {
            return baseAttack + additionalAttack;
        }

        public static double GetDefMultiplier(double characterLevel, double defReduction, double defIgnore, double enemyDefense)
        {
            double denominator = ((1 - defReduction) * (1 - defIgnore) * (enemyDefense + 100)) + (characterLevel + 100);
            if (denominator == 0) return 1.0; // or throw exception
            return (characterLevel + 100) / denominator;
        }

        public static double GetResistance(double enemyResistance, double resReduction)
        {
            return enemyResistance - resReduction;
        }

        public static double GetResMultiplier(double resMultiplier, double resistance)
        {
            double denominator = (resistance < 0 ? 1 - resistance / 2 : resistance < 0.75 ? 1 - resistance : 1 / (4 * resistance + 1));
            if (denominator == 0) return 1.0;
            return denominator;
        }

        public static double GetcritMultiplier(double critDamage)
        {
            return 1 + critDamage;
        }

        public static double GetnonCritDamage(double totalAttack, double multiplier, double dmgBonus, double resMultiplier, double defMultiplier)
        {
            return totalAttack* multiplier *(1 + dmgBonus) * defMultiplier * resMultiplier;
        }

        public static double GetcritDamageValue(double critMultiplier, double nonCritDamage)
        {
            return nonCritDamage * critMultiplier;
        }
        public static double GetexpectedDamage(double critRate, double critDamage, double nonCritDamage)
        {
            return nonCritDamage * (1 + critRate * critDamage);
        }
    }

    public class ScoreCalcLib
    {
        public double GetcritMultiplier(double critDamage, double critRate, double baseAttack)
        {
            return (critDamage + critRate * 2 + baseAttack) * 10 / 10;
        }

        public double GetdefMultiplier(double critDamage, double critRate, double def)
        {
            return (critDamage + critRate * 2 + def) * 10 / 10;
        }

        public double GethpMultiplier(double critDamage, double critRate, double hp)
        {
            return (critDamage + critRate * 2 + hp) * 10 / 10;
        }

        public double GetchargeMultiplier(double critDamage, double critRate, double charge)
        {
            return (critDamage + critRate * 2 + charge) * 10 / 10;
        }
        public double GetJyukutiMultiplier(double critDamage, double critRate, double jyukuti)
        {
            return (critDamage + critRate * 2 + jyukuti * 0.25) * 10 / 10;
        }
    }

    public class NumOperation
    {
        // パーセント入力を処理
        public static double ParsePercent(string input)
        {
            if (input.EndsWith("%"))
            {
                input = input.Replace("%", "").Trim();
                if (double.TryParse(input, out double value))
                    return value / 100.0;
            }
            else if (double.TryParse(input, out double value))
            {
                return value;
            }
            throw new FormatException("無効なパーセント形式です。");
        }
    }
}

