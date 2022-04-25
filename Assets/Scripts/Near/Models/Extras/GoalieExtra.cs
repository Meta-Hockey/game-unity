namespace Near.Models.Extras
{
    public class GoalieExtra : PlayerExtra
    {
        public GoalieStats Stats;
        
        public override Extra GetExtra()
        {
            return this;
        }
    }
}