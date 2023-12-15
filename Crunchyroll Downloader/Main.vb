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
Imports System.Security.Policy
Imports MyProvider.MyProvider
Imports System.Windows
Imports Microsoft.Web.WebView2.Core
Imports System.Net.Http
Imports Crunchyroll_Downloader.CRD_Classes


Public Class Main
    Inherits MetroForm

    Dim t As Thread
    Dim HTML As String = Nothing
    Public CR_Cookies As String = "Cookie: "

    Public CheckCRLogin As Boolean = True


    Public CR_AuthToken As String = ""
    Public CR_v1Token As String = ""


    'Public GetBetaSeasonsRetry As Boolean = False
    'Public GetBetaSeasonSingle As Boolean = False

    Public CR_MassSeasons As New List(Of CR_Seasons)
    Public CR_MassEpisodes As New List(Of CR_Seasons)

    'Public CrBetaMass As String = Nothing
    'Public CrBetaMassEpisodes As String = Nothing
    'Public CrBetaMassParameters As String = Nothing
    'Public CrBetaMassBaseURL As String = Nothing

    Public CrBetaBasic As String = Nothing
    Public locale As String = Nothing
    Public Url_locale As String = Nothing
    Dim ProcessCounting As Integer = 30
    'Public CrBetaObjects As String = Nothing
    'Public CrBetaStreams As String = Nothing
    'Public CrBetaStreamsUrl As String = Nothing
    Public LoadingUrl As String = ""
    Public LoadedUrls As New List(Of CoreWebView2WebResourceRequest)
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
    Public CookieList As New List(Of CoreWebView2Cookie)
    'Public liList As New List(Of String)
    Public HTMLString As String = My.Resources.Startuphtml
    Public ListBoxList As New List(Of String)
    'Public ItemList As New List(Of CRD_List_Item)
    Public RunningDownloads As Integer = 0
    Public UseQueue As Boolean = False
    Public StartServer As Integer = 0
    Public ResoAvalibe As String = Nothing
    Public ResoSearchRunning As Boolean = False
    Public UsedMap As String = Nothing
    Public Debug1 As Boolean = False
    Public Debug2 As Boolean = False
    Public LogBrowserData As Boolean = False
    Public Thumbnail As String = Nothing
    Public MergeSubs As Boolean = False
    'Public IgnoreS1 As Boolean = False
    Public IgnoreSeason As Integer = 0
    Public HideFLInt As Integer = 0
    Public KeepCache As Boolean = False
    'Public SubsOnly As Boolean = False
    Public DownloadScope As Integer = 0
    Public VideoFormat As String = ".mp4"
    Public MergeSubsFormat As String = "mov_text"
    'Public LoginDialog As Boolean = False
    'Public NonCR_Timeout As Integer = 5
    'Public NonCR_URL As String = Nothing
    Public DlSoftSubsRDY As Boolean = True
    Public DialogTaskString As String
    'Public ErrorBrowserString As String
    'Public ErrorBrowserUrl As String
    'Public ErrorBrowserBackString As String
    Public RunningQueue As Boolean = False
    Public UserCloseDialog As Boolean = False
    Dim Aktuell As String
    Dim Gesamt As String
    Public LabelUpdate As String = "Status: idle"
    Public LabelEpisode As String = "..."
    Public b As Boolean
    Public LoginOnly As String = "False"
    Public Pfad As String = My.Computer.FileSystem.CurrentDirectory
    Public TempFolder As String = Pfad
    Public ProfileFolder As String = Path.Combine(Application.StartupPath, "CRD-Profile") 'Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "CRD-Profile")
    Public ffmpeg_command As String = " -c copy -bsf:a aac_adtstoasc" '" -c:v hevc_nvenc -preset fast -b:v 6M -bsf:a aac_adtstoasc " 
    Public Reso As Integer
    Public Season_Prefix As String = "[default season prefix]"
    Public Season_PrefixDefault As String = "[default season prefix]"
    Public Episode_Prefix As String = "[default episode prefix]"
    Public Episode_PrefixDefault As String = "[default episode prefix]"

    Public ResoSave As String = "6666x6666"
    Public ResoFunBackup As String = "6666x6666"

    Public LangValueEnum As New List(Of NameValuePair)
    Public DubSprache As NameValuePair = New NameValuePair("Japanese", "jpn", "ja-JP", Nothing)
    Public SubSprache As NameValuePair = New NameValuePair("[ null ]", "", "null", Nothing)

    Public SoftSubs As New List(Of String)
    Public IncludeLangName As Boolean = False
    Public LangNameType As Integer = 0
    Public HybridThread As Integer = CInt(Environment.ProcessorCount / 2 - 1)
    Public TempSoftSubs As New List(Of String)
    Public AbourtList As New List(Of String)
    Public watingList As New List(Of String)
    Public GeckoLogFile As String = Nothing
    Dim SoftSubsString As String
    Dim CR_Unlock_Error As String
    Public Startseite As String = "https://www.crunchyroll.com/"
    Dim SubSprache2 As String
    'Dim URL_DL As String
    'Dim Pfad_DL As String
    Public Grapp_RDY As Boolean = True
    Public Funimation_Grapp_RDY As Boolean = True
    Public Grapp_non_cr_RDY As Boolean = True
    Public Grapp_Abord As Boolean = False
    Public NameBuilder As String = ""
    Public LeadingZero As Integer = 1
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
    Public BowserWasOpen As Boolean = False
    Public HybridMode As Boolean = False
    Public HardSubFunimation As String = "Disabled"
    Public Funimation_Bitrate As Integer = 0
    Public DubFunimation As String = "Disabled"
    Public Funimation_srt As Boolean = False
    Public Funimation_vtt As Boolean = False
    Public SubFunimationString As String = "en"
    Public SubFunimation As New List(Of String)
    Public DefaultSubFunimation As String = "Disabled"
    Public DefaultSubCR As String = "Disabled"
    Public DubMode As Boolean = True
    Public CR_Chapters As Boolean = False
    Public Curl_insecure As Boolean = False
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
    Dim ErrorNoPermisson As String = "Access is denied."
    'UI Variablen
    Public GB_Resolution_Text As String = "Resolution"
    Public GB_SubLanguage_Text As String = "Hardsub language"
    Public GB_Sub_Path_Text As String = "Sub directory"
    Public RBAnime_Text As String = "series name"
    Public RBStaffel_Text As String = "series name + season"
    Public NewStart_String As String = "to adopt all the settings, a restart is necessary."
    Public DL_Count_simultaneousText As String = "Simultaneous Downloads"
    Public GB_Sub_FormatText As String = "extended Sub Settings"
    Public LabelResoNotFoundText As String = "resolution not found" + vbNewLine + "Select another one below"
    Public LabelLangNotFoundText As String = "subtitle language not found" + vbNewLine + "Select another one below"
    Public ButtonResoNotFoundText As String = "Submit"
    'Public CB_SuB_Nothing As String = "[ null ]"
    Dim StatusToolTip As ToolTip = New ToolTip()
    Dim StatusToolTipText As String


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
        Panel1.BackColor = Color.FromArgb(50, 50, 50)
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
        Panel1.BackColor = SystemColors.Control
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

        Btn_add.Image = My.Resources.main_add
    End Sub

    Private Sub Btn_Browser_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Browser.MouseEnter, Btn_Browser.GotFocus

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

    Private Sub Btn_Queue_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Queue.MouseEnter, Btn_Queue.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Queue.Image = My.Resources.main_queue_invert_dark
        Else
            Btn_Queue.Image = My.Resources.main_queue_invert
        End If
    End Sub

    Private Sub Btn_Queue_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Queue.MouseLeave, Btn_Queue.LostFocus
        Btn_Queue.Image = My.Resources.main_queue
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

    Private Sub ConsoleBar_Click(sender As Object, e As EventArgs) Handles ConsoleBar.Click
        If TheTextBox.Visible = True Then
            'TheTextBox.Lines = DebugList.ToArray
            TheTextBox.Visible = False
            ListViewHeightOffset = 7
            ConsoleBar.Location = New Point(0, Me.Height - ListViewHeightOffset)
            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        Else
            ListViewHeightOffset = 103
            TheTextBox.Visible = True
            ConsoleBar.Location = New Point(0, Me.Height - ListViewHeightOffset)
            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        End If
        Me.Height = Me.Height + 1
    End Sub

    Private Sub ConsoleBar_MouseEnter(sender As Object, e As EventArgs) Handles ConsoleBar.MouseEnter
        ConsoleBar.BackgroundImage = My.Resources.balken_console
    End Sub

    Private Sub ConsoleBar_MouseLeave(sender As Object, e As EventArgs) Handles ConsoleBar.MouseLeave
        ConsoleBar.BackgroundImage = My.Resources.balken
    End Sub

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Width = Me.Width - 2
        Panel1.Height = Me.Height - 71 - ListViewHeightOffset
        PictureBox5.Width = Me.Width - 40
        ConsoleBar.Location = New Point(1, Me.Height - ListViewHeightOffset)
        ConsoleBar.Width = Me.Width - 40
        TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
        TheTextBox.Width = Me.Width - 2
        Btn_Close.Location = New Point(Me.Width - 36, 1)
        Btn_min.Location = New Point(Me.Width - 67, 1)
        Btn_Settings.Location = New Point(Me.Width - 165, 17)
        Btn_Queue.Location = New Point(Me.Width - 265, 17)
        Try
            Panel1.AutoScrollPosition = New Point(0, 0)

            Dim W As Integer = Panel1.Width
            If Panel1.Controls.Count * 142 > Panel1.Height Then
                W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
            End If

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For s As Integer = 0 To Item.Count - 1
                Item(s).SetBounds(0, 142 * s, W - 2, 142)
                If Debug2 = True Then
                    Debug.WriteLine("Bounds: " + Item(s).GetTextBound.ToString)

                    Debug.WriteLine("Ist: " + Item(s).Location.Y.ToString)
                    Debug.WriteLine("Soll: " + (142 * s).ToString)
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
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





    Function AddLeadingZeros(ByVal txt As String) As String

        txt = txt.Replace(",", ".")
        Dim Post As String = Nothing
        If CBool(InStr(txt, ".")) = True Then
            Dim txt_split As String() = txt.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
            txt = txt_split(0)
            Post = "." + txt_split(1)
        End If

        For i As Integer = 0 To LeadingZero + 1
            If txt.Count = LeadingZero + 1 Or txt.Count > LeadingZero + 1 Then
                Exit For
            Else
                txt = "0" + txt
            End If
        Next

        Dim Output As String = txt + Post

        Return Output
    End Function


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FillArray()

#Region "settings path"

        Dim mySettings As New DirectorySettings
        mySettings.DirectoryName = Application.StartupPath
        mySettings.FileName = "User.config.dat"
        mySettings.Save() ' muss explizit gepeichert werden...

#End Region

        Me.ContextMenuStrip = ContextMenuStrip1
        Dim tbtl As TextBoxTraceListener = New TextBoxTraceListener(TheTextBox)
        Trace.Listeners.Add(tbtl)
        b = True
        Thread.CurrentThread.Name = "Main"
        Debug.WriteLine("v" + Application.ProductVersion)
        Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)


        DarkModeValue = My.Settings.DarkModeValue

        DownloadScope = My.Settings.DownloadScope

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

        StartServer = My.Settings.ServerPort


        If StartServer > 0 Then
            Timer3.Enabled = True
            ServerThread = New Thread(AddressOf ServerStart)
            ServerThread.Priority = ThreadPriority.Normal
            ServerThread.IsBackground = True
            ServerThread.Start()
        End If
        waveOutSetVolume(0, 0)

        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        StatusToolTip.Active = True

        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception
        End Try


        If My.Settings.Pfad = Nothing Then
            Pfad = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Else
            Pfad = My.Settings.Pfad
        End If

        If My.Settings.TempFolder = Nothing Then
            TempFolder = Pfad
        Else
            TempFolder = My.Settings.TempFolder
        End If

        Episode_Prefix = My.Settings.Prefix_E

        Season_Prefix = My.Settings.Prefix_S

        DefaultSubFunimation = My.Settings.DefaultSubFunimation

        DefaultSubCR = My.Settings.DefaultSubCR

        Startseite = My.Settings.Startseite


        UseQueue = My.Settings.QueueMode

        KodiNaming = My.Settings.KodiSupport

        DubMode = My.Settings.DubMode

        CR_Chapters = My.Settings.CR_Chapters

        Curl_insecure = My.Settings.Curl_insecure

        KeepCache = My.Settings.Keep_Cache

        ffmpeg_command = My.Settings.ffmpeg_command

        If My.Settings.ffmpeg_command_override = "null" Then
        Else
            ffmpeg_command = My.Settings.ffmpeg_command_override
        End If


        Reso = My.Settings.Reso

        LeadingZero = My.Settings.LeadingZero


        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = My.Settings.Subtitle Then
                'MsgBox(My.Settings.Subtitle)
                SubSprache = LangValueEnum(i)
                Exit For
            End If
        Next

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = My.Settings.CR_Dub Then
                DubSprache = LangValueEnum(i)
                Exit For
            End If
        Next


        Funimation_Bitrate = My.Settings.Funimation_Bitrate

        SubFolder_Value = My.Settings.SubFolder_Value

        MaxDL = My.Settings.SL_DL

        ' 
        If My.Settings.NameTemplate = "Unused" Then 'convert old stlye
            If My.Settings.CR_NameMethode = 0 Then
                NameBuilder = "AnimeTitle;Season;EpisodeNR;"
            ElseIf My.Settings.CR_NameMethode = 1 Then
                NameBuilder = "AnimeTitle;Season;EpisodeName;"
            ElseIf My.Settings.CR_NameMethode = 2 Then
                NameBuilder = "AnimeTitle;Season;EpisodeNR;EpisodeName;"
            ElseIf My.Settings.CR_NameMethode = 3 Then
                NameBuilder = "AnimeTitle;Season;EpisodeName;EpisodeNR;"
            End If
        Else
            NameBuilder = My.Settings.NameTemplate
        End If

        ErrorTolerance = My.Settings.ErrorTolerance


        IncludeLangName = My.Settings.IncludeLangName


        LangNameType = My.Settings.LangNameType


        HybridThread = My.Settings.HybridThread

        IgnoreSeason = My.Settings.IgnoreSeason
        HideFLInt = My.Settings.HideSF
        HybridMode = My.Settings.HybridMode
        Funimation_srt = My.Settings.Funimation_srt
        Funimation_vtt = My.Settings.Funimation_vtt

        DubFunimation = My.Settings.FunimationDub


        HardSubFunimation = "Disabled"


        SoftSubsString = My.Settings.AddedSubs


        If SoftSubsString = "None" Then
        Else
            Dim SoftSubsStringSplit() As String = SoftSubsString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                SoftSubs.Add(SoftSubsStringSplit(i))
            Next
        End If



        SubFunimationString = My.Settings.Fun_Sub

        If SubFunimationString = "None" Then
        Else
            Dim SoftSubsStringSplit() As String = SubFunimationString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                SubFunimation.Add(SoftSubsStringSplit(i))
            Next
        End If



        MergeSubsFormat = My.Settings.MergeSubs


        If MergeSubsFormat = "[merge disabled]" Then
            MergeSubs = False
        Else
            MergeSubs = True
        End If


        VideoFormat = My.Settings.VideoFormat

        If VideoFormat = ".aac" Then
            VideoFormat = ".mp4"
            'My.Settings.VideoFormat = ".mp4"
            MsgBox("The 'Audio only' output option has been moved." + vbNewLine + "See https://github.com/hama3254/Crunchyroll-Downloader-v3.0/issues/698 for more information.")
        End If

        RetryWithCachedFiles()

        'MsgBox(Curl_insecure.ToString)

    End Sub



    Public Sub ListItemAdd(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, Optional Service As String = "CR") ', ByVal AudioLang As String)

        'With ListView1.Items.Add("0")
        'For i As Integer = 0 To 10
        ItemConstructor(NameKomplett, NameP1, NameP2, Reso, HardSub, ThumbnialURL, URL_DL, Pfad_DL, Service)

        'Next
        'End With
    End Sub

    Public Sub ItemConstructor(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal DisplayReso As String, ByVal HardSub As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, ByVal Service As String)
        Dim Item As New CRD_List_Item
        Item.Visible = False


