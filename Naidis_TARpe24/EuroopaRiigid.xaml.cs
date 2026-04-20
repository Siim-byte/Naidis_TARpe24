using System.Collections.ObjectModel;

namespace Naidis_TARpe24;


    // 1. Andmemudel
    public class Riik
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Rahvaarv { get; set; }
        public string Lipp { get; set; }
    }

// 2. Põhileht
public partial class EuroopaRiigid : ContentPage
{
    // Globaalsed muutujad, et neile pääseks ligi ka sündmuste töötlejates
    ObservableCollection<Riik> riigid;
    ListView list;
    Entry entryNimi, entryPealinn, entryRahvaarv, entryLipp;

    public EuroopaRiigid()
    {
        Title = "Euroopa riigid";

        // Andmebaasi algväärtustamine
        riigid = new ObservableCollection<Riik>
            {
                new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv= 5, Lipp="Eesti.png" },
                new Riik { Nimi="Soome", Pealinn="Helsinki", Rahvaarv= 6,  Lipp="Soome.png" },
                new Riik { Nimi="Saksamaa", Pealinn="Berliin", Rahvaarv= 17, Lipp="Saksamaa.png" }
            };

        // Tekstilahtrite loomine
        entryNimi = new Entry { Placeholder = "Riigi nimi nt (Eesti)" };
        entryPealinn = new Entry { Placeholder = "Pealinn (nt Tallinn)" };
        entryRahvaarv = new Entry { Placeholder = "Rahvaarv", Keyboard = Keyboard.Numeric };
        entryLipp = new Entry { Placeholder = "Lippu failinimi (valikuline)" };

        // Nuppude loomine
        Button btnLisa = new Button
        {
            Text = "Lisa riik",
            BackgroundColor = Colors.LightGreen // Xamarinis kasuta: Color.LightGreen
        };
        btnLisa.Clicked += BtnLisa_Clicked;

        Button btnKustuta = new Button
        {
            Text = "Kustuta valitud riik",
            BackgroundColor = Colors.LightPink // Xamarinis kasuta: Color.LightPink
        };
        btnKustuta.Clicked += BtnKustuta_Clicked;

        Button btnMuuda = new Button
        {
            Text = "Muuda valitud riik",
            BackgroundColor = Colors.LightBlue
        };
        btnMuuda.Clicked += BtnMuuda_Clicked;

