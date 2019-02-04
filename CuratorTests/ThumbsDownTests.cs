using Curator;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CuratorTests
{
    public class ThumbsDownTests
    {
        [Fact]
        public void ThumbsDownAddsSongToExcludeList()
        {
            var player = new Mock<IPlayer>();
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            lcv.OnThumbDown(null, null);
            Assert.Contains("track1", list.ExcludedTracks);
        }

        [Fact]
        public void ThumbsDownRemovesSongFromTrackList()
        {
            var player = new Mock<IPlayer>();
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            lcv.OnThumbDown(null, null);
            Assert.DoesNotContain("track1", list.Tracks);
        }

        [Fact]
        public void ThumbsDownGoesToNextSong()
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
            lcv.OnThumbDown(null, null);
            Assert.True(isPlaying);
            Assert.Equal("track2", currentTrack);
        }


        [Fact]
        public void ExcludedSongWillNotPlayAgain()
        {
            var player = new Mock<IPlayer>();
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            lcv.OnThumbDown(null, null);
            for (int i = 0; i < 3; i++)
            {
                lcv.OnSkip(null, null);
                Assert.NotEqual("track1", list.CurrentTrack);
            }
        }


        [Fact]
        public void UndoThumbsDownRemovesSongFromExcludeList()
        {
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, new Mock<IPlayer>().Object);
            lcv.OnThumbDown(null, null);
            lcv.OnUndo(null, null);
            Assert.DoesNotContain("track1", list.ExcludedTracks);
        }

        [Fact]
        public void UndoThumbsDownAddsSongToIncludeList()
        {
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, new Mock<IPlayer>().Object);
            lcv.OnThumbDown(null, null);
            lcv.OnUndo(null, null);
            Assert.Contains("track1", list.Tracks);
        }

        [Fact]
        public void UndoThumbsDownDoesNotAddSongToApprovedList()
        {
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, new Mock<IPlayer>().Object);
            lcv.OnThumbDown(null, null);
            lcv.OnUndo(null, null);
            Assert.DoesNotContain("track1", list.ApprovedTracks);
        }

        [Fact]
        public void UndoThumbsDownCausesSongToPlayAgain()
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
            lcv.OnThumbDown(null, null);
            lcv.OnUndo(null, null);
            Assert.Equal("track1", currentTrack);
        }
    }
}
