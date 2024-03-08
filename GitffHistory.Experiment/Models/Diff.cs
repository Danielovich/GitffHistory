using GitffHistory.Experiment.Domain;

namespace GitffHistory.Experiment.Models
{
    public class Diff
    {
        public Commit Current { get; set; } = new Commit();
        public Commit History { get; set; } = new Commit();
        public EugeneMyers.Item[] Diffs { get; internal set; } = new EugeneMyers.Item[] { };
    }
}
