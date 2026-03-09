namespace Naidis_TARpe24;

public partial class PickerImageGridPage : ContentPage
{
	Grid gr4x1, gr3x3;
	Picker picker;
	Image img;
	Switch s_pilt, s_grid;
	Random rnd = new Random();
	public PickerImageGridPage()
	{
		gr4x1 = new Grid()
		{
			RowDefinitions =
			{
				new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
				new RowDefinition {Height = new GridLength(3, GridUnitType.Star)},
				new RowDefinition {Height = new GridLength(3, GridUnitType.Star)},
				new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
			},
			ColumnDefinitions =
			{
				new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star)},
				new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star)},
			},
		};
		picker = new Picker()
		{
			Title = "Vali pilt",
			ItemsSource = new List<string> { "Pilt 1", "Pilt 2", "Pilt 3" },
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center
		};
		picker.SelectedIndexChanged += Piltide_valik;
		img = new Image()
		{
			Source = "dotnet_bot.png",
			HorizontalOptions = LayoutOptions.Center,
		};
		s_pilt = new Switch
		{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			IsToggled = true,
			IsEnabled = true
		};
		s_pilt.Toggled += (sender, e) =>
		{
			if (e.Value)
			{
				img.IsVisible = true;
			}
			else
			{
				img.IsVisible = false;
			}
		};
		s_grid = new Switch()
		{
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
			IsToggled = false,
			IsEnabled = true
        };
		s_grid.Toggled += (sender, e) =>
		{
			if (e.Value)
			{
				//gr3x3 = Täida_gr3x3();//kutsuime välja funktsiooni, mis loob 3x3 gridi ja lisab sinna kastid, mis muudavad värvi

				gr4x1.Add(gr3x3, 0, 2);
				gr4x1.SetColumnSpan(gr3x3, 2);
			}
			else
			{
				gr4x1.RemoveAt(4);
			}
		};
	}
	private void Piltide_valik(object? sender, EventArgs e)
	{
		if (picker.SelectedIndex == -1) return; //kui midagi pole valitud, siis ei tee midagi
		if (picker.SelectedIndex == 0) img.Source = "pilt1.jpg";
		else if (picker.SelectedIndex == 1) img.Source = "pilt2.jpg";
		else if (picker.SelectedIndex == 2) img.Source = "pilt3.jpg";
	}
}