namespace Naidis_TARpe24;

public partial class Table_Page : ContentPage
{
    TableView tabelView;
    SwitchCell sc;
    ImageCell ic;
    TableSection fotosection;
    EntryCell emailEntry;
    EntryCell phoneEntry;
    EntryCell nimiEntry;
    EntryCell kirjeldusEntry;
    public Table_Page()
    {
        sc = new SwitchCell { Text = "Näita pilti" };
        sc.OnChanged += Sc_OnChanged;
        phoneEntry = new EntryCell
        {
            Label = "Telefon",
            Placeholder = "Sisesta tel. number",
            Keyboard = Keyboard.Telephone
        };
        emailEntry = new EntryCell
        {
            Label = "Email",
            Placeholder = "Sisesta email",
            Keyboard = Keyboard.Email
        };
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
                    phoneEntry,
                    emailEntry,
                    sc
                },
                new TableSection("Tegevused:")
                {
                    new TextCell { Text = "Saada SMS", Command = new Command(Saada_sms_Clicked) },
                    new TextCell { Text = "Saada Email", Command = new Command(Saada_email_Clicked) },
                    new TextCell { Text = "Helista", Command = new Command(Helista_Clicked) }
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
    private async void Saada_sms_Clicked()
    {
        string phone = phoneEntry.Text;
        var message = "Tere tulemast! Saadan sõnumi";
        SmsMessage sms = new SmsMessage(message, phone);
        if (phone != null && Sms.Default.IsComposeSupported)
        {
            await Sms.Default.ComposeAsync(sms);
        }
    }
    private async void Saada_email_Clicked()
    {
        if (string.IsNullOrWhiteSpace(emailEntry.Text)) return;
        var message = "Tere tulemast! Saadan email";
        EmailMessage e_mail = new EmailMessage()
        {
            Subject = emailEntry.Text,
            Body = message,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(new[] {emailEntry.Text})
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
    private async void Helista_Clicked()
    {
        string phone = phoneEntry.Text;
        if (!string.IsNullOrWhiteSpace(phone))
        {
            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(phone);
            }
        }
        
    }
}

