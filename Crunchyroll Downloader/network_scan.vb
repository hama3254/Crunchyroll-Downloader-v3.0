Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Text

Public Class network_scan


    'If b = False Then
    '            m3u8List.Clear()
    '            mpdList.Clear()
    '            txtList.Clear()
    '            Button2.Enabled = False
    '            ScanTrue = True
    '            LogBrowserData = True
    '            NetworkScanEnd()
    '        End If

    Dim Manager As MetroStyleManager = Main.Manager
    Dim SubtitleFormat As String = Nothing
    Dim VideoStreams As New List(Of String)
    Dim AudioStreams As New List(Of String)

    Private Sub Network_scan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox2.Enabled = False
        Manager.Owner = Me
        Me.StyleManager = Manager
        Btn_Close.Image = Main.CloseImg
        Btn_min.Image = Main.MinImg
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception
        End Try
        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        pictureBox4.Enabled = False
        pictureBox4.Image = My.Resources.main_button_download_deactivate
    End Sub

    Sub CheckVideoAudio(ByVal url As String)
        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim cmd As String = "-headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + url + Chr(34)                            'start ffmpeg with command strFFCMD string

        'MsgBox(cmd)
        Dim ffmpegOutput As String = Nothing
        Dim ffmpegOutputLine As String = Nothing
        Dim ffmpegOutputLine2 As String = Nothing

        Dim NetworkScanTime As String = Nothing

        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        Dim proc As New Process
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        sr = proc.StandardError 'standard error is used by ffmpeg

        Do
            ffmpegOutputLine = sr.ReadLine
            ffmpegOutput = ffmpegOutput + vbNewLine + ffmpegOutputLine

        Loop Until proc.HasExited 'And ffmpegOutputLine = Nothing Or ffmpegOutputLine = ""

        Dim ffmpegOutput2() As String = ffmpegOutput.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To ffmpegOutput2.Count - 1


            If CBool(InStr(ffmpegOutput2(i), ": Video:")) Then

                Dim ZeileReso() As String = ffmpegOutput2(i).Split(New String() {" ["}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso3() As String = ffmpegOutput2(i).Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso4() As String = ZeileReso3(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)



                If CBool(InStr(ZeileReso2(ZeileReso2.Count - 1), ", ")) Then

                    Dim ZeileReso5() As String = ZeileReso2(ZeileReso2.Count - 1).Split(New String() {", "}, System.StringSplitOptions.RemoveEmptyEntries)
                    ComboBox3.Items.Add(ZeileReso5(0).Trim + ":--:" + ZeileReso4(1))

                Else
                    ComboBox3.Items.Add(ZeileReso2(ZeileReso2.Count - 1).Trim + ":--:" + ZeileReso4(1))

                End If
            ElseIf CBool(InStr(ffmpegOutput2(i), ": Audio:")) Then

                Dim ZeileStream() As String = ffmpegOutput2(i).Split(New String() {": Audio:"}, System.StringSplitOptions.RemoveEmptyEntries)

                ComboBox3.Items.Add("Audio:" + ZeileStream(1))


            End If

        Next
    End Sub

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox4.MouseEnter
        If pictureBox4.Enabled = True Then
            pictureBox4.Image = My.Resources.main_button_download_hovert
        Else
            pictureBox4.Image = My.Resources.main_button_download_deactivate
        End If

    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        If pictureBox4.Enabled = True Then
            pictureBox4.Image = My.Resources.main_button_download_default
        Else
            pictureBox4.Image = My.Resources.main_button_download_deactivate
        End If

    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Network_scan_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Btn_Close.Location = New Point(Me.Width - 40, 1)
        Btn_min.Location = New Point(Me.Width - 68, 10)
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox3.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Text = Nothing
        ComboBox2.Items.Clear()
        ComboBox2.Text = Nothing
        ComboBox2.Enabled = True
        SubtitleFormat = Nothing
        pictureBox4.Enabled = False
        pictureBox4.Image = My.Resources.main_button_download_deactivate
        If ComboBox1.SelectedItem.ToString = "Video Stream" Then
            If Main.m3u8List.Count > 0 Then
                For i As Integer = 0 To Main.m3u8List.Count - 1
                    ComboBox2.Items.Add(Main.m3u8List.Item(i))
                Next
            ElseIf Main.mpdList.Count > 0 Then
                If Main.mpdList.Count > 0 Then
                    For i As Integer = 0 To Main.mpdList.Count - 1
                        ComboBox2.Items.Add(Main.mpdList.Item(i))
                    Next
                End If
            End If
        ElseIf ComboBox1.SelectedItem.ToString = "Subtile" Then
            If Main.txtList.Count > 0 Then
                For i As Integer = 0 To Main.txtList.Count - 1
                    ComboBox2.Items.Add(Main.txtList.Item(i))
                Next
            End If
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        SubtitleFormat = Nothing
        pictureBox4.Enabled = False
        pictureBox4.Image = My.Resources.main_button_download_deactivate
        ComboBox3.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Text = Nothing

        NetworkStatusLabel.Text = "Checking input..."
        Pause(1)
        'If  CBool(InStr(ComboBox2.Text, ".mpd") Or  CBool(InStr(ComboBox2.Text, ".m3u8") Then

        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim cmd As String = "-headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + ComboBox2.Text + Chr(34)                            'start ffmpeg with command strFFCMD string

        'MsgBox(cmd)
        Dim ffmpegOutput As String = Nothing
        Dim ffmpegOutputLine As String = Nothing
        Dim ffmpegOutputLine2 As String = Nothing

        Dim NetworkScanTime As String = Nothing

        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        Dim proc As New Process
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        sr = proc.StandardError 'standard error is used by ffmpeg

        Do
            ffmpegOutputLine = sr.ReadLine
            ffmpegOutput = ffmpegOutput + vbNewLine + ffmpegOutputLine

        Loop Until proc.HasExited 'And ffmpegOutputLine = Nothing Or ffmpegOutputLine = ""

        Dim ffmpegOutput2() As String = ffmpegOutput.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To ffmpegOutput2.Count - 1

            If CBool(InStr(ffmpegOutput2(i), ": Video:")) Then
                Dim ZeileReso() As String = ffmpegOutput2(i).Split(New String() {" ["}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso3() As String = ffmpegOutput2(i).Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso4() As String = ZeileReso3(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)


                If CBool(InStr(ZeileReso2(ZeileReso2.Count - 1), ", ")) Then
                    Dim ZeileReso5() As String = ZeileReso2(ZeileReso2.Count - 1).Split(New String() {", "}, System.StringSplitOptions.RemoveEmptyEntries)
                    ComboBox3.Items.Add(ZeileReso5(0).Trim + ":--:" + ZeileReso4(1))

                Else
                    ComboBox3.Items.Add(ZeileReso2(ZeileReso2.Count - 1).Trim + ":--:" + ZeileReso4(1))

                End If



            ElseIf CBool(InStr(ffmpegOutput2(i), ": Audio:")) Then
                Dim ZeileStream() As String = ffmpegOutput2(i).Split(New String() {": Audio:"}, System.StringSplitOptions.RemoveEmptyEntries)


                ComboBox3.Items.Add("Audio:" + ZeileStream(1))

            ElseIf CBool(InStr(ffmpegOutput2(i), "Duration: N/A, bitrate: N/A")) Then

            ElseIf CBool(InStr(ffmpegOutput2(i), "Subtitle: ")) Then
                Dim Format As String() = ffmpegOutput2(i).Split(New String() {"Subtitle: "}, System.StringSplitOptions.RemoveEmptyEntries)
                SubtitleFormat = Format(1)

            ElseIf CBool(InStr(ffmpegOutput2(i), "Duration: ")) Then
                Dim ZeitGesamt As String() = ffmpegOutput2(i).Split(New String() {"Duration: "}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeitGesamt2 As String() = ZeitGesamt(1).Split(New [Char]() {System.Convert.ToChar(".")})
                NetworkScanTime = ZeitGesamt2(0)
            ElseIf CBool(InStr(ffmpegOutput2(i), "At least one output file must be specified")) Then

            ElseIf CBool(InStr(ffmpegOutput2(i), "Invalid data found when processing input")) Then
                NetworkStatusLabel.Text = "Invalid data found when processing input"
                Exit Sub
            End If
        Next
        If SubtitleFormat IsNot Nothing Then
            NetworkStatusLabel.Text = "Subtitle found with format: " + SubtitleFormat
            pictureBox4.Enabled = True
            pictureBox4.Image = My.Resources.main_button_download_default
        ElseIf NetworkScanTime = Nothing Then
            If ComboBox3.Items.Count > 0 Then
                NetworkStatusLabel.Text = "Duration check failed but it found valid streams."
                ComboBox3.Enabled = True
            Else
                NetworkStatusLabel.Text = "No video stream found in that url."
            End If
        Else
            NetworkStatusLabel.Text = "Video found with a duration of: " + NetworkScanTime
            ComboBox3.Enabled = True
        End If




    End Sub

    Private Sub ComboBox3_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        pictureBox4.Enabled = True
        pictureBox4.Image = My.Resources.main_button_download_default
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        pictureBox4.Enabled = False
        pictureBox4.Image = My.Resources.main_button_download_deactivate

        Dim FileName As String = Main.WebbrowserTitle.Replace(" - Watch on VRV", "").Replace("Free Streaming", "").Replace("Tubi", "")
        FileName = Main.RemoveExtraSpaces(String.Join(" ", FileName.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)) 'System.Text.RegularExpressions.Regex.Replace(FileName, "[^\w\\-]", " "))
        Dim FilePfad As String = Main.Pfad + "\" + FileName



        If CBool(InStr(ComboBox3.Text, "Audio:")) Then
            FilePfad = FilePfad + ".mka"


            Dim m3u8Final As String = "-i " + Chr(34) + ComboBox2.Text + Chr(34) + " -c:a copy"




            'MsgBox(m3u8Final)
            Dim DisplayReso As String = "Audio"
            Dim Pfad2 As String = Chr(34) + FilePfad + Chr(34)
            Dim Title As String = FileName '+ ".mp4"
            Dim L1Name_Split As String() = Main.WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1)
            Me.Invoke(New Action(Function() As Object
                                     Main.ListItemAdd(Main.Pfad, L1Name, Title, DisplayReso, "Unknown", "None", "", m3u8Final, Pfad2)
                                     'Main.liList.Add(My.Resources.htmlvorThumbnail + "" + My.Resources.htmlnachTumbnail + "<br>" + Title + My.Resources.htmlvorAufloesung + "[Auto]" + My.Resources.htmlvorSoftSubs + vbNewLine + "None" + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + Title + "-->")
                                     Return Nothing
                                 End Function))
        Else

            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            If Main.WebbrowserCookie = Nothing Then
            Else
                client0.Headers.Add(HttpRequestHeader.Cookie, Main.WebbrowserCookie)
            End If

            Dim RequestURL As String = ComboBox2.Text

            ComboBox2.Text = Nothing

            Dim RequestReso As String = Nothing
            Dim RequestMap As String = Nothing
            If ComboBox3.Enabled = True Then
                Dim ResoSplit() As String = ComboBox3.Text.Split(New String() {":--:"}, System.StringSplitOptions.RemoveEmptyEntries)
                RequestReso = ResoSplit(0)
                RequestMap = ResoSplit(1)
            End If

            If ComboBox1.SelectedItem.ToString = "Video Stream" Then

                If CBool(InStr(RequestURL, ".m3u8")) Then
                    Main.m3u8List.Remove(RequestURL)
                ElseIf CBool(InStr(RequestURL, ".mpd")) Then
                    Main.mpdList.Remove(RequestURL)
                End If

                'Me.Invoke(New Action(Function()
                '                         MsgBox(m3u8_Master_url)
                '                         Return Nothing
                '                     End Function))
                'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\Test.txt", text, False)
                Dim thumbnail As String() = Nothing
                Dim thumbnail2 As String() = Nothing
                Dim thumbnail4 As String = "https://abload.de/img/main-delx4krg.png"
                Try
                    If CBool(InStr(Main.WebbrowserText, "thumbnail")) Then
                        thumbnail = Main.WebbrowserText.Split(New String() {"thumbnail"}, System.StringSplitOptions.RemoveEmptyEntries)
                    End If
                Catch ex As Exception

                End Try
                Try
                    For i As Integer = 0 To thumbnail.Count - 1
                        If CBool(InStr(thumbnail(i), ".jpg")) Then
                            If CBool(InStr(thumbnail(i), "https:")) Then
                                thumbnail2 = thumbnail(i).Split(New String() {".jpg"}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim thumbnail3 As String() = thumbnail2(0).Split(New String() {"https:"}, System.StringSplitOptions.RemoveEmptyEntries)
                                thumbnail4 = "https:" + thumbnail3(thumbnail3.Count - 1).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\/", "/") + ".jpg"
                                Exit For
                            End If
                        End If
                    Next

                Catch ex As Exception
                End Try

                Dim m3u8Final As String = "-i " + Chr(34) + RequestURL + Chr(34) + " -map " + RequestMap + " -map 0:a" + Main.ffmpeg_command

                If Main.HybridMode = True Then
                    Dim client As New WebClient
                    client.Encoding = System.Text.Encoding.UTF8
                    Dim text As String = client.DownloadString(RequestURL)

                    If CBool(InStr(text, "RESOLUTION=")) Then 'master m3u8 no fragments 
                        Dim new_m3u8() As String = text.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                        For i2 As Integer = 0 To new_m3u8.Count - 1

                            'MsgBox("x" + Main.Resu.ToString)
                            If CBool(InStr(new_m3u8(i2), "x" + RequestReso.ToString)) = True Then

                                m3u8Final = "-i " + Chr(34) + new_m3u8(i2 + 1) + Chr(34) + Main.ffmpeg_command


                                Exit For

                            End If

                        Next

                    End If


                End If





                'MsgBox(m3u8Final)
                Dim DisplayReso As String = RequestReso.ToString + "p"
                Dim Pfad2 As String = Chr(34) + FilePfad + Main.VideoFormat + Chr(34)
                Dim Title As String = FileName '+ ".mp4"
                Dim L1Name_Split As String() = Main.WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim L1Name As String = L1Name_Split(1)
                Me.Invoke(New Action(Function() As Object
                                         Main.ListItemAdd(Main.Pfad, L1Name, Title, DisplayReso, "Unknown", "None", thumbnail4, m3u8Final, Pfad2)
                                         'Main.liList.Add(My.Resources.htmlvorThumbnail + thumbnail4 + My.Resources.htmlnachTumbnail + "<br>" + Title + My.Resources.htmlvorAufloesung + "[Auto]" + My.Resources.htmlvorSoftSubs + vbNewLine + "None" + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + Title + "-->")
                                         Return Nothing
                                     End Function))
            ElseIf ComboBox1.SelectedItem.ToString = "Subtile" Then
                Dim CheckFile As String = Nothing
                Main.txtList.Remove(RequestURL)
                If SubtitleFormat IsNot Nothing Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + "." + SubtitleFormat)
                    CheckFile = FilePfad + "." + SubtitleFormat
                ElseIf CBool(InStr(RequestURL, ".txt")) Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + ".txt")
                    CheckFile = FilePfad + ".txt"
                ElseIf CBool(InStr(RequestURL, ".vtt")) Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + ".vtt")
                    CheckFile = FilePfad + ".vtt"
                ElseIf CBool(InStr(RequestURL, ".srt")) Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + ".srt")
                    CheckFile = FilePfad + ".srt"
                ElseIf CBool(InStr(RequestURL, ".ass")) Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + ".ass")
                    CheckFile = FilePfad + ".ass"
                ElseIf CBool(InStr(RequestURL, ".ssa")) Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + ".ssa")
                    CheckFile = FilePfad + ".ssa"
                ElseIf CBool(InStr(RequestURL, ".dfxp")) Then
                    client0.DownloadFileAsync(New Uri(RequestURL), FilePfad + ".dfxp")
                    CheckFile = FilePfad + ".dfxp"
                End If

                Pause(5)
                If File.Exists(CheckFile) Then
                    NetworkStatusLabel.Text = "Subtitles have been Downloaded"
                Else
                    Pause(5)
                    If File.Exists(CheckFile) Then
                        NetworkStatusLabel.Text = "Subtitles have been Downloaded"
                    Else
                        'NetworkStatusLabel.Text = "Subtitles have been Downloaded"
                    End If
                End If

            End If

        End If


    End Sub

    Private Sub ComboBox2_Click(sender As Object, e As EventArgs) Handles ComboBox2.Click
        ComboBox3.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Text = Nothing
        ComboBox2.Items.Clear()
        ComboBox2.Text = Nothing
        SubtitleFormat = Nothing
        pictureBox4.Enabled = False
        pictureBox4.Image = My.Resources.main_button_download_deactivate
        If ComboBox1.SelectedItem.ToString = "Video Stream" Then
            If Main.m3u8List.Count > 0 Then
                For i As Integer = 0 To Main.m3u8List.Count - 1
                    ComboBox2.Items.Add(Main.m3u8List.Item(i))
                Next
            ElseIf Main.mpdList.Count > 0 Then
                If Main.mpdList.Count > 0 Then
                    For i As Integer = 0 To Main.mpdList.Count - 1
                        ComboBox2.Items.Add(Main.mpdList.Item(i))
                    Next
                End If
            End If
        ElseIf ComboBox1.SelectedItem.ToString = "Subtile" Then
            If Main.txtList.Count > 0 Then
                For i As Integer = 0 To Main.txtList.Count - 1
                    ComboBox2.Items.Add(Main.txtList.Item(i))
                Next
            End If
        End If
    End Sub
End Class