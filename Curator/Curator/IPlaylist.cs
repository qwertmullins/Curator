using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curator
{
    interface IPlaylist
    {
        string GetNextTrack();
        string GetPreviousTrack();

        void ApproveTrack(string track);
        void ExcludeTrack(string track);
    }

    class Playlist : IPlaylist
    {
        private List<string> _tracks;
        private int currentIndex;
        private List<string> _approvedTracks;
        private List<string> _rawTracks;
        private List<string> _excludedTracks;

        public Playlist(List<string> rawTracks, List<string> excludedTracks, List<string> approvedTracks)
        {
            _rawTracks = rawTracks;
            _excludedTracks = excludedTracks;
            _approvedTracks = approvedTracks;
            _tracks = rawTracks.Where(a => !_excludedTracks.Contains(a)).ToList();
        }

        void IncrementCurrentTrack()
        {
            currentIndex++;
            if (currentIndex >= _tracks.Count)
                currentIndex = 0;
        }

        void DecrementCurrentTrack()
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = _tracks.Count - 1;
        }
        public string GetNextTrack()
        {
            IncrementCurrentTrack();
            return _tracks[currentIndex];
        }

        public string GetPreviousTrack()
        {
            DecrementCurrentTrack();
            return _tracks[currentIndex];
        }

        public void ApproveTrack(string track)
        {
            _approvedTracks.Add(track);
        }

        public void ExcludeTrack(string track)
        {
            _excludedTracks.Add(track);
        }
    }
}