#Region "Set Variables"
        Item.SetService(Service)
        Item.SetTolerance(ErrorTolerance)
        Item.SetTargetReso(Reso)
        Item.SetLabelWebsite(NameP1)
        Item.SetLabelAnimeTitel(NameP2)
        Item.SetLabelResolution(DisplayReso)
        Item.SetLabelHardsub(HardSub)
        Item.SetThumbnailImage(ThumbnialURL)
        Item.SetLabelPercent("0%")
        Item.SetCache(KeepCache)
        Item.SetMergeSubstoMP4(MergeSubs)
        Item.SetDebug2(Debug2)
#End Region




        Dim W As Integer = Panel1.Width
        If Panel1.Controls.Count * 142 > Panel1.Height Then
            W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
        End If



        Item.SetBounds(0, 142 * Panel1.Controls.Count, W - 2, 142)


        Item.Parent = Panel1
        Panel1.Controls.Add(Item)

        Item.Visible = True
        Dim TempHybridMode As Boolean = HybridMode
        If CBool(InStr(URL_DL, ".mpd")) Then
            TempHybridMode = False
        End If

        If Pfad_DL.Length > 255 Then
            'MsgBox(Pfad_DL.Length.ToString)
            Pfad_DL = Chr(34) + "\\?\" + Pfad_DL.Replace(Chr(34), "") + Chr(34)
        End If


        'MsgBox(URL_DL + vbNewLine + Pfad_DL + vbNewLine + NameKomplett + vbNewLine + TempHybridMode.ToString)
        Item.StartDownload(URL_DL, Pfad_DL, NameKomplett, TempHybridMode, TempFolder)
    End Sub



#Region "Season DL"





#End Region
#Region "Convert Subs"

    Public Function ConvertSubValue(ByVal HardSub As String, ByVal Output As Integer) As String
        ' 0 = DisplayText ; 1 = MP4CC/ISO-639-2 ; 2 = Both 

        HardSub = HardSub.Replace(Chr(34), "") 'clean up any mess just in case... 

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = HardSub Or LangValueEnum(i).FM_Value = HardSub Then

                If Output = 1 Then ' MP4CC/ISO-639-2 replacing the old ConvertSubValue version |   'Return ConvertSubValue(HardSub)
                    Return LangValueEnum(i).MP4CC
                ElseIf Output = 2 Then '; 2 = Both replacing the old GetSubFileLangName funktion 
                    Dim RS As String = LangValueEnum(i).DisplayText + "." + LangValueEnum(i).MP4CC
                    Return RS
                Else
                    Return LangValueEnum(i).DisplayText
                End If

            End If
        Next

        Return HardSub + " not found"

    End Function

#End Region




