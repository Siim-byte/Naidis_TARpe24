namespace Naidis_TARpe24;

public partial class Table_Page : ContentPage
{
    TableView tabelView;
    SwitchCell sc;
    ImageCell ic;
    TableSection fotosection;
    public Table_Page()
    {
        sc = new SwitchCell { Text = "Näita veel" };
        sc.OnChanged += Sc_OnChanged;
        ic = new ImageCell
        {
            ImageSource = ImageSource.FromFile("bob.jpg"),
            Text = "Foto nimetus",
            Detail = "Foto kirjeldus"
        };
        fotosection = new TableSection();

        tabelView = new TableView
        {
            Root = new TableRoot
            {
                new TableSection("Kontaktandmed:")
                {
                    new EntryCell
                    {
                        Label="Telefon",
                        Placeholder="Sisesta tel. number",
                        Keyboard=Keyboard.Telephone
                    },
                    new EntryCell
                    {
                        Label="Email",
                        Placeholder="Sisesta email",
                        Keyboard=Keyboard.Email
                    },
                    sc
                },
                fotosection
            }
        };

        Content = tabelView;
    }

    private void Sc_OnChanged(object? sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            fotosection.Title = "Foto:";
            fotosection.Add(ic);
            sc.Text = "Peida";
        }
        else
        {
            fotosection.Title = "";
            fotosection.Remove(ic);
            sc.Text = "Näita veel";
        }
    }
    private async void Saada_sms_Clicked(object? sender, EventArgs e)
    {
        string phone = email_phone.Text;
        var message = "Tere tulemast! Saadan sõnumi";
        SmsMessage sms = new SmsMessage(message, phone);
        if (phone != null && Sms.Default.IsComposeSupported)
        {
            await Sms.Default.ComposeAsync(sms);
        }
    }
    private async void Saada_email_Clicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(email_phone.Text)) return;
        var message = "Tere tulemast! Saadan email";
        EmailMessage e_mail = new EmailMessage()
        {
            Subject = email_phone.Text,
            Body = message,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(new[] {email_phone.Text})
        };
        if (Email.Default.IsComposeSupported)
        {
            await Email.Default.ComposeAsync(e_mail);
        }
        else
        {
            await DisplayAlertAsync("Viga", "Emaili saadmine pole selles seadmes toetatud", "OK");
        }
    }
}

