using CMS.EventLog;
using CMS.FormEngine.Web.UI;
using CMS.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CMSApp.CMSFormControls
{
    public partial class DynamicInput : FormEngineUserControl
    {
        private const string _selector = "js-dynInput";
        public int Elements
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("Elements"), 0);
            }
            set
            {
                SetValue("Elements", value);
            }
        }

        public override object Value
        {
            get
            {
                return GetTextValues();
            }
            set
            {
                var strVal = Convert.ToString(value);
                if (!string.IsNullOrEmpty(strVal) && strVal != "0")
                {
                    Populate(strVal);
                }
            }
        }

        private void Populate(string strVal)
        {
            if (pnlItems.Controls.Count == 1)
            {
                List<string> list = null;
                try
                {
                    list = JsonConvert.DeserializeObject<List<string>>(strVal);
                }
                catch(Exception ex)
                {
                    EventLogProvider.LogException("E", "DYNAMIC_INPUT", ex);
                }

                if (list != null)
                {
                    Elements = list.Count;
                    txtElems.Text = Elements.ToString();
                    foreach (var item in list)
                    {
                        var txb = new TextBox()
                        {
                            Text = item,
                            CssClass = $"form-control {_selector}"
                        };
                        pnlItems.Controls.Add(txb);
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int elems = -1;
            Int32.TryParse(txtElems.Text, out elems);

            if (elems != -1 && elems != Elements)
            {
                if (elems > Elements)
                {
                    int ii = Elements;
                    for (; ii < elems; ii++)
                    {
                        var txt = new TextBox
                        {
                            CssClass = $"form-control {_selector}"
                        };
                        txt.Attributes["placeholder"] = "Enter text...";
                        pnlItems.Controls.Add(txt);
                    }
                    Elements = elems;
                    txtElems.Text = Elements.ToString();
                }
                else
                {
                    var tbs = pnlItems.Controls.OfType<TextBox>().ToList().Take(elems);
                    pnlItems.Controls.Clear();
                    foreach (var tb in tbs)
                    {
                        pnlItems.Controls.Add(tb);
                    }
                }
            }
        }

        private string GetTextValues()
        {
            var tbs = pnlItems.Controls
                .OfType<TextBox>()
                .Where(p => !string.IsNullOrEmpty(p.Text) && p.CssClass.Contains(_selector))
                .Select(p => p.Text)
                .ToList();

            if (!tbs.Any())
                return string.Empty;

            return JsonConvert.SerializeObject(tbs);
        }
    }
}