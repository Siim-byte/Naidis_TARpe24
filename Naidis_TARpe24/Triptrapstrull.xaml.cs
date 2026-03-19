using System.Threading.Tasks;

namespace Naidis_TARpe24;

public partial class Triptrapstrull : ContentPage
{
    Grid gr4x2, gr3x3;
    Button btnUus, btnAlustaja;
    Label lblStaatus;
    string kord = "X";
    Button[,] nupud = new Button[3, 3];
    Label[,] symbols = new Label[3, 3];
    List<Label> Ruudud = new List<Label>();
    public Triptrapstrull()
	{
        gr4x2 = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition{Height=new GridLength(1, GridUnitType.Star)},
                new RowDefinition{Height=new GridLength(1, GridUnitType.Star)},
                new RowDefinition{Height=new GridLength(6, GridUnitType.Star)},
                new RowDefinition{Height=new GridLength(1, GridUnitType.Star)}
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{Width=new GridLength(1, GridUnitType.Star)},
                new ColumnDefinition{Width=new GridLength(1, GridUnitType.Star)}
            }
        };
        btnAlustaja = new Button
        {
            Text = "Kes alustab?",
        };
        btnAlustaja.Clicked += (s, e) =>
        {
            kord = new Random().Next(0,2) == 0 ?  "X" : "O";
            lblStaatus.Text = $"Alustab mängija {kord}";
        };

        btnUus = new Button
        {
            Text = "Uus mäng",
        };
        btnUus.Clicked += UusMang;

        lblStaatus = new Label
        {
            Text = "Mängija X kord",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        Button btnReeglid = new Button()
        {
            Text = "Reeglid",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        btnReeglid.Clicked += Reeglid_Clicked;
        Button btnTaust = new Button()
        {
            Text = "Tausta valik",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        btnTaust.Clicked += Taustad_Clicked;
        LooManguvali();

        gr4x2 .Add(btnAlustaja, 0, 0);
        gr4x2 .Add (btnUus, 1, 0);
        gr4x2.Add(btnReeglid, 2, 1);
        gr4x2.Add(btnTaust, 2,2);            ///FIX GRID
        gr4x2.Add(lblStaatus, 0, 2);
        gr4x2.SetColumnSpan(lblStaatus, 3);
        gr4x2.Add(gr3x3 , 0, 3);
        gr4x2.SetColumnSpan(gr3x3, 3);

        Content = gr4x2;

    }
    private async void Taustad_Clicked(object? sender, EventArgs e)
    {
        string Teema = await DisplayActionSheetAsync("Vali teema", "Loobu", "Null", "Tume", "Hele", "Suvaline");

        if (Teema == "Tume")
        {
            this.BackgroundColor = Colors.Black;
        }
        else if (Teema == "Hele")
        {
            this.BackgroundColor = Colors.WhiteSmoke;
        }
        else if (Teema == "Suvaline")
        {
            Random rnd = new Random();
            this.BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
    private async void Reeglid_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Reeglid", "Mängu võitmiseks on vaja saada diagonaalselt, horizontaalselt või vertikaalselt 3 ruudu sinu märgiga X või O", "OK");
    }
    private void LooManguvali()
    {
        gr3x3 = new Grid();
        Ruudud.Clear();
        for (int i = 0; i < 3; i++)
        {
            gr3x3.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gr3x3.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                var l = new Label
                {
                    Text = "",
                    FontSize = 48,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                symbols[r, c] = l;
                Ruudud.Add(l);

                Frame f = new Frame
                {
                    BorderColor = Colors.Black,
                    Content = l,
                    Padding = 0,
                    BackgroundColor = Colors.White
                };

                var tap = new TapGestureRecognizer();
                tap.Tapped += Manguvali_Tap;
                f.GestureRecognizers .Add(tap);

                gr3x3.Add(f, c, r);
            }

        }
    }
    private async void Manguvali_Tap(object? sender, EventArgs e)
    {
        if (sender is not Frame { Content: Label l } || !string.IsNullOrEmpty(l.Text)) return;
        l.Text = kord;
        l.TextColor = kord == "X" ? Colors.Red : Colors.Blue;

        bool voit = KontrolliVoit();
        bool viik = KontrolliViik();

        if (voit || viik)
        {
            string teade = voit ? $"{kord} võitis!" : "Mäng jäi viiki!";
            await DisplayAlertAsync("Mäng lõppenud", teade, "OK");
            UusMang(null, null);
            return;
        }
        kord = kord == "X" ? "O" : "X";
        lblStaatus.Text = $"Mängija {kord} kord";
    }
    private void UusMang(object? sender, EventArgs e)
    {
        foreach (var l in symbols) l.Text = "";
        kord = "X";
        lblStaatus.Text = $"Mängija: {kord}";
    }
    private bool KontrolliVoit()
    {
        int[,] võidud = {
        { 0,1,2},
        { 3,4,5},
        { 6,7,8},
        { 0,3,6},
        { 1,4,7},
        { 2,5,8},
        { 0,4,8},
        { 2,4,6}
        };
        for (int i = 0; i < 8; i++)
        {
            string a = Ruudud[võidud[i, 0]].Text;
            string b = Ruudud[võidud[i, 1]].Text;
            string c = Ruudud[võidud[i, 2]].Text;

            if (a != "" && a == b && b == c)
            {
                return true;
            }
        }
        return false;
    }
    private bool KontrolliViik()
    {
        foreach (var l in symbols) if (string.IsNullOrEmpty(l.Text)) return false;
        return true;
    }
}//vaja teha botiga mang 