Option Strict On

Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports System.Threading
Imports System.Net.WebUtility
Imports System.Net.Sockets

Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Globalization
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq
Imports System.Runtime.InteropServices
Imports CefSharp.WinForms
Imports CefSharp
Imports MetroFramework.Controls

Public Class Main
    Inherits MetroForm

    Dim t As Thread
    Dim HTML As String = Nothing
    Public CrBetaMass As String = Nothing
    Public CrBetaMassEpisodes As String = Nothing
    Public CrBetaMassParameters As String = Nothing
    Public CrBetaMassBaseURL As String = Nothing
    Public BlockList As List(Of String)

    Public LoadedUrls As New List(Of String)

    Public VRVMass As String = Nothing
    Public VRVMassEpisodes As String = Nothing
    Public VRVMassParameters As String = Nothing
    Public VRVMassBaseURL As String = Nothing

    Public FunimationAPIRegion As String = Nothing
    Public FunimationRegion As String = Nothing
    Public FunimationDeviceRegion As String = Nothing
    Public FunimationToken As String = Nothing
    Public FunimationShowPath As String = Nothing
    Public FunimationEpisodeJSON As String = Nothing
    Public FunimtaionAPISeasonID As New List(Of String)
    Public FunimtaionSeasonList As New List(Of FunimationOverview)
    Public FunimationSeasonAPIUrl As String = Nothing
    Public FunimationJsonBrowser As String = Nothing


    Public Manager As New MetroStyleManager

    Public DarkModeValue As Boolean = False
    Public invalids As Char() = System.IO.Path.GetInvalidFileNameChars()
    Dim ServerThread As Thread

    Public KodiNaming As Boolean = False
    Public ErrorTolerance As Integer = 0
    Public liList As New List(Of String)
    Public HTMLString As String = My.Resources.Startuphtml
    Public ListBoxList As New List(Of String)
    Public ItemList As New List(Of CRD_List_Item)
    Public RunningDownloads As Integer = 0
    Public UseQueue As Boolean = False
    Public StartServer As Integer = 0
    Public m3u8List As New List(Of String)
    Public txtList As New List(Of String)
    Public mpdList As New List(Of String)
    Public ResoAvalibe As String = Nothing
    Public ResoSearchRunning As Boolean = False
    Public UsedMap As String = Nothing
    Public Debug1 As Boolean = False
    Public Debug2 As Boolean = False
    Public LogBrowserData As Boolean = False
    Public Thumbnail As String = Nothing
    Public MergeSubs As Boolean = False
    Public IgnoreS1 As Boolean = False
    Public KeepCache As Boolean = False
    Public SubsOnly As Boolean = False
    Public VideoFormat As String = ".mp4"
    Public MergeSubsFormat As String = "mov_text"

    Public LoginDialog As Boolean = False

    Public NonCR_Timeout As Integer = 5
    Public NonCR_URL As String = Nothing
    Public DlSoftSubsRDY As Boolean = True
    Public DialogTaskString As String
    Public ErrorBrowserString As String
    Public ErrorBrowserUrl As String
    Public ErrorBrowserBackString As String
    Public UserCloseDialog As Boolean = False
    Dim Aktuell As String
    Dim Gesamt As String
    Public LabelUpdate As String = "Status: idle"
    Public LabelEpisode As String = "..."

    Public b As Boolean

    Public c As Boolean = True
    Public LoginOnly As String = "False"
    Public Pfad As String = My.Computer.FileSystem.CurrentDirectory
    Public ProfileFolder As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "CRD-Profile")
    Public ffmpeg_command As String = " -c copy -bsf:a aac_adtstoasc" '" -c:v hevc_nvenc -preset fast -b:v 6M -bsf:a aac_adtstoasc " 
    Public Reso As Integer
    Public AoD_Reso As Integer = 0

    Public Season_Prefix As String = "[default season prefix]"
    Public Episode_Prefix As String = "[default episode prefix]"

    Dim Reso2 As String
    Public ResoSave As String = "6666x6666"
    Public ResoFunBackup As String = "6666x6666"
    Public SubSprache As String
    Public SoftSubs As New List(Of String)
    Public IncludeLangName As Boolean = False
    Public TempSoftSubs As New List(Of String)
    Public AbourtList As New List(Of String)
    Public watingList As New List(Of String)
    Public GeckoLogFile As String = Nothing
    Dim SoftSubsString As String
    Dim CR_Unlock_Error As String
    Public Startseite As String = "https://www.crunchyroll.com/"
    Dim SubSprache2 As String
    Dim URL_DL As String
    Dim Pfad_DL As String
    Public Grapp_RDY As Boolean = True
    Public Funimation_Grapp_RDY As Boolean = True
    Public Grapp_non_cr_RDY As Boolean = True
    Public Grapp_Abord As Boolean = False
    Public CR_NameMethode As Integer = 0
    Public MaxDL As Integer
    Public ResoNotFoundString As String
    Public ResoBackString As String
    Public WebbrowserHeadText As String = Nothing
    Public WebbrowserSoftSubURL As String = Nothing
    Public WebbrowserURL As String = Nothing
    Public SystemWebBrowserCookie As String = Nothing
    Public WebbrowserText As String = Nothing
    Public WebbrowserTitle As String = Nothing
    Public WebbrowserCookie As String = Nothing
    Public UserBowser As Boolean = False
    Public HybridMode As Boolean = False
    Public HardSubFunimation As String = "Disabled"
    Public DubFunimation As String = "Disabled"
    Public Funimation_srt As Boolean = False
    Public Funimation_vtt As Boolean = False
    Public SubFunimationString As String = "en"
    Public SubFunimation As New List(Of String)
    Public DefaultSubFunimation As String = "Disabled"
    Public DefaultSubCR As String = "Disabled"
#Region "Sprachen Vairablen"
    Public URL_Invaild As String = "something is wrong here..."

    Dim DL_Path_String As String = "Please choose download directory."
    Public No_Stream As String = "Please make sure that the URL is correct or check if the Anime is available in your country."
    Dim TaskNotCompleed As String = "Please wait until the current task is completed."
    Dim Premium_Stream As String = "For Premium episodes you need a premium membership and be logged in the Downloader."
    Public LoginReminder As String = "Please make sure that you logged in."

    Dim Error_Mass_DL As String = "We run into a problem here." + vbNewLine + "You can try to download every episode individually."
    Dim User_Fault_NoName As String = "no name, fallback solution : "
    Dim Sub_language_NotFound As String = "Could not find the sub language" + vbNewLine + "please make sure the language is available: "
    Dim Resolution_NotFound As String = "Could not find any resolution."
    Dim Error_unknown As String = "We run into a unknown problem here." + vbNewLine + "Do you like to send an Bug report?"
    Public CR_Unlock_Error_String As String = "unable to get an CR-US cookie."
    Dim ErrorNoPermisson As String = "Access is denied."
    'UI Variablen
    Public GB_Resolution_Text As String = "Resolution"
    Public GB_SubLanguage_Text As String = "Hardsub language"
    Public GB_Sub_Path_Text As String = "Sub directory"
    Public UL_US_Text As String = "enable US Cookie"
    Public RBAnime_Text As String = "series name"
    Public RBStaffel_Text As String = "series name + season"
    Public NewStart_String As String = "to adopt all the settings, a restart is necessary."
    Public DL_Count_simultaneousText As String = "Simultaneous Downloads"
    Public GB_Sub_FormatText As String = "extended Sub Settings"
    Public LabelResoNotFoundText As String = "resolution not found" + vbNewLine + "Select another one below"
    Public LabelLangNotFoundText As String = "subtitle language not found" + vbNewLine + "Select another one below"
    Public ButtonResoNotFoundText As String = "Submit"
    Public CB_SuB_Nothing As String = "[ null ]"
    Dim StatusToolTip As ToolTip = New ToolTip()
    Dim StatusToolTipText As String
    Public RunGecko As String = "Startup"

#End Region

#Region "UI"

    Private Sub Main_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        Me.Invalidate()
    End Sub

    Public CloseImg As Bitmap = My.Resources.main_del
    Public MinImg As Bitmap = My.Resources.main_mini
    Public BackColorValue As Color = Color.FromArgb(243, 243, 243)
    Public ForeColorValue As Color = SystemColors.WindowText
    Public Sub DarkMode()
        ListView1.BackColor = Color.FromArgb(50, 50, 50)
        CloseImg = My.Resources.main_close_dark
        MinImg = My.Resources.main_mini_dark
        Btn_min.Image = MinImg
        Btn_Close.Image = CloseImg
        BackColorValue = Color.FromArgb(50, 50, 50)
        ForeColorValue = Color.FromArgb(243, 243, 243)
    End Sub
    Public Sub LightMode()
        BackColorValue = Color.FromArgb(243, 243, 243)
        ForeColorValue = SystemColors.WindowText
        ListView1.BackColor = SystemColors.Control
        CloseImg = My.Resources.main_close
        MinImg = My.Resources.main_mini
        Btn_min.Image = MinImg
        Btn_Close.Image = CloseImg
    End Sub

    Dim ListViewHeightOffset As Integer = 7

    Private Sub Btn_add_MouseEnter(sender As Object, e As EventArgs) Handles Btn_add.MouseEnter, Btn_add.GotFocus

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_add.Image = My.Resources.main_add_invert_dark
        Else
            Btn_add.Image = My.Resources.main_add_invert
        End If

    End Sub

    Private Sub Btn_add_MouseLeave(sender As Object, e As EventArgs) Handles Btn_add.MouseLeave, Btn_add.LostFocus
        ' Dim btn As Button = sender
        ' btn.Invalidate()
        Btn_add.Image = My.Resources.main_add
    End Sub



    Private Sub Btn_Browser_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Browser.MouseEnter, Btn_Browser.GotFocus
        'Dim btn As Button = sender
        'btn.Invalidate()

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Browser.Image = My.Resources.main_browser_invert_dark
        Else
            Btn_Browser.Image = My.Resources.main_browser_invert
        End If

    End Sub

    Private Sub Btn_Browser_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Browser.MouseLeave, Btn_Browser.LostFocus

        Btn_Browser.Image = My.Resources.main_browser
    End Sub
    Private Sub Btn_Settings_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Settings.MouseEnter, Btn_Settings.GotFocus

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Settings.Image = My.Resources.main_setting_invert_dark
        Else
            Btn_Settings.Image = My.Resources.main_setting_invert
        End If

    End Sub

    Private Sub Btn_Settings_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Settings.MouseLeave, Btn_Settings.LostFocus

        Btn_Settings.Image = My.Resources.main_settings
    End Sub
    Private Sub Btn_min_MouseEnter(sender As Object, e As EventArgs) Handles Btn_min.MouseEnter, Btn_min.GotFocus

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_min.Image = My.Resources.main_mini_dark_hover
        Else
            Btn_min.Image = My.Resources.main_mini_red
        End If
    End Sub

    Private Sub Btn_min_MouseLeave(sender As Object, e As EventArgs) Handles Btn_min.MouseLeave, Btn_min.LostFocus

        Btn_min.Image = MinImg
    End Sub

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter, Btn_Close.GotFocus

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Close.Image = My.Resources.main_close_dark_hover
        Else
            Btn_Close.Image = My.Resources.main_close_hover
        End If


    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave, Btn_Close.LostFocus
        Btn_Close.Image = CloseImg
    End Sub


    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If TheTextBox.Visible = True Then
            TheTextBox.Visible = False
            ListViewHeightOffset = 7
            PictureBox6.Location = New Point(0, Me.Height - ListViewHeightOffset)

            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        Else
            ListViewHeightOffset = 103
            TheTextBox.Visible = True
            PictureBox6.Location = New Point(0, Me.Height - ListViewHeightOffset)

            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        End If

    End Sub

    Private Sub PictureBox6_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox6.MouseEnter
        PictureBox6.BackgroundImage = My.Resources.balken_console
    End Sub

    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.BackgroundImage = My.Resources.balken
    End Sub

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ListView1.Width = Me.Width - 2
        ListView1.Height = Me.Height - 71 - ListViewHeightOffset

        PictureBox5.Width = Me.Width - 40


        PictureBox6.Location = New Point(1, Me.Height - ListViewHeightOffset)
        PictureBox6.Width = Me.Width - 40

        TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
        TheTextBox.Width = Me.Width - 2

        Btn_Close.Location = New Point(Me.Width - 41, 1)
        Btn_min.Location = New Point(Me.Width - 82, 1)
        Btn_Settings.Location = New Point(Me.Width - 190, 17)

        Try
            For s As Integer = 0 To ListView1.Items.Count - 1
                Dim r As Rectangle = ListView1.Items.Item(s).Bounds
                ItemList(s).SetBounds(r.X, r.Y, ListView1.Width - 2, r.Height)

                If ItemList(s).GetToDispose() = True Then
                    ItemList(s).DisposeItem(ItemList(s).GetToDispose())
                    ItemList.RemoveAt(s)
                    ListView1.Items.RemoveAt(s)
                End If

            Next
        Catch ex As Exception

        End Try

    End Sub

