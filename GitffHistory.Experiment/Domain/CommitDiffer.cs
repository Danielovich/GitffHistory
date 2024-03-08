using GitffHistory.Experiment.IO;
using GitffHistory.Experiment.Models;

namespace GitffHistory.Experiment.Domain
{
    public interface IDiffFiles
    {
        List<Diff> Diff();
    }

    public class CommitDiffer : IDiffFiles
    {
        private readonly IReadFiles<Commit> commitReader;

        public CommitDiffer(IReadFiles<Commit> commitReader)
        {
            this.commitReader = commitReader;
        }

        public List<Diff> Diff()
        {
            var files = commitReader.ReadFilesFromDirectory();
            var result = new List<Diff>();
            EugeneMyers eugeneMyers = new EugeneMyers(); // yes yes, we could interface this

            for (var i = 0; i < files.Count; i++)
            {
                var diff = new Diff();
                var history = files[i];
                diff.History = history;

                if (i + 1 < files.Count)
                {
                    var current = files[i + 1];
                    diff.Diffs = eugeneMyers.DiffText(history.RawFileContents, current.RawFileContents);
                    diff.Current = current;
                }

                result.Add(diff);
            }

            return result;
        }
    }
}
