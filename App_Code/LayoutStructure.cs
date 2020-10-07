using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for LayoutStructure
/// </summary>
public class LayoutStructure
{
    public int height;
    public int width;
    public int xPos;
    public int yPos;
    public string Desc;
    public string name;
    public string type;

}
public class TickerStructure
{
    public string TickDesc;
    public string TickFunc;
    public string divId;
    public string labID;
}
public class Reply
{
    public DataSet DS { get; set; }

    public bool res { get; set; }

    public int DeviceCount { get; set; }
    public string strError { get; set; }
}

public class BroadcastTemplate
{
    public string[] KioksId;
    public string TemplateName { get; set; }
    public string StartDateTime { get; set; }
    public string EndDateTime { get; set; }
    public bool Instant { get; set; }

}


public class CommandIniUpdate
{
    public string[] KioskIP;

    public string[] MachineSrNo;

    public string CommandCount;
    public string Command { get; set; }
    public bool Instant { get; set; }

}

public class PatchUpdateINI
{
    public string[] KioskIP;

    public string[] MachineSrNo;

    public string patch;
    public string PatchName { get; set; }
    public string StartDateTime { get; set; }
    public string EndDateTime { get; set; }
    public bool Instant { get; set; }

}