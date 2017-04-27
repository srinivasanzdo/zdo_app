using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.IO;
using System.Text;
using System.Configuration;

using System.Net;
using System.Net.Mail;

using Newtonsoft.Json;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class Functions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Class start

    public class linkSave
    {
        public string agent_id { get; set; }
        public string url_id { get; set; }
        public string status { get; set; }
        public string admission_number { get; set; }
    }

    [WebMethod]
    public static List<linkSave> GetLink_Value(string url_id)
    {
        string json = string.Empty;
        List<linkSave> objlistlinkSave = new List<linkSave>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/link.txt"));
        string data = File.ReadAllText(fileSavePath1);
        linkSave linkSavecls = new linkSave();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistlinkSave = new List<linkSave>();
            objlistlinkSave = JsonConvert.DeserializeObject<List<linkSave>>(data);
            objlistlinkSave = (from m in objlistlinkSave where m.url_id == url_id select m).ToList();
            if (objlistlinkSave.Count > 0)
            {
                linkSavecls.agent_id = objlistlinkSave[0].agent_id;
                linkSavecls.url_id = objlistlinkSave[0].url_id;
                linkSavecls.status = objlistlinkSave[0].status;
                linkSavecls.admission_number = objlistlinkSave[0].admission_number;
            }
        }
        return objlistlinkSave;
    }

    [WebMethod]
    public static string updateURLdata(string url_id, string status, string admission_number)
    {
        string json = string.Empty;
        List<linkSave> objlistlinkSave = new List<linkSave>();
        List<linkSave> objlistlinkSave1 = new List<linkSave>();
        List<linkSave> linkSavelist = new List<linkSave>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/link.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistlinkSave1 = JsonConvert.DeserializeObject<List<linkSave>>(data);
            objlistlinkSave1 = (from m in objlistlinkSave1 select m).ToList();
            if (objlistlinkSave1.Count != 0)
            {
                objlistlinkSave = JsonConvert.DeserializeObject<List<linkSave>>(data);
                foreach (linkSave loopdata in objlistlinkSave)
                {
                    linkSave _data1 = new linkSave();
                    if (loopdata.url_id == url_id)
                    {
                        loopdata.status = status;
                        loopdata.admission_number = admission_number;
                        _data1 = loopdata;
                    }
                    else
                    {
                        _data1 = loopdata;
                    }
                    linkSavelist.Add(_data1);
                }
            }
        }
        File.WriteAllText(fileSavePath1, string.Empty);
        json = JsonConvert.SerializeObject(linkSavelist.ToArray());
        File.WriteAllText(fileSavePath1, json);
        return "true";
    }



    [WebMethod]
    public static string updateActiveURL(string status, string admission_number)
    {
        string json = string.Empty;
        List<linkSave> objlistlinkSave = new List<linkSave>();
        List<linkSave> objlistlinkSave1 = new List<linkSave>();
        List<linkSave> linkSavelist = new List<linkSave>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/link.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistlinkSave1 = JsonConvert.DeserializeObject<List<linkSave>>(data);
            objlistlinkSave1 = (from m in objlistlinkSave1 select m).ToList();
            if (objlistlinkSave1.Count != 0)
            {
                objlistlinkSave = JsonConvert.DeserializeObject<List<linkSave>>(data);
                foreach (linkSave loopdata in objlistlinkSave)
                {
                    linkSave _data1 = new linkSave();
                    if (loopdata.admission_number == admission_number)
                    {
                        loopdata.status = status;
                        _data1 = loopdata;
                    }
                    else
                    {
                        _data1 = loopdata;
                    }
                    linkSavelist.Add(_data1);
                }
            }
        }
        File.WriteAllText(fileSavePath1, string.Empty);
        json = JsonConvert.SerializeObject(linkSavelist.ToArray());
        File.WriteAllText(fileSavePath1, json);
        return "true";
    }



    [WebMethod]
    public static string saveURLdata(string agent_id, string url_id, string status, string admission_number)
    {
        string ret = "false";
        string json = string.Empty;
        linkSave _data = new linkSave();
        List<linkSave> objlistlinkSave = new List<linkSave>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/link.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistlinkSave = new List<linkSave>();
            objlistlinkSave = JsonConvert.DeserializeObject<List<linkSave>>(data);
            string editdata = data.Replace("]", ",");
            _data = new linkSave();
            _data.agent_id = agent_id;
            _data.url_id = url_id;
            _data.status = status;
            _data.admission_number = admission_number;
            json = JsonConvert.SerializeObject(_data);
            json = editdata + json + "]";
        }
        else
        {
            _data = new linkSave();
            _data.agent_id = agent_id;
            _data.url_id = url_id;
            _data.status = status;
            _data.admission_number = admission_number;
            objlistlinkSave = new List<linkSave>();
            objlistlinkSave.Add(_data);
            json = JsonConvert.SerializeObject(objlistlinkSave.ToArray());
        }
        File.WriteAllText(fileSavePath1, json);
        ret = "true";
        return ret;
    }



    public class agent
    {
        public string agent_slno { get; set; }
        public string agent_id { get; set; }
        public string agent_name { get; set; }
        public string agent_gender { get; set; }
        public string agent_email { get; set; }
        public string agent_phone { get; set; }
        public string agent_username { get; set; }
        public string agent_password { get; set; }
        public string agent_photo { get; set; }
        public string agent_status { get; set; }
        public string status { get; set; }
    }

     [WebMethod]
    public static string saveAgent(string dagent_slno, string dagent_id, string dagent_name, string dagent_gender, string dagent_email, string dagent_phone, string dagent_username, string dagent_password, string dagent_status, string dstatus)
    {
        string ret = "false";
        string json = string.Empty;
        agent _data = new agent();
        List<agent> objlistagent = new List<agent>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/agent.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistagent = new List<agent>();
            objlistagent = JsonConvert.DeserializeObject<List<agent>>(data);
            string editdata = data.Replace("]", ",");
            _data = new agent();
            _data.agent_slno = dagent_slno;
            _data.agent_id = dagent_id;
            _data.agent_name = dagent_name;
            _data.agent_gender = dagent_gender;
            _data.agent_email = dagent_email;
            _data.agent_phone = dagent_phone;
            _data.agent_username = dagent_username;
            _data.agent_password = dagent_password;
            _data.agent_photo = "no file upload";
            _data.agent_status = dagent_status;
            _data.status = dstatus;
            json = JsonConvert.SerializeObject(_data);
            json = editdata + json + "]";
        }
        else
        {
            _data = new agent();
            _data.agent_slno = dagent_slno;
            _data.agent_id = dagent_id;
            _data.agent_name = dagent_name;
            _data.agent_gender = dagent_gender;
            _data.agent_email = dagent_email;
            _data.agent_phone = dagent_phone;
            _data.agent_username = dagent_username;
            _data.agent_password = dagent_password;
            _data.agent_photo = "no file upload";
            _data.agent_status = dagent_status;
            _data.status = dstatus;
            objlistagent = new List<agent>();
            objlistagent.Add(_data);
            json = JsonConvert.SerializeObject(objlistagent.ToArray());
        }
        File.WriteAllText(fileSavePath1, json);
        ret = "true";
        return ret;
    }
    

    [WebMethod]
    public static List<agent> getAgentData()
    {
        string json = string.Empty;
        List<agent> objlistagent = new List<agent>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/agent.txt"));
        string data = File.ReadAllText(fileSavePath1);
        agent agentcls = new agent();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistagent = new List<agent>();
            objlistagent = JsonConvert.DeserializeObject<List<agent>>(data);
            objlistagent = (from m in objlistagent select m).ToList();
            if (objlistagent.Count > 0)
            {
                agentcls.agent_id = objlistagent[0].agent_id;
                agentcls.agent_name = objlistagent[0].agent_name;
                agentcls.agent_gender = objlistagent[0].agent_gender;
                agentcls.agent_email = objlistagent[0].agent_email;
                agentcls.agent_phone = objlistagent[0].agent_phone;
                agentcls.agent_phone = objlistagent[0].agent_photo; 
            }
        }
        return objlistagent;
    }

    [WebMethod]
    public static List<agent> GetSessionAgent(string dagent_id)
    {
        string json = string.Empty;
        List<agent> objlistagent = new List<agent>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/agent.txt"));
        string data = File.ReadAllText(fileSavePath1);
        agent agentcls = new agent();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistagent = new List<agent>();
            objlistagent = JsonConvert.DeserializeObject<List<agent>>(data);
            objlistagent = (from m in objlistagent where m.agent_id == dagent_id select m).ToList();
            if (objlistagent.Count > 0)
            {
                agentcls.agent_id = objlistagent[0].agent_id;
                agentcls.agent_name = objlistagent[0].agent_name;
                agentcls.agent_gender = objlistagent[0].agent_gender;
                agentcls.agent_email = objlistagent[0].agent_email;
                agentcls.agent_phone = objlistagent[0].agent_phone;
                agentcls.agent_phone = objlistagent[0].agent_photo;
            }
        }
        return objlistagent;
    }

    [WebMethod]
    public static string checkUserName(string duname)
    {
        string ret = "false";
        string json = string.Empty;
        List<agent> objlistcust = new List<agent>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/agent.txt"));
        string data = File.ReadAllText(fileSavePath1);
        agent usercls = new agent();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistcust = new List<agent>();
            objlistcust = JsonConvert.DeserializeObject<List<agent>>(data);
            objlistcust = (from m in objlistcust where m.agent_email == duname select m).ToList();
            if (objlistcust.Count > 0)
            {
                ret = "true";
            }
        }
        return ret;
    }


    public class application
    {
          public string admision_slno { get; set; }        
		  public string admission_number { get; set; }
		  public string admision_name { get; set; }
	      public string admision_gender { get; set; }
		  public string admision_nric { get; set; }
		  public string admision_dob { get; set; }
          public string admision_application_date { get; set; }
		  public string admision_time { get; set; }
		  public string admision_occupation { get; set; }
		  public string admision_contact { get; set; }
		  public string admision_nok_name { get; set; }
		  public string admision_nok_contact { get; set; }
		  public string admision_pa_plan { get; set; }	  
		  public string admision_exclusion_plan { get; set; }
		  public string admision_pre_existing_condition { get; set; }
		  public string admision_hign_bp { get; set; }
		  public string admision_diabetes { get; set; }
		  public string admision_high_cholesterol { get; set; }
		  public string admision_pending_claims { get; set; }
          public string admision_nric_front_screen { get; set; }
		  public string admision_nric_back_screen { get; set; }
		  public string admision_policy_file1 { get; set; }
		  public string admision_policy_file2 { get; set; }
		  public string admision_policy_file3 { get; set; }
		  public string admision_policy_file4 { get; set; }
		  public string admision_policy_file5 { get; set; }
		  public string admision_policy_file6 { get; set; }
	      public string admision_policy_file7 { get; set; }
		  public string admision_policy_file8 { get; set; }
		  public string admision_policy_file9 { get; set; }
		  public string admision_policy_file10 { get; set; }
		  public string admision_cash_pay_mc { get; set; }
		  public string admision_cause_complaint{ get; set; }
		  public string admision_sign_symptoms{ get; set; } 
		  public string admision_preferred_date_admission{ get; set; }
	      public string admision_insurance_agent_id { get; set; }
   	      public string admision_insurance_agent_name { get; set; }
		  public string admision_insurance_agent_contact { get; set; }
		  public string admision_remarks_admin { get; set; }
		  public string admision_remarks_agent{ get; set; } 
		  public string admision_status { get; set; }
          public string status { get; set; }
          public string saveType { get; set; }
          public string submissionType { get; set; }
          public string appSave { get; set; }
          public string admision_remarks_client { get; set; }
    }
    //Class end


    //file upload work start
    [System.Web.Services.WebMethod]
    public static string SaveNRICfront(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveNRICback(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile1(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile2(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile3(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile4(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile5(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile6(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile7(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile8(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile9(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string SaveFile10(string Based64BinaryString)
    {
        string result = "";
        try
        {
            string format = "";
            string path = HttpContext.Current.Server.MapPath("files/");
            string name = DateTime.Now.ToString("hhmmss");

            if (Based64BinaryString.Contains("data:application/pdf;base64,"))
            {
                format = "pdf";
            }
            if (Based64BinaryString.Contains("data:image/jpg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/jpeg;base64,"))
            {
                format = "jpg";
            }
            if (Based64BinaryString.Contains("data:image/png;base64,"))
            {
                format = "png";
            }
            if (Based64BinaryString.Contains("data:text/plain;base64,"))
            {
                format = "txt";
            }

            string str = Based64BinaryString.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:application/pdf;base64,", " ");//pdf check
            str = str.Replace("data:text/plain;base64,", " ");//text file check

            byte[] data = Convert.FromBase64String(str);

            if (format == "png")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".png");
                var imgpath = "files/file" + name + ".png";
                result = imgpath;
            }
            else if (format == "jpg")
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".jpg");
                var imgpath = "files/file" + name + ".jpg";
                result = imgpath;
            }
            else
            {
                MemoryStream ms = new MemoryStream(data, 0, data.Length);
                ms.Write(data, 0, data.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(path + "/file" + name + ".pdf");
                var imgpath = "files/file" + name + ".pdf";
                result = imgpath;
            }
        }
        catch (Exception ex)
        {
            result = "Error : " + ex;
        }
        return result;
    }

    //file upload work end  
    
    

    [WebMethod]
    public static string mobile_App_Ret_Client(string dadmission_number, string dadmision_remarks_client, string status)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        List<application> objlistapplication1 = new List<application>();
        List<application> applicationlist = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication1 = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication1 = (from m in objlistapplication1 select m).ToList();
            if (objlistapplication1.Count != 0)
            {
                objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
                foreach (application loopdata in objlistapplication)
                {
                    application _data1 = new application();
                    if (loopdata.admission_number == dadmission_number)
                    {
                        loopdata.admision_remarks_client = dadmision_remarks_client;
                        loopdata.status = status;
                        _data1 = loopdata;
                    }
                    else
                    {
                        _data1 = loopdata;
                    }
                    applicationlist.Add(_data1);
                }
            }
        }
        File.WriteAllText(fileSavePath1, string.Empty);
        json = JsonConvert.SerializeObject(applicationlist.ToArray());
        File.WriteAllText(fileSavePath1, json);
        return "true";
    }


    
[WebMethod]
    public static string update_Admin_Status(string dadmission_number, string dadmision_status, string dadmision_remarks_admin, string status)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        List<application> objlistapplication1 = new List<application>();
        List<application> applicationlist = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication1 = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication1 = (from m in objlistapplication1 select m).ToList();
            if (objlistapplication1.Count != 0)
            {
                objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
                foreach (application loopdata in objlistapplication)
                {
                    application _data1 = new application();
                    if (loopdata.admission_number == dadmission_number)
                    {
                        loopdata.admision_status = dadmision_status;
                        loopdata.admision_remarks_admin = dadmision_remarks_admin;
                        loopdata.status = status;
                        _data1 = loopdata;
                    }
                    else
                    {
                        _data1 = loopdata;
                    }
                    applicationlist.Add(_data1);
                }
            }
        }
        File.WriteAllText(fileSavePath1, string.Empty);
        json = JsonConvert.SerializeObject(applicationlist.ToArray());
        File.WriteAllText(fileSavePath1, json);
        return "true";
    }



[WebMethod]
public static string updateAmendApplicationWork(string admission_number, string admision_name, string admision_gender, string admision_nric, string admision_dob, string admision_application_date, string admision_occupation, string admision_contact, string admision_nok_name, string admision_nok_contact, string admision_pa_plan, string admision_exclusion_plan, string admision_pre_existing_condition, string admision_hign_bp, string admision_diabetes, string admision_high_cholesterol, string admision_pending_claims, string admision_cash_pay_mc, string admision_cause_complaint, string admision_sign_symptoms, string admision_preferred_date_admission, string admision_insurance_agent_id, string admision_insurance_agent_name, string admision_insurance_agent_contact, string admision_remarks_agent, string admision_status, string status)
{
    string json = string.Empty;
    List<application> objlistapplication = new List<application>();
    List<application> objlistapplication1 = new List<application>();
    List<application> applicationlist = new List<application>();
    var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
    string data = File.ReadAllText(fileSavePath1);
    if (data != "" && data != string.Empty && data != " ")
    {
        objlistapplication1 = JsonConvert.DeserializeObject<List<application>>(data);
        objlistapplication1 = (from m in objlistapplication1 select m).ToList();
        if (objlistapplication1.Count != 0)
        {
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            foreach (application loopdata in objlistapplication)
            {
                application _data1 = new application();
                if (loopdata.admission_number == admission_number)
                {
                    loopdata.admision_name = admision_name;
                    loopdata.admision_gender = admision_gender;
                    loopdata.admision_nric = admision_nric;
                    loopdata.admision_dob = admision_dob;
                    loopdata.admision_occupation = admision_occupation;
                    loopdata.admision_contact = admision_contact;
                    loopdata.admision_nok_name = admision_nok_name;
                    loopdata.admision_nok_contact = admision_nok_contact;
                    loopdata.admision_pa_plan = admision_pa_plan;
                    loopdata.admision_exclusion_plan = admision_exclusion_plan;
                    loopdata.admision_pre_existing_condition = admision_pre_existing_condition;
                    loopdata.admision_hign_bp = admision_hign_bp;
                    loopdata.admision_diabetes = admision_diabetes;
                    loopdata.admision_high_cholesterol = admision_high_cholesterol;
                    loopdata.admision_pending_claims = admision_pending_claims;
                    loopdata.admision_cash_pay_mc = admision_cash_pay_mc;
                    loopdata.admision_cause_complaint = admision_cause_complaint;
                    loopdata.admision_sign_symptoms = admision_sign_symptoms;
                    loopdata.admision_preferred_date_admission = admision_preferred_date_admission;
                    loopdata.admision_insurance_agent_id = admision_insurance_agent_id;
                    loopdata.admision_insurance_agent_name = admision_insurance_agent_name;
                    loopdata.admision_insurance_agent_contact = admision_insurance_agent_contact;
                    loopdata.admision_remarks_agent = admision_remarks_agent;
                    loopdata.admision_status = admision_status;
                    loopdata.status = status;

                    _data1 = loopdata;
                }
                else
                {
                    _data1 = loopdata;
                }
                applicationlist.Add(_data1);
            }
        }
    }
    File.WriteAllText(fileSavePath1, string.Empty);
    json = JsonConvert.SerializeObject(applicationlist.ToArray());
    File.WriteAllText(fileSavePath1, json);
    return "true";
}
        

    [WebMethod]
    public static string updateApplication(string admission_number, string admision_name, string admision_gender, string admision_nric, string admision_dob, string admision_application_date, string admision_occupation, string admision_contact, string admision_nok_name, string admision_nok_contact, string admision_pa_plan, string admision_exclusion_plan, string admision_pre_existing_condition, string admision_hign_bp, string admision_diabetes, string admision_high_cholesterol, string admision_pending_claims, string admision_cash_pay_mc, string admision_cause_complaint, string admision_sign_symptoms, string admision_preferred_date_admission, string admision_insurance_agent_id, string admision_insurance_agent_name, string admision_insurance_agent_contact, string admision_status, string status, string saveType, string submissionType, string appSave)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        List<application> objlistapplication1 = new List<application>();
        List<application> applicationlist = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication1 = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication1 = (from m in objlistapplication1 select m).ToList();
            if (objlistapplication1.Count != 0)
            {
                objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
                foreach (application loopdata in objlistapplication)
                {
                    application _data1 = new application();
                    if (loopdata.admission_number == admission_number)
                    {
                        loopdata.admision_name = admision_name;
                        loopdata.admision_gender = admision_gender;
                        loopdata.admision_nric = admision_nric;
                        loopdata.admision_dob = admision_dob;
                        loopdata.admision_occupation = admision_occupation;
                        loopdata.admision_contact = admision_contact;
                        loopdata.admision_nok_name = admision_nok_name;
                        loopdata.admision_nok_contact = admision_nok_contact;
                        loopdata.admision_pa_plan = admision_pa_plan;
                        loopdata.admision_exclusion_plan = admision_exclusion_plan;
                        loopdata.admision_pre_existing_condition = admision_pre_existing_condition;
                        loopdata.admision_hign_bp = admision_hign_bp;
                        loopdata.admision_diabetes = admision_diabetes;
                        loopdata.admision_high_cholesterol = admision_high_cholesterol;
                        loopdata.admision_pending_claims = admision_pending_claims;
                        loopdata.admision_cash_pay_mc = admision_cash_pay_mc;
                        loopdata.admision_cause_complaint = admision_cause_complaint;
                        loopdata.admision_sign_symptoms = admision_sign_symptoms;
                        loopdata.admision_preferred_date_admission = admision_preferred_date_admission;
                        loopdata.admision_insurance_agent_id = admision_insurance_agent_id;
                        loopdata.admision_insurance_agent_name = admision_insurance_agent_name;
                        loopdata.admision_insurance_agent_contact = admision_insurance_agent_contact;
                        loopdata.admision_status = admision_status;
                        loopdata.status = status;
                        loopdata.saveType = saveType;
                        loopdata.submissionType = submissionType;
                        loopdata.appSave = appSave;
                        _data1 = loopdata;
                    }
                    else
                    {
                        _data1 = loopdata;
                    }
                    applicationlist.Add(_data1);
                }
            }
        }
        File.WriteAllText(fileSavePath1, string.Empty);
        json = JsonConvert.SerializeObject(applicationlist.ToArray());
        File.WriteAllText(fileSavePath1, json);
        return "true";
    }
    

    [WebMethod]
    public static string update_ApplicationDocuments(string admission_number, string admision_nric_front_screen, string admision_nric_back_screen, string admision_policy_file1, string admision_policy_file2, string admision_policy_file3, string admision_policy_file4, string admision_policy_file5, string admision_policy_file6, string admision_policy_file7, string admision_policy_file8, string admision_policy_file9, string admision_policy_file10, string admision_status, string status, string saveType, string submissionType, string appSave, string admision_remarks_agent, string admision_remarks_admin)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        List<application> objlistapplication1 = new List<application>();
        List<application> applicationlist = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication1 = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication1 = (from m in objlistapplication1 select m).ToList();
            if (objlistapplication1.Count != 0)
            {
                objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
                foreach (application loopdata in objlistapplication)
                {
                    application _data1 = new application();
                    if (loopdata.admission_number == admission_number)
                    {
                        loopdata.admision_nric_front_screen = admision_nric_front_screen;
                        loopdata.admision_nric_back_screen = admision_nric_back_screen;
                        loopdata.admision_policy_file1 = admision_policy_file1;
                        loopdata.admision_policy_file2 = admision_policy_file2;
                        loopdata.admision_policy_file3 = admision_policy_file3;
                        loopdata.admision_policy_file4 = admision_policy_file4;
                        loopdata.admision_policy_file5 = admision_policy_file5;
                        loopdata.admision_policy_file6 = admision_policy_file6;
                        loopdata.admision_policy_file7 = admision_policy_file7;
                        loopdata.admision_policy_file8 = admision_policy_file8;
                        loopdata.admision_policy_file9 = admision_policy_file9;
                        loopdata.admision_policy_file10 = admision_policy_file10;
                        loopdata.admision_remarks_admin = admision_remarks_admin;
                        loopdata.admision_remarks_agent = admision_remarks_agent;
                        loopdata.admision_status = admision_status;
                        loopdata.status = status;
                        loopdata.saveType = saveType;
                        loopdata.submissionType = submissionType;
                        loopdata.appSave = appSave;

                        _data1 = loopdata;
                    }
                    else
                    {
                        _data1 = loopdata;
                    }
                    applicationlist.Add(_data1);
                }
            }
        }
        File.WriteAllText(fileSavePath1, string.Empty);
        json = JsonConvert.SerializeObject(applicationlist.ToArray());
        File.WriteAllText(fileSavePath1, json);
        return "true";
    }


    [WebMethod]
    public static string saveApplication(string admision_slno, string admission_number, string admision_name, string admision_gender, string admision_nric, string admision_dob, string admision_application_date, string admision_time, string admision_occupation, string admision_contact, string admision_nok_name, string admision_nok_contact, string admision_pa_plan, string admision_exclusion_plan, string admision_pre_existing_condition, string admision_hign_bp, string admision_diabetes, string admision_high_cholesterol, string admision_pending_claims, string admision_cash_pay_mc, string admision_cause_complaint, string admision_sign_symptoms, string admision_preferred_date_admission, string admision_insurance_agent_id, string admision_insurance_agent_name, string admision_insurance_agent_contact, string admision_remarks_admin, string admision_remarks_agent, string admision_status, string status, string saveType, string submissionType, string appSave)
    {
        string ret = "false";
        string json = string.Empty;
        application _data = new application();
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            string editdata = data.Replace("]", ",");
            _data = new application();
            _data.admision_slno = admision_slno;
            _data.admission_number = admission_number;
            _data.admision_name = admision_name;
            _data.admision_gender = admision_gender;
            _data.admision_nric = admision_nric;
            _data.admision_dob = admision_dob;
            _data.admision_application_date = admision_application_date;
            _data.admision_time = admision_time;
            _data.admision_occupation = admision_occupation;
            _data.admision_contact = admision_contact;
            _data.admision_nok_name = admision_nok_name;
            _data.admision_nok_contact = admision_nok_contact;
            _data.admision_pa_plan = admision_pa_plan;
            _data.admision_exclusion_plan = admision_exclusion_plan;
            _data.admision_pre_existing_condition = admision_pre_existing_condition;
            _data.admision_hign_bp = admision_hign_bp;
            _data.admision_diabetes = admision_diabetes;
            _data.admision_high_cholesterol = admision_high_cholesterol;
            _data.admision_pending_claims = admision_pending_claims;

            _data.admision_nric_front_screen = "Nil";
            _data.admision_nric_back_screen = "Nil";

            _data.admision_policy_file1 = "Nil";
            _data.admision_policy_file2 = "Nil";
            _data.admision_policy_file3 = "Nil";
            _data.admision_policy_file4 = "Nil";
            _data.admision_policy_file5 = "Nil";
            _data.admision_policy_file6 = "Nil";
            _data.admision_policy_file7 = "Nil";
            _data.admision_policy_file8 = "Nil";
            _data.admision_policy_file9 = "Nil";
            _data.admision_policy_file10 = "Nil";

            _data.admision_cash_pay_mc = admision_cash_pay_mc;
            _data.admision_cause_complaint = admision_cause_complaint;
            _data.admision_sign_symptoms = admision_sign_symptoms;
            _data.admision_preferred_date_admission = admision_preferred_date_admission;
            _data.admision_insurance_agent_id = admision_insurance_agent_id;
            _data.admision_insurance_agent_name = admision_insurance_agent_name;
            _data.admision_insurance_agent_contact = admision_insurance_agent_contact;
            _data.admision_remarks_admin = admision_remarks_admin;
            _data.admision_remarks_agent = admision_remarks_agent;
            _data.admision_status = admision_status;   
            _data.status = status;
            _data.saveType = saveType;
            _data.submissionType = submissionType;
            _data.appSave = appSave;
            _data.admision_remarks_client = "null";

            json = JsonConvert.SerializeObject(_data);
            json = editdata + json + "]";
        }
        else
        {
            _data = new application();
            _data.admision_slno = admision_slno;
            _data.admission_number = admission_number;
            _data.admision_name = admision_name;
            _data.admision_gender = admision_gender;
            _data.admision_nric = admision_nric;
            _data.admision_dob = admision_dob;
            _data.admision_application_date = admision_application_date;
            _data.admision_time = admision_time;
            _data.admision_occupation = admision_occupation;
            _data.admision_contact = admision_contact;
            _data.admision_nok_name = admision_nok_name;
            _data.admision_nok_contact = admision_nok_contact;
            _data.admision_pa_plan = admision_pa_plan;
            _data.admision_exclusion_plan = admision_exclusion_plan;
            _data.admision_pre_existing_condition = admision_pre_existing_condition;
            _data.admision_hign_bp = admision_hign_bp;
            _data.admision_diabetes = admision_diabetes;
            _data.admision_high_cholesterol = admision_high_cholesterol;
            _data.admision_pending_claims = admision_pending_claims;

            _data.admision_nric_front_screen = "Nil";
            _data.admision_nric_back_screen = "Nil";

            _data.admision_policy_file1 = "Nil";
            _data.admision_policy_file2 = "Nil";
            _data.admision_policy_file3 = "Nil";
            _data.admision_policy_file4 = "Nil";
            _data.admision_policy_file5 = "Nil";
            _data.admision_policy_file6 = "Nil";
            _data.admision_policy_file7 = "Nil";
            _data.admision_policy_file8 = "Nil";
            _data.admision_policy_file9 = "Nil";
            _data.admision_policy_file10 = "Nil";

            _data.admision_cash_pay_mc = admision_cash_pay_mc;
            _data.admision_cause_complaint = admision_cause_complaint;
            _data.admision_sign_symptoms = admision_sign_symptoms;
            _data.admision_preferred_date_admission = admision_preferred_date_admission;
            _data.admision_insurance_agent_id = admision_insurance_agent_id;
            _data.admision_insurance_agent_name = admision_insurance_agent_name;
            _data.admision_insurance_agent_contact = admision_insurance_agent_contact;
            _data.admision_remarks_admin = admision_remarks_admin;
            _data.admision_remarks_agent = admision_remarks_agent;
            _data.admision_status = admision_status;
            _data.status = status;
            _data.saveType = saveType;
            _data.submissionType = submissionType;
            _data.appSave = appSave;
            _data.admision_remarks_client = "null";

            objlistapplication = new List<application>();
            objlistapplication.Add(_data);
            json = JsonConvert.SerializeObject(objlistapplication.ToArray());
        }
        File.WriteAllText(fileSavePath1, json);
        ret = "true";
        return ret;
    }


    //send mail work 
    [WebMethod]
    public static List<application> sendEmailPdf(string admission_number)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admission_number == admission_number select m).ToList();
            if (objlistapplication.Count > 0)
            {
                var admission_number1 = objlistapplication[0].admission_number;
                var admision_name = objlistapplication[0].admision_name;
                var admision_gender = objlistapplication[0].admision_gender;
                var admision_nric = objlistapplication[0].admision_nric;
                var admision_time = objlistapplication[0].admision_time;
                var admision_dob = objlistapplication[0].admision_dob;
                var admision_application_date = objlistapplication[0].admision_application_date;
                var admision_occupation = objlistapplication[0].admision_occupation;
                var admision_contact = objlistapplication[0].admision_contact;
                var admision_nok_name = objlistapplication[0].admision_nok_name;
                var admision_nok_contact = objlistapplication[0].admision_nok_contact;
                var admision_pa_plan = objlistapplication[0].admision_pa_plan;
                var admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                var admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                var admision_hign_bp = objlistapplication[0].admision_hign_bp;
                var admision_diabetes = objlistapplication[0].admision_diabetes;
                var admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                var admision_pending_claims = objlistapplication[0].admision_pending_claims;
                var admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                var admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                var admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                var admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                var admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                var admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                var admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                var admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                var admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                var admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                var admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                var admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                var admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                var admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                var admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                var admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                var admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                var admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                // }
                // }


                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<br><div><h4 style='text-align:center;'>APPLICATION FORM</h4><br>");
                        sb.Append("<table width='90%' style='margin: 0 auto; border:1px solid;text-align:center'>");
                        sb.Append("<tr style='background-color: #d4d4d4;color: black;'>");
                        sb.Append("<td style='padding: 12px;font-size:9px'>APPLICATION NUMBER : <span>" + admission_number + " </span></td>");
                        sb.Append("<td style='padding: 12px;font-size:9px'>APPLICATION DATE : <span>" + admision_application_date + " </span></td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<br><div  width='90%' style='margin: 0 auto; border:1px solid;padding-left:25px;'>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>NAME : <span>" + admision_name + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>GENDER : <span>" + admision_gender + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>NRIC: <span>" + admision_nric + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>DATE OF BIRTH: <span>" + admision_dob + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>OCCUPATION: <span>" + admision_occupation + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>CONTACT: <span>" + admision_contact + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>NOK NAME & RELATIONSHIP: <span>" + admision_nok_name + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>NOK CONTACT: <span>" + admision_nok_contact + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>TYPE OF SHIELD PLAN (with or without rider) / PA PLAN & INFORCE DATE: <span>" + admision_pa_plan + "</span> </h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>EXCLUSION PLANS : <span>" + admision_exclusion_plan + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>PRE-EXISTING CONDITIONS [medical condition & medicine / past operation done not mentioned in policy] : <span>" + admision_pre_existing_condition + "</span></h6>");

                        sb.Append("<br><table width='90%' style='margin: 0 auto; border:1px solid;text-align:center'>");
                        sb.Append("<tr style='background-color: #d4d4d4;color: black;font-weight: bold;'>");
                        sb.Append("<th style='padding: 5px;font-size: 9px;'>HIGH BLOOD PRESSURE</th>");
                        sb.Append("<th style='padding: 5px;font-size: 9px;'>DIABETES</th>");
                        sb.Append("<th style='padding: 5px;font-size: 9px;'>HIGH CHOLESTEROL</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='font-size: 9px;'><span>" + admision_hign_bp + "</span></td>");
                        sb.Append("<td style='font-size: 9px;'><span>" + admision_diabetes + "</span></td>");
                        sb.Append("<td style='font-size: 9px;'><span>" + admision_high_cholesterol + "</span></td>");
                        sb.Append("</tr>");
                        sb.Append("</table><br>");

                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>PENDING CLAIM PLAN FOR THE CURRENT SHIELD PLAN : <span>" + admision_pending_claims + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>ANY OTHER CASH PAYING ENTITLEMENT POLICIES [MC or daily hospitalization cash benefits] : <span>" + admision_cash_pay_mc + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>CAUSE OF COMPLIANT : <span>" + admision_cause_complaint + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>SIGNS AND SYMPTOMS : <span>" + admision_sign_symptoms + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>DATE OF ADMISSION : <span>" + admision_preferred_date_admission + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>INSURANCE AGENT NAME : <span>" + admision_insurance_agent_name + "</span></h6>");
                        sb.Append("<br><h6 style='font-size: 9px;color: #777474;line-height: 5px;font-weight: bold;'>INSURANCE AGENT CONTACT : <span>" + admision_insurance_agent_contact + "</span></h6>");

                        sb.Append("</div>");
                        sb.Append("</div>");

                        StringReader sr = new StringReader(sb.ToString());

                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                            pdfDoc.Open();
                            htmlparser.Parse(sr);
                            pdfDoc.Close();
                            byte[] bytes = memoryStream.ToArray();
                            memoryStream.Close();
                            
                            string main1 = "sretherkrish@gmail.com";
                           // string main1 = "chandrubinaryarrows@gmail.com";
                            var un = "website@1migrate.net";
                            var pass = "Welcome123!@#";
                            MailMessage msg = new MailMessage();
                            msg.From = new MailAddress(un, "ZDO");
                            msg.To.Add(main1);
                            msg.Subject = "Mail From Site :Application form";


                            string msgs = "";
                            string furl = "http://test.zerodotone.net/";
                            //string fdata = "";
                            /*if (admision_nric_front_screen != "Nil") {
                                string fdata1 = furl + admision_nric_front_screen;
                                msgs = "<a href='" + fdata1 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_nric_back_screen != "Nil")
                            {
                                string fdata2 = furl + admision_nric_back_screen;
                                msgs = "<a href='" + fdata2 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file1 != "Nil")
                            {
                                string fdata3 = furl + admision_policy_file1;
                                msgs = "<a href='" + fdata3 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file2 != "Nil")
                            {
                                string fdata4 = furl + admision_policy_file2;
                                msgs = "<a href='" + fdata4 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file3 != "Nil")
                            {
                                string fdata5 = furl + admision_policy_file3;
                                msgs = "<a href='" + fdata5 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file4 != "Nil")
                            {
                                string fdata6 = furl + admision_policy_file4;
                                msgs = "<a href='" + fdata6 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file5 != "Nil")
                            {
                                string fdata7 = furl + admision_policy_file5;
                                msgs = "<a href='" + fdata7 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file6 != "Nil")
                            {
                                string fdata8 = furl + admision_policy_file6;
                                msgs = "<a href='" + fdata8 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file7 != "Nil")
                            {
                                string fdata9 = furl + admision_policy_file7;
                                msgs = "<a href='" + fdata9 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file8 != "Nil")
                            {
                                string fdata10 = furl + admision_policy_file8;
                                msgs = "<a href='" + fdata10 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file9 != "Nil")
                            {
                                string fdata11 = furl + admision_policy_file9;
                                msgs = "<a href='" + fdata11 + "' download><b>Download</b></a></br>";
                            }
                            else if (admision_policy_file10 != "Nil")
                            {
                                string fdata12 = furl + admision_policy_file10;
                                msgs = "<a href='" + fdata12 + "' download><b>Download</b></a></br>";
                            }*/
                            //<a href='" + fdata1 + "' download><b>Download</b></a></br>

                            string fdata1 = furl + admision_nric_front_screen;
                            string fdata2 = furl + admision_nric_back_screen;
                            string fdata3 = furl + admision_policy_file1;

                            msg.Body = "Test body message<br><br><table>"
                                        + "<tr><td><a href='" + fdata1 + "' download><b>Download NRIC Front</b></a></br></br></td></tr>"
                            +"<tr><td><a href='" + fdata2 + "' download><b>Download NRIC Back</b></a></br></br></td></tr>"
                            + "<tr><td><a href='" + fdata3 + "' download><b>Download IC Document</b></a></br></br></td></tr></table>";

                            msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "Application.pdf"));

                            msg.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "relay-hosting.secureserver.net";
                            smtp.Credentials = new NetworkCredential(un, pass);
                            smtp.EnableSsl = false;
                            smtp.Send(msg);

                        }
                    }
                }
            }
        }
        return objlistapplication;
    }


    [WebMethod]
    public static string sendURLmail(string url, string agentSessionName, string agentPh, string usersendEmail )
    {
        //string main1 = "hemachandru21@gmail.com";
string main1 = usersendEmail ;
        var un = "website@1migrate.net";
        var pass = "Welcome123!@#";
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(un, "ZDO");
        msg.To.Add(main1);
        msg.Subject = "Mail From Site : Application form URL";
        msg.Body = "<br><br><div style='background-color: white;padding: 25px;border: 2px solid navy;margin: 25px;color:black;'><center><h1>Application URL</h1><br><br><table border='1' style='border-collapse: collapse;'> " +
                   "<table border='1' style='border-collapse: collapse;'><tr><td style='padding-top:10px;padding-bottem:10px;'>Agent Name : </td><td style='padding-top:10px;padding-bottem:10px;'>" + agentSessionName + " </td></tr>" +
                   "<tr><td style='padding-top:10px;padding-bottem:10px;'>Agent Phone  : </td><td style='padding-top:10px;padding-bottem:10px;'>" + agentPh + "  </td></tr>" +
                   "<tr><td style='padding-top:10px;padding-bottem:10px;'>URL : </td><td style='padding-top:10px;padding-bottem:10px;'>" + url + "</td></tr>" +
                   "<table></center></div><br><br><br>";
        msg.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "relay-hosting.secureserver.net";
        smtp.Credentials = new NetworkCredential(un, pass);
        smtp.EnableSsl = false;
        smtp.Send(msg);
        return "true";
    }

    //send mail work




    [WebMethod]
    public static List<application> getApplicationData()
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }

    [WebMethod]
    public static List<application> getApplicationStatus(string dagent_id)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_insurance_agent_id == dagent_id select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    [WebMethod]
    public static List<application> getUnknownDocument(string agent_id)
    {
        //Nil
        var admision_nric_front_screen = "Nil";
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_nric_front_screen == admision_nric_front_screen && m.admision_insurance_agent_id == agent_id select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    [WebMethod]
    public static string user_login(string du_name, string du_password)
    {
        string ret = "false";
        string json = string.Empty;
        List<agent> objlistagent = new List<agent>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/agent.txt"));
        string data = File.ReadAllText(fileSavePath1);
        agent agentcls = new agent();
        if (data != "" && data != string.Empty && data != " ")
        {
            //objlistagent = new List<agent>();
            objlistagent = JsonConvert.DeserializeObject<List<agent>>(data);
            objlistagent = (from m in objlistagent where m.agent_username == du_name && m.agent_password == du_password select m).ToList();
            if (objlistagent.Count > 0)
            {
                var res_agent_id = agentcls.agent_id = objlistagent[0].agent_id;
                var res_agent_status = agentcls.status = objlistagent[0].status;
                ret = res_agent_id + "%" + res_agent_status;
            }
        }
        return ret;
    }


    //Agent Dashboad status start
    //Agent Customer list start
    [WebMethod]
    public static List<application> getSalesPersonDraft(string appSave, string dagent_id)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_insurance_agent_id == dagent_id && m.appSave == appSave select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }
    //Agent Customer list end

    [WebMethod]
    public static List<application> getSalesPersonStatus(string dstatusApp, string dagent_id)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_status == dstatusApp && m.admision_insurance_agent_id == dagent_id select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }
    //Agent Dashboad status end


    [WebMethod]
    public static List<application> getReceivedStatus(string dstatus, string dagent_id)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.status == dstatus && m.admision_insurance_agent_id == dagent_id select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    //Agent Customer list start
    [WebMethod]
    public static List<application> getSalesPersonClientList(string dagent_id)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_insurance_agent_id == dagent_id select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }
    //Agent Customer list end


    //Amend single start
    [WebMethod]
    public static List<application> getAmendSingle(string dadmission_number)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admission_number == dadmission_number select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }
    //Amend single end      admision_remarks_admin admision_remarks_agent



    [WebMethod]
    public static List<application> getFreshAppComplete(string dstatus)
    {
        var appSave = "Complete";
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_status == dstatus && m.appSave == appSave select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    [WebMethod]
    public static List<application> getFreshApp(string dstatus)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_status == dstatus select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    [WebMethod]
    public static List<application> getnewApplication(string dstatus)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.status == dstatus select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    [WebMethod]
    public static List<application> getStatusApplication(string admision_status)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.admision_status == admision_status select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }

    [WebMethod]
    public static List<application> reSubmitApplication(string submissionType)
    {
        string json = string.Empty;
        List<application> objlistapplication = new List<application>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/application.txt"));
        string data = File.ReadAllText(fileSavePath1);
        application applicationcls = new application();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<application>();
            objlistapplication = JsonConvert.DeserializeObject<List<application>>(data);
            objlistapplication = (from m in objlistapplication where m.submissionType == submissionType select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admision_slno = objlistapplication[0].admision_slno;
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_time = objlistapplication[0].admision_time;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_application_date = objlistapplication[0].admision_application_date;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_nric_front_screen = objlistapplication[0].admision_nric_front_screen;
                applicationcls.admision_nric_back_screen = objlistapplication[0].admision_nric_back_screen;
                applicationcls.admision_policy_file1 = objlistapplication[0].admision_policy_file1;
                applicationcls.admision_policy_file2 = objlistapplication[0].admision_policy_file2;
                applicationcls.admision_policy_file3 = objlistapplication[0].admision_policy_file3;
                applicationcls.admision_policy_file4 = objlistapplication[0].admision_policy_file4;
                applicationcls.admision_policy_file5 = objlistapplication[0].admision_policy_file5;
                applicationcls.admision_policy_file6 = objlistapplication[0].admision_policy_file6;
                applicationcls.admision_policy_file7 = objlistapplication[0].admision_policy_file7;
                applicationcls.admision_policy_file8 = objlistapplication[0].admision_policy_file8;
                applicationcls.admision_policy_file9 = objlistapplication[0].admision_policy_file9;
                applicationcls.admision_policy_file10 = objlistapplication[0].admision_policy_file10;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.admision_insurance_agent_id = objlistapplication[0].admision_insurance_agent_id;
                applicationcls.admision_insurance_agent_name = objlistapplication[0].admision_insurance_agent_name;
                applicationcls.admision_insurance_agent_contact = objlistapplication[0].admision_insurance_agent_contact;
                applicationcls.admision_remarks_admin = objlistapplication[0].admision_remarks_admin;
                applicationcls.admision_remarks_agent = objlistapplication[0].admision_remarks_agent;
                applicationcls.admision_status = objlistapplication[0].admision_status;
                applicationcls.status = objlistapplication[0].status;
                applicationcls.saveType = objlistapplication[0].saveType;
                applicationcls.submissionType = objlistapplication[0].submissionType;
                applicationcls.appSave = objlistapplication[0].appSave;
            }
        }
        return objlistapplication;
    }


    //-----------------------------------------------------------------------------------//

    public class applicationStatus
    {
        public string admission_number { get; set; }
        public string admision_name { get; set; }
        public string admision_gender { get; set; }
        public string admision_nric { get; set; }
        public string admision_dob { get; set; }
        public string admision_occupation { get; set; }
        public string admision_contact { get; set; }
        public string admision_nok_name { get; set; }
        public string admision_nok_contact { get; set; }
        public string admision_pa_plan { get; set; }
        public string admision_exclusion_plan { get; set; }
        public string admision_pre_existing_condition { get; set; }
        public string admision_hign_bp { get; set; }
        public string admision_diabetes { get; set; }
        public string admision_high_cholesterol { get; set; }
        public string admision_pending_claims { get; set; }
        public string admision_cash_pay_mc { get; set; }
        public string admision_cause_complaint { get; set; }
        public string admision_sign_symptoms { get; set; }
        public string admision_preferred_date_admission { get; set; }
        public string status { get; set; }
    }

    [WebMethod]
    public static string saveEditStatusApplication(string admission_number, string admision_name, string admision_gender, string admision_nric, string admision_dob, string admision_occupation, string admision_contact, string admision_nok_name, string admision_nok_contact, string admision_pa_plan, string admision_exclusion_plan, string admision_pre_existing_condition, string admision_hign_bp, string admision_diabetes, string admision_high_cholesterol, string admision_pending_claims, string admision_cash_pay_mc, string admision_cause_complaint, string admision_sign_symptoms, string admision_preferred_date_admission, string status)
    {
        string ret = "false";
        string json = string.Empty;
        applicationStatus _data = new applicationStatus();
        List<applicationStatus> objlistapplication = new List<applicationStatus>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/appStatus.txt"));
        string data = File.ReadAllText(fileSavePath1);
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<applicationStatus>();
            objlistapplication = JsonConvert.DeserializeObject<List<applicationStatus>>(data);
            string editdata = data.Replace("]", ",");
            _data = new applicationStatus();
            _data.admission_number = admission_number;
            _data.admision_name = admision_name;
            _data.admision_gender = admision_gender;
            _data.admision_nric = admision_nric;
            _data.admision_dob = admision_dob;
            _data.admision_occupation = admision_occupation;
            _data.admision_contact = admision_contact;
            _data.admision_nok_name = admision_nok_name;
            _data.admision_nok_contact = admision_nok_contact;
            _data.admision_pa_plan = admision_pa_plan;
            _data.admision_exclusion_plan = admision_exclusion_plan;
            _data.admision_pre_existing_condition = admision_pre_existing_condition;
            _data.admision_hign_bp = admision_hign_bp;
            _data.admision_diabetes = admision_diabetes;
            _data.admision_high_cholesterol = admision_high_cholesterol;
            _data.admision_pending_claims = admision_pending_claims;
            _data.admision_cash_pay_mc = admision_cash_pay_mc;
            _data.admision_cause_complaint = admision_cause_complaint;
            _data.admision_sign_symptoms = admision_sign_symptoms;
            _data.admision_preferred_date_admission = admision_preferred_date_admission;
            _data.status = status;
            json = JsonConvert.SerializeObject(_data);
            json = editdata + json + "]";
        }
        else
        {
            _data = new applicationStatus();
            _data.admission_number = admission_number;
            _data.admision_name = admision_name;
            _data.admision_gender = admision_gender;
            _data.admision_nric = admision_nric;
            _data.admision_dob = admision_dob;
            _data.admision_occupation = admision_occupation;
            _data.admision_contact = admision_contact;
            _data.admision_nok_name = admision_nok_name;
            _data.admision_nok_contact = admision_nok_contact;
            _data.admision_pa_plan = admision_pa_plan;
            _data.admision_exclusion_plan = admision_exclusion_plan;
            _data.admision_pre_existing_condition = admision_pre_existing_condition;
            _data.admision_hign_bp = admision_hign_bp;
            _data.admision_diabetes = admision_diabetes;
            _data.admision_high_cholesterol = admision_high_cholesterol;
            _data.admision_pending_claims = admision_pending_claims;
            _data.admision_cash_pay_mc = admision_cash_pay_mc;
            _data.admision_cause_complaint = admision_cause_complaint;
            _data.admision_sign_symptoms = admision_sign_symptoms;
            _data.admision_preferred_date_admission = admision_preferred_date_admission;
            _data.status = status;
            objlistapplication = new List<applicationStatus>();
            objlistapplication.Add(_data);
            json = JsonConvert.SerializeObject(objlistapplication.ToArray());
        }
        File.WriteAllText(fileSavePath1, json);
        ret = "true";
        return ret;
    }


    [WebMethod]
    public static List<applicationStatus> getEditState(string dadmission_number)
    {
        string json = string.Empty;
        List<applicationStatus> objlistapplication = new List<applicationStatus>();
        var fileSavePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/jsonfile/appStatus.txt"));
        string data = File.ReadAllText(fileSavePath1);
        applicationStatus applicationcls = new applicationStatus();
        if (data != "" && data != string.Empty && data != " ")
        {
            objlistapplication = new List<applicationStatus>();
            objlistapplication = JsonConvert.DeserializeObject<List<applicationStatus>>(data);
            objlistapplication = (from m in objlistapplication where m.admission_number == dadmission_number select m).ToList();
            if (objlistapplication.Count > 0)
            {
                applicationcls.admission_number = objlistapplication[0].admission_number;
                applicationcls.admision_name = objlistapplication[0].admision_name;
                applicationcls.admision_gender = objlistapplication[0].admision_gender;
                applicationcls.admision_nric = objlistapplication[0].admision_nric;
                applicationcls.admision_dob = objlistapplication[0].admision_dob;
                applicationcls.admision_occupation = objlistapplication[0].admision_occupation;
                applicationcls.admision_contact = objlistapplication[0].admision_contact;
                applicationcls.admision_nok_name = objlistapplication[0].admision_nok_name;
                applicationcls.admision_nok_contact = objlistapplication[0].admision_nok_contact;
                applicationcls.admision_pa_plan = objlistapplication[0].admision_pa_plan;
                applicationcls.admision_exclusion_plan = objlistapplication[0].admision_exclusion_plan;
                applicationcls.admision_pre_existing_condition = objlistapplication[0].admision_pre_existing_condition;
                applicationcls.admision_hign_bp = objlistapplication[0].admision_hign_bp;
                applicationcls.admision_diabetes = objlistapplication[0].admision_diabetes;
                applicationcls.admision_high_cholesterol = objlistapplication[0].admision_high_cholesterol;
                applicationcls.admision_pending_claims = objlistapplication[0].admision_pending_claims;
                applicationcls.admision_cash_pay_mc = objlistapplication[0].admision_cash_pay_mc;
                applicationcls.admision_cause_complaint = objlistapplication[0].admision_cause_complaint;
                applicationcls.admision_sign_symptoms = objlistapplication[0].admision_sign_symptoms;
                applicationcls.admision_preferred_date_admission = objlistapplication[0].admision_preferred_date_admission;
                applicationcls.status = objlistapplication[0].status;
            }
        }
        return objlistapplication;
    }


}