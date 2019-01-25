using Curator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CuratorTests
{
    public class GeneralTests
    {

        [Fact]
        public void PlayerBeginsPlayingUponListControlInit()
        {
            var player = new Mock<IPlayer>();
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            player.Verify(a => a.Play(list.CurrentTrack));
        }
    }
}
