using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XamarinEssentialsEx
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        // Uri uri;
        List<History> objHistoryList = new List<History>();
        public MainPage()
        {
            InitializeComponent();

            btnPhoneCall.Clicked += BtnPhoneCall_Clicked;
            btnSendMesage.Clicked += BtnSendMesage_Clicked;
            btnSeendEmail.Clicked += BtnSeendEmail_Clicked;
            btnSpeak.Clicked += BtnSpeak_Clicked;
            btnBrowse.Clicked += BtnBrowse_Clicked;
            btnHistory.Clicked += BtnHistory_Clicked;
            ButtonOpenCoords.Clicked += ButtonOpenCoords_Clicked;
            btnMaps.Clicked += BtnMaps_Clicked;
            swOnOf.Toggled += SwOnOf_Toggled;
            Buttonvibrate.Clicked += Buttonvibrate_Clicked;
            buttoncancel.Clicked += Buttoncancel_Clicked;
           
            LabelCurrent.Text = $"{ VersionTracking.CurrentBuild} ({ VersionTracking.CurrentVersion})";
            LabelPast.Text = $"{VersionTracking.PreviousBuild}({VersionTracking.PreviousVersion})";
            if (VersionTracking.IsFirstLaunchForCurrentBuild)
                LabelNew.Text = "New Version";

            ButtonCalculate.Clicked += ButtonCalculate_Clicked;

            Buttonshare.Clicked += Buttonshare_Clicked;
            //Tapgesture for label,after that write this line in tap event:"await Browser.OpenAsync("http://xamarin.com",BrowserLaunchMode.SystemPreferred);" &write above "async"//
            buttn.Clicked += Buttn_Clicked;
            ButtonLauncher.Clicked += ButtonLauncher_Clicked;

            // Device Model (SMG-950U, iPhone10,6)
            var device = DeviceInfo.Model;

            // Manufacturer (Samsung)
            var manufacturer = DeviceInfo.Manufacturer;

            // Device Name (Motz's iPhone)
            var deviceName = DeviceInfo.Name;

            // Operating System Version Number (7.0)
            var version = DeviceInfo.VersionString;

            // Platform (Android)
            var platform = DeviceInfo.Platform;
            if (platform == DevicePlatform.Android)
            {
                //for android here
            }

            // Idiom (Phone)
            var idiom = DeviceInfo.Idiom;
            if (idiom == DeviceIdiom.Tablet)
            {
                //if an tablet to something
            }

            // Device Type (Physical)
            var deviceType = DeviceInfo.DeviceType;
            LabelDeviceInfo.Text = $"{deviceName} {version} { platform}";

            PinButton.Clicked += PinButton_Clicked;

            LabelAppInfo.Text = $"{AppInfo.Name}";
            LabelVersionInfo.Text = $"{AppInfo.VersionString}" + $"{AppInfo.BuildString}";

        }

        private void PinButton_Clicked(object sender, EventArgs e)
        {
            if (EntryPin.Text == "1234")
                DisplayAlert("you did it!", "Pin was accepted!", "ok");
        }

        protected async  override void OnAppearing()
        {
            base.OnAppearing();
            if (Clipboard.HasText)
            {
                var test = await Clipboard.GetTextAsync();
                if (test.Length == 4)
                    EntryPin.Text = test;
            }
        }
        private async void ButtonLauncher_Clicked(object sender, EventArgs e)
        {
            if(await Launcher.CanOpenAsync("lyft://"))
            {
                await Launcher.OpenAsync("lyft://ridetype?id=lyft_line");
            }
        }

        private async void Buttn_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;
            LoadingIndicator.IsRunning=true;
            await Task.Run(async () =>
            {
            for (int i = 0; i < 5; i++)
            {
                MainThread.BeginInvokeOnMainThread(()=>{
                    LabelUpdate.Text = $"Update #{i}";
                });
                  
                    await Task.Delay(1000);
                }
            });
            LoadingIndicator.IsRunning = false;
            button.IsEnabled = true;
        }

        private async void ButtonOpenCoords_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(EntryLatitude.Text, out double lat))
                return;
            if (!double.TryParse(EntryLongitude.Text, out double lng))
                return;
           await Map.OpenAsync(lat, lng, new MapLaunchOptions { Name = EntryName.Text, NavigationMode = NavigationMode.None }); //None place lo "Driving" is also used it will go to direction way//
        }

        private async void Buttonshare_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest { Text=EntryShare.Text, Title="Share!"});
            
        }

        private void ButtonCalculate_Clicked(object sender, EventArgs e)
        {
          if(int.TryParse(Entryvalue.Text, out int val))
            {
                var mile = UnitConverters.KilometersToMiles(val);
                var cel = UnitConverters.FahrenheitToCelsius(val);
                Labelvalue.Text = $"Miles:{mile}, Celsius:{cel}";
            }

        }

        private void Buttoncancel_Clicked(object sender, EventArgs e)
        {
            Vibration.Cancel();
        }

        private void Buttonvibrate_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(Slidertime.Value));
        }

        private async void SwOnOf_Toggled(object sender, ToggledEventArgs e)
        {
            var x = e.Value;
            if (x == true)
            {

                //Turn On
                await Flashlight.TurnOnAsync();
            }
            else
            {
                //Turn Off
                await Flashlight.TurnOffAsync();
            }

        }

        private async void BtnMaps_Clicked(object sender, EventArgs e)
        {
            var location = new Location(17.6140, 78.0816);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

            await Map.OpenAsync(location, options);
        }

        private void BtnHistory_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage(objHistoryList));
        }

        public void getHistory()
        {
            objHistoryList.Add(new History { Url = eturl.Text });
        }

        private void BtnBrowse_Clicked(object sender, EventArgs e)
        {
            //uri = new Uri(eturl.Text);
            //OpenBrowser(uri);
            //await Browser.OpenAsync(eturl.Text);
            getHistory();

        }
        //public async void OpenBrowser(Uri uri)
        //{
        //    await Browser.OpenAsync(uri);
        //}


        private async void BtnSpeak_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync(etData.Text);
        }

        private async void BtnSeendEmail_Clicked(object sender, EventArgs e)
        {
            List<string> recipients = new List<string>();  // No.Mails to NO.Persons-1 Message("if" working")//
            if (etEmailId.Text.Contains(","))
            {
                string[] emails = etEmailId.Text.Split(',');
                foreach (var item in emails)
                {
                    recipients.Add(item);
                }
            }
            else
            {
                recipients.Add(etEmailId.Text);        // 1Email to 1Person-1 message("else working")//
            }

            //Below commands or 1 gmail 1 sender//
            // List<string> recipients = new List<string>();// 
            //recipients.Add(etEmailId.Text);//
            var message = new EmailMessage
            {
                Subject = etSubject.Text,
                Body = etDescription.Text,
                To = recipients
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }

        private async void BtnSendMesage_Clicked(object sender, EventArgs e)
        {
            List<string> to = new List<string>();
            if (etMobileNumber.Text.Contains(","))
            {
                string[] numbers = etMobileNumber.Text.Split(',');
                foreach (var item in numbers)
                {
                    var Mesg = new SmsMessage(etMessage.Text, numbers);
                    await Sms.ComposeAsync(Mesg);
                }
            }


            //Below two commands or 1 number-1 message in below the button Event//
        //    var message = new SmsMessage(etMessage.Text, new[] { etMobileNumber.Text });
        //    await Sms.ComposeAsync(message);
        }

        private void BtnPhoneCall_Clicked(object sender, EventArgs e)
        {
            //If u give the validations(&)Displayalerts then give the below commands//
            if (etMobileNumber.Text != null && etMobileNumber.Text.Length > 0)
            {
                if (etMobileNumber.Text.Length == 10)
                {
                    //If u give this command below the "call (or) PhoneCal Button Event{use this line}, then Direct call to others//
                    PhoneDialer.Open(etMobileNumber.Text);  
                }
                else
                {
                    DisplayAlert("Number", "Please enter 10 digit mobile number", "Got it");
                }
            }
            else
            {
                DisplayAlert("Number", "Please enter mobile number", "Got it");
            }

        }
    }
}
