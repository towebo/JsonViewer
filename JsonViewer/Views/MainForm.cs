using System.Diagnostics;
using System.IO;
using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JsonViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadJsonFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JToken root = JToken.Load(jsonReader);
                DisplayTreeView(root, Path.GetFileNameWithoutExtension(path));
            }
        }

        private void DisplayTreeView(JToken root, string rootName)
        {
            JsonTree.BeginUpdate();
            try
            {
                JsonTree.Nodes.Clear();
                TreeNode tNode = JsonTree.Nodes[JsonTree.Nodes.Add(new TreeNode(rootName))];
                tNode.Tag = root;

                AddNode(root, tNode);

                JsonTree.ExpandAll();
            }
            finally
            {
                JsonTree.EndUpdate();
            }
        }

        private void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token == null)
                return;
            if (token is JValue)
            {
                TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                childNode.Tag = token;
            }
            else if (token is JObject)
            {
                JObject obj = (JObject)token;
                foreach (JProperty property in obj.Properties())
                {
                    bool has_subchildren = property.Value.Children().Any();
                        string str = has_subchildren ?
                        property.Name :
$"{property.Name}: {property.Value}";

TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(str))];
                    childNode.Tag = property;
                    if (has_subchildren)
                    {
                        AddNode(property.Value, childNode);
                    }
                }
            }
            else if (token is JArray)
            {
                JArray array = (JArray)token;
                for (int i = 0; i < array.Count(); i++)
                {
                    TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                    childNode.Tag = array[i];
                    AddNode(array[i], childNode);
                }
            }
            else
            {
                Debug.WriteLine(string.Format("{0} not implemented", token.Type)); // JConstructor, JRaw
            }
        }

        private string CleanUpJson(string src)
        {
            int first_brace = src.IndexOf("[");
            int last_brace = src.LastIndexOf("]");
            string result = src.Substring(first_brace, last_brace - first_brace + 1);
            return result;
        }

        private void refreshFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string json_txt = CleanUpJson(Clipboard.GetText());
                JToken root = JToken.Parse(json_txt);
                DisplayTreeView(root, "From Clipboard");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
        }