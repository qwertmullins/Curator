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
            _currentList.ApproveTrack(_currentList.CurrentTrack);
        }

        public void OnThumbDown(object sender, EventArgs e)
        {
            _currentList.ExcludeTrack(_currentList.CurrentTrack);
            _player.Play(_currentList.GetNextTrack());
        }

        public void OnUndo(object sender, EventArgs e)
        {
            _currentList.Undo();
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