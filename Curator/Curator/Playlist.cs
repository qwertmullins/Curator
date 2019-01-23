using System;
using System.Collections.Generic;
using System.Linq;

namespace Curator
{
    public class SongList : ISongList
    {
        private List<string> _tracks;
        private int currentIndex;
        private List<string> _approvedTracks;
        private List<string> _rawTracks;
        private List<string> _excludedTracks;
        private Stack<Func<string>> _undoStack;

        public SongList(IPlayer @object, List<string> rawTracks, List<string> excludedTracks, List<string> approvedTracks)
        {
            _rawTracks = rawTracks;
            _excludedTracks = excludedTracks;
            _approvedTracks = approvedTracks;
            _tracks = rawTracks.Where(a => !_excludedTracks.Contains(a)).ToList();
            _undoStack = new Stack<Func<string>>();
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
            _undoStack.Push(() =>
            {
                DecrementCurrentTrack();
                return _tracks[currentIndex];
            });
            return _tracks[currentIndex];
        }

        public string GetPreviousTrack()
        {
            DecrementCurrentTrack();
            _undoStack.Push(() =>
            {
                IncrementCurrentTrack();
                return _tracks[currentIndex];
            });
            return _tracks[currentIndex];
        }

        public void ApproveTrack(string track)
        {
            _approvedTracks.Add(track);
            _undoStack.Push(() =>
            {
                _approvedTracks.Remove(track);
                return null;
            });
        }

        public void ExcludeTrack(string track)
        {
            _excludedTracks.Add(track);
            _undoStack.Push(() =>
            {
                _excludedTracks.Remove(track);
                return track;
            });
        }

        public string Undo()
        {
            if (CanUndo)
                return _undoStack.Pop()();
            throw new Exception("Tried to undo with nothing to undo!");
        }

        public bool CanUndo => _undoStack.Any();
    }
}
