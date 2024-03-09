using GitffHistory.Experiment.Domain;

namespace GitffHistory.Experiment.Models
{
    /// <summary>
    /// This type is a representation of two commits and the changes between them.
    /// </summary>
    public class Diff
    {
        public Commit Current { get; set; } = new Commit();
        public Commit Parent { get; set; } = new Commit();
        public EugeneMyersDiffAlgorithm.Item[] Diffs { get; internal set; } = new EugeneMyersDiffAlgorithm.Item[] { };
    }
}
