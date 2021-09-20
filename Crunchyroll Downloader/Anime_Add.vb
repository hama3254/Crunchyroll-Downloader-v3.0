Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components

Public Class Anime_Add
    Public Mass_DL_Cancel As Boolean = False
    Public List_DL_Cancel As Boolean = False
    Public AoD_Cookie As String = Nothing
    Dim AoD_OmUList As New List(Of String)
    Dim AoD_DubList As New List(Of String)

    Dim AoD_OmU_Episodes As New List(Of AoDEpisodes)
    Dim AoD_Dub_Episodes As New List(Of AoDEpisodes)


    Dim AoD_OmUVideoIDList As New List(Of String)
    Dim AoD_DubVideoIDList As New List(Of String)
    Public ThreadList As New List(Of Thread)

    Public ProcessedAoDNew As Integer = 0
    Public ToProcessAoDNew As Integer = 200



    Dim AoD_Mode As Boolean = False
    Dim AoD_DL_running As Boolean = False
    Public AoDHTML As String = Nothing

    Public Authorization As String = Nothing
    Public AuthorizationCookie As String = Nothing

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            If ComboBox2.Text = SubFolder_Nothing Then
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", SubFolder_Nothing, RegistryValueKind.String)
                SubFolder_Value = SubFolder_Nothing
            ElseIf ComboBox2.Text = SubFolder_automatic Then
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", SubFolder_automatic, RegistryValueKind.String)
                SubFolder_Value = SubFolder_automatic
            ElseIf ComboBox2.Text = SubFolder_automatic2 Then
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", SubFolder_automatic2, RegistryValueKind.String)
                SubFolder_Value = SubFolder_automatic2
            Else
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", ComboBox2.Text, RegistryValueKind.String)
                SubFolder_Value = ComboBox2.Text
            End If
        Catch ex As Exception
            ComboBox2.Text = SubFolder_Nothing
        End Try
    End Sub
    Dim Manager As MetroStyleManager = Main.Manager


    Private Sub Anime_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SubTitlesOnlyCB.SelectedIndex = 0
        Manager.Owner = Me
        Me.StyleManager = Manager
        Btn_Close.Image = Main.CloseImg
        Btn_min.Image = Main.MinImg

        ListBox1.BackColor = Main.BackColorValue
        ListBox1.ForeColor = Main.ForeColorValue


        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        Try
            Dim ListBox1List As New List(Of String)
            'Jeder Eintrag in der Combobox durchgehen
            For Each item As String In Main.ListBoxList
                'Wenn der Combobox-Eintrag noch nicht in der Result-List vorhanden ist, Eintrag der Result-List hinzufügen
                If ListBox1List.Contains(item) = False Then
                    ListBox1List.Add(item)
                End If
            Next
            ListBox1.Items.Clear()
            'Die Result-List der Combobox hinzufügen
            ListBox1.Items.AddRange(ListBox1List.ToArray)


            'For i As Integer = 0 To Main.ListBoxList.Count - 1
            '    ListBox1.Items.Add(Main.ListBoxList.Item(i))
            'Next
        Catch ex As Exception

        End Try
        Try
            Main.waveOutSetVolume(0, 0)
        Catch ex As Exception

        End Try
        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        TextBox4.Text = Main.Pfad

        ' Dim SubFolder_Value As String
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubFolder_Value = rkg.GetValue("SubFolder_Value").ToString
            If SubFolder_Value = SubFolder_Nothing Then
                ComboBox2.Items.Add(SubFolder_automatic)
                ComboBox2.Items.Add(SubFolder_automatic2)
                ComboBox2.Items.Add(SubFolder_Nothing)
            ElseIf SubFolder_Value = SubFolder_automatic Then
                ComboBox2.Items.Add(SubFolder_automatic)
                ComboBox2.Items.Add(SubFolder_automatic2)
                ComboBox2.Items.Add(SubFolder_Nothing)
            ElseIf SubFolder_Value = SubFolder_automatic2 Then
                ComboBox2.Items.Add(SubFolder_automatic)
                ComboBox2.Items.Add(SubFolder_automatic2)
                ComboBox2.Items.Add(SubFolder_Nothing)
            Else

                ComboBox2.Items.Add(SubFolder_automatic)
                ComboBox2.Items.Add(SubFolder_automatic2)
                ComboBox2.Items.Add(SubFolder_Nothing)
                ComboBox2.Items.Add(SubFolder_Value)
            End If
        Catch ex As Exception
            ComboBox2.Items.Add(SubFolder_automatic)
            ComboBox2.Items.Add(SubFolder_automatic2)
            ComboBox2.Items.Add(SubFolder_Nothing)
            ComboBox2.SelectedItem = SubFolder_Nothing
            SubFolder_Value = SubFolder_Nothing
        End Try

        Try
            Dim di As New System.IO.DirectoryInfo(Main.Pfad)
            For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                Else
                    ComboBox2.Items.Add(fi.Name)
                End If
            Next
            Dim Result As New List(Of String)
            'Jeder Eintrag in der Combobox durchgehen
            For Each item As String In ComboBox2.Items
                'Wenn der Combobox-Eintrag noch nicht in der Result-List vorhanden ist, Eintrag der Result-List hinzufügen
                If Result.Contains(item) = False Then
                    Result.Add(item)
                End If
            Next
            'In der Result-List sind jetzt alle Einträge einmal vorhanden
            'Combobox leeren
            ComboBox2.Items.Clear()
            'Die Result-List der Combobox hinzufügen
            ComboBox2.Items.AddRange(Result.ToArray)
            ComboBox2.SelectedItem = SubFolder_Value
        Catch ex As Exception
        End Try
    End Sub

    'Public Sub BetaCR(ByVal Auth As String, ByVal Cookie As String)

    '    Try
    '        Using client As New WebClient()
    '            client.Encoding = System.Text.Encoding.UTF8
    '            client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
    '            client.Headers.Add("ACCEPT: gzip")
    '            client.Headers.Add("Cookie: " + Cookie)
    '            client.Headers.Add("Authorization: " + Auth)
    '            client.Headers.Add("Content-Type: application/x-www-form-urlencoded")
    '            client.Headers.Add("Referer: https://beta.crunchyroll.com/") '
    '            Dim reqparm As New Specialized.NameValueCollection

    '            reqparm.Add("grant_type", "etp_rt_cookie")
    '            Dim responsebytes = client.UploadValues("https://beta-api.crunchyroll.com/auth/v1/token", "POST", reqparm)
    '            Dim responsebody = (New Text.UTF8Encoding).GetString(responsebytes)
    '            'My.Computer.Clipboard.SetText(responsebody)
    '        End Using
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try


    'End Sub


    Private Sub Btn_dl_Click(sender As Object, e As EventArgs) Handles btn_dl.Click
        CefSharp_Browser.Show()
        Main.LoginOnly = "Download Mode!"
        'MsgBox(Main.WebbrowserURL)
        If SubTitlesOnlyCB.Text = "[Default]" Then
            Main.SubsOnly = False
        Else
            Main.SubsOnly = True
        End If
        If groupBox1.Visible = True Then

            Try
                If CBool(InStr(textBox1.Text, "crunchyroll.com")) Or CBool(InStr(textBox1.Text, "funimation.com")) Or CBool(InStr(textBox1.Text, "vrv.co/series/")) Or CBool(InStr(textBox1.Text, "vrv.co/watch/")) Then

