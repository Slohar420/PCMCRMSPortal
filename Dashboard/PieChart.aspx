<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="PieChart.aspx.cs" Inherits="PieChart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style3 {
            position: absolute;
            top: 790px;
            left: 803px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="Row" style="height: 600px;">
                <div class="col-md-12 text-center " style="padding-top:100px;">
                      <asp:Chart ID="Chart1" runat="server" Style="margin-left: 80px; padding-top: 10px" Height="400px" Width="500px">
                            <Series>
                                <asp:Series Name="Series1" ChartArea="ChartArea1" YValuesPerPoint="4" Color="Highlight"></asp:Series>
                            </Series>

                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" Rotation="30" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                </div>
                <div class="col-sm-12" style="display:none">
                    <div class="col-sm-1">
                        <asp:Chart ID="Chart2" runat="server" Style="margin-left: 80px; display: none" Height="250px" Width="350px">
                            <Series>
                                <asp:Series Name="Series1"></asp:Series>
                            </Series>

                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>

                    </div>
                    <div class="col-sm-5">
                       <%-- <asp:Chart ID="Chart1" runat="server" Style="margin-left: 80px; padding-top: 10px" Height="250px" Width="350px">
                            <Series>
                                <asp:Series Name="Series1" ChartArea="ChartArea1" YValuesPerPoint="4" Color="Highlight"></asp:Series>
                            </Series>

                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" Rotation="30" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </div>

                    <div class="col-sm-5">
                        <asp:Chart ID="Chart4" runat="server" Style="margin-left: 80px;" Height="250px" Width="350px">
                            <Series>
                                <asp:Series Name="Series1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <div class="col-md-1">
                        <asp:Chart ID="Chart3" runat="server" Style="margin-left: 80px; display: none" Height="250" Width="320px" Visible="false">
                            <Series>
                                <asp:Series Name="Series1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-default" style="margin-top: 60px;display:none">
        <div class="panel-body">
            <div class="row" style="display:none">
                <div class="col-sm-12">
                    <div class="col-sm-1">
                        <asp:Chart ID="Chart5" runat="server" Style="margin-left: 80px; padding-top: 10px; display: none;" Height="250px" Width="320px">
                            <Series>
                                <asp:Series Name="Series1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="chartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="col-sm-5">
                        <div class="col-lg-4 text-right " style="padding-right:5px;display:none" >
                            <h5 style="margin: 3px 0 0 0; font-weight: 600">LOCATIONS : </h5>
                        </div>
                        <div class="col-md-4" style="padding-left:0px;display:none">
                            <asp:DropDownList runat="server" ID="ddlLocations" AutoPostBack="true" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged" CssClass="dropdown" Width="200px">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4 text-right">
                        </div>
                        <asp:UpdatePanel runat="server" >
                            <ContentTemplate>
                                <asp:Chart ID="Chart6" runat="server" Style="margin-left: 80px; padding-top: 10px" Height="250px" Width="350px">
                                    <Series>
                                        <asp:Series Name="Series1"></asp:Series>
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <Area3DStyle Enable3D="True" />
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger  ControlID="ddlLocations"/>
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>

                    <div class="col-sm-5">
                        <asp:Chart ID="Chart7" runat="server" Style="margin-left: 80px; padding-top: 10px" Width="350px" Height="250px">
                            <Series>
                                <asp:Series Name="Series1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



