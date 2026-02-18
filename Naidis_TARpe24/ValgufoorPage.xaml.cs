using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpe24;

public partial class ValgufoorPage : ContentPage
{
    Label status;
    bool sisse = false;
	Ellipse punane;
	Ellipse roheline;
	Ellipse kollane;
	List<string> nupud = new List<string>() { "SISSE", "VÄLJA", "ÖÖREZIIM", };
	HorizontalStackLayout hsl;
	VerticalStackLayout vsl;

	public ValgufoorPage()
	{
        punane = new Ellipse()
		{
            WidthRequest = 180,
            HeightRequest = 180,
            Fill = new SolidColorBrush(Colors.Red),
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
        roheline = new Ellipse()
		{
            WidthRequest = 180,
            HeightRequest = 180,
            Fill = new SolidColorBrush(Colors.Green),
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
        status = new Label()
        {
            Text = "Vali valgus",
            FontSize = 25,
            FontFamily = "Luffio",
            HorizontalOptions = LayoutOptions.Center
        };
        vsl = new VerticalStackLayout()
        {
            Padding = 20,
            Spacing = 15,
            Children = {  punane, kollane, roheline, hsl, status},
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;

        TapGestureRecognizer tap_kollane = new TapGestureRecognizer();
        tap_kollane.NumberOfTapsRequired = 1;
        kollane.GestureRecognizers.Add(tap_kollane);
        tap_kollane.Tapped += (sender, e) =>
        {
            if (sisse == true)
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
            if (sisse == true)
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
            if (sisse == true)
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

        if (nupp.ZIndex == 0)
        {
            sisse = true;
            status.Text = "Vali valgus";
            this.BackgroundImageSource = null;
            punane.Fill = new SolidColorBrush(Colors.Red);
            kollane.Fill = new SolidColorBrush(Colors.Yellow);
            roheline.Fill = new SolidColorBrush(Colors.Green);
        }
        else if (nupp.ZIndex == 1)
        {
            sisse = false;
            status.Text = "Lülita esmalt foor sisse";
            punane.Fill = new SolidColorBrush(Colors.Gray);
            kollane.Fill = new SolidColorBrush(Colors.Gray);
            roheline.Fill = new SolidColorBrush(Colors.Gray);
        }
        else if (nupp.ZIndex == 2)
        {
            punane.Fill = new SolidColorBrush(Colors.Yellow);
            kollane.Fill = new SolidColorBrush(Colors.Yellow);
            roheline.Fill = new SolidColorBrush(Colors.Yellow);
            this.BackgroundImageSource = "night.jpg";
            roheline.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Task.WhenAll(
                        punane.ScaleToAsync(1.2, 150),
                        punane.FadeToAsync(0.5, 150),
                        kollane.ScaleToAsync(1.2, 150),
                        kollane.FadeToAsync(0.5, 150),
                        roheline.ScaleToAsync(1.2, 150),
                        roheline.FadeToAsync(0.5, 150)
                    );
                    await Task.WhenAll(
                        punane.ScaleToAsync(1.0, 150),
                        punane.FadeToAsync(1.0, 150),
                        roheline.ScaleToAsync(1.0, 150),
                        roheline.FadeToAsync(1.0, 150),
                        kollane.ScaleToAsync(1.0, 150),
                        kollane.FadeToAsync(1.0, 150)
                    );
                    status.Text = "Seisa!";
                })
            });
        }
    }
}