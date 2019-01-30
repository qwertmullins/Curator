using Curator;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CuratorTests
{
    public class ThumbsUpTests
    {
        [Fact]
        public void ThumbUpContinuesPlayingCurrentSong()
        {
            var player = new Mock<IPlayer>();
            var isPlaying = false;
            var currentTrack = "";
            player.Setup(a => a.Play(It.IsAny<string>())).Callback<string>(track =>
            {
                isPlaying = true;
                currentTrack = track;
            });
            player.Setup(a => a.PausePlay()).Callback(() =>
            {
                isPlaying = !isPlaying;
            });
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            var thumbedUpTrack = list.CurrentTrack;
            lcv.OnThumbUp(null, null);
            Assert.True(isPlaying);
            Assert.Equal(thumbedUpTrack, currentTrack);
        }

        [Fact]
        public void ThumbsUpAddsSongToApprovedList()
        {
            var player = new Mock<IPlayer>();
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            lcv.OnThumbUp(null, null);
            Assert.Contains("track1", list.ApprovedTracks);
        }
    }
}
