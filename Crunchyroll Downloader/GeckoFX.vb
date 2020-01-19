Imports Gecko.Events
Imports Gecko
Imports System.IO
Imports Gecko.Cache
Imports System.ComponentModel
Imports System.Threading
Imports System.Net
Imports System.Net.WebUtility
Public Class GeckoFX
    Public keks As String = Nothing
    Public c As Boolean = True
    Dim t As Thread
    Dim ScanTrue As Boolean = False
    Private Sub GeckoWebBrowser1_DocumentCompleted(sender As Object, e As EventArgs) Handles WebBrowser1.DocumentCompleted
        If ScanTrue = False Then
            Button2.Enabled = True
        End If
        If Main.LoginOnly = "US_UnBlock" Then
            Main.LoginOnly = "US_UnBlocck_Wait2nd"
            Try
                WebBrowser1.Navigate("javascript:document.cookie =" + Chr(34) + "session_id=" + keks + "; expires=Thu, 05 Jan 2021 00:00:00 UTC; path=/;" + Chr(34) + ";")
                Main.Pause(1)
                WebBrowser1.Navigate("javascript:document.cookie = " + Chr(34) + "sess_id=" + keks + "; expires=Thu, 05 Jan 2021 00:00:00 UTC; path=/;" + Chr(34) + ";")
                Main.Pause(1)
                WebBrowser1.Navigate("javascript:document.cookie = " + Chr(34) + "c_locale=enUS; expires=Thu, 05 Jan 2021 00:00:00 UTC; path=/;" + Chr(34) + ";")
                Main.Pause(1)
                WebBrowser1.Navigate("https://www.crunchyroll.com/")
                Main.LoginOnly = "US_UnBlock_Check"
            Catch ex As Exception
            End Try
        ElseIf Main.LoginOnly = "US_UnBlock_Wait" Then
            Main.LoginOnly = "US_UnBlocck_Wait2nd"
            Main.Pause(2)
            Main.LoginOnly = "US_UnBlock_Check"
            WebBrowser1.Navigate("https://www.crunchyroll.com/")
        ElseIf Main.LoginOnly = "US_UnBlock_Check" Then
            Main.LoginOnly = "false"
            If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "Your detected location is United States of America.")) Then
                MsgBox("unlock successful", MsgBoxStyle.Information)
                Me.Close()
            Else
                MsgBox("unlock failes", MsgBoxStyle.Exclamation)
                Me.Close()
            End If
        Else


            If CBool(InStr(WebBrowser1.Url.ToString, "crunchyroll.com")) Then

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
                            Main.SeasonDropdownGrapp()
                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "wrapper container-shadow hover-classes")) Then
                            Main.b = True
                            Anime_Add.textBox2.Text = "Name of the Anime"
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.MassGrapp()
                        Else
                            MsgBox(Main.No_Stream, MsgBoxStyle.OkOnly)
                        End If
                    Catch ex As Exception
                        Main.LabelUpdate = "Status: idle"
                    End Try
                ElseIf c = False Then
                    If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "hardsub_lang")) Then
                        c = True
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        SoftSub.DownloadSubs()
                        Me.Close()
                    End If
                End If
                If Main.UserBowser = False Then
                    Main.WebbrowserURL = WebBrowser1.Url.ToString
                    Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                    Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                    Me.Close()
                End If
                'ElseIf CBool(InStr(WebBrowser1.Url.ToString, "https://www.anime-on-demand.de/anime/")) Then


            Else
                If Main.b = False Then
                    Main.WebbrowserURL = WebBrowser1.Url.ToString
                    Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                    Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                    Main.b = True
                    Main.UserBowser = True
                    For i As Integer = 20 To 0 Step -1
                        Main.Pause(1)
                        Anime_Add.StatusLabel.Text = "Status: scanning network traffic " + Math.Abs(i).ToString
                    Next
                    Anime_Add.StatusLabel.Text = "Status:  "
                    Dim FileLocation As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
                    Dim CurrentFile As String = Nothing
                    For Each File In FileLocation.GetFiles()
                        If InStr(File.FullName, "log.txt") Then
                            CurrentFile = File.FullName
                            Exit For
                        End If
                    Next
                    Dim logFileStream As FileStream = New FileStream(CurrentFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
                    Dim logFileReader As StreamReader = New StreamReader(logFileStream)
                    Dim line As String = Nothing
                    Dim HTMLString As String = Nothing
                    line = logFileReader.ReadLine

                    While (line IsNot Nothing)
                        line = logFileReader.ReadLine
                        If InStr(line, ".m3u8?") Then
                            If HTMLString = Nothing Then
                                HTMLString = line
                            Else
                                HTMLString = HTMLString + vbNewLine + line
                            End If
                            For i As Integer = 0 To 10
                                line = logFileReader.ReadLine
                                If InStr(line, " Host: ") Then
                                    HTMLString = HTMLString + vbNewLine + line
                                End If
                            Next
                        End If
                    End While
                    logFileReader.Close()
                    logFileStream.Close()
                    'MsgBox(HTMLString)
                    If InStr(HTMLString, ".m3u8?") Then
                        Anime_Add.StatusLabel.Text = "Status: m3u8 found, trying to start the download"
                        Main.LoggingBrowser = False
                        GeckoPreferences.Default("logging.config.LOG_FILE") = "log.txt"
                        GeckoPreferences.Default("logging.nsHttp") = 0
                        Dim URL As String = Nothing
                        Dim HTMLSplit() As String = HTMLString.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
                        For i As Integer = 0 To HTMLSplit.Count - 1
                            If InStr(HTMLSplit(i), ".m3u8?") Then
                                Dim URLPart2() As String = HTMLSplit(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim URLPart2Split2() As String = URLPart2(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim URLPart1() As String = HTMLSplit(i + 1).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                                Main.NonCR_URL = "https://" + URLPart1(1) + URLPart2Split2(0)
                                'MsgBox(Main.NonCR_URL)
                                'RichTextBox1.Text = RichTextBox1.Text + vbNewLine + URL_Final
                                t = New Thread(AddressOf Main.Grapp_non_CR)
                                t.Priority = ThreadPriority.Normal
                                t.IsBackground = True
                                t.Start()
                                Main.UserBowser = False
                                Exit For
                                Me.Close()
                            End If
                        Next
                    Else
                        Anime_Add.StatusLabel.Text = "Status: no m3u8 found, analyzing HTML content"
                        WebBrowser1.Navigate("view-source:" + Main.WebbrowserURL)
                        Main.Pause(3)
                        If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, ".m3u8")) Then
#Region "m3u8 suche"
                            Main.WebbrowserText = UrlDecode(WebBrowser1.Document.Body.OuterHtml)
                            If InStr(Main.WebbrowserText, ".m3u8?") Then
                            Else
                                Anime_Add.StatusLabel.Text = "Status: no m3u8 found"
                                Main.UserBowser = False
                                Me.Close()
                                Exit Sub
                            End If
                            Dim ii As Integer = 0
                            Dim Video_URI_Master As String = Nothing
                            Dim Video_URI_Master_Split1 As String() = Main.WebbrowserText.Split(New String() {".m3u8?"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim m3u8Link As String = Nothing
                            For i As Integer = 0 To Video_URI_Master_Split1.Count - 2
                                Dim Video_URI_Master_Split_Top As String() = Video_URI_Master_Split1(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Video_URI_Master_Split_Bottom As String() = Video_URI_Master_Split1(i + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                m3u8Link = Video_URI_Master_Split_Top(Video_URI_Master_Split_Top.Count - 1) + ".m3u8?" + Video_URI_Master_Split_Bottom(0)
                                Exit For
                            Next
                            m3u8Link = m3u8Link.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/")
                            Dim req As WebRequest
                            Dim res As WebResponse

                            req = WebRequest.Create(m3u8Link)

                            Try
                                res = req.GetResponse()
                                Dim ResponseStreamReader As StreamReader = New StreamReader(res.GetResponseStream)
                                Dim ResponseStreamString As String = ResponseStreamReader.ReadToEnd
                                If InStr(ResponseStreamString, "drm") Then
                                    Anime_Add.StatusLabel.Text = "Status: m3u8 found, but looks like it is DRM protected"
                                Else
                                    Anime_Add.StatusLabel.Text = "Status: m3u8 found, looks good"
                                    Main.Pause(1)
                                    Main.NonCR_URL = m3u8Link
                                    t = New Thread(AddressOf Main.Grapp_non_CR)
                                    t.Priority = ThreadPriority.Normal
                                    t.IsBackground = True
                                    t.Start()
                                    Me.Close()
                                End If
                            Catch ee As WebException
                                Anime_Add.StatusLabel.Text = "Status: error while loading m3u8"
                                Main.UserBowser = False
                                Me.Close()
                                Exit Sub
                                ' URL doesn't exists
                            Catch eee As Exception
                                'MsgBox(eee.ToString + vbNewLine + m3u8Link)
                            End Try
#End Region
                        End If
                        Anime_Add.StatusLabel.Text = "Status: idle"
                        Me.Close()
                        Main.UserBowser = False
                    End If
                End If
            End If
            If Main.UserBowser = False Then
                If Main.b = True Then
                    Anime_Add.StatusLabel.Text = "Status: idle"
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub GeckoFX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If WebBrowser1.Url.ToString = "about:blank" Then
            If Main.LoginOnly = "US_UnBlock" Then
                WebBrowser1.Navigate("https://www.crunchyroll.com/login")
            Else
                WebBrowser1.Navigate(Main.Startseite)
            End If
        End If
        Me.Icon = My.Resources.icon
        Main.UserBowser = True
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
        Try


            Button2.Enabled = False
            ScanTrue = True
            GeckoPreferences.Default("logging.config.LOG_FILE") = "log.txt"
            GeckoPreferences.Default("logging.nsHttp") = 3
            Main.LoggingBrowser = True
            Dim FileLocation As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
            Dim CurrentFile As String = Nothing
            For Each File In FileLocation.GetFiles()
                If InStr(File.FullName, "log.txt") Then
                    CurrentFile = File.FullName
                    Exit For
                End If
            Next
            Dim logFileStream As FileStream = New FileStream(CurrentFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
            Dim logFileReader As StreamReader = New StreamReader(logFileStream)
            logFileStream.SetLength(0)
            'WebBrowser1.Navigate(TextBox1.Text)
            Main.WebbrowserURL = WebBrowser1.Url.ToString
            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
            Main.b = True
            Main.UserBowser = True
            For i As Integer = 20 To 0 Step -1
                Main.Pause(1)
                Button2.Text = "network scan is in progess " + Math.Abs(i).ToString
            Next
            If InStr(Main.WebbrowserURL, "https://www.anime-on-demand.de/anime/") Then
                Main.WebbrowserTitle = WebBrowser1.Document.GetElementsByClassName("jw-title-primary").First.TextContent
                'Main.Thumbnail = WebBrowser1.Document.GetElementsByClassName("fullwidth-image anime-top-image").First.TextContent

            Else
                Main.WebbrowserTitle = WebBrowser1.DocumentTitle
            End If
            Dim line As String = Nothing
            Dim HTMLString As String = Nothing
            line = logFileReader.ReadLine

            While (line IsNot Nothing)
                line = logFileReader.ReadLine
                If InStr(line, ".m3u8?") Then
                    If HTMLString = Nothing Then
                        HTMLString = line
                    Else
                        HTMLString = HTMLString + vbNewLine + line
                    End If
                    For i As Integer = 0 To 10
                        line = logFileReader.ReadLine
                        If InStr(line, " Host: ") Then
                            HTMLString = HTMLString + vbNewLine + line
                        End If
                    Next
                End If
            End While
            logFileReader.Close()
            logFileStream.Close()
            'MsgBox(HTMLString)
            If InStr(HTMLString, ".m3u8?") Then
                Button2.Text = "found m3u8"
                Main.LoggingBrowser = False
                GeckoPreferences.Default("logging.config.LOG_FILE") = "log.txt"
                GeckoPreferences.Default("logging.nsHttp") = 0
                Dim URL As String = Nothing
                Dim HTMLSplit() As String = HTMLString.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To HTMLSplit.Count - 1
                    If InStr(HTMLSplit(i), ".m3u8?") Then
                        Dim URLPart2() As String = HTMLSplit(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLPart2Split2() As String = URLPart2(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLPart1() As String = HTMLSplit(i + 1).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Main.NonCR_URL = "https://" + URLPart1(1) + URLPart2Split2(0)
                        'MsgBox(Main.NonCR_URL)
                        'RichTextBox1.Text = RichTextBox1.Text + vbNewLine + URL_Final
                        t = New Thread(AddressOf Main.Grapp_non_CR)
                        t.Priority = ThreadPriority.Normal
                        t.IsBackground = True
                        t.Start()
                        Button2.Text = "Start network scan"
                        Exit For
                    End If
                Next
            End If
            ScanTrue = False
            Button2.Enabled = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.OkOnly)
            Button2.Enabled = True
            ScanTrue = False
        End Try
    End Sub

End Class
