Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports System.Threading
Imports System.ComponentModel
Imports System.Net.WebUtility
Imports System.Net.Sockets
Public Class Main
    Dim liList As New List(Of String)
    Public HTMLString As String = My.Resources.Startuphtml
    Public RunServer As Boolean = True
    Public ListBoxList As New List(Of String)
    Dim ItemList As New List(Of CRD_List_Item)
    Public RunningDownloads As Integer = 0
    Public UseQueue As Boolean = False
    Public StartServer As Boolean = False
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
    Public MergeSubstoMP4 As Boolean = False
    Public LoginDialog As Boolean = False
    Public SaveLog As Boolean = False
    Public NonCR_Timeout As Integer = 5
    Public NonCR_URL As String = Nothing
    Public DlSoftSubsRDY As Boolean = True
    Public DialogTaskString As String
    Public UserCloseDialog As Boolean = False
    Dim Aktuell As String
    Dim Gesamt As String
    Public LabelUpdate As String = "Status: idle"
    Public LabelEpisode As String = "..."
    Public b As Boolean = True
    Public c As Boolean = True
    Public d As Boolean = True
    Public LoginOnly As String = "False"
    Public Pfad As String = My.Computer.FileSystem.CurrentDirectory
    Public ffmpeg_command As String = " -c copy -bsf:a aac_adtstoasc" '" -c:v hevc_nvenc -preset fast -b:v 6M -bsf:a aac_adtstoasc " 
    Public Resu As Integer
    Dim Resu2 As String
    Public ResuSave As String = "6666x6666"
    Public SubSprache As String
    Public SubFolder As Integer
    Public SoftSubs As New List(Of String)
    Public AbourtList As New List(Of String)
    Public watingList As New List(Of String)
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
    Public MaxDL As Integer
    Public ResoNotFoundString As String
    Public ResoBackString As String
    Public WebbrowserHeadText As String = Nothing
    Public WebbrowserSoftSubURL As String = Nothing
    Public WebbrowserURL As String = Nothing
    Public WebbrowserText As String = Nothing
    Public WebbrowserTitle As String = Nothing
    Public WebbrowserCookie As String = Nothing
    Public UserBowser As Boolean = False

#Region "Sprachen Vairablen"
    Public URL_Invaild As String = "invalid URL, this Downloader is only for crunchyroll.com"
    Public SubFolder_automatic As String = "[automatic : Series/Season]"
    Public SubFolder_Nothing As String = "[ ignore subfolder ]"

    Dim DL_Path_String As String = "Please choose download directory."
    Public CR_Premium_Failed As String = "Can not verify the active premium membership."
    Public No_Stream As String = "Please make sure that the URL is correct or check if the Anime is available in your country."
    Dim TaskNotCompleed As String = "Please wait until the current task is completed."
    Dim Premium_Stream As String = "Please make sure that you logged in for this premium episode."
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
    Public LabelLangNotFoundText As String = "language not found" + vbNewLine + "Select another one below"
    Public ButtonResoNotFoundText As String = "Submit"
    Public CB_SuB_Nothing As String = "[ null ]"
    Dim StatusToolTip As ToolTip = New ToolTip()
    Dim StatusToolTipText As String
    Public RunGecko As String = "Startup"
#End Region

#Region "UI"

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles pictureBox1.MouseEnter
        pictureBox1.BackgroundImage = My.Resources.main_browser
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave
        pictureBox1.BackgroundImage = My.Resources.main_browser_hover
    End Sub


    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles pictureBox2.MouseEnter
        pictureBox2.BackgroundImage = My.Resources.main_settings
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox2.MouseLeave
        pictureBox2.BackgroundImage = My.Resources.main_settings_hover
    End Sub



    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter
        pictureBox3.BackColor = SystemColors.Control
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave
        pictureBox3.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles pictureBox4.MouseMove
        pictureBox4.BackColor = SystemColors.Control
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        pictureBox4.BackColor = Color.Transparent
    End Sub




#End Region
    Public Declare Function waveOutSetVolume Lib "winmm.dll" (ByVal uDeviceID As Integer, ByVal dwVolume As Integer) As Integer


    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            StartServer = CBool(Integer.Parse(rkg.GetValue("StartServer").ToString))
        Catch ex As Exception

        End Try
        If StartServer = True Then
            Dim t As New Thread(AddressOf ServerStart)
            t.Priority = ThreadPriority.Normal
            t.IsBackground = True
            t.Start()
        End If


        waveOutSetVolume(0, 0)
        Try
            Dim FileLocation As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
            For Each File In FileLocation.GetFiles()
                If InStr(File.FullName, "gecko-network.txt") Then
                    My.Computer.FileSystem.DeleteFile(Path.Combine(Application.StartupPath, File.FullName))
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Me.Icon = My.Resources.icon

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Pfad = rkg.GetValue("Ordner").ToString
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
        Catch ex As Exception

        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            ffmpeg_command = rkg.GetValue("ffmpeg_command").ToString
        Catch ex As Exception
            ffmpeg_command = " -c copy -bsf:a aac_adtstoasc "
        End Try

        If ffmpeg_command = " -c:v hevc_nvenc -preset fast -b:v 6M -bsf:a aac_adtstoasc " Then
            MaxDL = 2
        ElseIf ffmpeg_command = " -c:v libx265 -preset fast -b:v 6M -bsf:a aac_adtstoasc " Then
            MaxDL = 1
        End If
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Resu = Integer.Parse(rkg.GetValue("Resu").ToString)
            'MsgBox(Resu)
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubSprache = rkg.GetValue("Sub").ToString
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubFolder = Integer.Parse(rkg.GetValue("SubFolder").ToString)
        Catch ex As Exception
            SubFolder = 1
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            MaxDL = Integer.Parse(rkg.GetValue("SL_DL").ToString)


        Catch ex As Exception
            MaxDL = 1
        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            MergeSubstoMP4 = CBool(Integer.Parse(rkg.GetValue("MergeMP4").ToString))
        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SaveLog = CBool(Integer.Parse(rkg.GetValue("SaveLog").ToString))
        Catch ex As Exception

        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SaveLog = CBool(Integer.Parse(rkg.GetValue("SaveLog").ToString))
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

        If Resu = Nothing Then
            Resu = 1080
        End If

        If SubSprache = Nothing Then
            SubSprache = "enUS"
        End If

    End Sub

    Public Sub ListItemAdd(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal SoftSubs As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String) ', ByVal AudioLang As String)
        Dim Thumbnail As Image = My.Resources.main_del
        Try
            Dim wc As New WebClient()
            Dim bytes As Byte() = wc.DownloadData(ThumbnialURL)
            Dim ms As New MemoryStream(bytes)
            Thumbnail = System.Drawing.Image.FromStream(ms)
        Catch ex As Exception
            'MsgBox(ex.ToString)
            'MsgBox(ThumbnialURL)
        End Try

        With ListView1.Items.Add(0)
            ItemConstructor(NameP1, NameP2, Reso, HardSub, SoftSubs, Thumbnail, URL_DL, Pfad_DL)
        End With

    End Sub
    Public Sub ItemConstructor(ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal SoftSubs As String, ByVal Thumbnail As Image, ByVal URL_DL As String, ByVal Pfad_DL As String)
        Dim Item As New CRD_List_Item

        Item.Visible = False
        Item.Parent = ListView1
        Item.Width = 838
        Item.Height = 142
#Region "Set Variables"
        'Item.SetUsedMap(UsedMap)
        Item.Setffmpeg_command(ffmpeg_command)
        Item.SetMergeSubstoMP4(MergeSubstoMP4)
        Item.SetDebug2(Debug2)
        Item.SetSaveLog(SaveLog)
#End Region

        Dim r As Rectangle
        Dim c As Integer = ListView1.Items.Count - 1

        r = ListView1.Items(c).Bounds()
        r.Width = 838
        r.Height = 142
        Item.SetLabelWebsite(NameP1)
        Item.SetLabelAnimeTitel(NameP2)
        Item.SetLabelResolution(Reso)
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
        Item.DownloadFFMPEG(URL_DL, Pfad_DL, Pfad_DL)
    End Sub
