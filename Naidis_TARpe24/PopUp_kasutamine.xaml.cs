namespace Naidis_TARpe24;

public partial class PopUp_kasutamine : ContentPage
{
	public PopUp_kasutamine()
	{
        //InitializeComponent();
        Button alertQuestButton = new Button
        {
            Text = "Arvuta",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        alertQuestButton.Clicked += AlertQuestButton_Clicked;

        Button mButton = new Button
        {
            Text = "Mõistatus",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        mButton.Clicked += mButton_Clicked;

        Button visualMButton = new Button
        {
            Text = "Visuaalne mõistatus",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        visualMButton.Clicked += visualMButton_Clicked;

        Button nameButton = new Button
        {
            Text = "Sinu nimi",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        nameButton.Clicked += nameButton_Clicked;

        Button sheetButton = new Button
        {
            Text = "Vali alaleht",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        sheetButton.Clicked += sheetButton_Clicked;

        Content = new VerticalStackLayout()
        {
            Spacing = 20,//jätab nuppude vahele 20 pikslit vaba ruumi
            Padding = new Thickness(0, 50, 0, 0), //lükkab sisu veidi ülevalt alla
            Children = { alertQuestButton, mButton, visualMButton, nameButton, sheetButton }
        };
    }

    private async void AlertQuestButton_Clicked(object sender, EventArgs e)
    {
        Random rnd = new Random();
        int a = rnd.Next(1, 11);
        int b = rnd.Next(1, 11);
        //tehe
        int vastus1 = a * b;
        int vastus2 = a + b;
        int vastus3 = a - b;
        int vastus4 = a / b;


        string m1 = await DisplayPromptAsync("Küsimus", $"Palju on {a} * {a}?");
        string m2 = await DisplayPromptAsync("Küsimus", $"Palju on {a} + {b}");
        string m3 = await DisplayPromptAsync("Küsimus", $"Palju on {b} - {a}");
        string m4 = await DisplayPromptAsync("Küsimus", $"Palju on {b} / {a}");

        if (m1 == vastus1.ToString())
        {
            await DisplayAlertAsync("✅ Õige", $"Esimese küsimuse vastus on {vastus1}!", "OK");
        }
        else
        {
            await DisplayAlertAsync("❎ Vale", "Proovi esimene küsimus uuesti ", "OK");
        }
        if (m2 == vastus2.ToString())
        {
            await DisplayAlertAsync("✅ Õige", $"Teise küsimuse vastus on {vastus2}!", "OK");
        }
        else
        {
            await DisplayAlertAsync("❎ Vale", "Proovi teine küsimus uuesti ", "OK");
        }
        if (m3 == vastus3.ToString())
        {
            await DisplayAlertAsync("✅ Õige", $"Kolmanda küsimuse vastus on {vastus3}!", "OK");
        }
        else
        {
            await DisplayAlertAsync("❎ Vale", "Proovi kolmas küsimus uuesti ", "OK");
        }
        if (m4 == vastus4.ToString())
        {
            await DisplayAlertAsync("✅ Õige", $"neljanda küsimuse vastus on {vastus4}!", "OK");
        }
        else
        {
            await DisplayAlertAsync("❎ Vale", "Proovi neljas küsimus uuesti ", "OK");
        }
    }
    private async void mButton_Clicked(object? sender, EventArgs e)
    {
        bool ans = await DisplayAlert("Vasta", "Mis värvi on öö", "Must", "Valge");
        if (ans == true)
        {
            await DisplayAlertAsync("Õige", "Vastus on must", "OK");
        }
        else
        {
            await DisplayAlertAsync("Vale", "Proovi uuesti", "OK");
        }
    }
    private async void visualMButton_Clicked(object? sender, EventArgs e)
    {
        string ans = await DisplayPromptAsync("Mõistatus", "Mis kõnnib öösel ja päeval kahel jalal", keyboard: Keyboard.Text);
        if (ans == "Kass")
        {
            await DisplayAlertAsync("Õige", "Vastus on Kass.", "OK");
        }
        else
        {
            await DisplayAlertAsync("Vale", "Proovi uuesti", "OK");
        }
    }
    private async void nameButton_Clicked(object sender, EventArgs e)
    {
        string name = Preferences.Default.Get("Username", "");
        if (string.IsNullOrEmpty(name))
        {
            name = await DisplayPromptAsync("Vasta", "Mis on sinu nimi", keyboard: Keyboard.Chat);

            if (!string.IsNullOrEmpty(name))
            {
                Preferences.Default.Set("Username", name);
                await DisplayAlertAsync("Tervitus", $"Tere tulemast esmakordselt, {name}!", "OK");
            }

        }
        else
        {
            await DisplayAlertAsync("Tervitus", $"Tere tagasi, {name}!", "OK");
        }

    }
    private async void sheetButton_Clicked(object sender, EventArgs e)
    {
        string alaleht = await DisplayActionSheetAsync("Vali alaleht", "Loobu", "Null", "Valgusfoor", "Värv", "Lumememm");
        if (alaleht == "Valgusfoor")
        {
            await DisplayAlertAsync("Valitud leht", "Valgusfoor", "OK");
            await Navigation.PushAsync(new ValgufoorPage());
        }
        else if (alaleht == "Värv")
        {
            await DisplayAlertAsync("Valtiud leht", "Värvi leht", "OK");
            await Navigation.PushAsync(new VarviPage());
        }
        else if (alaleht == "Lumememm")
        {
            await DisplayAlertAsync("Valtiud leht", "Lumememme leht", "OK");
            await Navigation.PushAsync(new lumememm());
        }
    }
}
