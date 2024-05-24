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
            btnStarta.Text = "Startad";
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
                _ = Hanteraclient(client);
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




                        n = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                        if (n == 0) break; // Connection closed.
                        string inp = Encoding.UTF8.GetString(buffer, 0, n).Trim();

                        if (inp.StartsWith("-IMG"))
                        {
                            byte[]? img = await ReciveImage(client, int.Parse(inp.Split(';')[1]));
                            if (img != null)
                            {
                                await BroadcastImage(client, inp, img);
                            }

                            continue;
                        }

                        string message = idClient[client] + ": " + inp;
                        LogMessage(message);
                        await Broadcast(client, message);

                    }
                    // användaren är inte ansluten längre
                    string id = idClient.GetValueOrDefault(client, "okänd");

                    await Broadcast(client, $"{id} lämnade chatrummet.");
                    LogMessage($"{id} lämnade chatrummet.");
                    if (idClient.ContainsKey(client))
                    {
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
                        await Broadcast(client, $"{idClient[client]} lämnade chatrummet.");
                        LogMessage($"{idClient[client]} lämnade chatrummet.");
                        idClient.Remove(client);
                    }

                    client.Close();
                    UpdateClients();
                }
            }

        }

        private async Task<Byte[]?> ReciveImage(TcpClient client, int imageSize)
        {

            byte[] buffer = new byte[1024];
            byte[] imageBytes = new byte[imageSize];
            int bytesRead;
            int totalBytesRead = 0;


            try
            {
                while (totalBytesRead < imageSize)
                {
                    int bytesToRead = Math.Min(buffer.Length, imageSize - totalBytesRead);
                    bytesRead = await client.GetStream().ReadAsync(buffer, 0, bytesToRead);

                    if (bytesRead == 0)
                        break; // Connection closed

                    Buffer.BlockCopy(buffer, 0, imageBytes, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;
                }

                if (totalBytesRead == imageSize)
                {
                    return imageBytes;
                }
                else
                {
                    // Handle incomplete image data
                    MessageBox.Show("Incomplete image data received.", "Error");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await Broadcast(client, $"An error occurred: {ex.Message}");
                return null;
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
        async Task BroadcastImage(TcpClient sender, String meta, byte[] imageBytes)
        {



            byte[] buffer = Encoding.UTF8.GetBytes(meta);


            foreach (TcpClient client in idClient.Keys)
            {
                if (client != sender && client != null)
                {
                    await client.GetStream().WriteAsync(buffer, 0, buffer.Length);
                    await client.GetStream().WriteAsync(imageBytes, 0, imageBytes.Length);
                }
            }

        }
        void UpdateClients()
        {
            clientsTbx.Text = "";
            foreach (String id in idClient.Values)
            {

                if (clientsTbx.InvokeRequired)
                {
                    clientsTbx.Invoke(new MethodInvoker(delegate { clientsTbx.AppendText(id + Environment.NewLine); }));
                }
                else
                {
                    clientsTbx.AppendText(id + Environment.NewLine);
                }
            }
        }
    }


}