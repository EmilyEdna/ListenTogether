using MusicPlayerOnline.Maui.ViewModels;

namespace MusicPlayerOnline.Maui.Pages;

public partial class PlaylistPage : ContentPage
{
	public PlaylistPage(PlaylistPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void Entry_Completed(object sender, EventArgs e)
    {
        //TODO ���������������ܲ���ͨ��command��
        //_myModel.Search();
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        //TODO �����ܲ���ͨ��command��
        //MenuItem menuItem = sender as MenuItem;
        //MusicDetailViewModel contextItem = menuItem.BindingContext as MusicDetailViewModel;
        //_myModel.RemovePlaylistItem(contextItem);
    }
}