#Region "Funimation url parameter"
                    If CBool(InStr(textBox1.Text, "funimation.com")) Then
                        Main.WebbrowserURL = textBox1.Text
                        'If CBool(InStr(Main.FunimationAPIRegion, "?region=")) And CBool(InStr(textBox1.Text, "/shows/")) Then

                        'ProcessFunimationJS(textBox1.Text)

                        'Exit Sub
                        'End If

                        If Main.DubFunimation = "Disabled" Then
                        Else
                            If CBool(InStr(textBox1.Text, "?lang=")) Then
                                Dim ClearUri As String() = textBox1.Text.Split(New String() {"?lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                If ClearUri.Count > 1 Then
                                    If CBool(InStr(ClearUri(1), "&")) Then
                                        Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                        Dim Parms As String = Nothing
                                        For i As Integer = 1 To ClearUri2.Count - 1
                                            Parms = Parms + "&" + ClearUri2(i)
                                        Next
                                        textBox1.Text = ClearUri(0) + "?lang=" + Main.DubFunimation + Parms
                                    Else
                                        textBox1.Text = ClearUri(0) + "?lang=" + Main.DubFunimation
                                    End If
                                Else
                                    textBox1.Text = ClearUri(0) + "?lang=" + Main.DubFunimation
                                End If
                            ElseIf CBool(InStr(textBox1.Text, "&lang=")) Then
                                Dim ClearUri As String() = textBox1.Text.Split(New String() {"&lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                If ClearUri.Count > 1 Then

                                    If CBool(InStr(ClearUri(1), "&")) Then
                                        Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                        Dim Parms As String = Nothing
                                        For i As Integer = 1 To ClearUri2.Count - 1
                                            Parms = Parms + "&" + ClearUri2(i)
                                        Next
                                        textBox1.Text = ClearUri(0) + "&lang=" + Main.DubFunimation + Parms
                                    Else
                                        textBox1.Text = ClearUri(0) + "&lang=" + Main.DubFunimation
                                    End If
                                Else
                                    textBox1.Text = ClearUri(0) + "&lang=" + Main.DubFunimation
                                End If

                            ElseIf CBool(InStr(textBox1.Text, "?")) Then
                                textBox1.AppendText("&lang=" + Main.DubFunimation)
                            Else
                                textBox1.AppendText("?lang=" + Main.DubFunimation)
                            End If
                        End If
                        'ElseIf CBool(InStr(textBox1.Text, "vrv.co/series/")) Then



                    End If
#End Region

                    If StatusLabel.Text = "Status: waiting for episode selection" Then
                        If MessageBox.Show("Are you sure you want cancel the advanced download?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            StatusLabel.Text = "Status: idle"
                        Else
                            Exit Sub
                            btn_dl.Enabled = True
                        End If
                        'ElseIf LabelUpdate = "Status: looking for video file" Then
                        '    Exit Sub
                        '    pictureBox4.Enabled = True
                    Else
                        If Main.RunningDownloads >= Main.MaxDL Then
                            ListBox1.Items.Add(textBox1.Text)
                            textBox1.ForeColor = Color.FromArgb(9248044)
                            Pause(1)
                            textBox1.ForeColor = Color.Black
                            textBox1.Text = "URL"
                        Else
                            If Main.Grapp_RDY = True Then

                                Main.b = False
                                Debug.WriteLine("Start loading: " + Date.Now.ToString)
                                CefSharp_Browser.WebBrowser1.Load(textBox1.Text)
                                StatusLabel.Text = "Status: loading ...."

                            End If
                        End If
                    End If

#Region "AoD"
                ElseIf CBool(InStr(textBox1.Text, "anime-on-demand.de")) Then
                    If Main.SubsOnly = True Then
                        MsgBox("Anime on Demand wird nicht im [Subtitles only] modus unterstützt" + vbNewLine + "Normaler Download modus ist aktiv!", MsgBoxStyle.Information)
                        Main.SubsOnly = False
                        SubTitlesOnlyCB.Text = "[Default]"
                    End If
                    Main.b = False

                    CefSharp_Browser.WebBrowser1.Load(textBox1.Text)
#End Region

                ElseIf CBool(InStr(textBox1.Text, "Test=true")) Then
                    CefSharp_Browser.WebBrowser1.Load(textBox1.Text)
                    'Else 'If CBool(InStr(textBox1.Text, "vrv.co")) Then
                ElseIf CBool(InStr(textBox1.Text, "https://")) Then
                    If MessageBox.Show("This in NOT a Crunchyroll URL, try anyway?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Main.b = False
                        CefSharp_Browser.WebBrowser1.Load(textBox1.Text)
                        StatusLabel.Text = "Status: looking for non CR video file"

                    Else
                        Exit Sub
                        btn_dl.Enabled = True
                    End If

                Else
                    MsgBox(Main.URL_Invaild, MsgBoxStyle.OkOnly)
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
                Main.b = True
                MsgBox(Main.URL_Invaild, MsgBoxStyle.OkOnly)
            End Try
        ElseIf groupBox2.Visible = True Then


            If Mass_DL_Cancel = True Then
                Mass_DL_Cancel = False
                GroupBox3.Visible = False
                groupBox2.Visible = False
                Main.Grapp_Abord = True
                Main.b = True
                groupBox1.Visible = True
                btn_dl.BackgroundImage = My.Resources.main_button_download_default
            ElseIf CBool(InStr(Main.WebbrowserURL, "funimation.com")) = True Then


                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                PictureBox1.Enabled = False
                PictureBox1.Visible = False
                Main.DownloadFunimationJS_Seasons()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False

            ElseIf CBool(InStr(Main.WebbrowserURL, "beta.crunchyroll.com")) = True Then

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                PictureBox1.Enabled = False
                PictureBox1.Visible = False

                Main.DownloadBetaSeasons()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False
            ElseIf CBool(InStr(Main.WebbrowserURL, "vrv.co")) = True Then

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                PictureBox1.Enabled = False
                PictureBox1.Visible = False

                Main.Download_VRV_Seasons()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False
            ElseIf AoD_Mode = True Then
                If AoD_DL_running = False Then
                    If comboBox3.SelectedIndex < 0 And comboBox4.SelectedIndex < 0 Then
                        MsgBox("Error nothing selected!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    ElseIf comboBox3.SelectedIndex < 0 Or comboBox4.SelectedIndex < 0 Then
                        'MsgBox("deteced!", MsgBoxStyle.Exclamation)
                        If comboBox3.SelectedIndex < 0 Then
                            'MsgBox("deteced! 3", MsgBoxStyle.Exclamation)
                            Dim CB4 As Integer = comboBox4.SelectedIndex
                            comboBox3.SelectedIndex = CB4
                            comboBox3.SelectedIndex = CB4
                        ElseIf comboBox4.SelectedIndex < 0 Then
                            'MsgBox("deteced! 4", MsgBoxStyle.Exclamation)
                            Dim CB3 As Integer = comboBox3.SelectedIndex
                            comboBox4.SelectedIndex = CB3
                            comboBox4.SelectedIndex = CB3
                        Else
                            MsgBox("Error nothing selected!", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If
                    Else
                        'MsgBox("not deteced!", MsgBoxStyle.Exclamation)
                    End If
                    AoD_DL_running = True
                    ComboBox1.Enabled = False
                    comboBox3.Enabled = False
                    comboBox4.Enabled = False
                    Dim Evaluator = New Thread(Sub() Me.Add_AoD())
                    Evaluator.Start()
                    PictureBox1.Enabled = False
                    PictureBox1.Visible = False
                End If
                'Add_AoD()
            Else

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                PictureBox1.Enabled = False
                PictureBox1.Visible = False
                Main.MassDL()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False
            End If

        ElseIf GroupBox3.Visible = True Then
            GroupBox3.Visible = False
            groupBox2.Visible = False
            groupBox1.Visible = True
            List_DL_Cancel = False
            btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End If

        btn_dl.Enabled = True
    End Sub

    Public Sub ProcessFunimationJS(ByVal InputURL As String)


        Dim FunUri As String = Nothing
        If CBool(InStr(InputURL, "?")) Then
            Dim ClearUri As String() = InputURL.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
            FunUri = ClearUri(0)
        Else
            FunUri = InputURL
        End If
        Dim ShowPath As String = Nothing
        Dim EpisodePath As String = Nothing
        Dim ShowPath1 As String() = FunUri.Split(New String() {"/shows/"}, System.StringSplitOptions.RemoveEmptyEntries)
        'If CBool(InStr(ShowPath1(1), "/") Then
        Dim ShowPath2 As String() = ShowPath1(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)

        If ShowPath2.Count > 1 Then

            ShowPath = ShowPath2(0).Replace("/", "")
            EpisodePath = ShowPath2(1).Replace("/", "")
        Else
            ShowPath = ShowPath1(1).Replace("/", "")
        End If
        Main.FunimationShowPath = ShowPath + "/"
        Debug.WriteLine(ShowPath)
        Debug.WriteLine(Main.FunimationAPIRegion)
        If EpisodePath = Nothing Then 'overview site
            Main.GetFunimationJS_Seasons("https://title-api.prd.funimationsvc.com/v2/shows/" + ShowPath + Main.FunimationAPIRegion)

        Else 'single episode


        End If

        Dim FunimationCC As String() = ShowPath1(0).Split(New String() {"funimation.com"}, System.StringSplitOptions.RemoveEmptyEntries)
        If FunimationCC.Count > 1 Then
            Main.FunimationRegion = FunimationCC(1).Replace("/", "")
        End If
    End Sub

    Public Sub ProcessAoDNew()
        'My.Computer.Clipboard.SetText(Main.WebbrowserText)
        'MsgBox("set")
        AoD_OmUVideoIDList.Clear()
        AoD_DubVideoIDList.Clear()

        If AoD_Cookie = Nothing Then

            MsgBox(Main.LoginReminder)
            Main.Text = "Crunchyroll Downloader"
            Main.Invalidate()
            StatusLabel.Text = "Status: idle"
            Exit Sub

        End If
        Debug.WriteLine("running")
        If Main.WebbrowserText = Nothing Then
            Debug.WriteLine("Main.WebbrowserText true")
        Else
            Debug.WriteLine("Main.WebbrowserText false")
        End If

        Dim Evaluator = New Thread(Sub() Me.AddToList(Main.WebbrowserText))
        Evaluator.Start()
        ThreadList.Add(Evaluator)

        Debug.WriteLine("running2")

    End Sub

    Sub AddToList(ByVal html As String)

        If html = Nothing Then
            Me.Invoke(New Action(Function() As Object
                                     html = Main.WebbrowserText
                                     Return Nothing
                                 End Function))

        End If

        If CBool(InStr(html, "<div class=" + Chr(34) + "flipper" + Chr(34) + ">")) Then
            Dim VideoFlipper_Split() As String = html.Split(New String() {"<div class=" + Chr(34) + "flipper" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            Debug.WriteLine(VideoFlipper_Split.ToString)

            For ii As Integer = 0 To VideoFlipper_Split.Count - 1

                If CBool(InStr(VideoFlipper_Split(ii), "<h3 class=" + Chr(34) + "episodebox-title" + Chr(34) + " title=" + Chr(34))) Then

                    Dim VideoName_Split() As String = VideoFlipper_Split(ii).Split(New String() {"<h3 class=" + Chr(34) + "episodebox-title" + Chr(34) + " title=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim VideoName_Split2() As String = VideoName_Split(1).Split(New String() {Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim VideoName_Split3() As String = VideoName_Split2(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim VideoName As String = VideoName_Split3(0)


                    Dim VideoIDs_Split() As String = VideoFlipper_Split(ii).Split(New String() {"videomaterialurl/"}, System.StringSplitOptions.RemoveEmptyEntries)


                    For i As Integer = 0 To VideoIDs_Split.Count - 1



                        If CBool(InStr(VideoIDs_Split(i), "/OmU/")) Then
                            Dim OmUVideoIDs_Split() As String = VideoIDs_Split(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim VideoStreamUrl As String = "https://www.anime-on-demand.de/videomaterialurl/" + OmUVideoIDs_Split(0)
                            If Not AoD_OmUVideoIDList.Contains(VideoStreamUrl) Then
                                AoD_OmUVideoIDList.Add(VideoStreamUrl)

                                AoD_OmU_Episodes.Add(New AoDEpisodes(VideoName, VideoStreamUrl))
                                Debug.WriteLine("OmU: " + VideoName + " " + VideoStreamUrl)

                            End If

                        ElseIf CBool(InStr(VideoIDs_Split(i), "/Dub/")) Then
                            Dim DubVideoIDs_Split() As String = VideoIDs_Split(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim VideoStreamUrl As String = "https://www.anime-on-demand.de/videomaterialurl/" + DubVideoIDs_Split(0)
                            If Not AoD_DubVideoIDList.Contains(VideoStreamUrl) Then
                                AoD_DubVideoIDList.Add(VideoStreamUrl)
                                AoD_Dub_Episodes.Add(New AoDEpisodes(VideoName, VideoStreamUrl))
                                Debug.WriteLine("Dub: " + VideoName + " " + VideoStreamUrl)
                            End If

                        End If


                    Next
                End If

            Next

        End If
        Me.Invoke(New Action(Function() As Object
                                 If AoD_Dub_Episodes.Count > 0 And AoD_OmU_Episodes.Count > 0 Then
                                     ComboBox1.Items.Clear()
                                     ComboBox1.Text = Nothing
                                     GroupBox3.Visible = False
                                     groupBox2.Visible = True
                                     groupBox1.Visible = False
                                     ComboBox1.Enabled = True
                                     comboBox3.Enabled = False
                                     comboBox4.Enabled = False
                                     ComboBox1.Items.Add("Dub")
                                     ComboBox1.Items.Add("OmU")
                                     AoD_Mode = True
                                 ElseIf AoD_Dub_Episodes.Count > 0 Or AoD_OmU_Episodes.Count > 0 Then
                                     ComboBox1.Items.Clear()
                                     ComboBox1.Text = Nothing
                                     GroupBox3.Visible = False
                                     groupBox2.Visible = True
                                     groupBox1.Visible = False
                                     ComboBox1.Enabled = False
                                     comboBox3.Enabled = True
                                     comboBox4.Enabled = True
                                     FillAoDDropDown()
                                     AoD_Mode = True
                                 End If
                                 Return Nothing
                             End Function))


    End Sub



    Private Sub FillAoDDropDown()
        comboBox3.Items.Clear()
        comboBox4.Items.Clear()
        If AoD_OmU_Episodes.Count > 0 Then
            For i As Integer = 0 To AoD_OmU_Episodes.Count - 1
                comboBox3.Items.Add(AoD_OmU_Episodes.Item(i).Name)
                comboBox4.Items.Add(AoD_OmU_Episodes.Item(i).Name)
            Next
        ElseIf AoD_Dub_Episodes.Count > 0 Then
            For i As Integer = 0 To AoD_Dub_Episodes.Count - 1
                comboBox3.Items.Add(AoD_Dub_Episodes.Item(i).Name)
                comboBox4.Items.Add(AoD_Dub_Episodes.Item(i).Name)
            Next
        End If
    End Sub






#Region "Old AoD"


    Public Sub ProcessAoD()

        AoD_DubList.Clear()
        AoD_OmUList.Clear()

        AoD_OmUVideoIDList.Clear()
        AoD_DubVideoIDList.Clear()

        If AoD_Cookie = Nothing Then

            MsgBox(Main.LoginReminder)
            Main.Text = "Crunchyroll Downloader"
            Main.Invalidate()
            StatusLabel.Text = "Status: idle"
            Exit Sub

        End If

        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\test.log", Main.WebbrowserText, False)

        If CBool(InStr(Main.WebbrowserText, "/OmU/1080/hlsfirst/")) Then
            Dim OmUStreamSplit() As String = Main.WebbrowserText.Split(New String() {"/OmU/1080/hlsfirst/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim OmUStreamSplitToken() As String = OmUStreamSplit(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim OmUStreamSplitEpisodeIndex() As String = OmUStreamSplit(0).Split(New String() {"/videomaterialurl/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim OmUStreamSplitEpisodeIndex2() As String = OmUStreamSplitEpisodeIndex(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim m3u8Strings As String = Nothing

            Dim VideoStreamUrls As String = "https://www.anime-on-demand.de/videomaterialurl/" + OmUStreamSplitEpisodeIndex2(0) + "/OmU/1080/hlsfirst/" + OmUStreamSplitToken(0)
            VideoStreamUrls = VideoStreamUrls.Replace("/single", "")
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                    client.Headers.Add("Accept-Encoding: gzip, deflate, br")
                    client.Headers.Add("X-Requested-With: XMLHttpRequest")
                    client.Headers.Add("Cookie: " + AoD_Cookie) '+ WebBrowser1.Document.Cookie)
                    'MsgBox("https://www.anime-on-demand.de/videomaterialurl/" + OmUStreamSplitEpisodeIndex2(0) + "/OmU/1080/hlsfirst/" + OmUStreamSplitToken(0))

                    m3u8Strings = client.DownloadString(VideoStreamUrls)
                    '("Sub: " + m3u8Strings)
                End Using
            Catch ex As Exception
                MsgBox(ex.ToString + vbNewLine + VideoStreamUrls)
            End Try
            If m3u8Strings = Nothing Then
            Else

                Dim OmUStreams() As String = m3u8Strings.Split(New String() {My.Resources.AoD_files}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 1 To OmUStreams.Count - 1
                    AoD_OmUList.Add(OmUStreams(i))
                Next
            End If

        End If

        If CBool(InStr(Main.WebbrowserText, "/Dub/1080/hlsfirst/")) Then

            Dim DubStreamSplit() As String = Main.WebbrowserText.Split(New String() {"/Dub/1080/hlsfirst/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim DubStreamSplitToken() As String = DubStreamSplit(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim DubStreamSplitEpisodeIndex() As String = DubStreamSplit(0).Split(New String() {"/videomaterialurl/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim DubStreamSplitEpisodeIndex2() As String = DubStreamSplitEpisodeIndex(DubStreamSplitEpisodeIndex.Count - 1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim m3u8Strings As String = Nothing
            Dim VideoStreamUrls As String = "https://www.anime-on-demand.de/videomaterialurl/" + DubStreamSplitEpisodeIndex2(0) + "/Dub/1080/hlsfirst/" + DubStreamSplitToken(0)
            VideoStreamUrls = VideoStreamUrls.Replace("/single", "")
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                    client.Headers.Add("Accept-Encoding: gzip, deflate, br")
                    client.Headers.Add("X-Requested-With: XMLHttpRequest")
                    client.Headers.Add("Cookie: " + AoD_Cookie) '+ WebBrowser1.Document.Cookie)
                    'MsgBox(DubStreamSplitEpisodeIndex(1))
                    m3u8Strings = client.DownloadString(VideoStreamUrls)
                    'MsgBox("Dub: " + m3u8Strings)
                End Using
            Catch ex As Exception
                MsgBox(ex.ToString + vbNewLine + VideoStreamUrls)
            End Try
            If m3u8Strings = Nothing Then
            Else

                Dim DubStreams() As String = m3u8Strings.Split(New String() {My.Resources.AoD_files}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 1 To DubStreams.Count - 1
                    AoD_DubList.Add(DubStreams(i))
                Next
            End If


        End If
        AoD_Mode = True
        'MsgBox(AoD_DubList.Count)
        'MsgBox(AoD_OmUList.Count)

        If AoD_DubList.Count > 0 And AoD_OmUList.Count > 0 Then
            ComboBox1.Items.Clear()
            ComboBox1.Text = Nothing
            GroupBox3.Visible = False
            groupBox2.Visible = True
            groupBox1.Visible = False
            ComboBox1.Enabled = True
            comboBox3.Enabled = False
            comboBox4.Enabled = False
            ComboBox1.Items.Add("Dub")
            ComboBox1.Items.Add("OmU")
            FillAoDDropDownOld()
        ElseIf AoD_DubList.Count > 0 Or AoD_OmUList.Count > 0 Then
            ComboBox1.Items.Clear()
            ComboBox1.Text = Nothing
            GroupBox3.Visible = False
            groupBox2.Visible = True
            groupBox1.Visible = False
            ComboBox1.Enabled = False
            comboBox3.Enabled = True
            comboBox4.Enabled = True
            FillAoDDropDownOld()
        End If
    End Sub
#End Region

    Private Sub Btn_dl_MouseEnter(sender As Object, e As EventArgs) Handles btn_dl.MouseEnter
        If Mass_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        ElseIf List_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert

        Else
            btn_dl.Text = "Download"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        End If

    End Sub

    Private Sub Btn_dl_MouseLeave(sender As Object, e As EventArgs) Handles btn_dl.MouseLeave
        If Mass_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        ElseIf List_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        Else
            btn_dl.Text = "Download"
            btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End If

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles textBox1.Click
        If textBox1.Text = "URL" Then
            textBox1.Text = Nothing
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        groupBox1.Visible = True
        groupBox2.Visible = False
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If CBool(InStr(Main.WebbrowserURL, "beta.crunchyroll.com")) = True Then
            comboBox3.Items.Clear()
            comboBox4.Items.Clear()
            comboBox3.Enabled = True
            comboBox4.Enabled = True
            comboBox3.Text = Nothing
            comboBox4.Text = Nothing
            Dim SeasonSplit() As String = Main.CrBetaMass.Split(New String() {Chr(34) + "id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim SeasonSplit2() As String = SeasonSplit(ComboBox1.SelectedIndex + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim EpisodeJsonURL As String = Main.CrBetaMassBaseURL + "episodes?season_id=" + SeasonSplit2(0) + "&locale=" + Main.CrBetaMassParameters
            Dim EpisodeJson As String = Nothing
            Debug.WriteLine(EpisodeJsonURL)

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    EpisodeJson = client.DownloadString(EpisodeJsonURL)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting EpisodeJson data")
                Debug.WriteLine(ex.ToString)
                Exit Sub
            End Try
            Main.CrBetaMassEpisodes = EpisodeJson



            Dim EpisodeNameSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)


            Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "episode" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To EpisodeSplit.Count - 1
                Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim EpisodeNameSplit2() As String = EpisodeNameSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                If EpisodeSplit(i).Substring(0, 1) = Chr(34) Then
                    comboBox3.Items.Add(EpisodeNameSplit2(0))
                    comboBox4.Items.Add(EpisodeNameSplit2(0))
                Else
                    comboBox3.Items.Add("Episode " + EpisodeSplit2(0))
                    comboBox4.Items.Add("Episode " + EpisodeSplit2(0))
                End If

            Next
        ElseIf CBool(InStr(Main.WebbrowserURL, "vrv.co")) = True Then
            comboBox3.Items.Clear()
            comboBox4.Items.Clear()
            comboBox3.Enabled = True
            comboBox4.Enabled = True
            comboBox3.Text = Nothing
            comboBox4.Text = Nothing
            Dim SeasonSplit() As String = Main.VRVMass.Split(New String() {Chr(34) + "id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim SeasonSplit2() As String = SeasonSplit(ComboBox1.SelectedIndex + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim EpisodeJsonURL As String = Main.VRVMassBaseURL + "episodes?season_id=" + SeasonSplit2(0) + "&Policy=" + Main.VRVMassParameters
            Dim EpisodeJson As String = Nothing
            Debug.WriteLine(EpisodeJsonURL)

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    EpisodeJson = client.DownloadString(EpisodeJsonURL)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting EpisodeJson data")
                Debug.WriteLine(ex.ToString)
                Exit Sub
            End Try
            Main.VRVMassEpisodes = EpisodeJson



            Dim EpisodeNameSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)


            Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "episode" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To EpisodeSplit.Count - 1
                Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim EpisodeNameSplit2() As String = EpisodeNameSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                If EpisodeSplit(i).Substring(0, 1) = Chr(34) Then
                    comboBox3.Items.Add(EpisodeNameSplit2(0))
                    comboBox4.Items.Add(EpisodeNameSplit2(0))
                Else
                    comboBox3.Items.Add("Episode " + EpisodeSplit2(0))
                    comboBox4.Items.Add("Episode " + EpisodeSplit2(0))
                End If

            Next
        ElseIf Main.WebbrowserURL = "https://funimation.com/js" Then
            comboBox3.Items.Clear()
            comboBox4.Items.Clear()
            comboBox3.Text = Nothing
            comboBox4.Text = Nothing



            Dim EpisodeJsonURL As String = "https://title-api.prd.funimationsvc.com/v1/seasons/" + Main.FunimtaionAPISeasonID.Item(ComboBox1.SelectedIndex) + Main.FunimationAPIRegion
            Dim EpisodeJson As String = Nothing
            Debug.WriteLine(EpisodeJsonURL)

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    EpisodeJson = client.DownloadString(EpisodeJsonURL)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting EpisodeJson data")
                Debug.WriteLine(ex.ToString)
                Main.FunimationJsonBrowser = "EpisodeJson"
                CefSharp_Browser.WebBrowser1.Load(EpisodeJsonURL)
                Exit Sub
            End Try



            FillFunimationEpisodes(EpisodeJson)


        ElseIf AoD_Mode = False Then

            'MsgBox(Main.WebbrowserURL)
            comboBox3.Items.Clear()
            comboBox4.Items.Clear()
            comboBox3.Enabled = True
            comboBox4.Enabled = True
            'comboBox3.Items.Add("[First Episode]")
            'comboBox4.Items.Add("[Last Episode]")
            Dim SeasonDropdownAnzahl As String() = Main.WebbrowserText.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(SeasonDropdownAnzahl)
            Dim SDV As Integer = 0
            For i As Integer = 0 To SeasonDropdownAnzahl.Count - 1
                If CBool(InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + ComboBox1.SelectedItem.ToString + "</a>")) Then
                    SDV = i
                End If
            Next
            'MsgBox(SDV)
            Dim Anzahl As String() = SeasonDropdownAnzahl(SDV).Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(Anzahl(0))
            Dim c As Integer = Anzahl.Count - 1
            Array.Reverse(Anzahl)
            For i As Integer = 0 To Anzahl.Count - 2
                Dim URLGrapp As String() = Anzahl(i).Split(New String() {"title=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                comboBox3.Items.Add(URLGrapp2(0))
                comboBox4.Items.Add(URLGrapp2(0))
            Next
            'comboBox3.SelectedIndex = 0
            'comboBox4.SelectedIndex = 0
        ElseIf AoD_Mode = True Then
            comboBox3.Items.Clear()
            comboBox4.Items.Clear()
            comboBox3.Enabled = True
            comboBox4.Enabled = True

            If ComboBox1.Text = "OmU" Then

                For i As Integer = 0 To AoD_OmU_Episodes.Count - 1
                    comboBox3.Items.Add(AoD_OmU_Episodes.Item(i).Name)
                    comboBox4.Items.Add(AoD_OmU_Episodes.Item(i).Name)

                Next

            ElseIf ComboBox1.Text = "Dub" Then

                For i As Integer = 0 To AoD_Dub_Episodes.Count - 1
                    comboBox3.Items.Add(AoD_Dub_Episodes.Item(i).Name)
                    comboBox4.Items.Add(AoD_Dub_Episodes.Item(i).Name)

                Next

            End If

        End If
    End Sub


    Public Sub FillFunimationEpisodes(ByVal EpisodeJson As String)

        Main.FunimationEpisodeJSON = EpisodeJson


        comboBox3.Enabled = True
        comboBox4.Enabled = True

        Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "episodeNumber" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 1 To EpisodeSplit.Count - 1
            Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            comboBox3.Items.Add("Episode " + EpisodeSplit2(0))
            comboBox4.Items.Add("Episode " + EpisodeSplit2(0))
        Next

    End Sub
    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.add_mass_cancel_hover
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.add_mass_cancel
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ListBox1.Items.Count > 0 Then
            If StatusLabel.Text = "Status: idle" Then
                StatusLabel.Cursor = Cursors.Hand
                StatusLabel.Text = "Status: items in queue, click me to work off."
            End If
        Else
            StatusLabel.Cursor = Cursors.Default
        End If
    End Sub


#Region "Listbox"

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If GroupBox3.Visible = True Then
            If ListBox1.Items.Count = 0 Then
                GroupBox3.Visible = False
                groupBox2.Visible = False
                groupBox1.Visible = True
                List_DL_Cancel = False
                btn_dl.BackgroundImage = My.Resources.main_button_download_default
            End If
        End If
        Try
            Dim ItemFinshedCount As Integer = 0
            For i As Integer = 0 To Main.ListView1.Items.Count - 1
                If Main.ItemList(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next
            Main.RunningDownloads = Main.ListView1.Items.Count - ItemFinshedCount

        Catch ex As Exception
            Main.RunningDownloads = Main.ListView1.Items.Count
        End Try

        If Main.RunningDownloads < Main.MaxDL Then
            If ListBox1.Items.Count > 0 Then
                If GroupBox3.Visible = True Then
                    If CBool(InStr(ListBox1.GetItemText(ListBox1.Items(0)), "funimation.com")) Then
                        If Main.Funimation_Grapp_RDY = True Then

                            Dim UriUsed As String = ListBox1.GetItemText(ListBox1.Items(0))
                            If Main.DubFunimation = "Disabled" Then
                            Else
                                If CBool(InStr(UriUsed, "?lang=")) Then
                                    Dim ClearUri As String() = UriUsed.Split(New String() {"?lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then
                                        If CBool(InStr(ClearUri(1), "&")) Then
                                            Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim Parms As String = Nothing
                                            For i As Integer = 1 To ClearUri2.Count - 1
                                                Parms = Parms + "&" + ClearUri2(i)
                                            Next
                                            UriUsed = ClearUri(0) + "?lang=" + Main.DubFunimation + Parms
                                        Else
                                            UriUsed = ClearUri(0) + "?lang=" + Main.DubFunimation
                                        End If
                                    Else
                                        UriUsed = ClearUri(0) + "?lang=" + Main.DubFunimation
                                    End If
                                ElseIf CBool(InStr(UriUsed, "&lang=")) Then
                                    Dim ClearUri As String() = UriUsed.Split(New String() {"&lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then

                                        If CBool(InStr(ClearUri(1), "&")) Then
                                            Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim Parms As String = Nothing
                                            For i As Integer = 1 To ClearUri2.Count - 1
                                                Parms = Parms + "&" + ClearUri2(i)
                                            Next
                                            UriUsed = ClearUri(0) + "&lang=" + Main.DubFunimation + Parms
                                        Else
                                            UriUsed = ClearUri(0) + "&lang=" + Main.DubFunimation
                                        End If
                                    Else
                                        UriUsed = ClearUri(0) + "&lang=" + Main.DubFunimation
                                    End If

                                ElseIf CBool(InStr(UriUsed, "?")) Then
                                    UriUsed = UriUsed + "&lang=" + Main.DubFunimation
                                Else
                                    UriUsed = UriUsed + "?lang=" + Main.DubFunimation
                                End If
                            End If

                            Main.Funimation_Grapp_RDY = False
                            Main.WebbrowserURL = UriUsed
                            'MsgBox(UriUsed)
                            ListBox1.Items.Remove(ListBox1.Items(0))
                            'Main.b = False


                            Main.b = False
                            CefSharp_Browser.WebBrowser1.Load(UriUsed)
                            StatusLabel.Text = "Status: loading in browser"
                            Main.Text = "Status: loading in browser"

                            Main.Invalidate()
                        End If

                    Else
                        If Main.Grapp_RDY = True Then
                            Main.Grapp_RDY = False
                            CefSharp_Browser.WebBrowser1.Load(ListBox1.GetItemText(ListBox1.Items(0)))
                            ListBox1.Items.Remove(ListBox1.Items(0))
                            Main.b = False
                            StatusLabel.Text = "Status: loading ..."
                            Main.Text = "Status: loading ..."
                            Main.Invalidate()

                        End If
                    End If
                End If


            End If
        End If

    End Sub
    Private Sub StatusLabel_Click(sender As Object, e As EventArgs) Handles StatusLabel.Click
        If StatusLabel.Text = "Status: items in queue, click me to work off." Then
            groupBox1.Visible = False
            groupBox2.Visible = False
            GroupBox3.Visible = True
            btn_dl.Text = "Cancel"
            List_DL_Cancel = True
        End If

    End Sub



    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles textBox2.Click
        If textBox2.Text = "Use Custom Name" Then
            textBox2.Text = Nothing

        End If
    End Sub


    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub




#End Region

    Private Sub FillAoDDropDownOld()
        comboBox3.Items.Clear()
        comboBox4.Items.Clear()
        If AoD_OmUList.Count > 0 Then
            For i As Integer = 0 To AoD_OmUList.Count - 1
                Dim DropDownTitle As String() = AoD_OmUList(i).Split(New String() {My.Resources.AoD_Titel}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim DropDownTitle2 As String() = DropDownTitle(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Title As String = DropDownTitle2(0)
                Title = Title.Replace("&amp;", "&").Replace("&amp", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                Title = System.Text.RegularExpressions.Regex.Replace(Title, "[^\w\\-]", " ").Trim(CType(" ", Char()))
                Title = Main.RemoveExtraSpaces(Title)
                comboBox3.Items.Add(Title)
                comboBox4.Items.Add(Title)
            Next
        ElseIf AoD_DubList.Count > 0 Then
            For i As Integer = 0 To AoD_DubList.Count - 1
                Dim DropDownTitle As String() = AoD_DubList(i).Split(New String() {My.Resources.AoD_Titel}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim DropDownTitle2 As String() = DropDownTitle(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Title As String = DropDownTitle2(0)
                Title = Title.Replace("&amp;", "&").Replace("&amp", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                Title = System.Text.RegularExpressions.Regex.Replace(Title, "[^\w\\-]", " ").Trim(CType(" ", Char()))
                Title = Main.RemoveExtraSpaces(Title)
                comboBox3.Items.Add(Title)
                comboBox4.Items.Add(Title)
            Next
        End If
    End Sub

    Public Sub Add_AoD()
        Dim ProcessList As List(Of AoDEpisodes) = AoD_OmU_Episodes
        Dim Dub As Boolean = False
        Dim RDY As Boolean = True
        Dim VideoFormat As String = Main.VideoFormat
        Dim ffmpeg As String = Main.ffmpeg_command
        Dim Running As Integer = Main.RunningDownloads
        Dim DlMax As Integer = Main.MaxDL
        Dim Pfad0 As String = Main.Pfad
        Dim Pfad2 As String = Main.Pfad
        Dim NameMethode As Integer = Main.CR_NameMethode
        Dim c As Integer = 0
        Dim SubExit As Boolean = False
        Dim CB3 As Integer = 0
        Dim CB4 As Integer = 0
        Dim TargetReso As String = Main.Reso.ToString
        Dim AoD_1080pPlus As Boolean = False
        Dim AoDTempReso As String = "6666x6666"

        Dim AoD_Season As String = Nothing
        Dim AoD_Anime_Title As String = Nothing
        Dim AoD_Episode_Title As String = Nothing
        Dim AoD_Episode_Number As String = Nothing

        Me.Invoke(New Action(Function() As Object
                                 'Main.StatusMainForm.Text = "Crunchyroll Downloader"
                                 VideoFormat = Main.VideoFormat
                                 ffmpeg = Main.ffmpeg_command
                                 Pfad2 = Main.Pfad
                                 NameMethode = Main.CR_NameMethode
                                 If Main.AoD_Reso = 0 Then
                                     TargetReso = Main.Reso.ToString
                                 ElseIf Main.AoD_Reso = 576 Then
                                     TargetReso = "576"
                                 ElseIf Main.AoD_Reso = 1080 Then
                                     AoD_1080pPlus = True
                                     TargetReso = "1080"
                                 End If

                                 If ComboBox1.Text = "Dub" Then
                                     ProcessList = AoD_Dub_Episodes

                                 ElseIf ComboBox1.Text = "OmU" Then
                                     ProcessList = AoD_OmU_Episodes

                                 ElseIf ComboBox1.Enabled = False Then

                                     If AoD_Dub_Episodes.Count > 0 Then
                                         ProcessList = AoD_Dub_Episodes
                                         Dub = True

                                     ElseIf AoD_OmU_Episodes.Count > 0 Then
                                         ProcessList = AoD_OmU_Episodes

                                     Else
                                         MsgBox("error 1")
                                         SubExit = True
                                     End If

                                 Else
                                     MsgBox("error 2")
                                     SubExit = True


                                 End If


                                 If comboBox4.SelectedIndex > comboBox3.SelectedIndex Or comboBox4.SelectedIndex = comboBox3.SelectedIndex Then
                                     c = comboBox4.SelectedIndex - comboBox3.SelectedIndex + 1
                                 Else
                                     Dim TempCB3 As Integer = comboBox3.SelectedIndex
                                     Dim TempCB4 As Integer = comboBox4.SelectedIndex
                                     comboBox3.SelectedIndex = TempCB4
                                     comboBox4.SelectedIndex = TempCB3
                                     c = comboBox4.SelectedIndex - comboBox3.SelectedIndex + 1
                                 End If

                                 'MsgBox("00")

                                 CB3 = comboBox3.SelectedIndex
                                 CB4 = comboBox4.SelectedIndex
                                 Return Nothing
                             End Function))
        If SubExit = True Then
            Exit Sub
        End If




        For i As Integer = CB3 To CB4
            Dim ii As Integer = i

            Dim m3u8Strings As String = Nothing
            Dim EpisodePC As String = Nothing
            Dim VideoStreamUrl As String = ProcessList.Item(i).Url

            Try
                Using m3u8client As New WebClient()
                    m3u8client.Encoding = System.Text.Encoding.UTF8
                    m3u8client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    m3u8client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                    m3u8client.Headers.Add("Accept-Encoding: gzip, deflate, br")
                    m3u8client.Headers.Add("X-Requested-With: XMLHttpRequest")
                    m3u8client.Headers.Add("Cookie: " + AoD_Cookie) '+ WebBrowser1.Document.Cookie)
                    'MsgBox("https://www.anime-on-demand.de/videomaterialurl/" + OmUStreamSplitEpisodeIndex2(0) + "/OmU/1080/hlsfirst/" + OmUStreamSplitToken(0))

                    m3u8Strings = m3u8client.DownloadString(VideoStreamUrl)
                    '("Sub: " + m3u8Strings)
                End Using
            Catch ex As Exception
                Me.Invoke(New Action(Function() As Object
                                         MsgBox(ex.ToString + vbNewLine + VideoStreamUrl)
                                         Return Nothing
                                     End Function))
            End Try
            If m3u8Strings = Nothing Then
            Else

                Dim Streams() As String = m3u8Strings.Split(New String() {My.Resources.AoD_files}, System.StringSplitOptions.RemoveEmptyEntries)
                EpisodePC = Streams(1)
            End If



            If Mass_DL_Cancel = True Then
                Exit For
            End If
            For e As Integer = 0 To Integer.MaxValue
                Thread.Sleep(2000)

                If RDY = True Then
                    Try
                        Me.Invoke(New Action(Function() As Object
                                                 Running = Main.RunningDownloads
                                                 DlMax = Main.MaxDL
                                                 Return Nothing
                                             End Function))
                    Catch ex As Exception
                        Exit Sub
                    End Try
                    If DlMax > Running Then
                        RDY = False

                        Exit For

                    End If
                End If

            Next

            Me.Invoke(New Action(Function() As Object
                                     Running = Main.RunningDownloads
                                     DlMax = Main.MaxDL
                                     Dim d As Integer = ii - CB3 + 1
                                     Add_Display.Text = d.ToString + " / " + c.ToString
                                     Main.Text = "Status: " + d.ToString + " / " + c.ToString '  looking for video file"
                                     Main.Invalidate()
                                     Return Nothing
                                 End Function))

            Dim AoDTitle1() As String = EpisodePC.Split(New String() {My.Resources.AoD_Titel}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDTitle2() As String = AoDTitle1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDTitle As String = AoDTitle2(0)

            Dim AoDMediaID1() As String = EpisodePC.Split(New String() {My.Resources.AoD_MediaID}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDMediaID2() As String = AoDMediaID1(1).Split(New String() {"},"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDMediaID As String = AoDMediaID2(0)
            Try


                If CBool(InStr(AoDHTML, My.Resources.AoD_HTML_Episode_Title)) Then ' Serie 

                    Dim AoDTitleDivByMediaID() As String = AoDHTML.Split(New String() {AoDMediaID}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDTitle0() As String = AoDTitleDivByMediaID(1).Split(New String() {My.Resources.AoD_HTML_Episode_Title}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDTitle00() As String = AoDTitle0(ii + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoD_EpisodeSplit() As String = AoDTitle00(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)
                    If AoD_EpisodeSplit.Count > 2 Then
                        AoD_Episode_Title = Nothing
                        For i3 As Integer = 1 To AoD_EpisodeSplit.Count - 1
                            If AoD_Episode_Title = Nothing Then
                                AoD_Episode_Title = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(i3), "[^\w\\-]", " ").Trim(CType(" ", Char()))
                            Else
                                AoD_Episode_Title = AoD_Episode_Title + " - " + System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(i3), "[^\w\\-]", " ").Trim(CType(" ", Char()))

                            End If
                        Next
                        AoD_Episode_Number = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(0), "[^\w\\-]", " ").Trim(CType(" ", Char()))
                    Else
                        AoD_Episode_Number = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(0), "[^\w\\-]", " ").Trim(CType(" ", Char()))
                        AoD_Episode_Title = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(1), "[^\w\\-]", " ").Trim(CType(" ", Char()))

                    End If


                    Dim AoDTitle3() As String = AoDHTML.Split(New String() {My.Resources.AoD_HTML_Anime_Title}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDTitle4() As String = AoDTitle3(1).Split(New String() {"</h1>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If CBool(InStr(AoDTitle4(0), " - ")) Then
                        Dim AoD_Anime_Season_split() As String = AoDTitle4(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)
                        AoD_Anime_Title = AoD_Anime_Season_split(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoD_Anime_Title, "[^\w\\-]", " ").Trim(CType(" ", Char()))
                        AoD_Season = System.Text.RegularExpressions.Regex.Replace(AoD_Anime_Season_split(1), "[^\w\\-]", " ").Trim(CType(" ", Char()))
                    Else
                        AoD_Anime_Title = AoD_Anime_Title.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoDTitle4(0), "[^\w\\-]", " ").Trim(CType(" ", Char()))

                    End If


                Else 'keine Serie aka Film 

                    Dim AoDMovie1() As String = AoDHTML.Split(New String() {My.Resources.AoD_HTML_Anime_Title}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDMovie2() As String = AoDMovie1(1).Split(New String() {"</h1>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If CBool(InStr(AoDMovie2(0), " - ")) Then

                        Dim AoDMovie_split() As String = AoDMovie2(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)
                        AoD_Anime_Title = AoDMovie_split(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoD_Anime_Title, "[^\w\\-]", " ").Trim(CType(" ", Char()))
                        AoD_Episode_Number = System.Text.RegularExpressions.Regex.Replace(AoDMovie_split(1), "[^\w\\-]", " ").Trim(CType(" ", Char()))
                        AoD_Episode_Title = System.Text.RegularExpressions.Regex.Replace(AoDMovie_split(1), "[^\w\\-]", " ").Trim(CType(" ", Char()))
                    Else
                        AoD_Anime_Title = AoDMovie2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoD_Anime_Title, "[^\w\\-]", " ").Trim(CType(" ", Char()))

                    End If




                End If

                If NameMethode = 0 Then
                    If AoD_Season = Nothing Then
                        AoDTitle = AoD_Anime_Title + " " + AoD_Episode_Number
                    Else
                        AoDTitle = AoD_Anime_Title + " " + AoD_Season + " " + AoD_Episode_Number
                    End If

                ElseIf NameMethode = 1 Then
                    If AoD_Season = Nothing Then
                        AoDTitle = AoD_Anime_Title + " " + AoD_Episode_Title
                    Else
                        AoDTitle = AoD_Anime_Title + " " + AoD_Season + " " + AoD_Episode_Title
                    End If

                ElseIf NameMethode = 2 Then
                    If AoD_Season = Nothing Then
                        AoDTitle = AoD_Anime_Title + " " + AoD_Episode_Number + " " + AoD_Episode_Title
                    Else
                        AoDTitle = AoD_Anime_Title + " " + AoD_Season + " " + AoD_Episode_Number + " " + AoD_Episode_Title
                    End If

                End If

            Catch ex As Exception

            End Try


            AoDTitle = AoDTitle.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
            AoDTitle = System.Text.RegularExpressions.Regex.Replace(AoDTitle, "[^\w\\-]", " ").Trim(CType(" ", Char()))

            AoDTitle = Main.RemoveExtraSpaces(AoDTitle)

            Pfad2 = UseSubfolder(AoD_Anime_Title, AoD_Season, Pfad2)

            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = Pfad0
                End Try
            End If



            Dim DownloadPfad As String = Chr(34) + Pfad2 + "\" + AoDTitle + VideoFormat + Chr(34)

#Region "lösche doppel download"

            Dim Pfad5 As String = DownloadPfad.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                Me.Invoke(New Action(Function() As Object
                                         Main.Text = "Status: File already exists."
                                         Main.Invalidate()
                                         Return Nothing
                                     End Function))

                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Me.Invoke(New Action(Function() As Object
                                                 Main.Text = "Status: Old file overwritten."
                                                 Main.Invalidate()
                                                 Return Nothing
                                             End Function))

                    Catch ex As Exception
                    End Try
                Else
                    Me.Invoke(New Action(Function() As Object
                                             Main.Text = "Crunchyroll Downloader"
                                             Return Nothing
                                         End Function))

                    Continue For
                    Exit Sub
                End If

            End If
#End Region
            Dim AoDThumbnail1() As String = EpisodePC.Split(New String() {My.Resources.AoD_Image}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDThumbnail2() As String = AoDThumbnail1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDThumbnail As String = AoDThumbnail2(0)


            Dim AoDTm3u8() As String = EpisodePC.Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim m3u8_Master_url As String = AoDTm3u8(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
            Dim m3u8_list As New List(Of String)
            Dim m3u8_url As String = Nothing
            Dim m3u8_url_Temp As String = Nothing

            Dim client As New WebClient
            client.Encoding = System.Text.Encoding.UTF8
            Dim text As String = client.DownloadString(m3u8_Master_url)
            'Me.Invoke(New Action(Function() As Object
            '                         MsgBox(m3u8_Master_url)
            '                         Return Nothing
            '                     End Function))
            'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\Test.txt", text, False)

            If CBool(InStr(text, "RESOLUTION=")) Then 'master m3u8 no fragments 
                Dim new_m3u8() As String = text.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                If TargetReso = "42" Then
                    m3u8_url = m3u8_Master_url
                End If

                For i2 As Integer = 0 To new_m3u8.Count - 1

                    'MsgBox("x" + Main.Resu.ToString)
                    If CBool(InStr(new_m3u8(i2), "x" + TargetReso.ToString)) = True Then
                        m3u8_list.Add(new_m3u8(i2) + vbCrLf + new_m3u8(i2 + 1))
                        'm3u8_url_Temp = new_m3u8(i2 + 1)
                        'Exit For
                    ElseIf CBool(InStr(new_m3u8(i2), "x1081")) = True Then
                        If AoD_1080pPlus = True Then
                            'Me.Invoke(New Action(Function() As Object
                            '                         MsgBox(new_m3u8(i2 + 1))
                            '                         Return Nothing
                            '                     End Function))
                            m3u8_list.Add(new_m3u8(i2) + vbCrLf + new_m3u8(i2 + 1))
                        End If
                    ElseIf CBool(InStr(new_m3u8(i2), AoDTempReso)) = True Then
                        m3u8_list.Add(new_m3u8(i2) + vbCrLf + new_m3u8(i2 + 1))
                    End If

                Next
                'Me.Invoke(New Action(Function() As Object
                '                         'MsgBox(m3u8_list.Count.ToString)
                '                         Return Nothing
                '                     End Function))
                If m3u8_list.Count > 1 Then
                    Dim HigestBitrate As Integer = 0
                    For i2 As Integer = 0 To m3u8_list.Count - 1
                        'MsgBox("x" + Main.Resu.ToString)
                        If CBool(InStr(m3u8_list.Item(i2), "AVERAGE-BANDWIDTH=")) = True Then
                            Dim BitRate() As String = m3u8_list.Item(i2).Split(New String() {"AVERAGE-BANDWIDTH="}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim BitRate2() As String = BitRate(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                            If AoD_1080pPlus = True Then
                                If CInt(BitRate2(0)) > HigestBitrate Then
                                    HigestBitrate = CInt(BitRate2(0))
                                End If
                            Else
                                'Me.Invoke(New Action(Function() As Object
                                '                         'MsgBox(HigestBitrate.ToString + vbNewLine + BitRate2(0))
                                '                         Return Nothing
                                '                     End Function))
                                If HigestBitrate > CInt(BitRate2(0)) Then
                                    HigestBitrate = CInt(BitRate2(0))
                                ElseIf HigestBitrate = 0 Then
                                    HigestBitrate = CInt(BitRate2(0))
                                End If
                            End If
                        ElseIf CBool(InStr(m3u8_list.Item(i2), "BANDWIDTH=")) = True Then
                            Dim BitRate() As String = m3u8_list.Item(i2).Split(New String() {"BANDWIDTH="}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim BitRate2() As String = BitRate(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                            If AoD_1080pPlus = True Then
                                If CInt(BitRate2(0)) > HigestBitrate Then
                                    HigestBitrate = CInt(BitRate2(0))
                                End If
                            Else
                                'Me.Invoke(New Action(Function() As Object
                                '                         'MsgBox(HigestBitrate.ToString + vbNewLine + BitRate2(0))
                                '                         Return Nothing
                                '                     End Function))
                                If HigestBitrate > CInt(BitRate2(0)) Then
                                    HigestBitrate = CInt(BitRate2(0))
                                ElseIf HigestBitrate = 0 Then
                                    HigestBitrate = CInt(BitRate2(0))
                                End If
                            End If

                        End If
                    Next
                    'Me.Invoke(New Action(Function() As Object
                    '                         MsgBox(HigestBitrate.ToString)
                    '                         Return Nothing
                    '                     End Function))
                    For i2 As Integer = 0 To m3u8_list.Count - 1
                        If CBool(InStr(m3u8_list.Item(i2), HigestBitrate.ToString)) = True Then
                            Dim new_m3u8_2() As String = m3u8_list.Item(i2).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                            m3u8_url_Temp = new_m3u8_2(1)
                        End If
                    Next
                ElseIf m3u8_list.Count = 1 Then
                    Dim new_m3u8_2() As String = m3u8_list.Item(0).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                    m3u8_url_Temp = new_m3u8_2(1)
                Else

                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Resolution not found!"
                                             Me.Invalidate()
                                             Main.DialogTaskString = "AoD_Resolution"
                                             Main.ResoNotFoundString = text
                                             ErrorDialog.ShowDialog()
                                             AoDTempReso = Main.ResoBackString
                                             Return Nothing
                                         End Function))



                    Dim m3u8BackupReso() As String = text.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)

                    For i2 As Integer = 0 To m3u8BackupReso.Count - 1
                        Dim ii2 As Integer = i2
                        If CBool(InStr(m3u8BackupReso(i2), AoDTempReso)) = True Then

                            m3u8_url_Temp = m3u8BackupReso(ii2 + 1)
                        End If
                    Next

                End If

                If CBool(InStr(m3u8_url_Temp, "https://")) Then
                    m3u8_url = m3u8_url_Temp
                Else
                    Dim d() As String = New Uri(m3u8_Master_url).Segments
                    Dim path As String = "https://" + New Uri(m3u8_Master_url).Host
                    For i3 As Integer = 0 To d.Count - 2
                        path = path + d(i3)
                    Next
                    m3u8_url = path + m3u8_url_Temp
                    'MsgBox(m3u8_url_1)

                End If

            End If


            Dim AoDm3u8Final As String = "-i " + Chr(34) + m3u8_url + Chr(34) + " " + ffmpeg
            Dim DisplayReso As String = TargetReso.ToString + "p"
            If AoD_1080pPlus = True Then
                DisplayReso = "1080p+"
            End If
            If AoDTempReso = "6666x6666" Then
            Else
                Dim ResoSplit() As String = AoDTempReso.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                DisplayReso = ResoSplit(1) + "p"
            End If

            Dim L1Name As String = "anime-on-demand.de" 'L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Me.Invoke(New Action(Function() As Object
                                     Main.ListItemAdd(Pfad2, L1Name, AoDTitle, DisplayReso, "Unknown", "None", AoDThumbnail, AoDm3u8Final, DownloadPfad, "AoD")
                                     Main.liList.Add(My.Resources.htmlvorThumbnail + AoDThumbnail + My.Resources.htmlnachTumbnail + "<br>" + AoDTitle + My.Resources.htmlvorAufloesung + "[Auto]" + My.Resources.htmlvorSoftSubs + vbNewLine + "None" + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + AoDTitle + "-->")

                                     Return Nothing
                                 End Function))




            RDY = True

        Next
    End Sub

    Private Sub Anime_Add_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Btn_Close.Location = New Point(Me.Width - 40, 1)
        Btn_min.Location = New Point(Me.Width - 68, 10)
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Main.ListBoxList.Clear()
        If ListBox1.Items.Count > 0 Then
            For i As Integer = 0 To ListBox1.Items.Count - 1
                Main.ListBoxList.Add(ListBox1.Items.Item(i).ToString)
            Next
        End If
        Me.Close()
    End Sub

    Private Sub Btn_min_MouseEnter(sender As Object, e As EventArgs) Handles Btn_min.MouseEnter

        Btn_min.Image = My.Resources.main_mini_red
    End Sub

    Private Sub Btn_min_MouseLeave(sender As Object, e As EventArgs) Handles Btn_min.MouseLeave

        Btn_min.Image = Main.MinImg
    End Sub
    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter

        Btn_Close.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave

        Btn_Close.Image = Main.CloseImg
    End Sub

    Private Sub TextBox4_Click(sender As Object, e As EventArgs) Handles TextBox4.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            ComboBox2.Items.Clear()
            Main.Pfad = FolderBrowserDialog1.SelectedPath
            Dim rk0 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
            rk0.SetValue("Ordner", Main.Pfad, RegistryValueKind.String)

            ComboBox2.Items.Add(SubFolder_automatic)
            ComboBox2.Items.Add(SubFolder_automatic2)
            ComboBox2.Items.Add(SubFolder_Nothing)
            ComboBox2.SelectedItem = SubFolder_Nothing
            TextBox4.Text = Main.Pfad
            Try
                Dim di As New System.IO.DirectoryInfo(Main.Pfad)
                For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                    If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                    Else
                        ComboBox2.Items.Add(fi.Name)
                    End If
                Next
                Dim Result As New List(Of String)
                'Jeder Eintrag in der Combobox durchgehen
                For Each item As String In ComboBox2.Items
                    'Wenn der Combobox-Eintrag noch nicht in der Result-List vorhanden ist, Eintrag der Result-List hinzufügen
                    If Result.Contains(item) = False Then
                        Result.Add(item)
                    End If
                Next
                'In der Result-List sind jetzt alle Einträge einmal vorhanden
                'Combobox leeren
                'ComboBox2.Items.Clear()
                'Die Result-List der Combobox hinzufügen
                'ComboBox2.Items.AddRange(Result.ToArray)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SubTitlesOnlyCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SubTitlesOnlyCB.SelectedIndexChanged
        If SubTitlesOnlyCB.Text = "[Default]" Then
            Main.SubsOnly = False
        Else
            Main.SubsOnly = True
        End If
    End Sub




    Private Sub GroupBox1_VisibleChanged(sender As Object, e As EventArgs) Handles groupBox1.VisibleChanged
        If Not textBox2.Text = "Use Custom Name" Then
            textBox2.Text = "Use Custom Name"
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            For tlc As Integer = 0 To ThreadList.Count - 1
                If ThreadList.Item(tlc).IsAlive Then
                Else
                    ThreadList.Remove(ThreadList.Item(tlc))
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub


End Class

Public Class AoDEpisodes
    Public Name As String
    Public Url As String

    Public Sub New(ByVal Name As String, ByVal Url As String)
        Me.Name = Name
        Me.Url = Url
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Name, Me.Url)
    End Function


End Class