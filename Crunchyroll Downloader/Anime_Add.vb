Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports CefSharp
Imports System.Text

Public Class Anime_Add
    Public Mass_DL_Cancel As Boolean = False
    Public List_DL_Cancel As Boolean = False

    Public ThreadList As New List(Of Thread)


    Public Authorization As String = Nothing
    Public AuthorizationCookie As String = Nothing


    Private Sub LoadBrowser(ByVal Url As String)


        Dim locale As String = "en-US"
        If CBool(InStr(Url, "beta.crunchyroll.com")) = True And CBool(InStr(Url, "watch")) = True And CBool(Main.CrBetaBasic = Nothing) = False Then
#Region "Get Cookies"
            Dim Cookies As String = "Cookie: "
            Try
                Dim Collector As New TaskCookieVisitor
                Dim CM As ICookieManager = CefSharp_Browser.WebBrowser1.GetCookieManager
                CM.VisitAllCookies(Collector)
                Dim DeviceRegion As String = Nothing
                Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
                For i As Integer = 0 To list.Count - 1
                    If CBool(InStr(list.Item(i).Domain, ".crunchyroll.com")) And CBool(InStr(list.Item(i).Name, "_evidon_suppress")) = False Then
                        Cookies = Cookies + list.Item(i).Name + "=" + list.Item(i).Value + ";"
                    End If
                    If CBool(InStr(list.Item(i).Domain, ".crunchyroll.com")) And CBool(InStr(list.Item(i).Name, "c_locale")) Then
                        locale = list.Item(i).Value
                    End If
                Next
            Catch ex As Exception
                CefSharp_Browser.WebBrowser1.Load(Url)
                Exit Sub
            End Try