#Region "Manga DL"
    Public Sub MangaListItemAdd(ByVal NameP2 As String, ByVal ThumbnialURL As String, ByVal BaseURL As String, ByVal SiteList As List(Of String))

        Dim Thumbnail As Image = My.Resources.main_del
        Try
            Dim wc As New WebClient()
            wc.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/79.0")
            Dim bytes As Byte() = wc.DownloadData(ThumbnialURL)
            Dim ms As New MemoryStream(bytes)
            Thumbnail = System.Drawing.Image.FromStream(ms)
        Catch ex As Exception
            'MsgBox(ex.ToString)
            'MsgBox(ThumbnialURL)
        End Try

        With ListView1.Items.Add(0)
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
    Public Sub Pause(ByVal pau As Single)

        'Programmausführung verzögern *******************************************************

        Dim start, finish As Single
        start = Microsoft.VisualBasic.DateAndTime.Timer

        finish = start + pau
        Do While Microsoft.VisualBasic.DateAndTime.Timer < finish
            Application.DoEvents()
        Loop

    End Sub

#Region "Season DL"

    Public Sub MassGrapp()
        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
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
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = True
        Anime_Add.comboBox4.Enabled = True
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
                If InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + Anime_Add.ComboBox1.SelectedItem.ToString + "</a>") Then
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
                        If RunningDownloads < MaxDL Then
                            Exit For
                        Else
                            'MsgBox(e)
                            Await Task.Delay(2000)
                        End If
                    Else
                        Await Task.Delay(2000)
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
                    GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com" + URLGrapp2(0))
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
            Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
    End Sub
#End Region

