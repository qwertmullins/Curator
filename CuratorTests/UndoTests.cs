using Curator;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CuratorTests
{
    public class UndoTests
    {
        [Fact]
        public void UndoThumbsUpRemovesSongFromApprovedList()
        {
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, new Mock<IPlayer>().Object);
            lcv.OnThumbUp(null, null);
            lcv.OnUndo(null, null);
            Assert.DoesNotContain("track1", list.ApprovedTracks);
        }

        [Fact]
        public void UndoThumbsUpDoesNotChangePlayingSong()
        {
            var player = new Mock<IPlayer>();
            var isPlaying = false;
            var currentTrack = "";
            player.Setup(a => a.Play(It.IsAny<string>())).Callback<string>(track =>
            {
                isPlaying = true;
                currentTrack = track;
            });
            var list = new SongList(new[] { "track1", "track2" }.ToList(), new List<string>(), new List<string>());
            var lcv = new ListControlView();
            lcv.Init(list, player.Object);
            lcv.OnThumbUp(null, null);
            lcv.OnUndo(null, null);
            Assert.Equal("track1", currentTrack);
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
