using GitffHistory.Experiment.IO;
using GitffHistory.Experiment.Models;

namespace GitffHistory.Experiment.Domain
{
    public interface IDiffFiles
    {
        List<Diff> Diff(List<Diff> diffs);
        List<Diff> BuildParentCurrentHierachi(List<Commit> commits);
    }

    public class CommitDiffer : IDiffFiles
    {
        public List<Diff> Diff(List<Diff> diffs)
        {
            EugeneMyersDiffAlgorithm diffAlgorithm = new();

            foreach (var d in diffs)
            {
                if(!string.IsNullOrEmpty(d.Parent.RawFileContents) && !string.IsNullOrEmpty(d.Current.RawFileContents))
                {
                    d.Diffs = diffAlgorithm.DiffText(d.Parent.RawFileContents, d.Current.RawFileContents);
                }
            }
            
            return diffs;
        }

        public List<Diff> BuildParentCurrentHierachi(List<Commit> commits)
        {
            List<Diff> result = new();

            for (var i = 0; i < commits.Count(); i++)
            {
                Diff diff = new();

                diff.Parent = commits[i];
                if (i + 1 < commits.Count())
                {
                    diff.Current = commits[i + 1];
                }

                result.Add(diff);
            }

            return result;
        }
    }
}
