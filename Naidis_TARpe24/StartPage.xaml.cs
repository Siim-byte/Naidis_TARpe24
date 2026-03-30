namespace Naidis_TARpe24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>()
	{	//new TextPage(),
		//new FigurePage(),
		//new Timer_Page(),
		new ValgufoorPage(),
		new VarviPage(),
		new lumememm(), 
		//new Pop_Up_Page(), 
		new PopUp_kasutamine(),
		new PickerImageGridPage(),
		new Triptrapstrull(),
		new Table_Page(),
	};
	public List<string> lehedNimed = new List<string>() 
	{
		//"Tekst", 
		//"Kujund", 
		//"Timer", 
		"Valgusfoor", 
		"Värv", 
		"Lumememm", 
		//"PopUp", 
		"PopUp-Kasutamine",
		"PickerImageGrid",
		"Tripstrapstrull",
		"Table"
	};
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
				HeightRequest = 70,
				ZIndex = i
            };
			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
        }
		//loome punase testnupu
		Button nulliNupp = new Button()
		{
			Text = "Nulli seaded (Testimiseks)",
			BackgroundColor = Colors.Red,
			TextColor = Colors.White,
			CornerRadius = 10,
			HeightRequest = 50,
			Margin = new Thickness(0,30,0,0) //jätame veidi tühja ruumi üles
		};
		//mis juhtub nupule vajutades?
		nulliNupp.Clicked += async (sender, e) =>
		{
			//Kustutame seadme mälust meie spetsiifilise võtme
			Preferences.Default.Remove("EsimeneKäivitamine");
			Preferences.Default.Remove("Username");
			//Anname tagasisidet, et nullimine õnnestus
			await DisplayAlertAsync("Edukalt nullitud", "Mälu on tühjendatud. Kui sa lehe uuesti avad, käitub äpp nagu täiesti uus!", "OK");
		};
		vst.Add(nulliNupp);
		sv = new ScrollView { Content = vst };
		Content = sv;

    }
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//1. Loeme seadme mälust muutuja "EsimeneKäivitamine".
		//Kui sellist muutujat pole (äpp on uus), annab see vaikimisi väärtuseks 'true' -
		bool onEsimeneStart = Preferences.Default.Get("EsimeneKäivitamine", true);

		// 2. Kui on esimene start, kuvame dialoogiakna
		if (onEsimeneStart)
		{
			bool vastus = await DisplayAlertAsync("Tere tulemast!",
												"Tundub, et avasid selle rakenduse esimest korda. Kas soovid näha lühikest juhendit?",
												"Jah, palun",
												"Ei, Saan ise hakkama");
			if (vastus)
			{
				await DisplayAlertAsync("Juhend",
					"Siin on sinu lühike juhend: vali menüüst sobiv teema ja uuri, kuidas elemendid töötavad!",
					"Selge");
			}
			// 3. Salvestame info, et esimene käivitamine on tehtud.
			Preferences.Default.Set("EsimeneKäivitamine", false);
			Preferences.Default.Set("Username", false);
		}
	}
}