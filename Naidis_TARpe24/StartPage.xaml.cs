namespace Naidis_TARpe24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(), new FigurePage(), new Timer_Page(), new ValgufoorPage() };
	public List<string> lehedNimed = new List<string>() { "Tekst", "Kujund", "Timer", "Valgusfoor" };
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
				FontSize = 36,
				FontFamily ="Luffio",
				BackgroundColor = Colors.White,
				TextColor = Colors.Black,
				CornerRadius = 10,
				HeightRequest = 60,
				ZIndex = i
            };
			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
        }
		
		
		sv = new ScrollView { Content = vst };
		Content = sv;

    }
}