#End Region

    Public Declare Function waveOutSetVolume Lib "winmm.dll" (ByVal uDeviceID As Integer, ByVal dwVolume As Integer) As Integer


    <FlagsAttribute()>
    Public Enum EXECUTION_STATE As UInteger
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000UI
    End Enum

    <DllImport("Kernel32.DLL", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SetThreadExecutionState(ByVal state As EXECUTION_STATE) As EXECUTION_STATE
    End Function


    Public Sub SetSettingsTheme()

        Einstellungen.Theme = Manager.Theme
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.ContextMenuStrip = ContextMenuStrip1

        Dim tbtl As TextBoxTraceListener = New TextBoxTraceListener(TheTextBox)

        Trace.Listeners.Add(tbtl)
        b = True
        Thread.CurrentThread.Name = "Main"
        Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            ProfileFolder = rkg.GetValue("ProfilFolder").ToString


        Catch ex As Exception

        End Try


        Dim settings As CefSettings = New CefSettings()


        If Not Directory.Exists(Path.GetDirectoryName(ProfileFolder)) Then
            ' Nein! Jetzt erstellen...
            Try
                Directory.CreateDirectory(Path.GetDirectoryName(ProfileFolder))
                settings.CachePath = ProfileFolder

            Catch ex As Exception
                ' Ordner wurde nich erstellt
                settings.CachePath = Application.StartupPath + "\lib"
            End Try
        Else
            settings.CachePath = ProfileFolder
        End If

        '--disable-features=PreloadMediaEngagementData, MediaEngagementBypassAutoplayPolicies
        settings.CefCommandLineArgs.Add("disable-features=PreloadMediaEngagementData, MediaEngagementBypassAutoplayPolicies")
        settings.CefCommandLineArgs.Add("disable-gpu")
        settings.CefCommandLineArgs.Add("disable-gpu-vsync")
        settings.CefCommandLineArgs.Add("disable-d3d11")
        settings.CefCommandLineArgs.Add("disable-gpu-rasterization")

        'settings.CefCommandLineArgs("autoplay-policy") = "no-user-gesture-required"
        settings.LogFile = Path.Combine(Application.StartupPath, "lib", "browser.log")
        'Initialize Cef with the provided settings
        Cef.Initialize(settings)


        'Try
        '    Dim SettingsDone As Boolean = False
        '    Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
        '    SettingsDone = CBool(Integer.Parse(rkg.GetValue("SettingsDone").ToString))
        'Catch ex As Exception
        '    FirstStartup.ShowDialog()
        'End Try
        'Dim Style As New MetroStyleManager
        '
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            DarkModeValue = CBool(Integer.Parse(rkg.GetValue("Dark_Mode").ToString))
        Catch ex As Exception

        End Try
        Manager.Style = MetroColorStyle.Orange
        If DarkModeValue = True Then
            Manager.Theme = MetroThemeStyle.Dark
            DarkMode()
        Else
            Manager.Theme = MetroThemeStyle.Light
            LightMode()
        End If

        Me.StyleManager = Manager
        Manager.Owner = Me


        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            StartServer = Integer.Parse(rkg.GetValue("ServerPort").ToString)
        Catch ex As Exception

        End Try
        If StartServer > 0 Then
            Timer3.Enabled = True
            ServerThread = New Thread(AddressOf ServerStart)
            ServerThread.Priority = ThreadPriority.Normal
            ServerThread.IsBackground = True
            ServerThread.Start()
        End If


        waveOutSetVolume(0, 0)
        Try
            Dim FileLocation As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
            For Each File In FileLocation.GetFiles()
                If CBool(InStr(File.FullName, "gecko-network.txt")) Then
                    My.Computer.FileSystem.DeleteFile(Path.Combine(Application.StartupPath, File.FullName))
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Pfad = rkg.GetValue("Ordner").ToString
        Catch ex As Exception

        End Try


        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Episode_Prefix = rkg.GetValue("Prefix_E").ToString
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Season_Prefix = rkg.GetValue("Prefix_S").ToString
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            DefaultSubFunimation = rkg.GetValue("DefaultSubFunimation").ToString
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            DefaultSubCR = rkg.GetValue("DefaultSubCR").ToString
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Startseite = rkg.GetValue("Startseite").ToString
        Catch ex As Exception

        End Try
#Region "Startup IU"
        StatusToolTip.Active = True
#End Region

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            UseQueue = CBool(Integer.Parse(rkg.GetValue("QueueMode").ToString))
            'MsgBox(UseQueue.ToString)
        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            KodiNaming = CBool(Integer.Parse(rkg.GetValue("KodiSupport").ToString))

        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            KeepCache = CBool(Integer.Parse(rkg.GetValue("Keep_Cache").ToString))

        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            ffmpeg_command = rkg.GetValue("ffmpeg_command").ToString
        Catch ex As Exception
            ffmpeg_command = " -c copy -bsf:a aac_adtstoasc "
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Reso = Integer.Parse(rkg.GetValue("Resu").ToString)
            'MsgBox(Resu)
        Catch ex As Exception
        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            AoD_Reso = Integer.Parse(rkg.GetValue("AoD_Reso").ToString)
        Catch ex As Exception
            AoD_Reso = 0
        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubSprache = rkg.GetValue("Sub").ToString
        Catch ex As Exception
        End Try


        'Try
        '    Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
        '    SubFunimation = rkg.GetValue("Fun_Sub").ToString
        'Catch ex As Exception
        'End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubFunimationString = rkg.GetValue("Fun_Sub").ToString
            If SubFunimationString = "none" Then

            Else
                Dim SoftSubsStringSplit() As String = SubFunimationString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                    SubFunimation.Add(SoftSubsStringSplit(i))
                Next
            End If
        Catch ex As Exception
            If SubFunimation.Count = 0 Then
                SubFunimation.Add("en")
            End If
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubFolder_Value = rkg.GetValue("SubFolder_Value").ToString
        Catch ex As Exception
            SubFolder_Value = SubFolder_Nothing
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            MaxDL = Integer.Parse(rkg.GetValue("SL_DL").ToString)


        Catch ex As Exception
            MaxDL = 1
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            CR_NameMethode = Integer.Parse(rkg.GetValue("CR_NameMethode").ToString)
        Catch ex As Exception
            CR_NameMethode = 0
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            ErrorTolerance = Integer.Parse(rkg.GetValue("ErrorTolerance").ToString)
        Catch ex As Exception
            ErrorTolerance = 0
        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            MergeSubs = CBool(Integer.Parse(rkg.GetValue("MergeSubs").ToString))
        Catch ex As Exception
            Try
                Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
                MergeSubs = CBool(Integer.Parse(rkg.GetValue("MergeMP4").ToString))
            Catch ex2 As Exception

            End Try
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            IncludeLangName = CBool(Integer.Parse(rkg.GetValue("IncludeLangName").ToString))
        Catch ex As Exception

        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            IgnoreS1 = CBool(Integer.Parse(rkg.GetValue("IgnoreS1").ToString))
        Catch ex As Exception
            Try
                Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
                IgnoreS1 = CBool(Integer.Parse(rkg.GetValue("IgnoreS1").ToString))
            Catch ex2 As Exception

            End Try
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Dim Format As String = rkg.GetValue("VideoFormat").ToString
            If Format = ".mkv" Then
                VideoFormat = ".mkv"
                MergeSubsFormat = "copy"
            ElseIf Format = ".aac" Then
                VideoFormat = ".aac"
                MergeSubsFormat = "copy"
            End If
        Catch ex2 As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            HybridMode = CBool(Integer.Parse(rkg.GetValue("HybridMode").ToString))
        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Funimation_srt = CBool(Integer.Parse(rkg.GetValue("Funimation_srt").ToString))
        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Funimation_vtt = CBool(Integer.Parse(rkg.GetValue("Funimation_vtt").ToString))
        Catch ex As Exception

        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            'HardSubFunimation = rkg.GetValue("FunimationHardsub").ToString
            HardSubFunimation = "Disabled"
        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            DubFunimation = rkg.GetValue("FunimationDub").ToString
        Catch ex As Exception

        End Try

#Region "removed softsubtitle"

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SoftSubsString = rkg.GetValue("AddedSubs").ToString
            If SoftSubsString = "none" Then

            Else
                Dim SoftSubsStringSplit() As String = SoftSubsString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                    SoftSubs.Add(SoftSubsStringSplit(i))
                Next
            End If
        Catch ex As Exception
        End Try

#End Region

        If Reso = Nothing Then
            Reso = 1080
        End If

        If SubSprache = Nothing Then
            SubSprache = "enUS"
        End If

        BlockList = New List(Of String)
        BackgroundWorker1.RunWorkerAsync()

        RetryWithCachedFiles()


    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try

            Dim fileEntries As String() = Directory.GetFiles(Application.StartupPath + "\AdBlock", "*.txt")
            ' Process the list of .txt files found in the directory. '
            Dim fileName As String

            For Each fileName In fileEntries
                If (System.IO.File.Exists(fileName)) Then
                    BlockList.AddRange(System.IO.File.ReadAllLines(fileName).OrderBy(Function(x) Asc(x)).ToList)

                End If

            Next

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ListItemAdd(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal SoftSubs As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, Optional Service As String = "CR") ', ByVal AudioLang As String)
        Dim Thumbnail As Image = My.Resources.main_del
        Debug.WriteLine("ThumbnialURL: " + ThumbnialURL)
        Try
            Dim wc As New WebClient()
            Dim bytes As Byte() = wc.DownloadData(ThumbnialURL)
            Dim ms As New MemoryStream(bytes)
            Thumbnail = System.Drawing.Image.FromStream(ms)
        Catch ex As Exception
            'MsgBox(ex.ToString)

        End Try

        With ListView1.Items.Add("0")
            ItemConstructor(NameKomplett, NameP1, NameP2, Reso, HardSub, SoftSubs, Thumbnail, URL_DL, Pfad_DL, Service)
        End With

    End Sub
    Public Sub ItemConstructor(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal DisplayReso As String, ByVal HardSub As String, ByVal SoftSubs As String, ByVal Thumbnail As Image, ByVal URL_DL As String, ByVal Pfad_DL As String, ByVal Service As String)
        Dim Item As New CRD_List_Item

        Item.Visible = False
        Item.Parent = ListView1
        Item.Width = 838
        Item.Height = 142
#Region "Set Variables"
        'Item.SetUsedMap(UsedMap)
        'Item.Setffmpeg_command(ffmpeg_command)
        Item.SetCache(KeepCache)
        Item.SetMergeSubstoMP4(MergeSubs)
        Item.SetDebug2(Debug2)

#End Region

        Dim r As Rectangle
        Dim c As Integer = ListView1.Items.Count - 1

        r = ListView1.Items(c).Bounds()
        r.Width = 838
        r.Height = 142
        Item.SetService(Service)
        Item.SetTolerance(ErrorTolerance)
        Item.SetTargetReso(Reso)
        Item.SetLabelWebsite(NameP1)
        Item.SetLabelAnimeTitel(NameP2)
        Item.SetLabelResolution(DisplayReso)
        Item.SetLabelHardsub(HardSub)
        Item.SetThumbnailImage(Thumbnail)
        Item.SetLabelPercent("0%")
        Item.SetToolTip("Softsubs: " + SoftSubs)
        'MsgBox(Item.GetTextBound.ToString)
        ItemList.Add(Item)
        Item.SetBounds(r.X, r.Y, r.Width, r.Height)
        'Item.SetLocations(r.Y)
        'MsgBox("test " + r.Y.ToString)
        Item.Visible = True
        Dim TempHybridMode As Boolean = HybridMode
        If CBool(InStr(URL_DL, ".mpd")) Then
            TempHybridMode = False
        End If
        'MsgBox(URL_DL + vbNewLine + Pfad_DL + vbNewLine + NameKomplett + vbNewLine + TempHybridMode.ToString)
        Item.StartDownload(URL_DL, Pfad_DL, NameKomplett, TempHybridMode)
    End Sub
#Region "Manga DL"
    Public Sub MangaListItemAdd(ByVal NameP2 As String, ByVal ThumbnialURL As String, ByVal BaseURL As String, ByVal SiteList As List(Of String))

        Dim Thumbnail As Image = My.Resources.main_del
        Try
            Dim wc As New WebClient()
            wc.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
            Dim bytes As Byte() = wc.DownloadData(ThumbnialURL)
            Dim ms As New MemoryStream(bytes)
            Thumbnail = System.Drawing.Image.FromStream(ms)
        Catch ex As Exception
            'MsgBox(ex.ToString)
            'MsgBox(ThumbnialURL)
        End Try

        With ListView1.Items.Add("0")
            MangaItemConstructor("proxer.me", NameP2, Thumbnail, BaseURL, SiteList)
        End With


    End Sub
    Public Sub MangaItemConstructor(ByVal NameP1 As String, ByVal NameP2 As String, ByVal Thumbnail As Image, ByVal BaseURL As String, ByVal SiteList As List(Of String))
        Dim Item As New CRD_List_Item

        Item.Visible = False
        Item.Parent = ListView1
        Item.Width = 838
        Item.Height = 142
#Region "Set Variables"
        Item.SetDebug2(Debug2)
#End Region

        Dim r As Rectangle
        Dim c As Integer = ListView1.Items.Count - 1

        r = ListView1.Items(c).Bounds()
        r.Width = 838
        r.Height = 142
        Item.SetLabelWebsite(NameP1)
        Item.SetLabelAnimeTitel(NameP2)
        Item.SetLabelResolution("Manga")
        Item.SetLabelHardsub("Manga")
        Item.SetThumbnailImage(Thumbnail)
        Item.SetLabelPercent("0%")
        'MsgBox(Item.GetTextBound.ToString)
        ItemList.Add(Item)
        Item.SetBounds(r.X, r.Y, r.Width, r.Height)
        'Item.SetLocations(r.Y)
        'MsgBox("test " + r.Y.ToString)
        Item.Visible = True
        Item.DownloadMangaPages(Pfad, BaseURL, SiteList, NameP2)
    End Sub
#End Region


#Region "Season DL"

    Public Sub MassGrapp()
        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Text = Nothing
        Anime_Add.comboBox3.Text = Nothing
        Anime_Add.comboBox4.Text = Nothing
        Anime_Add.ComboBox1.Enabled = False
        Anime_Add.comboBox3.Enabled = True
        Anime_Add.comboBox4.Enabled = True
        Dim Anzahl As String() = WebbrowserText.Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Titel As String() = Anzahl(0).Split(New String() {"<meta content=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Titel2 As String() = Titel(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        'Label10.Text = Titel2(0)
        Dim c As Integer = Anzahl.Count - 1
        '  FolgenZahl.Text = c.ToString + " Folgen gefunden."

        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim URLGrapp As String() = Anzahl(i).Split(New String() {"title=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox("1" + Chr(34))

            Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Anime_Add.comboBox3.Items.Add(URLGrapp2(0))
            Anime_Add.comboBox4.Items.Add(URLGrapp2(0))
        Next

    End Sub

    Public Sub SeasonDropdownGrapp()

        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Text = Nothing
        Anime_Add.comboBox3.Text = Nothing
        Anime_Add.comboBox4.Text = Nothing
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = False
        Anime_Add.comboBox4.Enabled = False
        Dim Anzahl As String() = WebbrowserText.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim Titel As String() = Anzahl(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Titel2 As String() = Titel(0).Split(New String() {">"}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(Titel2(0))
            Anime_Add.ComboBox1.Items.Add(Titel2(1))
        Next

    End Sub

    Public Async Sub MassDL()
        If Anime_Add.comboBox3.Text = Nothing Then
            Exit Sub
        End If
        Anime_Add.Add_Display.Text = "preparing ..."
        Dim Website As String = WebbrowserText

        If Anime_Add.ComboBox1.Enabled = True Then
            Dim SeasonDropdownAnzahl As String() = Website.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(SeasonDropdownAnzahl)
            Dim SDV As Integer = 0
            For i As Integer = 0 To SeasonDropdownAnzahl.Count - 1
                If CBool(InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + Anime_Add.ComboBox1.SelectedItem.ToString + "</a>")) Then
                    SDV = i
                End If
            Next
            Website = SeasonDropdownAnzahl(SDV)
        End If
        Try
            Dim Anzahl As String() = Website.Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(Anzahl)
            Dim c As Integer = 0
            Aktuell = "0"
            If Anime_Add.comboBox4.SelectedIndex > Anime_Add.comboBox3.SelectedIndex Or Anime_Add.comboBox4.SelectedIndex = Anime_Add.comboBox3.SelectedIndex Then
                c = Anime_Add.comboBox4.SelectedIndex - Anime_Add.comboBox3.SelectedIndex + 1
            Else
                Dim TempCB3 As Integer = Anime_Add.comboBox3.SelectedIndex
                Dim TempCB4 As Integer = Anime_Add.comboBox4.SelectedIndex
                Anime_Add.comboBox3.SelectedIndex = TempCB4
                Anime_Add.comboBox4.SelectedIndex = TempCB3
                c = Anime_Add.comboBox4.SelectedIndex - Anime_Add.comboBox3.SelectedIndex + 1
            End If
            Gesamt = c.ToString
            For i As Integer = Anime_Add.comboBox3.SelectedIndex To Anime_Add.comboBox4.SelectedIndex

                For e As Integer = 0 To Integer.MaxValue
                    'FontLabel.Visible = True
                    'FontLabel.Text = RunningDownloads
                    If Grapp_RDY = True Then
                        Try
                            Dim ItemFinshedCount As Integer = 0
                            For i2 As Integer = 0 To ListView1.Items.Count - 1
                                If ItemList(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            RunningDownloads = ListView1.Items.Count - ItemFinshedCount

                        Catch ex As Exception
                            RunningDownloads = ListView1.Items.Count
                        End Try

                        If RunningDownloads < MaxDL Then
                            Exit For
                        Else
                            'MsgBox(e)
                            Await Task.Delay(1000)
                        End If
                    Else
                        Await Task.Delay(5000)
                    End If
                Next
                If Anime_Add.Mass_DL_Cancel = False Then
                    b = True
                    Exit For
                    Grapp_Abord = True
                    'MsgBox("dl_abourd")
                End If
                Dim d As Integer = i - Anime_Add.comboBox3.SelectedIndex + 1
                Dim URLGrapp As String() = Anzahl(i).Split(New String() {"<a href=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                If Debug2 = True Then
                    MsgBox("https://www.crunchyroll.com" + URLGrapp2(0))
                End If
                If UseQueue = True Then
                    Anime_Add.ListBox1.Items.Add("https://www.crunchyroll.com" + URLGrapp2(0))
                    Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    Anime_Add.Add_Display.ForeColor = Color.Black

                Else
                    Grapp_RDY = False
                    b = False
                    Navigate("https://www.crunchyroll.com" + URLGrapp2(0))
                End If

                Aktuell = d.ToString
                Anime_Add.Add_Display.Text = Aktuell + " / " + Gesamt
            Next


        Catch ex As Exception
            If Debug2 = True Then
                MsgBox(ex.ToString)
            End If
            Anime_Add.comboBox4.Items.Clear()
            Anime_Add.comboBox3.Items.Clear()
            Aktuell = 0.ToString
            Gesamt = 0.ToString

            Anime_Add.groupBox1.Visible = True
            Anime_Add.groupBox2.Visible = False
            Anime_Add.GroupBox3.Visible = False
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.btn_dl.Text = "Download" ' Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
    End Sub
#End Region



#Region "Sub to display"

    Public Function SubValuesToDisplay() As String
        Try
            Dim deDE As Boolean = False
            Dim enUS As Boolean = False
            Dim ptBR As Boolean = False
            Dim esLA As Boolean = False
            Dim frFR As Boolean = False
            Dim arME As Boolean = False
            Dim ruRU As Boolean = False
            Dim itIT As Boolean = False
            Dim esES As Boolean = False
            Dim ListReturn As String = Nothing
            For i As Integer = 0 To SoftSubs.Count - 1
                If SoftSubs(i) = "deDE" Then
                    deDE = True
                ElseIf SoftSubs(i) = "enUS" Then
                    enUS = True
                ElseIf SoftSubs(i) = "ptBR" Then
                    ptBR = True
                ElseIf SoftSubs(i) = "esLA" Then
                    esLA = True
                ElseIf SoftSubs(i) = "frFR" Then
                    frFR = True
                ElseIf SoftSubs(i) = "arME" Then
                    arME = True
                ElseIf SoftSubs(i) = "ruRU" Then
                    ruRU = True
                ElseIf SoftSubs(i) = "itIT" Then
                    itIT = True
                ElseIf SoftSubs(i) = "esES" Then
                    esES = True
                End If
            Next
            If deDE = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Deutsch"
                Else
                    ListReturn = ListReturn + ", Deutsch"
                End If

            End If
            If enUS = True Then
                If ListReturn = Nothing Then
                    ListReturn = "English"
                Else
                    ListReturn = ListReturn + ", English"
                End If
            End If
            If esLA = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Español (LA)"
                Else
                    ListReturn = ListReturn + ", Español (LA)"
                End If

            End If
            If ptBR = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Português (Brasil)"
                Else
                    ListReturn = ListReturn + ", Português (Brasil)"
                End If

            End If
            If frFR = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Français (France)"
                Else
                    ListReturn = ListReturn + ", Français (France)"
                End If
            End If
            If arME = True Then
                If ListReturn = Nothing Then
                    ListReturn = "العربية (Arabic)"
                Else
                    ListReturn = ListReturn + ", العربية (Arabic)"
                End If
            End If
            If ruRU = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Русский (Russian)"
                Else
                    ListReturn = ListReturn + ", Русский (Russian)"
                End If
            End If
            If itIT = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Italiano (Italian)"
                Else
                    ListReturn = ListReturn + ", Italiano (Italian)"
                End If

            End If
            If esES = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Español (España)"
                Else
                    ListReturn = ListReturn + ", Español (España)"
                End If
            End If

            Return ListReturn

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function HardSubValuesToDisplay(ByVal HardSub As String) As String
        Try
            HardSub = HardSub.Replace(Chr(34), "")
            If HardSub = "deDE" Then
                Return "Deutsch"
            ElseIf HardSub = "enUS" Or HardSub = "en" Then
                Return "English"
            ElseIf HardSub = "ptBR" Or HardSub = "pt" Then
                Return "Português (Brasil)"
            ElseIf HardSub = "esLA" Or HardSub = "es" Then
                Return "Español (LA)"
            ElseIf HardSub = "frFR" Then
                Return "Français (France)"
            ElseIf HardSub = "arME" Then
                Return "العربية (Arabic)"
            ElseIf HardSub = "ruRU" Then
                Return "Русский (Russian)"
            ElseIf HardSub = "itIT" Then
                Return "Italiano (Italian)"
            ElseIf HardSub = "esES" Then
                Return "Español (España)"
            ElseIf HardSub = "jaJP" Then
                Return "Japanese"
            ElseIf HardSub = "en" Then
                Return "English"
            ElseIf HardSub = "pt" Then
                Return "Português (Brasil)"
            ElseIf HardSub = "es" Then
                Return "Español (LA)"
            ElseIf HardSub = "de-DE" Then
                Return "Deutsch"
            ElseIf HardSub = "en-US" Then
                Return "English"
            ElseIf HardSub = "pt-BR" Then
                Return "Português (Brasil)"
            ElseIf HardSub = "es-LA" Then
                Return "Español (LA)"
            ElseIf HardSub = "fr-FR" Then
                Return "Français (France)"
            ElseIf HardSub = "ar-ME" Then
                Return "العربية (Arabic)"
            ElseIf HardSub = "ru-RU" Then
                Return "Русский (Russian)"
            ElseIf HardSub = "it-IT" Then
                Return "Italiano (Italian)"
            ElseIf HardSub = "es-ES" Then
                Return "Español (España)"
            ElseIf HardSub = "ja-JP" Then
                Return "Japanese"
            Else
                Return CB_SuB_Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CCtoMP4CC(ByVal HardSub As String) As String

        Try
            If HardSub = "deDE" Or HardSub = "de-DE" Then
                Return "ger"
            ElseIf HardSub = "enUS" Or HardSub = "en-US" Or HardSub = "en" Then
                Return "eng"
            ElseIf HardSub = "ptBR" Or HardSub = "pt-BR" Or HardSub = "pt" Then
                Return "por"
            ElseIf HardSub = "esLA" Or HardSub = "es-LA" Or HardSub = "es" Then
                Return "spa"
            ElseIf HardSub = "frFR" Or HardSub = "fr-FR" Then
                Return "fre"
            ElseIf HardSub = "arME" Or HardSub = "ar-ME" Then
                Return "ara"
            ElseIf HardSub = "ruRU" Or HardSub = "ru-RU" Then
                Return "rus"
            ElseIf HardSub = "itIT" Or HardSub = "it-IT" Then
                Return "ita"
            ElseIf HardSub = "esES" Or HardSub = "es-ES" Then
                Return "spa"
            ElseIf HardSub = "jaJP" Or HardSub = "ja-JP" Then
                Return "jpn"
            Else
                Return "chi"
            End If



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

    Public Sub GrappURL()

        Try
            'Throw New System.Exception("Test")

            Dim ffmpeg_command_temp As String = ffmpeg_command
            If VideoFormat = ".aac" Then
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
            End If

            Grapp_RDY = False
            Dim CR_Anime_Titel As String = Nothing
            Dim CR_Anime_Dub As String = Nothing
            Dim CR_Anime_Staffel As String = Nothing
            Dim CR_Anime_Folge As String = Nothing
            Dim CR_Anime_Name As String = Nothing

            Dim CR_Anime_Staffel_int As String = Nothing
            Dim CR_Anime_Folge_int As String = Nothing

#Region "Name + Pfad"
            Dim Pfad2 As String
            Dim TextBox2_Text As String = Nothing
            Dim CR_FilenName As String = Nothing



            Me.Invoke(New Action(Function() As Object

                                     ' My.Computer.Clipboard.SetText(WebbrowserText)
                                     Return Nothing
                                 End Function))
#Region "Name von Crunchyroll"


            If CBool(InStr(WebbrowserText, "<h4>")) Then ' false on movie true on series
                Dim CR_Name_1 As String() = WebbrowserText.Split(New String() {"<h4>"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim CR_Name_2 As String() = CR_Name_1(1).Split(New String() {"</h4>"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                Dim CR_Name_Staffel0_Folge1 As String()
                If CBool(InStr(CR_Name_2(0), ",")) Then
                    CR_Name_Staffel0_Folge1 = CR_Name_2(0).Split(New [Char]() {System.Convert.ToChar(",")}, System.StringSplitOptions.RemoveEmptyEntries)
                    CR_Anime_Staffel = CR_Name_Staffel0_Folge1(0).Trim()
                    CR_Anime_Folge = CR_Name_Staffel0_Folge1(1)
                    CR_Anime_Folge = String.Join(" ", CR_Anime_Folge.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
                Else
                    CR_Anime_Staffel = Nothing
                    CR_Anime_Folge = CR_Name_2(0).Trim()
                    'MsgBox(CR_Anime_Folge)

                    CR_Anime_Folge = String.Join(" ", CR_Anime_Folge.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")

                End If
                Dim CR_Name_4 As String() = CR_Name_1(0).Split(New String() {"class=" + Chr(34) + "text-link" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                Dim CR_Name_Anime0 As String() = CR_Name_4(CR_Name_4.Length - 1).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                CR_Name_Anime0(0) = String.Join(" ", CR_Name_Anime0(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_Name_Anime0(0), "[^\w\\-]", " ")
                CR_Anime_Titel = CR_Name_Anime0(0).Trim

                'CR_FilenName_Backup = RemoveExtraSpaces(CR_FilenName)


            End If
            If CBool(InStr(WebbrowserText, My.Resources.CR_Episode_Nr)) Then
                If CBool(InStr(WebbrowserText, My.Resources.CR_Episode_Nr + Chr(34))) Then
                    Debug.WriteLine("No Episode Number in a movie")
                Else
                    Dim CR_Episode_1 As String() = WebbrowserText.Split(New String() {My.Resources.CR_Episode_Nr}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim CR_Episode_2 As String() = CR_Episode_1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                    CR_Anime_Folge_int = String.Join(" ", CR_Episode_2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_Name_2(0), "[^\w\\-]", " ")

                    CR_Anime_Folge_int = RemoveExtraSpaces(CR_Anime_Folge_int)

                    Dim CleanedNumber As String = Nothing
                    Dim myChars() As Char = CR_Anime_Folge_int.ToCharArray()
                    For Each ch As Char In myChars
                        If Char.IsDigit(ch) Then
                            CleanedNumber = CleanedNumber + ch.ToString
                        ElseIf ch = "." Then
                            CleanedNumber = CleanedNumber + ch.ToString
                        ElseIf ch = "," Then
                            CleanedNumber = CleanedNumber + "."
                        End If
                    Next

                    If CleanedNumber = Nothing Then

                    ElseIf CBool(InStr(CleanedNumber, ".")) Then

                        Dim Folge_Double As Double = Double.Parse(CleanedNumber, CultureInfo.InvariantCulture)
                        If Folge_Double < 10 Then
                            CR_Anime_Folge_int = String.Format("{0:00.0}", Folge_Double)
                        End If
                    ElseIf Integer.Parse(CleanedNumber) < 10 Then
                        CR_Anime_Folge_int = "0" + CleanedNumber
                    End If
                End If
                If CBool(InStr(CR_Anime_Folge_int, ",")) Then
                    CR_Anime_Folge_int = CR_Anime_Folge_int.Replace(",", ".")
                End If

            End If

            If CBool(InStr(WebbrowserHeadText, My.Resources.CR_Season_Nr)) Then
                If CBool(InStr(WebbrowserHeadText, My.Resources.CR_Season_Nr + Chr(34))) Then
                    Debug.WriteLine("No Season Number in a movie")
                Else
                    Dim CR_Season_1 As String() = WebbrowserHeadText.Split(New String() {My.Resources.CR_Season_Nr}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim CR_Season_2 As String() = CR_Season_1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})

                    CR_Anime_Staffel_int = String.Join(" ", CR_Season_2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_Name_2(0), "[^\w\\-]", " ")
                    CR_Anime_Staffel_int = RemoveExtraSpaces(CR_Anime_Staffel_int)
                End If
            Else

                Debug.WriteLine("Not found?")
            End If

            If CBool(InStr(WebbrowserText, My.Resources.CR_MediaName)) = True Then ' And CBool(InStr(WebbrowserText, "&rdquo;</h4>")) 
                Dim CR_Name_1 As String() = WebbrowserText.Split(New String() {My.Resources.CR_MediaName}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim CR_Name_2 As String() = CR_Name_1(1).Split(New String() {My.Resources.CR_MediaName2}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                CR_Anime_Name = String.Join(" ", CR_Name_2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_Name_2(0), "[^\w\\-]", " ")
                CR_Anime_Name = RemoveExtraSpaces(CR_Anime_Name)
            End If

            If Season_Prefix = "[default season prefix]" Then

            Else
                If CR_Anime_Staffel_int = "0" Then
                Else
                    CR_Anime_Staffel = Season_Prefix + CR_Anime_Staffel_int
                End If
            End If

            If Episode_Prefix = "[default episode prefix]" Then

            Else
                CR_Anime_Folge = Episode_Prefix + CR_Anime_Folge_int
            End If

            If CR_Anime_Titel = Nothing Then

                CR_FilenName = CR_Anime_Name

            ElseIf CR_NameMethode = 0 Then
                If CR_Anime_Staffel = Nothing Then
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Folge
                Else
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Staffel + " " + CR_Anime_Folge
                End If
            ElseIf CR_NameMethode = 1 Then
                If CR_Anime_Staffel = Nothing Then
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Name
                Else
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Staffel + " " + CR_Anime_Name
                End If
            ElseIf CR_NameMethode = 2 Then
                If CR_Anime_Staffel = Nothing Then
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Folge + " " + CR_Anime_Name
                Else
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Staffel + " " + CR_Anime_Folge + " " + CR_Anime_Name
                End If
            ElseIf CR_NameMethode = 3 Then
                If CR_Anime_Staffel = Nothing Then
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Name + " " + CR_Anime_Folge
                Else
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Name + " " + CR_Anime_Staffel + " " + CR_Anime_Folge
                End If
            End If

            If KodiNaming = True And CR_Anime_Folge_int IsNot Nothing Then
                Dim KodiString As String = "[S"
                If CR_Anime_Staffel_int = "0" Then
                    CR_Anime_Staffel_int = "01"
                Else
                    CR_Anime_Staffel_int = "0" + CR_Anime_Staffel_int
                End If
                KodiString = KodiString + CR_Anime_Staffel_int + " E" + CR_Anime_Folge_int
                KodiString = KodiString + "] "
                CR_FilenName = KodiString + CR_FilenName
            End If
            'MsgBox(CR_FilenName)



#End Region
            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then

            Else
                CR_FilenName = RemoveExtraSpaces(String.Join(" ", TextBox2_Text.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)) 'System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))


            End If
            If CR_FilenName = Nothing Then
                CR_FilenName = WebbrowserTitle
            End If
            CR_FilenName = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = RemoveExtraSpaces(CR_FilenName)

            'My.Computer.FileSystem.WriteAllText("log.log", WebbrowserText, False)

            Pfad2 = UseSubfolder(CR_Anime_Titel, CR_Anime_Staffel, Pfad)

            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                    Pfad2 = Chr(34) + Pfad2 + CR_FilenName + VideoFormat + Chr(34)

                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = Chr(34) + Pfad + CR_FilenName + VideoFormat + Chr(34)
                End Try
            Else
                Pfad2 = Chr(34) + Pfad2 + CR_FilenName + VideoFormat + Chr(34)
            End If


#End Region
#Region "Subs"
            Dim SoftSubs2 As New List(Of String)
            If SoftSubs.Count > 0 Then
                For i As Integer = 0 To SoftSubs.Count - 1
                    If CBool(InStr(WebbrowserText, Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs(i) + Chr(34) + ",")) Then
                        SoftSubs2.Add(SoftSubs(i))
                    Else
                        'MsgBox("Softsubtitle for " + SoftSubs(i) + " is not avalible.", MsgBoxStyle.Information)
                    End If
                Next

            End If
            If SubSprache = "None" Then
                If CBool(InStr(WebbrowserText, Chr(34) + "hardsub_lang" + Chr(34) + ":null")) Then
                    SubSprache2 = "null"
                Else
                    Me.Invoke(New Action(Function() As Object
                                             ResoNotFoundString = WebbrowserText
                                             DialogTaskString = "Language"
                                             ErrorDialog.ShowDialog()
                                             Return Nothing
                                         End Function))
                    If UserCloseDialog = True Then
                        Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                    Else
                        If ResoBackString = Nothing Then
                        Else
                            SubSprache2 = ResoBackString
                        End If
                    End If
                    'Throw New System.Exception("Could not find the sub language")
                End If


            Else
                If CBool(InStr(WebbrowserText, Chr(34) + "hardsub_lang" + Chr(34) + ":" + Chr(34) + SubSprache + Chr(34) + ",")) Then
                    SubSprache2 = Chr(34) + SubSprache + Chr(34)

                ElseIf CBool(InStr(WebbrowserText, Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SubSprache + Chr(34) + ",")) Then
                    If MessageBox.Show("It look like only Softsubtitle are avalibe." + vbNewLine + "Are you want to use Softsubtitle this time instead?", "No Hardsubtitle", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        SubSprache2 = "null"
                        SoftSubs2.Add(SubSprache)
                    Else
                        Throw New System.Exception("Could not find the sub language")
                    End If


                Else
                    Me.Invoke(New Action(Function() As Object
                                             ResoNotFoundString = WebbrowserText
                                             DialogTaskString = "Language"
                                             ErrorDialog.ShowDialog()
                                             Return Nothing
                                         End Function))
                    If UserCloseDialog = True Then
                        Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                    Else
                        If ResoBackString = Nothing Then
                        Else
                            SubSprache2 = ResoBackString
                        End If
                    End If
                End If
            End If


#End Region
            If Grapp_Abord = True Then
                Grapp_RDY = True
                Grapp_Abord = False
                'MsgBox("grapp_abourd")
                Exit Sub

            End If
#Region "m3u8 suche"
            Dim CR_URI_Master As String = Nothing
            If SubsOnly = False Then
                Dim ii As Integer = 0
                'MsgBox(Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))

                Dim CR_URI_Master_Split1 As String() = WebbrowserText.Split(New String() {My.Resources.hls_Value}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim hls_List As New List(Of String)
                For i As Integer = 0 To CR_URI_Master_Split1.Count - 1
                    If CBool(InStr(CR_URI_Master_Split1(i), My.Resources.hls_endString)) Then
                        Dim s As String() = CR_URI_Master_Split1(i).Split(New String() {My.Resources.hls_endString}, System.StringSplitOptions.RemoveEmptyEntries)
                        hls_List.Add(s(0))
                    End If
                Next
                'Dim CR_URI_Master_Split1 As String() = WebbrowserText.Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To hls_List.Count - 1
                    If CBool(InStr(hls_List(i), Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then

                        Dim s() As String = hls_List(i).Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        CR_URI_Master = s(1).Replace("\/", "/")
                        Dim dub() As String = hls_List(i).Split(New String() {Chr(34) + "audio_lang" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                        Dim dub2() As String = dub(0).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                        CR_Anime_Dub = dub2(0)
                        'MsgBox(CR_URI_Master)
                    End If
                Next
                If CBool(InStr(CR_URI_Master, "master.m3u8")) Then
                    Me.Invoke(New Action(Function() As Object
                                             Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                             Me.Text = "Status: m3u8 found, looking for resolution"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))
                Else
                    Throw New System.Exception("Premium Episode")
                End If
            Else
                Me.Invoke(New Action(Function() As Object
                                         Anime_Add.StatusLabel.Text = "Status: Substitles only mode - skipped video"
                                         Me.Text = "Status: Substitles only mode - skipped video"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
            End If
#End Region

#Region "Download softsub file or build ffmpeg cmd"
            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            Dim SoftSubMergeMetatata As String = Nothing

            If SoftSubs2.Count > 0 Then
                If MergeSubs = True And SubsOnly = False Then
                    Dim DispositionIndex As Integer
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        Debug.WriteLine(SoftSubs2(i))
                        If SoftSubs2(i) = DefaultSubCR Then
                            DispositionIndex = i
                        End If
                        Dim SoftSub As String() = WebbrowserText.Split(New String() {Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs2(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("\/", "/")
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -i " + Chr(34) + SoftSub_3 + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -i " + Chr(34) + SoftSub_3 + Chr(34)
                        End If
                        SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (i + 1).ToString
                        If SoftSubMergeMetatata = Nothing Then
                            SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i)) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i)) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34)
                        End If

                    Next
                    If DispositionIndex = Nothing Then
                    Else
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        Dim ii As Integer = i
                        Me.Invoke(New Action(Function() As Object
                                                 Anime_Add.StatusLabel.Text = "Status: downloading subtitle file " + HardSubValuesToDisplay(SoftSubs2(ii))
                                                 Me.Text = "Status: downloading subtitle file " + HardSubValuesToDisplay(SoftSubs2(ii))
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                        Dim SoftSub As String() = WebbrowserText.Split(New String() {Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs2(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("\/", "/")
                        Dim client0 As New WebClient
                        client0.Encoding = Encoding.UTF8
                        Dim str0 As String = client0.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + "." + HardSubValuesToDisplay(SoftSubs2(i)) + Path.GetExtension(Pfad3)), "ass")
                        'MsgBox(FN)
                        If i = 0 And IncludeLangName = False Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        File.WriteAllText(Pfad4, str0, Encoding.UTF8)
                        Pause(3)
                    Next

                End If

            End If
#End Region

#Region "lösche doppel download"

            Dim Pfad5 As String = Pfad2.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object

                                         Anime_Add.StatusLabel.Text = "Status: The file video already exists."
                                         Me.Text = "Status: The file video already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                    Catch ex As Exception
                    End Try
                Else
                    Grapp_RDY = True
                    Exit Sub
                End If

            End If
#End Region
            If SubsOnly = False Then

                If Reso = 42 And HybridMode = False Then
                    If MergeSubs = True Then
                        URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub)
                    Else
                        URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub) + " " + ffmpeg_command_temp
                    End If
                    'MsgBox(URL_DL)
                Else


                    Dim client As New System.Net.WebClient
                    client.Encoding = Encoding.UTF8
                    'MsgBox(CR_URI_Master)
                    Dim str As String = client.DownloadString(CR_URI_Master)
                    'MsgBox(str)

                    If CBool(InStr(str, "x" + Reso.ToString + ",")) Then
                        Reso2 = "x" + Reso.ToString
                    Else
                        'MsgBox(str)
                        If CBool(InStr(str, ResoSave + ",")) Then
                            Reso2 = Reso2
                        Else
                            Me.Invoke(New Action(Function() As Object
                                                     DialogTaskString = "Resolution"
                                                     ResoNotFoundString = str
                                                     ErrorDialog.ShowDialog()
                                                     Return Nothing
                                                 End Function))


                            'MsgBox(ResoBackString)
                            If UserCloseDialog = True Then
                                Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                            Else
                                Reso2 = ResoBackString
                                ResoSave = ResoBackString
                            End If
                        End If
                    End If


                    Dim ffmpeg_url_1 As String() = str.Split(New String() {Reso2 + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim ffmpeg_url_3 As String() = Nothing
                    Dim ffmpeg_url_2 As String() = ffmpeg_url_1(1).Split(New [Char]() {Chr(34)})
                    ffmpeg_url_3 = ffmpeg_url_2(2).Split(New [Char]() {System.Convert.ToChar("#")})

                    If MergeSubs = True Then
                        URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub)

                    Else
                        URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub) + " " + ffmpeg_command_temp
                    End If

                End If

            End If
#Region "thumbnail"
            Dim thumbnail As String() = WebbrowserText.Split(New String() {My.Resources.thumbnailString}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + "}"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = thumbnail2(0).Replace("\/", "/")
#End Region
#Region "<li> constructor"
            Dim Subsprache3 As String = HardSubValuesToDisplay(SubSprache2.Replace(Chr(34), ""))
            Dim ResoHTMLDisplay As String = Nothing
            If ResoBackString = Nothing Then
                ResoHTMLDisplay = Reso.ToString + "p"
            ElseIf DialogTaskString = "Language" Then
                ResoHTMLDisplay = Reso.ToString + "p"
            Else
                Dim ResoHTML As String() = ResoBackString.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                If ResoHTML.Count > 1 Then
                    ResoHTMLDisplay = ResoHTML(1) + "p"

                Else
                    ResoHTMLDisplay = ResoHTML(0) + "p"
                End If
            End If
            Dim L2Name As String = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName_Backup, "[^\w\\-]", " ")
            If Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = "[Auto]"
            ElseIf Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = Reso2
            End If
            Pfad_DL = Pfad2
            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + HardSubValuesToDisplay(CR_Anime_Dub)

            If SubsOnly = True Then
                URL_DL = "-i [Subtitles only]"
            End If
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Pfad_DL, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail3, URL_DL, Pfad_DL)
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
            'Form1.RichTextBox1.Text = My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->"
#End Region

            Grapp_RDY = True
            Me.Invoke(New Action(Function() As Object

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
        Catch ex As Exception
            'Me.Invoke(New Action(Function() As Object

            '                         Anime_Add.StatusLabel.Text = "Status: idle"
            '                         Me.Text = "Crunchyroll Downloader"
            '                         Me.Invalidate()
            '                         Return Nothing
            '                     End Function))
            Grapp_RDY = True

            If CBool(InStr(ex.ToString, "Could not find the sub language")) Then
                MsgBox(Sub_language_NotFound + SubSprache)
            ElseIf CBool(InStr(ex.ToString, "RESOLUTION Not Found")) Then
                MsgBox(Resolution_NotFound)
            ElseIf CBool(InStr(ex.ToString, "Premium Episode")) Then
                MsgBox(Premium_Stream, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "System.UnauthorizedAccessException")) Then
                MsgBox(ErrorNoPermisson + vbNewLine + ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, Chr(34) + "UserAbort" + Chr(34))) Then
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            Else
                MsgBox(ex.ToString, MsgBoxStyle.Information)

            End If

        End Try
    End Sub

#Region "CR-Beta"
    Public Async Sub DownloadBetaSeasons()
        Try
            Dim ListOfEpisodes As New List(Of String)

            Dim EpisodeSplit() As String = CrBetaMassEpisodes.Split(New String() {Chr(34) + "id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To EpisodeSplit.Count - 1
                Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                ListOfEpisodes.Add("https://beta.crunchyroll.com/watch/" + EpisodeSplit2(0) + "/")
            Next


            Dim First As Integer = 0
            Dim Last As Integer = 0

            If Anime_Add.comboBox4.SelectedIndex > Anime_Add.comboBox3.SelectedIndex Or Anime_Add.comboBox4.SelectedIndex = Anime_Add.comboBox3.SelectedIndex Then
                First = Anime_Add.comboBox3.SelectedIndex
                Last = Anime_Add.comboBox4.SelectedIndex
            ElseIf Anime_Add.comboBox3.SelectedIndex > Anime_Add.comboBox4.SelectedIndex Then
                First = Anime_Add.comboBox4.SelectedIndex
                Last = Anime_Add.comboBox3.SelectedIndex
            End If



            Dim Anzahl As Integer = Anime_Add.comboBox4.SelectedIndex - Anime_Add.comboBox3.SelectedIndex
            For i As Integer = First To Last
                For e As Integer = 0 To Integer.MaxValue
                    If Grapp_RDY = True Then
                        Try
                            Dim ItemFinshedCount As Integer = 0
                            For i2 As Integer = 0 To ListView1.Items.Count - 1
                                If ItemList(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            RunningDownloads = ListView1.Items.Count - ItemFinshedCount

                        Catch ex As Exception
                            RunningDownloads = ListView1.Items.Count
                        End Try

                        If RunningDownloads < MaxDL Then
                            Exit For
                        Else
                            'MsgBox(e)
                            Await Task.Delay(1000)
                        End If
                    Else
                        Await Task.Delay(5000)
                    End If
                Next
                If Anime_Add.Mass_DL_Cancel = False Then
                    b = True
                    Exit For
                    Grapp_Abord = True
                    'MsgBox("dl_abourd")
                End If
                If UseQueue = True Then
                    Anime_Add.ListBox1.Items.Add(ListOfEpisodes(i))
                    Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    Anime_Add.Add_Display.ForeColor = Color.Black

                Else
                    Grapp_RDY = False
                    b = False
                    Debug.WriteLine("b: " + b.ToString)
                    Navigate(ListOfEpisodes(i))
                End If


                Anime_Add.Add_Display.Text = (i - First + 1).ToString + " / " + (Last - First + 1).ToString
            Next


        Catch ex As Exception
            If Debug2 = True Then
                MsgBox(ex.ToString)
            End If
            Anime_Add.comboBox4.Items.Clear()
            Anime_Add.comboBox3.Items.Clear()
            Aktuell = 0.ToString
            Gesamt = 0.ToString

            Anime_Add.groupBox1.Visible = True
            Anime_Add.groupBox2.Visible = False
            Anime_Add.GroupBox3.Visible = False
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.btn_dl.Text = "Download" 'btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
    End Sub
    Public Sub GetBetaSeasons(ByVal JsonUrl As String)


        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Text = Nothing
        Anime_Add.comboBox3.Text = Nothing
        Anime_Add.comboBox4.Text = Nothing
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = True
        Anime_Add.comboBox4.Enabled = True

        Dim SeasonJson As String = Nothing

        Try
            Using client As New WebClient()
                client.Encoding = System.Text.Encoding.UTF8
                client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                SeasonJson = client.DownloadString(JsonUrl)
            End Using
        Catch ex As Exception
            Debug.WriteLine("error- getting SeasonJson data")

        End Try


        Dim ParameterSplit() As String = JsonUrl.Split(New String() {"&locale="}, System.StringSplitOptions.RemoveEmptyEntries)

        CrBetaMassParameters = ParameterSplit(1)
        CrBetaMass = SeasonJson

        Dim BaseURLBuilder() As String = JsonUrl.Split(New String() {"seasons?"}, System.StringSplitOptions.RemoveEmptyEntries)
        CrBetaMassBaseURL = BaseURLBuilder(0)


        Dim SeasonSplit() As String = SeasonJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 1 To SeasonSplit.Count - 1
            Dim SeasonSplit2() As String = SeasonSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Anime_Add.ComboBox1.Items.Add(SeasonSplit2(0))
        Next



    End Sub
    Public Sub GetBetaVideoProxy(ByVal requesturl As String, ByVal WebsiteURL As String)
        Dim Evaluator = New Thread(Sub() Me.GetBetaVideo(requesturl, WebsiteURL))
        Evaluator.Start()
    End Sub

    Public Sub GetBetaVideo(ByVal Streams As String, ByVal WebsiteURL As String)

        Try
            Grapp_RDY = False

            Dim ffmpeg_command_temp As String = ffmpeg_command
            If VideoFormat = ".aac" Then
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
            End If
            Dim CR_series_title As String = Nothing
            Dim CR_season_number As String = Nothing
            Dim CR_episode As String = Nothing
            Dim CR_Anime_Staffel_int As String = Nothing
            Dim CR_episode_int As String = Nothing
            Dim CR_title As String = Nothing
            Dim CR_audio_locale As String = Nothing

#Region "Name + Pfad"
            Dim Pfad2 As String
            Dim TextBox2_Text As String = Nothing
            Dim CR_FilenName As String = Nothing
            Dim ObjectJson As String = Nothing


            Me.Invoke(New Action(Function() As Object
                                     TextBox2_Text = Anime_Add.textBox2.Text

                                     Return Nothing
                                 End Function))
#Region "Name von Crunchyroll"

            Dim ObjectsURLBuilder() As String = Streams.Split(New String() {"videos"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder2() As String = ObjectsURLBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder3() As String = WebsiteURL.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim ObjectsURL As String = ObjectsURLBuilder(0) + "objects/" + ObjectsURLBuilder4(0) + ObjectsURLBuilder2(1)

            Debug.WriteLine(ObjectsURL)

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    ObjectJson = client.DownloadString(ObjectsURL)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting name data")
                Exit Sub
            End Try

            Dim ser As JObject = JObject.Parse(ObjectJson)
            Dim data As List(Of JToken) = ser.Children().ToList


            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then



                For Each item As JProperty In data
                    item.CreateReader()
                    Select Case item.Name
                        Case "items" 'each record is inside the entries array
                            For Each Entry As JObject In item.Values
                                Try
                                    Dim Title As String = Entry("title").ToString
                                    CR_title = String.Join(" ", Title.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)

                                Catch ex As Exception
                                End Try
                                Dim SubData As List(Of JToken) = Entry.Children().ToList
                                For Each SubItem As JProperty In SubData
                                    'SubItem.CreateReader()

                                    Select Case SubItem.Name
                                        Case "episode_metadata"
                                            For Each SubEntry As JProperty In SubItem.Values
                                                Select Case SubEntry.Name
                                                    Case "series_title"
                                                        CR_series_title = SubEntry.Value.ToString
                                                    'Case "season_title"
                                                    '    CR_season_title = SubEntry.Value.ToString
                                                    Case "season_number"
                                                        CR_season_number = SubEntry.Value.ToString
                                                    Case "episode"
                                                        CR_episode = SubEntry.Value.ToString
                                                End Select
                                            Next
                                    End Select
                                Next
                            Next

                    End Select
                Next


                'My.Computer.Clipboard.SetText(ObjectJson)

                '
                CR_Anime_Staffel_int = CR_season_number


                If IgnoreS1 = True Then
                    If CR_season_number = "1" Or CR_season_number = "0" Then
                        CR_season_number = Nothing
                    End If
                End If

                CR_episode_int = CR_episode

                If Season_Prefix = "[default season prefix]" Then
                    If CR_episode = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing

                    ElseIf CR_season_number = Nothing Then
                    Else

                        CR_season_number = "Season " + CR_season_number
                    End If
                Else
                    If CR_episode = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing

                    ElseIf CR_season_number = Nothing Then

                    Else
                        CR_season_number = Season_Prefix + CR_season_number
                    End If

                End If

                If CR_episode = Nothing Then

                ElseIf Episode_Prefix = "[default episode prefix]" Then
                    CR_episode = "Episode " + CR_episode
                Else
                    CR_episode = Episode_Prefix + CR_episode
                End If

                If CR_NameMethode = 0 Then 'nummer
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_episode
                    Else
                        CR_FilenName = CR_series_title + " " + CR_season_number + " " + CR_episode
                    End If
                ElseIf CR_NameMethode = 1 Then 'name
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_series_title + " " + CR_title
                    Else
                        CR_FilenName = CR_series_title + " " + CR_season_number + " " + CR_title
                    End If
                ElseIf CR_NameMethode = 2 Then ' nummer - name
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_series_title + " " + CR_episode + " " + CR_title
                    Else
                        CR_FilenName = CR_series_title + " " + CR_season_number + " " + CR_episode + " " + CR_title
                    End If
                ElseIf CR_NameMethode = 3 Then ' name - nummer
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_title + " " + CR_episode
                    Else
                        CR_FilenName = CR_series_title + " " + CR_title + " " + CR_season_number + " " + CR_episode
                    End If
                End If




                If KodiNaming = True Then
                    Dim KodiString As String = "[S"
                    If CR_Anime_Staffel_int = "0" Then
                        CR_Anime_Staffel_int = "01"
                    Else
                        CR_Anime_Staffel_int = "0" + CR_Anime_Staffel_int
                    End If
                    Dim CR_episode_nr As String = CR_episode_int
                    If CR_episode_nr.Length = 1 Then
                        CR_episode_nr = "0" + CR_episode_nr
                    End If
                    KodiString = KodiString + CR_Anime_Staffel_int + " E" + CR_episode_nr
                    KodiString = KodiString + "] "
                    CR_FilenName = KodiString + CR_FilenName
                End If


                Debug.WriteLine(CR_FilenName)
#End Region

            Else
                CR_FilenName = RemoveExtraSpaces(String.Join(" ", TextBox2_Text.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)) 'System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))


            End If

            CR_FilenName = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = RemoveExtraSpaces(CR_FilenName)

            'My.Computer.FileSystem.WriteAllText("log.log", WebbrowserText, False)

            Pfad2 = UseSubfolder(CR_series_title, CR_season_number, Pfad)

            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                    Pfad2 = Chr(34) + Pfad2 + CR_FilenName + VideoFormat + Chr(34)

                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = Chr(34) + Pfad + "\" + CR_FilenName + VideoFormat + Chr(34)
                    Pfad2 = Pfad2.Replace("\\", "\")
                End Try
            Else
                Pfad2 = Chr(34) + Pfad2 + CR_FilenName + VideoFormat + Chr(34)
            End If




#End Region

#Region "VideoJson"


            Dim VideoJson As String = Nothing
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    VideoJson = client.DownloadString(Streams)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting stream data")
                Exit Sub
            End Try

            Dim hls_type As String = Nothing

            If CBool(InStr(VideoJson, Chr(34) + "adaptive_hls")) = True Then
                hls_type = "adaptive_hls"
            ElseIf CBool(InStr(VideoJson, Chr(34) + "multitrack_adaptive_hls_v2")) = True Then
                hls_type = "multitrack_adaptive_hls_v2"
            ElseIf CBool(InStr(VideoJson, Chr(34) + "vo_adaptive_hls")) = True Then
                hls_type = "vo_adaptive_hls"
            Else
                MsgBox("No download stream avalible", MsgBoxStyle.Critical)
                Exit Sub
            End If
            'My.Computer.Clipboard.SetText(VideoJson)
            'MsgBox(SubSprache)
            Dim LangNew As String = ConvertCC(SubSprache)


#End Region

#Region "Download softsub file or build ffmpeg cmd"
            Dim SoftSubs2 As New List(Of String)
            If SoftSubs.Count > 0 Then
                For i As Integer = 0 To SoftSubs.Count - 1
                    If CBool(InStr(VideoJson, Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + ConvertCC(SoftSubs(i)) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                        SoftSubs2.Add(SoftSubs(i))
                    Else
                        'MsgBox("Softsubtitle for " + SoftSubs(i) + " is not avalible.", MsgBoxStyle.Information)
                    End If
                Next

            End If

            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            Dim SoftSubMergeMetatata As String = Nothing

            If SoftSubs2.Count > 0 Then
                If MergeSubs = True And SubsOnly = False Then
                    Dim DispositionIndex As Integer
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        Debug.WriteLine(SoftSubs2(i))
                        If SoftSubs2(i) = DefaultSubCR Then
                            DispositionIndex = i
                        End If
                        Dim SoftSub As String() = VideoJson.Split(New String() {Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + ConvertCC(SoftSubs2(i)) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -i " + Chr(34) + SoftSub_3 + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -i " + Chr(34) + SoftSub_3 + Chr(34)
                        End If
                        SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (i + 1).ToString
                        If SoftSubMergeMetatata = Nothing Then
                            SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i)) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i)) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34)
                        End If

                    Next
                    If DispositionIndex = Nothing Then
                    Else
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        Dim i2 As Integer = i
                        Me.Invoke(New Action(Function() As Object
                                                 Anime_Add.StatusLabel.Text = "Status: downloading subtitle file " + HardSubValuesToDisplay(SoftSubs2(i2))
                                                 Me.Text = "Status: downloading subtitle file " + HardSubValuesToDisplay(SoftSubs2(i2))
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                        Dim SoftSub As String() = VideoJson.Split(New String() {Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + ConvertCC(SoftSubs2(i)) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        'MsgBox(SoftSub_3)
                        Dim client0 As New WebClient
                        client0.Encoding = Encoding.UTF8
                        Dim str0 As String = client0.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + "." + HardSubValuesToDisplay(SoftSubs2(i)) + Path.GetExtension(Pfad3)), "ass")
                        'MsgBox(FN)
                        If i = 0 And IncludeLangName = False Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        File.WriteAllText(Pfad4, str0, Encoding.UTF8)
                        Pause(3)
                    Next

                End If

            End If
#End Region

#Region "m3u8 suche"

            If CBool(InStr(VideoJson, "audio_locale")) Then
                Dim CR_audio As String() = VideoJson.Split(New String() {"audio_locale" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim CR_audio2 As String() = CR_audio(1).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                CR_audio_locale = String.Join(" ", CR_audio2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)

            End If

            Dim CR_URI_Master As String = Nothing
            'If SubsOnly = False Then
            Dim ii As Integer = 0
            Dim CR_VideoJson As String() = VideoJson.Split(New String() {hls_type}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim CR_VideoJsonHardSubs As String() = CR_VideoJson(1).Split(New String() {"hardsub_locale" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Debug.WriteLine(LangNew)
            Debug.WriteLine(CR_VideoJsonHardSubs.Count.ToString)
            Dim hls_List As New List(Of String)
            For i As Integer = 0 To CR_VideoJsonHardSubs.Count - 1
                If CBool(InStr(CR_VideoJsonHardSubs(i), LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                    CR_URI_Master = CR_VideoJsonHardSubs(i).Replace(LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34), "").Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)(0)
                    Exit For
                End If
            Next

            If CR_URI_Master = Nothing Then
                Me.Invoke(New Action(Function() As Object
                                         ResoNotFoundString = VideoJson
                                         DialogTaskString = "Language_CR_Beta"
                                         ErrorDialog.ShowDialog()
                                         Return Nothing
                                     End Function))
                If UserCloseDialog = True Then
                    Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                Else
                    LangNew = ResoBackString
                    ResoBackString = Nothing
                    For i As Integer = 0 To CR_VideoJsonHardSubs.Count - 1
                        If CBool(InStr(CR_VideoJsonHardSubs(i), LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                            CR_URI_Master = CR_VideoJsonHardSubs(i).Replace(LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34), "").Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)(0)
                            Exit For
                        End If
                    Next
                End If
            End If

            CR_URI_Master = CR_URI_Master.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")

            If CBool(InStr(CR_URI_Master, "master.m3u8")) Then
                Me.Invoke(New Action(Function() As Object
                                         Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                         Me.Text = "Status: m3u8 found, looking for resolution"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
            Else
                Throw New System.Exception("Premium Episode")
            End If
            'Else
            '    Me.Invoke(New Action(Function() As Object
            '                             Anime_Add.StatusLabel.Text = "Status: Substitles only mode - skipped video"
            '                             Me.Text = "Status: Substitles only mode - skipped video"
            '                             Me.Invalidate()
            '                             Return Nothing
            '                         End Function))
            'End If
#End Region


#Region "lösche doppel download"

            Dim Pfad5 As String = Pfad2.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) And SubsOnly = False Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object

                                         Anime_Add.StatusLabel.Text = "Status: The file video already exists."
                                         Me.Text = "Status: The file video already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                    Catch ex As Exception
                    End Try
                Else
                    Grapp_RDY = True
                    Exit Sub
                End If

            End If
#End Region
            If SubsOnly = False Then

                If Reso = 42 And HybridMode = False Then
                    If MergeSubs = True Then
                        URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command_temp + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale)
                    Else
                        URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command_temp
                    End If
                    'MsgBox(URL_DL)
                Else


                    Dim client As New System.Net.WebClient
                    client.Encoding = Encoding.UTF8
                    'MsgBox(CR_URI_Master)
                    Dim str As String = client.DownloadString(CR_URI_Master)
                    'MsgBox(str)

                    If CBool(InStr(str, "x" + Reso.ToString + ",")) Then
                        Reso2 = "x" + Reso.ToString
                    Else
                        'MsgBox(str)
                        If CBool(InStr(str, ResoSave + ",")) Then
                            Reso2 = Reso2
                        Else
                            Me.Invoke(New Action(Function() As Object
                                                     DialogTaskString = "Resolution"
                                                     ResoNotFoundString = str
                                                     ErrorDialog.ShowDialog()
                                                     Return Nothing
                                                 End Function))


                            'MsgBox(ResoBackString)
                            If UserCloseDialog = True Then
                                Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                            Else
                                Reso2 = ResoBackString
                                ResoSave = ResoBackString
                            End If
                        End If
                    End If


                    Dim ffmpeg_url_1 As String() = str.Split(New String() {Reso2 + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim ffmpeg_url_3 As String() = Nothing
                    Dim ffmpeg_url_2 As String() = ffmpeg_url_1(1).Split(New [Char]() {Chr(34)})
                    ffmpeg_url_3 = ffmpeg_url_2(2).Split(New [Char]() {System.Convert.ToChar("#")})
                    Debug.WriteLine(CR_audio_locale)
                    If MergeSubs = True Then

                        Debug.WriteLine(ConvertCC(CR_audio_locale))
                        URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale)
                        'URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command

                    Else
                        URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command_temp
                    End If

                End If

            End If
#Region "thumbnail"
            Dim thumbnail As String() = ObjectJson.Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + "}"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = "https://" + thumbnail2(0).Replace("\/", "/")
#End Region
#Region "<li> constructor"
            Dim Subsprache3 As String = "none" 'HardSubValuesToDisplay(SubSprache2.Replace(Chr(34), ""))
            Dim ResoHTMLDisplay As String = Nothing
            If ResoBackString = Nothing Then
                ResoHTMLDisplay = Reso.ToString + "p"
            ElseIf DialogTaskString = "Language" Then
                ResoHTMLDisplay = Reso.ToString + "p"
            Else
                Dim ResoHTML As String() = ResoBackString.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                If ResoHTML.Count > 1 Then
                    ResoHTMLDisplay = ResoHTML(1) + "p"

                Else
                    ResoHTMLDisplay = ResoHTML(0) + "p"
                End If
            End If
            Dim L2Name As String = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName_Backup, "[^\w\\-]", " ")
            If Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = "[Auto]"
            ElseIf Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = Reso2
            End If
            Pfad_DL = Pfad2
            Dim L1Name_Split As String() = WebsiteURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + HardSubValuesToDisplay(CR_audio_locale)

            If SubsOnly = True Then
                URL_DL = "-i [Subtitles only]"
            End If
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Path.GetFileName(Pfad_DL.Replace(Chr(34), "")), L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail3, URL_DL, Pfad_DL)
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_title + " <br> " + CR_season_number + " " + CR_episode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
            'Form1.RichTextBox1.Text = My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->"
#End Region

            Grapp_RDY = True
            Me.Invoke(New Action(Function() As Object

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
            Grapp_RDY = True

            If CBool(InStr(ex.ToString, "Could not find the sub language")) Then
                MsgBox(Sub_language_NotFound + SubSprache)
            ElseIf CBool(InStr(ex.ToString, "RESOLUTION Not Found")) Then
                MsgBox(Resolution_NotFound)
            ElseIf CBool(InStr(ex.ToString, "Premium Episode")) Then
                MsgBox(Premium_Stream, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "System.UnauthorizedAccessException")) Then
                MsgBox(ErrorNoPermisson + vbNewLine + ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, Chr(34) + "UserAbort" + Chr(34))) Then
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            Else
                MsgBox(ex.ToString, MsgBoxStyle.Information)

            End If

        End Try
    End Sub

    Function ConvertCC(ByVal CC As String) As String

        Try
            If CC = "deDE" Then
                Return "de-DE"
            ElseIf CC = "enUS" Then
                Return "en-US"
            ElseIf CC = "ptBR" Then
                Return "pt-BR"
            ElseIf CC = "esLA" Then
                Return "es-LA"
            ElseIf CC = "frFR" Then
                Return "fr-FR"
            ElseIf CC = "arME" Then
                Return "ar-ME"
            ElseIf CC = "ruRU" Then
                Return "ru-RU"
            ElseIf CC = "itIT" Then
                Return "it-IT"
            ElseIf CC = "esES" Then
                Return "es-ES"
            ElseIf CC = "jaJP" Then
                Return "ja-JP"
            ElseIf CC = "None" Then
                Return ""
            Else
                Return CB_SuB_Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region

#Region "vrv.co"


    Public Sub Get_VRV_VideoProxy(ByVal requesturl As String, ByVal WebsiteURL As String)
        Dim Evaluator = New Thread(Sub() Me.Get_VRV_Video(requesturl, WebsiteURL))
        Evaluator.Start()
    End Sub

    Public Sub Get_VRV_Video(ByVal Streams As String, ByVal WebsiteURL As String)

        Try
            Grapp_RDY = False

            Dim ffmpeg_command_temp As String = ffmpeg_command
            If VideoFormat = ".aac" Then
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
            End If
            Dim CR_series_title As String = Nothing
            Dim CR_season_number As String = Nothing
            Dim CR_episode As String = Nothing
            Dim CR_Anime_Staffel_int As String = Nothing
            Dim CR_episode_int As String = Nothing
            Dim CR_title As String = Nothing
            Dim CR_audio_locale As String = Nothing

#Region "Name + Pfad"
            Dim Pfad2 As String
            Dim TextBox2_Text As String = Nothing
            Dim CR_FilenName As String = Nothing
            Dim ObjectJson As String = Nothing

            Me.Invoke(New Action(Function() As Object
                                     TextBox2_Text = Anime_Add.textBox2.Text
                                     Return Nothing
                                 End Function))
#Region "Name von Crunchyroll"

            Dim ObjectsURLBuilder() As String = Streams.Split(New String() {"videos"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder2() As String = ObjectsURLBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder3() As String = WebsiteURL.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim ObjectsURL As String = ObjectsURLBuilder(0) + "objects/" + ObjectsURLBuilder4(0) + ObjectsURLBuilder2(1)

            Debug.WriteLine(ObjectsURL)

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    ObjectJson = client.DownloadString(ObjectsURL)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting name data")
                Exit Sub
            End Try

            Dim ser As JObject = JObject.Parse(ObjectJson)
            Dim data As List(Of JToken) = ser.Children().ToList




            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then


                For Each item As JProperty In data
                    item.CreateReader()
                    Select Case item.Name
                        Case "items" 'each record is inside the entries array
                            For Each Entry As JObject In item.Values
                                Try
                                    Dim Title As String = Entry("title").ToString
                                    CR_title = String.Join(" ", Title.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)

                                Catch ex As Exception
                                End Try
                                Dim SubData As List(Of JToken) = Entry.Children().ToList
                                For Each SubItem As JProperty In SubData
                                    'SubItem.CreateReader()


                                    Select Case SubItem.Name
                                        Case "episode_metadata"
                                            For Each SubEntry As JProperty In SubItem.Values
                                                Select Case SubEntry.Name
                                                    Case "series_title"
                                                        CR_series_title = SubEntry.Value.ToString
                                                    'Case "season_title"
                                                    '    CR_season_title = SubEntry.Value.ToString
                                                    Case "season_number"
                                                        CR_season_number = SubEntry.Value.ToString
                                                    Case "episode_number"
                                                        CR_episode = SubEntry.Value.ToString
                                                End Select
                                            Next
                                    End Select
                                Next
                            Next

                    End Select
                Next


                'My.Computer.Clipboard.SetText(ObjectJson)

                '
                CR_Anime_Staffel_int = CR_season_number

                If IgnoreS1 = True Then
                    If CR_season_number = "1" Or CR_season_number = "0" Then
                        CR_season_number = Nothing
                    End If
                End If

                CR_episode_int = CR_episode

                If Season_Prefix = "[default season prefix]" Then
                    If CR_episode = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing
                    ElseIf CR_season_number = Nothing Then
                    Else

                        CR_season_number = "Season " + CR_season_number
                    End If
                Else
                    If CR_episode = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing
                    ElseIf CR_season_number = Nothing Then
                    Else
                        CR_season_number = Season_Prefix + CR_season_number
                    End If

                End If

                If CR_episode = Nothing Then

                ElseIf Episode_Prefix = "[default episode prefix]" Then
                    CR_episode = "Episode " + CR_episode
                Else
                    CR_episode = Episode_Prefix + CR_episode
                End If

                If CR_NameMethode = 0 Then 'nummer
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_episode
                    Else
                        CR_FilenName = CR_series_title + " " + CR_season_number + " " + CR_episode
                    End If
                ElseIf CR_NameMethode = 1 Then 'name
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_title
                    Else
                        CR_FilenName = CR_series_title + " " + CR_season_number + " " + CR_title
                    End If
                ElseIf CR_NameMethode = 2 Then ' nummer - name
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_episode + " " + CR_title
                    Else
                        CR_FilenName = CR_series_title + " " + CR_season_number + " " + CR_episode + " " + CR_title
                    End If
                ElseIf CR_NameMethode = 3 Then ' name - nummer
                    If CR_season_number = Nothing Then
                        CR_FilenName = CR_series_title + " " + CR_title + " " + CR_episode
                    Else
                        CR_FilenName = CR_series_title + " " + CR_title + " " + CR_season_number + " " + CR_episode
                    End If
                End If


                Try

                    If KodiNaming = True Then
                        Dim KodiString As String = "[S"
                        If CR_Anime_Staffel_int = "0" Then
                            CR_Anime_Staffel_int = "01"
                        Else
                            CR_Anime_Staffel_int = "0" + CR_Anime_Staffel_int
                        End If
                        Dim CR_episode_nr As String = CR_episode_int

                        If CR_episode_nr.Length = 1 Then
                            CR_episode_nr = "0" + CR_episode_nr
                        ElseIf CR_episode_nr.Length = 0 Then
                            Throw New System.Exception("no episode")
                        End If
                        KodiString = KodiString + CR_Anime_Staffel_int + " E" + CR_episode_nr
                        KodiString = KodiString + "] "
                        CR_FilenName = KodiString + CR_FilenName
                    End If

                Catch ex As Exception

                End Try

                Debug.WriteLine(CR_FilenName)
#End Region

            Else
                CR_FilenName = RemoveExtraSpaces(String.Join(" ", TextBox2_Text.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)) 'System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))


            End If

            CR_FilenName = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = RemoveExtraSpaces(CR_FilenName)

            'My.Computer.FileSystem.WriteAllText("log.log", WebbrowserText, False)

            Pfad2 = UseSubfolder(CR_series_title, CR_season_number, Pfad)

            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                    Pfad2 = Chr(34) + Pfad2 + CR_FilenName + VideoFormat + Chr(34)

                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = Chr(34) + Pfad + "\" + CR_FilenName + VideoFormat + Chr(34)
                    Pfad2 = Pfad2.Replace("\\", "\")
                End Try
            Else
                Pfad2 = Chr(34) + Pfad2 + CR_FilenName + VideoFormat + Chr(34)
            End If




#End Region

#Region "VideoJson"


            Dim VideoJson As String = Nothing
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    VideoJson = client.DownloadString(Streams)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting stream data")
                Exit Sub
            End Try

            Dim hls_type As String = Nothing

            If CBool(InStr(VideoJson, Chr(34) + "adaptive_hls")) = True Then
                hls_type = "adaptive_hls"
            ElseIf CBool(InStr(VideoJson, Chr(34) + "multitrack_adaptive_hls_v2")) = True Then
                hls_type = "multitrack_adaptive_hls_v2"
            ElseIf CBool(InStr(VideoJson, Chr(34) + "vo_adaptive_hls")) = True Then
                hls_type = "vo_adaptive_hls"
            Else
                MsgBox("No download stream avalible", MsgBoxStyle.Critical)
                Exit Sub
            End If
            'My.Computer.Clipboard.SetText(VideoJson)
            'MsgBox(SubSprache)
            Dim LangNew As String = ConvertCC(SubSprache)


#End Region

#Region "Download softsub file or build ffmpeg cmd"
            Dim SoftSubs2 As New List(Of String)
            If SoftSubs.Count > 0 Then
                For i As Integer = 0 To SoftSubs.Count - 1
                    If CBool(InStr(VideoJson, Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + ConvertCC(SoftSubs(i)) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                        SoftSubs2.Add(SoftSubs(i))
                    Else
                        'MsgBox("Softsubtitle for " + SoftSubs(i) + " is not avalible.", MsgBoxStyle.Information)
                    End If
                Next

            End If

            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            Dim SoftSubMergeMetatata As String = Nothing

            If SoftSubs2.Count > 0 Then
                If MergeSubs = True And SubsOnly = False Then
                    Dim DispositionIndex As Integer
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        Debug.WriteLine(SoftSubs2(i))
                        If SoftSubs2(i) = DefaultSubCR Then
                            DispositionIndex = i
                        End If
                        Dim SoftSub As String() = VideoJson.Split(New String() {Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + ConvertCC(SoftSubs2(i)) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -i " + Chr(34) + SoftSub_3 + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -i " + Chr(34) + SoftSub_3 + Chr(34)
                        End If
                        SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (i + 1).ToString
                        If SoftSubMergeMetatata = Nothing Then
                            SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i)) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i)) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(SoftSubs2(i)) + Chr(34)
                        End If

                    Next
                    If DispositionIndex = Nothing Then
                    Else
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        Dim i2 As Integer = i
                        Me.Invoke(New Action(Function() As Object
                                                 Anime_Add.StatusLabel.Text = "Status: downloading subtitle file " + HardSubValuesToDisplay(SoftSubs2(i2))
                                                 Me.Text = "Status: downloading subtitle file " + HardSubValuesToDisplay(SoftSubs2(i2))
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                        Dim SoftSub As String() = VideoJson.Split(New String() {Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + ConvertCC(SoftSubs2(i)) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        'MsgBox(SoftSub_3)
                        Dim client0 As New WebClient
                        client0.Encoding = Encoding.UTF8
                        Dim str0 As String = client0.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + "." + HardSubValuesToDisplay(SoftSubs2(i)) + Path.GetExtension(Pfad3)), "ass")
                        'MsgBox(FN)
                        If i = 0 And IncludeLangName = False Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        File.WriteAllText(Pfad4, str0, Encoding.UTF8)
                        Pause(3)
                    Next

                End If

            End If
#End Region

#Region "m3u8 suche"

            If CBool(InStr(VideoJson, "audio_locale")) Then
                Dim CR_audio As String() = VideoJson.Split(New String() {"audio_locale" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim CR_audio2 As String() = CR_audio(1).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                CR_audio_locale = String.Join(" ", CR_audio2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)

            End If

            Dim CR_URI_Master As String = Nothing
            'If SubsOnly = False Then
            Dim ii As Integer = 0
            Dim CR_VideoJson As String() = VideoJson.Split(New String() {hls_type}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim CR_VideoJsonHardSubs As String() = CR_VideoJson(1).Split(New String() {"hardsub_locale" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Debug.WriteLine(LangNew)
            Debug.WriteLine(CR_VideoJsonHardSubs.Count.ToString)
            Dim hls_List As New List(Of String)
            For i As Integer = 0 To CR_VideoJsonHardSubs.Count - 1
                If CBool(InStr(CR_VideoJsonHardSubs(i), LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                    CR_URI_Master = CR_VideoJsonHardSubs(i).Replace(LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34), "").Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)(0)
                    Exit For
                End If
            Next

            If CR_URI_Master = Nothing Then
                Me.Invoke(New Action(Function() As Object
                                         ResoNotFoundString = VideoJson
                                         DialogTaskString = "Language_CR_Beta"
                                         ErrorDialog.ShowDialog()
                                         Return Nothing
                                     End Function))
                If UserCloseDialog = True Then
                    Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                Else
                    LangNew = ResoBackString
                    ResoBackString = Nothing
                    For i As Integer = 0 To CR_VideoJsonHardSubs.Count - 1
                        If CBool(InStr(CR_VideoJsonHardSubs(i), LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                            CR_URI_Master = CR_VideoJsonHardSubs(i).Replace(LangNew + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34), "").Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)(0)
                            Exit For
                        End If
                    Next
                End If
            End If

            CR_URI_Master = CR_URI_Master.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")

            If CBool(InStr(CR_URI_Master, "master.m3u8")) Then
                Me.Invoke(New Action(Function() As Object
                                         Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                         Me.Text = "Status: m3u8 found, looking for resolution"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
            Else
                Throw New System.Exception("Premium Episode")
            End If
            'Else
            '    Me.Invoke(New Action(Function() As Object
            '                             Anime_Add.StatusLabel.Text = "Status: Substitles only mode - skipped video"
            '                             Me.Text = "Status: Substitles only mode - skipped video"
            '                             Me.Invalidate()
            '                             Return Nothing
            '                         End Function))
            'End If
#End Region


#Region "lösche doppel download"

            Dim Pfad5 As String = Pfad2.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) And SubsOnly = False Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object

                                         Anime_Add.StatusLabel.Text = "Status: The file video already exists."
                                         Me.Text = "Status: The file video already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                    Catch ex As Exception
                    End Try
                Else
                    Grapp_RDY = True
                    Exit Sub
                End If

            End If
#End Region
            If SubsOnly = False Then

                If Reso = 42 And HybridMode = False Then
                    If MergeSubs = True Then
                        URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command_temp + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale)
                    Else
                        URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command_temp
                    End If
                    'MsgBox(URL_DL)
                Else


                    Dim client As New System.Net.WebClient
                    client.Encoding = Encoding.UTF8
                    'MsgBox(CR_URI_Master)
                    Dim str As String = client.DownloadString(CR_URI_Master)
                    'MsgBox(str)

                    If CBool(InStr(str, "x" + Reso.ToString + ",")) Then
                        Reso2 = "x" + Reso.ToString
                    Else
                        'MsgBox(str)
                        If CBool(InStr(str, ResoSave + ",")) Then
                            Reso2 = Reso2
                        Else
                            Me.Invoke(New Action(Function() As Object
                                                     DialogTaskString = "Resolution"
                                                     ResoNotFoundString = str
                                                     ErrorDialog.ShowDialog()
                                                     Return Nothing
                                                 End Function))


                            'MsgBox(ResoBackString)
                            If UserCloseDialog = True Then
                                Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                            Else
                                Reso2 = ResoBackString
                                ResoSave = ResoBackString
                            End If
                        End If
                    End If


                    Dim ffmpeg_url_1 As String() = str.Split(New String() {Reso2 + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim ffmpeg_url_3 As String() = Nothing
                    Dim ffmpeg_url_2 As String() = ffmpeg_url_1(1).Split(New [Char]() {Chr(34)})
                    ffmpeg_url_3 = ffmpeg_url_2(2).Split(New [Char]() {System.Convert.ToChar("#")})
                    Debug.WriteLine(CR_audio_locale)
                    If MergeSubs = True Then

                        Debug.WriteLine(ConvertCC(CR_audio_locale))
                        URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale)
                        'URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command

                    Else
                        URL_DL = "-i " + Chr(34) + ffmpeg_url_3(0).Trim() + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command_temp
                    End If

                End If

            End If
#Region "thumbnail"
            Dim thumbnail As String() = ObjectJson.Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + "}"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = "https://" + thumbnail2(0).Replace("\/", "/")
            Debug.WriteLine(thumbnail3)
#End Region
#Region "<li> constructor"
            Dim Subsprache3 As String = "none" 'HardSubValuesToDisplay(SubSprache2.Replace(Chr(34), ""))
            Dim ResoHTMLDisplay As String = Nothing
            If ResoBackString = Nothing Then
                ResoHTMLDisplay = Reso.ToString + "p"
            ElseIf DialogTaskString = "Language" Then
                ResoHTMLDisplay = Reso.ToString + "p"
            Else
                Dim ResoHTML As String() = ResoBackString.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                If ResoHTML.Count > 1 Then
                    ResoHTMLDisplay = ResoHTML(1) + "p"

                Else
                    ResoHTMLDisplay = ResoHTML(0) + "p"
                End If
            End If
            Dim L2Name As String = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName_Backup, "[^\w\\-]", " ")
            If Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = "[Auto]"
            ElseIf Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = Reso2
            End If
            Pfad_DL = Pfad2
            Dim L1Name_Split As String() = WebsiteURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + HardSubValuesToDisplay(CR_audio_locale)

            If SubsOnly = True Then
                URL_DL = "-i [Subtitles only]"
            End If
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Path.GetFileName(Pfad_DL.Replace(Chr(34), "")), L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail3, URL_DL, Pfad_DL)
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_title + " <br> " + CR_season_number + " " + CR_episode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
            'Form1.RichTextBox1.Text = My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->"
#End Region

            Grapp_RDY = True
            Me.Invoke(New Action(Function() As Object

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
            Grapp_RDY = True

            If CBool(InStr(ex.ToString, "Could not find the sub language")) Then
                MsgBox(Sub_language_NotFound + SubSprache)
            ElseIf CBool(InStr(ex.ToString, "RESOLUTION Not Found")) Then
                MsgBox(Resolution_NotFound)
            ElseIf CBool(InStr(ex.ToString, "Premium Episode")) Then
                MsgBox(Premium_Stream, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "System.UnauthorizedAccessException")) Then
                MsgBox(ErrorNoPermisson + vbNewLine + ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, Chr(34) + "UserAbort" + Chr(34))) Then
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            Else
                MsgBox(ex.ToString, MsgBoxStyle.Information)

            End If

        End Try
    End Sub

    Public Sub Get_VRV_Seasons(ByVal JsonUrl As String)


        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Text = Nothing
        Anime_Add.comboBox3.Text = Nothing
        Anime_Add.comboBox4.Text = Nothing
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = True
        Anime_Add.comboBox4.Enabled = True

        Dim SeasonJson As String = Nothing

        Try
            Using client As New WebClient()
                client.Encoding = System.Text.Encoding.UTF8
                client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                SeasonJson = client.DownloadString(JsonUrl)
            End Using
        Catch ex As Exception
            Debug.WriteLine("error- getting SeasonJson data")

        End Try

        Dim ParameterSplit() As String = JsonUrl.Split(New String() {"&Policy="}, System.StringSplitOptions.RemoveEmptyEntries)
        VRVMassParameters = ParameterSplit(1)

        VRVMass = SeasonJson

        Dim BaseURLBuilder() As String = JsonUrl.Split(New String() {"seasons?"}, System.StringSplitOptions.RemoveEmptyEntries)
        VRVMassBaseURL = BaseURLBuilder(0)



        Dim ser As JObject = JObject.Parse(SeasonJson)
        Dim data As List(Of JToken) = ser.Children().ToList


        For Each item As JProperty In data
            item.CreateReader()

            Select Case item.Name
                Case "items" 'each record is inside the entries array
                    For Each Entry As JObject In item.Values
                        Dim title As String = Entry("title").ToString

                        Anime_Add.ComboBox1.Items.Add(title)

                    Next

            End Select
        Next


        'Dim SeasonSplit() As String = SeasonJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        'For i As Integer = 1 To SeasonSplit.Count - 1
        '    Dim SeasonSplit2() As String = SeasonSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

        'Next



    End Sub

    Public Async Sub Download_VRV_Seasons()
        Try
            Dim ListOfEpisodes As New List(Of String)

            Dim EpisodeSplit() As String = VRVMassEpisodes.Split(New String() {Chr(34) + "id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To EpisodeSplit.Count - 1
                Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                ListOfEpisodes.Add("https://vrv.co/watch/" + EpisodeSplit2(0) + "/")
                Debug.WriteLine("https://vrv.co/watch/" + EpisodeSplit2(0) + "/")
            Next


            Dim First As Integer = 0
            Dim Last As Integer = 0

            If Anime_Add.comboBox4.SelectedIndex > Anime_Add.comboBox3.SelectedIndex Or Anime_Add.comboBox4.SelectedIndex = Anime_Add.comboBox3.SelectedIndex Then
                First = Anime_Add.comboBox3.SelectedIndex
                Last = Anime_Add.comboBox4.SelectedIndex
            ElseIf Anime_Add.comboBox3.SelectedIndex > Anime_Add.comboBox4.SelectedIndex Then
                First = Anime_Add.comboBox4.SelectedIndex
                Last = Anime_Add.comboBox3.SelectedIndex
            End If



            Dim Anzahl As Integer = Anime_Add.comboBox4.SelectedIndex - Anime_Add.comboBox3.SelectedIndex
            For i As Integer = First To Last
                For e As Integer = 0 To Integer.MaxValue
                    If Grapp_RDY = True Then
                        Try
                            Dim ItemFinshedCount As Integer = 0
                            For i2 As Integer = 0 To ListView1.Items.Count - 1
                                If ItemList(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            RunningDownloads = ListView1.Items.Count - ItemFinshedCount

                        Catch ex As Exception
                            RunningDownloads = ListView1.Items.Count
                        End Try

                        If RunningDownloads < MaxDL Then
                            Exit For
                        Else
                            'MsgBox(e)
                            Await Task.Delay(1000)
                        End If
                    Else
                        Await Task.Delay(5000)
                    End If
                Next
                If Anime_Add.Mass_DL_Cancel = False Then
                    b = True
                    Exit For
                    Grapp_Abord = True
                    'MsgBox("dl_abourd")
                End If
                If UseQueue = True Then
                    Anime_Add.ListBox1.Items.Add(ListOfEpisodes(i))
                    Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    Anime_Add.Add_Display.ForeColor = Color.Black

                Else
                    Grapp_RDY = False
                    b = False
                    Debug.WriteLine("b: " + b.ToString)
                    Navigate(ListOfEpisodes(i))
                End If


                Anime_Add.Add_Display.Text = (i - First + 1).ToString + " / " + (Last - First + 1).ToString
            Next


        Catch ex As Exception
            If Debug2 = True Then
                MsgBox(ex.ToString)
            End If
            Anime_Add.comboBox4.Items.Clear()
            Anime_Add.comboBox3.Items.Clear()
            Aktuell = 0.ToString
            Gesamt = 0.ToString

            Anime_Add.groupBox1.Visible = True
            Anime_Add.groupBox2.Visible = False
            Anime_Add.GroupBox3.Visible = False
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.btn_dl.Text = "Download" 'btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
    End Sub
#End Region


    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        If RunningDownloads > 0 Then
            If MessageBox.Show("Are you sure you want close the program and end all active downloads?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                For i As Integer = 0 To ListView1.Items.Count - 1
                    ItemList(i).KillRunningTask()
                Next

                RemoveTempFiles()
                Me.Close()
            End If
        Else

            Timer3.Enabled = False

            RemoveTempFiles()



            Me.Close()

        End If
    End Sub
    Private Sub RemoveTempFiles()
        Try
            Dim files() As String = System.IO.Directory.GetFiles(Application.StartupPath)
            For Each file As String In files
                If CBool(InStr(file, "CRD-Temp-File-")) Then
                    System.IO.File.Delete(file)
                End If

            Next
        Catch ex As Exception
        End Try
        If KeepCache = False Then
            Try
                Dim di As New System.IO.DirectoryInfo(Pfad)
                For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                    If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                    Else
                        If CBool(InStr(fi.Name, "CRD-Temp-File-")) Then
                            System.IO.Directory.Delete(fi.FullName, True)
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub RetryWithCachedFiles()

        Try
            Dim di As New System.IO.DirectoryInfo(Pfad)
            For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                Else
                    If CBool(InStr(fi.Name, "CRD-Temp-File-")) Then
                        If File.Exists(fi.FullName + "\Retry\retry.txt") Then
                            If MessageBox.Show("Cached data found, you can try to retry the download by pressing 'Yes'", "Retry?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                Dim L1Name As String = Nothing
                                Dim L2Name As String = Nothing
                                Dim ResoHTMLDisplay As String = Nothing
                                Dim Subsprache3 As String = Nothing
                                Dim thumbnail3 As String = "file:///" + fi.FullName + "/Retry/retry.jpg"
                                Dim Pfad2 As String = fi.FullName
                                Dim URL2 As String = Nothing
                                Dim Filename As String = Nothing

                                Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(fi.FullName + "\Retry\retry.txt")
                                Dim a As String

                                For i As Integer = 0 To 5
                                    a = reader.ReadLine
                                    If i = 0 Then
                                        URL2 = a
                                    ElseIf i = 1 Then
                                        L1Name = a
                                    ElseIf i = 2 Then
                                        L2Name = a
                                    ElseIf i = 3 Then
                                        ResoHTMLDisplay = a
                                    ElseIf i = 4 Then
                                        Subsprache3 = a
                                    ElseIf i = 5 Then
                                        Filename = Path.GetFileName(a.Replace(Chr(34), ""))
                                    End If

                                Next
                                reader.Close()
                                Me.Invoke(New Action(Function() As Object
                                                         ListItemAdd(Filename, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail3, URL2, Pfad2)
                                                         Return Nothing
                                                     End Function))
                                liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + L1Name + " <br> " + L2Name + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
                            Else
                                Grapp_non_cr_RDY = True
                                System.IO.Directory.Delete(fi.FullName, True)
                                Exit Sub
                            End If

                        Else
                            System.IO.Directory.Delete(fi.FullName, True)
                        End If


                    End If
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub


    Private Sub Btn_add_Click(sender As Object, e As EventArgs) Handles Btn_add.Click
        If Anime_Add.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            Anime_Add.WindowState = System.Windows.Forms.FormWindowState.Normal
        Else
            Anime_Add.Show()
        End If
    End Sub

    Private Sub Btn_Settings_Click(sender As Object, ByVal e As EventArgs) Handles Btn_Settings.Click

        Einstellungen.Show()
    End Sub


    Private Sub ToggleDebugModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToggleDebugModeToolStripMenuItem.Click
        If Debug2 = True Then
            Debug2 = False
            MsgBox("Debug Mode Disabled")
        Else
            Debug2 = True
            MsgBox("Debug Mode Enabled")
        End If
    End Sub

    Private Sub OpenSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenSettingsToolStripMenuItem.Click
        Einstellungen.Show()
    End Sub

    Private Sub Btn_Settings_DoubleClick(sender As Object, e As EventArgs) Handles Btn_Settings.DoubleClick
        Einstellungen.Close()

        If Debug1 = True Then
            If Debug2 = True Then
                Einstellungen.Close()
                Try
                    My.Computer.Clipboard.SetText(WebbrowserText)

                    MsgBox("webbrowser text copyed to the clipboard")
                Catch ex As Exception
                End Try
            Else
                Debug2 = True
                Einstellungen.Close()
                MsgBox("Debug activated")
            End If
        Else
            Debug1 = True
            Einstellungen.Close()
            'MsgBox("Debug activated")
        End If
    End Sub


    Private Sub Btn_Browser_Click(sender As Object, e As EventArgs) Handles Btn_Browser.Click
        If Application.OpenForms().OfType(Of CefSharp_Browser).Any = True Then
            CefSharp_Browser.Location = Me.Location
        End If
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        UserBowser = True
        CefSharp_Browser.Show()
    End Sub

    Public Function RemoveExtraSpaces(input_text As String) As String

        Dim rsRegEx As System.Text.RegularExpressions.Regex
        rsRegEx = New System.Text.RegularExpressions.Regex("\s+")

        Return rsRegEx.Replace(input_text, " ").Trim()

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            For s As Integer = 0 To ListView1.Items.Count - 1
                Dim r As Rectangle = ListView1.Items.Item(s).Bounds
                ItemList(s).SetBounds(r.X, r.Y, ListView1.Width, r.Height)
                ItemList(s).SetTheme(Manager.Theme)
                If ItemList(s).GetToDispose() = True Then
                    ItemList(s).DisposeItem(ItemList(s).GetToDispose())
                    ItemList.RemoveAt(s)
                    ListView1.Items.RemoveAt(s)
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

#Region "unused"




    'Public Shared Function GetPage(url As String) As String
    '    Try
    '        Dim ourUri As New Uri(url)
    '        Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(ourUri), HttpWebRequest)
    '        myHttpWebRequest.Timeout = 10000
    '        Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
    '        Return myHttpWebResponse.ResponseUri.ToString
    '        myHttpWebResponse.Close()
    '    Catch e As Exception
    '        'MsgBox(e.Message.ToString)
    '        Return url
    '    End Try
    'End Function
    Sub FFMPEGResoBack(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        If CBool(InStr(e.Data, ": Video:")) Then
            Dim ZeileReso() As String = e.Data.Split(New String() {" ["}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ZeileReso3() As String = e.Data.Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ZeileReso4() As String = ZeileReso3(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)
            ResoAvalibe = ResoAvalibe + vbNewLine + ZeileReso2(ZeileReso2.Count - 1).Trim + ":--:" + ZeileReso4(1)
        ElseIf CBool(InStr(e.Data, "At least one output file must be specified")) Then
            ResoSearchRunning = False
        End If
    End Sub

    Public Sub FFMPEG_Reso(ByVal DL_URL As String)
        ResoSearchRunning = True
        Dim proc As New Process
        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo

        Dim cmd As String = "-i " + Chr(34) + DL_URL + Chr(34) 'start ffmpeg with command strFFCMD string
        Dim ffmpegOutput As String = Nothing
        Dim ffmpegOutput2 As String = Nothing
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        AddHandler proc.ErrorDataReceived, AddressOf FFMPEGResoBack
        AddHandler proc.OutputDataReceived, AddressOf FFMPEGResoBack
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        'Dim ZeitAnzeige As String = Nothing
        'Dim StreamNR As String = Nothing
        ''Math.Abs()
        'Dim AllReso As String = "1080p720p480p360p"
        'Dim AllResoArry() As String = AllReso.Split(New String() {"p"}, System.StringSplitOptions.RemoveEmptyEntries)
        'Dim Zeilen() As String = ffmpegOutput.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
        'For i As Integer = 0 To Zeilen.Count - 1
        '    If CBool(InStr(Zeilen(i), "x" + Reso.ToString + " [") Then
        '        Dim ZeileReso() As String = Zeilen(i).Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
        '        Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)
        '        StreamNR = ZeileReso2(1)
        '    End If
        'Next

        'Return ZeitAnzeige + "#1" + StreamNR
    End Sub
#End Region

    Public Sub Grapp_non_CR()

        Dim ffmpeg_command_temp As String = ffmpeg_command
        If VideoFormat = ".aac" Then
            Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

            ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
        End If


        If NonCR_URL = Nothing Then Exit Sub
        Me.Invoke(New Action(Function() As Object
                                 Anime_Add.StatusLabel.Text = "Status: m3u8 found, trying to start the download"
                                 Me.Text = "Status: m3u8 found, trying to start the download"
                                 Me.Invalidate()
                                 Return Nothing
                             End Function))
        Grapp_non_cr_RDY = False
        For i As Integer = 0 To 30
            If ResoSearchRunning = True Then
                Pause(1)
            Else
                Exit For
            End If
        Next
        If Debug2 = True Then
            MsgBox(ResoSearchRunning.ToString)
        End If
        Dim Video_Title As String = WebbrowserTitle.Replace(" - Watch on VRV", "").Replace("Free Streaming", "").Replace("Tubi", "")
        Video_Title = RemoveExtraSpaces(Video_Title)
#Region "Name + Pfad"
        Dim Video_FilenName As String = Video_Title
        Video_FilenName = String.Join(" ", Video_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(Video_FilenName, "[^\w\\-]", " ")
        Video_FilenName = RemoveExtraSpaces(Video_FilenName + VideoFormat)
        Pfad_DL = Chr(34) + Pfad + "\" + Video_FilenName + Chr(34)
#End Region

#Region "thumbnail"
        Dim thumbnail As String() = Nothing
        Dim thumbnail2 As String() = Nothing
        Dim thumbnail4 As String = "None, will usese fail image"
        Try
            If CBool(InStr(WebbrowserText, "thumbnail")) Then
                thumbnail = WebbrowserText.Split(New String() {"thumbnail"}, System.StringSplitOptions.RemoveEmptyEntries)
            End If
        Catch ex As Exception

        End Try
        Try
            For i As Integer = 0 To thumbnail.Count - 1
                If CBool(InStr(thumbnail(i), ".jpg")) Then
                    If CBool(InStr(thumbnail(i), "https:")) Then
                        thumbnail2 = thumbnail(i).Split(New String() {".jpg"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim thumbnail3 As String() = thumbnail2(0).Split(New String() {"https:"}, System.StringSplitOptions.RemoveEmptyEntries)
                        thumbnail4 = "https:" + thumbnail3(thumbnail3.Count - 1).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\/", "/") + ".jpg"
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
        End Try
#End Region


#Region "lösche doppel download"

        Dim Pfad5 As String = Path.Combine(Pfad + Video_FilenName)
        If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
            If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                Try
                    My.Computer.FileSystem.DeleteFile(Pfad5)
                Catch ex As Exception
                End Try
            Else
                Grapp_non_cr_RDY = True
                Exit Sub
            End If

        End If
#End Region
        URL_DL = NonCR_URL.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/") 'hls_List.Item(i2).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/")
#Region "<li> constructor"
        Dim Subsprache3 As String = "undefined" '
        Dim ResoHTMLDisplay As String = "[Auto]"
        Dim L2Name As String = Video_Title
        Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim L1Name As String = L1Name_Split(1)
        Pfad_DL = Chr(34) + Pfad + "\" + Video_FilenName + Chr(34)

        ResoHTMLDisplay = "[Auto]"

        Dim cmd As String = "-i " + Chr(34) + URL_DL + Chr(34) + " " + ffmpeg_command_temp
        Me.Invoke(New Action(Function() As Object
                                 ListItemAdd(Pfad_DL, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail4, cmd, Pfad_DL)
                                 Return Nothing
                             End Function))

#End Region

        Grapp_non_cr_RDY = True
        Me.Invoke(New Action(Function() As Object

                                 Anime_Add.StatusLabel.Text = "Status: idle"
                                 Me.Text = "Crunchyroll Downloader"
                                 Me.Invalidate()
                                 Return Nothing
                             End Function))

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim ItemFinshedCount As Integer = 0
            For i As Integer = 0 To ListView1.Items.Count - 1
                If ItemList(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next
            RunningDownloads = ListView1.Items.Count - ItemFinshedCount
            If RunningDownloads > 0 Then
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_CONTINUOUS)
            Else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
            End If
        Catch ex As Exception
            RunningDownloads = ListView1.Items.Count
        End Try
        'FontLabel2.Text = RunningDownloads.ToString
        'Debug.WriteLine("downloads.tick: " + RunningDownloads.ToString)
    End Sub


    Public Sub Funitmation_Grapp()

        Try
            Dim ffmpeg_command_temp As String = ffmpeg_command
            If VideoFormat = ".aac" Then
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
            End If

            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Status: looking for video file"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

            Funimation_Grapp_RDY = False
#Region "Name"

            Dim DownloadPfad As String = Nothing
            Dim FunimationSeason As String = Nothing
            Dim FunimationEpisode As String = Nothing
            Dim FunimationTitle As String = Nothing
            Dim FunimationEpisodeTitle As String = Nothing
            Dim FunimationDub As String = Nothing
            Dim FunimationAudioMap As String = Nothing

            Dim FunimationSeason1() As String = WebbrowserText.Split(New String() {"seasonNum: "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationSeason2() As String = FunimationSeason1(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            If Season_Prefix = "[default season prefix]" Then
                FunimationSeason = "Season " + FunimationSeason2(0)
            Else
                FunimationSeason = Season_Prefix + FunimationSeason2(0)
            End If

            Dim FunimationEpisode1() As String = WebbrowserText.Split(New String() {"episodeNum: "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationEpisode2() As String = FunimationEpisode1(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            If Episode_Prefix = "[default episode prefix]" Then
                FunimationEpisode = "Episode " + FunimationEpisode2(0)
            Else
                FunimationEpisode = Episode_Prefix + FunimationEpisode2(0)
            End If

            Dim FunimationTitle1() As String = WebbrowserText.Split(New String() {".showName = '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationTitle2() As String = FunimationTitle1(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationTitle = String.Join(" ", FunimationTitle2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(FunimationTitle2(0), "[^\w\\-]", " ").Trim(" ")
            FunimationTitle = RemoveExtraSpaces(FunimationTitle)

            Dim FunimationDub1() As String = WebbrowserText.Split(New String() {".showLanguage =  '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationDub2() As String = FunimationDub1(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationDub = FunimationDub2(0)

            Dim FunimationEpisodeTitle1() As String = WebbrowserText.Split(New String() {".videoTitle = '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationEpisodeTitle2() As String = FunimationEpisodeTitle1(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationEpisodeTitle2(0) = HtmlDecode(FunimationEpisodeTitle2(0))
            FunimationEpisodeTitle = String.Join(" ", FunimationEpisodeTitle2(0).Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(FunimationEpisodeTitle2(0), "[^\w\\-]", " ").Trim(" ")
            FunimationEpisodeTitle = RemoveExtraSpaces(FunimationEpisodeTitle)

            Dim DefaultName As String = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)

            'If CR_NameMethode = 0 Then
            '    DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)
            'Else
            If CR_NameMethode = 1 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisodeTitle)
            ElseIf CR_NameMethode = 2 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode + " " + FunimationEpisodeTitle)
            ElseIf CR_NameMethode = 3 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationEpisodeTitle + " " + FunimationSeason + " " + FunimationEpisode)
            End If

            DefaultName = DefaultName.Replace("&#x27;", "'")

            'Dim DefaultPath As String = Pfad + "\" + DefaultName + VideoFormat
            'DefaultPath = DefaultPath.Replace("\\", "\")
#End Region

#Region "Pfad"
            Dim TextBox2_Text As String = Nothing

            Me.Invoke(New Action(Function() As Object
                                     TextBox2_Text = Anime_Add.textBox2.Text

                                     Return Nothing
                                 End Function))

            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then

            Else
                Me.Invoke(New Action(Function() As Object

                                         Return Nothing
                                     End Function))
            End If



            DownloadPfad = UseSubfolder(FunimationTitle, FunimationSeason, Pfad)

            If Not Directory.Exists(Path.GetDirectoryName(DownloadPfad)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(DownloadPfad))
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    DownloadPfad = Pfad '+ "\" + DefaultName + VideoFormat
                End Try
            End If

            DownloadPfad = DownloadPfad + DefaultName + VideoFormat


#Region "lösche doppel download"

            Dim Pfad5 As String = DownloadPfad.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: File already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: Old file overwritten."
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))

                    Catch ex As Exception
                    End Try
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Crunchyroll Downloader"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))

                    Funimation_Grapp_RDY = True
                    Exit Sub
                End If

            End If
#End Region

#End Region
#Region "m3u8 URL"
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            Dim Funimation_m3u8_final As String = Nothing
            Dim Funimation_iFrame As String = Nothing
            If CBool(InStr(WebbrowserText, My.Resources.Funimation_Player_ID)) Then
                Funimation_iFrame = My.Resources.Funimation_Player_ID
            ElseIf CBool(InStr(WebbrowserText, My.Resources.Funimation_Player_ID_2)) Then
                Funimation_iFrame = My.Resources.Funimation_Player_ID_2
            End If
            Dim Player_ID() As String = WebbrowserText.Split(New String() {Funimation_iFrame}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Player_ID2() As String = Player_ID(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            If SubsOnly = False Then

                Me.Invoke(New Action(Function() As Object
                                         '    Anime_Add.StatusLabel.Text = iFrameURL

                                         Return Nothing
                                     End Function))




                If Not WebbrowserCookie = Nothing Then
                    client0.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                ElseIf Not SystemWebBrowserCookie = Nothing Then
                    client0.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                End If
                Dim str0 As String = client0.DownloadString("https://www.funimation.com/api/showexperience/" + Player_ID2(0) + "/?pinst_id=fzQc9p9f")


                Dim Funimation_m3u8() As String = str0.Split(New String() {My.Resources.Funimation_src_string}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Funimation_m3u8_Main As String = Nothing
                For i As Integer = 0 To Funimation_m3u8.Count - 1
                    If CBool(InStr(Funimation_m3u8(i), "m3u8?")) Then
                        Dim Funimation_m3u8_split() As String = Funimation_m3u8(i).Split(New String() {", "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Funimation_m3u8_Main = Funimation_m3u8_split(0)
                        Exit For
                    End If
                Next
                If Funimation_m3u8_Main = Nothing Then

                    If MessageBox.Show("No media found in:" + vbNewLine + str0, "No media", MessageBoxButtons.RetryCancel) = DialogResult.Retry Then
                        Me.Invoke(New Action(Function() As Object
                                                 Navigate(WebbrowserURL)
                                                 Try
                                                     Anime_Add.StatusLabel.Text = "retrying Funimation"
                                                     Me.Text = "retrying Funimation"
                                                     Me.Invalidate()
                                                 Catch ex As Exception
                                                 End Try
                                                 Return Nothing
                                             End Function))
                        Exit Sub
                    Else
                        Funimation_Grapp_RDY = True
                        Exit Sub
                    End If

                End If
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: Video found!"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

                Dim str1 As String = client0.DownloadString(Funimation_m3u8_Main.Replace(Chr(34), ""))

                If CBool(InStr(str1, "# AUDIO groups")) Then
                    Dim FunimationAudio() As String = str1.Split(New String() {"# AUDIO groups"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio2() As String = FunimationAudio(1).Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio3() As String = FunimationAudio2(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    FunimationAudioMap = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + FunimationAudio3(0) + Chr(34)
                End If

                Dim Streams() As String = str1.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)


                'MsgBox(Funimation_m3u8_Main)
                Dim FunimationBackupm3u8 As String = Nothing
                For i As Integer = 0 To Streams.Length - 1

                    Try

                        If CBool(InStr(Streams(i), "x" + Reso.ToString)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim

                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8
                            If Not WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                            ElseIf Not SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                            End If

                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            'MsgBox(m3u8String)
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)

                            If CBool(InStr(keyfileurl2(0), "https://")) Then

                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host

                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If

                            'MsgBox(keyfileurl3)
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                Funimation_m3u8_final = StreamURL
                                FunimationBackupm3u8 = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + vbNewLine + ex.ToString)
                            End Try
                        ElseIf CBool(InStr(Streams(i), ResoFunBackup)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim

                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8
                            If Not WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                            ElseIf Not SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                            End If
                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)

                            If CBool(InStr(keyfileurl2(0), "https://")) Then

                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host

                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If

                            'MsgBox(keyfileurl3)
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                FunimationBackupm3u8 = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + vbNewLine + ex.ToString)
                            End Try

                        End If

                    Catch ex As Exception

                    End Try
                Next


                If Funimation_m3u8_final = Nothing And FunimationBackupm3u8 = Nothing Then

                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Resolution not found!"
                                             Me.Invalidate()
                                             DialogTaskString = "Funimation_Resolution"
                                             ResoNotFoundString = str1
                                             ErrorDialog.ShowDialog()
                                             Return Nothing
                                         End Function))
                    ResoFunBackup = ResoBackString
                    For i As Integer = 0 To Streams.Length - 1
                        If CBool(InStr(Streams(i), ResoBackString)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim
                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8

                            If Not WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                            ElseIf Not SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                            End If

                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            'MsgBox(textLenght(i))
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)
                            If CBool(InStr(keyfileurl2(0), "https://")) Then
                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host

                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                Funimation_m3u8_final = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + ex.ToString)
                            End Try


                            'Funimation_m3u8_final = textLenght(i)
                            'Exit For

                        End If
                    Next
                ElseIf Funimation_m3u8_final = Nothing Then
                    Funimation_m3u8_final = FunimationBackupm3u8
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Resolution found!"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))

                End If
            Else
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: Substitles only mode - skipped video"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
            End If
            'MsgBox(FunimationName3)
            'MsgBox(Funimation_m3u8_final)
#Region "thumbnail"

            Dim thumbnail As String() = WebbrowserHeadText.Split(New String() {My.Resources.Funimation_thumbnail}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = thumbnail2(0) '.Replace("\/", "/")
#End Region
            Dim ResoHTMLDisplay As String = Reso.ToString + "p"

#Region "Subs"


            Dim SubsClient As New WebClient
            SubsClient.Encoding = Encoding.UTF8
            If Not WebbrowserCookie = Nothing Then
                SubsClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
            ElseIf Not SystemWebBrowserCookie = Nothing Then
                SubsClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
            End If
            Dim PlayerPage As String = SubsClient.DownloadString("https://www.funimation.com/player/" + Player_ID2(0) + "/?bdub=0&qid=")

            Dim Subs_in_srt As New List(Of String)
            Dim Subs_in_vtt As New List(Of String)

            Dim SoftSubs2 As New List(Of String)

            If SubFunimation.Count > 0 Then
                For i As Integer = 0 To SubFunimation.Count - 1
                    If CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String + SubFunimation(i))) Then
                        SoftSubs2.Add(My.Resources.Funimation_Subtitle_String + SubFunimation(i))
                        Continue For
                    ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String2 + SubFunimation(i))) Then
                        SoftSubs2.Add(My.Resources.Funimation_Subtitle_String2 + SubFunimation(i))
                    ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String3 + SubFunimation(i))) Then
                        SoftSubs2.Add(My.Resources.Funimation_Subtitle_String3 + SubFunimation(i))
                    End If

                Next
                If SoftSubs2.Count = 0 Then
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "No Subtitles found..."
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))
                    File.WriteAllText(DownloadPfad.Replace(VideoFormat, "-subtitle_error.log"), PlayerPage, Encoding.UTF8)

                End If

            End If


            Dim HardSubFound As Boolean = False
            Dim HardSubSplittString As String = Nothing
            Dim UsedSub As String = Nothing
            Dim UsedSubs As New List(Of String)
            Dim ffmpeg_hardsub As String = Nothing

            If CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String + HardSubFunimation)) Then
                HardSubFound = True
                HardSubSplittString = My.Resources.Funimation_Subtitle_String + HardSubFunimation
            ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String2 + HardSubFunimation)) Then
                HardSubFound = True
                HardSubSplittString = My.Resources.Funimation_Subtitle_String2 + HardSubFunimation
            ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String3 + HardSubFunimation)) Then
                HardSubFound = True
                HardSubSplittString = My.Resources.Funimation_Subtitle_String3 + HardSubFunimation
            End If

            If HardSubFound = True Then 'anyways not true if hardsub is "Disabled"


                If CBool(InStr(ffmpeg_command, "-c copy")) Then
                    ffmpeg_hardsub = "-bsf:a aac_adtstoasc"
                Else
                    ffmpeg_hardsub = ffmpeg_command
                End If
                'MsgBox(HardSubSplittString)
                Dim HardSubTitle() As String = PlayerPage.Split(New String() {HardSubSplittString}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To HardSubTitle.Count - 1
                    Dim HardSubTitle2() As String = HardSubTitle(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    If CBool(InStr(HardSubTitle2(HardSubTitle2.Count - 1), ".srt")) Then
                        UsedSub = HardSubTitle2(HardSubTitle2.Count - 1)
                        Exit For
                    ElseIf CBool(InStr(HardSubTitle2(HardSubTitle2.Count - 1), ".vtt")) Then
                        UsedSub = HardSubTitle2(HardSubTitle2.Count - 1)
                        Exit For
                    End If

                Next

                If UsedSub = Nothing Then
                    Throw New System.Exception("Error - No valid Subtitle for hard-subtiles found")
                End If
                'MsgBox(UsedSub)
                Dim SubText As String = client0.DownloadString(UsedSub)
                Dim SubtitelFormat As String = ".srt"
                If CBool(InStr(UsedSub, ".vtt")) Then
                    SubtitelFormat = ".vtt"
                End If
                UsedSub = GeräteID() + SubtitelFormat
                File.WriteAllText(Application.StartupPath + "\" + UsedSub, SubText, Encoding.UTF8)
            ElseIf SoftSubs2.Count > 0 Then
                For i As Integer = 0 To SoftSubs2.Count - 1
                    Dim SubTitle() As String = PlayerPage.Split(New String() {SoftSubs2.Item(i)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FoundCount As Integer = 0
                    For ii As Integer = 0 To SubTitle.Count - 1
                        Dim SubTitle2() As String = SubTitle(ii).Split(New String() {My.Resources.Funimation_subs_src}, System.StringSplitOptions.RemoveEmptyEntries)
                        For iii As Integer = 0 To SubTitle2.Count - 1
                            If CBool(InStr(SubTitle2(iii), ".srt" + Chr(34))) Then
                            ElseIf CBool(InStr(SubTitle2(iii), ".vtt" + Chr(34))) Then
                            ElseIf CBool(InStr(SubTitle2(iii), ".srt")) Then
                                If Subs_in_srt.Contains(SubTitle2(iii)) Then
                                Else
                                    Subs_in_srt.Add(SubTitle2(iii))
                                End If
                            ElseIf CBool(InStr(SubTitle2(iii), ".vtt")) Then
                                If Subs_in_vtt.Contains(SubTitle2(iii)) Then
                                Else
                                    Subs_in_vtt.Add(SubTitle2(iii))
                                End If


                            End If
                        Next

                    Next
                    Dim TempCount As Integer = UsedSubs.Count
                    Try
                        If Funimation_srt = True Then
                            UsedSubs.Add(Subs_in_srt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If Funimation_vtt = True Then
                            UsedSubs.Add(Subs_in_vtt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        End If
                    Catch ex As Exception
                    End Try




                    If TempCount = UsedSubs.Count Then
                        If Subs_in_srt.Count > 0 Then
                            UsedSubs.Add(Subs_in_srt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        ElseIf Subs_in_vtt.Count > 0 Then
                            UsedSubs.Add(Subs_in_vtt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        End If
                    End If

                    Subs_in_srt.Clear()
                    Subs_in_vtt.Clear()


                Next
            End If

            '
            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            If Not FunimationAudioMap = Nothing Then
                SoftSubMergeMaps = " -map 0:v -map 1:a"
            End If
            Dim SoftSubMergeMetatata As String = Nothing

            If UsedSubs.Count > 0 Then
                If MergeSubs = True And SubsOnly = False Then
                    Dim DispositionIndex As Integer = 999
                    Dim LastMerged As String = Nothing
                    Dim MapCount As Integer = -1
                    For i As Integer = 0 To UsedSubs.Count - 1
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        If CCtoMP4CC(SoftSub(1)) = LastMerged Then
                            Continue For
                        Else
                            LastMerged = CCtoMP4CC(SoftSub(1))
                        End If
                        MapCount = MapCount + 1
                        If DefaultSubFunimation = SoftSub(1) Then
                            'Debug.WriteLine(SoftSub(1))
                            DispositionIndex = MapCount
                        End If
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        End If

                        If FunimationAudioMap = Nothing Then
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 1).ToString
                        Else
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 2).ToString
                        End If

                        If SoftSubMergeMetatata = Nothing Then
                            'SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSub(1))
                            SoftSubMergeMetatata = " -metadata:s:s:" + MapCount.ToString + " language=" + CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + MapCount.ToString + " language=" + CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                            'SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i))


                        End If

                    Next
                    If DispositionIndex < 999 Then
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    For i As Integer = 0 To UsedSubs.Count - 1
                        LabelUpdate = "Status: downloading subtitle file"
                        LabelEpisode = UsedSubs(i)
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_3 As String = SoftSub(0).Replace("\/", "/")
                        Dim Subfile As String = SubsClient.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = DownloadPfad.Replace(Chr(34), "")
                        'MsgBox(FN)
                        Dim SubtitelFormat As String = "srt"
                        If CBool(InStr(SoftSub_3, ".vtt")) Then
                            SubtitelFormat = "vtt"
                        End If
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSub(1) + Path.GetExtension(Pfad3)), SubtitelFormat)

                        If i = 0 Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), SubtitelFormat)
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        File.WriteAllText(Pfad4, Subfile, Encoding.UTF8)
                        Pause(1)
                    Next

                End If
            End If

#End Region

#Region "ffmpeg command"

            Dim DubMetatata As String = Nothing
            If FunimationDub = "japanese" Then
                DubMetatata = " -metadata:s:a:0 language=jpn"

            ElseIf FunimationDub = "portuguese-brazil" Then
                DubMetatata = " -metadata:s:a:0 language=por"

            ElseIf FunimationDub = "spanish-latin-am" Then
                DubMetatata = " -metadata:s:a:0 language=spa"

            Else '
                DubMetatata = " -metadata:s:a:0 language=eng"

            End If

            If HardSubFound = True And CBool(InStr(VideoFormat, ".aac")) = False Then
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + " -vf subtitles=" + Chr(34) + UsedSub + Chr(34) + " " + ffmpeg_hardsub

            ElseIf MergeSubs = True Then

                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + DubMetatata

            ElseIf CBool(InStr(VideoFormat, ".aac")) = True Then
                If FunimationAudioMap = Nothing Then
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command_temp
                Else
                    Funimation_m3u8_final = FunimationAudioMap.Replace(" -headers " + My.Resources.ffmpeg_user_agend + " ", "") + DubMetatata + " " + ffmpeg_command_temp
                End If

            Else

                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + DubMetatata + " " + ffmpeg_command_temp

            End If



#End Region
            'MsgBox(Funimation_m3u8_final)
            'DownloadPfad = DownloadPfad.Replace(" \", "\")
            If SubsOnly = True Then
                Funimation_m3u8_final = "-i [Subtitles only]"
            End If
            DownloadPfad = RemoveExtraSpaces(DownloadPfad)
            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Pfad_DL, L1Name, DefaultName, ResoHTMLDisplay, "Unknown", SubValuesToDisplay(), thumbnail3, Funimation_m3u8_final, Chr(34) + DownloadPfad + Chr(34), "FM")
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + FunimationTitle + " <br> " + FunimationSeason + " " + FunimationEpisode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + DefaultName + "-->")

#End Region
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader!"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

            MsgBox(ex.ToString)
        End Try
        Funimation_Grapp_RDY = True
    End Sub

#Region "Funimation JS "
    Public Sub GetFunimationJS_Seasons(Optional ByVal JsonUrl As String = Nothing, Optional ByVal Json As String = Nothing)

        Dim SeasonJson As String = Nothing
        Debug.WriteLine("JsonUrl: " + JsonUrl)
        If JsonUrl = Nothing Then
            SeasonJson = Json
        Else
            FunimationSeasonAPIUrl = JsonUrl
            'Navigate(JsonUrl)
            'FunimationJsonBrowser = "SeasonJson"
            'Exit Sub

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    SeasonJson = client.DownloadString(JsonUrl)


                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting funimation SeasonJson data")
                FunimationJsonBrowser = "SeasonJson"
                Navigate(JsonUrl)
                'Navigate(JsonUrl)
                Exit Sub
            End Try
        End If

        Debug.WriteLine("SeasonJson: " + SeasonJson)

        Dim ser As JObject = JObject.Parse(SeasonJson)
        Dim data As List(Of JToken) = ser.Children().ToList
        Dim Slug As String = Nothing
        Dim Title As String = Nothing
        Dim ID As String = Nothing

        For Each item As JProperty In data
            item.CreateReader()
            'MsgBox(item.Name)
            Select Case item.Name
                Case "slug"
                    Slug = item.Value.ToString

                Case "index" 'each record is inside the entries array

                    Dim SubData2 As List(Of JToken) = item.Values("seasons").Children().ToList

                    For i As Integer = 0 To SubData2.Count - 1
                        Dim SubItem As JToken = SubData2.Item(i)
                        Dim SeasonSubData As List(Of JToken) = SubItem.Children().ToList

                        For Each SeasonSubItem As JProperty In SeasonSubData
                            SeasonSubItem.CreateReader()
                            Select Case SeasonSubItem.Name
                                Case "contentId"
                                    'MsgBox(SeasonSubItem.Value.ToString)
                                    ID = SeasonSubItem.Value.ToString
                                Case "title"
                                    ' MsgBox(SeasonSubItem.Value.Item("en").ToString)
                                    Title = SeasonSubItem.Value.Item("en").ToString
                                    FunimtaionSeasonList.Add(New FunimationOverview(Slug, ID, Title))
                            End Select
                        Next
                    Next
            End Select



        Next

        Debug.WriteLine("SeasonJson: ")

        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Text = Nothing
        Anime_Add.comboBox3.Text = Nothing
        Anime_Add.comboBox4.Text = Nothing
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = False
        Anime_Add.comboBox4.Enabled = False
        WebbrowserURL = "https://funimation.com/js"

        Debug.WriteLine("Count: " + FunimtaionSeasonList.Count.ToString)
        For i As Integer = 0 To FunimtaionSeasonList.Count - 1
            Debug.WriteLine(FunimtaionSeasonList.Item(i).Title)
            Anime_Add.ComboBox1.Items.Add(FunimtaionSeasonList.Item(i).Title)
        Next

    End Sub

    Public Sub GetFunimationJS_SeasonsOld(Optional ByVal JsonUrl As String = Nothing, Optional ByVal Json As String = Nothing)


        FunimtaionAPISeasonID.Clear()

        Dim SeasonJson As String = Nothing
        Debug.WriteLine("JsonUrl: " + JsonUrl)
        If JsonUrl = Nothing Then
            SeasonJson = Json
        Else
            'Navigate(JsonUrl)
            'FunimationJsonBrowser = "SeasonJson"
            'Exit Sub

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    SeasonJson = client.DownloadString(JsonUrl)


                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting funimation SeasonJson data")
                FunimationJsonBrowser = "SeasonJson"
                Navigate(JsonUrl)
                'Navigate(JsonUrl)
                Exit Sub
            End Try
        End If

        Debug.WriteLine("SeasonJson: " + SeasonJson)

        SeasonJson = SeasonJson.Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; white-space: pre-wrap;" + Chr(34) + ">", "")

        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Text = Nothing
        Anime_Add.comboBox3.Text = Nothing
        Anime_Add.comboBox4.Text = Nothing
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = False
        Anime_Add.comboBox4.Enabled = False



        Try



            Dim ser As JObject = JObject.Parse(SeasonJson)
            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "seasons" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Dim name As String = Entry("name").ToString
                            Anime_Add.ComboBox1.Items.Add(name)
                            'Debug.WriteLine(name)
                            Dim id As String = Entry("id").ToString
                            FunimtaionAPISeasonID.Add(id)

                        Next

                End Select
            Next

            WebbrowserURL = "https://funimation.com/js"
        Catch ex As Exception
            MsgBox("fatal error", MsgBoxStyle.Critical)
        End Try
    End Sub


    Public Async Sub DownloadFunimationJS_Seasons()
        Try
#Region "JS"
            Debug.WriteLine("EpisodeJson: " + FunimationEpisodeJSON)
            Anime_Add.Add_Display.Text = "preparing ...."
            Dim ListOfEpisodes As New List(Of String)
            Dim BaseURL As String = "https://www.funimation.com/v/" + FunimtaionSeasonList.Item(0).Slug + "/"
            'If FunimationRegion IsNot Nothing Then

            '    BaseURL = "https://www.funimation.com/" + FunimationRegion + "/shows/"
            'Else

            '    BaseURL = "https://www.funimation.com/en/shows/"
            'End If

            'Dim EpisodeSplit() As String = FunimationEpisodeJSON.Split(New String() {Chr(34) + "slug" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            'For i As Integer = 1 To EpisodeSplit.Count - 1
            '    Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            '    Debug.WriteLine(BaseURL + FunimationShowPath + EpisodeSplit2(0))
            '    ListOfEpisodes.Add(BaseURL + FunimationShowPath + EpisodeSplit2(0)) '+ FunimationAPIRegion)

            'Next 


            Dim ser As JObject = JObject.Parse(FunimationEpisodeJSON)
            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "episodes" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Dim slug As String = Entry("slug").ToString

                            'Debug.WriteLine(BaseURL + FunimationShowPath + slug)
                            'ListOfEpisodes.Add(BaseURL + FunimationShowPath + slug) '+ FunimationAPIRegion)

                            Debug.WriteLine(BaseURL + slug)
                            ListOfEpisodes.Add(BaseURL + slug)


                        Next

                End Select
            Next



            Dim First As Integer = 0
            Dim Last As Integer = 0

            If Anime_Add.comboBox4.SelectedIndex > Anime_Add.comboBox3.SelectedIndex Then
                First = Anime_Add.comboBox3.SelectedIndex
                Last = Anime_Add.comboBox4.SelectedIndex
            ElseIf Anime_Add.comboBox4.SelectedIndex = Anime_Add.comboBox3.SelectedIndex Then

                Exit Sub
            Else
                First = Anime_Add.comboBox4.SelectedIndex
                Last = Anime_Add.comboBox4.SelectedIndex
            End If



            Dim Anzahl As Integer = Anime_Add.comboBox4.SelectedIndex - Anime_Add.comboBox3.SelectedIndex
            For i As Integer = First To Last
                For e As Integer = 0 To Integer.MaxValue
                    If Funimation_Grapp_RDY = True Then
                        Try
                            Dim ItemFinshedCount As Integer = 0
                            For i2 As Integer = 0 To ListView1.Items.Count - 1
                                If ItemList(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            RunningDownloads = ListView1.Items.Count - ItemFinshedCount

                        Catch ex As Exception
                            RunningDownloads = ListView1.Items.Count
                        End Try

                        If RunningDownloads < MaxDL Then
                            Exit For
                        Else
                            'MsgBox(e)
                            Await Task.Delay(1000)
                        End If
                    Else
                        Await Task.Delay(5000)
                    End If
                Next
                If Anime_Add.Mass_DL_Cancel = False Then
                    b = True
                    Exit For
                    Grapp_Abord = True
                    'MsgBox("dl_abourd")
                End If
                If UseQueue = True Then
                    Anime_Add.ListBox1.Items.Add(ListOfEpisodes(i))
                    Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    Anime_Add.Add_Display.ForeColor = Color.Black

                Else
                    Funimation_Grapp_RDY = False
                    b = False
                    Navigate(ListOfEpisodes(i))
                End If


                Anime_Add.Add_Display.Text = (i - First + 1).ToString + " / " + (Last - First + 1).ToString
            Next

#End Region
        Catch ex As Exception
            If Debug2 = True Then
                MsgBox(ex.ToString)
            End If
            Anime_Add.comboBox4.Items.Clear()
            Anime_Add.comboBox3.Items.Clear()
            Aktuell = 0.ToString
            Gesamt = 0.ToString

            Anime_Add.groupBox1.Visible = True
            Anime_Add.groupBox2.Visible = False
            Anime_Add.GroupBox3.Visible = False
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        FunimationEpisodeJSON = Nothing
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
    End Sub

    Private Function ConvertFunimationDub(ByVal Dub As String) As String
        If Dub = "english" Then
            Return "English"
        ElseIf Dub = "spanish(Mexico)" Then
            Return "Spanish (Latin Am)"
        ElseIf Dub = "portuguese(Brazil)" Then
            Return "Portuguese (Brazil)"
        ElseIf Dub = "japanese" Then
            Return "Japanese"
        Else
            Return "N/A"
        End If
    End Function
    Private Function ConvertFunimationDubToJson(ByVal Dub As String) As String
        If Dub = "english" Then
            Return "en"
        ElseIf Dub = "spanish(Mexico)" Then
            Return "es"
        ElseIf Dub = "portuguese(Brazil)" Then
            Return "pt"
        ElseIf Dub = "japanese" Then 'japanese
            Return "ja"
        Else
            Return "N/A"
        End If
    End Function
    Private Function ConvertJsonToFunimationDub(ByVal Dub As String) As String
        If Dub = "en" Then
            Return "english"
        ElseIf Dub = "es" Then
            Return "spanish(Mexico)"
        ElseIf Dub = "pt" Then
            Return "portuguese(Brazil)"
        ElseIf Dub = "ja" Then
            Return "japanese"
        Else
            Return "N/A"
        End If
    End Function
    Public Sub GetFunimationNewJS_VideoProxy(Optional ByVal v1JsonURL As String = Nothing, Optional ByVal v1JsonData As String = Nothing)


        Dim Collector As New TaskCookieVisitor
        Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
        CM.VisitAllCookies(Collector)

        Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
        Dim Cookie As String = ""
        For i As Integer = 0 To list.Count - 1

            If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
            End If

            If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
                'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                FunimationToken = "Token " + list.Item(i).Value
            End If
            If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "region")) Then 'list.Item(i).Domain = "funimation.com" Then
                'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                FunimationDeviceRegion = "?deviceType=web&" + list.Item(i).Name + "=" + list.Item(i).Value
            End If
        Next
        ' region=US;

        LoadedUrls.Clear()
        Dim Evaluator = New Thread(Sub() Me.GetFunimationNewJS_Video(v1JsonURL, v1JsonData))
        Evaluator.Start()


    End Sub
    Public Sub GetFunimationNewJS_Video(ByVal v1JsonUrl As String, ByVal v1JsonData As String) ', ByVal WebsiteURL As String
        Debug.WriteLine(v1JsonUrl)
        Dim v1Json As String = Nothing

        If v1JsonUrl = Nothing Then
            v1Json = v1JsonData
        Else
            Try
                'Throw New Exception("TEst")
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    v1Json = client.DownloadString(v1JsonUrl)
                End Using
            Catch ex As Exception


                Debug.WriteLine("error- getting v1Json data")
                Debug.WriteLine(ex.ToString)

                Me.Invoke(New Action(Function() As Object
                                         'Me.Text = "Status: error - getting v1Json data"

                                         FunimationJsonBrowser = "v1Json"
                                         Navigate(v1JsonUrl)
                                         'Anime_Add.StatusLabel.Text = "Status: error - getting v1Json data"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                Exit Sub
            End Try
        End If
        'Debug.WriteLine("v1Json: " + v1Json)
        If v1Json = Nothing Then

            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Status: error - getting v1Json data"

                                     Anime_Add.StatusLabel.Text = "Status: error - getting v1Json data"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
            Exit Sub
        End If


        Try
            Dim ffmpeg_command_temp As String = ffmpeg_command
            If VideoFormat = ".aac" Then
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
            End If

            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Status: looking for video file"

                                     Anime_Add.StatusLabel.Text = "Status: looking for video file"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

            Funimation_Grapp_RDY = False
#Region "Name"

            Dim DownloadPfad As String = Nothing
            Dim FunimationSeason As String = Nothing
            Dim FunimationEpisode As String = Nothing
            Dim FunimationTitle As String = Nothing
            Dim FunimationEpisodeTitle As String = Nothing
            Dim FunimationDub As String = Nothing
            Dim FunimationAudioMap As String = Nothing
            Dim FunimationEpisodeJson As String = Nothing
            Dim thumbnail4 As String = ""

            Dim ser As JObject = JObject.Parse(v1Json)



            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "images" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values

                            Dim key As String = Entry("key").ToString

                            If key = "Key Art - Official Video Image" Or key = "Episode Thumbnail" Then
                                Dim path As String = Entry("path").ToString
                                thumbnail4 = path

                            End If


                        Next
                    Case "id"  'id.json for video

                        FunimationEpisodeJson = item.Value.ToString
                    Case "episodeNumber"

                        Dim FunimationEpisode3 As String = RemoveExtraSpaces(item.Value.ToString)
                        If Episode_Prefix = "[default episode prefix]" Then
                            FunimationEpisode = "Episode " + FunimationEpisode3
                        Else
                            FunimationEpisode = Episode_Prefix + FunimationEpisode3
                        End If

                    Case "name"

                        Dim NameData As List(Of JToken) = item.Values.ToList()


                        For Each Name As JProperty In NameData
                            Select Case Name.Name
                                Case "en"
                                    FunimationEpisodeTitle = Name.Value.ToString
                                    Debug.WriteLine("FunimationEpisodeTitle: " + FunimationEpisodeTitle)
                            End Select


                        Next




                    Case "season" 'each record is inside the entries array


                        Dim SubData As List(Of JToken) = item.Values.ToList()


                        For Each SubItem As JProperty In SubData

                            Select Case SubItem.Name
                                Case "name"

                                    If Season_Prefix = "[default season prefix]" Then
                                        Dim SeasonNameData As List(Of JToken) = SubItem.Values.ToList()


                                        For Each SeasonName As JProperty In SeasonNameData
                                            Select Case SeasonName.Name
                                                Case "en"
                                                    FunimationSeason = SeasonName.Value.ToString
                                                    Debug.WriteLine("FunimationSeason: " + FunimationSeason)
                                            End Select


                                        Next


                                    End If
                                Case "number"

                                    If Season_Prefix = "[default season prefix]" Then
                                        'FunimationSeason = Entry("name")
                                    Else
                                        Dim EpisodeNumer As String = SubItem.Value.ToString
                                        FunimationSeason = Season_Prefix + " " + EpisodeNumer
                                        Debug.WriteLine("FunimationSeason: " + FunimationSeason)

                                    End If


                            End Select
                        Next




                    Case "show" 'each record is inside the entries array


                        Dim SubData As List(Of JToken) = item.Values.ToList()


                        For Each SubItem As JProperty In SubData

                            Select Case SubItem.Name
                                Case "name"
                                    Dim SeasonNameData As List(Of JToken) = SubItem.Values.ToList()


                                    For Each SeasonName As JProperty In SeasonNameData
                                        Select Case SeasonName.Name
                                            Case "en"
                                                FunimationTitle = SeasonName.Value.ToString
                                                Debug.WriteLine("FunimationTitle: " + FunimationTitle)
                                        End Select


                                    Next


                            End Select
                        Next







                End Select
            Next

            FunimationTitle = RemoveExtraSpaces(String.Join(" ", FunimationTitle.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c))
            FunimationEpisodeTitle = String.Join(" ", FunimationEpisodeTitle.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)

            FunimationDub = ConvertFunimationDub(DubFunimation) 'FunimationDub2(0)

            Dim DefaultName As String = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)

            If CR_NameMethode = 1 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisodeTitle)
            ElseIf CR_NameMethode = 2 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode + " " + FunimationEpisodeTitle)
            ElseIf CR_NameMethode = 3 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationEpisodeTitle + " " + FunimationSeason + " " + FunimationEpisode)
            End If

            DefaultName = DefaultName.Replace("&#x27;", "'")




            'Dim DefaultPath As String = Pfad + "\" + DefaultName + VideoFormat
            'DefaultPath = DefaultPath.Replace("\\", "\")
#End Region

#Region "Pfad"
            Dim TextBox2_Text As String = Nothing

            Me.Invoke(New Action(Function() As Object
                                     TextBox2_Text = Anime_Add.TextBox2.Text

                                     Return Nothing
                                 End Function))

            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then

            Else
                Me.Invoke(New Action(Function() As Object

                                         Return Nothing
                                     End Function))
            End If

            DefaultName = DefaultName.Replace(":", "")

            DownloadPfad = RemoveExtraSpaces(UseSubfolder(FunimationTitle, FunimationSeason, Pfad))

            If Not Directory.Exists(Path.GetDirectoryName(DownloadPfad)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(DownloadPfad))
                    DownloadPfad = RemoveExtraSpaces(Chr(34) + DownloadPfad + DefaultName + VideoFormat + Chr(34))

                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    DownloadPfad = RemoveExtraSpaces(Chr(34) + Pfad + DefaultName + VideoFormat + Chr(34))
                End Try
            Else
                DownloadPfad = RemoveExtraSpaces(Chr(34) + DownloadPfad + DefaultName + VideoFormat + Chr(34))
            End If


#Region "lösche doppel download"

            Dim Pfad5 As String = DownloadPfad.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: File already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: Old file overwritten."
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))

                    Catch ex As Exception
                    End Try
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Crunchyroll Downloader"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))

                    Funimation_Grapp_RDY = True
                    Exit Sub
                End If

            End If
#End Region

#End Region

#Region "json"
            Dim EpisodeJsonString As String = Nothing


            Dim PlayerClient As New WebClient

            PlayerClient.Encoding = Encoding.UTF8
            PlayerClient.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
            PlayerClient.Headers.Add(HttpRequestHeader.Accept, "application/json, text/plain, */*")
            PlayerClient.Headers.Add("origin: https://www.funimation.com/")
            PlayerClient.Headers.Add(HttpRequestHeader.Referer, "https://www.funimation.com/")

            Dim BaseUrl As String = "https://playback.prd.funimationsvc.com/v1/play/"
            Debug.WriteLine(PlayerClient.Headers.ToString)
            If FunimationToken = Nothing Then
                Debug.WriteLine("FunimationToken: false")
                BaseUrl = "https://playback.prd.funimationsvc.com/v1/play/anonymous/"
            Else
                Debug.WriteLine("FunimationToken: true")
                PlayerClient.Headers.Add(HttpRequestHeader.Authorization, FunimationToken)
            End If

            'FunimationToken
            'MsgBox(WebbrowserCookie)
            'BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion

            If FunimationDeviceRegion = Nothing Then
                FunimationDeviceRegion = "?deviceType=web"
            End If
            Debug.WriteLine(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)
            If WebbrowserCookie = Nothing Then
            Else
                PlayerClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
            End If

            If SystemWebBrowserCookie = Nothing Then
            Else
                PlayerClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
            End If
            ' Dim PlayerPage As String = SubsClient.DownloadString("https://www.funimation.com/player/" + ExperienceID + "/?bdub=0&qid=")

            Try
                'Throw New System.Exception("Test")
                'MsgBox(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)
                EpisodeJsonString = PlayerClient.DownloadString(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)

            Catch ex As Exception

                Debug.WriteLine(ex.ToString)
                Pause(2)
                Debug.WriteLine("showexperience data via browser")
                'Me.Invoke(New Action(Function() As Object
                'PlayerClient.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip")

                'EpisodeJsonString = DecompressString(PlayerClient.DownloadData(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion))
                EpisodeJsonString = PlayerClient.DownloadString(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)

                'Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)

                'ErrorBrowserString = "Funimation_showexperience"
                'ErrorBrowserUrl = "https://www.funimation.com/api/showexperience/" + ExperienceID + "/?pinst_id=fzQc9p9f"
                'Debug.WriteLine("2-showexperience data via browser")
                'ErrorBrowser.ShowDialog()
                'Debug.WriteLine("3-showexperience data via browser")
                'showexperience = ErrorBrowserBackString


            End Try

            'MsgBox(EpisodeJsonString)


            Dim SubsFiles As New List(Of FunimationSubs)
            Dim VideoStreams As New List(Of FunimationStream)

            Dim EpisodeJson As JObject = JObject.Parse(EpisodeJsonString)
            Dim EpisodeJsonData As List(Of JToken) = EpisodeJson.Children().ToList

            Dim PrimaryVersion As String = Nothing ' item.Item("version").ToString
            Dim PrimaryaudioLanguage As String = Nothing ' item.Item("audioLanguage").ToString
            Dim PrimarymanifesUrl As String = Nothing 'item.Item("manifestPath").ToString


            For Each item As JProperty In EpisodeJsonData
                item.CreateReader()
                Select Case item.Name

                    Case "fallback"


                        '  MsgBox(SubItem.Value.ToString())

                        Dim SubData2 As List(Of JToken) = item.Values.ToList
                        'MsgBox(SubData2.Count.ToString)
                        For i As Integer = 0 To SubData2.Count - 1
                            Dim audioLanguage As String = Nothing
                            Dim Format As String = Nothing

                            Try
                                Dim Version As String = SubData2(i).Item("version").ToString
                                audioLanguage = SubData2(i).Item("audioLanguage").ToString
                                Dim Url As String = SubData2(i).Item("manifestPath").ToString
                                Debug.WriteLine(Version)
                                Debug.WriteLine(audioLanguage)
                                Debug.WriteLine(Url)
                                Format = SubData2(i).Item("fileExt").ToString

                                If Format = "m3u8" Then
                                    VideoStreams.Add(New FunimationStream(audioLanguage, Version, Url, False))
                                End If

                            Catch ex As Exception

                            End Try

                            Try

                                Dim SubData3 As List(Of JToken) = SubData2(i).Item("subtitles").Children.ToList
                                'MsgBox(SubData2.Count.ToString)
                                For i3 As Integer = 0 To SubData2.Count - 1
                                    Try
                                        Dim LangCode As String = SubData3(i3).Item("languageCode").ToString
                                        Dim CCFormat As String = SubData3(i3).Item("fileExt").ToString
                                        Dim Url As String = SubData3(i3).Item("filePath").ToString

                                        If audioLanguage = "ja" And Format = "m3u8" Then
                                            SubsFiles.Add(New FunimationSubs(LangCode, CCFormat, Url))
                                        End If

                                    Catch ex As Exception

                                    End Try

                                Next
                            Catch ex As Exception

                            End Try



                        Next




                    Case "primary" 'each record is inside the entries array

                        Dim SubData As List(Of JToken) = item.Values.ToList()

                        For Each SubItem As JProperty In SubData

                            Select Case SubItem.Name
                               ' Case "manifestPath"
                               '     Funimation_m3u8_Main = SubItem.Value.ToString
                               '' MsgBox()
                                Case "version"
                                    PrimaryVersion = SubItem.Value.ToString
                                Case "audioLanguage"
                                    PrimaryaudioLanguage = SubItem.Value.ToString
                                Case "manifestPath"
                                    PrimarymanifesUrl = SubItem.Value.ToString
                                Case "subtitles"
                                    '  MsgBox(SubItem.Value.ToString())

                                    Dim SubData2 As List(Of JToken) = SubItem.Values.ToList
                                    'MsgBox(SubData2.Count.ToString)
                                    For i As Integer = 0 To SubData2.Count - 1
                                        Try
                                            Dim LangCode As String = SubData2(i).Item("languageCode").ToString
                                            Dim Format As String = SubData2(i).Item("fileExt").ToString
                                            Dim Url As String = SubData2(i).Item("filePath").ToString

                                            SubsFiles.Add(New FunimationSubs(LangCode, Format, Url))
                                        Catch ex As Exception

                                        End Try
                                    Next

                            End Select
                        Next
                        Debug.WriteLine("primary version: " + PrimaryVersion)
                        Debug.WriteLine("primary audioLanguage: " + PrimaryaudioLanguage)
                        Debug.WriteLine("primary manifesUrl: " + PrimarymanifesUrl)

                        VideoStreams.Add(New FunimationStream(PrimaryaudioLanguage, PrimaryVersion, PrimarymanifesUrl, True))

                End Select
            Next

#End Region





#Region "m3u8 URL"
            Dim Funimation_m3u8_Main As String = Nothing
            Dim Funimation_m3u8_MainVersion As String = Nothing

            Dim Funimation_m3u8_Primary_Version As String = Nothing
            Dim Funimation_m3u8_Primary As String = Nothing
            Dim Funimation_m3u8_Primary_audioLanguage As String = Nothing

            Dim Funimation_m3u8_final As String = Nothing
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8

            If SubsOnly = False Then

                For i As Integer = 0 To VideoStreams.Count - 1

                    If VideoStreams(i).Primary = True Then
                        Funimation_m3u8_Primary = VideoStreams(i).Url
                        Funimation_m3u8_Primary_Version = VideoStreams(i).version
                        Funimation_m3u8_Primary_audioLanguage = VideoStreams(i).audioLanguage
                    End If

                    If VideoStreams(i).audioLanguage = ConvertFunimationDubToJson(DubFunimation) And Funimation_m3u8_Main = Nothing Then
                        Funimation_m3u8_Main = VideoStreams(i).Url
                        Funimation_m3u8_MainVersion = VideoStreams(i).version
                    ElseIf VideoStreams(i).audioLanguage = ConvertFunimationDubToJson(DubFunimation) And VideoStreams(i).version = "uncut" Then
                        Funimation_m3u8_Main = VideoStreams(i).Url
                        Funimation_m3u8_MainVersion = VideoStreams(i).version
                    End If

                Next

                If Funimation_m3u8_Main = Nothing Then
                    Funimation_m3u8_Main = Funimation_m3u8_Primary
                    Funimation_m3u8_MainVersion = Funimation_m3u8_Primary_Version
                    FunimationDub = ConvertFunimationDub(ConvertJsonToFunimationDub(Funimation_m3u8_Primary_audioLanguage))
                End If

                If Funimation_m3u8_Main = Nothing Then

                    If MessageBox.Show("No media matching your settings." + vbNewLine + "Avalible: Not implimentented, press 'Yes' to copy the data into the clipboard.", "No media", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Me.Invoke(New Action(Function() As Object

                                                 Try
                                                     My.Computer.Clipboard.SetText(EpisodeJsonString)
                                                 Catch ex As Exception
                                                 End Try
                                                 Return Nothing
                                             End Function))
                        Exit Sub
                    Else
                        Funimation_Grapp_RDY = True
                        Exit Sub
                    End If

                End If
                Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Video found!"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))

                    Dim str1 As String = client0.DownloadString(Funimation_m3u8_Main.Replace(Chr(34), ""))

                    If CBool(InStr(str1, "# AUDIO groups")) Then
                        Dim FunimationAudio() As String = str1.Split(New String() {"# AUDIO groups"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim FunimationAudio2() As String = FunimationAudio(1).Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim FunimationAudio3() As String = FunimationAudio2(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        FunimationAudioMap = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + FunimationAudio3(0) + Chr(34)
                    End If

                    Dim Streams() As String = str1.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)


                    'MsgBox(Funimation_m3u8_Main)
                    Dim FunimationBackupm3u8 As String = Nothing
                    For i As Integer = 0 To Streams.Length - 1

                        Try

                            If CBool(InStr(Streams(i), "x" + Reso.ToString)) Then
                                Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim StreamURL As String = "https://" + Streams3(0).Trim

                                Dim CheckClient As New WebClient
                                CheckClient.Encoding = Encoding.UTF8
                                If Not WebbrowserCookie = Nothing Then
                                    CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                                ElseIf Not SystemWebBrowserCookie = Nothing Then
                                    CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                                End If

                                Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                                'MsgBox(m3u8String)
                                Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim keyfileurl3 As String = keyfileurl2(0)

                                If CBool(InStr(keyfileurl2(0), "https://")) Then

                                Else
                                    Dim c() As String = New Uri(StreamURL).Segments
                                    Dim path As String = "https://" + New Uri(StreamURL).Host

                                    For i3 As Integer = 0 To c.Count - 2
                                        path = path + c(i3)
                                    Next
                                    keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                                End If

                                'MsgBox(keyfileurl3)
                                Try
                                    Dim CheckClient2 As New WebClient
                                    CheckClient2.Encoding = System.Text.Encoding.UTF8
                                    Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                    Funimation_m3u8_final = StreamURL
                                    FunimationBackupm3u8 = StreamURL
                                    Exit For
                                Catch ex As Exception
                                    Debug.WriteLine(keyfileurl3 + vbNewLine + vbNewLine + ex.ToString)
                                End Try
                            ElseIf CBool(InStr(Streams(i), ResoFunBackup)) Then
                                Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim StreamURL As String = "https://" + Streams3(0).Trim

                                Dim CheckClient As New WebClient
                                CheckClient.Encoding = Encoding.UTF8
                                If Not WebbrowserCookie = Nothing Then
                                    CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                                ElseIf Not SystemWebBrowserCookie = Nothing Then
                                    CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                                End If
                                Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                                Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim keyfileurl3 As String = keyfileurl2(0)

                                If CBool(InStr(keyfileurl2(0), "https://")) Then

                                Else
                                    Dim c() As String = New Uri(StreamURL).Segments
                                    Dim path As String = "https://" + New Uri(StreamURL).Host

                                    For i3 As Integer = 0 To c.Count - 2
                                        path = path + c(i3)
                                    Next
                                    keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                                End If

                                'MsgBox(keyfileurl3)
                                Try
                                    Dim CheckClient2 As New WebClient
                                    CheckClient2.Encoding = System.Text.Encoding.UTF8
                                    Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                    FunimationBackupm3u8 = StreamURL
                                    Exit For
                                Catch ex As Exception
                                    Debug.WriteLine(keyfileurl3 + vbNewLine + vbNewLine + ex.ToString)
                                End Try

                            End If

                        Catch ex As Exception

                        End Try
                    Next


                    If Funimation_m3u8_final = Nothing And FunimationBackupm3u8 = Nothing Then

                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: Resolution not found!"
                                                 Me.Invalidate()
                                                 DialogTaskString = "Funimation_Resolution"
                                                 ResoNotFoundString = str1
                                                 ErrorDialog.ShowDialog()
                                                 Return Nothing
                                             End Function))
                        ResoFunBackup = ResoBackString
                        For i As Integer = 0 To Streams.Length - 1
                            If CBool(InStr(Streams(i), ResoBackString)) Then
                                Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim StreamURL As String = "https://" + Streams3(0).Trim
                                Dim CheckClient As New WebClient
                                CheckClient.Encoding = Encoding.UTF8

                                If Not WebbrowserCookie = Nothing Then
                                    CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                                ElseIf Not SystemWebBrowserCookie = Nothing Then
                                    CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                                End If

                                Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                                'MsgBox(textLenght(i))
                                Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim keyfileurl3 As String = keyfileurl2(0)
                                If CBool(InStr(keyfileurl2(0), "https://")) Then
                                Else
                                    Dim c() As String = New Uri(StreamURL).Segments
                                    Dim path As String = "https://" + New Uri(StreamURL).Host

                                    For i3 As Integer = 0 To c.Count - 2
                                        path = path + c(i3)
                                    Next
                                    keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                                End If
                                Try
                                    Dim CheckClient2 As New WebClient
                                    CheckClient2.Encoding = System.Text.Encoding.UTF8
                                    Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                    Funimation_m3u8_final = StreamURL
                                    Exit For
                                Catch ex As Exception
                                    Debug.WriteLine(keyfileurl3 + vbNewLine + ex.ToString)
                                End Try


                                'Funimation_m3u8_final = textLenght(i)
                                'Exit For

                            End If
                        Next
                    ElseIf Funimation_m3u8_final = Nothing Then
                        Funimation_m3u8_final = FunimationBackupm3u8
                    Else
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: Resolution found!"
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))

                    End If
                Else
                    Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: Substitles only mode - skipped video"
                                             Me.Invalidate()
                                             Return Nothing
                                     End Function))
            End If
            'MsgBox(FunimationName3)
            'MsgBox(Funimation_m3u8_final)

            Dim ResoHTMLDisplay As String = Reso.ToString + "p"

#Region "Subs"


            Dim HardSubFound As Boolean = False
            Dim HardSubSplittString As String = Nothing
            Dim UsedSub As String = Nothing
            Dim UsedSubs As New List(Of String)
            Dim ffmpeg_hardsub As String = Nothing




            For i As Integer = 0 To SubsFiles.Count - 1
                Debug.WriteLine(SubsFiles(i).LangugageCode + "-" + SubsFiles(i).Format)

                If SubFunimation.Count = 0 Then
                    Exit For
                End If

                If Funimation_vtt = True And SubsFiles(i).Format = "vtt" And CBool(InStr(SubFunimationString, SubsFiles(i).LangugageCode)) Then
                    UsedSubs.Add(SubsFiles(i).Url + " , " + SubsFiles(i).LangugageCode)

                ElseIf Funimation_srt = True And SubsFiles(i).Format = "srt" And CBool(InStr(SubFunimationString, SubsFiles(i).LangugageCode)) Then
                    UsedSubs.Add(SubsFiles(i).Url + " , " + SubsFiles(i).LangugageCode)
                End If

            Next


            '
            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            If Not FunimationAudioMap = Nothing Then
                SoftSubMergeMaps = " -map 0:v -map 1:a"
            End If
            Dim SoftSubMergeMetatata As String = Nothing

            If UsedSubs.Count > 0 Then
                If MergeSubs = True And SubsOnly = False Then
                    Dim DispositionIndex As Integer = 999
                    Dim LastMerged As String = Nothing
                    Dim MapCount As Integer = -1
                    For i As Integer = 0 To UsedSubs.Count - 1
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        If CCtoMP4CC(SoftSub(1)) = LastMerged Then
                            Continue For
                        Else
                            LastMerged = CCtoMP4CC(SoftSub(1))
                        End If
                        MapCount = MapCount + 1
                        If DefaultSubFunimation = SoftSub(1) Then
                            'Debug.WriteLine(SoftSub(1))
                            DispositionIndex = MapCount
                        End If
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        End If

                        If FunimationAudioMap = Nothing Then
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 1).ToString
                        Else
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 2).ToString
                        End If

                        If SoftSubMergeMetatata = Nothing Then
                            'SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSub(1))
                            SoftSubMergeMetatata = " -metadata:s:s:" + MapCount.ToString + " language=" + CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + MapCount.ToString + " language=" + CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                            'SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i))


                        End If

                    Next
                    If DispositionIndex < 999 Then
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    Dim SubsClient As New WebClient
                    SubsClient.Encoding = Encoding.UTF8
                    If Not WebbrowserCookie = Nothing Then
                        SubsClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                    ElseIf Not SystemWebBrowserCookie = Nothing Then
                        SubsClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                    End If
                    For i As Integer = 0 To UsedSubs.Count - 1
                        LabelUpdate = "Status: downloading subtitle file"
                        LabelEpisode = UsedSubs(i)
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_3 As String = SoftSub(0).Replace("\/", "/")
                        Dim Subfile As String = SubsClient.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = DownloadPfad.Replace(Chr(34), "")
                        'MsgBox(FN)
                        Dim SubtitelFormat As String = "srt"
                        If CBool(InStr(SoftSub_3, ".vtt")) Then
                            SubtitelFormat = "vtt"
                        End If
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSub(1) + Path.GetExtension(Pfad3)), SubtitelFormat)

                        If i = 0 Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), SubtitelFormat)
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        Debug.WriteLine(Pfad4)
                        File.WriteAllText(Pfad4, Subfile, Encoding.UTF8)
                        Pause(1)
                    Next

                End If
            End If

#End Region

#Region "ffmpeg command"

            Dim DubMetatata As String = Nothing
            If FunimationDub = "Japanese" Then
                DubMetatata = " -metadata:s:a:0 language=jpn"

            ElseIf FunimationDub = "Portuguese (Brazil)" Then
                DubMetatata = " -metadata:s:a:0 language=por"

            ElseIf FunimationDub = "Spanish (Latin Am)" Then
                DubMetatata = " -metadata:s:a:0 language=spa"

            Else '
                DubMetatata = " -metadata:s:a:0 language=eng"

            End If

            If HardSubFound = True And CBool(InStr(VideoFormat, ".aac")) = False Then
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + " -vf subtitles=" + Chr(34) + UsedSub + Chr(34) + " " + ffmpeg_hardsub

            ElseIf MergeSubs = True Then

                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + DubMetatata

            ElseIf CBool(InStr(VideoFormat, ".aac")) = True Then
                If FunimationAudioMap = Nothing Then
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command_temp
                Else
                    Funimation_m3u8_final = FunimationAudioMap.Replace(" -headers " + My.Resources.ffmpeg_user_agend + " ", "") + DubMetatata + " " + ffmpeg_command_temp
                End If

            Else

                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + DubMetatata + " " + ffmpeg_command

            End If

            Funimation_m3u8_final = Funimation_m3u8_final + " -metadata:g encoding_tool=CrD_Funimation_JS"


#End Region
            'MsgBox(Funimation_m3u8_final)
            'DownloadPfad = DownloadPfad.Replace(" \", "\")
            If SubsOnly = True Then
                Funimation_m3u8_final = "-i [Subtitles only]"
            End If

            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Pfad_DL, L1Name, DefaultName, ResoHTMLDisplay, Funimation_m3u8_MainVersion, SubValuesToDisplay(), thumbnail4, Funimation_m3u8_final, DownloadPfad, "FM")
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail4 + My.Resources.htmlnachTumbnail + FunimationTitle + " <br> " + FunimationSeason + " " + FunimationEpisode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + DefaultName + "-->")

#End Region
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader!"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

            MsgBox(ex.ToString)
        End Try
        Funimation_Grapp_RDY = True

    End Sub

#Region "Old JS??"


    Public Sub GetFunimationJS_VideoProxy(Optional ByVal v1JsonURL As String = Nothing, Optional ByVal v1JsonData As String = Nothing)
        LoadedUrls.Clear()
        Dim Evaluator = New Thread(Sub() Me.GetFunimationJS_Video(v1JsonURL, v1JsonData))
        Evaluator.Start()

        'Me.Invoke(New Action(Function() As Object

        '                         GetFunimationJS_Video(v1JsonURL, v1JsonData)
        '                         Return Nothing
        '                     End Function))

    End Sub

    Public Sub GetFunimationJS_Video(ByVal v1JsonUrl As String, ByVal v1JsonData As String) ', ByVal WebsiteURL As String
        Debug.WriteLine(v1JsonUrl)
        Dim v1Json As String = Nothing

        If v1JsonUrl = Nothing Then
            v1Json = v1JsonData
        Else
            Try
                'Throw New Exception("TEst")
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    v1Json = client.DownloadString(v1JsonUrl)
                End Using
            Catch ex As Exception


                Debug.WriteLine("error- getting v1Json data")
                Debug.WriteLine(ex.ToString)

                Me.Invoke(New Action(Function() As Object
                                         'Me.Text = "Status: error - getting v1Json data"

                                         FunimationJsonBrowser = "v1Json"
                                         Navigate(v1JsonUrl)
                                         'Anime_Add.StatusLabel.Text = "Status: error - getting v1Json data"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                Exit Sub
            End Try
        End If
        Debug.WriteLine("v1Json: " + v1Json)
        If v1Json = Nothing Then

            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Status: error - getting v1Json data"

                                     Anime_Add.StatusLabel.Text = "Status: error - getting v1Json data"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
            Exit Sub
        End If

        Try
            Dim ffmpeg_command_temp As String = ffmpeg_command
            If VideoFormat = ".aac" Then
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)

                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)
            End If

            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Status: looking for video file"

                                     Anime_Add.StatusLabel.Text = "Status: looking for video file"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

            Funimation_Grapp_RDY = False
#Region "Name"

            Dim DownloadPfad As String = Nothing
            Dim FunimationSeason As String = Nothing
            Dim FunimationEpisode As String = Nothing
            Dim FunimationTitle As String = Nothing
            Dim FunimationEpisodeTitle As String = Nothing
            Dim FunimationDub As String = Nothing
            Dim FunimationAudioMap As String = Nothing



            Dim ser As JObject = JObject.Parse(v1Json)
            Try
                Try
                    FunimationEpisodeTitle = RemoveExtraSpaces(ser("name").ToString)
                Catch ex As Exception
                End Try

                Try
                    Dim FunimationEpisode3 As String = RemoveExtraSpaces(ser("episodeNumber").ToString)
                    If Episode_Prefix = "[default episode prefix]" Then
                        FunimationEpisode = "Episode " + FunimationEpisode3
                    Else
                        FunimationEpisode = Episode_Prefix + FunimationEpisode3
                    End If
                Catch ex As Exception
                End Try

                Try
                    FunimationTitle = RemoveExtraSpaces(ser("name").ToString)
                Catch ex As Exception
                End Try


            Catch ex As Exception

            End Try

            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "season" 'each record is inside the entries array


                        Dim SubData As List(Of JToken) = item.Values.ToList()


                        For Each SubItem As JProperty In SubData

                            Select Case SubItem.Name
                                Case "name"

                                    If Season_Prefix = "[default season prefix]" Then
                                        FunimationSeason = SubItem.Value.ToString
                                        Debug.WriteLine("FunimationSeason: " + FunimationSeason)
                                    End If
                                Case "number"

                                    If Season_Prefix = "[default season prefix]" Then
                                        'FunimationSeason = Entry("name")
                                    Else
                                        Dim EpisodeNumer As String = SubItem.Value.ToString
                                        FunimationSeason = Season_Prefix + " " + EpisodeNumer
                                        Debug.WriteLine("FunimationSeason: " + FunimationSeason)

                                    End If


                            End Select
                        Next




                    Case "show" 'each record is inside the entries array


                        Dim SubData As List(Of JToken) = item.Values.ToList()


                        For Each SubItem As JProperty In SubData

                            Select Case SubItem.Name
                                Case "name"
                                    FunimationTitle = SubItem.Value.ToString
                                    Debug.WriteLine("FunimationTitle: " + FunimationTitle)

                            End Select
                        Next







                End Select
            Next

            FunimationTitle = String.Join(" ", FunimationTitle.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)
            FunimationEpisodeTitle = String.Join(" ", FunimationEpisodeTitle.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)

            FunimationDub = ConvertFunimationDub(DubFunimation) 'FunimationDub2(0)

            Dim DefaultName As String = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)

            If CR_NameMethode = 1 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisodeTitle)
            ElseIf CR_NameMethode = 2 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode + " " + FunimationEpisodeTitle)
            ElseIf CR_NameMethode = 3 Then
                DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationEpisodeTitle + " " + FunimationSeason + " " + FunimationEpisode)
            End If

            DefaultName = DefaultName.Replace("&#x27;", "'")




            'Dim DefaultPath As String = Pfad + "\" + DefaultName + VideoFormat
            'DefaultPath = DefaultPath.Replace("\\", "\")
#End Region

#Region "Pfad"
            Dim TextBox2_Text As String = Nothing

            Me.Invoke(New Action(Function() As Object
                                     TextBox2_Text = Anime_Add.textBox2.Text

                                     Return Nothing
                                 End Function))

            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then

            Else
                Me.Invoke(New Action(Function() As Object

                                         Return Nothing
                                     End Function))
            End If

            DefaultName = DefaultName.Replace(":", "")

            DownloadPfad = UseSubfolder(FunimationTitle, FunimationSeason, Pfad)

            If Not Directory.Exists(Path.GetDirectoryName(DownloadPfad)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(DownloadPfad))
                    DownloadPfad = Chr(34) + DownloadPfad + DefaultName + VideoFormat + Chr(34)

                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    DownloadPfad = Chr(34) + Pfad + DefaultName + VideoFormat + Chr(34)
                End Try
            Else
                DownloadPfad = Chr(34) + DownloadPfad + DefaultName + VideoFormat + Chr(34)
            End If


#Region "lösche doppel download"

            Dim Pfad5 As String = DownloadPfad.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: File already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: Old file overwritten."
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))

                    Catch ex As Exception
                    End Try
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Crunchyroll Downloader"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))

                    Funimation_Grapp_RDY = True
                    Exit Sub
                End If

            End If
#End Region

#End Region
#Region "m3u8 URL"
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            Dim Funimation_m3u8_final As String = Nothing
            Dim ExperienceID As String = Nothing


            'Dim ser As JObject = JObject.Parse(v1Json)
            'Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "videoList" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Dim id As String = Entry("id").ToString
                            Dim SubData As List(Of JToken) = Entry.Children().ToList
                            For Each SubItem As JProperty In SubData
                                Select Case SubItem.Name
                                    Case "spokenLanguages"
                                        For Each SubEntry As JObject In SubItem.Values
                                            Dim name As String = SubEntry("name").ToString
                                            If name = FunimationDub Then
                                                ExperienceID = id
                                            End If
                                        Next
                                End Select
                            Next

                        Next



                End Select
            Next

            If ExperienceID = Nothing Then
                MsgBox("Dub not found") 'proper error handling to be done.
                Exit Sub
            End If

            If SubsOnly = False Then

                Me.Invoke(New Action(Function() As Object
                                         '    Anime_Add.StatusLabel.Text = iFrameURL
                                         'MsgBox(WebbrowserCookie)
                                         Return Nothing
                                     End Function))

                Dim showexperience As String = Nothing


                Dim PlayerClient As New WebClient
                PlayerClient.Encoding = Encoding.UTF8
                'MsgBox(WebbrowserCookie)
                If WebbrowserCookie = Nothing Then
                Else
                    PlayerClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                End If

                If SystemWebBrowserCookie = Nothing Then
                Else
                    PlayerClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                End If
                ' Dim PlayerPage As String = SubsClient.DownloadString("https://www.funimation.com/player/" + ExperienceID + "/?bdub=0&qid=")

                Try
                    'Throw New System.Exception("Test")
                    showexperience = PlayerClient.DownloadString("https://www.funimation.com/api/showexperience/" + ExperienceID + "/?pinst_id=fzQc9p9f")

                Catch ex As Exception

                    Debug.WriteLine("showexperience data via browser")
                    'Me.Invoke(New Action(Function() As Object
                    showexperience = PlayerClient.DownloadString("https://www.funimation.com/api/showexperience/" + ExperienceID + "/?pinst_id=fzQc9p9f")

                    'Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)

                    'ErrorBrowserString = "Funimation_showexperience"
                    'ErrorBrowserUrl = "https://www.funimation.com/api/showexperience/" + ExperienceID + "/?pinst_id=fzQc9p9f"
                    'Debug.WriteLine("2-showexperience data via browser")
                    'ErrorBrowser.ShowDialog()
                    'Debug.WriteLine("3-showexperience data via browser")
                    'showexperience = ErrorBrowserBackString


                End Try






                'If Not WebbrowserCookie = Nothing Then
                '    client0.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                'ElseIf Not SystemWebBrowserCookie = Nothing Then
                '    client0.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                'End If

                'Dim str0 As String = client0.DownloadString("")
                ''MsgBox("https://www.funimation.com/api/showexperience/" + Player_ID2(0) + "/?pinst_id=fzQc9p9f")
                ''MsgBox(str0)
                'End Sub

                'Sub weier()
                Dim Funimation_m3u8() As String = showexperience.Split(New String() {My.Resources.Funimation_src_string}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Funimation_m3u8_Main As String = Nothing
                For i As Integer = 0 To Funimation_m3u8.Count - 1
                    If CBool(InStr(Funimation_m3u8(i), "m3u8?")) Then
                        Dim Funimation_m3u8_split() As String = Funimation_m3u8(i).Split(New String() {", "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Funimation_m3u8_Main = Funimation_m3u8_split(0)
                        Exit For
                    End If
                Next
                If Funimation_m3u8_Main = Nothing Then

                    If MessageBox.Show("No media found in:" + vbNewLine + showexperience, "No media", MessageBoxButtons.RetryCancel) = DialogResult.Retry Then
                        Me.Invoke(New Action(Function() As Object
                                                 Navigate(WebbrowserURL)
                                                 Try
                                                     Anime_Add.StatusLabel.Text = "retrying Funimation"
                                                     Me.Text = "retrying Funimation"
                                                     Me.Invalidate()
                                                 Catch ex As Exception
                                                 End Try
                                                 Return Nothing
                                             End Function))
                        Exit Sub
                    Else
                        Funimation_Grapp_RDY = True
                        Exit Sub
                    End If

                End If
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: Video found!"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

                Dim str1 As String = client0.DownloadString(Funimation_m3u8_Main.Replace(Chr(34), ""))

                If CBool(InStr(str1, "# AUDIO groups")) Then
                    Dim FunimationAudio() As String = str1.Split(New String() {"# AUDIO groups"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio2() As String = FunimationAudio(1).Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio3() As String = FunimationAudio2(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    FunimationAudioMap = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + FunimationAudio3(0) + Chr(34)
                End If

                Dim Streams() As String = str1.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)


                'MsgBox(Funimation_m3u8_Main)
                Dim FunimationBackupm3u8 As String = Nothing
                For i As Integer = 0 To Streams.Length - 1

                    Try

                        If CBool(InStr(Streams(i), "x" + Reso.ToString)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim

                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8
                            If Not WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                            ElseIf Not SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                            End If

                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            'MsgBox(m3u8String)
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)

                            If CBool(InStr(keyfileurl2(0), "https://")) Then

                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host

                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If

                            'MsgBox(keyfileurl3)
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                Funimation_m3u8_final = StreamURL
                                FunimationBackupm3u8 = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + vbNewLine + ex.ToString)
                            End Try
                        ElseIf CBool(InStr(Streams(i), ResoFunBackup)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim

                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8
                            If Not WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                            ElseIf Not SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                            End If
                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)

                            If CBool(InStr(keyfileurl2(0), "https://")) Then

                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host

                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If

                            'MsgBox(keyfileurl3)
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                FunimationBackupm3u8 = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + vbNewLine + ex.ToString)
                            End Try

                        End If

                    Catch ex As Exception

                    End Try
                Next


                If Funimation_m3u8_final = Nothing And FunimationBackupm3u8 = Nothing Then

                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Resolution not found!"
                                             Me.Invalidate()
                                             DialogTaskString = "Funimation_Resolution"
                                             ResoNotFoundString = str1
                                             ErrorDialog.ShowDialog()
                                             Return Nothing
                                         End Function))
                    ResoFunBackup = ResoBackString
                    For i As Integer = 0 To Streams.Length - 1
                        If CBool(InStr(Streams(i), ResoBackString)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim
                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8

                            If Not WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
                            ElseIf Not SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
                            End If

                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            'MsgBox(textLenght(i))
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)
                            If CBool(InStr(keyfileurl2(0), "https://")) Then
                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host

                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                Funimation_m3u8_final = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + ex.ToString)
                            End Try


                            'Funimation_m3u8_final = textLenght(i)
                            'Exit For

                        End If
                    Next
                ElseIf Funimation_m3u8_final = Nothing Then
                    Funimation_m3u8_final = FunimationBackupm3u8
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Resolution found!"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))

                End If
            Else
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: Substitles only mode - skipped video"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
            End If
            'MsgBox(FunimationName3)
            'MsgBox(Funimation_m3u8_final)
#Region "thumbnail"

            'Dim thumbnail As String() = v1Json.Split(New String() {"episodeThumbnail"}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim thumbnail2 As String() = thumbnail(1).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            'Dim thumbnail3 As String() = thumbnail2(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail4 As String = ""
            For Each item As JProperty In Data
                item.CreateReader()
                Select Case item.Name
                    Case "images" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values

                            Dim key As String = Entry("key").ToString

                            If key = "episodeThumbnail" Then
                                Dim path As String = Entry("path").ToString
                                thumbnail4 = path
                                Exit Select
                            End If


                            'Dim factor As String = Entry("factor").ToList.Item(0)
                            ' you can continue listing the array items untill you reach the end of you array

                        Next



                End Select
            Next


#End Region
            Dim ResoHTMLDisplay As String = Reso.ToString + "p"

#Region "Subs"


            Dim SubsClient As New WebClient
            SubsClient.Encoding = Encoding.UTF8
            If Not WebbrowserCookie = Nothing Then
                SubsClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
            ElseIf Not SystemWebBrowserCookie = Nothing Then
                SubsClient.Headers.Add(HttpRequestHeader.Cookie, SystemWebBrowserCookie)
            End If
            ' Dim PlayerPage As String = SubsClient.DownloadString("https://www.funimation.com/player/" + ExperienceID + "/?bdub=0&qid=")

            Dim PlayerPage As String = Nothing
            Try
                'Throw New System.Exception("Test")
                PlayerPage = SubsClient.DownloadString("https://www.funimation.com/player/" + ExperienceID + "/?bdub=0&qid=")

            Catch ex As Exception
                Debug.WriteLine("error- getting funimation PlayerPage")
                PlayerPage = SubsClient.DownloadString("https://www.funimation.com/player/" + ExperienceID + "/?bdub=0&qid=")
                'Me.Invoke(New Action(Function() As Object

                '                         ErrorBrowserString = "Funimation_showexperience"
                '                         ErrorBrowserUrl = "https://www.funimation.com/player/" + ExperienceID + "/?bdub=0&qid="
                '                         Debug.WriteLine("error-2-getting funimation PlayerPage")
                '                         ErrorBrowser.ShowDialog()
                '                         Debug.WriteLine("error-3-getting funimation PlayerPage")
                '                         PlayerPage = ErrorBrowserBackString
                '                         Return Nothing
                '                     End Function))


            End Try



            Dim Subs_in_srt As New List(Of String)
            Dim Subs_in_vtt As New List(Of String)

            Dim SoftSubs2 As New List(Of String)

            If SubFunimation.Count > 0 Then
                For i As Integer = 0 To SubFunimation.Count - 1
                    If CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String + SubFunimation(i))) Then
                        SoftSubs2.Add(My.Resources.Funimation_Subtitle_String + SubFunimation(i))
                        Continue For
                    ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String2 + SubFunimation(i))) Then
                        SoftSubs2.Add(My.Resources.Funimation_Subtitle_String2 + SubFunimation(i))
                    ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String3 + SubFunimation(i))) Then
                        SoftSubs2.Add(My.Resources.Funimation_Subtitle_String3 + SubFunimation(i))
                    End If

                Next
                If SoftSubs2.Count = 0 Then

                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "No Subtitles found..."
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))
                    Try
                        File.WriteAllText(DownloadPfad.Replace(Chr(34), "").Replace(VideoFormat, "-subtitle_error.log"), PlayerPage, Encoding.UTF8)
                    Catch ex As Exception
                        Debug.WriteLine("Error writing 'subtitle_error.log'")
                    End Try
                End If

            End If


            Dim HardSubFound As Boolean = False
            Dim HardSubSplittString As String = Nothing
            Dim UsedSub As String = Nothing
            Dim UsedSubs As New List(Of String)
            Dim ffmpeg_hardsub As String = Nothing

            If CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String + HardSubFunimation)) Then
                HardSubFound = True
                HardSubSplittString = My.Resources.Funimation_Subtitle_String + HardSubFunimation
            ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String2 + HardSubFunimation)) Then
                HardSubFound = True
                HardSubSplittString = My.Resources.Funimation_Subtitle_String2 + HardSubFunimation
            ElseIf CBool(InStr(PlayerPage, My.Resources.Funimation_Subtitle_String3 + HardSubFunimation)) Then
                HardSubFound = True
                HardSubSplittString = My.Resources.Funimation_Subtitle_String3 + HardSubFunimation
            End If

            If HardSubFound = True Then 'anyways not true if hardsub is "Disabled"


                If CBool(InStr(ffmpeg_command, "-c copy")) Then
                    ffmpeg_hardsub = "-bsf:a aac_adtstoasc"
                Else
                    ffmpeg_hardsub = ffmpeg_command
                End If
                'MsgBox(HardSubSplittString)
                Dim HardSubTitle() As String = PlayerPage.Split(New String() {HardSubSplittString}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To HardSubTitle.Count - 1
                    Dim HardSubTitle2() As String = HardSubTitle(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    If CBool(InStr(HardSubTitle2(HardSubTitle2.Count - 1), ".srt")) Then
                        UsedSub = HardSubTitle2(HardSubTitle2.Count - 1)
                        Exit For
                    ElseIf CBool(InStr(HardSubTitle2(HardSubTitle2.Count - 1), ".vtt")) Then
                        UsedSub = HardSubTitle2(HardSubTitle2.Count - 1)
                        Exit For
                    End If

                Next

                If UsedSub = Nothing Then
                    Throw New System.Exception("Error - No valid Subtitle for hard-subtiles found")
                End If
                'MsgBox(UsedSub)
                Dim SubText As String = client0.DownloadString(UsedSub)
                Dim SubtitelFormat As String = ".srt"
                If CBool(InStr(UsedSub, ".vtt")) Then
                    SubtitelFormat = ".vtt"
                End If
                UsedSub = GeräteID() + SubtitelFormat
                File.WriteAllText(Application.StartupPath + "\" + UsedSub, SubText, Encoding.UTF8)
            ElseIf SoftSubs2.Count > 0 Then
                For i As Integer = 0 To SoftSubs2.Count - 1
                    Dim SubTitle() As String = PlayerPage.Split(New String() {SoftSubs2.Item(i)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FoundCount As Integer = 0
                    For ii As Integer = 0 To SubTitle.Count - 1
                        Dim SubTitle2() As String = SubTitle(ii).Split(New String() {My.Resources.Funimation_subs_src}, System.StringSplitOptions.RemoveEmptyEntries)
                        For iii As Integer = 0 To SubTitle2.Count - 1
                            If CBool(InStr(SubTitle2(iii), ".srt" + Chr(34))) Then
                            ElseIf CBool(InStr(SubTitle2(iii), ".vtt" + Chr(34))) Then
                            ElseIf CBool(InStr(SubTitle2(iii), ".srt")) Then
                                If Subs_in_srt.Contains(SubTitle2(iii)) Then
                                Else
                                    Subs_in_srt.Add(SubTitle2(iii))
                                End If
                            ElseIf CBool(InStr(SubTitle2(iii), ".vtt")) Then
                                If Subs_in_vtt.Contains(SubTitle2(iii)) Then
                                Else
                                    Subs_in_vtt.Add(SubTitle2(iii))
                                End If
                            End If
                        Next

                    Next
                    Dim TempCount As Integer = UsedSubs.Count
                    Try
                        If Funimation_srt = True Then
                            UsedSubs.Add(Subs_in_srt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If Funimation_vtt = True Then
                            UsedSubs.Add(Subs_in_vtt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        End If
                    Catch ex As Exception
                    End Try




                    If TempCount = UsedSubs.Count Then
                        If Subs_in_srt.Count > 0 Then
                            UsedSubs.Add(Subs_in_srt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        ElseIf Subs_in_vtt.Count > 0 Then
                            UsedSubs.Add(Subs_in_vtt.Item(0) + " , " + SoftSubs2.Item(i).Replace(My.Resources.Funimation_Subtitle_String, "").Replace(My.Resources.Funimation_Subtitle_String2, "").Replace(My.Resources.Funimation_Subtitle_String3, ""))
                        End If
                    End If

                    Subs_in_srt.Clear()
                    Subs_in_vtt.Clear()


                Next
            End If

            '
            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            If Not FunimationAudioMap = Nothing Then
                SoftSubMergeMaps = " -map 0:v -map 1:a"
            End If
            Dim SoftSubMergeMetatata As String = Nothing

            If UsedSubs.Count > 0 Then
                If MergeSubs = True And SubsOnly = False Then
                    Dim DispositionIndex As Integer = 999
                    Dim LastMerged As String = Nothing
                    Dim MapCount As Integer = -1
                    For i As Integer = 0 To UsedSubs.Count - 1
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        If CCtoMP4CC(SoftSub(1)) = LastMerged Then
                            Continue For
                        Else
                            LastMerged = CCtoMP4CC(SoftSub(1))
                        End If
                        MapCount = MapCount + 1
                        If DefaultSubFunimation = SoftSub(1) Then
                            'Debug.WriteLine(SoftSub(1))
                            DispositionIndex = MapCount
                        End If
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        End If

                        If FunimationAudioMap = Nothing Then
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 1).ToString
                        Else
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 2).ToString
                        End If

                        If SoftSubMergeMetatata = Nothing Then
                            'SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSub(1))
                            SoftSubMergeMetatata = " -metadata:s:s:" + MapCount.ToString + " language=" + CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + MapCount.ToString + " language=" + CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                            'SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i))


                        End If

                    Next
                    If DispositionIndex < 999 Then
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    For i As Integer = 0 To UsedSubs.Count - 1
                        LabelUpdate = "Status: downloading subtitle file"
                        LabelEpisode = UsedSubs(i)
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_3 As String = SoftSub(0).Replace("\/", "/")
                        Dim Subfile As String = SubsClient.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = DownloadPfad.Replace(Chr(34), "")
                        'MsgBox(FN)
                        Dim SubtitelFormat As String = "srt"
                        If CBool(InStr(SoftSub_3, ".vtt")) Then
                            SubtitelFormat = "vtt"
                        End If
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSub(1) + Path.GetExtension(Pfad3)), SubtitelFormat)

                        If i = 0 Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), SubtitelFormat)
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        Debug.WriteLine(Pfad4)
                        File.WriteAllText(Pfad4, Subfile, Encoding.UTF8)
                        Pause(1)
                    Next

                End If
            End If

#End Region

#Region "ffmpeg command"

            Dim DubMetatata As String = Nothing
            If FunimationDub = "Japanese" Then
                DubMetatata = " -metadata:s:a:0 language=jpn"

            ElseIf FunimationDub = "Portuguese (Brazil)" Then
                DubMetatata = " -metadata:s:a:0 language=por"

            ElseIf FunimationDub = "Spanish (Latin Am)" Then
                DubMetatata = " -metadata:s:a:0 language=spa"

            Else '
                DubMetatata = " -metadata:s:a:0 language=eng"

            End If

            If HardSubFound = True And CBool(InStr(VideoFormat, ".aac")) = False Then
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + " -vf subtitles=" + Chr(34) + UsedSub + Chr(34) + " " + ffmpeg_hardsub

            ElseIf MergeSubs = True Then

                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + DubMetatata

            ElseIf CBool(InStr(VideoFormat, ".aac")) = True Then
                If FunimationAudioMap = Nothing Then
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command_temp
                Else
                    Funimation_m3u8_final = FunimationAudioMap.Replace(" -headers " + My.Resources.ffmpeg_user_agend + " ", "") + DubMetatata + " " + ffmpeg_command_temp
                End If

            Else

                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + DubMetatata + " " + ffmpeg_command

            End If

            Funimation_m3u8_final = Funimation_m3u8_final + " -metadata:g encoding_tool=CrD_Funimation_JS"


#End Region
            'MsgBox(Funimation_m3u8_final)
            'DownloadPfad = DownloadPfad.Replace(" \", "\")
            If SubsOnly = True Then
                Funimation_m3u8_final = "-i [Subtitles only]"
            End If
            DownloadPfad = RemoveExtraSpaces(DownloadPfad)
            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Pfad_DL, L1Name, DefaultName, ResoHTMLDisplay, "Unknown", SubValuesToDisplay(), thumbnail4, Funimation_m3u8_final, DownloadPfad, "FM")
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail4 + My.Resources.htmlnachTumbnail + FunimationTitle + " <br> " + FunimationSeason + " " + FunimationEpisode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + DefaultName + "-->")

#End Region
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader!"
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

            MsgBox(ex.ToString)
        End Try
        Funimation_Grapp_RDY = True
    End Sub

#End Region



#End Region


    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        Me.Invalidate()
        Try
            Dim GeckoHTML As String = My.Resources.htmlTop + vbNewLine + My.Resources.htmlTitlel.Replace("Placeholder", Me.Text.Replace("open the add window to continue", ""))
            Dim LiAdd As String = Nothing
            For ii As Integer = 0 To ItemList.Count - 1
                For i As Integer = 0 To liList.Count - 1
                    If CBool(InStr(liList(i), "<!-- " + ItemList.Item(ii).GetNameAnime + "-->")) Then
                        If CBool(InStr(liList(i), "Finished - ")) Then
                            If LiAdd = Nothing Then
                                LiAdd = liList(i)
                            Else
                                LiAdd = LiAdd + vbNewLine + liList(i)
                            End If
                        Else
                            Dim ProzentBalken As String() = liList(i).Split(New String() {"width:"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim ProzentBalken2 As String() = ProzentBalken(1).Split(New String() {"%" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim ProzentZahl As String() = ProzentBalken2(1).Split(New String() {"'percenttext'>"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim ProzentZahl2 As String() = ProzentZahl(1).Split(New String() {"%<"}, System.StringSplitOptions.RemoveEmptyEntries)

                            liList(i) = ProzentBalken(0) + "width:" + ItemList.Item(ii).GetPercentValue.ToString + "%" + Chr(34) + ProzentZahl(0) + "'percenttext'>" + ItemList.Item(ii).GetLabelPercent.ToString + "<" + ProzentZahl2(1)
                            If LiAdd = Nothing Then
                                LiAdd = liList(i)
                            Else
                                LiAdd = LiAdd + vbNewLine + liList(i)
                            End If
                            Exit For
                        End If
                    End If
                Next
            Next
            Dim c As String = GeckoHTML + vbNewLine + LiAdd + vbNewLine + My.Resources.htmlEnd

            Dim Balken As String = "balken.png"
            c = c.Replace("balken1.png", Balken)
            Dim CC As String = "cc.png"
            c = c.Replace("cc1.png", CC)
            HTML = c
        Catch ex As Exception
            'Debug.WriteLine(ex.ToString)
            'MsgBox(ex.ToString)
        End Try
    End Sub

#Region "process html"

    Public Sub ProcessHTML(ByVal document As String, ByVal Address As String, ByVal DocumentTitle As String)
        Dim localHTML As String = document
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        Debug.WriteLine(Address)

        If CBool(InStr(Address, "title-api.prd.funimationsvc.com")) Then

            If FunimationJsonBrowser = "EpisodeJson" Then
                Anime_Add.FillFunimationEpisodes(localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; white-space: pre-wrap;" + Chr(34) + ">", "")) '
                FunimationJsonBrowser = Nothing
                WebbrowserURL = "https://funimation.com/js"
            ElseIf FunimationJsonBrowser = "v1Json" Then
                GetFunimationJS_VideoProxy(Nothing, localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; white-space: pre-wrap;" + Chr(34) + ">", "")) '
                FunimationJsonBrowser = Nothing
                WebbrowserURL = "https://funimation.com/js"
            End If

            Exit Sub
        ElseIf CBool(InStr(Address, "/data/v2/shows/")) Then

            If FunimationJsonBrowser = "SeasonJson" Then
                Me.Invoke(New Action(Function() As Object
                                         'My.Computer.Clipboard.SetText(localHTML)
                                         FunimationSeasonAPIUrl = Address
                                         GetFunimationJS_Seasons(Nothing, localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; white-space: pre-wrap;" + Chr(34) + ">", "")) '
                                         FunimationJsonBrowser = Nothing
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))

            End If
            Exit Sub

        ElseIf CBool(InStr(Address, "wakanim.tv")) Then

            If CBool(InStr(document, "var tracks = [{" + Chr(34) + "file" + Chr(34) + ":" + Chr(34))) Then
                Dim WakanimSub() As String = document.Split(New String() {"var tracks = [{" + Chr(34) + "file" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim WakanimSub2() As String = WakanimSub(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                Try
                    Using client As New WebClient()
                        client.Encoding = System.Text.Encoding.UTF8
                        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))

                        Dim SaveName As String = System.Text.RegularExpressions.Regex.Replace(DocumentTitle.Replace(" - Schaue legal auf Wakanim.TV", ""), "[^\w\\-]", " ").Replace(":", "")
                        SaveName = RemoveExtraSpaces(SaveName)


                        client.DownloadFile(WakanimSub2(0), Pfad + "\" + SaveName + ".vtt")


                    End Using
                Catch ex As Exception
                    'Debug.WriteLine("error- getting funimation SeasonJson data")
                    'FunimationJsonBrowser = "SeasonJson"
                    'Navigate(JsonUrl)
                    ''Navigate(JsonUrl)

                    Exit Sub
                End Try
            End If

        End If


        If b = True Then
            Exit Sub
        End If


        'MsgBox("loaded!")


        If CBool(InStr(Address, "beta.crunchyroll.com")) Then
            WebbrowserURL = Address
            Pause(10)
            ProcessUrls()
            Exit Sub

        ElseIf CBool(InStr(Address, "crunchyroll.com")) Then
            If b = False Then
                Try
                    If Address = "https://www.crunchyroll.com/" Then

                    ElseIf Address = "https://www.crunchyroll.com/en-gb" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/es" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/es-es" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/pt-br" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/pt-pt" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/fr" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/de" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/ar" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/it" Then
                        b = True
                    ElseIf Address = "https://www.crunchyroll.com/ru" Then
                        b = True
                    ElseIf CBool(InStr(localHTML, "hardsub_lang")) Then
                        Debug.WriteLine("starting grabber")
                        WebbrowserURL = Address
                        WebbrowserText = localHTML
                        WebbrowserTitle = DocumentTitle
                        WebbrowserHeadText = localHTML
                        b = True
                        Debug.WriteLine("Invoke Required: " + InvokeRequired.ToString)
                        'Dim Evaluator = New Thread(Sub() Me.GrappURL())

                        'Evaluator.Start()
                        t = New Thread(AddressOf GrappURL)
                        t.Priority = ThreadPriority.Normal
                        t.IsBackground = True
                        t.Start()


                        Exit Sub
                    ElseIf CBool(InStr(localHTML, "season-dropdown content-menu block")) Then
                        b = True
                        Anime_Add.TextBox2.Text = "Use Custom Name"
                        WebbrowserURL = Address
                        WebbrowserText = localHTML
                        WebbrowserTitle = DocumentTitle
                        WebbrowserHeadText = localHTML

                        SeasonDropdownGrapp()
                        Exit Sub
                    ElseIf CBool(InStr(localHTML, "wrapper container-shadow hover-classes")) Then
                        b = True
                        Anime_Add.TextBox2.Text = "Use Custom Name"
                        WebbrowserURL = Address
                        WebbrowserText = localHTML
                        WebbrowserTitle = DocumentTitle
                        WebbrowserHeadText = localHTML
                        MassGrapp()
                        Exit Sub
                    Else
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\html.log", localHTML, True)
                        b = True
                        MsgBox(No_Stream, MsgBoxStyle.OkOnly)
                        Anime_Add.StatusLabel.Text = "Status: idle"
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString)
                    Anime_Add.StatusLabel.Text = "Status: idle"
                End Try
            ElseIf c = False Then
                If CBool(InStr(localHTML, "hardsub_lang")) Then
                    c = True
                    WebbrowserURL = Address
                    WebbrowserText = localHTML
                    WebbrowserTitle = DocumentTitle
                    WebbrowserHeadText = localHTML
                    'SoftSub.DownloadSubs()
                    Exit Sub
                End If


            End If


        ElseIf CBool(InStr(Address, "funimation.com")) Then
            Dim Collector As New TaskCookieVisitor
            Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
            CM.VisitAllCookies(Collector)

            Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
            Dim Cookie As String = ""
            For i As Integer = 0 To list.Count - 1

                If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                    Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
                End If

                If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                    FunimationToken = "Token " + list.Item(i).Value
                End If
            Next

            If b = False Then

                If CBool(InStr(localHTML, My.Resources.Funimation_Player_ID)) = True Or CBool(InStr(localHTML, My.Resources.Funimation_Player_ID_2)) = True Then
                    WebbrowserURL = Address
                    WebbrowserText = localHTML
                    WebbrowserTitle = DocumentTitle
                    WebbrowserHeadText = localHTML

                    WebbrowserCookie = Cookie
                    b = True

                    t = New Thread(AddressOf Funitmation_Grapp)
                    t.Priority = ThreadPriority.Normal
                    t.IsBackground = True
                    t.Start()
                    Exit Sub
                Else
                    WebbrowserCookie = Cookie
                    WebbrowserURL = Address
                    Text = "Crunchyroll Downloader"
                    For i As Integer = 10 To 0 Step -1
                        Anime_Add.StatusLabel.Text = "Status: checking traffic - " + i.ToString
                        Pause(1)
                    Next
                    Dim Evaluator = New Thread(Sub() Me.ProcessUrls())
                    Evaluator.Start()

                    Exit Sub
                End If
            End If


        ElseIf CBool(InStr(Address, "anime-on-demand.de")) Then
            Dim Collector As New TaskCookieVisitor
            Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
            CM.VisitAllCookies(Collector)

            Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
            Dim Cookie As String = ""
            For i As Integer = 0 To list.Count - 1

                If CBool(InStr(list.Item(i).Domain, "anime-on-demand.de")) Then 'list.Item(i).Domain = "funimation.com" Then
                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
                    Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
                End If
            Next

            If b = False Then
                b = True
                WebbrowserURL = Address
                WebbrowserText = localHTML
                WebbrowserTitle = DocumentTitle
                Anime_Add.AoD_Cookie = Cookie
                Anime_Add.AoDHTML = localHTML


                Anime_Add.ProcessAoDNew()
                'Anime_Add.ProcessAoD()
                Exit Sub


            End If
        Else
            WebbrowserURL = Address
            Text = "Crunchyroll Downloader"
            For i As Integer = 10 To 0 Step -1
                Anime_Add.StatusLabel.Text = "Status: checking traffic - " + i.ToString
                Pause(1)
            Next
            ProcessUrls()

            'Pause(10)
            'ProcessUrls()
        End If


        'End If
    End Sub



#End Region
    Public Sub ProcessUrls()

        Debug.WriteLine(LoadedUrls.Count.ToString)
        Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)

        Dim VRVSeason As String = Nothing

        For i As Integer = 0 To LoadedUrls.Count - 1

            Dim requesturl As String = LoadedUrls.Item(i)

            If CBool(InStr(requesturl, "https://beta-api.crunchyroll.com/")) And CBool(InStr(requesturl, "streams?")) Then

                If b = False Then
                    GetBetaVideoProxy(requesturl, WebbrowserURL)
                    b = True
                    LoadedUrls.Clear()
                    Exit Sub
                End If

            ElseIf CBool(InStr(requesturl, "https://beta-api.crunchyroll.com/")) And CBool(InStr(requesturl, "seasons?series_id=")) Then

                If b = False Then
                    GetBetaSeasons(requesturl)
                    b = True
                    LoadedUrls.Clear()
                    Exit Sub
                End If

            End If

            If CBool(InStr(requesturl, "https://api.vrv.co")) And CBool(InStr(requesturl, "streams?")) Then
                Debug.WriteLine("vrv-1 " + requesturl)
                If b = False Then
                    Get_VRV_VideoProxy(requesturl, WebbrowserURL)
                    b = True
                    LoadedUrls.Clear()
                    Exit Sub
                End If

            ElseIf CBool(InStr(requesturl, "https://api.vrv.co")) And CBool(InStr(requesturl, "seasons?series_id=")) Then

                If b = False Then
                    'GetBetaSeasons(requesturl)
                    VRVSeason = requesturl
                    'b = True
                    'LoadedUrls.Clear()
                    'Exit Sub
                End If
            End If
            If CBool(InStr(requesturl, "/data/v2/shows/")) Then
                b = True
                'MsgBox("The new Funimation Overview is not supportet yet!", MsgBoxStyle.Information)


                Me.Invoke(New Action(Function() As Object
                                         'My.Computer.Clipboard.SetText(localHTML)
                                         GetFunimationJS_Seasons(requesturl)
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))




                LoadedUrls.Clear()
                Exit Sub


                'Dim ShowJson As String = Nothing

                'Try
                '    Using client As New WebClient()
                '        client.Encoding = System.Text.Encoding.UTF8
                '        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                '        ShowJson = client.DownloadString(requesturl)
                '    End Using
                'Catch ex As Exception
                '    Debug.WriteLine("error- getting ShowJson data")

                'End Try




            End If
            If CBool(InStr(requesturl, "data/v1/episodes/")) Then
                b = True
                'MsgBox("The new Funimation Overview is not supportet yet!", MsgBoxStyle.Information)


                Me.Invoke(New Action(Function() As Object
                                         'My.Computer.Clipboard.SetText(localHTML)
                                         GetFunimationNewJS_VideoProxy(requesturl)
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))




                LoadedUrls.Clear()
                Exit Sub

            End If
            If CBool(InStr(requesturl, "https://title-api.prd.funimationsvc.com")) And CBool(InStr(requesturl, "?region=")) Then
                Try
                    Dim Collector As New TaskCookieVisitor
                    Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
                    CM.VisitAllCookies(Collector)

                    Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
                    Dim Cookie As String = ""
                    For ii As Integer = 0 To list.Count - 1

                        If CBool(InStr(list.Item(ii).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                            'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
                            Cookie = Cookie + list.Item(ii).Name + "=" + list.Item(ii).Value + ";"
                        End If
                    Next
                    WebbrowserCookie = Cookie
                Catch ex As Exception
                End Try

                If FunimationAPIRegion = Nothing Then

                    Me.Invoke(New Action(Function() As Object
                                             Dim parms As String() = requesturl.Split(New String() {"?region="}, System.StringSplitOptions.RemoveEmptyEntries)
                                             FunimationAPIRegion = "?region=" + parms(1)
                                             Return Nothing
                                         End Function))

                End If

                If b = False Then
                    'If CBool(InStr(requesturl, "https://title-api.prd.funimationsvc.com/v1/episodes/")) Then
                    '    GetFunimationJS_VideoProxy(requesturl)
                    '    Debug.WriteLine("processing :" + requesturl)
                    '    b = True
                    '    Exit For

                    'Else
                    If CBool(InStr(requesturl, "https://title-api.prd.funimationsvc.com/v1/show")) And CBool(InStr(requesturl, "/episodes/")) Then
                        b = True
                        Try
                            Dim Collector As New TaskCookieVisitor
                            Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
                            CM.VisitAllCookies(Collector)

                            Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
                            Dim Cookie As String = ""
                            For ii As Integer = 0 To list.Count - 1

                                If CBool(InStr(list.Item(ii).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
                                    Cookie = Cookie + list.Item(ii).Name + "=" + list.Item(ii).Value + ";"
                                End If
                            Next
                            WebbrowserCookie = Cookie
                        Catch ex As Exception
                        End Try
                        GetFunimationJS_VideoProxy(requesturl)

                        Debug.WriteLine("processing :" + requesturl)

                        LoadedUrls.Clear()

                        Exit Sub

                        'Else
                        '    If FunimationEpisodeJSON = Nothing Then
                        '        Debug.WriteLine("processing overview")
                        '        Me.Invoke(New Action(Function() As Object
                        '                                 'MsgBox(WebbrowserURL)
                        '                                 Anime_Add.ProcessFunimationJS(WebbrowserURL)
                        '                                 Return Nothing
                        '                             End Function))
                        '        b = True
                        '        LoadedUrls.Clear()
                        '        Exit Sub
                        'End If

                    End If


                End If
            End If
        Next


        If Not VRVSeason = Nothing Then
            Debug.WriteLine("vrv-2 " + VRVSeason)
            Get_VRV_Seasons(VRVSeason)
            b = True
            LoadedUrls.Clear()
            Exit Sub

        End If

        'If GeckoFX.ScanTrue = True Then

        '    If CBool(InStr(requesturl, ".m3u8") Then
        '        Dim client0 As New WebClient
        '        client0.Encoding = Encoding.UTF8
        '        'client0.Headers.Add(HttpRequestHeader.Cookie, e.Channel.GetRequestHeader("Cookie"))
        '        Dim str0 As String = client0.DownloadString(requesturl)

        '        If CBool(InStr(str0, "#EXTM3U") Then
        '            m3u8List.Add(requesturl)
        '        Else
        '            Dim DecodedUrl As String = UrlDecode(requesturl)
        '            'MsgBox(DecodedUrl)
        '            Dim URLSplit() As String = DecodedUrl.Split(New String() {".m3u8"}, System.StringSplitOptions.RemoveEmptyEntries)
        '            Dim URLSplit2() As String = URLSplit(0).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
        '            Dim NewUrl As String = "https://" + URLSplit2(URLSplit2.Count - 1) + ".m3u8" + URLSplit(1)
        '            'MsgBox(NewUrl)
        '            Dim str1 As String = client0.DownloadString(NewUrl)
        '            'MsgBox(str1)
        '            If CBool(InStr(str1, "#EXTM3U") Then
        '                m3u8List.Add(NewUrl)
        '            End If

        '        End If
        '    ElseIf CBool(InStr(requesturl, ".mpd") Then
        '        mpdList.Add(requesturl)
        '    ElseIf CBool(InStr(requesturl, "googlevideo.com")) And CBool(InStr(requesturl, "&range=")) = True Then

        '        Dim DecodedUrl As String = UrlDecode(requesturl)
        '        'MsgBox(DecodedUrl)
        '        Dim VideoUrl() As String = DecodedUrl.Split(New String() {"&range="}, System.StringSplitOptions.RemoveEmptyEntries)
        '        Dim VideoUrl2() As String = VideoUrl(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
        '        Dim NewUrl As String = VideoUrl(0) + "&" + VideoUrl2(1)
        '        'Debug.WriteLine(NewUrl)

        '        If Not mpdList.Contains(NewUrl) Then
        '            mpdList.Add(NewUrl)
        '        End If


        '    ElseIf CBool(InStr(requesturl, ".txt") Then
        '        txtList.Add(requesturl)
        '    ElseIf CBool(InStr(requesturl, ".vtt") Then
        '        txtList.Add(requesturl)
        '    ElseIf CBool(InStr(requesturl, ".srt") Then
        '        txtList.Add(requesturl)
        '    ElseIf CBool(InStr(requesturl, ".ass") Then
        '        txtList.Add(requesturl)
        '    ElseIf CBool(InStr(requesturl, ".ssa") Then
        '        txtList.Add(requesturl)
        '    End If
        'End If

        LoadedUrls.Clear()

    End Sub

    Public Sub Navigate(ByVal Url As String)
        If Application.OpenForms().OfType(Of CefSharp_Browser).Any = True Then
            If InvokeRequired = True Then
                Me.Invoke(New Action(Function() As Object
                                         CefSharp_Browser.WebBrowser1.Load(Url)
                                         Return Nothing
                                     End Function))
            Else
                CefSharp_Browser.WebBrowser1.Load(Url)
            End If

        Else
            If InvokeRequired = True Then
                Me.Invoke(New Action(Function() As Object
                                         CefSharp_Browser.Show()
                                         CefSharp_Browser.WebBrowser1.Load(Url)
                                         Return Nothing
                                     End Function))
            Else
                CefSharp_Browser.Show()
                CefSharp_Browser.WebBrowser1.Load(Url)
            End If
        End If
    End Sub

#Region "server"


    Dim ListOfThread As New List(Of Thread)

    Sub ServerStart()
        Dim server As TcpListener
        server = Nothing
        Try


            Dim Port As String = StartServer.ToString

            Dim localAddr As IPAddress = IPAddress.Parse("127.0.0.1")

            server = New TcpListener(localAddr, Int32.Parse(Port))

            ' Start listening for client requests.
            server.Start()

            Debug.WriteLine("Web server started at: " & localAddr.ToString() & ":" & Port)

            While True

                Dim client As TcpClient = server.AcceptTcpClient()
                Dim clientThread As New Thread(Sub() Me.ManageConnections(client))
                clientThread.Start()

            End While

        Catch ex As SocketException
            Debug.WriteLine("SocketException: " + ex.ToString)


        Finally
            Debug.WriteLine(Date.Now.ToString + " " + "End server")
            server.Stop()
        End Try
        Debug.WriteLine(ControlChars.Cr + "Hit enter to continue....")

    End Sub
    Sub ManageConnections(ByVal client As TcpClient)
        Dim bytes(1048576) As Byte

        Dim stream As NetworkStream = client.GetStream()

        ' Debug.WriteLine(Date.Now + " " + "stream opend")

        Dim numberOfBytesRead As Integer = 0
        Dim myCompleteMessage As StringBuilder = New StringBuilder()

        Dim stopWatch As New Stopwatch()
        stopWatch.Start()

        Do While stopWatch.Elapsed.TotalSeconds < 4 And stream.DataAvailable

            'Debug.WriteLine(Date.Now + " " + numberOfBytesRead.ToString + " " + stopWatch.Elapsed.TotalSeconds.ToString)
            numberOfBytesRead = stream.Read(bytes, 0, bytes.Length)
            myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(bytes, 0, numberOfBytesRead))

        Loop
        stopWatch.Stop()

        ProcessRequest(stream, myCompleteMessage.ToString())

        client.Close()
    End Sub


    Sub ProcessRequest(ByVal stream As NetworkStream, ByVal htmlReq As String)
        ' Debug.WriteLine(htmlReq)
        Dim recvBytes(1048576) As Byte

        Try
            Dim rootPath As String = Directory.GetCurrentDirectory() & "\WebInterface\"
            ' Set default page
            Dim defaultPage As String = "index.html"
            Dim PostPage As String = "post.html"
            Dim strArray() As String
            Dim strRequest As String
            strArray = htmlReq.Trim.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(htmlReq)

            If strArray(0).Trim().ToUpper.Equals("POST") Then

                'Debug.WriteLine("receiving data from the add-on")
                'Debug.WriteLine(htmlReq)
                'UrlDecode
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: receiving data from the add-on"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
#Region "CR Einzeln"
                If CBool(InStr(htmlReq, "HTMLSingle=")) Then
                    Debug.WriteLine("Single episode mode - Crunchyroll")

                    Try
                        Dim html() As String = htmlReq.Split(New String() {"HTMLSingle="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {My.Resources.CR_Head_Url_Split}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLSplit2() As String = URLSplit(1).Split(New String() {Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserURL = URLSplit2(0)
                        Dim BodySplit() As String = DecodedHTML.Split(New String() {"<body"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserText = BodySplit(1)
                        WebbrowserHeadText = BodySplit(0)
                        Dim TitleSplit() As String = DecodedHTML.Split(New String() {"<title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim TitleSplit2() As String = TitleSplit(1).Split(New String() {"</title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserTitle = TitleSplit2(0)
                        If CBool(InStr(WebbrowserText, "hardsub_lang")) Then
                            Me.Invoke(New Action(Function() As Object
                                                     Me.Text = "Status: Download added from add-on"
                                                     Me.Invalidate()
                                                     Return Nothing
                                                 End Function))
                            If Grapp_RDY = True Then
                                Dim t As Thread
                                t = New Thread(AddressOf GrappURL)
                                t.Priority = ThreadPriority.Normal
                                t.IsBackground = True
                                t.Start()
                            Else
                                If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                                    Me.Invoke(New Action(Function() As Object
                                                             If Anime_Add.ListBox1.Items.Contains(WebbrowserURL) = False Then
                                                                 Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                                                             End If
                                                             'Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                                                             Return Nothing
                                                         End Function))

                                Else
                                    If ListBoxList.Contains(WebbrowserURL) = False Then
                                        ListBoxList.Add(WebbrowserURL)
                                    End If
                                    'ListBoxList.Add(WebbrowserURL)
                                End If
                            End If
                            strRequest = rootPath & "Post_Single_Sucess.html" 'PostPage
                            SendHTMLResponse(strRequest, stream)
                        Else
                            Me.Invoke(New Action(Function() As Object
                                                     Me.Text = "Status: no video found"
                                                     Me.Invalidate()
                                                     Return Nothing
                                                 End Function))
                            Dim ErrorPage As String = My.Resources.Post_error_Top + "no video found" + My.Resources.Post_error_Bottom
                            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                            strRequest = rootPath & "error_Page.html" 'PostPage
                            SendHTMLResponse(strRequest, stream)
                        End If
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        SendHTMLResponse(strRequest, stream)
                    End Try
#End Region
#Region "mass-dl"


                ElseIf CBool(InStr(htmlReq, "HTMLMass=")) Then
                    Debug.WriteLine("multi episode mode")

                    Try
                        Dim html() As String = htmlReq.Split(New String() {"HTMLMass="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {"javascript:"}, System.StringSplitOptions.RemoveEmptyEntries)
                        If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                            For i As Integer = 0 To URLSplit.Count - 1
                                Dim ii As Integer = i
                                Me.Invoke(New Action(Function() As Object
                                                         If Anime_Add.ListBox1.Items.Contains(URLSplit(ii)) = False Then
                                                             Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                                                         End If
                                                         'Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                                                         Return Nothing
                                                     End Function))
                            Next
                        Else

                            For i As Integer = 0 To URLSplit.Count - 1
                                If ListBoxList.Contains(URLSplit(i)) = False Then
                                    ListBoxList.Add(URLSplit(i))
                                End If

                            Next
                            Me.Invoke(New Action(Function() As Object
                                                     Me.Text = "Status: " + ListBoxList.Count.ToString + " Downloads in queue" + vbNewLine + "open the add window to continue"
                                                     Me.Invalidate()
                                                     Return Nothing
                                                 End Function))
                        End If
                        strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                        SendHTMLResponse(strRequest, stream)
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        SendHTMLResponse(strRequest, stream)
                    End Try
#End Region
#Region "Funimation-mass"


                ElseIf CBool(InStr(htmlReq, "FunimationMass=")) Then
                    Debug.WriteLine("Funimation multi episode mode")

                    Try
                        Dim DecodedHTML As String = UrlDecode(htmlReq)
                        If CBool(InStr(DecodedHTML, "&FunimationCookie=")) Then




                            Dim CookieSplit() As String = DecodedHTML.Split(New String() {"&FunimationCookie="}, System.StringSplitOptions.RemoveEmptyEntries)
                            SystemWebBrowserCookie = CookieSplit(1)

                            Dim URLSplit() As String = CookieSplit(0).Split(New String() {"FunimationMass="}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim URLSplit2() As String = URLSplit(1).Split(New String() {"javascript:"}, System.StringSplitOptions.RemoveEmptyEntries)


                            If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                                For i As Integer = 0 To URLSplit2.Count - 1
                                    Dim ii As Integer = i
                                    Me.Invoke(New Action(Function() As Object
                                                             If Anime_Add.ListBox1.Items.Contains(URLSplit2(ii)) = False Then
                                                                 Anime_Add.ListBox1.Items.Add(URLSplit2(ii))
                                                             End If
                                                             'Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                                                             Return Nothing
                                                         End Function))
                                Next
                            Else

                                For i As Integer = 0 To URLSplit2.Count - 1
                                    If ListBoxList.Contains(URLSplit2(i)) = False Then
                                        ListBoxList.Add(URLSplit2(i))
                                    End If

                                Next
                                Me.Invoke(New Action(Function() As Object
                                                         Me.Text = "Status: " + ListBoxList.Count.ToString + " Downloads in queue" + vbNewLine + "open the add window to continue"
                                                         Me.Invalidate()
                                                         Return Nothing
                                                     End Function))
                            End If
                            strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                            SendHTMLResponse(strRequest, stream)

                        End If
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        SendHTMLResponse(strRequest, stream)
                    End Try
#End Region
#Region "funimation Einzeln"
                ElseIf CBool(InStr(htmlReq, "FunimationURL=")) Then
                    Debug.WriteLine("single episode mode - Funimation")
                    'MsgBox(htmlReq)

                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Download added from add-on"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))
                    Try
                        Dim URLSplit() As String = htmlReq.Split(New String() {"FunimationURL="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLSplit2() As String = URLSplit(1).Split(New String() {"&FunimationCookie="}, System.StringSplitOptions.RemoveEmptyEntries)
                        SystemWebBrowserCookie = URLSplit2(1)
                        WebbrowserURL = UrlDecode(URLSplit2(0))

                        If CBool(InStr(WebbrowserURL, "funimation.com")) Then
                            If DubFunimation = "Disabled" Then
                            Else
                                If CBool(InStr(WebbrowserURL, "?lang=")) Then
                                    Dim ClearUri As String() = WebbrowserURL.Split(New String() {"?lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then
                                        If CBool(InStr(ClearUri(1), "&")) Then
                                            Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim Parms As String = Nothing
                                            For i As Integer = 1 To ClearUri2.Count - 1
                                                Parms = Parms + "&" + ClearUri2(i)
                                            Next
                                            WebbrowserURL = ClearUri(0) + "?lang=" + DubFunimation + Parms
                                        Else
                                            WebbrowserURL = ClearUri(0) + "?lang=" + DubFunimation
                                        End If
                                    Else
                                        WebbrowserURL = ClearUri(0) + "?lang=" + DubFunimation
                                    End If
                                ElseIf CBool(InStr(WebbrowserURL, "&lang=")) Then
                                    Dim ClearUri As String() = WebbrowserURL.Split(New String() {"&lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then

                                        If CBool(InStr(ClearUri(1), "&")) Then
                                            Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim Parms As String = Nothing
                                            For i As Integer = 1 To ClearUri2.Count - 1
                                                Parms = Parms + "&" + ClearUri2(i)
                                            Next
                                            WebbrowserURL = ClearUri(0) + "&lang=" + DubFunimation + Parms
                                        Else
                                            WebbrowserURL = ClearUri(0) + "&lang=" + DubFunimation
                                        End If
                                    Else
                                        WebbrowserURL = ClearUri(0) + "&lang=" + DubFunimation
                                    End If

                                ElseIf CBool(InStr(WebbrowserURL, "?")) Then
                                    WebbrowserURL = WebbrowserURL + "&lang=" + DubFunimation
                                Else
                                    WebbrowserURL = WebbrowserURL + "?lang=" + DubFunimation
                                End If
                            End If

                        End If


                        If Funimation_Grapp_RDY = True Then
                            If RunningDownloads >= MaxDL Then
                                If ListBoxList.Contains(WebbrowserURL) = False Then
                                    ListBoxList.Add(WebbrowserURL)
                                End If
                                'ListBoxList.Add(WebbrowserURL)
                            Else
                                Me.Invoke(New Action(Function() As Object
                                                         Navigate(WebbrowserURL)
                                                         Return Nothing
                                                     End Function))

                                b = False



                            End If

                        Else
                            If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                                Me.Invoke(New Action(Function() As Object
                                                         If Anime_Add.ListBox1.Items.Contains(WebbrowserURL) = False Then
                                                             Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                                                         End If

                                                         Return Nothing
                                                     End Function))

                            Else
                                If ListBoxList.Contains(WebbrowserURL) = False Then
                                    ListBoxList.Add(WebbrowserURL)
                                End If
                                Me.Invoke(New Action(Function() As Object
                                                         Me.Text = "Status: " + ListBoxList.Count.ToString + " Downloads in queue"
                                                         Me.Invalidate()
                                                         Return Nothing
                                                     End Function))
                            End If
                        End If
                        strRequest = rootPath & "Post_Single_Sucess.html" 'PostPage
                        SendHTMLResponse(strRequest, stream)
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        SendHTMLResponse(strRequest, stream)
                    End Try

#End Region
                Else

                    strRequest = rootPath & "error_Page_default.html" 'PostPage
                    SendHTMLResponse(strRequest, stream)
                End If


            ElseIf strArray(0).Trim().ToUpper.Equals("GET") Then
                'Debug.WriteLine(Date.Now + " " + "found GET while procesing")

                'Debug.WriteLine(Date.Now + " " + strArray(1))
                strRequest = strArray(1).Trim

                If strRequest.StartsWith("/") Then
                    strRequest = strRequest.Substring(1)

                End If

                If strRequest.EndsWith("/") Or strRequest.Equals("") Then
                    'Debug.WriteLine(Date.Now + " " + "it's index.html")
                    strRequest = strRequest & defaultPage '"HTMLString" 'strRequest & defaultPage

                End If

                'If CBool(InStr(htmlReq, "CRD_Handshake") Then
                '    'Debug.WriteLine(Date.Now + " " + "it's a handshake")
                '    SendHTMLResponse("Handshake_Confirm", stream)
                'Else
                '    
                'End If
                strRequest = rootPath & strRequest
                SendHTMLResponse(strRequest, stream)

            Else ' Not HTTP GET method
                'Debug.WriteLine(Date.Now + " " + "empty request, returning index.html")
                'Debug.WriteLine(Date.Now + " " + strArray(0))
                strRequest = rootPath & defaultPage
                SendHTMLResponse(strRequest, stream)

            End If

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
            'strRequest = rootPath & "error_Page.html" 'PostPage
            SendHTMLResponse(Application.StartupPath + "\WebInterface\error_Page.html", stream)

        End Try
    End Sub
    ' Send HTTP Response




    Private Sub SendHTMLResponse(ByVal httpRequest As String, ByVal stream As NetworkStream)
        Try

            Dim respByte() As Byte
            If CBool(InStr(httpRequest, "index.html")) Then
                Debug.WriteLine(httpRequest)
                respByte = System.Text.Encoding.UTF8.GetBytes(HTML) 'File.ReadAllBytes("") '


                ' Set HTML Header
                Dim htmlHeader As String =
                    "HTTP/1.0 200 OK" & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                   "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & GetContentType(httpRequest) &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                'Debug.WriteLine("HTML Header: " & ControlChars.CrLf & htmlHeader)
                ' Send HTML Header back to Web Browser
                'Dim response() As Byte = headerByte.Concat(respByte).ToArray()

                ' stream.Write(response, 0, response.Length)
                'Debug.WriteLine("sending headers")
                stream.Write(headerByte, 0, headerByte.Length)
                'Debug.WriteLine("headers send")
                'Debug.WriteLine("sending content")
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
                'clientSocket.Send(respByte, 0, respByte.Length, SocketFlags.None)
                ' Close HTTP Socket connection
                'Debug.WriteLine("content send")
            ElseIf File.Exists(httpRequest) Then
                Debug.WriteLine(httpRequest)
                respByte = File.ReadAllBytes(httpRequest)

                ' Set HTML Header
                Dim htmlHeader As String =
                    "HTTP/1.0 200 OK" & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                   "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & GetContentType(httpRequest) & ControlChars.CrLf &
                    "Connection: close" &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                ' Send HTML Header back to Web Browser
                stream.Write(headerByte, 0, headerByte.Length)
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
            ElseIf httpRequest = "Handshake_Confirm" Then
                respByte = System.Text.Encoding.UTF8.GetBytes("CRD_Handshake_Confirm") 'File.ReadAllBytes("") '
                Dim htmlHeader As String =
                    "HTTP/1.0 200 OK" & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                    "Access-Control-Allow-Origin: *" & ControlChars.CrLf &
                    "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: text/plain" &
                    "Connection: close" &
                      ControlChars.CrLf & ControlChars.CrLf
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                stream.Write(headerByte, 0, headerByte.Length)
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)

                Debug.WriteLine("content send")
            Else

                respByte = Encoding.UTF8.GetBytes(My.Resources.Error_404) 'File.ReadAllBytes(httpRequest)
                Debug.WriteLine("404 Not Found : " + httpRequest)
                ' Set HTML Header
                Dim htmlHeader As String =
                "HTTP/1.0 404 Not Found" & ControlChars.CrLf &
                "Server: WebServer 1.0" & ControlChars.CrLf &
                "Connection: close" &
                 ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)

                ' Send HTML Header back to Web Browser
                stream.Write(headerByte, 0, headerByte.Length)

                'stream.Write(headerByte, 0, headerByte.Length, SocketFlags.None)
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())


        End Try
    End Sub



    ' Get Content Type
    Private Function GetContentType(ByVal httpRequest As String) As String
        If (httpRequest.EndsWith("html")) Then
            Return "text/html"
        ElseIf (httpRequest.EndsWith("htm")) Then
            Return "text/html"
        ElseIf (httpRequest.EndsWith("txt")) Then
            Return "text/plain"
        ElseIf (httpRequest.EndsWith("gif")) Then
            Return "image/gif"
        ElseIf (httpRequest.EndsWith("jpg")) Then
            Return "image/jpeg"
        ElseIf (httpRequest.EndsWith("jpg")) Then
            Return "image/jpeg"
        ElseIf (httpRequest.EndsWith("ico")) Then
            Return "image/x-icon"
        ElseIf (httpRequest.EndsWith("png")) Then
            Return "image/png"
        ElseIf (httpRequest.EndsWith("jpeg")) Then
            Return "image/jpeg"
        ElseIf (httpRequest.EndsWith("pdf")) Then
            Return "application/pdf"
        ElseIf (httpRequest.EndsWith("pdf")) Then
            Return "application/pdf"
        ElseIf (httpRequest.EndsWith("doc")) Then
            Return "application/msword"
        ElseIf (httpRequest.EndsWith("xls")) Then
            Return "application/vnd.ms-excel"
        ElseIf (httpRequest.EndsWith("ppt")) Then
            Return "application/vnd.ms-powerpoint"
        ElseIf (httpRequest.EndsWith("js")) Then
            Return "application/javascript"
        ElseIf (httpRequest.EndsWith("ass")) Then
            Return "application/octet-stream"
        ElseIf (httpRequest.EndsWith("check")) Then
            Return "application/json"
        Else
            Return "text/plain"
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ErrorDialog.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        ErrorDialog.ShowDialog()
    End Sub



    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        network_scan.Show()
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        If ListBoxList.Count > 0 Then
            If CBool(InStr(Me.Text, "Crunchyroll Downloader")) Then
                Me.Text = "Status: " + ListBoxList.Count.ToString + " Downloads in queue" + vbNewLine + "open the add window to continue"
            End If
        End If
    End Sub

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Btn_add.Image = My.Resources.main_add
        ListView1.Select()
    End Sub

    Private Sub TestDownloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestDownloadToolStripMenuItem.Click

        Dim Collector As New TaskCookieVisitor
        Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
        CM.VisitAllCookies(Collector)
        Dim Token As String = Nothing
        Dim DeviceRegion As String = Nothing
        Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
        Dim Cookie As String = ""
        For i As Integer = 0 To list.Count - 1

            If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
            End If

            If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
                'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                Token = "Token " + list.Item(i).Value
            End If
            If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "region")) Then 'list.Item(i).Domain = "funimation.com" Then
                'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)

                DeviceRegion = "?deviceType=web&" + list.Item(i).Name + "=" + list.Item(i).Value
            End If
        Next
        ' region=US;
        If Token = Nothing Then
            MsgBox("No Token has been found...", MsgBoxStyle.Exclamation)
        Else
            FunimationToken = Token
            MsgBox("Token found!", MsgBoxStyle.Information)

        End If

    End Sub


#End Region


End Class

Public Class FunimationOverview
    Public ID As String
    Public Title As String
    Public Slug As String

    Public Sub New(ByVal Slug As String, ByVal ID As String, ByVal Title As String)
        Me.ID = ID
        Me.Title = Title
        Me.Slug = Slug
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.Slug, Me.ID, Me.Title)
    End Function


End Class

Public Class FunimationSubs
    Public LangugageCode As String
    Public Url As String
    Public Format As String

    Public Sub New(ByVal LangugageCode As String, ByVal Format As String, ByVal Url As String)
        Me.Url = Url
        Me.LangugageCode = LangugageCode
        Me.Format = Format
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.LangugageCode, Me.Format, Me.Url)
    End Function


End Class

Public Class FunimationStream
    Public audioLanguage As String
    Public Url As String
    Public version As String
    Public Primary As Boolean
    Public Sub New(ByVal audioLanguage As String, ByVal version As String, ByVal Url As String, ByVal Primary As Boolean)
        Me.Primary = Primary
        Me.Url = Url
        Me.audioLanguage = audioLanguage
        Me.version = version
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.audioLanguage, Me.version, Me.Url)
    End Function


End Class






