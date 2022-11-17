using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JsonViewer.JsonNode;

namespace JsonViewer
{
    public class JsonNode : IEquatable<JsonNode>
    {
        private JToken _token;
        private string _name;
        private bool _has_children;
        public JsonNode[] _children;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public bool HasChildren
        {
            get { return _has_children; }
        }
        public JsonNode[] Children
        {
            get { return _children; }
        }
        
        public JsonNode(JToken token, string name)
        {
            _token = token; ;
            _name = name;
            if (_name == null)
            {
                _name = GetName();
            }
            _children = GetChildren();
            _has_children = _children.Length > 0;
        }

        public JsonNode(JToken token)
            :this(token, null)
        {
        }

        public bool Equals(JsonNode? other)
        {
            return other._token == this._token;
        }

        protected string GetName()
        {
            if (_token is JValue)
            {
                return $"JValue: {_token.ToString()}";
            }
            else if (_token is JObject)
            {
                JObject obj = (JObject)_token;
                return "(object)";
            }
            else if (_token is JProperty)
            {
                JProperty prop = (JProperty)_token;
                bool has_subchildren = prop.Value.Children().Any();
                string str = has_subchildren ?
                prop.Name :
$"{prop.Name}: {prop.Value}";
                return str;
            }
            return _name;
        }

        protected JsonNode[] GetChildren()
        {
            List<JsonNode> result = new List<JsonNode>();

            if (_token == null)
            {
                return result.ToArray();
            } // Is null

            if (_token is JValue)
            {
            }
            else if (_token is JObject)
            {
                JObject obj = (JObject)_token;
                foreach (JProperty property in obj.Properties())
                {
                    bool has_subchildren = property.Value.Children().Any();
                    string str = has_subchildren ?
                    property.Name :
$"{property.Name}: {property.Value}";

                    JsonNode child = new JsonNode(property);
                    result.Add(child);
                }
            }
            else if (_token is JProperty)
            {
                JProperty property = (JProperty)_token;
                foreach (JToken child in property.Value.Children())
                {
                    JsonNode new_child = new JsonNode(child);
                    result.Add(new_child);
                } // foreach
            }
            else if (_token is JArray)
            {
                JArray array = (JArray)_token;
                for (int i = 0; i < array.Count(); i++)
                {
                    JsonNode child = new JsonNode(array[i], i.ToString());
                    result.Add(child);
                }
            }
            else
            {
            }
            return result.ToArray();
        }

    } // class
}