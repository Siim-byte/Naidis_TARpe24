using Microsoft.Maui.Controls;
using Naidis_TARpe24.Localization;
using Naidis_TARpe24.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Naidis_TARpe24;

public class Galerii : ContentPage
{
    public class CarouselItem

    {

        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string HelloWorld { get; set; }

    }

    private CarouselView carouselView;

    // List asendati ObservableCollectioniga

    private ObservableCollection<CarouselItem> items;

    private int position = 0;

    public Galerii()

    {
        Title = AppResources.GreetingText;
        //Title = "Programmeerimiskeelte portfoolio 💻";
        


        // Initsialiseerime ObservableCollectioni

        items = new ObservableCollection<CarouselItem>

        {

            new CarouselItem { Title = "C#", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse2.mm.bing.net%2Fth%2Fid%2FOIP.pr6KrbRwFHy2S4g_g2FlRgHaEK%3Fpid%3DApi&f=1&ipt=2c515cc058e2d3fe5c71ce398dd91d0d4f7c9d948d37025ab015ddbba7097645&ipo=images", Description = AppResources.CSharpDesc, HelloWorld = "Console.WriteLine(\"Hello, World!\");" },

            new CarouselItem { Title = "Python", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%2Fid%2FOIP.UoAILAx3sCg7Qu36-okV5wHaEK%3Fpid%3DApi&f=1&ipt=b8e47e08947985e8103717bc66ecabd9c2c92999e603786757f414146f9c6ffc&ipo=images", Description = AppResources.PythonDesc, HelloWorld = "print(\"Hello World!\")" },

            new CarouselItem { Title = "JavaScript", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fres.cloudinary.com%2Fteepublic%2Fimage%2Fprivate%2Fs--NWgv-y2Y--%2Ft_Preview%2Ft_watermark_lock%2Fb_rgb%3Affb81c%2Cc_lpad%2Cf_jpg%2Ch_630%2Cq_90%2Cw_1200%2Fv1617189178%2Fproduction%2Fdesigns%2F20726646_0.jpg&f=1&nofb=1&ipt=d19bf297c16522941e023b0794596131d263c04c4a9c7b39e2010c3a48e857a5", Description = AppResources.JavaScriptDesc, HelloWorld = "console.log(\"Hello, World!\");"},

            new CarouselItem { Title = "Java", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2F736x%2Fda%2Fa1%2F7b%2Fdaa17b58c629a99d80ef620582b10c1c.jpg&f=1&nofb=1&ipt=49c94b2c9f03f3560c7c89e224b2e7fe3ae924188e08a34857b64486954efe47", Description = AppResources.JavaDesc, HelloWorld = "void main() {\r\n    System.out.println(\"Hello, World!\");\r\n}"},

            new CarouselItem { Title = "C++", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse2.mm.bing.net%2Fth%2Fid%2FOIP.UkzB6-z3fB1WZ7HhmGKehAHaEK%3Fpid%3DApi&f=1&ipt=259e90860507557a8ffa053cd6bb1be1d6d1eeeafc011115836b515e1e1e99c3&ipo=images", Description = AppResources.CPlusDesc, HelloWorld = "\r\nint main() {\r\n    std::cout << \"Hello, World!\" << std::endl;\r\n    return 0;\r\n}"}

        };

        var btnEn = new Button { Text = AppResources.EnglishButton, Command = new Command(() => ChangeLanguage("en")) };
        var btnEt = new Button { Text = AppResources.EstonianButton, Command = new Command(() => ChangeLanguage("et")) };

        var languageStack = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Children = { btnEn, btnEt }
        };
        // Karusselli loomine (kood on sama, mis eelmises versioonis)

        carouselView = new CarouselView

        {

            ItemsSource = items,

            HeightRequest = 350,

            PeekAreaInsets = new Thickness(40, 0, 40, 0),



            ItemTemplate = new DataTemplate(() =>

            {

                var frame = new Frame

                {

                    CornerRadius = 15,

                    HasShadow = true,

                    Padding = 0,

                    Margin = new Thickness(5),

                    BackgroundColor = Colors.Black

                };
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += OnItemTapped;
                frame.GestureRecognizers.Add(tapGesture);


                var grid = new Grid();

                var image = new Image { Aspect = Aspect.AspectFill };

                image.SetBinding(Image.SourceProperty, "ImageUrl");



                var gradient = new BoxView
                {

                    Background = new LinearGradientBrush
                    {

                        StartPoint = new Point(0, 1),

                        EndPoint = new Point(0, 0),

                        GradientStops = new GradientStopCollection

                        {

                            new GradientStop(Colors.Black.WithAlpha(0.7f), 0),

                            new GradientStop(Colors.Transparent, 1)

                        }

                    }

                };



                var label = new Label

                {

                    TextColor = Colors.Black.WithAlpha(0.7f),

                    FontSize = 20,

                    FontAttributes = FontAttributes.Bold,

                    Margin = new Thickness(15),

                    VerticalOptions = LayoutOptions.End

                };

                label.SetBinding(Label.TextProperty, "Title");



                grid.Children.Add(image);

                grid.Children.Add(gradient);

                grid.Children.Add(label);



                frame.Content = grid;

                return frame;

            })

        };



        var indicatorView = new IndicatorView

        {

            IndicatorColor = Colors.LightGray,

            SelectedIndicatorColor = Colors.DarkSlateBlue,

            HorizontalOptions = LayoutOptions.Center,

            Margin = new Thickness(0, 10)

        };

        carouselView.IndicatorView = indicatorView;


        {




            // Soovi korral saame karusselli kohe uuele pildile kerida

            carouselView.Position = items.Count - 1;

        };



        // Automaatne kerimine

        Device.StartTimer(TimeSpan.FromSeconds(4), () =>

        {

            if (items == null || items.Count == 0) return false;



            position = (position + 1) % items.Count;

            carouselView.Position = position;

            return true;

        });



        Content = new ScrollView
        {

            Content = new VerticalStackLayout
            {

                Padding = 20,

                Spacing = 20, // Jätab elementide vahele ilusa tühimiku

                Children =

                {
                    languageStack,
                    carouselView,
                    indicatorView,



                }

            }

        };

    }
    private async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;

        var item = frame?.BindingContext as CarouselItem;

        if (item != null)
        {
            await DisplayAlert("Keele info", $"{item.Title } - \n{item.Description} \n Example: {item.HelloWorld}", "Sulge");
        }
    }


    public void ChangeLanguage(string languageCode)
    {
        var culture = new CultureInfo(languageCode);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        AppResources.Culture = culture;

        Application.Current.MainPage = new NavigationPage(new Galerii());
    }

}