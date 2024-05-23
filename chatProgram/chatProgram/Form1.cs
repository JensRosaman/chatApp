using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace chatProgram
{
    public partial class Form1 : Form
    {
        TcpListener lyssnare;
        TcpClient klient;
        int port = 12345;
        private Dictionary<TcpClient, string> anvKarta = new Dictionary<TcpClient, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStarta_Click(object sender, EventArgs e)
        {
            try
            {
                lyssnare = new TcpListener(IPAddress.Any, port);
                lyssnare.Start();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }

            btnStarta.Enabled = false;
            await StartaMottagning();
        }



        public async Task StartaMottagning()
        {
            while (true)
            {
                klient = await lyssnare.AcceptTcpClientAsync();
                string användarnamn = await TaEmotAnvnamn(klient);
                anvKarta[klient] = användarnamn;
                UppdateraAnslutnaKlienter();
                HanteraKlient(klient);
            }
        }

        private async Task<string> TaEmotAnvnamn(TcpClient client)
        {
            byte[] usernameBytes = new byte[256];
            int bytesRead = await client.GetStream().ReadAsync(usernameBytes, 0, usernameBytes.Length);
            string username = Encoding.UTF8.GetString(usernameBytes, 0, bytesRead);
            byte[] data = Encoding.UTF8.GetBytes(tbxInkorg.Text);
            await client.GetStream().WriteAsync(data, 0, data.Length);
            return username;
        }

        private async void HanteraKlient(TcpClient klient)
        {
            byte[] buffert = new byte[10024];
            int n;
            if (klient.Connected)
            {
                try
                {
                    while ((n = await klient.GetStream().ReadAsync(buffert, 0, buffert.Length)) != 0)
                    {

                        

                            // Convert the byte array to a JSON string
                            string json = Encoding.UTF8.GetString(buffert);

                            // Deserialize the JSON string to a MyData object
                            Data data = JsonSerializer.Deserialize<Data>(json);

                            if (data != null && data.IsImg)
                            {
                                // Convert the byte array back to an image
                                using (MemoryStream imgStream = new MemoryStream(data.Message))
                                {
                                    Image image = Image.FromStream(imgStream);

                                    // Save the image to a file (or process it as needed)
                                    BackgroundImage = image;

                                }
                            }
                            string inp = Encoding.UTF8.GetString(buffert, 0, n);
                            string meddelande = anvKarta[klient] + ": " + inp;
                            LoggaMeddelande(meddelande)
                            SynkaMeddelandenMedKlienter(klient, meddelande);
                        
                    }
                }
                catch (Exception error) { MessageBox.Show(error.Message, Text); }
                finally
                {
                    SynkaMeddelandenMedKlienter(klient, $"{anvKarta[klient]} lämnade chatrummet.");
                    LoggaMeddelande($"{anvKarta[klient]} lämnade chatrummet.");

                    if (anvKarta.ContainsKey(klient))
                    {
                        anvKarta.Remove(klient);
                    }

                    klient.Close();
                    UppdateraAnslutnaKlienter();
                }
            }

        }

        private void LoggaMeddelande(string meddelande)
        {
            if (tbxInkorg.InvokeRequired)
            {
                tbxInkorg.Invoke(new MethodInvoker(delegate { tbxInkorg.AppendText(meddelande + Environment.NewLine); }));
            }
            else
            {
                tbxInkorg.AppendText(meddelande + Environment.NewLine);
            }
        }


        public async void StartaLäsning(TcpClient k)
        {
            byte[] buffert = new byte[1024];
            int n = 0;
            try
            {
                n = await k.GetStream().ReadAsync(buffert, 0, buffert.Length);
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            tbxInkorg.AppendText(Encoding.UTF8.GetString(buffert, 0, n));

            StartaLäsning(k);
        }

        async void SynkaMeddelandenMedKlienter(TcpClient avsändare, string meddelande)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(meddelande);
            foreach (var klient in anvKarta.Keys)
            {
                if (klient != avsändare && klient != null)
                    await klient.GetStream().WriteAsync(buffer, 0, buffer.Length);
            }
        }

        void UppdateraAnslutnaKlienter()
        {
            klienterRichtxtbx.Text = "";
            foreach (String användarnamn in anvKarta.Values)
            {
                LoggaMeddelande(användarnamn);
            }
        }
    }
    public class Data
    {

        public byte[] Message { get; set; }
        public bool IsImg { get; set; }
    }

}