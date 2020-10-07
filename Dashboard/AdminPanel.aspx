<%@ Page Title="Admin Panel | PCMC" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="AdminPanel.aspx.cs" Inherits="Dashboard_AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Admin Panel
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Admin Panel</li>
        </ol>
    </section>
    <br />
    <br />
    <br />
    <br />
    <div class="row" id="div1" style="justify-content: center; display: flex;">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="PatchUpdation.aspx" style="color: black;">
                <div class="info-box">
                    <span class="info-box-icon bg-aqua"><i class="ion ion-ios-gear-outline"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">PATCH UPDATION</span>
                        <span class="info-box-number">Client/Monitor/RD Service</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="LogImage.aspx" style="color: black;">
                <div class="info-box">
                    <span class="info-box-icon bg-red"><i class="ion ion-android-calendar"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">DATA PULLING</span>
                        <span class="info-box-number">Logs/EJ/Images</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
        <!-- /.col -->

        <!-- fix for small devices only -->



        <!-- /.col -->
    </div>
    <div class="row" id="div2" style="justify-content: center; display: flex;">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="ProcessService.aspx" style="color: black">
                <div class="info-box">
                    <span class="info-box-icon bg-green"><i class="ion ion-alert"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">SERVICES/PROCESS</span>
                        <span class="info-box-number">Start/Stop/Kill</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="SystemCommand.aspx" style="color: black;">
                <div class="info-box">
                    <span class="info-box-icon bg-yellow" style="background-color: chocolate;"><i class="ion ion-android-laptop"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">WINDOW COMMAND</span>
                        <span class="info-box-number">Shutdown/Restart/ Hibernate/Sleep/LogOff</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
    </div>

    <div class="row" id="div3" style="justify-content: center; display: none;">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="" style="color: black;">
                <div class="info-box" style="background-color: whitesmoke; color: brown;">
                    <span class="info-box-icon bg-aqua" style="background-color: blueviolet;"><i class="ion ion-android-archive"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">PURGING</span>
                        <span class="info-box-number">Data Purging</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="" style="color: black;">
                <div class="info-box" style="background-color: whitesmoke; color: brown;">
                    <span class="info-box-icon bg-red" style="background-color: cornflowerblue"><i class="ion ion-android-warning"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">HDD ENCRYPTION</span>
                        <span class="info-box-number">Enable\Disable</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </a>
        </div>
        <!-- /.col -->

        <!-- fix for small devices only -->



        <!-- /.col -->
    </div>
    <div class="row" id="div4" style="justify-content: center; display: none;">
        <div class="col-md-3 col-sm-6 col-xs-12">
           <%-- <a href="" runat="server" onserverclick="ScreenCapture" style="color: black;">--%>
              <a href="" style="color: black;">
                <div class="info-box" style="background-color: whitesmoke; color: brown;" >
                    <span class="info-box-icon bg-green" style="background-color: darkorange"><i class="ion ion-android-desktop"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">CAPTURE</span>
                        <span class="info-box-number">Screen Capture</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="" style="color: black;">
                <div class="info-box" style="background-color: whitesmoke; color: brown;" >
                    <span class="info-box-icon bg-yellow" style="background-color: darkcyan"><i class="ion ion-key"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">ENABLE/DISABLE</span>
                        <span class="info-box-number">Keyboard/Mouse/USB/ Remote</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
    </div>


    <div class="row" id="div6" style="justify-content: center; display: none;">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="" style="color: black;">
                <div class="info-box" style="background-color: whitesmoke; color: brown;" >
                    <span class="info-box-icon bg-aqua" style="background-color: mediumturquoise"><i class="ion ion-android-open"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">PASSWORD CHANGE</span>
                        <span class="info-box-number">Remote Password Change</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
            <a href="" style="color: black;">
                <div class="info-box" style="background-color: whitesmoke; color: brown;">
                    <span class="info-box-icon bg-red" style="background-color: purple"><i class="ion ion-connection-bars"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">CUSTOM COMMAND</span>
                        <span class="info-box-number">Advanced Setting/Remote Restart/Download/Upload</span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
            </a>
            <!-- /.info-box -->
        </div>

    </div>

</asp:Content>