#End Region
            Dim CRBetaBearer As String = "Bearer "
            Try

                Dim Request As HttpWebRequest = CType(WebRequest.Create("https://beta.crunchyroll.com/auth/v1/token"), HttpWebRequest)
                Request.Method = "POST"
                Request.ContentType = "application/x-www-form-urlencoded"
                Request.Accept = "application/json, text/plain, */*"
                'Request.Headers.Add("Accept-Language: de,en-US;q=0.7,en;q=0.3")
                Request.Headers.Add("Accept-Encoding: identity")
                Request.Headers.Add("Authorization: " + Main.CrBetaBasic)
                'Request.Headers.Add("Referer:  " + Url)
                Request.Referer = Url
                Request.Headers.Add("Origin: https://beta.crunchyroll.com")
                Request.Headers.Add(Cookies)
                Dim Post As String = "grant_type=etp_rt_cookie"
                Dim byteArray() As Byte = Encoding.UTF8.GetBytes(Post)
                Request.ContentLength = byteArray.Length
                Dim DataStream As Stream = Request.GetRequestStream()
                DataStream.Write(byteArray, 0, byteArray.Length)
                DataStream.Close()
                Dim Response As HttpWebResponse = CType(Request.GetResponse(), HttpWebResponse)
                DataStream = Response.GetResponseStream()
                Dim reader As New StreamReader(DataStream)
                Dim ServerResponseString As String = reader.ReadToEnd()
                reader.Close()
                DataStream.Close()
                Response.Close()
                'Debug.WriteLine(ServerResponseString)
                Dim Token() As String = ServerResponseString.Split(New String() {Chr(34) + "access_token" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Token2() As String = Token(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                CRBetaBearer = CRBetaBearer + Token2(0)
            Catch ex As Exception
                Debug.WriteLine(ex.ToString)
                CefSharp_Browser.WebBrowser1.Load(Url)
                Exit Sub
            End Try

            Dim ObjectsUrl As String = Nothing

            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    client.Headers.Add("ACCEPT: application/json, text/javascript, */*; q=0.01")
                    client.Headers.Add("Accept-Encoding: identity")
                    client.Headers.Add("Referer:  " + Url)
                    client.Headers.Add("Authorization: " + CRBetaBearer)
                    client.Headers.Add(Cookies) '+ WebBrowser1.Document.Cookie)
                    'MsgBox(OmUStreamSplitEpisodeIndex(1))
                    Dim v2Content As String = client.DownloadString("https://beta.crunchyroll.com/index/v2")
                    'Debug.WriteLine(v2Content)

                    Dim v2ContentBeta() As String = v2Content.Split(New String() {Chr(34) + "cms_beta" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)


                    Dim bucket() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "bucket" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim bucket2() As String = bucket(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim policy() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "policy" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim policy2() As String = policy(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim signature() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "signature" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim signature2() As String = signature(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim key_pair_id() As String = v2ContentBeta(1).Split(New String() {Chr(34) + "key_pair_id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim key_pair_id2() As String = key_pair_id(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim ObjectsURLBuilder3() As String = Url.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)


                    ObjectsUrl = "https://beta.crunchyroll.com/cms/v2" + bucket2(0) + "/objects/" + ObjectsURLBuilder4(0) + "?locale=" + locale + "&Signature=" + signature2(0) + "&Policy=" + policy2(0) + "&Key-Pair-Id=" + key_pair_id2(0)
                End Using


                'Debug.WriteLine(ObjectsUrl)

            Catch ex As Exception
                CefSharp_Browser.WebBrowser1.Load(Url)
                Exit Sub
            End Try

            Dim StreamsUrl As String = Nothing
            Dim ObjectJson As String
            Try
                Try
                    Using client As New WebClient()
                        client.Encoding = System.Text.Encoding.UTF8
                        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                        ObjectJson = client.DownloadString(ObjectsUrl)
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("error- getting name data")
                    Exit Sub
                End Try

            Catch ex As Exception
                CefSharp_Browser.WebBrowser1.Load(Url)
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
                CefSharp_Browser.WebBrowser1.Load(Url)
                Exit Sub
            End Try

            Main.GetBetaVideoProxy(StreamsUrl, Url)


        Else
            CefSharp_Browser.WebBrowser1.Load(Url)
        End If


    End Sub

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

                            End If

                            If Main.Grapp_RDY = True Then

                                Main.b = False
                                Debug.WriteLine("Start loading: " + Date.Now.ToString)
                                LoadBrowser(textBox1.Text)
                                StatusLabel.Text = "Status: loading ...."

                            End If
                        End If
                    End If


                ElseIf CBool(InStr(textBox1.Text, "Test=true")) Then
                    LoadBrowser(textBox1.Text)
                    'Else 'If CBool(InStr(textBox1.Text, "vrv.co")) Then
                ElseIf CBool(InStr(textBox1.Text, "https://")) Then
                    If MessageBox.Show("This in NOT a Crunchyroll URL, try anyway?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Main.b = False
                        LoadBrowser(textBox1.Text)
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
                bt_Cancel_mass.Enabled = False
                bt_Cancel_mass.Visible = False
                Main.DownloadFunimationJS_Seasons()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False

            ElseIf CBool(InStr(Main.WebbrowserURL, "beta.crunchyroll.com")) = True Then

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
            ElseIf CBool(InStr(Main.WebbrowserURL, "vrv.co")) = True Then

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                bt_Cancel_mass.Enabled = False
                bt_Cancel_mass.Visible = False

                Main.Download_VRV_Seasons()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False

            Else

                StatusLabel.Text = "Status: idle"
                'btn_dl.BackgroundImage = My.Resources.add_mass_running_cancel
                btn_dl.Text = "Cancel"
                Mass_DL_Cancel = True
                bt_Cancel_mass.Enabled = False
                bt_Cancel_mass.Visible = False
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

            If comboBox3.Items.Count > 0 Then
                comboBox3.SelectedIndex = 0
                comboBox4.SelectedIndex = comboBox4.Items.Count - 1
            End If



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

            If comboBox3.Items.Count > 0 Then
                comboBox3.SelectedIndex = 0
                comboBox4.SelectedIndex = comboBox4.Items.Count - 1
            End If

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
                            LoadBrowser(UriUsed)
                            StatusLabel.Text = "Status: loading in browser"
                            Main.Text = "Status: loading in browser"

                            Main.Invalidate()
                        End If

                    Else
                        If Main.Grapp_RDY = True Then
                            Main.Grapp_RDY = False
                            LoadBrowser(ListBox1.GetItemText(ListBox1.Items(0)))
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
        If Not TextBox2.Text = "Use Custom Name" Then
            TextBox2.Text = "Use Custom Name"
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

