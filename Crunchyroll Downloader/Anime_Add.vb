Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Text
Imports System.Runtime.InteropServices.ComTypes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports Newtonsoft.Json.Linq
Imports System.Timers
Imports System.Security.Policy
Imports Crunchyroll_Downloader.CRD_Classes

Public Class Anime_Add
    Public Mass_DL_Cancel As Boolean = False
    Public List_DL_Cancel As Boolean = False


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            If ComboBox2.Text = SubFolder_Nothing Then
                SubFolder_Value = SubFolder_Nothing
                My.Settings.SubFolder_Value = SubFolder_Value
            ElseIf ComboBox2.Text = SubFolder_automatic Then
                SubFolder_Value = SubFolder_automatic
                My.Settings.SubFolder_Value = SubFolder_Value
            ElseIf ComboBox2.Text = SubFolder_automatic2 Then
                SubFolder_Value = SubFolder_automatic2
                My.Settings.SubFolder_Value = SubFolder_Value
            Else
                SubFolder_Value = ComboBox2.Text
                My.Settings.SubFolder_Value = SubFolder_Value
            End If
        Catch ex As Exception
            ComboBox2.Text = SubFolder_Nothing
        End Try
    End Sub
    Dim Manager As MetroStyleManager = Main.Manager


    Private Sub Anime_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DownloadScope.SelectedIndex = Main.DownloadScope

        Manager.Owner = Me
        Me.StyleManager = Manager
        Btn_Close.Image = Main.CloseImg
        Btn_min.Image = Main.MinImg

        btn_dl.Cursor = Cursors.No
        btn_dl.BackgroundImage = My.Resources.main_button_download_deactivate

        If File.Exists("cookies.txt") = True Or Main.BowserWasOpen = True Then
            btn_dl.BackgroundImage = My.Resources.main_button_download_default
            btn_dl.Cursor = Cursors.Default
        End If



        Try
            Me.Icon = My.Resources.icon
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
            SubFolder_Value = My.Settings.SubFolder_Value
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

        Dim TS As TimeSpan = TimeSpan.FromDays(3666)

        If Main.HideFLInt = 1 Then
            TS = TimeSpan.FromMilliseconds(1)
        ElseIf Main.HideFLInt = 2 Then
            TS = TimeSpan.FromDays(7)
        ElseIf Main.HideFLInt = 3 Then
            TS = TimeSpan.FromDays(30)
        ElseIf Main.HideFLInt = 4 Then
            TS = TimeSpan.FromDays(90)
        ElseIf Main.HideFLInt = 5 Then
            TS = TimeSpan.FromDays(180)
        End If
        'MsgBox(TS.ToString)
        Try
            Dim di As New System.IO.DirectoryInfo(Main.Pfad)
            For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) = False Then
                    'MsgBox(fi.Name + vbNewLine + fi.LastAccessTime.ToString)
                    If fi.Name = SubFolder_Value = True Then
                        ComboBox2.Items.Add(fi.Name)
                    ElseIf fi.LastAccessTime > Date.Now.Subtract(TS) = True Then
                        ComboBox2.Items.Add(fi.Name)
                    End If

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
            ' MsgBox(ex.ToString)
            'SubFolder_Nothing
            ComboBox2.SelectedItem = SubFolder_Nothing
        End Try

        'Timer3.Enabled = True

    End Sub




    Private Sub Btn_dl_Click(sender As Object, e As EventArgs) Handles btn_dl.Click

        If btn_dl.Cursor = Cursors.No Then
            Exit Sub
        End If


        Main.LoginOnly = "Download Mode!"
        'MsgBox(Main.WebbrowserURL)


        If groupBox1.Visible = True Then
            ' Main.LoadedUrls.Clear()
            Try
                If CBool(InStr(textBox1.Text, ":\")) Then

                    Main.ProcessLocal(textBox1.Text)
                ElseIf CBool(InStr(textBox1.Text, ".m3u8")) Then

                    ' Main.ProcessLocal(textBox1.Text)
                    Dim NameKomplett As String = TextBox2.Text
                    If NameKomplett = Nothing Or NameKomplett = "Use Custom Name" Then
                        NameKomplett = GeräteID2().Replace("CRD-Temp-File", "misc_download-#")
                    End If
                    Dim Namep1 As String = "Other"
                    Dim Namep2 As String = NameKomplett
                    Dim Reso As String = "NaN"
                    Dim HardSub As String = "maybe?"
                    Dim ThumbnialURL As String = "no"
                    Dim URL_DL As String = "-i " + Chr(34) + textBox1.Text + Chr(34) + " " + Main.ffmpeg_command
                    Dim Pfad_DL As String = Path.Combine(Main.Pfad, Namep2)
                    Dim Service As String = "other"

                    'MsgBox(URL_DL)

                    Main.ItemConstructor(NameKomplett, Namep1, Namep2, Reso, HardSub, ThumbnialURL, URL_DL, Chr(34) + Pfad_DL + Chr(34), Service)

                ElseIf CBool(InStr(textBox1.Text, "crunchyroll.com")) Or CBool(InStr(textBox1.Text, "funimation.com")) Then


                    'If StatusLabel.Text = "Status: waiting for episode selection" Then
                    '    If MessageBox.Show("Are you sure you want cancel the advanced download?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    '        StatusLabel.Text = "Status: idle"
                    '    Else
                    '        Exit Sub
                    '        btn_dl.Enabled = True
                    '    End If
                    '    'ElseIf LabelUpdate = "Status: looking for video file" Then
                    '    '    Exit Sub
                    '    '    pictureBox4.Enabled = True
                    'Else
                    If Main.RunningQueue = True Then
                        If CBool(InStr(textBox1.Text, "series/")) Then
                            Debug.WriteLine("Queue_Block_series")
                            'textBox1.Text = "URL"
                            StatusLabel.Text = "Status: Series add blocked, queue is running!"
                            Pause(5)
                            StatusLabel.Text = "Status: Idle"
                        Else
                            Debug.WriteLine("Queue_Block")
                            Main.ListBoxList.Add(textBox1.Text)
                            textBox1.Text = "URL"
                            StatusLabel.Text = "Status: Added to Queue"
                            Pause(5)
                            StatusLabel.Text = "Status: Idle"
                        End If
                    ElseIf Main.RunningDownloads >= Main.MaxDL Then
                        Debug.WriteLine("Max_Dl")
                        Main.ListBoxList.Add(textBox1.Text)
                        textBox1.Text = "URL"
                        StatusLabel.Text = "Status: Added to Queue"
                        Pause(5)
                        StatusLabel.Text = "Status: Idle"


                    Else

                        If CBool(InStr(textBox1.Text, "funimation.com")) Then

                            Main.WebbrowserURL = textBox1.Text

                            If CBool(InStr(textBox1.Text, "funimation.com/v/")) Then
                                Dim Episode0() As String = textBox1.Text.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
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
                                    Main.WebbrowserURL = textBox1.Text
                                    Main.GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                                    Exit Sub
                                Catch ex As Exception
                                    Debug.WriteLine("error- getting v1Json data for the bypass")
                                    Debug.WriteLine(ex.ToString)
                                End Try
                            ElseIf CBool(InStr(textBox1.Text, "funimation.com/shows/")) Then
                                Main.LoadingUrl = textBox1.Text
                                Main.LoadedUrls.Clear()
                                Main.b = False
                                Debug.WriteLine("loading funimation show url: " + Date.Now.ToString)
                                StatusLabel.Text = "Status: loading funimation...."
                                'Main.LoadBrowser()
                                Browser.WebView2.CoreWebView2.Navigate(textBox1.Text)
                                Exit Sub
                            End If

                        End If

                        If Main.Grapp_RDY = True Then

                            Main.b = False
                            Debug.WriteLine("what's going on?: " + Date.Now.ToString)
                            StatusLabel.Text = "Status: loading ...."
                            Main.LoadBrowser(textBox1.Text)
                        Else
                            Debug.WriteLine("Not Ready!")
                        End If
                    End If
                    'End If


                ElseIf CBool(InStr(textBox1.Text, "Test=true")) Then
                    Main.LoadBrowser(textBox1.Text)

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
                groupBox2.Visible = False
                Main.Grapp_Abord = True
                Main.b = True
                groupBox1.Visible = True
                btn_dl.Text = "Download"
                btn_dl.BackgroundImage = My.Resources.main_button_download_default
                StatusLabel.Text = "Status: idle"
            ElseIf CBool(InStr(Main.WebbrowserURL, "funimation.com")) = True Then



                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                bt_Cancel_mass.Enabled = False
                bt_Cancel_mass.Visible = False
                Main.DownloadFunimationJS_Seasons()
                CB_EP1.Enabled = False
                CB_EP0.Enabled = False
                CB_Season.Enabled = False

            ElseIf CBool(InStr(Main.WebbrowserURL, "crunchyroll.com")) = True Then

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                bt_Cancel_mass.Enabled = False
                bt_Cancel_mass.Visible = False

                Main.DownloadBetaSeasons()
                CB_EP1.Enabled = False
                CB_EP0.Enabled = False
                CB_Season.Enabled = False


            End If


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
        If btn_dl.Cursor = Cursors.No Then
            Exit Sub
        End If


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
        If btn_dl.Cursor = Cursors.No Then
            Exit Sub
        End If


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


    Private Sub bt_Cancel_mass_Click(sender As Object, e As EventArgs) Handles bt_Cancel_mass.Click
        groupBox1.Visible = True
        groupBox2.Visible = False
    End Sub

    Private Sub bt_Cancel_mass_MouseEnter(sender As Object, e As EventArgs) Handles bt_Cancel_mass.MouseEnter

        bt_Cancel_mass.BackgroundImage = My.Resources.add_mass_cancel_hover


    End Sub
    Private Sub bt_Cancel_mass_MouseLeave(sender As Object, e As EventArgs) Handles bt_Cancel_mass.MouseLeave
        bt_Cancel_mass.BackgroundImage = My.Resources.add_mass_cancel

    End Sub

    Public Sub FillCREpisodes(ByVal EpisodeJson As String)



        EpisodeJson = CleanJSON(EpisodeJson)
        Main.CR_MassEpisodes.Clear()

        Dim audio_locale_filter As String = Nothing

        For i As Integer = 0 To Main.LangValueEnum.Count - 1
            If Main.LangValueEnum.Item(i).DisplayText = CB_Dub.Text Then
                audio_locale_filter = Main.LangValueEnum.Item(i).CR_Value
            End If
        Next

        Dim EpisodeJObject As JObject = JObject.Parse(EpisodeJson)
        Dim EpisodeData As List(Of JToken) = EpisodeJObject.Children().ToList

        For Each item As JProperty In EpisodeData
            item.CreateReader()
            Select Case item.Name
                Case "data" 'each record is inside the entries array
                    For Each Entry As JObject In item.Values

                        Dim audio_locale As String = Entry.GetValue("audio_locale").ToString
                        If audio_locale_filter = Nothing Then
                            audio_locale_filter = audio_locale
                        ElseIf CBool(InStr(audio_locale_filter, audio_locale)) = False Then
                            'MsgBox(audio_locale + vbNewLine + audio_locale_filter)
                            Continue For
                        End If

                        Dim episode_number As String = Entry.GetValue("episode_number").ToString
                        Dim episode_id As String = Entry.GetValue("id").ToString
                        Dim slug_title As String = Entry.GetValue("slug_title").ToString

                        CB_EP0.Items.Add("Episode " + episode_number)
                        CB_EP1.Items.Add("Episode " + episode_number)
                        Main.CR_MassEpisodes.Add(New CR_Seasons(episode_id, slug_title, Main.CR_MassSeasons.Item(CB_Season.SelectedIndex).Auth, ""))

                    Next
            End Select
        Next

        If CB_EP0.Items.Count > 0 Then
            CB_EP0.SelectedIndex = 0
            CB_EP1.SelectedIndex = CB_EP1.Items.Count - 1
        End If

        CB_EP0.Enabled = True
        CB_EP1.Enabled = True

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Season.SelectedIndexChanged
        If CBool(InStr(Main.WebbrowserURL, "crunchyroll.com")) = True Then
            CB_EP0.Items.Clear()
            CB_EP1.Items.Clear()
            CB_EP0.Enabled = False
            CB_EP1.Enabled = False
            CB_EP0.Text = Nothing
            CB_EP1.Text = Nothing


            'get guid

            Dim guid As String = Nothing

            For i As Integer = 0 To Main.CR_MassSeasons.Count - 1
                If "Season " + Main.CR_MassSeasons.Item(i).Season = CB_Season.Text And Main.ConvertSubValue(Main.CR_MassSeasons.Item(i).audio_locale, ConvertSubsEnum.DisplayText) = CB_Dub.Text Then
                    guid = Main.CR_MassSeasons.Item(i).guid
                    'MsgBox(guid + vbNewLine + Main.CR_MassSeasons.Item(i).audio_locale)
                End If
            Next

            If guid = Nothing Then
                MsgBox("Requested guid not found", MsgBoxStyle.Critical)
                Exit Sub
            End If

            ' Dim JsonUrl As String = "https://www.crunchyroll.com/content/v2/cms/seasons/" + Main.CR_MassSeasons.Item(CB_Season.SelectedIndex).guid + "/episodes?preferred_audio_language=" + Main.DubSprache.CR_Value + "&locale=" + Main.locale

            Dim JsonUrl As String = "https://www.crunchyroll.com/content/v2/cms/seasons/" + guid + "/episodes?preferred_audio_language=" + Main.DubSprache.CR_Value + "&locale=" + Main.locale


            Dim Loc_CR_Cookies = " -H " + Chr(34) + Main.CR_Cookies.Replace(Chr(34), "").Replace(" -H ", "") + Chr(34)


            Dim EpisodeJson As String = Nothing 'CurlAuth(JsonUrl, Loc_CR_Cookies, Main.CR_MassSeasons.Item(ComboBox1.SelectedIndex).Auth) '


            Try

                EpisodeJson = CurlAuthNew(JsonUrl, Loc_CR_Cookies, Main.CR_MassSeasons.Item(CB_Season.SelectedIndex).Auth) '

            Catch ex As Exception
                If CBool(InStr(ex.ToString, "Error - Getting")) Then
                    MsgBox("Error invalid CR respone")
                    Exit Sub
                Else
                    MsgBox("Error processing data")
                    Exit Sub
                End If
            End Try

            'My.Computer.Clipboard.SetText(EpisodeJson)
            Debug.WriteLine("EpisodeJson: " + EpisodeJson)

            FillCREpisodes(EpisodeJson)


        ElseIf Main.WebbrowserURL = "https://funimation.com/js" Then
            CB_EP0.Items.Clear()
            CB_EP1.Items.Clear()
            CB_EP0.Text = Nothing
            CB_EP1.Text = Nothing
            Dim ContentID As String = Nothing

            For i As Integer = 0 To Main.FunimtaionSeasonList.Count - 1
                If CB_Season.Text = Main.FunimtaionSeasonList.Item(i).Title Then
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
                    client.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))
                    EpisodeJson = client.DownloadString(EpisodeJsonURL)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting EpisodeJson data")
                Debug.WriteLine(ex.ToString)
                Main.FunimationJsonBrowser = "EpisodeJson"
                Main.LoadBrowser(EpisodeJsonURL)
                Exit Sub
            End Try

            FillFunimationEpisodes(EpisodeJson)


            If CB_EP0.Items.Count > 0 Then
                CB_EP0.SelectedIndex = 0
                CB_EP1.SelectedIndex = CB_EP1.Items.Count - 1
            End If



        End If
    End Sub


    Public Sub FillFunimationEpisodes(ByVal EpisodeJson As String)

        Main.FunimationEpisodeJSON = EpisodeJson
        CB_EP0.Enabled = True
        CB_EP1.Enabled = True

        Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {Chr(34) + "episodeNumber" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        'EpisodeJson.Split(New String() {Chr(34) + "episodeNumber" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Debug.WriteLine(EpisodeSplit.Count.ToString)
        For i As Integer = 1 To EpisodeSplit.Count - 1
            Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            CB_EP0.Items.Add("Episode " + EpisodeSplit2(0))
            CB_EP1.Items.Add("Episode " + EpisodeSplit2(0))
        Next
        Main.WebbrowserURL = "https://funimation.com/js"
    End Sub







    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        If TextBox2.Text = "Use Custom Name" Then
            TextBox2.Text = Nothing

        End If
    End Sub


    Private Sub Anime_Add_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Btn_Close.Location = New Point(Me.Width - 40, 1)
        Btn_min.Location = New Point(Me.Width - 68, 10)
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click

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
            My.Settings.Pfad = Main.Pfad
            My.Settings.Save()


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

    Private Sub SubTitlesOnlyCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DownloadScope.SelectedIndexChanged

        Main.DownloadScope = DownloadScope.SelectedIndex
        My.Settings.DownloadScope = Main.DownloadScope
        My.Settings.Save()
    End Sub




    Private Sub GroupBox1_VisibleChanged(sender As Object, e As EventArgs) Handles groupBox1.VisibleChanged
        If Not TextBox2.Text = "Use Custom Name" And CBool(InStr(TextBox2.Text, "++")) = False Then
            TextBox2.Text = "Use Custom Name"
        End If
    End Sub

    Private Sub CB_Dub_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Dub.SelectedIndexChanged
        'MsgBox(CB_Dub.Text)
        'MsgBox(Main.DubSprache.DisplayText)

        If My.Settings.OverrideDub = True And CBool(InStr(CB_Dub.Text, Main.DubSprache.DisplayText)) = False Then
            MessageBox.Show("The Duboveride might change the Dub to: " + Main.DubSprache.DisplayText, "Settings - Override enabled", MessageBoxButtons.OK)
        End If

        'clear everything below the dub 
        CB_Season.Items.Clear()
        CB_EP0.Items.Clear()
        CB_EP1.Items.Clear()

        'also remove display text
        CB_Season.Text = Nothing
        CB_EP0.Text = Nothing
        CB_EP1.Text = Nothing

        For i As Integer = 0 To Main.CR_MassSeasons.Count - 1
            If Main.ConvertSubValue(Main.CR_MassSeasons.Item(i).audio_locale, ConvertSubsEnum.DisplayText) = CB_Dub.Text Then
                CB_Season.Items.Add("Season " + Main.CR_MassSeasons.Item(i).Season)
            End If
        Next



        CB_Season.Enabled = True
    End Sub


End Class

