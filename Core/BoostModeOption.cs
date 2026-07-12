namespace frm_PowerPlanTweak.Core
{
    /// <summary>
    /// One entry for the Boost Mode combo box. Value maps directly to the PERFBOOSTMODE power
    /// setting index accepted by powercfg (0=Disabled, 1=Enabled, 2=Aggressive, 3=Efficient Enabled,
    /// 4=Efficient Aggressive).
    /// </summary>
    internal sealed class BoostModeOption
    {
        public string DisplayName { get; }
        public int Value { get; }

        public BoostModeOption(string displayName, int value)
        {
            DisplayName = displayName;
            Value = value;
        }

        public override string ToString() => DisplayName;

        public static BoostModeOption[] All => new[]
        {
            new BoostModeOption("Disabled", 0),
            new BoostModeOption("Enabled", 1),
            new BoostModeOption("Aggressive", 2),
            new BoostModeOption("Efficient Enabled", 3),
            new BoostModeOption("Efficient Aggressive", 4)
        };
    }
}
