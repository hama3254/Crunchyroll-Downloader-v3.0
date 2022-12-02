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

Public Class Anime_Add
    Public Mass_DL_Cancel As Boolean = False
    Public List_DL_Cancel As Boolean = False




    Public Authorization As String = Nothing
    Public AuthorizationCookie As String = Nothing


    Public Sub LoadBrowser(ByVal Url As String)

        Main.LoadingUrl = Url
        Main.LoadedUrls.Clear()

        'Browser.WebView2.Source = New Uri(Url)
        'Exit Sub
        'MsgBox(Url)

        If CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "series")) = True Or CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "watch")) = True Then



#Region "Get Cookies"

            Main.CR_Cookies = "Cookie: "
            'MsgBox("Cookies")
            Browser.GetCookies(Url)
            'MsgBox("Cookies2")

            Debug.WriteLine(Main.CookieList.Count.ToString)
            If Main.CookieList.Count = 0 Then
                Browser.WebView2.CoreWebView2.Navigate(Url)
                StatusLabel.Text = "Status: loading in browser..."
                Main.Text = "Status: loading in browser..."
                Exit Sub
            End If

            Try



                Dim DeviceRegion As String = Nothing
                ' Main.CookieList = Collector.Task.Result()
                For i As Integer = 0 To Main.CookieList.Count - 1

                    If CBool(InStr(Main.CookieList.Item(i).Domain, ".crunchyroll.com")) And CBool(InStr(Main.CookieList.Item(i).Name, "_evidon_suppress")) = False Then
                        Main.CR_Cookies = Main.CR_Cookies + Main.CookieList.Item(i).Name + "=" + Main.CookieList.Item(i).Value + ";"
                    End If

                Next

                If CBool(InStr(Url, "/series")) Then
                    Dim locale1() As String = Url.Split(New String() {"crunchyroll.com/"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim locale2() As String = locale1(1).Split(New String() {"/series"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Main.locale = Main.Convert_locale(locale2(0))
                    If Main.locale = "en-US" Then
                        Main.Url_locale = ""
                    Else
                        Main.Url_locale = locale2(0)
                    End If
                ElseIf CBool(InStr(Url, "/watch")) Then
                    Dim locale1() As String = Url.Split(New String() {"crunchyroll.com/"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim locale2() As String = locale1(1).Split(New String() {"/watch"}, System.StringSplitOptions.RemoveEmptyEntries)
                    'MsgBox(locale2(0))

                    Main.locale = Main.Convert_locale(locale2(0))
                    'End If
                    If Main.locale = "en-US" Then
                        Main.Url_locale = ""
                    Else
                        Main.Url_locale = locale2(0)
                    End If
                End If

                'MsgBox("locale: " + Main.locale)

            Catch ex As Exception
                Browser.WebView2.CoreWebView2.Navigate(Url)
                StatusLabel.Text = "Status: loading in browser..."
                Exit Sub
            End Try




            'MsgBox(Cookies)
            Main.CR_Cookies = " -H " + Chr(34) + Main.CR_Cookies + Chr(34)



#End Region
            Dim Auth As String = " -H " + Chr(34) + "Authorization: " + Main.CrBetaBasic + Chr(34)
            Dim Post As String = " -d " + Chr(34) + "grant_type=etp_rt_cookie" + Chr(34) + " -X POST"

            Dim CRBetaBearer As String = "Bearer "

            Dim v1Token As String = Main.CurlPost("https://www.crunchyroll.com/auth/v1/token", Main.CR_Cookies, Auth, Post)



            If CBool(InStr(v1Token, "curl:")) = True And CBool(InStr(v1Token, "400")) = True Then

                v1Token = Main.CurlPost("https://www.crunchyroll.com/auth/v1/token", Main.CR_Cookies, Auth, Post.Replace("etp_rt_cookie", "client_id"))

            End If


            If CBool(InStr(v1Token, "curl:")) = True And CBool(InStr(v1Token, "400")) = True Then
                Me.StatusLabel.Text = "Status: Failed - bad request, check CR login"
                Main.Text = "Status: Failed - bad request, check CR login"
                Debug.WriteLine("Status: Failed - bad request, check CR login")
                If GroupBox3.Visible = True Then
                    GroupBox3.Visible = False
                    groupBox2.Visible = False
                    groupBox1.Visible = True
                    List_DL_Cancel = False
                    btn_dl.BackgroundImage = My.Resources.main_button_download_default
                End If
                Main.b = True
                Exit Sub

            ElseIf CBool(InStr(v1Token, "curl:")) = True Then
                v1Token = Main.CurlPost("https://www.crunchyroll.com/auth/v1/token", Main.CR_Cookies, Auth, Post)
            End If

            If CBool(InStr(v1Token, "curl:")) = True Then
                Browser.WebView2.CoreWebView2.Navigate(Url)
                StatusLabel.Text = "Status: loading in browser..."
                Exit Sub
            End If

            'MsgBox(v1Token)

            Dim Token() As String = v1Token.Split(New String() {Chr(34) + "access_token" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Token2() As String = Token(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            CRBetaBearer = CRBetaBearer + Token2(0)

            Dim ObjectsUrl As String = Nothing


            Dim Auth2 As String = " -H " + Chr(34) + "Authorization: " + CRBetaBearer + Chr(34)
            Dim v2Content As String = Main.CurlAuth("https://www.crunchyroll.com/index/v2", Main.CR_Cookies, Auth2) 'client.DownloadString("https://www.crunchyroll.com/index/v2")

            If CBool(InStr(v2Content, "curl:")) = True Then
                v2Content = Main.CurlAuth("https://www.crunchyroll.com/index/v2", Main.CR_Cookies, Auth2)
            End If

            If CBool(InStr(v2Content, "curl:")) = True Then
                Browser.WebView2.CoreWebView2.Navigate(Url)
                StatusLabel.Text = "Status: loading in browser..."
                Exit Sub
            End If


            Dim v2ContentBeta() As String = v2Content.Split(New String() {Chr(34) + "cms_web" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)


            Dim bucket() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "bucket" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim bucket2() As String = bucket(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim policy() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "policy" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim policy2() As String = policy(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim signature() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "signature" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim signature2() As String = signature(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim key_pair_id() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "key_pair_id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim key_pair_id2() As String = key_pair_id(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)


            If CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "series/")) = True Then


                Dim Series_idUrlBuilder() As String = Url.Split(New String() {"series/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Series_idUrlBuilder2() As String = Series_idUrlBuilder(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)


                Dim SeriesUrl As String = "https://www.crunchyroll.com/cms/v2" + bucket2(0) + "/seasons?series_id=" + Series_idUrlBuilder2(0) + "&locale=" + Main.locale + "&Signature=" + signature2(0) + "&Policy=" + policy2(0) + "&Key-Pair-Id=" + key_pair_id2(0)

                'MsgBox(SeriesUrl)
                Main.GetBetaSeasons(SeriesUrl)


            ElseIf CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "watch/")) = True And CBool(Main.CrBetaBasic = Nothing) = False Then



                'MsgBox(Url)
                Dim ObjectsURLBuilder3() As String = Url.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)


                ObjectsUrl = "https://www.crunchyroll.com/cms/v2" + bucket2(0) + "/objects/" + ObjectsURLBuilder4(0) + "?locale=" + Main.locale + "&Signature=" + signature2(0) + "&Policy=" + policy2(0) + "&Key-Pair-Id=" + key_pair_id2(0)
                'End Using
                'MsgBox(ObjectsUrl)

                Debug.WriteLine("ObjectsUrl: " + ObjectsUrl)


                Dim StreamsUrl As String = Nothing
                Dim ObjectJson As String
                Try
                    ObjectJson = Main.Curl(ObjectsUrl)
                    'MsgBox(ObjectJson)

                    If CBool(InStr(ObjectJson, "curl:")) = True Then
                        ObjectJson = Main.Curl(ObjectsUrl)
                    End If

                    If CBool(InStr(ObjectJson, "curl:")) = True Then
                        Browser.WebView2.CoreWebView2.Navigate(Url)
                        Exit Sub
                    ElseIf CBool(InStr(ObjectJson, "videos/")) = False Then

                        StatusLabel.Text = "Status: Failed - no video, check CR login"
                        Main.Text = "Status: Failed - no video, check CR login"
                        Debug.WriteLine("Status: Failed - no video, check CR login")
                        If GroupBox3.Visible = True Then
                            GroupBox3.Visible = False
                            groupBox2.Visible = False
                            groupBox1.Visible = True
                            List_DL_Cancel = False
                            btn_dl.BackgroundImage = My.Resources.main_button_download_default
                        End If
                        Exit Sub
                    End If

                Catch ex As Exception
                    Browser.WebView2.CoreWebView2.Navigate(Url)
                    Exit Sub
                End Try

                Try
                    Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"videos/"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim StreamsUrlBuilder3() As String = ObjectsUrl.Split(New String() {"objects/"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StreamsUrlBuilder4() As String = StreamsUrlBuilder3(1).Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)

                    StreamsUrl = StreamsUrlBuilder3(0) + "videos/" + StreamsUrlBuilder2(0) + "/streams?" + StreamsUrlBuilder4(1)

                    ' Debug.WriteLine(StreamsUrl)
                Catch ex As Exception
                    Browser.WebView2.CoreWebView2.Navigate(Url)
                    Exit Sub
                End Try

                Main.GetBetaVideoProxy(StreamsUrl, Url)


            Else
                Browser.WebView2.CoreWebView2.Navigate(Url)
            End If
        Else
            'to do
        End If
    End Sub

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

        'Timer3.Enabled = True

    End Sub




    Private Sub Btn_dl_Click(sender As Object, e As EventArgs) Handles btn_dl.Click


        If Application.OpenForms().OfType(Of Browser).Any = True Then
        Else
            Main.UserBowser = False
            Browser.Show()
        End If

        Main.LoginOnly = "Download Mode!"
        'MsgBox(Main.WebbrowserURL)
        If SubTitlesOnlyCB.Text = "[Default]" Then
            Main.SubsOnly = False
        Else
            Main.SubsOnly = True
        End If
        If groupBox1.Visible = True Then
            ' Main.LoadedUrls.Clear()
            Try
                If CBool(InStr(textBox1.Text, "crunchyroll.com")) Or CBool(InStr(textBox1.Text, "funimation.com")) Then


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
                    If Main.RunningDownloads >= Main.MaxDL Then
                        Debug.WriteLine("Max_Dl")
                        ListBox1.Items.Add(textBox1.Text)
                        textBox1.ForeColor = Color.FromArgb(9248044)
                        Pause(2)
                        textBox1.ForeColor = Color.Black
                        textBox1.Text = "URL"
                    Else

                        If CBool(InStr(textBox1.Text, "funimation.com")) Then

                            Main.WebbrowserURL = textBox1.Text

                            If CBool(InStr(textBox1.Text, "funimation.com/v/")) Then
                                Dim Episode0() As String = textBox1.Text.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Episode() As String = Episode0(0).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
                                MsgBox(v1JsonUrl)
                                Dim v1Json As String = Nothing
                                Try
                                    Using client As New WebClient()
                                        client.Encoding = System.Text.Encoding.UTF8
                                        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                                        v1Json = client.DownloadString(v1JsonUrl)
                                    End Using
                                    Main.WebbrowserURL = textBox1.Text
                                    Main.GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                                    Exit Sub
                                Catch ex As Exception
                                    Debug.WriteLine("error- getting v1Json data for the bypass")
                                    Debug.WriteLine(ex.ToString)
                                End Try

                            End If

                        End If

                        If Main.Grapp_RDY = True Then

                            Main.b = False
                            Debug.WriteLine("Start loading: " + Date.Now.ToString)
                            StatusLabel.Text = "Status: loading ...."
                            LoadBrowser(textBox1.Text)
                        Else
                            Debug.WriteLine("Not Ready!")
                        End If
                    End If
                    'End If


                ElseIf CBool(InStr(textBox1.Text, "Test=true")) Then
                    LoadBrowser(textBox1.Text)

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
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False

            ElseIf CBool(InStr(Main.WebbrowserURL, "crunchyroll.com")) = True Then

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                bt_Cancel_mass.Enabled = False
                bt_Cancel_mass.Visible = False

                Main.DownloadBetaSeasons()
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If CBool(InStr(Main.WebbrowserURL, "crunchyroll.com")) = True Then
            comboBox3.Items.Clear()
            comboBox4.Items.Clear()
            comboBox3.Enabled = False
            comboBox4.Enabled = False
            comboBox3.Text = Nothing
            comboBox4.Text = Nothing
            Dim SeasonSplit() As String = Main.CrBetaMass.Split(New String() {Chr(34) + "id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim SeasonSplit2() As String = SeasonSplit(ComboBox1.SelectedIndex + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim EpisodeJsonURL As String = Main.CrBetaMassBaseURL + "episodes?season_id=" + SeasonSplit2(0) + "&locale=" + Main.CrBetaMassParameters

            Debug.WriteLine(EpisodeJsonURL)

            Dim EpisodeJson As String = Main.Curl(EpisodeJsonURL) 'localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace(My.Resources.htmlReplace, "")


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

            If comboBox3.Items.Count > 0 Then
                comboBox3.SelectedIndex = 0
                comboBox4.SelectedIndex = comboBox4.Items.Count - 1
            End If

            comboBox3.Enabled = True
            comboBox4.Enabled = True





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
                LoadBrowser(EpisodeJsonURL)
                Exit Sub
            End Try

            FillFunimationEpisodes(EpisodeJson)


            If comboBox3.Items.Count > 0 Then
                comboBox3.SelectedIndex = 0
                comboBox4.SelectedIndex = comboBox4.Items.Count - 1
            End If



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


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ListBox1.Items.Count > 0 Then
            If StatusLabel.Text = "Status: idle" Then
                StatusLabel.Cursor = Cursors.Hand
                StatusLabel.Text = "Status: items in queue, click me to work off."
            End If

            If CBool(InStr(Main.Text, "Crunchyroll Downloader")) Or CBool(InStr(Main.Text, " downloads in queue")) Then
                Main.Text = "Status: " + ListBox1.Items.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
            End If

        Else
            If CBool(InStr(Main.Text, " downloads in queue")) Then
                Main.Text = "Crunchyroll Downloader"
            End If
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
            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Main.Panel1.Controls.OfType(Of CRD_List_Item))

            For i As Integer = 0 To Item.Count - 1
                Debug.WriteLine(Item(i).GetIsStatusFinished().ToString)
                If Item(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next

            Main.RunningDownloads = Item.Count - ItemFinshedCount

        Catch ex As Exception
            Main.RunningDownloads = Main.Panel1.Controls.Count
        End Try

        If Main.RunningDownloads < Main.MaxDL Then
            If ListBox1.Items.Count > 0 Then
                If GroupBox3.Visible = True Then
                    If CBool(InStr(ListBox1.GetItemText(ListBox1.Items(0)), "funimation.com")) Then
                        If Main.Funimation_Grapp_RDY = True Then
                            Dim UriUsed As String = ListBox1.GetItemText(ListBox1.Items(0))

                            If CBool(InStr(UriUsed, "funimation.com/v/")) Then
                                Dim Episode0() As String = textBox1.Text.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Episode() As String = Episode0(0).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)

                                Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
                                Dim v1Json As String = Nothing
                                Try
                                    Using client As New WebClient()
                                        client.Encoding = System.Text.Encoding.UTF8
                                        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                                        v1Json = client.DownloadString(v1JsonUrl)
                                    End Using
                                    Main.Funimation_Grapp_RDY = False
                                    Main.WebbrowserURL = UriUsed
                                    ListBox1.Items.Remove(ListBox1.Items(0))
                                    Main.b = False
                                    Main.Invalidate()
                                    Main.GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                                    Exit Sub
                                Catch ex As Exception
                                    Debug.WriteLine("error- getting v1Json data for the bypasss")
                                    Debug.WriteLine(ex.ToString)
                                End Try

                            End If

                            Main.Funimation_Grapp_RDY = False
                            Main.WebbrowserURL = UriUsed
                            ListBox1.Items.Remove(ListBox1.Items(0))
                            Main.b = False

                            StatusLabel.Text = "Status: loading in browser"
                            Main.Text = "Status: loading in browser"
                            LoadBrowser(UriUsed)
                            Main.Invalidate()
                        End If

                    Else
                        If Main.Grapp_RDY = True Then
                            Main.Grapp_RDY = False
                            StatusLabel.Text = "Status: loading ..."
                            Main.Text = "Status: loading ..."
                            LoadBrowser(ListBox1.GetItemText(ListBox1.Items(0)))
                            ListBox1.Items.Remove(ListBox1.Items(0))
                            Main.b = False
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



    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        If TextBox2.Text = "Use Custom Name" Then
            TextBox2.Text = Nothing

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

    Private Sub SubTitlesOnlyCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SubTitlesOnlyCB.SelectedIndexChanged
        If SubTitlesOnlyCB.Text = "[Default]" Then
            Main.SubsOnly = False
        Else
            Main.SubsOnly = True
        End If
    End Sub




    Private Sub GroupBox1_VisibleChanged(sender As Object, e As EventArgs) Handles groupBox1.VisibleChanged
        If Not TextBox2.Text = "Use Custom Name" Then
            TextBox2.Text = "Use Custom Name"
        End If
    End Sub

    'Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
    '    Timer3.Enabled = False
    '    'MsgBox(True.ToString)
    'End Sub
End Class

