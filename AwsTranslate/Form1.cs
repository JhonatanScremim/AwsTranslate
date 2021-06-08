using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.Translate;
using Amazon.Translate.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwsTranslate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string credentialName = "AWS Educate";
        AWSCredentials credentials;
        private static readonly RegionEndpoint region = RegionEndpoint.USEast1;

        AmazonS3Client client;

        private readonly string sourceLang = "pt-br";
        private readonly string targetLang = "en-us";



        private void GetCredentials()
        {
            var chain = new CredentialProfileStoreChain();
            if (!chain.TryGetAWSCredentials(credentialName, out credentials))
            {
                MessageBox.Show("Erro ao pegar credencial");
            }
        }

        private void S3Client()
        {
            GetCredentials();
            client = new AmazonS3Client(credentials, region);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GetCredentials();

            using (var clientTranslate = new AmazonTranslateClient(credentials))
            {
                var request = new TranslateTextRequest()
                {
                    SourceLanguageCode = sourceLang,
                    TargetLanguageCode = targetLang,
                    Text = "Mesa cadeira livro campo tenis"
                };


                var response = await clientTranslate.TranslateTextAsync(request);
            }


        }
    }
}
