Imports Microsoft.Win32
Imports System.Net
Imports Gecko
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
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)
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

    Public Sub BetaCR(ByVal Auth As String, ByVal Cookie As String)

        Try
            Using client As New WebClient()
                client.Encoding = System.Text.Encoding.UTF8
                client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                client.Headers.Add("ACCEPT: gzip")
                client.Headers.Add("Cookie: " + Cookie)
                client.Headers.Add("Authorization: " + Auth)
                client.Headers.Add("Content-Type: application/x-www-form-urlencoded")
                client.Headers.Add("Referer: https://beta.crunchyroll.com/") '
                Dim reqparm As New Specialized.NameValueCollection

                reqparm.Add("grant_type", "etp_rt_cookie")
                Dim responsebytes = client.UploadValues("https://beta-api.crunchyroll.com/auth/v1/token", "POST", reqparm)
                Dim responsebody = (New Text.UTF8Encoding).GetString(responsebytes)
                'My.Computer.Clipboard.SetText(responsebody)
            End Using
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        'pictureBox4.Enabled = False
        Main.LoginOnly = "Download Mode!"
        If SubTitlesOnlyCB.Text = "[Default]" Then
            Main.SubsOnly = False
        Else
            Main.SubsOnly = True
        End If
        If groupBox1.Visible = True Then
            Try
                If CBool(InStr(textBox1.Text, "crunchyroll.com")) Or CBool(InStr(textBox1.Text, "funimation.com")) Then 'Or CBool(InStr(textBox1.Text, "anime-on-demand.de")) Then

