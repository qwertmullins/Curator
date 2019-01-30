using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListControlView : ContentView
    {
        private ISongList _currentList;
        private IPlayer _player;
        private Action _undo;
        public ListControlView()
        {
            InitializeComponent();
        }

        public void Init(ISongList list, IPlayer player)
        {
            _currentList = list;
            _player = player;
            _player.Play(list.CurrentTrack);
        }

        public void OnThumbUp(object sender, EventArgs e)
        {
            _undo = () => { _currentList.UnApproveTrack(_currentList.CurrentTrack); };
            _currentList.ApproveTrack(_currentList.CurrentTrack);
        }

        public void OnThumbDown(object sender, EventArgs e)
        {
            var badTrack = _currentList.CurrentTrack;
            _undo = () =>
            {
                _currentList.IncludeTrack(badTrack);
                _player.Play(badTrack);
            };
            _currentList.ExcludeTrack(badTrack);
            _player.Play(_currentList.GetNextTrack());
        }

        public bool CanUndo => _undo != null;

        public void OnUndo(object sender, EventArgs e)
        {
            if (CanUndo)
            {
                _undo();
                _undo = null;
            }
        }

        public void OnBack(object sender, EventArgs e)
        {
            _player.Play(_currentList.GetPreviousTrack());
        }

        public void OnPausePlay(object sender, EventArgs e)
        {
            _player.PausePlay();
        }

        public void OnSkip(object sender, EventArgs e)
        {
            _player.Play(_currentList.GetNextTrack());
        }
    }
}