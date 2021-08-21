namespace csharp.Rules
{
    public interface IRule
    {
        bool IsApplicable(Item item);
        void ApplyRule(Item item);
    }
}