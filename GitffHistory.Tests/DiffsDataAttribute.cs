namespace GitffHistory.Tests;

public class DiffsDataAttribute : AutoDataAttribute
{
    public DiffsDataAttribute()
        : base(() => new Fixture().Customize(new CommitDiffCustomization()))
    {
    }
}