using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace Naidis_TARpe24;

public partial class lumememm : ContentPage
{
    AbsoluteLayout al;
    Ellipse pea;
    Ellipse keha;
    Frame ämber;
    Picker tegevusPicker;
    Button tegevusButton;
    Label tegevusLabel;
    Slider opacitySlider;

    public lumememm()
    {
        al = new AbsoluteLayout();
        keha = new Ellipse
        {
            WidthRequest = 100,
            HeightRequest = 100,
            Fill = Colors.White,
            Stroke = Colors.Gray,
            StrokeThickness = 2
        };
        AbsoluteLayout.SetLayoutBounds(keha, new Rect(0.5, 0.57, -1, -1));
        AbsoluteLayout.SetLayoutFlags(keha, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(keha);

        pea = new Ellipse
        {
            WidthRequest = 70,
            HeightRequest = 70,
            Fill = Colors.White,
            Stroke = Colors.Gray,
            StrokeThickness = 2
        };
        AbsoluteLayout.SetLayoutBounds(pea, new Rect(0.5, 0.45, -1, -1));
        AbsoluteLayout.SetLayoutFlags(pea, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(pea);

        ämber = new Frame
        {
            WidthRequest = 60,
            HeightRequest = 30,
            BackgroundColor = Colors.Gray,
            BorderColor = Colors.Gray,
            CornerRadius = 0
        };
        AbsoluteLayout.SetLayoutBounds(ämber, new Rect(0.5, 0.40, -1, -1));
        AbsoluteLayout.SetLayoutFlags(ämber, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(ämber);

        tegevusPicker = new Picker
        {
            Title = "Vali tegevus",
            ItemsSource = new string[] { "Peida", "Näita", "Muuda värvi", "Sulata", "Tantsi" },
            HorizontalOptions = LayoutOptions.Center
        };
        AbsoluteLayout.SetLayoutBounds(tegevusPicker, new Rect(0.5, 0.8, 200, -1));
        AbsoluteLayout.SetLayoutFlags(tegevusPicker, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(tegevusPicker);

        tegevusButton = new Button
        {
            Text = "Käivita tegevus"
        };
        tegevusButton.Clicked += KäivitaTegevus;
        AbsoluteLayout.SetLayoutBounds(tegevusButton, new Rect(0.5, 0.85, 150, -1));
        AbsoluteLayout.SetLayoutFlags(tegevusButton, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(tegevusButton);

        tegevusLabel = new Label
        {
            Text = "Tegevus:",
            HorizontalOptions = LayoutOptions.Center
        };
        AbsoluteLayout.SetLayoutBounds(tegevusLabel, new Rect(0.5, 0.9, -1, -1));
        AbsoluteLayout.SetLayoutFlags(tegevusLabel, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(tegevusLabel);

        opacitySlider = new Slider
        {
            Minimum = 0,
            Maximum = 1,
            Value = 1
        };
        opacitySlider.ValueChanged += (sender, e) =>
        {
            pea.Opacity = e.NewValue;
            keha.Opacity = e.NewValue;
            ämber.Opacity = e.NewValue;
        };
        AbsoluteLayout.SetLayoutBounds(opacitySlider, new Rect(0.5, 0.95, 250, -1));
        AbsoluteLayout.SetLayoutFlags(opacitySlider, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(opacitySlider);
        Content = al;
    }

    async void KäivitaTegevus(object sender, EventArgs e)
    {
        if (tegevusPicker.SelectedItem == null)
            return;

        string tegevus = tegevusPicker.SelectedItem.ToString();
        tegevusLabel.Text = $"Tegevus: {tegevus}";

        switch (tegevus)
        {
            case "Peida":
                pea.IsVisible = false;
                keha.IsVisible = false;
                ämber.IsVisible = false;
                break;

            case "Näita":
                pea.IsVisible = true;
                keha.IsVisible = true;
                ämber.IsVisible = true;
                break;

            case "Muuda värvi":
                Random rnd = new Random();
                Color randomColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                pea.Fill = randomColor;
                keha.Fill = randomColor;
                break;

            case "Sulata":
                pea.FadeTo(0);
                keha.FadeTo(0) ;
                ämber.FadeTo(0);
                break;

            case "Tantsi":
                await Tantsi();
                break;
        }
    }
    async Task Tantsi()
    {
        // Liikumine edasi
        await Task.WhenAll(
            pea.TranslateTo(20, 0, 500),
            keha.TranslateTo(20, 0, 500),
            ämber.TranslateTo(20, 0, 500)
        );

        // Liikumine tagasi
        await Task.WhenAll(
            pea.TranslateTo(-20, 0, 500),
            keha.TranslateTo(-20, 0, 500),
            ämber.TranslateTo(-20, 0, 500)
        );
    }
}