using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace chatKlient
{
    internal class ImageMsg
    {
        private Image Image { get; set; }
        public string Text { get; set; }
       
        private Form form;
      
        public ImageMsg(Image image, string text)
        {
            Image = image;
            Text = text;
            form = new Form
            {
                Text = "Image Viewer",
                Width = 600,
                Height = 500,
                BackgroundImage = Image,
                BackgroundImageLayout = ImageLayout.Zoom // Adjust the layout to better fit the form
            }; 
            
        }

        public void ShowForm() { form.ShowDialog(); }

        
    }
}
