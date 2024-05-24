using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace chatKlient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string userId;
        private Dictionary<string, ImageMsg> linkActions = new Dictionary<string, ImageMsg>();
        public Form1()
        {
            InitializeComponent();
            chatWin.LinkClicked += new LinkClickedEventHandler(chatWin_LinkClicked);
        }


        private async void btnSkicka_Click(object sender, EventArgs e)
        {
            string message = txtSendBox.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);
                    DisplayMessage("Du: " + message);
                    txtSendBox.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending message: " + ex.Message);
                    DisconnectFromServer();
                    ConnectToServer();
                }
                finally
                {
                    // DisconnectFromServer();
                }
            }
            else
            {
                MessageBox.Show("Please enter a message.");
            }
        }
        private async Task ReceiveMessage()
        {
            try
            {
                byte[] buffer = new byte[1024];
               

                
                while (client.Connected)
                {

                    int n = await stream.ReadAsync(buffer, 0, buffer.Length); // metoden k�r p� sin egna tr�d s� den kan blocka den utan problem
                    if (n == 0) break; // Connection closed.
                    string inp = Encoding.UTF8.GetString(buffer, 0, n).Trim();
                    if (inp == "")
                        continue;
                    if (inp.StartsWith("-IMG;")) //inp.Split(';')[0] == "-IMG"
                    {
                        await ReciveImage(int.Parse(inp.Split(';')[1]));
                        continue;
                    }
                        
                    DisplayMessage(inp);
                        
                }
                
            }
            catch (Exception ex)
            {
                DisplayMessage("Error: " + ex.Message);
            }
        }

        private void DisplayMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { DisplayMessage(message); }));
            }
            else
            {
                chatWin.AppendText(message + Environment.NewLine);
            }
        }
        private void AddLink(Image img)
        {
            string text = "skickade en bild tryck h�r f�r att visa: " + System.DateTime.Now.ToString();
            int start = chatWin.TextLength;
            DisplayMessage(text);
            chatWin.Select(start, text.Length);
            chatWin.SetSelectionLink(true);

            // Store the link information in the dictionary
            linkActions[text] = new ImageMsg(image: img, text: text);
            chatWin.Select(chatWin.TextLength, 0);
            chatWin.SelectionColor = chatWin.ForeColor;
            chatWin.SelectionFont = chatWin.Font;
        }
        private void chatWin_LinkClicked(object? sender, LinkClickedEventArgs e)
        {
            BackColor = Color.Azure;
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { chatWin_LinkClicked(sender, e); }));
            }
            else
            {
                
                if (linkActions.TryGetValue(e.LinkText, out ImageMsg i))
                {

                    i.ShowForm();
                }
            }


        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 12345);
                stream = client.GetStream();
                Task.Run(ReceiveMessage);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {


            ConnectToServer();
            userId = !string.IsNullOrEmpty(usernameTxtbox.Text) ? usernameTxtbox.Text : DateTime.Now.ToString();
            try
            {


                byte[] data = Encoding.UTF8.GetBytes(userId);
                await stream.WriteAsync(data, 0, data.Length);
                button1.Enabled = false;
                button1.Text = "Ansluten";
                byte[] bytes = new byte[256];
                int bytesRead = await stream.ReadAsync(bytes, 0, bytes.Length);
                chatWin.Text = Encoding.UTF8.GetString(bytes, 0, bytesRead);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
                DisconnectFromServer();
            }
        }
        private void DisconnectFromServer()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            button1.Enabled = true;
            button1.Text = "Anslut";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectFromServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddLink(Image.FromFile(@"C:\Users\olive\Pictures\r�s\20231207_221449.jpg"));
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(openFileDialog.FileName);
                        // Log the image locally
                        //AddLink(img);

                        // Convert image to byte array
                        byte[] imageBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, img.RawFormat);
                            imageBytes = ms.ToArray();
                        }

                        // Send the byte array over the network stream
                        SendImage(imageBytes);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading or sending image: " + ex.Message);
                    }
                }
            }
        }
        private async Task SendImage(byte[] imageBytes)
        {
            try
            {

                byte[] buffer = Encoding.UTF8.GetBytes("-IMG;" + imageBytes.Length.ToString() + $";{userId}");
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.WriteAsync(imageBytes, 0, imageBytes.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending image: " + ex.Message);
            }
        }
        private async Task ReciveImage(int imageSize)
        {

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

                    AddLink(Image.FromStream(ms));

                }
                else
                {
                    // Handle incomplete image data
                    MessageBox.Show("Incomplete image data received.", Text);
                }
            }

        }
    }
    
}