        // ListView loomine ja kujundamine
        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = riigid,
            SelectionMode = ListViewSelectionMode.Single
        };

        // Seome valimise sündmuse
        list.ItemTapped += List_ItemTapped;

        // Kuidas iga rida (telefon) välja näeb
        list.ItemTemplate = new DataTemplate(() =>
        {

            // -- 1. Loome pildi --
            Image imgLipp = new Image
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 10, 0) // Paremalt veeris, kui pilt on vasakul
            };
            imgLipp.SetBinding(Image.SourceProperty, "Lipp"); // Seome pildi failinimega

            // -- 2. Loome tekstid --
            Label lblNimi = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
            lblNimi.SetBinding(Label.TextProperty, "Nimi");

            Label lblPealinn = new Label { TextColor = Colors.Gray, VerticalOptions = LayoutOptions.Center }; // Xamarin: Color.Gray
            lblPealinn.SetBinding(Label.TextProperty, "Pealinn");

            Label lblRahvaarv = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center }; // Xamarin: Color.DarkBlue
            lblRahvaarv.SetBinding(Label.TextProperty, new Binding("Rahvaarv", stringFormat: "{0} mln")); // Lisame mln märgi

            // Teksti paigutus (vertikaalne virn)
            var textLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { lblNimi, lblPealinn, lblRahvaarv }
            };

            // -- 3. Loome REA PEAVIRNA (horisontaalne StackLayout) --
            var rowLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10),
                VerticalOptions = LayoutOptions.Center,

                // !!! SIIN MÄÄRATAKSE PILDI ASUKOHT !!!

                // PILT VASAKUL:
                Children = { imgLipp, textLayout }

                // PILT PAREMAL (kommenteeri eelmine rida välja ja kasuta seda):
                // Children = { textLayout, imgPilt } 
            };

            // Kui pilt on paremal, muuda ka pildi veerist:
            // imgPilt.Margin = new Thickness(10, 0, 0, 0); // Vasakult veeris

            return new ViewCell
            {
                View = rowLayout
            };
        });

        // Paneme kõik elemendid lehele kokku
        Content = new StackLayout
        {
            Padding = new Thickness(10),
            Children =
                {
                    entryNimi,
                    entryPealinn,
                    entryRahvaarv,
                    entryLipp,
                    btnLisa,
                    btnKustuta,
                    btnMuuda,
                    list // Nimekiri on kõige all
                }
        };
    }
    // --- SÜNDMUSTE TÖÖTLEJAD ---

    // 3. Elemendile vajutamine
    private async void List_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        Riik valitudRiik = e.Item as Riik;

        if (valitudRiik != null)
        {
            await DisplayAlertAsync("Riigi info", $"Nimi: {valitudRiik.Nimi}\nPealinn: {valitudRiik.Pealinn}\nRahvaarv: {valitudRiik.Rahvaarv} mln", "Sulge");
        }
    }
    // 2. Telefoni kustutamine (koos kinnitusega)
    private async void BtnKustuta_Clicked(object? sender, EventArgs e)
    {
        Riik valitudRiik = list.SelectedItem as Riik;

        if (valitudRiik != null)
        {
            bool vastus = await DisplayAlertAsync("Kinnitus", $"Kas oled kindel, et soovid riigi {valitudRiik.Nimi} kustutada?", "Jah", "Ei");
            if (vastus == true)
            {
                riigid.Remove(valitudRiik);
                list.SelectedItem = null; // Tühistame valiku
            }
        }
        else
        {
            await DisplayAlertAsync("Viga", "Palun vali nimekirjast riik, mida soovid kustutada.", "OK");
        }
    }
    // 1. Uue telefoni lisamine
    private void BtnLisa_Clicked(object? sender, EventArgs e)
    {
        string uusNimi = entryNimi.Text;
        if (!string.IsNullOrWhiteSpace(entryNimi.Text) && !string.IsNullOrWhiteSpace(entryPealinn.Text))
        {
            DisplayAlertAsync("Viga", "Palun täida vähemalt riigi ja pealinna väljad!", "OK");
        }

        // LINQ abil on väga lihtne kontrollida, kas nimekirjas leidub juba selline nimi
        // StringComparison.OrdinalIgnoreCase tagab, et "Eesti" ja "eesti" loetakse samaks
        bool riikOnOlemas = riigid.Any(r => r.Nimi.Equals(uusNimi, StringComparison.OrdinalIgnoreCase));

        if (riikOnOlemas)
        {
            DisplayAlert("Viga", "See riik on juba nimekirjas!", "OK");
            return;
        }
            int rahvaarv = 0;
            int.TryParse(entryRahvaarv.Text, out rahvaarv);

            // Kui pildi failinime ei sisestata, kasuta vaikimisi pilti
            string lippuNimi = string.IsNullOrWhiteSpace(entryLipp.Text) ? "default_Lipp.png" : entryLipp.Text;

            riigid.Add(new Riik
            {
                Nimi = entryNimi.Text,
                Pealinn = entryPealinn.Text,
                Rahvaarv = rahvaarv,
                Lipp = lippuNimi
            });

            // Tühjendame väljad pärast lisamist
            entryNimi.Text = "";
            entryPealinn.Text = "";
            entryRahvaarv.Text = "";
            entryLipp.Text = "";
        
    }
    private void BtnMuuda_Clicked(object sender, EventArgs e)
    {
        var valitud = list.SelectedItem as Riik;
        if (valitud != null)
        {
            valitud.Nimi = entryNimi.Text;
            valitud.Pealinn = entryPealinn.Text;
            valitud.Rahvaarv = int.Parse(entryRahvaarv.Text);
            valitud.Lipp = entryLipp.Text;

            list.ItemsSource = null;
            list.ItemsSource = riigid;
        }
    }
}
