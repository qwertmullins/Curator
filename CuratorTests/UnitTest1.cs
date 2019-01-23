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
        public void PlayerPlaysNextSongWhenSongFinishes()
        {
            var player = new Mock<IPlayer>();
            var lcv = new ListControlView();
            lcv.

            var tracks = new []{ "track1", "track2" };
            var list = new SongList(tracks.ToList(), new List<string>(), new List<string>());

            //player.Object.Play("track2");

            player.Verify(a => a.Play("track2"));

        }

        [Fact]
        public void PlayerPlaysNextSongWhenSongFinishes()
        {
            var player = new Mock<IPlayer>();

            var tracks = new[] { "track1", "track2" };
            var list = new SongList(player.Object, tracks.ToList(), new List<string>(), new List<string>());

            //player.Object.Play("track2");

            player.Verify(a => a.Play("track2"));

        }
    }
}
