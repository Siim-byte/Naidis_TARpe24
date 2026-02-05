namespace Naidis_TARpe24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(), new FigurePage() };
	public List<string> lehedNimed = new List<string>() { "Tekst", "Joonis" };
	ScrollView sv;
	VerticalStackLayout vst;
    public StartPage()
	{
		Title= "TARpe24";
		vst = new VerticalStackLayout { BackgroundColor = Colors.LightBlue, Padding = 20, Spacing = 15 };
		for (int i = 0; i < lehedNimed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = lehedNimed[i],
				BackgroundColor = Colors.White,
				TextColor = Colors.Black,
				CornerRadius = 10,
				FontSize = 18,
				Padding = new Thickness(10),
				ZIndex = i
            };
			vst.Add(nupp);
			nupp.Clicked += (s, e) =>
			{
				var valik = lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
        }
		
		
		sv = new ScrollView { Content = vst };
		Content = sv;

    }
}