Imports Gecko.Events
Imports Gecko
Imports System.IO
Imports Gecko.Cache
Imports System.ComponentModel
Imports System.Threading
Imports System.Net
Imports System.Net.WebUtility
Imports System.IO.Compression
Imports System.Text

Public Class GeckoFX
    Public keks As String = Nothing
    'Public c As Boolean = True
    Dim t As Thread
    Public ScanTrue As Boolean = False
    Public ScanTime As Integer = 0



    Private Sub GeckoWebBrowser1_DocumentCompleted(sender As Object, e As EventArgs) Handles WebBrowser1.DocumentCompleted
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        'MsgBox("loaded!")
        If ScanTrue = False Then
            Button2.Enabled = True
        End If
        If Main.LoginOnly = "US_UnBlock" Then
            Main.LoginOnly = "US_UnBlock_Wait"
            If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "waiting for reCAPTCHA . . .")) Then
                Pause(4)
                Main.LoginOnly = "US_UnBlock"
            Else
                Try

                    If CBool(InStr(WebBrowser1.Document.Uri, "https://api.crunchyroll.com/login.0.json")) Then
                        Main.LoginOnly = "US_UnBlock_Finsihed"
                    Else
                        Dim cookieGrapp As String = WebBrowser1.Document.Body.OuterHtml '.Replace(vbTab, "").Replace(" ", "")
                        If Main.Debug2 = True Then
                            MsgBox(cookieGrapp)
                        End If
                        Dim cookieGrapp2() As String = cookieGrapp.Split(New String() {"<a class=" + Chr(34) + "cookie" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim cookieGrapp3() As String = cookieGrapp2(1).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        keks = cookieGrapp3(0)
                        If Main.Debug2 = True Then
                            MsgBox(keks)
                        End If

                        WebBrowser1.Navigate("https://www.crunchyroll.com/logout")
                        Pause(5)
                        WebBrowser1.Navigate("javascript:document.cookie =" + Chr(34) + "session_id=" + keks + "; expires=Thu, 04 Jan 2022 00:00:00 UTC; path=/;" + Chr(34) + ";")
                        Pause(1)
                        WebBrowser1.Navigate("javascript:document.cookie =" + Chr(34) + "sess_id=" + keks + "; expires=Thu, 04 Jan 2022 00:00:00 UTC; path=/;" + Chr(34) + ";")
                        Pause(1)
                        WebBrowser1.Navigate("https://www.crunchyroll.com/")
                        Main.LoginOnly = "US_UnBlock_Finsihed"
                    End If

                Catch ex As Exception
                    If Main.LoginOnly = "US_UnBlock_Finsihed" And Main.UserBowser = False Then
                        Me.Close()
                    End If
                    'MsgBox(ex.ToString)
                End Try

            End If
        ElseIf Main.LoginOnly = "US_UnBlock_Finsihed" And Main.UserBowser = False Then
            Me.Close()
        Else

            If CBool(InStr(WebBrowser1.Url.ToString, "beta.crunchyroll.com")) Then
                Main.WebbrowserURL = WebBrowser1.Url.ToString
                Exit Sub

            ElseIf CBool(InStr(WebBrowser1.Url.ToString, "crunchyroll.com")) Then

                If Main.b = False Then
                    Try
                        If WebBrowser1.Url.ToString = "https://www.crunchyroll.com/" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/en-gb" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/es" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/es-es" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/pt-br" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/pt-pt" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/fr" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/de" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/ar" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/it" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/ru" Then
                            Main.b = True
                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "hardsub_lang")) Then
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                            Main.b = True

                            t = New Thread(AddressOf Main.GrappURL)
                            t.Priority = ThreadPriority.Normal
                            t.IsBackground = True
                            t.Start()



                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "season-dropdown content-menu block")) Then
                            Main.b = True
                            Anime_Add.textBox2.Text = "Name of the Anime"
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml

                            Main.SeasonDropdownGrapp()

                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "wrapper container-shadow hover-classes")) Then
                            Main.b = True
                            Anime_Add.textBox2.Text = "Name of the Anime"
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                            Main.MassGrapp()
                        Else
                            Main.b = True
                            MsgBox(Main.No_Stream, MsgBoxStyle.OkOnly)
                            Anime_Add.StatusLabel.Text = "Status: idle"
                        End If
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        Anime_Add.StatusLabel.Text = "Status: idle"
                    End Try
                ElseIf Main.c = False Then
                    If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "hardsub_lang")) Then
                        Main.c = True
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                        'SoftSub.DownloadSubs()
                        Me.Close()
                    End If


                End If
                If Main.UserBowser = False Then
                    Try
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                    Catch ex As Exception
                    End Try

                    Me.Close()
                End If

            ElseIf CBool(InStr(WebBrowser1.Url.ToString, "funimation.com")) Then
                If Main.b = False Then

                    If InStr(WebBrowser1.Document.Body.OuterHtml, My.Resources.Funimation_Player_ID) Then
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                        Main.WebbrowserCookie = WebBrowser1.Document.Cookie
                        Main.b = True

                        t = New Thread(AddressOf Main.Funitmation_Grapp)
                        t.Priority = ThreadPriority.Normal
                        t.IsBackground = True
                        t.Start()

                    Else
                        Main.Text = "Status: no video found"
                        Anime_Add.StatusLabel.Text = "fail?"
                    End If
                End If
            ElseIf CBool(InStr(WebBrowser1.Url.ToString, "anime-on-demand.de")) Then
                If Main.b = False Then
                    Main.b = True
                    Main.WebbrowserURL = WebBrowser1.Url.ToString
                    Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                    Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                    Anime_Add.AoDHTML = WebBrowser1.Document.Body.OuterHtml
                    Anime_Add.ProcessAoD()
                    Exit Sub


                End If

            Else
                If Main.b = False Then
                    Main.m3u8List.Clear()
                    Main.mpdList.Clear()
                    Main.txtList.Clear()
                    Button2.Enabled = False
                    ScanTrue = True
                    Main.LogBrowserData = True
                    NetworkScanEnd()
                End If
            End If
        End If
        If Main.UserBowser = False Then
            If Main.b = True Then
                Anime_Add.StatusLabel.Text = "Status: idle"
                Me.Close()
            End If
        End If
        'End If
    End Sub



    Private Sub GeckoFX_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Me.Width > My.Computer.Screen.Bounds.Width Then
            Me.Width = My.Computer.Screen.Bounds.Width
            WebBrowser1.Width = Me.Size.Width - 15 ', Me.Size.Height - 69)
            WebBrowser1.Location = New Point(0, 30)
            TextBox1.Width = My.Computer.Screen.Bounds.Width - 435

        End If

        If Me.Size.Height > My.Computer.Screen.Bounds.Height Then
            Me.Height = My.Computer.Screen.Bounds.Height
            WebBrowser1.Height = Me.Size.Height - 69
            WebBrowser1.Location = New Point(0, 30)
        End If

        If Main.Debug2 = True Then
            Debug_Mode.Show()
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If

        If WebBrowser1.Url.ToString = "about:blank" Then
            If Main.LoginOnly = "US_UnBlock" Then
                WebBrowser1.Navigate("https://www.crunchyroll.com/login")
            Else
                WebBrowser1.Navigate(Main.Startseite)
            End If
        End If
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        'Main.UserBowser = True
        'Main.Pause(15)
        'For ii As Integer = 19 To 46
        '    WebBrowser1.Navigate("https://proxer.me/read/22459/" + ii.ToString + "/en/1")
        '    Main.Pause(15)
        '    Main.WebbrowserURL = WebBrowser1.Url.ToString
        '    Dim NameDLFinal As String = Nothing
        '    Dim NameDL As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"<div id=" + Chr(34) + "breadcrumb" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim NameDL2 As String() = NameDL(1).Split(New String() {"<div>"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim NameDL3 As String() = NameDL2(0).Split(New String() {Chr(34) + "true" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    For i As Integer = 0 To NameDL3.Count - 1
        '        If InStr(NameDL3(i), "</a>") Then
        '            Dim NameDL4 As String() = NameDL3(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
        '            If NameDLFinal = Nothing Then
        '                NameDLFinal = NameDL4(0)
        '            Else
        '                NameDLFinal = NameDLFinal + " " + NameDL4(0)
        '            End If
        '        End If
        '    Next
        '    NameDLFinal = System.Text.RegularExpressions.Regex.Replace(NameDLFinal, "[^\w\\-]", " ")
        '    If Main.Debug2 = True Then
        '        MsgBox(NameDLFinal)
        '    End If
        '    Dim SiteData As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"var pages ="}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim SiteData2 As String() = SiteData(1).Split(New String() {"</script>"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim ImageNumbers As String() = SiteData2(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim ImageList As New List(Of String)
        '    Dim ImageListString As String = Nothing
        '    For i As Integer = 0 To ImageNumbers.Count - 1
        '        If InStr(ImageNumbers(i), ".jpg") Then
        '            ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
        '            ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
        '        ElseIf InStr(ImageNumbers(i), ".png") Then
        '            ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
        '            ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
        '        End If
        '    Next
        '    If Main.Debug2 = True Then
        '        MsgBox(ImageListString)
        '    End If
        '    Dim BaseURL As String() = SiteData2(0).Split(New String() {"var serverurl = '"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim BaseURL2 As String() = BaseURL(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim BaseURL3 As String = "https:" + BaseURL2(0)
        '    If Main.Debug2 = True Then
        '        MsgBox(BaseURL3)
        '    End If
        '    AsyncWorkerX.RunAsync(AddressOf Main.DownloadMangaPages, BaseURL3, ImageList, Main.RemoveExtraSpaces(NameDLFinal))
        'Next
    End Sub

    Private Sub GeckoFX_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Main.UserBowser = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            My.Computer.Clipboard.SetText(WebBrowser1.Url.ToString)
            MsgBox("copied: " + Chr(34) + WebBrowser1.Url.ToString + Chr(34))
        Catch ex As Exception
        End Try

        'MsgBox(WebBrowser1.Document.Cookie)
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Return Then
                e.SuppressKeyPress = True
                WebBrowser1.Navigate(TextBox1.Text)
            End If

        Catch ex As Exception
            MsgBox("Error in URL", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentTitleChanged(sender As Object, e As EventArgs) Handles WebBrowser1.DocumentTitleChanged
        Try
            TextBox1.Text = WebBrowser1.Url.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If InStr(WebBrowser1.Url.ToString, "https://proxer.me/read/") Then
            Main.WebbrowserURL = WebBrowser1.Url.ToString
            Dim NameDLFinal As String = Nothing
            Dim NameDL As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"<div id=" + Chr(34) + "breadcrumb" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NameDL2 As String() = NameDL(1).Split(New String() {"<div>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NameDL3 As String() = NameDL2(0).Split(New String() {Chr(34) + "true" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To NameDL3.Count - 1
                If InStr(NameDL3(i), "</a>") Then
                    Dim NameDL4 As String() = NameDL3(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If NameDLFinal = Nothing Then
                        NameDLFinal = NameDL4(0)
                    Else
                        NameDLFinal = NameDLFinal + " " + NameDL4(0)
                    End If
                End If
            Next
            NameDLFinal = String.Join(" ", NameDLFinal.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) ''System.Text.RegularExpressions.Regex.Replace(NameDLFinal, "[^\w\\-]", " ")
            If Main.Debug2 = True Then
                MsgBox(NameDLFinal)
            End If
            Dim SiteData As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"var pages ="}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SiteData2 As String() = SiteData(1).Split(New String() {"</script>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ImageNumbers As String() = SiteData2(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ImageList As New List(Of String)
            Dim ImageListString As String = Nothing
            For i As Integer = 0 To ImageNumbers.Count - 1
                If InStr(ImageNumbers(i), ".jpg") Then
                    ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
                    ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
                ElseIf InStr(ImageNumbers(i), ".png") Then
                    ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
                    ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
                End If
            Next
            If Main.Debug2 = True Then
                MsgBox(ImageListString)
            End If
            Dim BaseURL As String() = SiteData2(0).Split(New String() {"var serverurl = '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim BaseURL2 As String() = BaseURL(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim BaseURL3 As String = "https:" + BaseURL2(0)
            If Main.Debug2 = True Then
                MsgBox(BaseURL3)
            End If
            'AsyncWorkerX.RunAsync(AddressOf Main.DownloadMangaPages, BaseURL3, ImageList, Main.RemoveExtraSpaces(NameDLFinal))
            Dim Thumbnail As String = BaseURL3 + ImageList(0)
            Main.MangaListItemAdd(Main.RemoveExtraSpaces(NameDLFinal), Thumbnail, BaseURL3, ImageList)

        ElseIf InStr(WebBrowser1.Url.ToString, "cr-cookie-ui.php") Then
            'MsgBox(WebBrowser1.Document.Body.InnerHtml)
        Else
            Main.m3u8List.Clear()
            Main.mpdList.Clear()
            Main.txtList.Clear()
            Button2.Enabled = False
            ScanTrue = True
            Main.LogBrowserData = True
            NetworkScanEnd()
        End If
    End Sub

    Private Sub GeckoFX_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Main.Debug2 = True Then
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If
    End Sub

    Private Sub WebBrowser1_GotFocus(sender As Object, e As EventArgs) Handles WebBrowser1.GotFocus
        'Debug_Mode.ActiveForm = True
    End Sub

    Private Sub WebBrowser1_LostFocus(sender As Object, e As EventArgs) Handles WebBrowser1.LostFocus
        'Debug_Mode.TopMost = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If InStr(WebBrowser1.Url.ToString, "funimation.com") Then
            Dim Funimation_List As New List(Of String)
            Dim Funimation_list1() As String = WebBrowser1.Document.Body.OuterHtml.Split(New String() {My.Resources.Funimation_Split_1}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 1 To Funimation_list1.Count - 1
                Dim Funimation_list2() As String = Funimation_list1(i).Split(New String() {My.Resources.Funimation_Split_2}, System.StringSplitOptions.RemoveEmptyEntries)
                Funimation_List.Add("https://www.funimation.com" + Funimation_list2(0))
                Main.ListBoxList.Add("https://www.funimation.com" + Funimation_list2(0))
            Next
            MsgBox(Funimation_List.Count.ToString + " episodes added to Download queue")
            'For ii As Integer = 0 To Funimation_List.Count - 1
            '    MsgBox(Funimation_List.Item(ii))
            'Next
        End If
    End Sub


    Private Sub ObserveHttpModifyRequest(sender As Object, e As GeckoObserveHttpModifyRequestEventArgs) Handles WebBrowser1.ObserveHttpModifyRequest
        Dim requesturl As String = e.Channel.Uri.ToString()
        Dim url As New Uri(requesturl)

        If Main.BlockList.Contains(url.Host) Then
            e.Cancel = True
            'Debug.WriteLine(requesturl)
            Exit Sub
        ElseIf requesturl.Contains("ad_") Or requesturl.Contains("ads") Or requesturl.Contains(".swf") Or requesturl.Contains("unsupported") Then
            e.Cancel = True
            'Debug.WriteLine(requesturl)
            Exit Sub

        End If
        If CBool(InStr(requesturl, ".js")) = True Then

            'Debug.WriteLine(requesturl)
        End If
        If CBool(InStr(requesturl, "https://beta-api.crunchyroll.com/")) And CBool(InStr(requesturl, "streams?")) Then
            If Main.b = False Then
                Main.GetBetaVideo(requesturl, Main.WebbrowserURL)
                Exit Sub
            End If
        ElseIf CBool(InStr(requesturl, "https://beta-api.crunchyroll.com/")) And CBool(InStr(requesturl, "seasons?series_id=")) Then
            'Debug.WriteLine(requesturl)
            If Main.b = False Then
                'Main.WebbrowserURL = WebBrowser1.Url.ToString
                Main.GetBetaSeasons(requesturl)
                Exit Sub
            End If

        End If
        'If CBool(InStr(requesturl, "https://beta-api.crunchyroll.com/")) And CBool(InStr(requesturl, "episodes?")) Then
        '    Debug.WriteLine(requesturl)
        'End If
        If ScanTrue = True Then

            If InStr(requesturl, ".m3u8") Then
                Dim client0 As New WebClient
                client0.Encoding = Encoding.UTF8
                client0.Headers.Add(HttpRequestHeader.Cookie, e.Channel.GetRequestHeader("Cookie"))
                Dim str0 As String = client0.DownloadString(requesturl)

                If InStr(str0, "#EXTM3U") Then
                    Main.m3u8List.Add(requesturl)
                Else
                    Dim DecodedUrl As String = UrlDecode(requesturl)
                    'MsgBox(DecodedUrl)
                    Dim URLSplit() As String = DecodedUrl.Split(New String() {".m3u8"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim URLSplit2() As String = URLSplit(0).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim NewUrl As String = "https://" + URLSplit2(URLSplit2.Count - 1) + ".m3u8" + URLSplit(1)
                    'MsgBox(NewUrl)
                    Dim str1 As String = client0.DownloadString(NewUrl)
                    'MsgBox(str1)
                    If InStr(str1, "#EXTM3U") Then
                        Main.m3u8List.Add(NewUrl)
                    End If

                End If
            ElseIf InStr(requesturl, ".mpd") Then
                Main.mpdList.Add(requesturl)
            ElseIf InStr(requesturl, ".txt") Then
                Main.txtList.Add(requesturl)
            ElseIf InStr(requesturl, ".vtt") Then
                Main.txtList.Add(requesturl)
            ElseIf InStr(requesturl, ".srt") Then
                Main.txtList.Add(requesturl)
            ElseIf InStr(requesturl, ".ass") Then
                Main.txtList.Add(requesturl)
            ElseIf InStr(requesturl, ".ssa") Then
                Main.txtList.Add(requesturl)
            ElseIf InStr(requesturl, ".dfxp") Then
                Main.txtList.Add(requesturl)
            End If
        End If

        If InStr(url.Host, "anime-on-demand.de") Then
            Debug.WriteLine(e.Channel.GetRequestHeader("Cookie"))
            If InStr(e.Channel.GetRequestHeader("Cookie"), "remember_user_token") Then
                Anime_Add.AoD_Cookie = "Cookie: " + e.Channel.GetRequestHeader("Cookie")

            End If

        End If

        If Main.UserBowser = False Then
            If CBool(InStr(requesturl, ".jpg")) = True Or CBool(InStr(requesturl, ".bmp")) = True Or CBool(InStr(requesturl, ".gif")) = True Or CBool(InStr(requesturl, ".png")) = True Or CBool(InStr(requesturl, ".webp")) = True Then
                e.Cancel = True

            End If

        End If

    End Sub

    Public Sub NetworkScanEnd()
        For i As Integer = 20 To 0 Step -1
            Pause(1)
            Button2.Text = "network scan is in progess " + Math.Abs(i).ToString
            Try
                Anime_Add.StatusLabel.Text = "network scan is in progess " + Math.Abs(i).ToString
            Catch ex As Exception

            End Try
        Next
        ScanTrue = False
        Main.LogBrowserData = False
        Try
            Main.WebbrowserURL = WebBrowser1.Url.ToString
            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
            Main.WebbrowserCookie = WebBrowser1.Document.Cookie
        Catch ex As Exception
        End Try
        Button2.Text = "use network scan dialog"
        network_scan.ShowDialog()
    End Sub

    Private Sub WebBrowser1_NSSError(sender As Object, e As GeckoNSSErrorEventArgs) Handles WebBrowser1.NSSError
        Debug.WriteLine(e.Message)
        If e.Message.Contains("certificate") And CBool(InStr(e.Uri.Host, "funimation.com")) = True Then
            CertOverrideService.GetService().RememberValidityOverride(e.Uri, e.Certificate, CertOverride.Mismatch, False)
        End If
    End Sub

    Private Sub WebBrowser1_ConsoleMessage(sender As Object, e As ConsoleMessageEventArgs) Handles WebBrowser1.ConsoleMessage
        ' Debug.WriteLine(e.Message)
        ' MsgBox(e.Message)
    End Sub
End Class