#Region "CR-Beta"
    Public Async Sub DownloadBetaSeasons()
        Try
            Dim ListOfEpisodes As New List(Of String)

            For i As Integer = 0 To CR_MassEpisodes.Count - 1

                If Url_locale = "" Then
                    ListOfEpisodes.Add("https://www.crunchyroll.com/watch/" + CR_MassEpisodes.Item(i).guid + "/" + CR_MassEpisodes.Item(i).audio_locale + "/")
                Else
                    ListOfEpisodes.Add("https://www.crunchyroll.com/" + Url_locale + "/" + "watch/" + CR_MassEpisodes.Item(i).guid + "/" + CR_MassEpisodes.Item(i).audio_locale + "/")
                End If

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
                            Dim ItemFinshedCount As Integer = 0 '
                            Dim Item As New List(Of CRD_List_Item)
                            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
                            Item.Reverse()

                            For i2 As Integer = 0 To Item.Count - 1
                                If Item(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            RunningDownloads = Panel1.Controls.Count - ItemFinshedCount
                        Catch ex As Exception
                            RunningDownloads = Panel1.Controls.Count
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
                    Grapp_RDY = True
                    'MsgBox("dl_abourd")
                End If
                If UseQueue = True Then
                    'Anime_Add.ListBox1.Items.Add(ListOfEpisodes(i))
                    ListBoxList.Add(ListOfEpisodes(i))
                    Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    Anime_Add.Add_Display.ForeColor = Color.Black
                Else
                    Grapp_RDY = False
                    b = False
                    Debug.WriteLine("b: " + b.ToString)
                    LoadBrowser(ListOfEpisodes(i))
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
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.btn_dl.Text = "Download" 'btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
    End Sub

    Public Sub GetBetaSeasons(ByVal AnimeUrl As String, ByVal JsonUrl As String, ByVal Auth As String, Optional ByVal BrowserData As String = Nothing) ', ByVal SeasonJson As String)
        Anime_Add.groupBox2.Visible = True
        Anime_Add.bt_Cancel_mass.Enabled = True
        Anime_Add.bt_Cancel_mass.Visible = True
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
        CR_MassSeasons.Clear()
        If BrowserData = Nothing Then

            Dim Loc_CR_Cookies = " -H " + Chr(34) + CR_Cookies.Replace(Chr(34), "").Replace(" -H ", "") + Chr(34)

            Try

                SeasonJson = CurlAuthNew(JsonUrl, Loc_CR_Cookies, Auth)

            Catch ex As Exception
                If CBool(InStr(ex.ToString, "Error - Getting")) Then
                    MsgBox("Error invalid CR respone")
                    Exit Sub
                Else
                    MsgBox("Error processing data")
                    Exit Sub
                End If
            End Try



        Else
            SeasonJson = BrowserData
            Debug.WriteLine("BrowserData: " + BrowserData)
        End If


        SeasonJson = CleanJSON(SeasonJson)

        Dim SeasonJObject As JObject = JObject.Parse(SeasonJson)
        Dim SeasonData As List(Of JToken) = SeasonJObject.Children().ToList

        For Each item As JProperty In SeasonData
            item.CreateReader()
            Select Case item.Name
                Case "data" 'each record is inside the entries array
                    For Each Entry As JObject In item.Values
                        Dim SeasonSubData As List(Of JToken) = Entry.Children().ToList
                        Dim localSeasons As New List(Of CR_Seasons)
                        Dim season_number As String = Nothing
                        Dim id As String = Nothing
                        Dim audio_localeMain As String = Nothing


                        For Each SeasonSubItem As JProperty In SeasonSubData
                            SeasonSubItem.CreateReader()

                            Select Case SeasonSubItem.Name
                                Case "versions"
                                    Try
                                        For Each VersionItem As JObject In SeasonSubItem.Values

                                            Dim guid As String = VersionItem.GetValue("guid").ToString
                                            Dim audio_locale As String = VersionItem.GetValue("audio_locale").ToString

                                            localSeasons.Add(New CR_Seasons(guid, audio_locale, Auth))
                                        Next
                                    Catch ex As Exception
                                        Debug.WriteLine("Error getting season data")
                                    End Try
                                Case "season_number"
                                    season_number = SeasonSubItem.Value.ToString
                                Case "id"
                                    id = SeasonSubItem.Value.ToString
                                Case "audio_locale"
                                    audio_localeMain = SeasonSubItem.Value.ToString
                            End Select
                        Next

                        If localSeasons.Count = 0 Then
                            Anime_Add.ComboBox1.Items.Add(ConvertSubValue(audio_localeMain, ConvertSubsEnum.DisplayText) + " - Season " + season_number)
                            CR_MassSeasons.Add(New CR_Seasons(id, audio_localeMain, Auth))
                        End If

                        If localSeasons.Count > 0 Then
                            For i As Integer = 0 To CR_MassSeasons.Count - 1
                                If CR_MassSeasons.Item(i).guid = localSeasons.Item(0).guid Then
                                    localSeasons.Clear()
                                    Exit For
                                End If
                            Next
                        End If


                        If localSeasons.Count > 0 Then
                            For i As Integer = 0 To localSeasons.Count - 1
                                Anime_Add.ComboBox1.Items.Add(ConvertSubValue(localSeasons.Item(i).audio_locale, ConvertSubsEnum.DisplayText) + " - Season " + season_number)
                                CR_MassSeasons.Add(localSeasons.Item(i))
                            Next
                        End If

                    Next
            End Select
        Next

    End Sub

    Public Sub GetCRVideoProxy(ByVal requesturl As String, ByVal AuthToken As String, ByVal WebsiteURL As String, ByVal RT_count As Integer)
        Dim Evaluator = New Thread(Sub() Me.GetCRVideo(requesturl, AuthToken, WebsiteURL, RT_count))
        Evaluator.Start()
    End Sub

    Public Sub GetCRVideo(ByVal Streams As String, ByVal AuthToken As String, ByVal WebsiteURL As String, ByVal RT_count As Integer)
        If b = False Then
            b = True
        End If
        'Debug.WriteLine(Streams)
        'Debug.WriteLine(vbCrLf)
        Debug.WriteLine("Website: " + WebsiteURL)



        Try
            Grapp_RDY = False
            Dim ffmpeg_command_temp As String = ffmpeg_command
            Dim CR_MetadataUsage As Boolean = False
            Dim CR_Streams As New List(Of CR_Beta_Stream)
            Dim CR_series_title As String = Nothing
            Dim CR_season_number As String = Nothing
            Dim CR_FolderSeason As String = Nothing
            Dim CR_episode As String = Nothing
            Dim CR_episode_duration_ms As String = "60000000"
            Dim CR_episode2 As String = Nothing
            Dim CR_Anime_Staffel_int As String = Nothing
            Dim CR_episode_int As String = Nothing
            Dim CR_title As String = Nothing
            Dim CR_audio_locale As String = "ja-JP"
            Dim CR_audio_isDubbed As Boolean = False
            Dim CR_Region As String = "DE"
            Dim ResoUsed As String = "x" + Reso.ToString
            Dim ffmpegInput As String = "-i [Subtitles only]"


            Dim Pfad2 As String
            Dim TextBox2_Text As String = Nothing
            Dim CR_FilenName As String = Nothing
            Dim ObjectJson As String = Nothing
            Me.Invoke(New Action(Function() As Object
                                     TextBox2_Text = Anime_Add.TextBox2.Text
                                     Return Nothing
                                 End Function))

            '
            Dim Loc_CR_Cookies = " -H " + Chr(34) + CR_Cookies + Chr(34)

            Dim Loc_AuthToken = " -H " + Chr(34) + "Authorization: " + AuthToken + Chr(34)

            If CBool(InStr(AuthToken, "Authorization")) = True Then
                Loc_AuthToken = AuthToken
            End If

            Dim CR_EpisodeID As String = ""

#Region "No anime :("


            If CBool(InStr(WebsiteURL, "musicvideo")) = True Then
                'TextBox2_Text to bypasss name for now

                Dim ObjectsURL As String = Streams.Replace("music/", "music/music_videos/").Replace("/streams", "")

                ObjectJson = CurlAuthNew(ObjectsURL, Loc_CR_Cookies, Loc_AuthToken)

                Dim Title() As String = ObjectJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Title2() As String = Title(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim Arti() As String = ObjectJson.Split(New String() {Chr(34) + "name" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Arti2() As String = Arti(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                TextBox2_Text = Arti2(0) + " - " + Title2(0)

            ElseIf CBool(InStr(WebsiteURL, "/concert/")) = True Then

                Dim ObjectsURL As String = Streams.Replace("music/", "music/concerts/").Replace("/streams", "")

                ObjectJson = CurlAuthNew(ObjectsURL, Loc_CR_Cookies, Loc_AuthToken)


                Dim Title() As String = ObjectJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Title2() As String = Title(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim Arti() As String = ObjectJson.Split(New String() {Chr(34) + "name" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Arti2() As String = Arti(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)


                'MsgBox(Arti2(0))
                'MsgBox(Title2(0))


                TextBox2_Text = Arti2(0) + " - " + Title2(0)
#End Region

            Else
                Dim ObjectsURLBuilder() As String = Streams.Split(New String() {"videos"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ObjectsURLBuilder2() As String = ObjectsURLBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ObjectsURLBuilder3() As String = WebsiteURL.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ObjectsURL As String = ObjectsURLBuilder(0) + "objects/" + ObjectsURLBuilder4(0) + ObjectsURLBuilder2(1)

                CR_EpisodeID = ObjectsURLBuilder4(0)

                Debug.WriteLine(ObjectsURL)

                ObjectJson = CurlAuthNew(ObjectsURL, Loc_CR_Cookies, Loc_AuthToken)

                'Filter JSON esqaped characters
                'Debug.WriteLine(Date.Now.ToString + "before:" + ObjectJson)
                Debug.WriteLine("1288: " + ObjectJson)
                ObjectJson = CleanJSON(ObjectJson)
                'Debug.WriteLine(Date.Now.ToString + "after:" + ObjectJson)

                Dim ser As JObject = JObject.Parse(ObjectJson)
                Dim data As List(Of JToken) = ser.Children().ToList

                For Each item As JProperty In data
                    item.CreateReader()
                    Select Case item.Name

                        Case "data" 'each record is inside the entries array
                            For Each Entry As JObject In item.Values
                                Try
                                    Dim Title As String = Entry("title").ToString
                                    CR_title = String.Join(" ", Title.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                    Debug.WriteLine(Date.Now.ToString + " CR-Title: " + CR_title)
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
                                                        CR_series_title = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    'Case "season_title"
                                                    '    CR_season_title = SubEntry.Value.ToString
                                                    Case "season_number"
                                                        CR_season_number = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    Case "episode_number"
                                                        CR_episode2 = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    Case "episode"
                                                        CR_episode = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    Case "duration_ms"
                                                        CR_episode_duration_ms = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    Case "is_dubbed"
                                                        CR_audio_isDubbed = CBool(SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", ""))
                                                        'Case "eligible_region"
                                                        '    CR_Region = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                End Select
                                            Next '
                                    End Select
                                Next
                            Next
                    End Select
                Next

            End If

#Region "VideoJson"

            Dim cms As String = CurlAuthNew("https://www.crunchyroll.com/index/v2", Loc_CR_Cookies, Loc_AuthToken)

            Dim cms_web() As String = cms.Split(New String() {Chr(34) + "cms_web" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim bucket1() As String = cms_web(1).Split(New String() {Chr(34) + "bucket" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim bucket2() As String = bucket1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim bucket As String = bucket2(0)

            Dim policy1() As String = cms_web(1).Split(New String() {Chr(34) + "policy" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim policy2() As String = policy1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim policy As String = policy2(0)

            Dim signature1() As String = cms_web(1).Split(New String() {Chr(34) + "signature" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim signature2() As String = signature1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim signature As String = signature2(0)

            Dim key_pair_id1() As String = cms_web(1).Split(New String() {Chr(34) + "key_pair_id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim key_pair_id2() As String = key_pair_id1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim key_pair_id As String = key_pair_id2(0)

            'key_pair_id
            'cms_web
            Dim Videos1() As String = Streams.Split(New String() {"v2/cms"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Videos2() As String = Videos1(1).Split(New String() {"streams"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Videos As String = Videos2(0)

            Dim VideoJson As String = Nothing
            Dim StreamUrl As String = "https://www.crunchyroll.com/cms/v2" + bucket + Videos + "streams?Policy=" + policy + "&Signature=" + signature + "&Key-Pair-Id=" + key_pair_id

            VideoJson = CurlAuthNew(StreamUrl, Loc_CR_Cookies, Loc_AuthToken) 'Curl(StreamUrl) '

            'VideoJson = CurlAuthNew(Streams, Loc_CR_Cookies, Loc_AuthToken)
            Debug.WriteLine("VideoJson: " + VideoJson)
            Debug.WriteLine("VideoStreams: " + Streams)


            Dim CR_HardSubLang As String = SubSprache.CR_Value
            VideoJson = CleanJSON(VideoJson)

            'MsgBox(VideoJson)
#End Region

#Region "Check for dub override"

            'If My.Settings.OverrideDub = True And CBool(InStr(Streams, "/videos/")) Then 'einstellung ein + kein musikvideo oder Konzert

            '    Dim Meta() As String = VideoJson.Split(New String() {Chr(34) + "meta" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)
            '    Dim Meta2() As String = Meta(1).Split(New String() {Chr(34) + "audio_locale" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            '    'MsgBox(DubSprache.CR_Value)

            '    For i As Integer = 0 To Meta2.Count - 1

            '        If CBool(InStr(Meta2(i), Chr(34) + "media_guid" + Chr(34) + ":" + Chr(34))) And CBool(InStr(Meta2(i), DubSprache.CR_Value)) Then

            '            Dim media_guid() As String = Meta2(i).Split(New String() {Chr(34) + "media_guid" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            '            Dim media_guid2() As String = media_guid(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            '            If CBool(InStr(Streams, media_guid2(0))) Then
            '                'MsgBox("done")
            '                'done 
            '                Exit For
            '            Else
            '                ' overriding '	https://www.crunchyroll.com/content/v2/cms/videos/GPPFKG08N/streams?locale=de-DE

            '                Streams = "https://www.crunchyroll.com/content/v2/cms/videos/" + media_guid2(0) + "/streams?locale=" + locale

            '                'MsgBox(Streams)




            '                VideoJson = CurlAuthNew(Streams, Loc_CR_Cookies, Loc_AuthToken)


            '            End If

            '        End If
            '    Next


            'End If

#End Region

#Region "m3u8 suche"




            Dim VideoJObject As JObject = JObject.Parse(VideoJson)
            Dim VideoData As List(Of JToken) = VideoJObject.Children().ToList

            Dim download_hls As CR_Beta_Stream = Nothing

            For Each item As JProperty In VideoData
                item.CreateReader()
                Select Case item.Name
                    Case "streams" 'each record is inside the entries array
                        For Each Entry As JProperty In item.Values

                            Dim JsonEntryFormat As String = Entry.Name
                            If CBool(InStr(Entry.Name, "drm")) Or CBool(InStr(Entry.Name, "dash")) Or CBool(InStr(Entry.Name, "download")) Then
                                Continue For
                            End If




                            Dim SubData As List(Of JToken) = Entry.Children().ToList
                            For Each SubItem As JObject In SubData
                                SubItem.CreateReader()

                                Dim StreamFormats As List(Of JToken) = SubItem.Children().ToList


                                For Each Formats As JProperty In StreamFormats
                                    Formats.CreateReader()
                                    Dim SubLang As String = Formats.Name
                                    If SubLang = Nothing Or SubLang = "" Then
                                        SubLang = "null"
                                    End If

                                    Dim Url As String = CStr(Formats.Value("url"))


                                    If CBool(InStr(JsonEntryFormat, "download")) Then
                                        download_hls = New CR_Beta_Stream(SubLang, JsonEntryFormat, Url)
                                        Continue For
                                    End If

                                    CR_Streams.Add(New CR_Beta_Stream(SubLang, JsonEntryFormat, Url))


                                Next
                            Next
                        Next
                        'Case "meta" 'each record is inside the entries array
                        '    For Each MetaEntrys As JProperty In item.Values
                        '        Select Case MetaEntrys.Name
                        '            Case "audio_locale"
                        '                If CR_audio_isDubbed = True Then
                        '                    Dim AudioTag As String = MetaEntrys.Value.ToString
                        '                    CR_audio_locale = String.Join(" ", AudioTag.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                        '                Else
                        '                    CR_audio_locale = "ja-JP"
                        '                End If

                        '        End Select

                        '    Next
                    Case "audio_locale" 'each record is inside the entries array
                        'MsgBox(item.Value)
                        If CR_audio_isDubbed = True Then
                            Dim AudioTag As String = CStr(item.Value)
                            CR_audio_locale = String.Join(" ", AudioTag.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                        Else
                            CR_audio_locale = "ja-JP"
                        End If
                End Select
            Next


            'MsgBox(CR_Streams.Count.ToString)

            MsgBox(CR_HardSubLang)

            Dim CR_URI_Master As New List(Of String)


            Dim RawStream As New List(Of String)



            For c As Integer = 0 To CR_Streams.Count - 1
                Dim i As Integer = c
                'Debug.WriteLine("1457: " + i.ToString + "/" + CR_Streams.Count.ToString + " " + CR_Streams.Item(i).subLang + " " + CR_Streams.Item(i).Format)
                'https://www.crunchyroll.com/watch/GN7UD2K8N/dragon-ball-super-super-hero
                Debug.WriteLine("Streams-1254: " + CR_Streams.Item(i).subLang)
                If CR_Streams.Item(i).subLang = CR_HardSubLang Then
                    CR_URI_Master.Add(CR_Streams.Item(i).Url)
                    'MsgBox(CR_Streams.Item(i).Format + CR_Streams.Item(i).Url)
                ElseIf CR_Streams.Item(i).subLang = "" And CR_audio_locale IsNot "ja-JP" And DubMode = True Then 'nothing/raw
                    RawStream.Add(CR_Streams.Item(i).Url)
                End If
            Next
            'MsgBox(CR_URI_Master.Count.ToString)
            If CR_URI_Master.Count = 0 And RawStream.Count > 0 Then
                CR_URI_Master.Clear()
                CR_URI_Master.AddRange(RawStream)

            ElseIf CR_URI_Master.Count = 0 Then
                Me.Invoke(New Action(Function() As Object
                                         ResoNotFoundString = VideoJson
                                         DialogTaskString = "Language_CR_Beta"
                                         ErrorDialog.ShowDialog()
                                         Return Nothing
                                     End Function))
                If UserCloseDialog = True Then
                    Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                Else
                    'MsgBox(CR_HardSubLang)
                    CR_HardSubLang = ResoBackString
                    ResoBackString = Nothing
                    'MsgBox(CR_Streams.Count.ToString)
                    For i As Integer = 0 To CR_Streams.Count - 1
                        Debug.WriteLine("1280: " + CR_Streams.Item(i).subLang)
                        If CR_Streams.Item(i).subLang = CR_HardSubLang Then
                            CR_URI_Master.Add(CR_Streams.Item(i).Url)

                        End If

                    Next

                End If
            End If

            'MsgBox(CR_URI_Master.Count.ToString)

            If CBool(InStr(CR_URI_Master(0), "master.m3u8")) Then
                Me.Invoke(New Action(Function() As Object
                                         Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                         Me.Text = "Status: m3u8 found, looking for resolution"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
            Else
                Throw New System.Exception("Premium Episode")
            End If

#End Region

#Region "Name"

#Region "Name von Crunchyroll"

            Dim MergeSearch As String = "if not changed no Merch possible, i hope..."

            If CR_episode = Nothing Or CR_episode = "" And CR_episode2 = Nothing Then
                CR_episode_int = "0"
            ElseIf CR_episode IsNot Nothing And CR_episode IsNot "" Then
                CR_episode_int = CR_episode
            ElseIf CR_episode2 IsNot Nothing Then
                CR_episode_int = CR_episode2
            End If
            CR_Anime_Staffel_int = CR_season_number



            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Or CBool(InStr(TextBox2_Text, "++")) = True Then

                If IgnoreSeason = 1 And CR_season_number = "1" Or IgnoreSeason = 1 And CR_season_number = "0" Then
                    CR_season_number = Nothing
                ElseIf IgnoreSeason = 2 Then
                    CR_season_number = Nothing
                End If

                If Season_Prefix = "[default season prefix]" Then
                    If CR_episode = Nothing And CR_episode2 = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing
                    ElseIf CR_season_number = Nothing Then
                    Else
                        CR_season_number = "Season " + CR_season_number
                    End If
                Else
                    If CR_episode = Nothing And CR_episode2 = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing
                    ElseIf CR_season_number = Nothing Then
                    Else
                        CR_season_number = Season_Prefix + CR_season_number
                    End If
                End If

                CR_FolderSeason = CR_season_number



                If Episode_Prefix = "[default episode prefix]" Then
                    If CR_episode = Nothing Or CR_episode = "" And CR_episode2 = Nothing Then
                        CR_episode = CR_title
                    ElseIf CR_episode IsNot Nothing And CR_episode IsNot "" Then
                        CR_episode = "Episode " + AddLeadingZeros(CR_episode)
                    ElseIf CR_episode2 IsNot Nothing Then
                        CR_episode = "Episode " + AddLeadingZeros(CR_episode2)
                    End If
                    'CR_episode = "Episode " + AddLeadingZeros(CR_episode)
                Else
                    CR_episode = Episode_Prefix + AddLeadingZeros(CR_episode)
                End If


                Dim NameParts As String() = NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To NameParts.Count - 1

                    If NameParts(i) = "AnimeTitle" Then
                        CR_FilenName = CR_FilenName + " " + CR_series_title
                        MergeSearch = MergeSearch + " " + CR_series_title
                    ElseIf NameParts(i) = "Season" Then
                        CR_FilenName = CR_FilenName + " " + CR_season_number
                        MergeSearch = MergeSearch + " " + CR_season_number
                    ElseIf NameParts(i) = "EpisodeNR" Then
                        CR_FilenName = CR_FilenName + " " + CR_episode
                        MergeSearch = MergeSearch + " " + CR_episode
                    ElseIf NameParts(i) = "EpisodeName" Then
                        CR_FilenName = CR_FilenName + " " + CR_title
                    ElseIf NameParts(i) = "AnimeDub" Then
                        CR_FilenName = CR_FilenName + " " + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.DisplayText)
                    ElseIf NameParts(i) = "AnimeSub" Then
                        ' CR_FilenName = CR_FilenName + " RepSub" 'to be done
                    End If

                Next

                If CBool(InStr(TextBox2_Text, "++")) = True Then
                    Dim Backup_CR_FilenName As String = CR_FilenName
                    Try
                        Dim AddDef As String = "CRD"
                        Dim TestString As String = AddDef + TextBox2_Text + AddDef
                        Dim PrePost As String() = TestString.Split(New String() {"++"}, System.StringSplitOptions.RemoveEmptyEntries)
                        CR_FilenName = PrePost(0) + CR_FilenName + PrePost(1)
                        CR_FilenName = CR_FilenName.Replace(AddDef, "")
                    Catch ex As Exception
                        CR_FilenName = Backup_CR_FilenName
                    End Try
                End If






#End Region
            Else
                CR_FilenName = RemoveExtraSpaces(String.Join(" ", TextBox2_Text.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)).Replace(Chr(34), "").Replace("\", "").Replace("/", "") 'System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))
            End If

            If KodiNaming = True Then
                Dim KodiString As String = "[S"
                If CR_Anime_Staffel_int = "0" Then
                    CR_Anime_Staffel_int = "01"
                Else
                    CR_Anime_Staffel_int = "0" + CR_Anime_Staffel_int
                End If

                KodiString = KodiString + CR_Anime_Staffel_int + " E" + AddLeadingZeros(CR_episode_int) ' CR_episode_nr
                KodiString = KodiString + "] "
                CR_FilenName = KodiString + CR_FilenName
            End If
            Debug.WriteLine(CR_FilenName)

            CR_FilenName = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "") 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = RemoveExtraSpaces(CR_FilenName)
            'My.Computer.FileSystem.WriteAllText("log.log", WebbrowserText, False)
            Pfad2 = UseSubfolder(CR_series_title, CR_FolderSeason, Pfad)
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



#Region "Chapters"
            Dim Mdata_File As String = Application.StartupPath + "\" + CR_EpisodeID + "-mdata.txt"
            Debug.WriteLine("Checking chapters")
            If CR_EpisodeID = "" Then
                'ignore for music video
            Else


                If CR_Chapters = True Then
                    Dim ChaptersJson As String = Nothing
                    Dim ChaptersUrl As String = "https://static.crunchyroll.com/datalab-intro-v2/" + CR_EpisodeID + ".json"
                    Try
                        ChaptersJson = Curl(ChaptersUrl)

                        If CBool(InStr(ChaptersJson, "curl:")) = True Then
                            ChaptersJson = Curl(ChaptersUrl)
                        End If

                        If CBool(InStr(ChaptersJson, "curl:")) = True Then
                            ChaptersJson = Nothing
                            Debug.WriteLine("no Chapter data... ignoring")
                        End If
                    Catch ex As Exception

                    End Try


                    If ChaptersJson IsNot Nothing Then
                        'MsgBox(ChaptersJson)
                        Dim StartTime As String() = ChaptersJson.Split(New String() {Chr(34) + "startTime" + Chr(34) + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim StartTime2 As String() = StartTime(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim StartTime3 As String() = StartTime2(0).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim StartTime4 As String = StartTime3(1)

                        For i As Integer = StartTime4.Length To 2
                            StartTime4 = StartTime4 + "0"
                        Next

                        Dim StartTime_ms As String = StartTime3(0) + StartTime4
                        '
                        Dim StartTime_int As Integer = CInt(StartTime_ms)

                        Dim EndTime As String() = ChaptersJson.Split(New String() {Chr(34) + "endTime" + Chr(34) + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim EndTime2 As String() = EndTime(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim EndTime3 As String() = EndTime2(0).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

                        Dim EndTime4 As String = EndTime3(1)
                        Dim AfterTime As String = Nothing

                        For i As Integer = EndTime4.Length To 2
                            If EndTime4.Length = 2 Then
                                AfterTime = EndTime4 + "1"
                            End If
                            EndTime4 = EndTime4 + "0"
                        Next

                        Dim EndTime_ms As String = EndTime3(0) + EndTime4
                        Dim AfterTime_ms As String = EndTime3(0) + AfterTime
                        Dim Metadata As String = Nothing


                        If CInt(CR_episode_duration_ms) < CInt(StartTime_ms) Then
                            'Totaly invalid...
                        ElseIf CInt(CR_episode_duration_ms) < CInt(EndTime_ms) And StartTime_int = 0 Then
                            'the answer is 42...
                            Debug.WriteLine("Skip Chapters, the answer is 42...")
                            'this is pointless
                        ElseIf CInt(CR_episode_duration_ms) < CInt(EndTime_ms) Then
                            'it's not an Intro it's an outro 
                            Dim DeCh As Integer = StartTime_int - 1
                            Metadata = My.Resources.ffmpeg_metadata_out.Replace("[Start-1]", DeCh.ToString).Replace("[Start]", StartTime_ms).Replace("[duration_ms]", CR_episode_duration_ms)

                        Else
                            Metadata = My.Resources.ffmpeg_metadata.Replace("[Start]", StartTime_ms).Replace("[END]", EndTime_ms).Replace("[after]", AfterTime_ms).Replace("[duration_ms]", CR_episode_duration_ms)

                        End If

                        If Metadata = Nothing Then
                        Else
                            Dim utf8WithoutBom2 As New System.Text.UTF8Encoding(False)
                            Using sink As New StreamWriter(Mdata_File, False, utf8WithoutBom2)
                                sink.WriteLine(Metadata)
                                CR_MetadataUsage = True
                            End Using
                        End If


                    End If
                End If
            End If
            Debug.WriteLine("Checking chapters ended")
#End Region





#Region "lösche doppel download"
            'MsgBox(Pfad2)
            Dim Pfad5 As String = Pfad2.Replace(Chr(34), "")
            Dim Pfad6 As String = Pfad5
            Dim MergeAudio As Boolean = False

            If DownloadScope = DownloadScopeEnum.MergeAudio Then

                Pfad5 = FindExistingVideo(Path.GetDirectoryName(Pfad5), MergeSearch, Pfad5)

            End If
            '

            If My.Computer.FileSystem.FileExists(Pfad5) And DownloadScope = DownloadScopeEnum.OldDefault Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object
                                         Anime_Add.StatusLabel.Text = "Status: The file already exists."
                                         Me.Text = "Status: The file already exists."
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

            ElseIf My.Computer.FileSystem.FileExists(Pfad5) And DownloadScope = DownloadScopeEnum.MergeAudio Then


                Pfad6 = Path.GetDirectoryName(Pfad5) + "\" + GeräteID() + Path.GetExtension(Pfad5) '+ "."
                FileSystem.Rename(Pfad5, Pfad6)
                MergeAudio = True



            ElseIf My.Computer.FileSystem.FileExists(Path.GetDirectoryName(Pfad5) + "\" + Path.GetFileNameWithoutExtension(Pfad5) + "aac") And DownloadScope = DownloadScopeEnum.AudioOnly Then

                Me.Invoke(New Action(Function() As Object
                                         Anime_Add.StatusLabel.Text = "Status: The file already exists."
                                         Me.Text = "Status: The file already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                If MessageBox.Show("The file " + Path.GetDirectoryName(Pfad5) + "\" + Path.GetFileNameWithoutExtension(Pfad5) + "aac" + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Path.GetDirectoryName(Pfad5) + "\" + Path.GetFileNameWithoutExtension(Pfad5) + "aac")
                    Catch ex As Exception
                    End Try
                Else
                    Grapp_RDY = True
                    Exit Sub
                End If

            ElseIf DownloadScope = DownloadScopeEnum.AudioOnly Then

                'replace format with aac
                Pfad2 = Pfad2.Replace(VideoFormat, ".aac")

                'replace command for aac
                Dim ffmpeg_command_Builder() As String = ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)
                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)

            End If
#End Region


#Region "GetResolution"

            If Reso = 42 And HybridMode = False Then

                ffmpegInput = "-i " + Chr(34) + CR_URI_Master(0) + Chr(34)

            ElseIf DownloadScope = DownloadScopeEnum.SubsOnly Then
                ffmpegInput = "-i [Subtitles only]"
            Else

                Dim str As String = Nothing




                For i As Integer = 0 To CR_URI_Master.Count - 1
                    Dim Count As String = (i + 1).ToString
                    Try
                        str = Curl(CR_URI_Master(i))
                        If CBool(InStr(str, "curl:")) = False Then
                            Exit For
                        End If
                    Catch ex As Exception

                        Me.Invoke(New Action(Function() As Object
                                                 Anime_Add.StatusLabel.Text = "failed accessing master.m3u8 " + Count + "/" + CR_URI_Master.Count.ToString
                                                 Me.Text = "failed accessing master.m3u8 " + Count + "/" + CR_URI_Master.Count.ToString
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                        Debug.WriteLine("Error accessing master #" + i.ToString + " -- " + CR_URI_Master(i))
                        Pause(5)
                    End Try
                Next



                If CBool(InStr(str, "curl:")) = True Or str = Nothing Then

                    Debug.WriteLine("Checked " + CR_URI_Master.Count.ToString)
                    MsgBox("Unable to get master.m3u8" + vbNewLine + str, MsgBoxStyle.Critical)
                    Grapp_RDY = True
                    Exit Sub

                ElseIf DownloadScope = DownloadScopeEnum.AudioOnly Or MergeAudio = True Then

                    If CBool(InStr(str, My.Settings.AudioOnlyReso)) Then
                        ResoUsed = My.Settings.AudioOnlyReso.Replace(",", "")
                    ElseIf CBool(InStr(str, "x" + Reso.ToString + ",")) Then
                        ResoUsed = "x" + Reso.ToString
                    End If

                ElseIf CBool(InStr(str, "x" + Reso.ToString + ",")) Then
                    ResoUsed = "x" + Reso.ToString
                Else
                    If CBool(InStr(str, ResoSave + ",")) Then
                        ResoUsed = ResoSave
                    Else
                        Me.Invoke(New Action(Function() As Object
                                                 DialogTaskString = "Resolution"
                                                 ResoNotFoundString = str
                                                 ErrorDialog.ShowDialog()
                                                 Return Nothing
                                             End Function))
                        If UserCloseDialog = True Then
                            Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                        Else
                            ResoUsed = ResoBackString
                            ResoSave = ResoBackString
                        End If
                    End If
                End If

                'MsgBox(ResoUsed)

                Dim ffmpeg_url_3 As String = Nothing
                Dim LineChar As String = vbLf
                If CBool(InStr(str, vbCrLf)) Then
                    LineChar = vbCrLf
                ElseIf CBool(InStr(str, vbCr)) Then
                    LineChar = vbCr
                End If
                'MsgBox(str)
                Dim ffmpeg_url_1 As String() = str.Split(New String() {LineChar}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To ffmpeg_url_1.Count - 2 'Step 2
                    If CBool(InStr(ffmpeg_url_1(i), ResoUsed + ",")) Then
                        ffmpeg_url_3 = ffmpeg_url_1(i + 1)
                    End If
                Next

                'MsgBox(ffmpeg_url_3.Trim())

                ffmpegInput = "-i " + Chr(34) + ffmpeg_url_3.Trim() + Chr(34)

            End If



#End Region

#Region "GetSoftsubs"
            Dim SoftSubsAvailable As New List(Of String)
            Dim CCAvailable As New List(Of String)

            Dim SoftSubsList As New List(Of CR_Subtiles)




            Dim SplitVideo As String() = VideoJson.Split(New String() {Chr(34) + "closed_captions" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)

            If SoftSubs.Count > 0 And My.Settings.Captions = True Then
                For i As Integer = 0 To SoftSubs.Count - 1
                    If CBool(InStr(SplitVideo(1), Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + SoftSubs(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                        CCAvailable.Add(SoftSubs(i))
                    End If
                Next
            End If

            If SoftSubs.Count > 0 And CCAvailable.Count = 0 Then
                For i As Integer = 0 To SoftSubs.Count - 1
                    If CBool(InStr(VideoJson, Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + SoftSubs(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))) Then
                        SoftSubsAvailable.Add(SoftSubs(i))
                    End If
                Next
            ElseIf SoftSubs.Count > 0 And CCAvailable.Count > 0 Then
                SoftSubsAvailable.AddRange(CCAvailable)
            End If


            If DownloadScope = DownloadScopeEnum.AudioOnly Then

                If CR_MetadataUsage = False Then
                    ffmpegInput = ffmpegInput + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " " + ffmpeg_command_temp
                Else
                    ffmpegInput = ffmpegInput + " -i " + Chr(34) + Mdata_File + Chr(34) + " -map_metadata 1" + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " " + ffmpeg_command_temp
                End If

            ElseIf MergeAudio = True Then

                ffmpegInput = "-i " + Chr(34) + Pfad6 + Chr(34) + " " + ffmpegInput + " -map 0 -map 1:a" + " -metadata:s:a:" + FFMPEG_Audio(Pfad6).ToString + " language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " -c copy"


            ElseIf SoftSubsAvailable.Count > 0 Or CCAvailable.Count > 0 Then

                Dim MergeSubsNow As Boolean = MergeSubs

                If DownloadScope = DownloadScopeEnum.SubsOnly Then
                    MergeSubsNow = False
                End If

                Debug.WriteLine("Softsubs Default: " + DefaultSubCR)

                For i As Integer = 0 To SoftSubsAvailable.Count - 1

                    Dim SubsJson As String = VideoJson
                    If CCAvailable.Count > 0 Then
                        SubsJson = SplitVideo(1)
                    End If


                    Dim SoftSub As String() = SubsJson.Split(New String() {Chr(34) + "locale" + Chr(34) + ":" + Chr(34) + SoftSubsAvailable(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                    Dim SoftSub_3 As String = SoftSub_2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                    SoftSubsList.Add(New CR_Subtiles(SoftSubsAvailable(i), ConvertSubValue(SoftSubsAvailable(i), ConvertSubsEnum.DisplayText), " -i " + Chr(34) + SoftSub_3 + Chr(34), i.ToString, SoftSubsAvailable(i) = DefaultSubCR))

                Next


                If MergeSubsNow = True Then
                    Dim DispositionIndex As Integer = 69
                    Dim SoftSubMergeURLs As String = ""
                    Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
                    Dim SoftSubMergeMetatata As String = ""
                    Dim IndexMoveMap As Integer = 1
                    If CR_MetadataUsage = True Then
                        IndexMoveMap = 2
                    End If

                    For i As Integer = 0 To SoftSubsList.Count - 1

                        SoftSubMergeURLs = SoftSubMergeURLs + " " + SoftSubsList(i).Url
                        SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (i + IndexMoveMap).ToString
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + ConvertSubValue(SoftSubsList(i).SubLangValue, ConvertSubsEnum.MP4CC_ISO_639_2) + " -metadata:s:s:" + i.ToString + " title=" + Chr(34) + SoftSubsList(i).SubLangName + Chr(34) + " -metadata:s:s:" + i.ToString + " handler_name=" + Chr(34) + SoftSubsList(i).SubLangName + Chr(34)

                        If SoftSubsList(i).DefaultSub = True Then
                            DispositionIndex = i
                        End If

                    Next

                    Debug.WriteLine("-disposition:s: " + DispositionIndex.ToString)

                    If DispositionIndex < 69 Then
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If

                    If CR_MetadataUsage = False Then
                        ffmpegInput = ffmpegInput + " " + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command_temp + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2)
                    Else
                        ffmpegInput = ffmpegInput + " -i " + Chr(34) + Mdata_File + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " -map_metadata 1 " + ffmpeg_command_temp + " -c:s " + MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2)

                    End If


                Else

                    For i As Integer = 0 To SoftSubsList.Count - 1
                        Dim SubFormat As String = "ass"
                        If CCAvailable.Count > 0 Then
                            SubFormat = "vtt"
                        End If
                        Dim i2 As Integer = i
                        Me.Invoke(New Action(Function() As Object
                                                 Anime_Add.StatusLabel.Text = "Status: downloading subtitle file " + SoftSubsList(i2).SubLangName
                                                 Me.Text = "Status: downloading subtitle file " + SoftSubsList(i2).SubLangName
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))

                        Dim SubText As String = ""
                        SubText = Curl(SoftSubsList(i2).Url.Replace(" -i ", "").Replace(Chr(34), ""))
                        If My.Settings.SubtitleMod1 = True Then
                            SubText = AddScaledBorderAndShadow(SubText)
                        End If

                        Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + "." + ConvertSubValue(SoftSubsList(i2).SubLangValue, LangNameType) + Path.GetExtension(Pfad3)), SubFormat)
                        If i = 0 And IncludeLangName = False Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), SubFormat)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        WriteText(Pfad4, SubText)
                        Pause(3)

                    Next

                    If CR_MetadataUsage = False Then
                        ffmpegInput = ffmpegInput + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " " + ffmpeg_command_temp
                    Else
                        ffmpegInput = ffmpegInput + " -i " + Chr(34) + Mdata_File + Chr(34) + " -map_metadata 1" + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " " + ffmpeg_command_temp
                    End If
                End If
            Else

                If CR_MetadataUsage = False Then
                    ffmpegInput = ffmpegInput + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " " + ffmpeg_command_temp
                Else
                    ffmpegInput = ffmpegInput + " -i " + Chr(34) + Mdata_File + Chr(34) + " -map_metadata 1" + " -metadata:s:a:0 language=" + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.MP4CC_ISO_639_2) + " " + ffmpeg_command_temp
                End If
            End If

            ffmpegInput = RemoveExtraSpaces(ffmpegInput)
#End Region

#Region "thumbnail"
            Dim thumbnail3 As String = ""

            Try

                Dim thumbnail As String() = ObjectJson.Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To thumbnail.Count - 1
                    If CBool(InStr(thumbnail(i), ".jpg" + Chr(34))) Then
                        Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".jpg" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                        thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".jpg"
                        Exit For
                    ElseIf CBool(InStr(thumbnail(i), ".jpeg" + Chr(34))) Then
                        Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".jpeg" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                        thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".jpeg"
                        Exit For
                    ElseIf CBool(InStr(thumbnail(i), ".jpe" + Chr(34))) Then
                        Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".jpe" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                        thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".jpe"
                        Exit For
                    ElseIf CBool(InStr(thumbnail(i), ".JPEG" + Chr(34))) Then
                        Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".JPEG" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                        thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".JPEG"
                        Exit For
                    ElseIf CBool(InStr(thumbnail(i), ".JPG" + Chr(34))) Then
                        Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".JPG" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                        thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".JPG"
                        Exit For
                    ElseIf CBool(InStr(thumbnail(i), ".JPE" + Chr(34))) Then
                        Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".JPE" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                        thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".JPE"
                        Exit For
                    End If
                Next

            Catch ex As Exception

            End Try
#End Region

#Region "item constructor"

#Region "Display Hard_Softsubs"
            Dim SubType_Value As String = Nothing
            If Not CR_HardSubLang = "" Then
                SubType_Value = "Hardsub: " + ConvertSubValue(CR_HardSubLang, ConvertSubsEnum.DisplayText)
            End If
            If SoftSubsList.Count > 0 And CR_HardSubLang = "" Then
                SubType_Value = "Softsubs: "
                For i As Integer = 0 To SoftSubsList.Count - 1
                    SubType_Value = SubType_Value + SoftSubsList(i).SubLangName
                    If i < SoftSubsList.Count - 1 Then
                        SubType_Value = SubType_Value + ", "
                    End If
                Next
            End If
#End Region


#Region "Display Resolution"


            Dim ResoHTMLDisplay As String = Nothing
            Dim ResoHTML As String() = ResoUsed.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
            If ResoHTML.Count > 1 Then
                ResoHTMLDisplay = ResoHTML(1) + "p"
            Else
                ResoHTMLDisplay = ResoHTML(0) + "p"
            End If

            Dim L2Name As String = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName_Backup, "[^\w\\-]", " ")

            If Reso = 42 And HybridMode = False Then
                ResoHTMLDisplay = "[Auto]"
            End If
#End Region


            Dim L1Name_Split As String() = WebsiteURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.DisplayText)

            'MsgBox(URL_DL)


            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(Path.GetFileName(Pfad2.Replace(Chr(34), "")), L1Name, L2Name, ResoHTMLDisplay, SubType_Value, thumbnail3, ffmpegInput, Pfad2)
                                     Return Nothing
                                 End Function))
            'liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_title + " <br> " + CR_season_number + " " + CR_episode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
            'Form1.RichTextBox1.Text = My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->"
#End Region
            Grapp_RDY = True
            Me.Invoke(New Action(Function() As Object
                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     ResoBackString = Nothing
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))

        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object
                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Me.Text = "Crunchyroll Downloader"
                                     ResoBackString = Nothing
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
            Grapp_RDY = True
            If CBool(InStr(ex.ToString, "Could not find the sub language")) Then
                MsgBox(Sub_language_NotFound + SubSprache.DisplayText)
            ElseIf CBool(InStr(ex.ToString, "RESOLUTION Not Found")) Then
                MsgBox(Resolution_NotFound)
            ElseIf CBool(InStr(ex.ToString, "Premium Episode")) Then
                MsgBox(Premium_Stream, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "System.UnauthorizedAccessException")) Then
                MsgBox(ErrorNoPermisson + vbNewLine + ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, Chr(34) + "UserAbort" + Chr(34))) Then
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "Error - Getting")) Then

                MsgBox(ex.ToString)
                'If RT_count = 0 Then
                '    If File.Exists("cookies.txt") = True Then
                '        MsgBox("Request refused, try a new cookies.txt", MsgBoxStyle.Exclamation)
                '        Exit Sub
                '    End If
                '    Me.Invoke(New Action(Function() As Object
                '                             Anime_Add.StatusLabel.Text = "Browser reset..."
                '                             Me.Text = "Browser reset..."
                '                             ResoBackString = Nothing
                '                             Me.Invalidate()
                '                             Return Nothing
                '                         End Function))

                '    If Application.OpenForms().OfType(Of Browser).Any = True Then
                '        Browser.Close()
                '        Startseite = WebsiteURL
                '        Pause(5)
                '        UserBowser = False
                '        Browser.Show()
                '        'Anime_Add.btn_dl.Cursor = Cursors.Default
                '        'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
                '    End If

                '    'Navigate(WebsiteURL)
                '    'Pause(5)
                '    'LoadBrowser(WebsiteURL, 1)
                '    Exit Sub
                'End If
                ' MsgBox(ex.ToString)
                ' b = False
                ' Navigate(WebsiteURL)
            Else
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            End If
        End Try '
    End Sub

    Function Convert_locale(ByVal locale As String) As String
        Try
            If locale = "de" Then
                Return "de-DE"
            ElseIf locale = "" Then
                Return "en-US"
            ElseIf locale = "pt-br" Then
                Return "pt-BR"
            ElseIf locale = "es" Then
                Return "es-419"
            ElseIf locale = "fr" Then
                Return "fr-FR"
            ElseIf locale = "ar" Then
                Return "ar-SA"
            ElseIf locale = "ru" Then
                Return "ru-RU"
            ElseIf locale = "it" Then
                Return "it-IT"
            ElseIf locale = "es-es" Then
                Return "es-ES"
            ElseIf locale = "pt-pt" Then
                Return "pt-PT"
            Else
                Return "en-US"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        If RunningDownloads > 0 Then
            If MessageBox.Show("Are you sure you want close the program and end all active downloads?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim Item As New List(Of CRD_List_Item)
                Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
                Item.Reverse()

                For i As Integer = 0 To Item.Count - 1
                    Item(i).KillRunningTask()
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
                If CBool(InStr(file, "CRD-Temp-File-")) Or CBool(InStr(file, "-mdata.txt")) Then
                    System.IO.File.Delete(file)
                End If
            Next
        Catch ex As Exception
        End Try
        Try

            If KeepCache = False Then


                Dim folderPath As String = Pfad
                Dim files() As String = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)

                For Each file As String In files
                    If CBool(InStr(file, "CRD-Temp-File-")) Then
                        'MsgBox(file)
                        System.IO.File.Delete(file)
                    End If
                Next
                Try
                    Dim di As New System.IO.DirectoryInfo(Pfad)
                    For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                        If CBool(InStr(fi.Name, "CRD-Temp-File-")) Then
                            System.IO.Directory.Delete(fi.FullName, True)
                        End If
                    Next
                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception

        End Try
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
                                                         ListItemAdd(Filename, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, thumbnail3, URL2, Pfad2)
                                                         Return Nothing
                                                     End Function))
                                ' liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + L1Name + " <br> " + L2Name + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
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

        If File.Exists("cookies.txt") = False Then
            If Application.OpenForms().OfType(Of Browser).Any = True Then
            Else
                UserBowser = False
                Browser.Show()
            End If
        End If

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

    Private Sub OpenSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs)
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

        'Dim Teststring As String = TheTextBox.Text
        'TheTextBox.Text = AddScaledBorderAndShadow(Teststring)
        'Exit Sub
        'Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        UserBowser = True

        If Application.OpenForms().OfType(Of Browser).Any = True Then
            Browser.Location = Me.Location
        Else
            Browser.Location = Me.Location
            Browser.Show()
        End If


    End Sub

    Public Function RemoveExtraSpaces(input_text As String) As String
        Dim rsRegEx As System.Text.RegularExpressions.Regex
        rsRegEx = New System.Text.RegularExpressions.Regex("\s+")
        Return rsRegEx.Replace(input_text, " ").Trim()
    End Function



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim ItemFinshedCount As Integer = 0
            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For i As Integer = 0 To Item.Count - 1
                'Debug.WriteLine(Item(i).GetIsStatusFinished().ToString)
                If Item(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next

            RunningDownloads = Item.Count - ItemFinshedCount

            If RunningDownloads > 0 Then
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_CONTINUOUS)
            Else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
            End If
        Catch ex As Exception
            Debug.WriteLine("Failed? : " + ex.ToString)

            RunningDownloads = Panel1.Controls.Count
        End Try
        'Debug.WriteLine("Running: " + RunningDownloads.ToString)

        'FontLabel2.Text = RunningDownloads.ToString
        'Debug.WriteLine("downloads.tick: " + RunningDownloads.ToString)
    End Sub

#Region "Funimation JS "
    Public Sub GetFunimationJS_Seasons(Optional ByVal JsonUrl As String = Nothing, Optional ByVal Json As String = Nothing)
        FunimtaionSeasonList.Clear()
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
                    client.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))
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
        'Debug.WriteLine("SeasonJson: ")
        Anime_Add.groupBox2.Visible = True
        Anime_Add.bt_Cancel_mass.Enabled = True
        Anime_Add.bt_Cancel_mass.Visible = True
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
            Dim Anzahl As Integer = 0

            If Anime_Add.comboBox4.SelectedIndex > Anime_Add.comboBox3.SelectedIndex Then
                First = Anime_Add.comboBox3.SelectedIndex
                Last = Anime_Add.comboBox4.SelectedIndex
                Anzahl = Last - First + 1
            ElseIf Anime_Add.comboBox4.SelectedIndex < Anime_Add.comboBox3.SelectedIndex Then
                First = Anime_Add.comboBox4.SelectedIndex
                Last = Anime_Add.comboBox3.SelectedIndex

                Anime_Add.comboBox4.SelectedIndex = Last
                Anime_Add.comboBox3.SelectedIndex = First
                Anzahl = Last - First + 1
            ElseIf Anime_Add.comboBox4.SelectedIndex = Anime_Add.comboBox3.SelectedIndex Then

                First = Anime_Add.comboBox4.SelectedIndex
                Last = Anime_Add.comboBox4.SelectedIndex

                Anzahl = Last - First + 1
            End If

            Anime_Add.Add_Display.Text = Anzahl.ToString + " episodes selected"

            For i As Integer = First To Last
                For e As Integer = 0 To Integer.MaxValue
                    If Funimation_Grapp_RDY = True Then
                        Try
                            Dim ItemFinshedCount As Integer = 0
                            Dim Item As New List(Of CRD_List_Item)
                            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
                            Item.Reverse()

                            For i2 As Integer = 0 To Item.Count - 1
                                If Item(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            RunningDownloads = Panel1.Controls.Count - ItemFinshedCount
                        Catch ex As Exception
                            RunningDownloads = Panel1.Controls.Count
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
                    'Anime_Add.ListBox1.Items.Add(ListOfEpisodes(i))
                    ListBoxList.Add(ListOfEpisodes(i))
                    Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    Anime_Add.Add_Display.ForeColor = Color.Black
                Else
                    Funimation_Grapp_RDY = False
                    b = False

                    If CBool(InStr(ListOfEpisodes(i), "funimation.com/v/")) Then
                        Dim Episode0() As String = ListOfEpisodes(i).Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim Episode() As String = Episode0(0).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
                        'MsgBox(v1JsonUrl)
                        Dim v1Json As String = Nothing
                        Try
                            Using client As New WebClient()
                                client.Encoding = System.Text.Encoding.UTF8
                                client.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))
                                v1Json = client.DownloadString(v1JsonUrl)
                            End Using
                            WebbrowserURL = ListOfEpisodes(i)
                            GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                        Catch ex As Exception
                            Debug.WriteLine("error- getting v1Json data for the bypasss")
                            Debug.WriteLine(ex.ToString)
                            Navigate(ListOfEpisodes(i))
                        End Try
                    Else
                        Navigate(ListOfEpisodes(i))
                    End If

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
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        FunimationEpisodeJSON = Nothing
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
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
    Public Async Sub GetFunimationNewJS_VideoProxy(Optional ByVal v1JsonURL As String = Nothing, Optional ByVal v1JsonData As String = Nothing)
        Try
            Dim list As List(Of CoreWebView2Cookie) = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com/")
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

        Catch ex As Exception

        End Try
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
                    client.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))
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
                            FunimationEpisode = "Episode " + AddLeadingZeros(FunimationEpisode3)
                        Else
                            FunimationEpisode = Episode_Prefix + AddLeadingZeros(FunimationEpisode3)
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
            FunimationTitle = RemoveExtraSpaces(String.Join(" ", FunimationTitle.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)).Replace(Chr(34), "").Replace("\", "").Replace("/", "")
            FunimationEpisodeTitle = String.Join(" ", FunimationEpisodeTitle.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "")
            FunimationDub = ConvertFunimationDub(DubFunimation) 'FunimationDub2(0)
            Dim DefaultName As String = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)

            Dim NameParts As String() = NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 0 To NameParts.Count - 1

                If NameParts(i) = "AnimeTitle" Then
                    DefaultName = DefaultName + " " + FunimationTitle
                ElseIf NameParts(i) = "Season" Then
                    DefaultName = DefaultName + " " + FunimationSeason
                ElseIf NameParts(i) = "EpisodeNR" Then
                    DefaultName = DefaultName + " " + FunimationEpisode
                ElseIf NameParts(i) = "EpisodeName" Then
                    DefaultName = DefaultName + " " + FunimationEpisodeTitle
                ElseIf NameParts(i) = "AnimeDub" Then
                    DefaultName = DefaultName + " RepDub"
                ElseIf NameParts(i) = "AnimeSub" Then
                    DefaultName = DefaultName + " RepSub"
                End If

            Next


            If CBool(InStr(DefaultName, " RepDub")) Then
                DefaultName = DefaultName.Replace(" RepDub", "")
            End If

            If CBool(InStr(DefaultName, " RepSub")) Then
                DefaultName = DefaultName.Replace(" RepSub", "")
            End If


            'If CR_NameMethode = 1 Then
            '    DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisodeTitle)
            'ElseIf CR_NameMethode = 2 Then
            '    DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode + " " + FunimationEpisodeTitle)
            'ElseIf CR_NameMethode = 3 Then
            '    DefaultName = RemoveExtraSpaces(FunimationTitle + " " + FunimationEpisodeTitle + " " + FunimationSeason + " " + FunimationEpisode)
            'End If


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
                                         Anime_Add.StatusLabel.Text = "Status: File already exists."
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: Old file overwritten."
                                                 Anime_Add.StatusLabel.Text = "Status: Old file overwritten."
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                    Catch ex As Exception
                    End Try
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Crunchyroll Downloader"
                                             Anime_Add.StatusLabel.Text = "idle"
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
            PlayerClient.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))
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
                Debug.WriteLine("showexperience 2nd try")
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
                        'MsgBox(SubItem.Value.ToString())
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
                               ''MsgBox()
                                Case "version"
                                    PrimaryVersion = SubItem.Value.ToString
                                Case "audioLanguage"
                                    PrimaryaudioLanguage = SubItem.Value.ToString
                                Case "manifestPath"
                                    PrimarymanifesUrl = SubItem.Value.ToString
                                Case "subtitles"
                                    'MsgBox(SubItem.Value.ToString())
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
            If DownloadScope = DownloadScopeEnum.SubsOnly Then

                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: Substitles only mode - skipped video"
                                         Anime_Add.StatusLabel.Text = "Status: Substitles only mode - skipped video"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

                'ElseIf DownloadScope = DownloadScopeEnum.MergeAudio Then


            ElseIf DownloadScope = DownloadScopeEnum.OldDefault Or DownloadScope = DownloadScopeEnum.MergeAudio Or DownloadScope = DownloadScopeEnum.AudioOnly Then

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

                'MsgBox(Funimation_m3u8_Main)
                'Funimation_m3u8_Main = InputBox("Edit Url", "Change")

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
                                         Anime_Add.StatusLabel.Text = "Status: Video found!"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))
                Dim str1 As String = client0.DownloadString(Funimation_m3u8_Main.Replace(Chr(34), ""))
                If CBool(InStr(str1, "# AUDIO groups")) Then
                    Dim FunimationAudio() As String = str1.Split(New String() {"# AUDIO groups"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio2() As String = FunimationAudio(1).Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio3() As String = FunimationAudio2(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    FunimationAudioMap = " -headers " + My.Settings.User_Agend + " -i " + Chr(34) + FunimationAudio3(0) + Chr(34)
                End If

                Dim str2() As String = str1.Split(New String() {"# keyframes"}, System.StringSplitOptions.RemoveEmptyEntries)


                Dim Streams() As String = str2(0).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim FunimationBackupm3u8 As String = Nothing

                Dim Tartegt_m3u8_list As New List(Of String)

                Dim Secondary_m3u8_list As New List(Of String)


                For i As Integer = 0 To Streams.Length - 1


                    If CBool(InStr(Streams(i), "x" + Reso.ToString)) Then

                        Tartegt_m3u8_list.Add(Streams(i) + vbCrLf + Streams(i + 1))
                        FunimationBackupm3u8 = Streams(i + 1)

                    ElseIf CBool(InStr(Streams(i), ResoFunBackup)) And FunimationBackupm3u8 = Nothing Then

                        Secondary_m3u8_list.Add(Streams(i) + vbCrLf + Streams(i + 1))
                        FunimationBackupm3u8 = Streams(i + 1)

                    End If

                Next

                If Tartegt_m3u8_list.Count = 0 And Secondary_m3u8_list.Count > 0 Then
                    Tartegt_m3u8_list = Secondary_m3u8_list
                End If

                If Tartegt_m3u8_list.Count > 1 Then
                    Dim HigestBitrate As Integer = 0
                    For i2 As Integer = 0 To Tartegt_m3u8_list.Count - 1
                        Dim Bandwidth_String As String = Nothing
                        If CBool(InStr(Tartegt_m3u8_list.Item(i2), "AVERAGE-BANDWIDTH=")) = True Then
                            Bandwidth_String = "AVERAGE-BANDWIDTH="
                        ElseIf CBool(InStr(Tartegt_m3u8_list.Item(i2), "BANDWIDTH=")) = True Then
                            Bandwidth_String = "BANDWIDTH="
                        Else
                            Continue For
                        End If

                        Dim BitRate() As String = Tartegt_m3u8_list.Item(i2).Split(New String() {Bandwidth_String}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim BitRate2() As String = BitRate(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                        If Funimation_Bitrate = 0 Then
                            If CInt(BitRate2(0)) > HigestBitrate Then
                                HigestBitrate = CInt(BitRate2(0))
                            End If
                        Else

                            If HigestBitrate > CInt(BitRate2(0)) Then
                                HigestBitrate = CInt(BitRate2(0))
                            ElseIf HigestBitrate = 0 Then
                                HigestBitrate = CInt(BitRate2(0))
                            End If
                        End If
                    Next

                    For i2 As Integer = 0 To Tartegt_m3u8_list.Count - 1
                        If CBool(InStr(Tartegt_m3u8_list.Item(i2), HigestBitrate.ToString)) = True Then
                            Dim new_m3u8_2() As String = Tartegt_m3u8_list.Item(i2).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                            Funimation_m3u8_final = new_m3u8_2(1)
                            FunimationBackupm3u8 = new_m3u8_2(1)
                        End If
                    Next
                ElseIf Tartegt_m3u8_list.Count = 1 Then
                    Dim new_m3u8_2() As String = Tartegt_m3u8_list.Item(0).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                    Funimation_m3u8_final = new_m3u8_2(1)
                    FunimationBackupm3u8 = new_m3u8_2(1)
                End If



                If Funimation_m3u8_final = Nothing And FunimationBackupm3u8 = Nothing Then
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Resolution not found!"
                                             Anime_Add.StatusLabel.Text = "Status: Resolution not found!"
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
                                             Anime_Add.StatusLabel.Text = "Status: Resolution found!"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))
                End If
                Debug.WriteLine("Funimation_m3u8_final: " + Funimation_m3u8_final)
                Funimation_m3u8_final = Funimation_m3u8_final.Replace(Chr(34), "")

            End If
            'MsgBox(FunimationName3)
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
                If MergeSubs = True And DownloadScope = 0 Then
                    Dim DispositionIndex As Integer = 999
                    Dim LastMerged As String = Nothing
                    Dim MapCount As Integer = -1
                    For i As Integer = 0 To UsedSubs.Count - 1
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        If ConvertSubValue(SoftSub(1), ConvertSubsEnum.MP4CC_ISO_639_2) = LastMerged Then
                            Continue For
                        Else
                            LastMerged = ConvertSubValue(SoftSub(1), ConvertSubsEnum.MP4CC_ISO_639_2)
                        End If
                        MapCount = MapCount + 1
                        If DefaultSubFunimation = SoftSub(1) Then
                            'Debug.WriteLine(SoftSub(1))
                            DispositionIndex = MapCount
                        End If
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -headers " + My.Settings.User_Agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -headers " + My.Settings.User_Agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        End If
                        If FunimationAudioMap = Nothing Then
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 1).ToString
                        Else
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 2).ToString
                        End If
                        If SoftSubMergeMetatata = Nothing Then
                            'SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + ConvertSubValue(SoftSub(1))
                            SoftSubMergeMetatata = " -metadata:s:s:" + MapCount.ToString + " language=" + ConvertSubValue(SoftSub(1), ConvertSubsEnum.MP4CC_ISO_639_2) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + ConvertSubValue(Chr(34) + SoftSub(1) + Chr(34), ConvertSubsEnum.DisplayText) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + ConvertSubValue(Chr(34) + SoftSub(1) + Chr(34), ConvertSubsEnum.DisplayText) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + MapCount.ToString + " language=" + ConvertSubValue(SoftSub(1), ConvertSubsEnum.MP4CC_ISO_639_2) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + ConvertSubValue(Chr(34) + SoftSub(1) + Chr(34), ConvertSubsEnum.DisplayText) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + ConvertSubValue(Chr(34) + SoftSub(1) + Chr(34), ConvertSubsEnum.DisplayText) + Chr(34)
                            'SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + ConvertSubValue(SoftSubs2(i))
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
                        'File.WriteAllText(Pfad4, Subfile, Encoding.UTF8)
                        WriteText(Pfad4, Subfile)
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
                    Funimation_m3u8_final = FunimationAudioMap.Replace(" -headers " + My.Settings.User_Agend + " ", "") + DubMetatata + " " + ffmpeg_command_temp
                End If
            Else
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + DubMetatata + " " + ffmpeg_command
            End If
            Funimation_m3u8_final = Funimation_m3u8_final + " -metadata:g encoding_tool=CrD_Funimation_JS"
#End Region
            'MsgBox(Funimation_m3u8_final)
            'DownloadPfad = DownloadPfad.Replace(" \", "\")
            If DownloadScope = 1 Then
                Funimation_m3u8_final = "-i [Subtitles only]"
            End If
            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Me.Invoke(New Action(Function() As Object
                                     ListItemAdd(DownloadPfad, L1Name, DefaultName, ResoHTMLDisplay, Funimation_m3u8_MainVersion, thumbnail4, Funimation_m3u8_final, DownloadPfad, "FM")
                                     Return Nothing
                                 End Function))
            'liList.Add(My.Resources.htmlvorThumbnail + thumbnail4 + My.Resources.htmlnachTumbnail + FunimationTitle + " <br> " + FunimationSeason + " " + FunimationEpisode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + DefaultName + "-->")
#End Region
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader"
                                     Anime_Add.StatusLabel.Text = "idle"
                                     ResoBackString = Nothing
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
        Catch ex As Exception
            Me.Invoke(New Action(Function() As Object
                                     Me.Text = "Crunchyroll Downloader!"
                                     Anime_Add.StatusLabel.Text = "idle"
                                     ResoBackString = Nothing
                                     Me.Invalidate()
                                     Return Nothing
                                 End Function))
            MsgBox(ex.ToString)
        End Try
        Funimation_Grapp_RDY = True
    End Sub

#End Region
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        Dim Item As New List(Of CRD_List_Item)
        Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
        Item.Reverse()

        Dim GeckoHTML As String = My.Resources.htmlTop + vbNewLine + My.Resources.htmlTitlel.Replace("Placeholder", Me.Text.Replace("open the add window to continue", ""))

        For i As Integer = 0 To Item.Count - 1
            Dim ItemString As String = My.Resources.htmlvorThumbnail + Item(i).GetThumbnailSource + My.Resources.htmlnachTumbnail + Item(i).Label_website.Text + " <br> " + Item(i).Label_Anime.Text + My.Resources.htmlvorAufloesung.Replace("0%", Item(i).Label_percent.Text).Replace("width:0%", Item(i).GetPercentValue.ToString + "%") + Item(i).Label_Reso.Text + My.Resources.htmlvorSoftSubs + vbNewLine + My.Resources.htmlvorHardSubs + Item(i).Label_Hardsub.Text + My.Resources.htmlnachHardSubs
            GeckoHTML = GeckoHTML + vbNewLine + ItemString
        Next



        Dim c As String = GeckoHTML + vbNewLine + My.Resources.htmlEnd
        Dim Balken As String = "balken.png"
        c = c.Replace("balken1.png", Balken)
        Dim CC As String = "cc.png"
        c = c.Replace("cc1.png", CC)
        HTML = c

    End Sub




#Region "process html"
    Public Sub ProcessHTML(ByVal document As String, ByVal Address As String, ByVal DocumentTitle As String)
        Dim localHTML As String = document
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        Debug.WriteLine(Address)

        If CBool(InStr(Address, "title-api.prd.funimationsvc.com")) Then
            If FunimationJsonBrowser = "EpisodeJson" Then
                Anime_Add.FillFunimationEpisodes(localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; 
-space: pre-wrap;" + Chr(34) + ">", "")) '
                FunimationJsonBrowser = Nothing
                WebbrowserURL = "https://funimation.com/js"
            ElseIf FunimationJsonBrowser = "v1Json" Then
                GetFunimationNewJS_VideoProxy(Nothing, localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; white-space: pre-wrap;" + Chr(34) + ">", "")) '
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
                        client.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))
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
            LoadedUrls.Clear()
            Grapp_RDY = True
            Debug.WriteLine("Just Browsing, exiting...")
            'Debug.WriteLine("Just Browsing, exiting... for real...")
            Exit Sub
        End If
        'MsgBox("loaded!")
        If CBool(InStr(Address, "crunchyroll.com")) Or CBool(InStr(Address, "funimation.com")) Then
            WebbrowserURL = Address

            ScanTimeout.Start()


            'ElseIf CBool(InStr(Address, "funimation.com")) Then

            '    Dim list As List(Of CoreWebView2Cookie) = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com")
            '    Dim Cookie As String = ""
            '    For i As Integer = 0 To list.Count - 1
            '        If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
            '            'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
            '            Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
            '        End If
            '        If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
            '            'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
            '            FunimationToken = "Token " + list.Item(i).Value
            '        End If
            '    Next
            '    If b = False Then
            '        WebbrowserCookie = Cookie
            '        WebbrowserURL = Address
            '        Text = "Crunchyroll Downloader"
            '        For i As Integer = 10 To 0 Step -1
            '            Anime_Add.StatusLabel.Text = "Status: checking traffic - " + i.ToString
            '            Pause(1)
            '        Next
            '        Dim Evaluator = New Thread(Sub() Me.ProcessUrls())
            '        Evaluator.Start()
            '        Exit Sub
            '    End If
            'Else
            '    WebbrowserURL = Address
            '    Text = "Crunchyroll Downloader"
            '    For i As Integer = 10 To 0 Step -1
            '        Anime_Add.StatusLabel.Text = "Status: checking traffic - " + i.ToString
            '        Pause(1)
            '    Next
            '    ProcessUrls()
            '    'Pause(10)
            '    'ProcessUrls()
        End If
        'End If
    End Sub




#End Region

    Private Sub Process(sender As Object, e As EventArgs) Handles ScanTimeout.Tick
        If b = True Then
            If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                Anime_Add.StatusLabel.Text = "Status: idle"
            End If
            Me.Text = "Crunchyroll Downloader"
            Grapp_RDY = True
            LoadedUrls.Clear()
            Debug.WriteLine("canceled....")
            ProcessCounting = 30
            ScanTimeout.Enabled = False
            Exit Sub
        End If

        If LoadedUrls.Count = 0 And ProcessCounting > 0 Then

            If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                Anime_Add.StatusLabel.Text = "Status: Processing Url " + ProcessCounting.ToString
            End If
            Me.Text = "Status: Processing Url " + ProcessCounting.ToString

            ProcessCounting = ProcessCounting - 1
            Exit Sub
        ElseIf LoadedUrls.Count = 1 And ProcessCounting > 0 Then

            If CBool(InStr(LoadedUrls.Item(0).Uri, "/objects/")) Then
                If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                    Anime_Add.StatusLabel.Text = "Status: Processing Url " + ProcessCounting.ToString
                End If
                Me.Text = "Status: Processing Url " + ProcessCounting.ToString

                ProcessCounting = ProcessCounting - 1
                Exit Sub
            End If

        ElseIf LoadedUrls.Count = 0 And ProcessCounting = 0 Then
            If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                Anime_Add.StatusLabel.Text = "Status: nothing found"
            End If
            Me.Text = "Status: nothing found"
            'ProcessUrls()
            b = True
            Debug.WriteLine("3508: nothing found")
            Grapp_RDY = True
            ProcessCounting = 30
            ScanTimeout.Enabled = False
            Exit Sub
        End If


        Debug.WriteLine("LoadedUrls: " + LoadedUrls.Count.ToString)
        'For i As Integer = 0 To LoadedUrls.Count - 1
        '    Debug.WriteLine("LoadedUrls: " + LoadedUrls(i))
        'Next

        If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
            Anime_Add.StatusLabel.Text = "Status: Processing... "
        End If
        Me.Text = "Status: Processing... "
        Debug.WriteLine("ProcessUrls")
        ProcessCounting = 30
        ScanTimeout.Enabled = False
        ProcessUrls()

        Exit Sub



    End Sub
    Public Sub ProcessUrls()
        Debug.WriteLine(LoadedUrls.Count.ToString)
        Debug.WriteLine(Date.Now.ToString + " Thread Name: " + Thread.CurrentThread.Name)
        Dim SavedObjectsUrl = ""
        For i As Integer = 0 To LoadedUrls.Count - 1

            Dim Request As CoreWebView2WebResourceRequest = LoadedUrls.Item(i)


            If CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "streams?")) Then

                If b = False Then

                    If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        Anime_Add.StatusLabel.Text = "Status: Crunchyroll episode found."
                    End If
                    Me.Text = "Status: Crunchyroll episode found."
                    Debug.WriteLine("Crunchyroll episode found")
                    GetCRVideoProxy(Request.Uri, CR_AuthToken, WebbrowserURL, 0)
                    b = True
                    LoadedUrls.Clear()
                    Me.Text = "Crunchyroll Downloader"
                    Exit Sub
                End If

            ElseIf CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "seasons?preferred_audio_language=")) And CBool(InStr(WebbrowserURL, "series")) Then

                If b = False Then

                    If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        Anime_Add.StatusLabel.Text = "Status: Crunchyroll season found."
                    End If
                    Me.Text = "Status: Crunchyroll season found."
                    Debug.WriteLine("Crunchyroll season found")

                    Dim Auth As String = " -H " + Chr(34) + "Authorization: " + Request.Headers.GetHeader("Authorization") + Chr(34)
                    Debug.WriteLine(Auth)

                    CR_Cookies = "Cookie: " + Request.Headers.GetHeader("Cookie")

                    GetBetaSeasons(WebbrowserURL, Request.Uri, Auth)

                    'Browser.WebBrowser1.LoadUrl(Request.Uri)
                    b = True
                    LoadedUrls.Clear()
                    Me.Text = "Crunchyroll Downloader"
                    Exit Sub
                End If
            ElseIf CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "seasons?series_id=")) Then

                If b = False Then

                    If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        Anime_Add.StatusLabel.Text = "Status: Error found invalid data."
                    End If
                    b = True
                    LoadedUrls.Clear()
                    Me.Text = "Crunchyroll Downloader"
                    Exit Sub
                End If

            End If

            If CBool(InStr(Request.Uri, "/data/v2/shows/")) Then
                b = True
                'MsgBox("The new Funimation Overview is not supportet yet!", MsgBoxStyle.Information)
                Me.Invoke(New Action(Function() As Object
                                         'My.Computer.Clipboard.SetText(localHTML)
                                         GetFunimationJS_Seasons(Request.Uri)
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))
                LoadedUrls.Clear()
                Me.Text = "Crunchyroll Downloader"
                Exit Sub
            End If
            If CBool(InStr(Request.Uri, "data/v1/episodes/")) Then
                b = True
                'MsgBox("The new Funimation Overview is not supportet yet!", MsgBoxStyle.Information)
                Me.Invoke(New Action(Function() As Object
                                         'My.Computer.Clipboard.SetText(localHTML)
                                         GetFunimationNewJS_VideoProxy(Request.Uri)
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))
                LoadedUrls.Clear()
                Me.Text = "Crunchyroll Downloader"
                Exit Sub
            End If
            If CBool(InStr(Request.Uri, "https://title-api.prd.funimationsvc.com")) And CBool(InStr(Request.Uri, "?region=")) Then
                If FunimationAPIRegion = Nothing Then
                    Me.Invoke(New Action(Function() As Object
                                             Dim parms As String() = Request.Uri.Split(New String() {"?region="}, System.StringSplitOptions.RemoveEmptyEntries)
                                             FunimationAPIRegion = "?region=" + parms(1)
                                             Return Nothing
                                         End Function))
                End If
                If b = False Then

                    If CBool(InStr(Request.Uri, "https://title-api.prd.funimationsvc.com/v1/show")) And CBool(InStr(Request.Uri, "/episodes/")) Then
                        b = True
                        GetFunimationNewJS_VideoProxy(Request.Uri)
                        Debug.WriteLine("processing :" + Request.Uri)
                        LoadedUrls.Clear()
                        Me.Text = "Crunchyroll Downloader"
                        Exit Sub
                    End If
                End If
            End If
        Next

        LoadedUrls.Clear()

        If b = True Then
            LoadedUrls.Clear()
            Debug.WriteLine("Just Browsing after all, exiting...")
            Grapp_RDY = True
            Me.Text = "Crunchyroll Downloader"
            Exit Sub
        End If

    End Sub

    Public Sub Navigate(ByVal Url As String)
        If Application.OpenForms().OfType(Of Browser).Any = True Then
            If InvokeRequired = True Then
                Me.Invoke(New Action(Function() As Object
                                         Browser.WebView2.CoreWebView2.Navigate(Url)
                                         Return Nothing
                                     End Function))
            Else
                Browser.WebView2.CoreWebView2.Navigate(Url)
            End If
        Else
            If InvokeRequired = True Then
                Me.Invoke(New Action(Function() As Object
                                         Browser.Show()
                                         Browser.WebView2.CoreWebView2.Navigate(Url)
                                         Return Nothing
                                     End Function))
            Else
                Browser.Show()
                Browser.WebView2.CoreWebView2.Navigate(Url)
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
        Debug.WriteLine(htmlReq)
        ' Dim recvBytes(1048576) As Byte
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

#Region "mass-dl"


                If CBool(InStr(htmlReq, "HTMLMass=")) Then
                    Debug.WriteLine("multi episode mode")
                    Try
                        Dim html() As String = htmlReq.Split(New String() {"HTMLMass="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {"javascript:"}, System.StringSplitOptions.RemoveEmptyEntries)
                        'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        '    For i As Integer = 0 To URLSplit.Count - 1
                        '        Dim ii As Integer = i
                        '        Me.Invoke(New Action(Function() As Object
                        '                                 If Anime_Add.ListBox1.Items.Contains(URLSplit(ii)) = False Then
                        '                                     Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                        '                                 End If
                        '                                 'Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                        '                                 Return Nothing
                        '                             End Function))
                        '    Next
                        'Else
                        For i As Integer = 0 To URLSplit.Count - 1
                            If ListBoxList.Contains(URLSplit(i)) = False Then
                                ListBoxList.Add(URLSplit(i))
                            End If
                        Next
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                        'End If
                        strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                        SendHTMLResponse(stream, strRequest)
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        'strRequest = rootPath & "error_Page.html" 'PostPage
                        'SendHTMLResponse(stream, strRequest)
                        SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

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
                            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                            '    For i As Integer = 0 To URLSplit2.Count - 1
                            '        Dim ii As Integer = i
                            '        Me.Invoke(New Action(Function() As Object
                            '                                 If Anime_Add.ListBox1.Items.Contains(URLSplit2(ii)) = False Then
                            '                                     Anime_Add.ListBox1.Items.Add(URLSplit2(ii))
                            '                                 End If
                            '                                 'Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                            '                                 Return Nothing
                            '                             End Function))
                            '    Next
                            'Else
                            For i As Integer = 0 To URLSplit2.Count - 1
                                If ListBoxList.Contains(URLSplit2(i)) = False Then
                                    ListBoxList.Add(URLSplit2(i))
                                End If
                            Next
                            Me.Invoke(New Action(Function() As Object
                                                     Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
                                                     Me.Invalidate()
                                                     Return Nothing
                                                 End Function))
                            'End If
                            strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                            SendHTMLResponse(stream, strRequest)
                        End If
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        'strRequest = rootPath & "error_Page.html" 'PostPage
                        'SendHTMLResponse(stream, strRequest)
                        SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

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
                            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                            '    Me.Invoke(New Action(Function() As Object
                            '                             If Anime_Add.ListBox1.Items.Contains(WebbrowserURL) = False Then
                            '                                 Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                            '                             End If
                            '                             Return Nothing
                            '                         End Function))
                            'Else
                            If ListBoxList.Contains(WebbrowserURL) = False Then
                                ListBoxList.Add(WebbrowserURL)
                            End If
                            Me.Invoke(New Action(Function() As Object
                                                     Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue"
                                                     Me.Invalidate()
                                                     Return Nothing
                                                 End Function))
                            'End If
                        End If
                        strRequest = rootPath & "Post_Single_Sucess.html" 'PostPage
                        SendHTMLResponse(stream, strRequest)
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        'strRequest = rootPath & "error_Page.html" 'PostPage
                        'SendHTMLResponse(stream, strRequest)
                        SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

                    End Try
#End Region
                Else
                    strRequest = rootPath & "error_Page_default.html" 'PostPage
                    SendHTMLResponse(stream, strRequest)
                End If
            ElseIf strArray(0).Trim().ToUpper.Equals("GET") Then
                strRequest = strArray(1).Trim

                If CBool(InStr(strRequest, "403")) Then
                    strRequest = strRequest & defaultPage '"HTMLString" 'strRequest & defaultPage
                    SendHTMLResponse(stream, "index.html", Nothing, "HTTP/1.0 403 Forbidden")
                End If

                If strRequest.StartsWith("/") Then
                    strRequest = strRequest.Substring(1)
                End If
                If strRequest.EndsWith("/") Or strRequest.Equals("") Then
                    'Debug.WriteLine(Date.Now + " " + "it's index.html")
                    strRequest = strRequest & defaultPage '"HTMLString" 'strRequest & defaultPage
                End If

                strRequest = rootPath & strRequest
                SendHTMLResponse(stream, strRequest)
            Else ' Not HTTP GET method

                strRequest = rootPath & defaultPage
                SendHTMLResponse(stream, strRequest)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
            ' My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
            SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

        End Try
    End Sub

    ' Send HTTP Response
    Private Sub SendHTMLResponse(ByVal stream As NetworkStream, Optional ByVal httpRequest As String = Nothing, Optional ByVal Response As ServerResponse = Nothing, Optional ByVal httpCode As String = "HTTP/1.0 200 OK")
        Try
            Dim respByte() As Byte
            If httpRequest = Nothing Then
                Debug.WriteLine(httpRequest)
                respByte = System.Text.Encoding.UTF8.GetBytes(Response.Content) 'File.ReadAllBytes("") '
                ' Set HTML Header
                Dim htmlHeader As String =
                   httpCode & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                   "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & GetContentType(Response.Type) &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                stream.Write(headerByte, 0, headerByte.Length)
                stream.Write(respByte, 0, respByte.Length)

            ElseIf CBool(InStr(httpRequest, "index.html")) Then
                Debug.WriteLine(httpRequest)
                respByte = System.Text.Encoding.UTF8.GetBytes(HTML) 'File.ReadAllBytes("") '
                ' Set HTML Header
                Dim htmlHeader As String =
                    httpCode & ControlChars.CrLf &
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
                    httpCode & ControlChars.CrLf &
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
        ElseIf (httpRequest.EndsWith("css")) Then
            Return "text/css"
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

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

        'If Application.OpenForms().OfType(Of Anime_Add).Any = False Then
        '    If ListBoxList.Count > 0 Then
        '        If CBool(InStr(Me.Text, "Crunchyroll Downloader")) Or CBool(InStr(Me.Text, " downloads in queue")) Then
        '            Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Btn_add.Image = My.Resources.main_add
        Panel1.Select()



    End Sub
    Private Async Sub Funimation_Token_Click(sender As Object, e As EventArgs) Handles Funimation_Token.Click
        Dim Token As String = Nothing
        Try
            Dim DeviceRegion As String = Nothing

            'Browser.GetCookies()

            Dim list As List(Of CoreWebView2Cookie) = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com/")
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
        Catch ex As Exception

        End Try
        ' region=US;
        If Token = Nothing Then
            MsgBox("No Token has been found...", MsgBoxStyle.Exclamation)
        Else
            FunimationToken = Token
            MsgBox("Token found!" + vbNewLine + Token, MsgBoxStyle.Information)
        End If
    End Sub


    Private Sub CheckCRBetaTokenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckCRBetaTokenToolStripMenuItem.Click
        If CrBetaBasic = Nothing Then
            If CBool(MessageBox.Show("No CR Beta Basic Token has been found..." + vbNewLine + "Press 'Yes' to manuel edit the Token", "Token", MessageBoxButtons.YesNo) = DialogResult.Yes) Then
                CrBetaBasic = InputBox("Please enter a valid Token", "Token")
            End If

        Else
            MsgBox("CR Beta Basic Token found!" + vbNewLine + CrBetaBasic, MsgBoxStyle.Information)
            ' CrBetaBasic = Nothing


        End If
    End Sub

    Private Sub AddonHTMLToolStripMenuItem_Click(sender As Object, e As EventArgs)
        My.Computer.Clipboard.SetText(HTML)
    End Sub

    Private Sub Timer3OffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Timer3OffToolStripMenuItem.Click
        Timer3.Enabled = False
    End Sub

    Private Sub ThreadCount_Click(sender As Object, e As EventArgs) Handles ThreadCount.Click
        Trackbar.ShowDialog()
    End Sub

    Private Sub MsgBoxToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MsgBox(LoadedUrls.Count.ToString)
        For i As Integer = 0 To LoadedUrls.Count - 1
            MsgBox(LoadedUrls(i))
        Next
    End Sub

    Private Sub CRCookieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CRCookieToolStripMenuItem.Click

        'MsgBox(Curl(InputBox("test", "test")))
        'For i As Integer = 0 To CookieList.Count - 1


        'Next
        MsgBox(CookieList.Count.ToString)
        'MsgBox(CR_Cookies)
    End Sub




    Private Sub ItemBoundsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try

            For s As Integer = 0 To Panel1.Controls.Count - 1
                MsgBox(Panel1.Controls.Item(s).Bounds.ToString)
            Next
        Catch ex As Exception
        End Try
    End Sub




    Private Sub PanelControlRemoved(sender As Object, e As ControlEventArgs) Handles Panel1.ControlAdded, Panel1.ControlRemoved

        ItemBounds()
    End Sub

    'Private Sub PanelScroll(sender As Object, e As ScrollEventArgs) Handles Panel1.Scroll
    '    'MsgBox("Scroll")
    '    'ItemBounds()
    'End Sub

    Sub ItemBounds()
        Try
            Panel1.AutoScrollPosition = New Point(0, 0)
            Dim W As Integer = Panel1.Width
            If Panel1.Controls.Count * 142 > Panel1.Height Then
                W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
            End If

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For s As Integer = 0 To Item.Count - 1
                Item(s).SetBounds(0, 142 * s, W - 2, 142)
                If Debug2 = True Then
                    Debug.WriteLine("Ist: " + Item(s).Location.Y.ToString)
                    Debug.WriteLine("Soll: " + (142 * s).ToString)
                End If
            Next


        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub DummyItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DummyItemToolStripMenuItem.Click
        Dim TN As String = "https://invalid.com/"
        Dim cmd As String = "-i " + Chr(34) + "https://invalid.com/" + Chr(34) + " -c copy "
        ListItemAdd("TestDL", "CR", "TestDL", "9987p", "DE", "None", TN, cmd, "E:\Test\RWBY\Testdl.mkv")


    End Sub

#End Region

#Region "enum"

    Sub FillArray() '

        LangValueEnum.Add(New NameValuePair("[ null ]", "", "null", Nothing))

        LangValueEnum.Add(New NameValuePair("Deutsch", "ger", "de-DE", Nothing)) '

        LangValueEnum.Add(New NameValuePair("English", "eng", "en-US", "en"))

        LangValueEnum.Add(New NameValuePair("Português (Brasil)", "por", "pt-BR", "pt"))

        LangValueEnum.Add(New NameValuePair("Português (Portugal)", "por", "pt-PT", Nothing))

        LangValueEnum.Add(New NameValuePair("Español (LA)", "spa", "es-419", "es"))

        LangValueEnum.Add(New NameValuePair("Français (France)", "fre", "fr-FR", Nothing))

        LangValueEnum.Add(New NameValuePair("العربية (Arabic)", "ara", "ar-SA", Nothing))

        LangValueEnum.Add(New NameValuePair("Русский (Russian)", "rus", "ru-RU", Nothing))

        LangValueEnum.Add(New NameValuePair("Italiano (Italian)", "ita", "it-IT", Nothing))

        LangValueEnum.Add(New NameValuePair("Español (España)", "spa", "es-ES", Nothing))

        LangValueEnum.Add(New NameValuePair("Bahasa Indonesia", "ind", "id-ID", Nothing))

        LangValueEnum.Add(New NameValuePair("Català", "cat", "ca-ES", Nothing))

        LangValueEnum.Add(New NameValuePair("Polski", "pol", "pl-PL", Nothing))

        LangValueEnum.Add(New NameValuePair("Tiếng Việt", "vie", "vi-VN", Nothing))

        LangValueEnum.Add(New NameValuePair("తెలుగు", "tel", "te-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("Türkçe", "tur", "tr-TR", Nothing))

        LangValueEnum.Add(New NameValuePair("हिंदी", "hin", "hi-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("தமிழ்", "tam", "ta-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("中文 (中国)", "zho", "zh-CN", Nothing))

        LangValueEnum.Add(New NameValuePair("中文 (台灣)", "zho", "zh-TW", Nothing))

        LangValueEnum.Add(New NameValuePair("한국어", "kor", "ko-KR", Nothing))

        LangValueEnum.Add(New NameValuePair("ไทย", "tha", "th-TH", Nothing))

        LangValueEnum.Add(New NameValuePair("Japanese", "jpn", "ja-JP", Nothing))

    End Sub



    Private Sub QueueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueueToolStripMenuItem.Click
        'ffmpeg_options.ShowDialog()
        Dim newCmd As New ffmpeg_options
        newCmd.command = ffmpeg_command
        'MsgBox(newCmd.ShowDialog.ToString)
        If newCmd.ShowDialog = DialogResult.OK Then
            ffmpeg_command = newCmd.command
            My.Settings.ffmpeg_command_override = newCmd.command
        End If
    End Sub

    Private Sub Btn_Queue_Click(sender As Object, e As EventArgs) Handles Btn_Queue.Click
        If File.Exists("cookies.txt") = False Then
            If Application.OpenForms().OfType(Of Browser).Any = True Then
            Else
                UserBowser = False
                Browser.Show()
            End If
        End If

        If Queue.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            Queue.WindowState = System.Windows.Forms.FormWindowState.Normal
        Else
            Queue.Show()
        End If

    End Sub



#End Region

#Region "Process Urls"


    Public Sub LoadBrowser(ByVal Url As String, Optional ByVal RT_count As Integer = 0)


        LoadingUrl = Url
        LoadedUrls.Clear()
        Dim NoBrowser As Boolean = False

        If My.Settings.SaveMode = True Then
            Browser.WebView2.CoreWebView2.Navigate(Url)
            Exit Sub
        End If

        'CR_v1Token = "Get"
        'Browser.WebView2.Source = New Uri(Url)
        'Exit Sub
        'MsgBox(Url)

        If CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "series")) = True Or CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "watch")) = True Then



#Region "Get Cookies"

            CR_Cookies = "Cookie: "
            'MsgBox("Cookies")
            If File.Exists("cookies.txt") = True Then
                CR_Cookies = GetCookiesFromFile("crunchyroll.com")
                NoBrowser = True
                CrBetaBasic = "Basic bm9haWhkZXZtXzZpeWcwYThsMHE6"
                'MsgBox(True.ToString)
            Else
                Browser.GetCookies(Url)

                Debug.WriteLine(CookieList.Count.ToString)
                If CookieList.Count = 0 Then
                    Browser.WebView2.CoreWebView2.Navigate(Url)
                    SetStatusLabel("Status: loading in browser...")
                    Me.Text = "Status: loading in browser..."
                    Exit Sub
                End If



                For i As Integer = 0 To CookieList.Count - 1

                    If CBool(InStr(CookieList.Item(i).Domain, ".crunchyroll.com")) And CBool(InStr(CookieList.Item(i).Name, "_evidon_suppress")) = False Then
                        CR_Cookies = CR_Cookies + CookieList.Item(i).Name + "=" + CookieList.Item(i).Value + ";"
                    End If

                Next

            End If

            'MsgBox(Main.CR_Cookies)

            Dim DeviceRegion As String = Nothing

            If CBool(InStr(Url, "/series")) Then
                Dim locale1() As String = Url.Split(New String() {"crunchyroll.com/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim locale2() As String = locale1(1).Split(New String() {"/series"}, System.StringSplitOptions.RemoveEmptyEntries)
                locale = Convert_locale(locale2(0))
                If locale = "en-US" Then
                    Url_locale = ""
                Else
                    Url_locale = locale2(0)
                End If


            ElseIf CBool(InStr(Url, "/watch")) Then
                Dim locale1() As String = Url.Split(New String() {"crunchyroll.com/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim locale2() As String = locale1(1).Split(New String() {"/watch"}, System.StringSplitOptions.RemoveEmptyEntries)
                'MsgBox(locale2(0))

                locale = Convert_locale(locale2(0))
                'End If
                If locale = "en-US" Then
                    Url_locale = ""
                Else
                    Url_locale = locale2(0)
                End If

                'If CBool(InStr(Url, "musicvideo/")) Then
                '    SetStatusLabel("Status: musicvideo detected - partial support only")

                '    Browser.WebView2.CoreWebView2.Navigate(Url)
                '    Exit Sub
                'Else
                'If CBool(InStr(Url, "/concert/")) Then

                '        SetStatusLabel("Status: concert detected - partial support only")
                '        Browser.WebView2.CoreWebView2.Navigate(Url)
                '        Exit Sub

                '    End If


            End If

            'Debug.WriteLine("###" + CR_Cookies + "###")

            Dim Loc_CR_Cookies = " -H " + Chr(34) + CR_Cookies + Chr(34)


            'CR_v1Token = "Get"
            'Browser.WebView2.Source = New Uri(Url)
            'Exit Sub

#End Region
            Dim Auth As String = " -H " + Chr(34) + "Authorization: " + CrBetaBasic + Chr(34)
            Dim Post As String = " -d " + Chr(34) + "grant_type=etp_rt_cookie" + Chr(34) + " -X POST"

            Dim CRBetaBearer As String = "Bearer "


                Dim v1Token As String = CurlPost("https://www.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post, "add_main_4494")


                If CBool(InStr(v1Token, "curl:")) = True And CBool(InStr(v1Token, "400")) = True Then
                Debug.WriteLine("Post error!, 400")
                Debug.WriteLine(Post.Replace("etp_rt_cookie", "client_id"))
                v1Token = CurlPost("https://www.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post.Replace("etp_rt_cookie", "client_id"), "add_main-4499")

                End If



                'MsgBox(v1Token)

                If CBool(InStr(v1Token, "curl:")) = True And CBool(InStr(v1Token, "400")) = True Then
                SetStatusLabel("Status: Failed - bad request, check CR login")
                Me.Text = "Status: Failed - bad request, check CR login"
                Debug.WriteLine("Status: Failed - bad request, check CR login")

                b = True
                Exit Sub

            ElseIf CBool(InStr(v1Token, "curl:")) = True Then
                v1Token = CurlPost("https://www.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post, "add_main_4516")
            End If

            'MsgBox(v1Token)
            If CBool(InStr(v1Token, "curl: (60)")) = True Then
                SetStatusLabel("Status: Critical error. #4478")
                MsgBox("Please try the '--insecure' option found on the 'Main' page of the settings.")
                Exit Sub
                'ElseIf CBool(InStr(v1Token, "curl:")) Then

            ElseIf CBool(InStr(v1Token, "curl:")) = True Then
                Browser.WebView2.CoreWebView2.Navigate(Url)
                Exit Sub

            End If

            Dim Token() As String = v1Token.Split(New String() {Chr(34) + "access_token" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Token2() As String = Token(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            CRBetaBearer = CRBetaBearer + Token2(0)

            Dim Auth2 As String = " -H " + Chr(34) + "Authorization: " + CRBetaBearer + Chr(34)

            ProcessLoading(Url, Auth2, Loc_CR_Cookies, RT_count)

        Else
            'to do
        End If
    End Sub

    Public Sub ProcessLoading(ByVal url As String, Auth2 As String, ByVal Loc_CR_Cookies As String, ByVal RT_Count As Integer)

        ' MsgBox(url)
        If CBool(InStr(url, "crunchyroll.com")) = True And CBool(InStr(url, "series/")) = True Then

            Dim SeriesUrlBuilder() As String = url.Split(New String() {"series/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SeriesUrlBuilder2() As String = SeriesUrlBuilder(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)


            Dim SeriesUrl As String = "https://www.crunchyroll.com/content/v2/cms/series/" + SeriesUrlBuilder2(0) + "/seasons?preferred_audio_language=" + DubSprache.CR_Value + "&locale=" + locale '+ "&Signature=" + signature2(0) + "&Policy=" + policy2(0) + "&Key-Pair-Id=" + key_pair_id2(0)
            Debug.WriteLine(SeriesUrl)
            'MsgBox(SeriesUrl)
            GetBetaSeasons(url, SeriesUrl, Auth2)


        ElseIf CBool(InStr(url, "crunchyroll.com")) = True And CBool(InStr(url, "watch/")) = True And CBool(CrBetaBasic = Nothing) = False And CBool(InStr(url, "/musicvideo/")) = False And CBool(InStr(url, "/concert/")) = False Then
#Region "Anime"


            Dim ObjectsUrl As String = Nothing


            'MsgBox(Url)
            Dim ObjectsURLBuilder3() As String = url.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)



            ObjectsUrl = "https://www.crunchyroll.com/content/v2/cms/objects/" + ObjectsURLBuilder4(0) + "?preferred_audio_language=" + DubSprache.CR_Value + "&locale=" + locale
            'End Using
            'MsgBox(ObjectsUrl)

            Debug.WriteLine("ObjectsUrl: " + ObjectsUrl)


            Dim StreamsUrl As String = Nothing
            Dim ObjectJson As String



            Try

                ObjectJson = CurlAuthNew(ObjectsUrl, Loc_CR_Cookies, Auth2)

            Catch ex As Exception

                If CBool(InStr(ex.ToString, "Error - Getting")) Then
                    MsgBox("Error invalid CR respone")
                    Exit Sub
                Else
                    MsgBox("Error processing data")
                    Exit Sub
                End If
            End Try


            If CBool(InStr(ObjectJson, "curl:")) = True Then
                Browser.WebView2.CoreWebView2.Navigate(url)

                Exit Sub
            ElseIf CBool(InStr(ObjectJson, "videos/")) = False Then

                SetStatusLabel("Status: Failed - no video, check CR login")
                Me.Text = "Status: Failed - no video, check CR login"
                Debug.WriteLine("Status: Failed - no video, check CR login")

                Exit Sub
            End If

            Try
                Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"videos/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim StreamsUrlBuilder3() As String = ObjectsUrl.Split(New String() {"objects/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim StreamsUrlBuilder4() As String = StreamsUrlBuilder3(1).Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)

                StreamsUrl = StreamsUrlBuilder3(0) + "videos/" + StreamsUrlBuilder2(0) + "/streams?" + StreamsUrlBuilder4(1)

                'MsgBox(StreamsUrl)

                ' Debug.WriteLine(StreamsUrl)
            Catch ex As Exception
                Browser.WebView2.CoreWebView2.Navigate(url)
                Exit Sub
            End Try

            GetCRVideoProxy(StreamsUrl, Auth2, url, RT_Count)
#End Region
        ElseIf CBool(InStr(url, "crunchyroll.com")) = True And CBool(InStr(url, "musicvideo/")) = True And CBool(CrBetaBasic = Nothing) = False Then
#Region "musik videos"


            Dim ObjectsUrl As String = Nothing

            Dim ObjectsURLBuilder3() As String = url.Split(New String() {"musicvideo/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)



            ObjectsUrl = "https://www.crunchyroll.com/content/v2/music/music_videos/" + ObjectsURLBuilder4(0) + "?locale=" + locale

            Debug.WriteLine("ObjectsUrl: " + ObjectsUrl)


            Dim StreamsUrl As String = Nothing
            Dim ObjectJson As String

            Try

                ObjectJson = CurlAuthNew(ObjectsUrl, Loc_CR_Cookies, Auth2)

            Catch ex As Exception

                If CBool(InStr(ex.ToString, "Error - Getting")) Then
                    MsgBox("Error invalid CR respone")
                    Exit Sub
                Else
                    MsgBox("Error processing data")
                    Exit Sub
                End If
            End Try


            If CBool(InStr(ObjectJson, "/content/v2/music/")) = False Then

                SetStatusLabel("Status: Failed - no video, check CR login")
                Me.Text = "Status: Failed - no video, check CR login"
                Debug.WriteLine("Status: Failed - no video, check CR login")

                Exit Sub
            End If

            'Try
            Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"/content/v2/music/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)


            StreamsUrl = "https://www.crunchyroll.com/content/v2/music/" + StreamsUrlBuilder2(0) + "/streams?locale=" + locale


            GetCRVideoProxy(StreamsUrl, Auth2, url, RT_Count)
#End Region
        ElseIf CBool(InStr(url, "crunchyroll.com")) = True And CBool(InStr(url, "concert/")) = True And CBool(CrBetaBasic = Nothing) = False Then

            Dim ObjectsUrl As String = Nothing

            Dim ObjectsURLBuilder3() As String = url.Split(New String() {"concert/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)



            ObjectsUrl = "https://www.crunchyroll.com/content/v2/music/concerts/" + ObjectsURLBuilder4(0) + "?locale=" + locale

            Debug.WriteLine("ObjectsUrl: " + ObjectsUrl)


            Dim StreamsUrl As String = Nothing
            Dim ObjectJson As String

            Try

                ObjectJson = CurlAuthNew(ObjectsUrl, Loc_CR_Cookies, Auth2)

            Catch ex As Exception

                If CBool(InStr(ex.ToString, "Error - Getting")) Then
                    MsgBox("Error invalid CR respone")
                    Exit Sub
                Else
                    MsgBox("Error processing data")
                    Exit Sub
                End If
            End Try


            If CBool(InStr(ObjectJson, "/content/v2/music/")) = False Then

                SetStatusLabel("Status: Failed - no video, check CR login")
                Me.Text = "Status: Failed - no video, check CR login"
                Debug.WriteLine("Status: Failed - no video, check CR login")

                Exit Sub
            End If

            'Try
            Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"/content/v2/music/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)


            StreamsUrl = "https://www.crunchyroll.com/content/v2/music/" + StreamsUrlBuilder2(0) + "/streams?locale=" + locale


            GetCRVideoProxy(StreamsUrl, Auth2, url, RT_Count)
        Else
            Browser.WebView2.CoreWebView2.Navigate(url)
        End If


    End Sub


    Public Function GetCookiesFromFile(ByVal Host As String) As String

        Dim Cookies As String = "Cookie: "
        Dim Cookie_txt As String = My.Computer.FileSystem.ReadAllText("cookies.txt")

        Dim LineChar As String = vbCrLf

        If CBool(InStr(Cookie_txt, vbCr)) Then
            LineChar = vbCr
            'Debug.WriteLine("vbCr")
        ElseIf CBool(InStr(Cookie_txt, vbLf)) Then
            LineChar = vbLf
            'Debug.WriteLine("vbLf")
        End If

        Dim Cookie_txt1() As String = Cookie_txt.Split(New String() {LineChar}, System.StringSplitOptions.RemoveEmptyEntries)

        Debug.WriteLine("got txt")

        For i As Integer = 0 To Cookie_txt1.Count - 1

            Dim Cookie_txt2() As String = Cookie_txt1(i).Split(New String() {Chr(9)}, System.StringSplitOptions.RemoveEmptyEntries)

            If CBool(InStr(Cookie_txt2(0), Host)) = True Then

                If CBool(InStr(Cookie_txt2(5), "_evidon_suppress")) = True Then
                    Continue For
                End If

                Cookies = Cookies + Cookie_txt2(5) + "=" + Cookie_txt2(6) + ";"



            End If

        Next


        'Debug.WriteLine(Cookies)

        Return Cookies
    End Function

    Sub SetStatusLabel(ByVal txt As String)
        If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
            Anime_Add.StatusLabel.Text = txt

        End If

    End Sub

    Private Sub SaveThumbnailAsImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveThumbnailAsImageToolStripMenuItem.Click
        If My.Settings.SaveThumbnail = False Then
            My.Settings.SaveThumbnail = True
            MsgBox("Thumbnails will be saved into the video folder")
            My.Settings.Save()

        Else
            My.Settings.SaveThumbnail = False
            MsgBox("Thumbnail saving disabled")
            My.Settings.Save()

        End If
    End Sub

    Private Sub SaveModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveModeToolStripMenuItem.Click
        If My.Settings.SaveMode = False Then
            My.Settings.SaveMode = True
            MsgBox("SaveMode enabled")
            My.Settings.Save()

        Else
            My.Settings.SaveMode = False
            MsgBox("SaveMode disabled")
            My.Settings.Save()

        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub AudioOnlyQualityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AudioOnlyQualityToolStripMenuItem.Click
        Me.Invoke(New Action(Function() As Object
                                 ' ResoNotFoundString = VideoJson
                                 DialogTaskString = "AudioOnlyResolution"
                                 ErrorDialog.ShowDialog()
                                 Return Nothing
                             End Function))
        If UserCloseDialog = True Then
            'Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
        Else

            MsgBox(ResoBackString)
            My.Settings.AudioOnlyReso = ResoBackString
            ResoBackString = Nothing

        End If
    End Sub


    Public Sub ProcessLocal(ByVal Input As String)

        Dim Pfad2 As String = Input.Replace(Path.GetExtension(Input), VideoFormat)


        If File.Exists(Pfad2) = True Then
            Pfad2 = Input.Replace(Path.GetExtension(Input), GeräteID().Replace("CRD-Temp-File-", "")) + VideoFormat
        End If

        Dim ffmpegInput As String = " -i " + Chr(34) + Input + Chr(34) + ffmpeg_command

        Me.Invoke(New Action(Function() As Object
                                 ListItemAdd(Path.GetFileName(Pfad2.Replace(Chr(34), "")), "Local", Path.GetFileName(Pfad2.Replace(Chr(34), "")), "Local", "unkown", "non", ffmpegInput, Chr(34) + Pfad2 + Chr(34))
                                 Return Nothing
                             End Function))

    End Sub


#End Region

End Class


