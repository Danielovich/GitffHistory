
using GitffHistory.Experiment.Offline;

namespace GitffHistory.Tests
{
    public class RenderOfflineTests
    {
        [Fact]
        public async Task Rendering_Results_In_HTML()
        {
            RenderOffline ro = new RenderOffline();
            var html = await ro.Render();

            Assert.True(html.Length > 0);

#if DEBUG
            var path = string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                Path.DirectorySeparatorChar,
                "Offline",
                Path.DirectorySeparatorChar,
                Guid.NewGuid(),
                ".html");

            await File.WriteAllTextAsync(path, html);
#endif
        }
    }
}

file endpoint
https://api.github.com/repos/izuzak/pmrpc/commits?path=README.markdown