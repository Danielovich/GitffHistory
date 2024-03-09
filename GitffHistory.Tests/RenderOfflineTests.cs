
using GitffHistory.Experiment.Offline;

namespace GitffHistory.Tests
{
    public class RenderOfflineTests
    {
        [Fact]
        public async Task dd()
        {
            RenderOffline ro = new RenderOffline();
            var html = await ro.Render();

            var path = string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                Path.DirectorySeparatorChar,
                "Offline",
                Path.DirectorySeparatorChar,
                Guid.NewGuid(),
                ".html");

            await File.WriteAllTextAsync(path, html);
        }
    }
}
