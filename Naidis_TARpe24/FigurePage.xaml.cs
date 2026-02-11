using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpe24;

public partial class FigurePage : ContentPage
{
	BoxView BoxView;
	Ellipse pall;
	Polygon kolmnurk;
	Random rnd = new Random();
	HorizontalStackLayout hsl;
	List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
	VerticalStackLayout vsl;
	Point A;
	Point B;
	Point C;
    public FigurePage()
	{
		//BoxView kasutamine
		int r = rnd.Next(256);
		int g = rnd.Next(256);
		int b = rnd.Next(256);
		BoxView = new BoxView()
		{
			Color = Color.FromRgb(r, g, b),
			WidthRequest = 200,
			HeightRequest = 200,
			HorizontalOptions = LayoutOptions.Center,
			BackgroundColor=Color.FromRgba(0,0,0,0),
			CornerRadius = 30,
		};
		TapGestureRecognizer tap = new TapGestureRecognizer();
		BoxView.GestureRecognizers.Add(tap);
		tap.Tapped += (sender, e) =>
		{
			int r = rnd.Next(256);
			int g = rnd.Next(256);
			int b = rnd.Next(256);
			BoxView.Color = Color.FromRgb(r, g, b);
			BoxView.WidthRequest = BoxView.Width + 20;
			BoxView.HeightRequest = BoxView.Height + 20;
			if (BoxView.WidthRequest > (int)DeviceDisplay.MainDisplayInfo.Width / 3)
			{
				BoxView.WidthRequest = 200;
				BoxView.HeightRequest = 200;
			}
		};
		//ellipse kasutamine
		pall = new Ellipse()
		{
			WidthRequest = 200,
			HeightRequest = 200,
			Fill=new SolidColorBrush(Color.FromRgb(b, g,r)),
			Stroke=Colors.BurlyWood,
			StrokeThickness=5,
			HorizontalOptions=LayoutOptions.Center
		};
		pall.GestureRecognizers.Add(tap);
		//polygon kasutamine
		A = new Point(0, 200); //vasak all
		B = new Point(100, 0); //keskel
		C = new Point(200, 200); //paremal all
		kolmnurk = new Polygon()
		{
			Points = new PointCollection()
			{
				A,
				B,
				C
			},
			Fill = new SolidColorBrush(Color.FromRgb(g, b ,r)), //kujundi värv brushi abil
			Stroke = Colors.Aquamarine, //äärise värv
			StrokeThickness = 5, //äärise paksus 
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center
		};
		TapGestureRecognizer tap_kolmnurk = new TapGestureRecognizer();
		tap_kolmnurk.NumberOfTapsRequired = 2; //double tap
		kolmnurk.GestureRecognizers.Add(tap_kolmnurk);
		tap_kolmnurk.Tapped += (sender, e) =>
		{
			A = new Point(0, 300); B = new Point(200, 0); C = new Point(300, 300);
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
            nupp.Clicked += Liikumine;
        }
        vsl = new VerticalStackLayout()
        {
            Padding = 20,
            Spacing = 15,
            Children = { BoxView, pall, kolmnurk, hsl},
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PopAsync();
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new FigurePage());
        }
    }
}