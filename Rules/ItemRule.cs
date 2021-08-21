using System;

namespace csharp.Rules
{
    public class ItemRule : IRule
    {
        private readonly Func<Item, bool> rulePredicate;
        private readonly Action<Item> ruleAction;

        public ItemRule(Func<Item, bool> rulePredicate, Action<Item> ruleAction)
        {
            this.rulePredicate = rulePredicate;
            this.ruleAction = ruleAction;
        }
        
        public bool IsApplicable(Item item)
        {
            return rulePredicate(item);
        }

        public void ApplyRule(Item item)
        {
            ruleAction(item);
        }
    }
}