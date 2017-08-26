using CMS.FormEngine.Web.UI;
using CMS.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CMSApp.CMSFormControls
{
    public partial class DictionaryInput : FormEngineUserControl
    {
        private Dictionary<string, string> _dic;
        private string sKey;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            sKey = $"dicItems_{CurrentPageInfo.DocumentGUID}";
        }

        public override object Value
        {
            get
            {
                EnsureItems();
                return ItemsToString();
            }
            set
            {
                var str = ValidationHelper.GetString(value, string.Empty);
                EnsureItems();
                if (!string.IsNullOrEmpty(str))
                {
                    SetValues(str);
                }
            }
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    var data = (KeyValuePair<string, string>)e.Item.DataItem;
                    var key = e.Item.FindControl("txtKey") as HtmlGenericControl;
                    var val = e.Item.FindControl("txtValue") as HtmlGenericControl;

                    key.InnerText = data.Key;
                    val.InnerText = data.Value;
                }
                catch
                {

                }
            }
        }

        private void EnsureItems()
        {
            if (Session[sKey] != null)
            {
                var json = Session[sKey] as string;
                SetValues(json);
            }
            else
            {
                _dic = new Dictionary<string, string>();
            }
        }

        private object ItemsToString()
        {
            return JsonConvert.SerializeObject(_dic);
        }

        private void SetValues(string str)
        {
            var json = str;
            if (!string.IsNullOrEmpty(json))
                _dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            Session[sKey] = ItemsToString();

            rptItems.DataSource = _dic;
            rptItems.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txbKey.Text.Length * txbValue.Text.Length == 0)
                return;

            if (!_dic.ContainsKey(txbKey.Text))
                _dic.Add(txbKey.Text, txbValue.Text);
            else
                _dic[txbKey.Text] = txbValue.Text;

            SetValues(null);
        }
        
        protected void btnDel_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.CommandArgument == null)
                return;

            var arg = btn.CommandArgument;
            _dic.Remove(arg);
            SetValues(null);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.CommandArgument == null)
                return;

            var arg = btn.CommandArgument;
        }
    }
}