using Curator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CuratorTests
{
    public class ListControlTests
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

        //[Fact]
        //public void PlayerPlaysNextSongWhenSongFinishes()
        //{
        //    var player = new Mock<IPlayer>();

        //    var tracks = new[] { "track1", "track2" };
        //    var list = new SongList(player.Object, tracks.ToList(), new List<string>(), new List<string>());

        //    //player.Object.Play("track2");

        //    player.Verify(a => a.Play("track2"));

        //}
    }
}
