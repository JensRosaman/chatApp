using Microsoft.VisualBasic.ApplicationServices;
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
        private Dictionary<string, ImageMsg> linkActions = new Dictionary<string, ImageMsg>();
        public Form1()
        {
            InitializeComponent();
            chatWin.LinkClicked += new LinkClickedEventHandler(chatWin_LinkClicked);
        }


        private void btnSkicka_Click(object sender, EventArgs e)
        {
            string message = txtSendBox.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    DisplayMessage("Du: " + message);
                    txtSendBox.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending message: " + ex.Message);
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
        private void ReceiveMessage()
        {
            try
            {
                byte[] bytes = new byte[256];
                int i;
                StringBuilder data = new StringBuilder();

                while (client.Connected && (i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data.Append(Encoding.UTF8.GetString(bytes, 0, i));
                    string receivedMessage = data.ToString();
                    DisplayMessage(receivedMessage);
                    data.Clear(); // Clear the StringBuilder for the next message
                }

                if (!client.Connected)
                {
                    // If client is no longer connected, display a message indicating disconnection
                    DisplayMessage("Disconnected from server.");
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
            string text = "skickade en bild tryck här för att visa: " + System.DateTime.Now.ToString();
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
                BackColor = Color.Red;
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
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {


            ConnectToServer();
            string userId = !string.IsNullOrEmpty(usernameTxtbox.Text) ? usernameTxtbox.Text : DateTime.Now.ToString();
            try
            {


                byte[] data = Encoding.UTF8.GetBytes(userId);
                stream.Write(data, 0, data.Length);
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectFromServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddLink(Image.FromFile(@"C:\Users\olive\Pictures\rös\20231207_221449.jpg"));
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

                //byte[] prefix = Convert.FromBase64String("IMG;");
                var data = new Data
                {
                    Message = imageBytes,
                    IsImg = true
                };
                String json = JsonSerializer.Serialize(data);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                // byte[] combinedBytes = new byte[prefix.Length + imageBytes.Length];
                // Buffer.BlockCopy(prefix, 0, combinedBytes, 0, prefix.Length);
                // Buffer.BlockCopy(imageBytes, 0, combinedBytes, prefix.Length, imageBytes.Length);


                //AddLink(Image.FromStream(new MemoryStream(Convert.FromBase64String(System.Convert.ToBase64String(combinedBytes).Split(';')[1]))));
                // Send the actual image bytes
                stream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending image: " + ex.Message);
            }
        }
    }
    public class Data
    {
        
        public byte[] Message { get; set; }
        public bool IsImg { get; set; }
    }
}