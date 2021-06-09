using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Translate;
using Amazon.Translate.Model;
using System;
using System.Windows.Forms;

namespace AwsTranslate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string credentialName = "FelixAWS";
        AWSCredentials credentials;
        private static readonly RegionEndpoint region = RegionEndpoint.USEast1;

        private void GetCredentials()
        {
            var chain = new CredentialProfileStoreChain();
            if (!chain.TryGetAWSCredentials(credentialName, out credentials))
            {
                MessageBox.Show("Erro ao pegar credencial");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GetCredentials();

            using (var clientTranslate = new AmazonTranslateClient(credentials, region))
            {
                if(string.IsNullOrEmpty(comboBoxInput.Text) || string.IsNullOrEmpty(comboBoxOutput.Text) || string.IsNullOrEmpty(richTextBoxInput.Text))
                {
                    MessageBox.Show("ERRO, Campos vazios, por favor preencha todos os campos");
                    return;
                }

                string text = richTextBoxInput.Text;
                string sourceLang = GetLangCode(comboBoxInput.Text);
                string targetLang = GetLangCode(comboBoxOutput.Text);

                var request = new TranslateTextRequest()
                {
                    SourceLanguageCode = sourceLang,
                    TargetLanguageCode = targetLang,
                    Text = text
                };

                var response = await clientTranslate.TranslateTextAsync(request);

                if (response == null || string.IsNullOrEmpty(response?.TranslatedText))
                {
                    MessageBox.Show("ERRO, Resposta não encontrada.");
                    return;
                }
                richTextBoxOutput.Text = response.TranslatedText;
            }
        }


        private string GetLangCode(string combo)
        {
            combo = combo.ToLower();

            switch (combo)
            {
                case "português":
                    return "pt";
                case "inglês":
                    return "en";
                case "francês":
                    return "fr";
                case "suéco":
                    return "sv";
                case "coreano":
                    return "ko";
                case "espanhol":
                    return "es";
                default:
                    return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string WhataDelightMan = comboBoxInput.Text;
            comboBoxInput.Text = comboBoxOutput.Text;
            comboBoxOutput.Text = WhataDelightMan;
        }
    }
}
