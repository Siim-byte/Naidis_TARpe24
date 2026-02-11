using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpe24;

public partial class ValgufoorPage : ContentPage
{
	BoxView BoxView;
	Ellipse foor;
	Ellipse punane;
	Ellipse roheline;
	Ellipse kollane;
	List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi", "Sisse", "Välja" };
	HorizontalStackLayout hsl;
	VerticalStackLayout vsl;
	public ValgusFoorPage()
	{
		foor = new Ellipse()
		{
			WidthRequest = 180,
			HeightRequest = 180,
			Fill=new SolidColorBrush(Colors.Gray),
			StrokeThickness =5,
			HorizontalOptions=LayoutOptions.Center
		};
		punane = new Ellipse()
		{
            WidthRequest = 180,
            HeightRequest = 180,
            Fill = new SolidColorBrush(Colors.Red),
            StrokeThickness = 5,
            HorizontalOptions = LayoutOptions.Center
        };
		roheline = new Ellipse()
		{
            WidthRequest = 180,
            HeightRequest = 180,
            Fill = new SolidColorBrush(Colors.Green),
            StrokeThickness = 5,
            HorizontalOptions = LayoutOptions.Center
        };
		kollane = new Ellipse()
		{
            WidthRequest = 180,
            HeightRequest = 180,
            Fill = new SolidColorBrush(Colors.Yellow),
            StrokeThickness = 5,
            HorizontalOptions = LayoutOptions.Center
        };
		hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center };
        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button()
            {
                Text = nupud[j],
                FontSize = 28,
                FontFamily = "Luffio",
                TextColor = Colors.BlueViolet,
                BackgroundColor = Colors.LightGray,
                CornerRadius = 10,
                HeightRequest = 50,
                ZIndex = j
            };
            hsl.Add(nupp);
        }
        vsl = new VerticalStackLayout()
        {
            Padding = 20,
            Spacing = 15,
            Children = { BoxView, punane, roheline, kollane, hsl},
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;
	}
	private void Foorid(object? sender, EventArgs e)
	{
		Button nupp = sender as Button;
	}
}