using Curator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CuratorTests
{
    public class UndoTests
    {
        [Fact]
        public void UndoThumbsUpRemovesSongFromApprovedList()
        {
            var player = new Mock<IPlayer>();
            var tracks = new[] { "track1", "track2" };
            var list = new SongList(tracks.ToList(), new List<string>(), new List<string>());
            player.Verify(a => a.Play("track2"));
            throw new NotImplementedException();
        }

        [Fact]
        public void UndoThumbsUpDoesNotChangePlayingSong()
        {
            var player = new Mock<IPlayer>();
            var tracks = new[] { "track1", "track2" };
            var list = new SongList(tracks.ToList(), new List<string>(), new List<string>());
            player.Verify(a => a.Play("track2"));
            throw new NotImplementedException();
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
