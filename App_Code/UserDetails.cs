using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDetails
/// </summary>
public struct UserDetails
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Location { get; set; }
    public string Role { get; set; }

}
public class UserLogin
{ 
public string UserName { get; set; }
public string Password { get; set; }

public string Role { get; set; }
}

public class UserDetailsCreation
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string UserCreationDate { get; set; }
    public string Pwd { get; set; }
}

public class BranchDetails
{
    public string CircleCode { get; set; }
    public string CircleName { get; set; }
    public string BranchCode { get; set; }
    public string BranchName { get; set; }
    public string Region { get; set; }
    public string ModuleCode { get; set; }
    public string ModuleName { get; set; }

    public string Network { get; set; }

    public string PersonName { get; set; }
    public string PersonPhone { get; set; }
    public string PersonEmail { get; set; }
    public bool res { get; set; }
}


public class PasscodeRecovery
{
    public string Username { get; set; }
    public string SecurityQuestion { get; set; }
    public string Answer { get; set; }
    public string Email { get; set; }
}
public class PasscodeChange
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string OldPass { get; set; }
}

public class ResetPassword
{
    public string name { get; set; }
    public string password { get; set; }


}



public class EncRequest
{
    public string RequestData { get; set; }
}

public class EncResponse
{   public string ResponseData { get; set; }
}


public class ReqCommandExecute
{
    public string Command { get; set; }

    public string[] KioskIPs { get; set; }

    public string Patchdata;
    public string DisplayData { get; set; }
}

public class ResCommandExecute
{
    public bool Result { get; set; }
    public string Error { get; set; }
}

public class ReqUpdateExcelUpdateStatus
{
 
    public string strQuery { get; set; }


}
public class ResUpdateExcelUpdateStatus
{  
    public bool Result { get; set; }
 
    public string ErrorDes { get; set; }
}


public class ReqTicketGenerate
{
    public string IssueTitle { get; set; }
    public string IssueDateTime { get; set; }
    public string KioskID { get; set; }
    public string brCode { get; set; }
    public string IssueStatus { get; set; }
    public string Sender { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPersonMobile { get; set; }
    public string Email_To { get; set; }
    public string ProblemDescription { get; set; }
    public string SerialNumber { get; set; }

}

public class ResTicket
{
    public string Ticket { get; set; }
    public bool Result { get; set; }
    public string Error { get; set; }

    public string CallTicketNumber { get; set; }
    public string Message { get; set; }
    public string SerialNumber { get; set; }

    public DateTime DateCreated { get; set; }
    public bool IsSuccess { get; set; }
}


public class TicketNumberList
{
    public bool success { get; set; }
    public string requestId { get; set; }
    public string req_type { get; set; }
    public string errorCode { get; set; }
    public string errorMessage { get; set; }
}

public class CallLogin
{
    public string requestId { get; set; }
    public string reqType { get; set; }
    public string reqDatetime { get; set; }
    public string brCode { get; set; }
    public string src { get; set; }
    public string kioskProvider { get; set; }
    public string kioskId { get; set; }
    public string kioskSrno { get; set; }
    public string issueCategory { get; set; }
    public string issueSubcategory { get; set; }
    public string issueDescription { get; set; }
    public string contactName { get; set; }
    public string contactNumber { get; set; }
    public string contactEmailid { get; set; }
    public string status { get; set; }
}

public class CallClose
{

    public string requestId { get; set; }
    public string openrequestId { get; set; }
    public string reqType { get; set; }
    public string reqDatetime { get; set; }
    public string brCode { get; set; }
    public string src { get; set; }
    public string kioskProvider { get; set; }
    public string kioskId { get; set; }
    public string engineerName { get; set; }
    public string engineerMobile { get; set; }
    public string status { get; set; }
    public string CallCategory { get; set; }
    public string IsCommercial { get; set; }
    public string Faults { get; set; }
    public string SLM_FLM { get; set; }
    public string TotalVisit { get; set; }
    public string sparePartConsumed { get; set; }
}

public class RemoteDesktop
{ 
    public string KioskIP { get; set; }
}


public class globle
{

    public static string UserValue = null;

    public string TrimString(string text, char char1)
    {
        string str = null;
        string[] strArr = null;

        str = text;
        char[] splitchar = { char1 };
        strArr = str.Split(splitchar);

        string output = strArr[0];
        return output;
    }


}