#Region "SubsOnly"

    Public Sub MassGrappSubs()
        einstellungen.MultiDLSoftSubs.Enabled = True
        einstellungen.PictureBox3.Image = My.Resources.softsubs_download
        einstellungen.ComboBox1.Items.Clear()
        einstellungen.comboBox3.Items.Clear()
        einstellungen.comboBox4.Items.Clear()
        einstellungen.ComboBox2.Enabled = False
        Dim Anzahl As String() = WebbrowserText.Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Titel As String() = Anzahl(0).Split(New String() {"<meta content=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Titel2 As String() = Titel(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim c As Integer = Anzahl.Count - 1
        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim URLGrapp As String() = Anzahl(i).Split(New String() {"title=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            einstellungen.comboBox3.Items.Add(URLGrapp2(0))
            einstellungen.comboBox4.Items.Add(URLGrapp2(0))
        Next

    End Sub

    Public Sub SeasonDropdownGrappSubs()
        einstellungen.MultiDLSoftSubs.Enabled = True
        einstellungen.PictureBox3.Image = My.Resources.softsubs_download
        einstellungen.ComboBox1.Items.Clear()
        einstellungen.comboBox3.Items.Clear()
        einstellungen.comboBox4.Items.Clear()
        einstellungen.ComboBox2.Enabled = True
        einstellungen.comboBox3.Enabled = True
        einstellungen.comboBox4.Enabled = True
        Dim Anzahl As String() = WebbrowserText.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim Titel As String() = Anzahl(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Titel2 As String() = Titel(0).Split(New String() {">"}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(Titel2(0))
            einstellungen.ComboBox2.Items.Add(Titel2(1))
        Next

    End Sub

    Public Sub DownloadSubsOnly()
        'Try
        'Throw New System.Exception("Test")
        DlSoftSubsRDY = False
        Dim CR_Anime_Titel As String = Nothing
        Dim CR_Anime_Staffel As String = Nothing
        Dim CR_Anime_Folge As String = Nothing
#Region "Name + Pfad"
        Dim Pfad2 As String
        Dim CR_FilenName As String = Nothing
        Dim SubfolderValue As String = Nothing
        Dim CR_FilenName_Backup As String = Nothing


#Region "Name von Crunchyroll"

        'Dim Bug_Deutsch As String = "-"
        'If CBool(InStr(WebbrowserTitle, "Anschauen auf Crunchyroll")) Then
        '    Bug_Deutsch = ":"
        'End If
        'Dim CR_Name_by_Titel_2 As String() = WebbrowserTitle.Split(New String() {Bug_Deutsch}, System.StringSplitOptions.RemoveEmptyEntries)
        'CR_FilenName = CR_Name_by_Titel_2(0).Trim()

        Dim Bug_Deutsch As String = "-"
        If CBool(InStr(WebbrowserTitle, "Anschauen auf Crunchyroll")) Then
            Bug_Deutsch = ":"
        End If
        Dim CR_Name_by_Titel_2 As String() = WebbrowserTitle.Split(New String() {Bug_Deutsch}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim CR_Title As String = Nothing
        If CR_Name_by_Titel_2.Count > 2 Then
            For i As Integer = 0 To CR_Name_by_Titel_2.Count - 2
                If CR_Title = Nothing Then
                    CR_Title = CR_Name_by_Titel_2(i).Trim()
                Else
                    CR_Title = CR_Title + " " + CR_Name_by_Titel_2(i).Trim()
                End If

            Next
        End If
        CR_FilenName = CR_Title
        CR_FilenName_Backup = CR_Title
        'MsgBox(CR_FilenName)
        If CBool(InStr(WebbrowserText, "<h4>")) Then ' false on movie true on series
            Dim CR_Name_1 As String() = WebbrowserText.Split(New String() {"<h4>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim CR_Name_2 As String() = CR_Name_1(1).Split(New String() {"</h4>"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim CR_Name_Staffel0_Folge1 As String()
            If CBool(InStr(CR_Name_2(0), ",")) Then
                CR_Name_Staffel0_Folge1 = CR_Name_2(0).Split(New [Char]() {System.Convert.ToChar(",")}, System.StringSplitOptions.RemoveEmptyEntries)
                CR_Anime_Staffel = CR_Name_Staffel0_Folge1(0).Trim()
                CR_Anime_Folge = CR_Name_Staffel0_Folge1(1).Trim()
                CR_Anime_Folge = System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
            Else
                CR_Anime_Staffel = Nothing
                CR_Anime_Folge = CR_Name_2(0).Trim()
                CR_Anime_Folge = System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
            End If


            Dim CR_Name_4 As String() = CR_Name_1(0).Split(New String() {"class=" + Chr(34) + "text-link" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim CR_Name_Anime0 As String() = CR_Name_4(CR_Name_4.Length - 1).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
            CR_Name_Anime0(0) = System.Text.RegularExpressions.Regex.Replace(CR_Name_Anime0(0), "[^\w\\-]", " ")
            CR_Anime_Titel = CR_Name_Anime0(0).Trim
            If CR_Anime_Staffel = Nothing Then
                CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Folge
            Else
                CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Staffel + " " + CR_Anime_Folge
            End If

            CR_FilenName_Backup = RemoveExtraSpaces(CR_FilenName)


        End If
#End Region
        CR_FilenName = System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
        CR_FilenName = RemoveExtraSpaces(CR_FilenName)

        If SubfolderValue = Nothing Then
            Pfad2 = Pfad + "\" + CR_FilenName + ".mp4"
        Else
            Pfad2 = Pfad + "\" + SubfolderValue + CR_FilenName + ".mp4"
        End If
        If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
            ' Nein! Jetzt erstellen...
            Try
                Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
            Catch ex As Exception
                ' Ordner wurde nich erstellt
                Pfad2 = Pfad + "\" + CR_FilenName_Backup + ".mp4"
            End Try
        End If
        Pfad2 = Chr(34) + Pfad2 + Chr(34)

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
                Me.Invoke(New Action(Function()
                                         ResoNotFoundString = WebbrowserText
                                         DialogTaskString = "Language"
                                         Reso.ShowDialog()
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
                Me.Invoke(New Action(Function()
                                         ResoNotFoundString = WebbrowserText
                                         DialogTaskString = "Language"
                                         Reso.ShowDialog()
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


#Region "Download softsub file"

        If SoftSubs2.Count > 0 Then
            For i As Integer = 0 To SoftSubs2.Count - 1
                LabelUpdate = "Status: downloading subtitle file"
                LabelEpisode = SoftSubs2(i)
                Dim SoftSub As String() = WebbrowserText.Split(New String() {Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs2(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                Dim SoftSub_3 As String = SoftSub_2(0).Replace("\/", "/")
                Dim client0 As New WebClient
                client0.Encoding = Encoding.UTF8
                Dim str0 As String = client0.DownloadString(SoftSub_3)
                Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSubs2(i) + Path.GetExtension(Pfad3)), "ass")
                'MsgBox(FN)
                If i = 0 Then
                    FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                    'MsgBox(FN)
                End If
                Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                'MsgBox(Pfad4)
                File.WriteAllText(Pfad4, str0, Encoding.UTF8)
                Pause(1)
            Next


        End If
#End Region

        DlSoftSubsRDY = True

        'Catch ex As Exception
        'End Try
    End Sub

    Public Async Sub MassSubsDL()
        If einstellungen.comboBox3.Text = Nothing Then
            Exit Sub
        ElseIf einstellungen.comboBox4.Text = Nothing Then
            Exit Sub
        End If
        einstellungen.SoftSubsMass.Text = "preparing ..."
        Dim Website As String = WebbrowserText

        If einstellungen.ComboBox2.Enabled = True Then
            Dim SeasonDropdownAnzahl As String() = Website.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(SeasonDropdownAnzahl)
            Dim SDV As Integer = 0
            For i As Integer = 0 To SeasonDropdownAnzahl.Count - 1
                If InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + einstellungen.ComboBox2.SelectedItem.ToString + "</a>") Then
                    SDV = i
                End If
            Next
            Website = SeasonDropdownAnzahl(SDV)
        End If
        Try
            Dim Anzahl As String() = Website.Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(Anzahl)
            Dim c As Integer = einstellungen.comboBox4.SelectedIndex - einstellungen.comboBox3.SelectedIndex + 1
            'AnzahlGesamt.Text = c.ToString
            Gesamt = c.ToString
            Aktuell = "0"
            If einstellungen.comboBox4.SelectedIndex > einstellungen.comboBox3.SelectedIndex Then

                For i As Integer = einstellungen.comboBox3.SelectedIndex To einstellungen.comboBox4.SelectedIndex

                    For e As Integer = 0 To Integer.MaxValue

                        If DlSoftSubsRDY = True Then
                            Exit For
                        Else
                            Await Task.Delay(2000)
                        End If
                    Next

                    Dim dd As Integer = i - einstellungen.comboBox3.SelectedIndex + 1
                    Dim URLGrapp As String() = Anzahl(i).Split(New String() {"<a href=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    'MsgBox("https://www.crunchyroll.com" + URLGrapp2(0))
                    DlSoftSubsRDY = False
                    b = False
                    d = False
                    GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com" + URLGrapp2(0))
                    Aktuell = dd.ToString
                    einstellungen.SoftSubsMass.Text = Aktuell + " / " + Gesamt
                Next



            End If
        Catch ex As Exception
            Anime_Add.comboBox4.Items.Clear()
            Anime_Add.comboBox3.Items.Clear()
            ' MsgBox(Error_Mass_DL, MsgBoxStyle.Information)
            'MsgBox(ex.ToString)
            Aktuell = 0.ToString
            Gesamt = 0.ToString

            Anime_Add.groupBox1.Visible = True
            Anime_Add.groupBox2.Visible = False
            Anime_Add.GroupBox3.Visible = False
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
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
            If HardSub = Chr(34) + "deDE" + Chr(34) Then
                Return "Deutsch"
            ElseIf HardSub = Chr(34) + "enUS" + Chr(34) Then
                Return "English"
            ElseIf HardSub = Chr(34) + "ptBR" + Chr(34) Then
                Return "Português (Brasil)"
            ElseIf HardSub = Chr(34) + "esLA" + Chr(34) Then
                Return "Español (LA)"
            ElseIf HardSub = Chr(34) + "frFR" + Chr(34) Then
                Return "Français (France)"
            ElseIf HardSub = Chr(34) + "arME" + Chr(34) Then
                Return "العربية (Arabic)"
            ElseIf HardSub = Chr(34) + "ruRU" + Chr(34) Then
                Return "Русский (Russian)"
            ElseIf HardSub = Chr(34) + "itIT" + Chr(34) Then
                Return "Italiano (Italian)"
            ElseIf HardSub = Chr(34) + "esES" + Chr(34) Then
                Return "Español (España)"
            End If

            Return CB_SuB_Nothing

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CCtoMP4CC(ByVal HardSub As String) As String
        Try
            If HardSub = "deDE" Then
                Return "ger"
            ElseIf HardSub = "enUS" Then
                Return "eng"
            ElseIf HardSub = "ptBR" Then
                Return "por"
            ElseIf HardSub = "esLA" Then
                Return "spa"
            ElseIf HardSub = "frFR" Then
                Return "fre"
            ElseIf HardSub = "arME" Then
                Return "ara"
            ElseIf HardSub = "ruRU" Then
                Return "rus"
            ElseIf HardSub = "itIT" Then
                Return "ita"
            ElseIf HardSub = "esES" Then
                Return "spa"
            ElseIf HardSub = "jaJP" Then
                Return "jpn"
            End If

            Return "chi"

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

    Public Sub GrappURL()

        Try
            'Throw New System.Exception("Test")
            Grapp_RDY = False
            Dim CR_Anime_Titel As String = Nothing
            Dim CR_Anime_Dub As String = Nothing
            Dim CR_Anime_Staffel As String = Nothing
            Dim CR_Anime_Folge As String = Nothing
#Region "Name + Pfad"
            Dim Pfad2 As String
            Dim TextBox2_Text As String = Nothing
            Dim CR_FilenName As String = Nothing
            Dim SubfolderValue As String = Nothing
            Dim CR_FilenName_Backup As String = Nothing

            Me.Invoke(New Action(Function()
                                     TextBox2_Text = Anime_Add.textBox2.Text
                                     Return Nothing
                                 End Function))
#Region "Name von Crunchyroll"
            If TextBox2_Text = Nothing Or TextBox2_Text = "Name of the Anime" Then
                Dim Bug_Deutsch As String = "-"
                If CBool(InStr(WebbrowserTitle, "Anschauen auf Crunchyroll")) Then
                    Bug_Deutsch = ":"
                End If
                Dim CR_Name_by_Titel_2 As String() = WebbrowserTitle.Split(New String() {Bug_Deutsch}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim CR_Title As String = Nothing
                'If CR_Name_by_Titel_2.Count > 2 Then
                For i As Integer = 0 To CR_Name_by_Titel_2.Count - 2
                    If CR_Title = Nothing Then
                        CR_Title = CR_Name_by_Titel_2(i).Trim()
                    Else
                        CR_Title = CR_Title + " " + CR_Name_by_Titel_2(i).Trim()
                    End If

                Next
                'Else

                'End If
                CR_FilenName = CR_Title
                CR_FilenName_Backup = CR_Title
                'MsgBox(CR_FilenName)

                If CBool(InStr(WebbrowserText, "<h4>")) Then ' false on movie true on series
                    Dim CR_Name_1 As String() = WebbrowserText.Split(New String() {"<h4>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim CR_Name_2 As String() = CR_Name_1(1).Split(New String() {"</h4>"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                    Dim CR_Name_Staffel0_Folge1 As String()
                    If CBool(InStr(CR_Name_2(0), ",")) Then
                        CR_Name_Staffel0_Folge1 = CR_Name_2(0).Split(New [Char]() {System.Convert.ToChar(",")}, System.StringSplitOptions.RemoveEmptyEntries)
                        CR_Anime_Staffel = CR_Name_Staffel0_Folge1(0).Trim()
                        CR_Anime_Folge = CR_Name_Staffel0_Folge1(1).Trim()
                        CR_Anime_Folge = System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
                    Else
                        CR_Anime_Staffel = Nothing
                        CR_Anime_Folge = CR_Name_2(0).Trim()
                        CR_Anime_Folge = System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
                    End If


                    Dim CR_Name_4 As String() = CR_Name_1(0).Split(New String() {"class=" + Chr(34) + "text-link" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                    Dim CR_Name_Anime0 As String() = CR_Name_4(CR_Name_4.Length - 1).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    CR_Name_Anime0(0) = System.Text.RegularExpressions.Regex.Replace(CR_Name_Anime0(0), "[^\w\\-]", " ")
                    CR_Anime_Titel = CR_Name_Anime0(0).Trim
                    If CR_Anime_Staffel = Nothing Then
                        CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Folge
                    Else
                        CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Staffel + " " + CR_Anime_Folge
                    End If

                    CR_FilenName_Backup = RemoveExtraSpaces(CR_FilenName)


                End If
#End Region

            Else
                Me.Invoke(New Action(Function()
                                         If Anime_Add.ComboBox2.Text = SubFolder_automatic Then
                                             MsgBox(SubFolder_automatic + " is not working with a costum name", MsgBoxStyle.Information)
                                         ElseIf Anime_Add.ComboBox2.Text = SubFolder_Nothing Then
                                         Else
                                             SubfolderValue = Anime_Add.ComboBox2.Text + "\"
                                         End If
                                         Return Nothing
                                     End Function))
                'MsgBox(TextBox2_Text)
                CR_FilenName = RemoveExtraSpaces(System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))
                CR_FilenName_Backup = CR_FilenName
            End If
            Me.Invoke(New Action(Function()
                                     If Anime_Add.ComboBox2.Text = SubFolder_automatic Then
                                         If SubFolder = 2 Then
                                             SubfolderValue = CR_Anime_Titel + "\" + CR_Anime_Staffel + "\"
                                         ElseIf SubFolder = 1 Then
                                             SubfolderValue = CR_Anime_Titel + "\"
                                         End If
                                     ElseIf Anime_Add.ComboBox2.Text = SubFolder_Nothing Then
                                     Else
                                         SubfolderValue = Anime_Add.ComboBox2.Text + "\"
                                     End If
                                     Return Nothing
                                 End Function))

            CR_FilenName = System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = RemoveExtraSpaces(CR_FilenName)
            'MsgBox(CR_FilenName)
            If SubfolderValue = Nothing Then
                Pfad2 = Pfad + "\" + CR_FilenName + ".mp4"
            Else
                Pfad2 = Pfad + "\" + SubfolderValue + CR_FilenName + ".mp4"
            End If
            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = Pfad + "\" + CR_FilenName_Backup + ".mp4"
                End Try
            End If
            Pfad2 = Chr(34) + Pfad2 + Chr(34)

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
                    Me.Invoke(New Action(Function()
                                             ResoNotFoundString = WebbrowserText
                                             DialogTaskString = "Language"
                                             Reso.ShowDialog()
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
                    Me.Invoke(New Action(Function()
                                             ResoNotFoundString = WebbrowserText
                                             DialogTaskString = "Language"
                                             Reso.ShowDialog()
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
            Dim ii As Integer = 0
            'MsgBox(Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))
            Dim CR_URI_Master As String = Nothing
            Dim CR_URI_Master_Split1 As String() = WebbrowserText.Split(New String() {My.Resources.hls_Value}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim hls_List As New List(Of String)
            For i As Integer = 0 To CR_URI_Master_Split1.Count - 1
                If InStr(CR_URI_Master_Split1(i), My.Resources.hls_endString) Then
                    Dim s As String() = CR_URI_Master_Split1(i).Split(New String() {My.Resources.hls_endString}, System.StringSplitOptions.RemoveEmptyEntries)
                    hls_List.Add(s(0))
                End If
            Next
            'Dim CR_URI_Master_Split1 As String() = WebbrowserText.Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 0 To hls_List.Count - 1
                If InStr(hls_List(i), Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)) Then

                    Dim s() As String = hls_List(i).Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    CR_URI_Master = s(1).Replace("\/", "/")
                    Dim dub() As String = hls_List(i).Split(New String() {Chr(34) + "audio_lang" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim dub2() As String = dub(0).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                    CR_Anime_Dub = dub2(0)
                    'MsgBox(CR_URI_Master)
                End If
            Next
            If CBool(InStr(CR_URI_Master, "master.m3u8")) Then
                Me.Invoke(New Action(Function()
                                         Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                         Return Nothing
                                     End Function))
            Else
                Throw New System.Exception("Premium Episode")
            End If
#End Region

#Region "Download softsub file or build ffmpeg cmd"
            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            Dim SoftSubMergeMetatata As String = Nothing

            If SoftSubs2.Count > 0 Then
                If MergeSubstoMP4 = True Then
                    For i As Integer = 0 To SoftSubs2.Count - 1
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
                            SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i))
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i))
                        End If

                    Next

                Else
                    For i As Integer = 0 To SoftSubs2.Count - 1
                        LabelUpdate = "Status: downloading subtitle file"
                        LabelEpisode = SoftSubs2(i)
                        Dim SoftSub As String() = WebbrowserText.Split(New String() {Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs2(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                        Dim SoftSub_3 As String = SoftSub_2(0).Replace("\/", "/")
                        Dim client0 As New WebClient
                        client0.Encoding = Encoding.UTF8
                        Dim str0 As String = client0.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSubs2(i) + Path.GetExtension(Pfad3)), "ass")
                        'MsgBox(FN)
                        If i = 0 Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        File.WriteAllText(Pfad4, str0, Encoding.UTF8)
                        Pause(1)
                    Next

                End If
            End If
#End Region

#Region "lösche doppel download"

            Dim Pfad5 As String = Pfad2.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
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
            If Resu = 42 Then
                If MergeSubstoMP4 = True Then
                    URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s mov_text" + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub)
                Else
                    URL_DL = "-i " + Chr(34) + CR_URI_Master + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub) + " " + ffmpeg_command
                End If
                'MsgBox(URL_DL)
            Else


                Dim client As New System.Net.WebClient
                client.Encoding = Encoding.UTF8
                'MsgBox(CR_URI_Master)
                Dim str As String = client.DownloadString(CR_URI_Master)
                'MsgBox(str)

                If CBool(InStr(str, "x" + Resu.ToString + ",")) Then
                    Resu2 = "x" + Resu.ToString
                Else
                    'MsgBox(str)
                    If CBool(InStr(str, ResuSave + ",")) Then
                        Resu2 = Resu2
                    Else
                        Me.Invoke(New Action(Function()
                                                 DialogTaskString = "Resolution"
                                                 ResoNotFoundString = str
                                                 Reso.ShowDialog()
                                                 Return Nothing
                                             End Function))


                        'MsgBox(ResoBackString)
                        If UserCloseDialog = True Then
                            Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                        Else
                            Resu2 = ResoBackString
                        End If
                    End If
                End If
                'MsgBox(Resu2)
                Dim VLC_URI_1 As String() = str.Split(New String() {Resu2 + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim VLC_URI_2 As String() = VLC_URI_1(1).Split(New [Char]() {Chr(34)})
                Dim VLC_URI_3 As String() = VLC_URI_2(2).Split(New [Char]() {System.Convert.ToChar("#")})
                If MergeSubstoMP4 = True Then
                    URL_DL = "-i " + Chr(34) + VLC_URI_3(0).Trim() + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s mov_text" + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub)

                    'URL_DL = "-i " + Chr(34) + VLC_URI_3(0).Trim() + Chr(34) + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s mov_text" + SoftSubMergeMetatata
                Else
                    URL_DL = "-i " + Chr(34) + VLC_URI_3(0).Trim() + Chr(34) + " -metadata:s:a:0 language=" + CCtoMP4CC(CR_Anime_Dub) + " " + ffmpeg_command
                    'URL_DL = VLC_URI_3(0).Trim()
                End If
                'MsgBox(URL_DL)
            End If
#Region "thumbnail"
            Dim thumbnail As String() = WebbrowserText.Split(New String() {My.Resources.thumbnailString}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + "}"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = thumbnail2(0).Replace("\/", "/")
#End Region
#Region "<li> constructor"
            Dim Subsprache3 As String = HardSubValuesToDisplay(SubSprache2)
            Dim ResoHTMLDisplay As String = Nothing
            If ResoBackString = Nothing Then
                ResoHTMLDisplay = Resu.ToString + "p"
            ElseIf DialogTaskString = "Language" Then
                ResoHTMLDisplay = Resu.ToString + "p"
            Else
                Dim ResoHTML As String() = ResoBackString.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                If ResoHTML.Count > 1 Then
                    ResoHTMLDisplay = ResoHTML(1) + "p"

                Else
                    ResoHTMLDisplay = ResoHTML(0) + "p"
                End If
            End If
            Dim L2Name As String = CR_FilenName_Backup
            If Resu = 42 Then
                ResoHTMLDisplay = "[Auto]"
            End If
            Pfad_DL = Pfad2
            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "")
            Me.Invoke(New Action(Function()
                                     ListItemAdd(Pfad_DL, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail3, URL_DL, Pfad_DL)
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
            'Form1.RichTextBox1.Text = My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->"
#End Region

            Grapp_RDY = True
            Me.Invoke(New Action(Function()

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Return Nothing
                                 End Function))
        Catch ex As Exception
            Me.Invoke(New Action(Function()

                                     Anime_Add.StatusLabel.Text = "Status: idle"
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
                If MessageBox.Show(Error_unknown, "Error!", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim CCC As String() = WebbrowserText.Split(New String() {My.Resources.CC_String}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim CCC1 As String() = CCC(1).Split(New String() {".gif" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim SaveString As String = "Operating System: " + My.Computer.Info.OSFullName + vbNewLine + vbNewLine + "Crunchyroll URL: " + WebbrowserURL + vbNewLine + vbNewLine + "subtitle language: " + SubSprache + vbNewLine + vbNewLine + "video resolution: " + Resu.ToString + vbNewLine + vbNewLine + "error message: " + ex.ToString + vbNewLine + ex.StackTrace.ToString + vbNewLine + vbNewLine + "softsubs enabled?: " + SoftSubs.ToString + vbNewLine + vbNewLine + "Crunchyroll Downloader Version: " + Application.ProductVersion + vbNewLine + vbNewLine + "detected location from Crunchyroll: " + CCC1(0)

                    File.WriteAllText("Error " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".txt", SaveString)
                    Dim Request As HttpWebRequest = CType(WebRequest.Create("https://docs.google.com/forms/d/e/1FAIpQLSdR1QI19Lh-c-XO_iXNkDwsTUZhCMEu84boQkgW5AOBUxyiyA/formResponse"), HttpWebRequest)
                    Request.Method = "POST"
                    Request.ContentType = "application/x-www-form-urlencoded"
                    Dim Post As String = "entry.240217066=" + My.Computer.Info.OSFullName + "&entry.358200455=" + WebbrowserURL + "&entry.618751432=" + SubSprache + "&entry.924054550=" + Resu.ToString + "&entry.679000538=" + ex.ToString + "&entry.1789515979=" + SoftSubsString + "&entry.683247287=" + Application.ProductVersion + "&entry.377264428=" + CCC1(0) + "&fvv=1&draftResponse=[null,null," + Chr(34) + "-3005021683493723280" + Chr(34) + "] &pageHistory=0&fbzx=-3005021683493723280"
                    Dim byteArray() As Byte = Encoding.UTF8.GetBytes(Post)
                    Request.ContentLength = byteArray.Length
                    Dim DataStream As Stream = Request.GetRequestStream()
                    DataStream.Write(byteArray, 0, byteArray.Length)
                    DataStream.Close()
                    Dim Response As HttpWebResponse = Request.GetResponse()
                    DataStream = Response.GetResponseStream()
                    Dim reader As New StreamReader(DataStream)
                    Dim ServerResponse As String = reader.ReadToEnd()
                    reader.Close()
                    DataStream.Close()
                    Response.Close()
                    Dim Version_Check As String() = ServerResponse.Split(New String() {"<div class=" + Chr(34) + "freebirdFormviewerViewResponseConfirmationMessage" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim Version_Check2 As String() = Version_Check(1).Split(New String() {"</div>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    'If Application.ProductVersion = Version_Check2(0) Then
                    'Else
                    'MsgBox("A newer version is available: v" + Version_Check2(0))
                    'End If
                End If
            End If

        End Try
    End Sub


    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            Me.Visible = False
            'Pause(2)
        Catch ex As Exception
        End Try

    End Sub


    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        If RunningDownloads > 0 Then
            If MessageBox.Show("Are you sure you want close the program and end all active downloads?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                For i As Integer = 0 To ListView1.Items.Count - 1
                    ItemList(i).KillRunningTask()
                Next
                RunServer = False

                Me.Close()
            End If
        Else
            RunServer = False
            Me.Close()
        End If
    End Sub


    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        Anime_Add.Show()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles pictureBox2.Click
        einstellungen.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        UserBowser = True
        GeckoFX.Show()
    End Sub

#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
#End Region

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs)
        Startup.ShowDialog()
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
                ItemList(s).SetBounds(r.X, r.Y, r.Width, r.Height)

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
        If InStr(e.Data, ": Video:") Then
            Dim ZeileReso() As String = e.Data.Split(New String() {" ["}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ZeileReso3() As String = e.Data.Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ZeileReso4() As String = ZeileReso3(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)
            'If ResoAvalibe = Nothing Then
            '    ResoAvalibe = ZeileReso2(ZeileReso2.Count - 1).Trim + ":--:" + ZeileReso4(1)
            'Else
            ResoAvalibe = ResoAvalibe + vbNewLine + ZeileReso2(ZeileReso2.Count - 1).Trim + ":--:" + ZeileReso4(1)
            'End If
        ElseIf InStr(e.Data, "Duration:") Then
            ResoAvalibe = Nothing
        ElseIf InStr(e.Data, "At least one output file must be specified") Then
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
        '    If InStr(Zeilen(i), "x" + Resu.ToString + " [") Then
        '        Dim ZeileReso() As String = Zeilen(i).Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
        '        Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)
        '        StreamNR = ZeileReso2(1)
        '    End If
        'Next

        'Return ZeitAnzeige + "#1" + StreamNR
    End Sub
#End Region

    Public Sub Grapp_non_CR()

        If NonCR_URL = Nothing Then Exit Sub
        Me.Invoke(New Action(Function()
                                 Anime_Add.StatusLabel.Text = "Status: m3u8 found, trying to start the download"
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
        Video_FilenName = System.Text.RegularExpressions.Regex.Replace(Video_FilenName, "[^\w\\-]", " ")
        Video_FilenName = RemoveExtraSpaces(Video_FilenName + ".mp4")
#End Region

#Region "thumbnail"
        Dim thumbnail As String() = Nothing
        Dim thumbnail2 As String() = Nothing
        Dim thumbnail4 As String = "None, will usese fail image"
        Try
            If InStr(WebbrowserText, "thumbnail") Then
                thumbnail = WebbrowserText.Split(New String() {"thumbnail"}, System.StringSplitOptions.RemoveEmptyEntries)
            End If
        Catch ex As Exception

        End Try
        Try
            For i As Integer = 0 To thumbnail.Count - 1
                If InStr(thumbnail(i), ".jpg") Then
                    If InStr(thumbnail(i), "https:") Then
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
        Dim Subsprache3 As String = "undefined" 'HardSubValuesToDisplay(SubSprache2)
        Dim ResoHTMLDisplay As String = "[Auto]"
        Dim L2Name As String = Video_Title
        Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim L1Name As String = L1Name_Split(1)
        Pfad_DL = Chr(34) + Pfad + "\" + Video_FilenName + Chr(34)

        'If InStr(ResoAvalibe, Resu.ToString) Then
        '    Dim ResoUse As String() = ResoAvalibe.Split(New String() {Resu.ToString + ":--:"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim ResoUse2 As String() = ResoUse(1).Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
        '    UsedMap = ResoUse2(0)
        '    If Debug2 = True Then
        '        MsgBox(UsedMap)
        '    End If
        '    ResoHTMLDisplay = Resu.ToString + "p"
        'Else
        ResoHTMLDisplay = "[Auto]"
        'End If
        Dim cmd As String = "-i " + Chr(34) + URL_DL + Chr(34) + " " + ffmpeg_command
        Me.Invoke(New Action(Function()
                                 ListItemAdd(Pfad_DL, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail4, cmd, Pfad_DL)
                                 Return Nothing
                             End Function))

#End Region
        'AsyncWorkerX.RunAsync(AddressOf DownloadFFMPEG, URL_DL, Pfad_DL, Pfad_DL)
        Grapp_non_cr_RDY = True
        Me.Invoke(New Action(Function()

                                 Anime_Add.StatusLabel.Text = "Status: idle"
                                 Return Nothing
                             End Function))

    End Sub

    Private Sub PictureBox2_DoubleClick(sender As Object, e As EventArgs) Handles pictureBox2.DoubleClick
        If Debug1 = True Then
            If Debug2 = True Then
                einstellungen.Close()
                Try
                    My.Computer.Clipboard.SetText(WebbrowserText)

                    MsgBox("webbrowser text copyed to the clipboard")
                Catch ex As Exception
                End Try
            Else
                Debug2 = True
                einstellungen.Close()
                MsgBox("Debug activated")
            End If
        Else
            Debug1 = True
            einstellungen.Close()
            'MsgBox("Debug activated")
        End If
    End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim ItemDownloadingCount As Integer = 0
            For i As Integer = 0 To ListView1.Items.Count - 1
                If ItemList(i).GetIsStatusFinished() = False Then
                    ItemDownloadingCount = ItemDownloadingCount + 1
                End If
            Next
            RunningDownloads = ItemDownloadingCount
        Catch ex As Exception

        End Try
        'FontLabel2.Text = RunningDownloads.ToString
    End Sub

    Public Sub Funitmation_Grapp()
        Try


            Funimation_Grapp_RDY = False
#Region "Name"
#Region "old version"


            'Dim FunimationName() As String = WebbrowserText.Split(New String() {"</h1>"}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim FunimationName2() As String = FunimationName(0).Split(New String() {Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim FunimationName3 As String = FunimationName2(FunimationName2.Count - 1).Replace("</a>", "")
            'FunimationName3 = System.Text.RegularExpressions.Regex.Replace(FunimationName3, "[^\w\\-]", " ")
            'FunimationName3 = RemoveExtraSpaces(FunimationName3)
            'Dim DownloadPfad As String = Chr(34) + Pfad + "\" + FunimationName3 + ".mp4" + Chr(34)
#End Region
            Dim DownloadPfad As String = Nothing
            Dim FunimationSeason As String = Nothing
            Dim FunimationEpisode As String = Nothing
            Dim FunimationTitle As String = Nothing
            Dim FunimationDub As String = Nothing

            Dim FunimationSeason1() As String = WebbrowserText.Split(New String() {"seasonNum: "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationSeason2() As String = FunimationSeason1(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationSeason = "Season " + FunimationSeason2(0)

            Dim FunimationEpisode1() As String = WebbrowserText.Split(New String() {"episodeNum: "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationEpisode2() As String = FunimationEpisode1(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationEpisode = "Episode " + FunimationEpisode2(0)

            Dim FunimationTitle1() As String = WebbrowserText.Split(New String() {".showName = '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationTitle2() As String = FunimationTitle1(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationTitle = System.Text.RegularExpressions.Regex.Replace(FunimationTitle2(0), "[^\w\\-]", " ").Trim(" ")
            FunimationTitle = RemoveExtraSpaces(FunimationTitle)
            Dim FunimationDub1() As String = WebbrowserText.Split(New String() {".showLanguage =  '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim FunimationDub2() As String = FunimationDub1(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            FunimationDub = FunimationDub2(0)


            Dim DefaultName As String = RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)

            Dim DefaultPath As String = Pfad + "\" + DefaultName + ".mp4"
#End Region

#Region "Pfad"
            Dim TextBox2_Text As String = Nothing
            Dim SubfolderValue As String = Nothing
            Me.Invoke(New Action(Function()
                                     TextBox2_Text = Anime_Add.textBox2.Text
                                     Return Nothing
                                 End Function))

            If TextBox2_Text = Nothing Or TextBox2_Text = "Name of the Anime" Then

            Else
                Me.Invoke(New Action(Function()
                                         If Anime_Add.ComboBox2.Text = SubFolder_automatic Then
                                             MsgBox(SubFolder_automatic + " is not working with a costum name", MsgBoxStyle.Information)
                                         ElseIf Anime_Add.ComboBox2.Text = SubFolder_Nothing Then
                                         Else
                                             SubfolderValue = Anime_Add.ComboBox2.Text + "\"
                                         End If
                                         Return Nothing
                                     End Function))
            End If

            Me.Invoke(New Action(Function()
                                     If Anime_Add.ComboBox2.Text = SubFolder_automatic Then
                                         If SubFolder = 2 Then
                                             SubfolderValue = FunimationTitle + "\" + FunimationSeason + "\"
                                         ElseIf SubFolder = 1 Then
                                             SubfolderValue = FunimationTitle + "\"
                                         End If
                                     ElseIf Anime_Add.ComboBox2.Text = SubFolder_Nothing Then
                                     Else
                                         SubfolderValue = Anime_Add.ComboBox2.Text + "\"
                                     End If
                                     Return Nothing
                                 End Function))

            If SubfolderValue = Nothing Then
                DownloadPfad = Pfad + "\" + DefaultName + ".mp4"
            Else
                DownloadPfad = Pfad + "\" + SubfolderValue + DefaultName + ".mp4"
            End If
            If Not Directory.Exists(Path.GetDirectoryName(DownloadPfad)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(DownloadPfad))
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    DownloadPfad = Pfad + "\" + DefaultName + ".mp4"
                End Try
            End If


#End Region
#Region "m3u8 URL"
            Dim Player_ID() As String = WebbrowserText.Split(New String() {My.Resources.Funimation_Player_ID}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Player_ID2() As String = Player_ID(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Me.Invoke(New Action(Function()
                                     '    Anime_Add.StatusLabel.Text = iFrameURL

                                     Return Nothing
                                 End Function))

            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            If WebbrowserCookie = Nothing Then
            Else
                client0.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
            End If
            Dim str0 As String = client0.DownloadString("https://www.funimation.com/api/showexperience/" + Player_ID2(0) + "/?pinst_id=fzQc9p9f")
            Dim Funimation_m3u8() As String = str0.Split(New String() {My.Resources.Funimation_src_string}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Funimation_m3u8_final As String = Nothing
            Dim Funimation_m3u8_Main As String = Nothing
            For i As Integer = 0 To Funimation_m3u8.Count - 1
                If InStr(Funimation_m3u8(i), "m3u8?") Then
                    Dim Funimation_m3u8_split() As String = Funimation_m3u8(i).Split(New String() {", "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Funimation_m3u8_Main = Funimation_m3u8_split(0)
                    Exit For
                End If
            Next
            If Funimation_m3u8_Main = Nothing Then

                If MessageBox.Show("No media found in:" + vbNewLine + str0, "No media", MessageBoxButtons.RetryCancel) = DialogResult.Retry Then
                    Me.Invoke(New Action(Function()
                                             GeckoFX.WebBrowser1.Navigate(WebbrowserURL)
                                             Try
                                                 Anime_Add.StatusLabel.Text = "retrying Funimation"
                                             Catch ex As Exception
                                             End Try
                                             Return Nothing
                                         End Function))
                    Exit Sub
                Else
                    Funimation_Grapp_RDY = True
                    Exit Sub
                End If
            Else
                'MsgBox(Funimation_m3u8_Main)
            End If
            Dim str1 As String = client0.DownloadString(Funimation_m3u8_Main.Replace(Chr(34), ""))
            Dim textLenght() As String = str1.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)

            Me.Invoke(New Action(Function()
                                     FontLabel2.Text = textLenght.Count
                                     Return Nothing
                                 End Function))


            For i As Integer = 0 To textLenght.Length - 1
                If InStr(textLenght(i), "https") Then
                    If InStr(textLenght(i - 1), "x" + Resu.ToString) Then
                        Funimation_m3u8_final = textLenght(i)
                        Exit For
                    End If
                End If
            Next

            If Funimation_m3u8_final = Nothing Then
                Me.Invoke(New Action(Function()
                                         DialogTaskString = "Funimation_Resolution"
                                         ResoNotFoundString = str1
                                         Reso.ShowDialog()
                                         Return Nothing
                                     End Function))

                For i As Integer = 0 To textLenght.Length - 1
                    If InStr(textLenght(i), "https") Then
                        If InStr(textLenght(i - 1), ResoBackString) Then
                            Funimation_m3u8_final = textLenght(i)
                            Exit For
                        End If
                    End If
                Next

            End If

            'MsgBox(FunimationName3)
            'MsgBox(Funimation_m3u8_final)
#Region "thumbnail"

            Dim thumbnail As String() = WebbrowserHeadText.Split(New String() {My.Resources.Funimation_thumbnail}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = thumbnail2(0) '.Replace("\/", "/")
#End Region
            Dim ResoHTMLDisplay As String = Resu.ToString + "p"

#Region "Subs"
            Dim SubsClient As New WebClient
            SubsClient.Encoding = Encoding.UTF8
            If WebbrowserCookie = Nothing Then
            Else
                SubsClient.Headers.Add(HttpRequestHeader.Cookie, WebbrowserCookie)
            End If
            Dim PlayerPage As String = SubsClient.DownloadString("https://www.funimation.com/player/" + Player_ID2(0) + "/?bdub=0&qid=")
            Dim SplittString As String = Nothing
            If InStr(PlayerPage, My.Resources.Funimation_Subtitle_String) Then
                SplittString = My.Resources.Funimation_Subtitle_String
            ElseIf InStr(PlayerPage, My.Resources.Funimation_Subtitle_String2) Then
                SplittString = My.Resources.Funimation_Subtitle_String2

            End If
            Dim UsedSub As String = Nothing
            If SplittString = Nothing Then
                If InStr(PlayerPage, ".srt") Then
                    Dim SubTitle1() As String = PlayerPage.Split(New String() {".srt"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SubTitle2() As String = SubTitle1(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    UsedSub = SubTitle2(SubTitle2.Count - 1) + ".srt"
                ElseIf InStr(PlayerPage, ".vtt") Then
                    Dim SubTitle1() As String = PlayerPage.Split(New String() {".vtt"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SubTitle2() As String = SubTitle1(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    UsedSub = SubTitle2(SubTitle2.Count - 1) + ".vtt"
                ElseIf InStr(PlayerPage, ".dfxp") Then
                    Dim SubTitle1() As String = PlayerPage.Split(New String() {".dfxp"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SubTitle2() As String = SubTitle1(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    UsedSub = SubTitle2(SubTitle2.Count - 1) + ".dfxp"
                Else
                    If MessageBox.Show("No Subtitle found in the website, a logfile was created." + vbNewLine + "Press 'Yes' to download the video without subtitle", "No Subtitle", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        File.WriteAllText(Path.Combine(Application.StartupPath + "No Subtitle for" + DownloadPfad.Replace(".mp4", ".log")), PlayerPage, Encoding.UTF8)
                    Else
                        File.WriteAllText(Path.Combine(Application.StartupPath + "No Subtitle for" + DownloadPfad.Replace(".mp4", ".log")), PlayerPage, Encoding.UTF8)
                        Exit Sub
                    End If
                    'MsgBox("No Subtitle found in the website, a logfile was created.", MsgBoxStyle.OkCancel, "No Subtitle")
                End If
            Else


                Dim SubTitle1() As String = PlayerPage.Split(New String() {SplittString}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Subs_in_srt As New List(Of String)
                Dim Subs_in_vtt As New List(Of String)
                Dim Subs_in_dfxp As New List(Of String)

                For i As Integer = 0 To SubTitle1.Count - 1
                    Dim SubTitle2() As String = SubTitle1(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)


                    If InStr(SubTitle2(SubTitle2.Count - 1), ".srt") Then
                        Subs_in_srt.Add(SubTitle2(SubTitle2.Count - 1))
                    ElseIf InStr(SubTitle2(SubTitle2.Count - 1), ".vtt") Then
                        Subs_in_vtt.Add(SubTitle2(SubTitle2.Count - 1))
                    ElseIf InStr(SubTitle2(SubTitle2.Count - 1), ".dfxp") Then
                        Subs_in_dfxp.Add(SubTitle2(SubTitle2.Count - 1))
                    End If
                Next

                If Subs_in_srt.Count > 0 Then
                    UsedSub = Subs_in_srt.Item(0)
                ElseIf Subs_in_vtt.Count > 0 Then
                    UsedSub = Subs_in_vtt.Item(0)
                ElseIf Subs_in_dfxp.Count > 0 Then
                    UsedSub = Subs_in_dfxp.Item(0)
                End If

                If MergeSubstoMP4 = True Then
                Else
                    'MsgBox(WebbrowserSoftSubURL)
                    Dim str2 As String = client0.DownloadString(UsedSub)
                    Dim SubtitelFormat As String = ".srt"
                    If InStr(UsedSub, ".vtt") Then
                        SubtitelFormat = ".vtt"
                    ElseIf InStr(UsedSub, ".dfxp") Then
                        SubtitelFormat = ".dfxp"
                    End If
                    Dim Pfad3 As String = DownloadPfad.Replace(Chr(34), "")
                    Dim Pfad4 As String = Pfad3.Replace(".mp4", SubtitelFormat)
                    File.WriteAllText(Pfad4, str2, Encoding.UTF8)
                End If

            End If
#End Region

#Region "SubsToMP4"
            If MergeSubstoMP4 = True Then
                If UsedSub = Nothing Then
                Else
                    Dim DubMetatata As String = " -metadata:s:a:0 language=jpn"
                    If FunimationDub = "japanese" Then
                        DubMetatata = " -metadata:s:a:0 language=jpn"
                        'Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command
                    Else
                        DubMetatata = " -metadata:s:a:0 language=eng"
                        'Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command
                    End If

                    Dim SoftSubMergeURLs As String = " -i " + Chr(34) + UsedSub + Chr(34)
                    Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a -map 1"
                    Dim SoftSubMergeMetatata As String = " -metadata:s:s:0 language=eng"
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command + " -c:s mov_text" + SoftSubMergeMetatata
                End If
            Else
                If FunimationDub = "japanese" Then
                    Dim DubMetatata As String = " -metadata:s:a:0 language=jpn"
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command
                Else
                    Dim DubMetatata As String = " -metadata:s:a:0 language=eng"
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command
                End If

            End If

#End Region

            'DownloadPfad = DownloadPfad.Replace(" \", "\")
            DownloadPfad = RemoveExtraSpaces(DownloadPfad)
            Dim L1Name_Split As String() = WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Me.Invoke(New Action(Function()
                                     ListItemAdd(Pfad_DL, L1Name, DefaultName, ResoHTMLDisplay, "Unknown", SubValuesToDisplay(), thumbnail3, Funimation_m3u8_final, Chr(34) + DownloadPfad + Chr(34))
                                     Return Nothing
                                 End Function))
            liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + FunimationTitle + " <br> " + FunimationSeason + " " + FunimationEpisode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + DefaultName + "-->")

#End Region

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Funimation_Grapp_RDY = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            Dim GeckoHTML As String = My.Resources.htmlTop
            Dim LiAdd As String = Nothing
            For ii As Integer = 0 To ItemList.Count - 1

                For i As Integer = 0 To liList.Count - 1
                    If InStr(liList(i), "<!-- " + ItemList.Item(ii).GetNameAnime + "-->") Then

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
                Next
            Next
            Dim c As String = GeckoHTML + vbNewLine + LiAdd + vbNewLine + My.Resources.htmlEnd

            Dim Balken As String = "balken.png"
            c = c.Replace("balken1.png", Balken)
            Dim CC As String = "cc.png"
            c = c.Replace("cc1.png", CC)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\index.html", c, False)

        Catch ex As Exception

            'MsgBox(ex.ToString)
        End Try
    End Sub
#Region "server"
    Dim tcpListener As TcpListener
    Public Sub ServerStart()
        Try
            Dim hostName As String = "localhost" 'Dns.GetHostName()
            Dim Adresscount As Integer
            For i As Integer = 0 To Dns.GetHostEntry(hostName).AddressList.Count - 1
                If Dns.GetHostEntry(hostName).AddressList(i).ToString = "127.0.0.1" Then
                    Adresscount = i
                End If
            Next
            If Adresscount = Nothing Then
                MsgBox("http server start failed")
                Exit Sub
            End If
            Dim serverIP As IPAddress = Dns.GetHostEntry(hostName).AddressList(Adresscount) 'Dns.Resolve(hostName).AddressList(0) 'New IPAddress("localhost") '
            ' Web Server Port = 80  
            Dim Port As String = "80"
            tcpListener = New TcpListener(serverIP, Int32.Parse(Port))
            tcpListener.Start()
            Console.WriteLine("Web server started at: " & serverIP.ToString() & ":" & Port)

            ProcessThread()
        Catch ex As Exception

            MsgBox(ex.ToString())
        End Try
    End Sub


    Public Sub ProcessThread()
        While (True)

            Dim clientSocket As Socket
            Try
                clientSocket = tcpListener.AcceptSocket()
                clientSocket.ReceiveBufferSize = 1048576
                ' Socket Information
                Dim clientInfo As IPEndPoint = CType(clientSocket.RemoteEndPoint, IPEndPoint)
                Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString())
                ' Set Thread for each Web Browser Connection
                Dim clientThread As New Thread(Sub() Me.ProcessRequest(clientSocket))
                clientThread.Start()
            Catch ex As Exception
                Console.WriteLine(ex.ToString())
                'If clientSocket.Connected Then
                '    clientSocket.Close()
                'End If
            End Try
        End While

    End Sub
    Protected Sub ProcessRequest(ByVal clientSocket As Socket)
        Dim recvBytes(1048576) As Byte
        Dim htmlReq As String = Nothing
        Dim bytes As Long
        Try
            ' Receive HTTP Request from Web Browser
            bytes = clientSocket.Receive(recvBytes, 0, clientSocket.Available, SocketFlags.None)
            htmlReq = Encoding.UTF8.GetString(recvBytes, 0, bytes)

            Dim rootPath As String = Directory.GetCurrentDirectory() & "\WebInterface\"
            ' Set default page
            Dim defaultPage As String = "index.html"
            Dim PostPage As String = "post.html"
            Dim strArray() As String
            Dim strRequest As String
            strArray = htmlReq.Trim.Split(" ")
            'MsgBox(htmlReq)
            If strArray(0).Trim().ToUpper.Equals("GET") Then
                strRequest = strArray(1).Trim

                If strRequest.StartsWith("/") Then
                    strRequest = strRequest.Substring(1)
                End If
                If strRequest.EndsWith("/") Or strRequest.Equals("") Then
                    strRequest = strRequest & defaultPage '"HTMLString" 'strRequest & defaultPage
                End If

                strRequest = rootPath & strRequest
                sendHTMLResponse(strRequest, clientSocket)
            ElseIf strArray(0).Trim().ToUpper.Equals("POST") Then
                If InStr(htmlReq, "HTMLSingle=") Then
                    Try
                        Dim html() As String = htmlReq.Split(New String() {"HTMLSingle="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {My.Resources.CR_Head_Url_Split}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLSplit2() As String = URLSplit(1).Split(New String() {Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserURL = URLSplit2(0)
                        Dim BodySplit() As String = DecodedHTML.Split(New String() {"<body"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserText = BodySplit(1)
                        Dim TitleSplit() As String = DecodedHTML.Split(New String() {"<title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim TitleSplit2() As String = TitleSplit(1).Split(New String() {"</title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserTitle = TitleSplit2(0)
                        If Grapp_RDY = True Then
                            Dim t As Thread
                            t = New Thread(AddressOf GrappURL)
                            t.Priority = ThreadPriority.Normal
                            t.IsBackground = True
                            t.Start()
                        Else
                            If Anime_Add.Visible = True Then
                                Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                            Else
                                ListBoxList.Add(WebbrowserURL)
                            End If
                        End If
                        strRequest = rootPath & "Post_Single_Sucess.html" 'PostPage
                        sendHTMLResponse(strRequest, clientSocket)
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        sendHTMLResponse(strRequest, clientSocket)
                    End Try
                ElseIf InStr(htmlReq, "HTMLMass=") Then
                    Try
                        Dim html() As String = htmlReq.Split(New String() {"HTMLMass="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {"javascript:"}, System.StringSplitOptions.RemoveEmptyEntries)
                        If Anime_Add.Visible = True Then
                            For i As Integer = 0 To URLSplit.Count - 1
                                Anime_Add.ListBox1.Items.Add(URLSplit(i))
                            Next
                        Else
                            For i As Integer = 0 To URLSplit.Count - 1
                                ListBoxList.Add(URLSplit(i))
                            Next
                        End If
                        strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                        sendHTMLResponse(strRequest, clientSocket)
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        sendHTMLResponse(strRequest, clientSocket)
                    End Try
                ElseIf InStr(htmlReq, "FunimationHTML=") Then
                    Try


                        Dim html() As String = htmlReq.Split(New String() {"FunimationHTML="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "/log.txt", DecodedHTML, True)
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {"FunimationURL="}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserURL = URLSplit(1)
                        Dim BodySplit() As String = DecodedHTML.Split(New String() {"<body"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserText = BodySplit(1)
                        Dim TitleSplit() As String = DecodedHTML.Split(New String() {"<title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim TitleSplit2() As String = TitleSplit(1).Split(New String() {"</title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserTitle = TitleSplit2(0)
                        Dim HeadSplit() As String = DecodedHTML.Split(New String() {"<head"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim HeadSplit2() As String = HeadSplit(0).Split(New String() {"</head>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        WebbrowserHeadText = HeadSplit2(0)

                        If Funimation_Grapp_RDY = True Then
                            'Dim t As Thread
                            't = New Thread(AddressOf Funitmation_Grapp)
                            't.Priority = ThreadPriority.Normal
                            't.IsBackground = True
                            't.Start()


                            If RunningDownloads >= MaxDL Then
                                Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                            Else
                                Me.Invoke(New Action(Function()
                                                         GeckoFX.WebBrowser1.Navigate(WebbrowserURL)
                                                         Return Nothing
                                                     End Function))

                                b = False
                            End If

                            Else
                            If Anime_Add.Visible = True Then
                                Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                            Else
                                ListBoxList.Add(WebbrowserURL)
                            End If
                        End If
                        strRequest = rootPath & "Post_Single_Sucess.html" 'PostPage
                        sendHTMLResponse(strRequest, clientSocket)
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        strRequest = rootPath & "error_Page.html" 'PostPage
                        sendHTMLResponse(strRequest, clientSocket)
                    End Try
                Else

                    strRequest = rootPath & "error_Page_default.html" 'PostPage
                    sendHTMLResponse(strRequest, clientSocket)
                End If

            Else ' Not HTTP GET method
                strRequest = rootPath & defaultPage
                sendHTMLResponse(strRequest, clientSocket)

            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            If clientSocket.Connected Then
                clientSocket.Close()
            End If
        End Try
    End Sub
    ' Send HTTP Response


    Private Sub sendHTMLResponse(ByVal httpRequest As String, ByVal clientSocket As Socket)
        Try

            Dim respByte() As Byte
            If File.Exists(httpRequest) Then
                'Console.WriteLine(httpRequest)
                respByte = File.ReadAllBytes(httpRequest)

                ' Set HTML Header
                Dim htmlHeader As String =
                    "HTTP/1.0 200 OK" & ControlChars.CrLf &
                    "Server: WebServer 1.0" & ControlChars.CrLf &
                    "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & getContentType(httpRequest) &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                'Console.WriteLine("HTML Header: " & ControlChars.CrLf & htmlHeader)
                ' Send HTML Header back to Web Browser
                clientSocket.Send(headerByte, 0, headerByte.Length, SocketFlags.None)
                ' Send HTML Content back to Web Browser
                clientSocket.Send(respByte, 0, respByte.Length, SocketFlags.None)
                ' Close HTTP Socket connection
                clientSocket.Shutdown(SocketShutdown.Both)
                clientSocket.Close()
            Else

                respByte = Encoding.UTF8.GetBytes(My.Resources.Error_404) 'File.ReadAllBytes(httpRequest)

                ' Set HTML Header
                Dim htmlHeader As String =
                "HTTP/1.0 404 Not Found" & ControlChars.CrLf &
                "Server: WebServer 1.0" & ControlChars.CrLf &
                 ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                Console.WriteLine("HTML Header: " & ControlChars.CrLf & htmlHeader)
                ' Send HTML Header back to Web Browser
                clientSocket.Send(headerByte, 0, headerByte.Length, SocketFlags.None)
                ' Send HTML Content back to Web Browser
                clientSocket.Send(respByte, 0, respByte.Length, SocketFlags.None)
                ' Close HTTP Socket connection
                clientSocket.Shutdown(SocketShutdown.Both)
                clientSocket.Close()
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            If clientSocket.Connected Then
                clientSocket.Close()
            End If

        End Try
    End Sub

    ' Get Content Type
    Private Function getContentType(ByVal httpRequest As String) As String
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
        Else
            Return "text/plain"
        End If
    End Function

    Private Sub Main_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        'Try
        '    For i As Integer = 0 To ListBoxList.Count - 1
        '        MsgBox(ListBoxList.Item(i))
        '    Next
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub
#End Region
End Class
