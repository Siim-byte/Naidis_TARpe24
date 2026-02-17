using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpe24;

public partial class ValgufoorPage : ContentPage
{
	BoxView BoxView;
    Label status;
    bool sisse = false;
    Ellipse foor;
	Ellipse punane;
	Ellipse roheline;
	Ellipse kollane;
	List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi", "Sisse", "Välja" };
	HorizontalStackLayout hsl;
	VerticalStackLayout vsl;
	public ValgufoorPage()
	{
		BoxView = new BoxView()
		{
			WidthRequest = 180,
			HeightRequest = 180,
			BackgroundColor = Colors.Black,
            HorizontalOptions =LayoutOptions.Center
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
            nupp.Clicked += Foorid;
            hsl.Add(nupp);
        }
        vsl = new VerticalStackLayout()
        {
            Padding = 20,
            Spacing = 15,
            Children = { BoxView, punane, roheline, kollane, hsl, status},
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;
        status = new Label()
        {
            Text = "Vali valgus",
            FontSize = 25,
            HorizontalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tap_kollane = new TapGestureRecognizer();
        tap_kollane.NumberOfTapsRequired = 1;
        kollane.GestureRecognizers.Add(tap_kollane);
        tap_kollane.Tapped += (sender, e) =>
        {
            if (sisse)
            {
                status.Text = "Valmistu";
                punane.Fill = new SolidColorBrush(Colors.Gray);
                kollane.Fill = new SolidColorBrush(Colors.Yellow);
                roheline.Fill = new SolidColorBrush(Colors.Gray);
            }
        };
        TapGestureRecognizer tap_roheline = new TapGestureRecognizer();
        tap_roheline.NumberOfTapsRequired = 1;
        roheline.GestureRecognizers.Add(tap_roheline);
        tap_roheline.Tapped += (sender, e) =>
        {
            if (sisse)
            {
                status.Text = "Sõida";
                punane.Fill = new SolidColorBrush(Colors.Gray);
                kollane.Fill = new SolidColorBrush(Colors.Gray);
                roheline.Fill = new SolidColorBrush(Colors.Green);
            }
        };
        TapGestureRecognizer tap_punane = new TapGestureRecognizer();
        tap_punane.NumberOfTapsRequired = 1;
        punane.GestureRecognizers.Add(tap_punane);
        tap_punane.Tapped += (sender, e) =>
        {
            if (sisse)
            {
                status.Text = "Seisa";
                punane.Fill = new SolidColorBrush(Colors.Red);
                kollane.Fill = new SolidColorBrush(Colors.Gray);
                roheline.Fill = new SolidColorBrush(Colors.Gray);
            }
        };
    }
	private void Foorid(object? sender, EventArgs e)
	{
		Button nupp = sender as Button;

        if (nupp.Text == "Sisse")
        {
            sisse = true;
            status.Text = "Vali valgu";
            punane.Fill = new SolidColorBrush(Colors.Red);
            kollane.Fill = new SolidColorBrush(Colors.Yellow);
            roheline.Fill = new SolidColorBrush(Colors.Green);
        }
        else if (nupp.Text == "Välja")
        {
            sisse = false;
            status.Text = "Lülita esmalt foor sisse";
            punane.Fill = new SolidColorBrush(Colors.Gray);
            kollane.Fill = new SolidColorBrush(Colors.Gray);
            roheline.Fill = new SolidColorBrush(Colors.Gray);
        }
    }
}