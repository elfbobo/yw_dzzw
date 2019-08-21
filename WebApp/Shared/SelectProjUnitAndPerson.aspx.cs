using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore.SupportApi.Entity;
using Yawei.Common;
using Yawei.FacadeCore.Project;
using System.Data;

namespace Yawei.App.Shared
{
    public partial class SelectProjUnitAndPerson : SharedPage
    {
        protected string projGuid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            projGuid = Request["projGuid"] != null ? Request["projGuid"] : "";

            if (Request["type"] == "getUnit")
            {
                string pGuid = Request["pGuids"] != null ? Request["pGuids"] : "";
                string uName = Request["uName"] != null ? Request["uName"] : "";

                DataTable dt = SelectUnitAndPersonFacade.GetBuilderUnitsByProjGuid(pGuid, uName);
                string json = string.Empty;
                json += "{";
                json += "\"total\":" + dt.Rows.Count + ",\"rows\":[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{\"Guid\":\"" + FormatData(dt.Rows[i]["Guid"].ToString()) + "\"" +
                        ",\"ProjGuid\":\"" + FormatData(dt.Rows[i]["ProjGuid"].ToString()) + "\"" +
                        ",\"UnitsName\":\"" + FormatData(dt.Rows[i]["UnitsName"].ToString()) + "\"" +
                        ",\"UnitsType\":\"" + FormatData(dt.Rows[i]["UnitsType"].ToString()) + "\"" +
                        ",\"UnitsTypeName\":\"" +  FormatData(dt.Rows[i]["UnitsTypeName"].ToString()) + "\"" +
                        ",\"UnitsQuality\":\"" +  FormatData(dt.Rows[i]["UnitsQuality"].ToString()) + "\"" +
                        ",\"UnitsQualityName\":\"" +  FormatData(dt.Rows[i]["UnitsQualityName"].ToString()) + "\"" +
                        ",\"RegisteredNo\":\"" +  FormatData(dt.Rows[i]["RegisteredNo"].ToString()) + "\"" +
                        ",\"OrganizationCode\":\"" +  FormatData(dt.Rows[i]["OrganizationCode"].ToString()) + "\"" +
                        ",\"UnitsIntelligence\":\"" +  FormatData(dt.Rows[i]["UnitsIntelligence"].ToString()) + "\"" +
                        ",\"IntelligenceNo\":\"" + FormatData(dt.Rows[i]["IntelligenceNo"].ToString()) + "\"" +
                        ",\"LegalRepresentative\":\"" +  FormatData(dt.Rows[i]["LegalRepresentative"].ToString()) + "\"" +
                        ",\"LegalRepresentativeTel\":\"" +  FormatData(dt.Rows[i]["LegalRepresentativeTel"].ToString()) + "\"" +
                        ",\"ProjectManager\":\"" +  FormatData(dt.Rows[i]["ProjectManager"].ToString()) + "\"" +
                        ",\"ProjectManagerTel\":\"" + FormatData(dt.Rows[i]["ProjectManagerTel"].ToString()) + "\"" +
                        ",\"Contacts\":\"" +  FormatData(dt.Rows[i]["Contacts"].ToString()) + "\"" +
                        ",\"ContactsTel\":\"" +  FormatData(dt.Rows[i]["ContactsTel"].ToString()) + "\"" +
                        ",\"Principal\":\"" +  FormatData(dt.Rows[i]["Principal"].ToString()) + "\"" +
                        ",\"PrincipalTel\":\"" + FormatData(dt.Rows[i]["PrincipalTel"].ToString()) + "\"}";
                }
                json += "]}";
                Response.Write(json);
                Response.End();
            }
        }

        private static string FormatData(string data)
        {
            if (data == "\\")
                return @"\\";
            else
                return data;
        }
    }
}