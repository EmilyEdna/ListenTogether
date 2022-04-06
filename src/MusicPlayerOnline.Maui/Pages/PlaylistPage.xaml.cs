using MusicPlayerOnline.Maui.ViewModels;

namespace MusicPlayerOnline.Maui.Pages;

public partial class PlaylistPage : ContentPage
{
    PlaylistPageViewModel vm => BindingContext as PlaylistPageViewModel;
    public PlaylistPage(PlaylistPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.InitializeAsync();
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        //TODO �����ܲ���ͨ��command��
        //MenuItem menuItem = sender as MenuItem;
        //MusicDetailViewModel contextItem = menuItem.BindingContext as MusicDetailViewModel;
        //_myModel.RemovePlaylistItem(contextItem);
    }
}