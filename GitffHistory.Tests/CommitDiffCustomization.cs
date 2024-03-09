namespace GitffHistory.Tests;
public class CommitDiffCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        CommitReaderStub readerStub = new CommitReaderStub();
        var commits = readerStub.LoadCommits();
        fixture.Register(() => commits);

        CommitDiffer commitDiffer = new CommitDiffer();
        var diffs = commitDiffer.BuildParentCurrentHierachi(commits);
        fixture.Register(() => diffs);
    }
}