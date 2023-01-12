using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Text;
using Newtonsoft.Json;


namespace TokenChecker
{
    public partial class Form1 : Form
    {
        int done = 0;
        int bad = 0;
        
        public Form1()
        {
            InitializeComponent();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/uhoeb");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fileStream = ofd.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    List<string> lines = new List<string>();
                    string Token;

                    lines.AddRange(File.ReadAllLines(ofd.FileName));

                    Label2.Text = string.Format("Tokens Count : {0}", lines.Count);

                    while ((Token = reader.ReadLine()) != null)
                    {
                        
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/accounts/fb_profile/");

                        byte[] MainData = Encoding.UTF8.GetBytes(string.Format("profilePicSize=88&accessToken="+ Token));

                        req.Method = "POST";
                        req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36";
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.Headers.Add("X-CSRFToken", "missing");
                        req.ContentLength = MainData.Length;

                        Stream stm = req.GetRequestStream();
                        stm.Write(MainData, 0, MainData.Length);
                        stm.Dispose();
                        stm.Close();

                        HttpWebResponse res;
                        try
                        {

                            res = (HttpWebResponse)req.GetResponse();

                        }
                        catch (WebException ep)
                        {

                            res = (HttpWebResponse)ep.Response;
                            
                        }

                        using (StreamReader stmm = new StreamReader(res.GetResponseStream()))
                        { 
                            
                            string response = stmm.ReadToEnd();
                            String json = response;

                            if (response.Contains("email"))
                            {
                                
                                done++;
                                label3.Text = string.Format("> Good : {0} | Bad : {1} <", done, bad);

                                dynamic meResponse = JsonConvert.DeserializeObject<dynamic>(json);
                                string Email = meResponse.meResponse.email;
                              
                                string Id = meResponse.meResponse.id;

                                string Name = meResponse.meResponse.name;


                                if (response.Contains("igAccount"))
                                {

                                    dynamic igAccount = JsonConvert.DeserializeObject<dynamic>(json);
                                    string User = igAccount.igAccount.username;

                                    File.AppendAllText("Good-Tokens.txt", $"Token: {Token}\nEmail: {Email}\nUser: {User}\nId: {Id}\nName: {Name}\n\n");
                                    
                                }
                                else
                                {
                                    string User = "No User";

                                    File.AppendAllText("Good-Tokens.txt", $"Token: {Token}\nEmail: {Email}\nUser: {User}\nId: {Id}\nName: {Name}\n\n");
                                    
                                }
                                
                            }
                            

                            else
                            {
                                bad++;
                                label3.Text = string.Format("> Good : {0} | Bad : {1} <", done, bad);
                            }
                            

                            
                        }


                    }
                }
                MessageBox.Show("DONE CHECKING <3", "@dramaticspace");
                fileStream.Close();
            }
        }

        
    }
}
