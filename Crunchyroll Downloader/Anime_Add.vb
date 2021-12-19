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

    Public ThreadList As New List(Of Thread)






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

                        If CBool(InStr(textBox1.Text, "funimation.com/v/")) Then
                            Dim Episode() As String = textBox1.Text.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
                            Dim v1Json As String = Nothing
                            Try
                                Using client As New WebClient()
                                    client.Encoding = System.Text.Encoding.UTF8
                                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                                    v1Json = client.DownloadString(v1JsonUrl)
                                End Using
                                Main.GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                                Exit Sub
                            Catch ex As Exception
                                Debug.WriteLine("error- getting v1Json data for the bypasss")
                                Debug.WriteLine(ex.ToString)
                            End Try

                        End If




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
            Dim ContentID As String = Nothing

            For i As Integer = 0 To Main.FunimtaionSeasonList.Count - 1
                If ComboBox1.Text = Main.FunimtaionSeasonList.Item(i).Title Then
                    ContentID = Main.FunimtaionSeasonList.Item(i).ID
                    Exit For
                End If
            Next

            If ContentID = Nothing Then
                MsgBox("error during season selection")
                Exit Sub
            End If

            Dim BaseUrl() As String = Main.FunimationSeasonAPIUrl.Split(New String() {"/shows/"}, System.StringSplitOptions.RemoveEmptyEntries)



            Dim EpisodeJsonURL As String = BaseUrl(0) + "/seasons/" + ContentID + ".json"
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



        Else

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


        End If
    End Sub


    Public Sub FillFunimationEpisodes(ByVal EpisodeJson As String)

        Main.FunimationEpisodeJSON = EpisodeJson
        comboBox3.Enabled = True
        comboBox4.Enabled = True

        Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "episodeNumber" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        'EpisodeJson.Split(New String() {Chr(34) + "episodeNumber" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Debug.WriteLine(EpisodeSplit.Count.ToString)
        For i As Integer = 1 To EpisodeSplit.Count - 1
            Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            comboBox3.Items.Add("Episode " + EpisodeSplit2(0))
            comboBox4.Items.Add("Episode " + EpisodeSplit2(0))
        Next
        Main.WebbrowserURL = "https://funimation.com/js"
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

                            If CBool(InStr(UriUsed, "funimation.com/v/")) Then
                                Dim Episode() As String = UriUsed.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
                                Dim v1Json As String = Nothing
                                Try
                                    Using client As New WebClient()
                                        client.Encoding = System.Text.Encoding.UTF8
                                        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                                        v1Json = client.DownloadString(v1JsonUrl)
                                    End Using
                                    Main.GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                                    Exit Sub
                                Catch ex As Exception
                                    Debug.WriteLine("error- getting v1Json data for the bypasss")
                                    Debug.WriteLine(ex.ToString)
                                End Try

                            End If


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

