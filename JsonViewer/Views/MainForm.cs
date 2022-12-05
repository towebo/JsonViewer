using System.Diagnostics;
using System.IO;
using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections;
using System.Linq;
using System.Media;

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
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version? assemblyVersion = assembly.GetName().Version;
            Text = $"{Text} {assemblyVersion}";

            JsonTree.CanExpandGetter = delegate (object x)
            {
                JsonNode? node = x as JsonNode;

                return (node != null) && node.HasChildren;
            }; // Can Expand
            JsonTree.ChildrenGetter = delegate (object x)
            {
                JsonNode? node = x as JsonNode;
                return new ArrayList(node .Children);
            };

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                string fn = args[1];
                if (File.Exists(fn))
                {
                    LoadJsonFromFile(fn);
                } // File Exists
            } // More than the exe file
        }

        private void LoadJsonFromFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    using (JsonTextReader jsonReader = new JsonTextReader(reader))
                    {
                        JToken root = JToken.Load(jsonReader);
                        DisplayTreeView(root, Path.GetFileNameWithoutExtension(path));
                    } // using
                } // using
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayTreeView(JToken root, string rootName, bool clearExisting = true)
        {
            JsonTree.BeginUpdate();
            if (clearExisting)
            {
                JsonTree.ClearObjects();
            } // Do clear
            try
            {
                JsonNode root_node = new JsonNode(root, rootName);
                JsonTree.AddObject(root_node);
                JsonTree.Expand(root_node);
                Focus();
                JsonTree.Focus();

                SystemSounds.Asterisk.Play();
            }
            finally
            {
                JsonTree.EndUpdate();
            }
        }

        private void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token == null)
            {
                return;
            } // Is null
            if (token is JValue)
            {
                TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                childNode.Name = childNode.Text;
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
                    childNode.Name = childNode.Text;
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
                    childNode.Name = childNode.Text;
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
            int first_bracket = src.IndexOf("[");
            int first_brace = src.IndexOf("{");
            int last_bracket = src.LastIndexOf("]");
            int last_brace = src.LastIndexOf("}");

            int start = Math.Min(first_bracket, first_brace);
            int end = Math.Max(last_bracket, last_brace);

            if (start < 0 ||
                end < start)
            {
                throw new Exception("The text seems malformed and start and end can't be determined properly.");
            }

            string result = src.Substring(start, end - start + 1);
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

        private void openJsonFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != OpenJsonFileDialog.ShowDialog())
            {
                return;
            } // Cancelled
                
                LoadJsonFromFile(OpenJsonFileDialog.FileName);
        }

        private void JsonTree_Collapsed(object sender, BrightIdeasSoftware.TreeBranchCollapsedEventArgs e)
        {
            ListViewItem lv = new ListViewItem();
            
            //e.Model

        }

        private void JsonTree_Expanded(object sender, BrightIdeasSoftware.TreeBranchExpandedEventArgs e)
        {

        }
    } // class

        }