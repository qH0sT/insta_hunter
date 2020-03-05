using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace insta_user_hunt
{
    public partial class Form1 : Form
    {
        /*
  _________                                     ____  __.           __                      
 /   _____/____     ____   ____ ___________    |    |/ _|____      |__| _____   ___________ 
 \_____  \\__  \   / ___\ /  _ \\____ \__  \   |      < \__  \     |  |/     \_/ __ \_  __ \
 /        \/ __ \_/ /_/  >  <_> )  |_> > __ \_ |    |  \ / __ \_   |  |  Y Y  \  ___/|  | \/
/_______  (____  /\___  / \____/|   __(____  / |____|__ (____  /\__|  |__|_|  /\___  >__|   
        \/     \//_____/        |__|       \/          \/    \/\______|     \/     \/       
        
             */
        public Form1()
        {
            InitializeComponent();
        }
        private async Task<bool> Bul(string kullaniciAdi)
        {
            try
            {
                await Task.Run(() =>
                {
                    Process cmd = new Process();
                    ProcessStartInfo sinfo = new ProcessStartInfo("CMD.exe",
                    @"/C python main.py --username " + kullaniciAdi)
                    {
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    cmd.StartInfo = sinfo;
                    cmd.Start();
                    cmd.WaitForExit();
                });
                return true;
            }
            catch (Exception) { return false; }
        }
        Dictionary<string, string> translation = new Dictionary<string, string>() {
            {"Username","Kullanıcı Adı: "},
            {"Profile name","Profil ismi: " },
            {"URL","Link: " },
            {"Followers","Takipçiler: " },
            {"Following","Takip ediyor: " },
            {"Posts","Gönderiler: " },
            {"Bio","Biyografi: " },
            {"profile_pic_url","Profil foto url: " },
            {"is_business_account","Business Hesap? " },
            {"connected_to_fb","Facebook'a bağlı: " },
            {"externalurl","Dış bağlantı: " },
            {"joined_recently","Son giriş: " },
            {"business_category_name","Business kategori ismi: " },
            {"is_private","Özel? " },
            {"is_verified","Doğrulandı? " }
        };
        string[] dataForParsing = {
            "Username",
            "Profile name",
            "URL",
            "Followers",
            "Following",
            "Posts",
            "Bio",
            "profile_pic_url",
            "is_business_account",
            "connected_to_fb",
            "externalurl",
            "joined_recently",
            "business_category_name",
            "is_private",
            "is_verified"
        };
        private async void button1_Click(object sender, EventArgs e)
        {
            if (await Bul(textBox1.Text))
            {
                try {
                    dynamic attriubutes = JObject.Parse(File.ReadAllText(Environment.CurrentDirectory + "\\" + textBox1.Text +
                        "\\data.txt"));
                    for (int k = 0; k < dataForParsing.Length; k++) {
                        listBox1.Items.Add(translation[dataForParsing[k]] + attriubutes[dataForParsing[k]].ToString().Replace("True", "Evet").
                            Replace("False", "Hayır").Replace("None", "Hayır").Replace("Yes", "Evet"));
                    }

                    string img = (Environment.CurrentDirectory + "\\" + textBox1.Text +
                        "\\profile_pic.jpg");
                    pictureBox1.ImageLocation = img;
                }
                catch (Exception){ }
           }
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count > 0) {
                Clipboard.SetText((string)listBox1.SelectedItems[0]); }
        }
    }
}