#Region "Funimation url parameter"
                    If InStr(textBox1.Text, "funimation.com") Then
                        If Main.DubFunimation = "Disabled" Then
                        Else
                            If InStr(textBox1.Text, "?lang=") Then
                                Dim ClearUri As String() = textBox1.Text.Split(New String() {"?lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                If ClearUri.Count > 1 Then
                                    If InStr(ClearUri(1), "&") Then
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
                            ElseIf InStr(textBox1.Text, "&lang=") Then
                                Dim ClearUri As String() = textBox1.Text.Split(New String() {"&lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                If ClearUri.Count > 1 Then

                                    If InStr(ClearUri(1), "&") Then
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

                            ElseIf InStr(textBox1.Text, "?") Then
                                textBox1.AppendText("&lang=" + Main.DubFunimation)
                            Else
                                textBox1.AppendText("?lang=" + Main.DubFunimation)
                            End If
                        End If

                    End If
#End Region

                    If StatusLabel.Text = "Status: waiting for episode selection" Then
                        If MessageBox.Show("Are you sure you want cancel the advanced download?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            StatusLabel.Text = "Status: idle"
                        Else
                            Exit Sub
                            pictureBox4.Enabled = True
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
                                GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                                StatusLabel.Text = "Status: loading ..."
                                Main.b = False
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

                    GeckoFX.WebBrowser1.Navigate(textBox1.Text)
#End Region

                ElseIf CBool(InStr(textBox1.Text, "Test=true")) Then
                    GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                    'Else 'If CBool(InStr(textBox1.Text, "vrv.co")) Then
                ElseIf CBool(InStr(textBox1.Text, "https://")) Then
                    If MessageBox.Show("This in NOT a Crunchyroll URL, try anyway?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Main.b = False
                        GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                        StatusLabel.Text = "Status: looking for non CR video file"

                    Else
                        Exit Sub
                        pictureBox4.Enabled = True
                    End If

                    'Else
                    'MsgBox(Main.URL_Invaild, MsgBoxStyle.OkOnly)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
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
                pictureBox4.Image = My.Resources.main_button_download_default
            ElseIf CBool(InStr(Main.WebbrowserURL, "beta.crunchyroll.com")) = True Then

                StatusLabel.Text = "Status: idle"
                pictureBox4.Image = My.Resources.add_mass_running_cancel
                Mass_DL_Cancel = True
                PictureBox1.Enabled = False
                PictureBox1.Visible = False
                Main.DownloadBetaSeasons()
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
                pictureBox4.Image = My.Resources.add_mass_running_cancel
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
            pictureBox4.Image = My.Resources.main_button_download_default
        End If

        pictureBox4.Enabled = True
    End Sub

    Public Sub ProcessAoD()

        AoD_DubList.Clear()
        AoD_OmUList.Clear()
        If AoD_Cookie = Nothing Then

            MsgBox(Main.LoginReminder)
            Main.Text = "Crunchyroll Downloader"
            Main.Invalidate()
            StatusLabel.Text = "Status: idle"
            Exit Sub

        End If

        If CBool(InStr(Main.WebbrowserText, "/OmU/1080/hlsfirst/")) Then
            Dim OmUStreamSplit() As String = Main.WebbrowserText.Split(New String() {"/OmU/1080/hlsfirst/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim OmUStreamSplitToken() As String = OmUStreamSplit(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim OmUStreamSplitEpisodeIndex() As String = OmUStreamSplit(0).Split(New String() {"/videomaterialurl/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim OmUStreamSplitEpisodeIndex2() As String = OmUStreamSplitEpisodeIndex(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim m3u8Strings As String = Nothing

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                    client.Headers.Add("Accept-Encoding: gzip, deflate, br")
                    client.Headers.Add("X-Requested-With: XMLHttpRequest")
                    client.Headers.Add(AoD_Cookie) '+ WebBrowser1.Document.Cookie)
                    'MsgBox(OmUStreamSplitEpisodeIndex(1))
                    m3u8Strings = client.DownloadString("https://www.anime-on-demand.de/videomaterialurl/" + OmUStreamSplitEpisodeIndex2(0) + "/OmU/1080/hlsfirst/" + OmUStreamSplitToken(0))
                    '("Sub: " + m3u8Strings)
                End Using
            Catch ex As Exception
                MsgBox(ex.ToString + vbNewLine + "https://www.anime-on-demand.de/videomaterialurl/" + OmUStreamSplitEpisodeIndex2(0) + "/OmU/1080/hlsfirst/" + OmUStreamSplitToken(0))
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
            Dim DubStreamSplitEpisodeIndex2() As String = DubStreamSplitEpisodeIndex(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim m3u8Strings As String = Nothing

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                    client.Headers.Add("Accept-Encoding: gzip, deflate, br")
                    client.Headers.Add("X-Requested-With: XMLHttpRequest")
                    client.Headers.Add(AoD_Cookie) '+ WebBrowser1.Document.Cookie)
                    'MsgBox(DubStreamSplitEpisodeIndex(1))
                    m3u8Strings = client.DownloadString("https://www.anime-on-demand.de/videomaterialurl/" + DubStreamSplitEpisodeIndex2(0) + "/Dub/1080/hlsfirst/" + DubStreamSplitToken(0))
                    'MsgBox("Dub: " + m3u8Strings)
                End Using
            Catch ex As Exception
                MsgBox(ex.ToString + vbNewLine + "https://www.anime-on-demand.de/videomaterialurl/" + DubStreamSplitEpisodeIndex2(0) + "/Dub/1080/hlsfirst/" + DubStreamSplitToken(0))
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
        If AoD_DubList.Count And AoD_OmUList.Count > 0 Then
            ComboBox1.Items.Clear()
            GroupBox3.Visible = False
            groupBox2.Visible = True
            groupBox1.Visible = False
            ComboBox1.Enabled = True
            comboBox3.Enabled = True
            comboBox4.Enabled = True
            ComboBox1.Items.Add("Dub")
            ComboBox1.Items.Add("OmU")
            FillAoDDropDown()
        ElseIf AoD_DubList.Count Or AoD_OmUList.Count > 0 Then
            ComboBox1.Items.Clear()
            GroupBox3.Visible = False
            groupBox2.Visible = True
            groupBox1.Visible = False
            ComboBox1.Enabled = False
            comboBox3.Enabled = True
            comboBox4.Enabled = True
            FillAoDDropDown()
        End If
    End Sub

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox4.MouseEnter
        If Mass_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel_hover
        ElseIf List_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel_hover

        Else
            pictureBox4.Image = My.Resources.main_button_download_hovert
        End If

    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        If Mass_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel
        ElseIf List_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel
        Else
            pictureBox4.Image = My.Resources.main_button_download_default
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
            Dim SeasonSplit() As String = Main.CrBetaMass.Split(New String() {Chr(34) + "id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim SeasonSplit2() As String = SeasonSplit(ComboBox1.SelectedIndex + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim EpisodeJsonURL As String = "https://beta-api.crunchyroll.com/cms/v2/DE/M3/crunchyroll/episodes?season_id=" + SeasonSplit2(0) + "&locale=" + Main.CrBetaMassParameters
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
                Exit Sub
            End Try
            Main.CrBetaMassEpisodes = EpisodeJson




            Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "episode" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 1 To EpisodeSplit.Count - 1
                    Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    comboBox3.Items.Add("Episode " + EpisodeSplit2(0))
                    comboBox4.Items.Add("Episode " + EpisodeSplit2(0))
                Next

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
                If InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + ComboBox1.SelectedItem.ToString + "</a>") Then
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
        End If
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
                StatusLabel.Text = "Status: items in queue, click to work off."
            End If
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
                pictureBox4.Image = My.Resources.main_button_download_default
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
                    If InStr(ListBox1.GetItemText(ListBox1.Items(0)), "funimation.com") Then
                        If Main.Funimation_Grapp_RDY = True Then

                            Dim UriUsed As String = ListBox1.GetItemText(ListBox1.Items(0))
                            If Main.DubFunimation = "Disabled" Then
                            Else
                                If InStr(UriUsed, "?lang=") Then
                                    Dim ClearUri As String() = UriUsed.Split(New String() {"?lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then
                                        If InStr(ClearUri(1), "&") Then
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
                                ElseIf InStr(UriUsed, "&lang=") Then
                                    Dim ClearUri As String() = UriUsed.Split(New String() {"&lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then

                                        If InStr(ClearUri(1), "&") Then
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

                                ElseIf InStr(UriUsed, "?") Then
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

                            If Main.SystemWebBrowserCookie = Nothing Then
                                Main.b = False
                                GeckoFX.WebBrowser1.Navigate(UriUsed)
                                StatusLabel.Text = "Status: loading in browser"
                                Main.Text = "Status: loading in browser"
                            Else
                                StatusLabel.Text = "Status: loading ..."
                                Main.Text = "Status: loading ..."
                                'MsgBox(Main.SystemWebBrowserCookie)
                                ServicePointManager.Expect100Continue = True
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                Try
                                    Using client As New WebClient()
                                        client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0")
                                        client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                                        client.Headers.Add("Accept-Encoding: *")
                                        client.Headers.Add("Cookie:" + Main.SystemWebBrowserCookie)
                                        Dim HTMLString As String = "No Value"

                                        Try
                                            HTMLString = DecompressString(client.DownloadData(Main.WebbrowserURL))
                                        Catch ex As Exception
                                            HTMLString = client.DownloadString(Main.WebbrowserURL)
                                        End Try

                                        If InStr(HTMLString, My.Resources.Funimation_Player_ID) Then
                                            Dim WebbrowserHeadTextSplit() As String = HTMLString.Split(New String() {"<head"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim WebbrowserHeadTextSplit2() As String = WebbrowserHeadTextSplit(1).Split(New String() {"</head>"}, System.StringSplitOptions.RemoveEmptyEntries)

                                            Dim WebbrowserTitleSplit() As String = HTMLString.Split(New String() {"<title>"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim WebbrowserTitleSplit2() As String = WebbrowserTitleSplit(1).Split(New String() {"</title>"}, System.StringSplitOptions.RemoveEmptyEntries)


                                            Main.WebbrowserText = HTMLString
                                            Main.WebbrowserTitle = "<title>" + WebbrowserTitleSplit2(0) + "</title>"
                                            Main.WebbrowserHeadText = "<head" + WebbrowserHeadTextSplit2(0) + "</head>"
                                            Main.WebbrowserCookie = Main.SystemWebBrowserCookie

                                            Dim t As Thread
                                            t = New Thread(AddressOf Main.Funitmation_Grapp)
                                            t.Priority = ThreadPriority.Normal
                                            t.IsBackground = True
                                            t.Start()

                                        ElseIf HTMLString = "No Value" Then
                                            Me.Invoke(New Action(Function()
                                                                     Main.Text = "Status: Website error"
                                                                     Me.StatusLabel.Text = "Status: no video found"
                                                                     Return Nothing
                                                                     Main.Funimation_Grapp_RDY = True
                                                                 End Function))

                                        Else

                                            Me.Invoke(New Action(Function()
                                                                     Main.Text = "Status: no video found"
                                                                     Me.StatusLabel.Text = "Status: no video found"
                                                                     My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\funimation.log", HTMLString, True)
                                                                     Main.Funimation_Grapp_RDY = True
                                                                     Return Nothing
                                                                 End Function))

                                        End If
                                    End Using
                                Catch ex As Exception
                                    MsgBox(ex.ToString)

                                    Main.Funimation_Grapp_RDY = True
                                    Exit Sub
                                End Try

                            End If
                            Main.Invalidate()
                        End If

                    Else
                        If Main.Grapp_RDY = True Then
                            Main.Grapp_RDY = False
                            GeckoFX.WebBrowser1.Navigate(ListBox1.GetItemText(ListBox1.Items(0)))
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
        If StatusLabel.Text = "Status: items in queue, click to work off." Then
            groupBox1.Visible = False
            groupBox2.Visible = False
            GroupBox3.Visible = True
            pictureBox4.Image = My.Resources.add_mass_running_cancel
            List_DL_Cancel = True
        End If

    End Sub


    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles textBox2.Click
        If textBox2.Text = "Name of the Anime" Then
            textBox2.Text = Nothing
        End If
    End Sub


    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub




#End Region

    Private Sub FillAoDDropDown()
        comboBox3.Items.Clear()
        comboBox4.Items.Clear()
        If AoD_OmUList.Count > 0 Then
            For i As Integer = 0 To AoD_OmUList.Count - 1
                Dim DropDownTitle As String() = AoD_OmUList(i).Split(New String() {My.Resources.AoD_Titel}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim DropDownTitle2 As String() = DropDownTitle(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                comboBox3.Items.Add(DropDownTitle2(0))
                comboBox4.Items.Add(DropDownTitle2(0))
            Next
        ElseIf AoD_DubList.Count > 0 Then
            For i As Integer = 0 To AoD_DubList.Count - 1
                Dim DropDownTitle As String() = AoD_DubList(i).Split(New String() {My.Resources.AoD_Titel}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim DropDownTitle2 As String() = DropDownTitle(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                comboBox3.Items.Add(DropDownTitle2(0))
                comboBox4.Items.Add(DropDownTitle2(0))
            Next
        End If
    End Sub

    Public Sub Add_AoD()
        Dim ProcessList As New List(Of String)
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
        Dim TargetReso As String = Main.Reso
        Dim AoD_1080pPlus As Boolean = False
        Dim AoDTempReso As String = "6666x6666"

        Dim AoD_Season As String = Nothing
        Dim AoD_Anime_Title As String = Nothing
        Dim AoD_Episode_Title As String = Nothing
        Dim AoD_Episode_Number As String = Nothing

        Me.Invoke(New Action(Function()
                                 'Main.StatusMainForm.Text = "Crunchyroll Downloader"
                                 VideoFormat = Main.VideoFormat
                                 ffmpeg = Main.ffmpeg_command
                                 Pfad2 = Main.Pfad
                                 NameMethode = Main.CR_NameMethode
                                 If Main.AoD_Reso = 0 Then
                                     TargetReso = Main.Reso
                                 ElseIf Main.AoD_Reso = 576 Then
                                     TargetReso = 576
                                 ElseIf Main.AoD_Reso = 1080 Then
                                     AoD_1080pPlus = True
                                     TargetReso = 1080
                                 End If

                                 If ComboBox1.Text = "Dub" Then
                                     For i As Integer = 0 To AoD_DubList.Count - 1
                                         ProcessList.Add(AoD_DubList(i))
                                     Next

                                 ElseIf ComboBox1.Text = "OmU" Then
                                     For i As Integer = 0 To AoD_OmUList.Count - 1
                                         ProcessList.Add(AoD_OmUList(i))
                                     Next
                                 ElseIf ComboBox1.Enabled = False Then

                                     If AoD_DubList.Count > 0 Then
                                         For i As Integer = 0 To AoD_DubList.Count - 1
                                             ProcessList.Add(AoD_DubList(i))
                                         Next
                                         Dub = True
                                     ElseIf AoD_OmUList.Count > 0 Then
                                         For i As Integer = 0 To AoD_OmUList.Count - 1
                                             ProcessList.Add(AoD_OmUList(i))
                                         Next
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

            If Mass_DL_Cancel = True Then
                Exit For
            End If
            For e As Integer = 0 To Integer.MaxValue
                Thread.Sleep(2000)

                If RDY = True Then
                    Try
                        Me.Invoke(New Action(Function()
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

            Me.Invoke(New Action(Function()
                                     Running = Main.RunningDownloads
                                     DlMax = Main.MaxDL
                                     Dim d As Integer = ii - CB3 + 1
                                     Add_Display.Text = d.ToString + " / " + c.ToString
                                     Main.Text = "Status: " + d.ToString + " / " + c.ToString '  looking for video file"
                                     Main.Invalidate()
                                     Return Nothing
                                 End Function))

            Dim AoDTitle1() As String = ProcessList.Item(i).Split(New String() {My.Resources.AoD_Titel}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDTitle2() As String = AoDTitle1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDTitle As String = AoDTitle2(0)

            Try


                If InStr(AoDHTML, My.Resources.AoD_HTML_Episode_Title) Then ' Serie 
                    Dim AoDTitle0() As String = AoDHTML.Split(New String() {My.Resources.AoD_HTML_Episode_Title}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDTitle00() As String = AoDTitle0(ii + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoD_EpisodeSplit() As String = AoDTitle00(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)
                    If AoD_EpisodeSplit.Count > 2 Then
                        AoD_Episode_Title = Nothing
                        For i3 As Integer = 1 To AoD_EpisodeSplit.Count - 1
                            If AoD_Episode_Title = Nothing Then
                                AoD_Episode_Title = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(i3), "[^\w\\-]", " ").Trim(" ")
                            Else
                                AoD_Episode_Title = AoD_Episode_Title + " - " + System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(i3), "[^\w\\-]", " ").Trim(" ")

                            End If
                        Next
                        AoD_Episode_Number = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(0), "[^\w\\-]", " ").Trim(" ")
                    Else
                        AoD_Episode_Number = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(0), "[^\w\\-]", " ").Trim(" ")
                        AoD_Episode_Title = System.Text.RegularExpressions.Regex.Replace(AoD_EpisodeSplit(1), "[^\w\\-]", " ").Trim(" ")

                    End If


                    Dim AoDTitle3() As String = AoDHTML.Split(New String() {My.Resources.AoD_HTML_Anime_Title}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDTitle4() As String = AoDTitle3(1).Split(New String() {"</h1>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If InStr(AoDTitle4(0), " - ") Then
                        Dim AoD_Anime_Season_split() As String = AoDTitle4(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoD_Anime_Season_split(0), "[^\w\\-]", " ").Trim(" ")
                        AoD_Season = System.Text.RegularExpressions.Regex.Replace(AoD_Anime_Season_split(1), "[^\w\\-]", " ").Trim(" ")
                    Else
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoDTitle4(0), "[^\w\\-]", " ").Trim(" ")

                    End If


                Else 'keine Serie aka Film 

                    Dim AoDMovie1() As String = AoDHTML.Split(New String() {My.Resources.AoD_HTML_Anime_Title}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim AoDMovie2() As String = AoDMovie1(1).Split(New String() {"</h1>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If InStr(AoDMovie2(0), " - ") Then

                        Dim AoDMovie_split() As String = AoDMovie2(0).Split(New String() {" - "}, System.StringSplitOptions.RemoveEmptyEntries)
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoDMovie_split(0), "[^\w\\-]", " ").Trim(" ")
                        AoD_Episode_Number = System.Text.RegularExpressions.Regex.Replace(AoDMovie_split(1), "[^\w\\-]", " ").Trim(" ")
                        AoD_Episode_Title = System.Text.RegularExpressions.Regex.Replace(AoDMovie_split(1), "[^\w\\-]", " ").Trim(" ")
                    Else
                        AoD_Anime_Title = System.Text.RegularExpressions.Regex.Replace(AoDMovie2(0), "[^\w\\-]", " ").Trim(" ")

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

            AoDTitle = System.Text.RegularExpressions.Regex.Replace(AoDTitle, "[^\w\\-]", " ").Trim(" ")
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
                Me.Invoke(New Action(Function()
                                         Main.Text = "Status: File already exists."
                                         Main.Invalidate()
                                         Return Nothing
                                     End Function))

                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Me.Invoke(New Action(Function()
                                                 Main.Text = "Status: Old file overwritten."
                                                 Main.Invalidate()
                                                 Return Nothing
                                             End Function))

                    Catch ex As Exception
                    End Try
                Else
                    Me.Invoke(New Action(Function()
                                             Main.Text = "Crunchyroll Downloader"
                                             Return Nothing
                                         End Function))

                    Continue For
                    Exit Sub
                End If

            End If
#End Region
            Dim AoDThumbnail1() As String = ProcessList.Item(i).Split(New String() {My.Resources.AoD_Image}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDThumbnail2() As String = AoDThumbnail1(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim AoDThumbnail As String = AoDThumbnail2(0)
            Dim AoDTm3u8() As String = ProcessList.Item(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim m3u8_Master_url As String = AoDTm3u8(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
            Dim m3u8_list As New List(Of String)
            Dim m3u8_url As String = Nothing
            Dim m3u8_url_Temp As String = Nothing

            Dim client As New WebClient
            client.Encoding = System.Text.Encoding.UTF8
            Dim text As String = client.DownloadString(m3u8_Master_url)
            'Me.Invoke(New Action(Function()
            '                         MsgBox(m3u8_Master_url)
            '                         Return Nothing
            '                     End Function))
            'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\Test.txt", text, False)

            If InStr(text, "RESOLUTION=") Then 'master m3u8 no fragments 
                Dim new_m3u8() As String = text.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                If TargetReso = 42 Then
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
                            'Me.Invoke(New Action(Function()
                            '                         MsgBox(new_m3u8(i2 + 1))
                            '                         Return Nothing
                            '                     End Function))
                            m3u8_list.Add(new_m3u8(i2) + vbCrLf + new_m3u8(i2 + 1))
                        End If
                    ElseIf CBool(InStr(new_m3u8(i2), AoDTempReso)) = True Then
                        m3u8_list.Add(new_m3u8(i2) + vbCrLf + new_m3u8(i2 + 1))
                    End If

                Next
                'Me.Invoke(New Action(Function()
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
                                'Me.Invoke(New Action(Function()
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
                                'Me.Invoke(New Action(Function()
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
                    'Me.Invoke(New Action(Function()
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

                    Me.Invoke(New Action(Function()
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

                If InStr(m3u8_url_Temp, "https://") Then
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
            Me.Invoke(New Action(Function()
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
                Main.ListBoxList.Add(ListBox1.Items.Item(i))
            Next
        End If
        Me.Close()
    End Sub

    Private Sub Btn_min_MouseEnter(sender As Object, e As EventArgs) Handles Btn_min.MouseEnter
        Dim PB As PictureBox = sender
        PB.Image = My.Resources.main_mini_red
    End Sub

    Private Sub Btn_min_MouseLeave(sender As Object, e As EventArgs) Handles Btn_min.MouseLeave
        Dim PB As PictureBox = sender
        PB.Image = Main.MinImg
    End Sub
    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter
        Dim PB As PictureBox = sender
        PB.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave
        Dim PB As PictureBox = sender
        PB.Image = Main.CloseImg
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
End Class