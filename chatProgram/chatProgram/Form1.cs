using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using System;

namespace chatProgram
{
    public partial class Form1 : Form
    {
        TcpListener lyssnare;
        TcpClient client;
        int port = 12345;
        private Dictionary<TcpClient, string> idClient = new Dictionary<TcpClient, string>();
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
                client = await lyssnare.AcceptTcpClientAsync();
                string id = await TaEmotAnvnamn(client);
                idClient[client] = id;
                UpdateClients();
                Hanteraclient(client);
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

        private async Task Hanteraclient(TcpClient client)
        {
            byte[] buffer = new byte[1024];
            int n;
            if (client.Connected)
            {
                try
                {


                    while (client.Connected)
                    {
                        using (NetworkStream stream = client.GetStream())
                        {



                            n = await stream.ReadAsync(buffer, 0, buffer.Length);
                            if (n == 0) break; // Connection closed.
                            string inp = Encoding.UTF8.GetString(buffer, 0, n).Trim();

                            if (inp.Split(';')[0] == "-IMG")
                            {
                                await ReciveImage(client, int.Parse(inp.Split(';')[1]));

                                continue;
                            }

                            string message = idClient[client] + ": " + inp;
                            LogMessage(message);
                            Broadcast(client, message);
                        }
                    }
                    // användaren är inte ansluten
                    if (idClient.ContainsKey(client))
                    {
                        Broadcast(client, $"{idClient[client]} lämnade chatrummet.");
                        LogMessage($"{idClient[client]} lämnade chatrummet.");
                        idClient.Remove(client);
                    }

                    client.Close();
                    UpdateClients();

                }

                catch (Exception error) { MessageBox.Show(error.Message, Text); }
                finally
                {


                    if (idClient.ContainsKey(client))
                    {
                        Broadcast(client, $"{idClient[client]} lämnade chatrummet.");
                        LogMessage($"{idClient[client]} lämnade chatrummet.");
                        idClient.Remove(client);
                    }

                    client.Close();
                    UpdateClients();
                }
            }

        }

        private async Task ReciveImage(TcpClient client, int imageSize) {

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;
            int totalBytesRead = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                while (totalBytesRead < imageSize)
                {
                    int bytesToRead = Math.Min(buffer.Length, imageSize - totalBytesRead);
                    bytesRead = await stream.ReadAsync(buffer, 0, bytesToRead);

                    if (bytesRead == 0) break; // Connection closed

                    ms.Write(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;
                }

                if (totalBytesRead == imageSize)
                {
                    ms.Position = 0;
                    
                    Broadcast(sender: client, img: Image.FromStream(ms));
                    
                }
                else
                {
                    // Handle incomplete image data
                    MessageBox.Show("Incomplete image data received.", Text);
                }
            }
            
        }

        private void LogMessage(string meddelande)
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


        public async Task StartaLäsning(TcpClient k)
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

        async Task Broadcast(TcpClient avsändare, string meddelande)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(meddelande);
            foreach (var client in idClient.Keys)
            {
                if (client != avsändare && client != null && client.Connected)
                    await client.GetStream().WriteAsync(buffer, 0, buffer.Length);
            }
        }

        // overload to send an image to all clients
        async Task Broadcast(TcpClient sender, Image img)
        {
           
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                imageBytes = ms.ToArray();
            }
            byte[] buffer = Encoding.UTF8.GetBytes("-IMG;" + imageBytes.Length.ToString());

            using (NetworkStream stream = client.GetStream())
            {
                foreach (TcpClient client in idClient.Keys)
                {
                    if (client != sender && client != null)
                    {
                        await stream.WriteAsync(buffer, 0, buffer.Length);
                        await stream.WriteAsync(imageBytes, 0, imageBytes.Length);
                    }
                }
            }
        }
        void UpdateClients()
        {
            klienterRichtxtbx.Text = "";
            foreach (String id in idClient.Values)
            {
                LogMessage(id);
            }
        }
    }
    

}