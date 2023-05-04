Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.Remoting
Imports System.Text
Imports System.Windows.Forms.VisualStyles

Module Subfolder
    Public SubFolder_automatic As String = "[automatic by Series and Season]"
    Public SubFolder_automatic2 As String = "[automatic by Series]"
    Public SubFolder_automatic_old As String = "[automatic: Series/Season]"
    Public SubFolder_Nothing As String = "[ ignore subfolder ]"

    Public SubFolder_Value As String = "[ ignore subfolder ]"

    Public Function CleanJSON(ByVal JSON As String) As String
        JSON = JSON.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
        While CBool(InStr(JSON, "\"))
            Dim index As Integer = InStr(JSON, "\")
            Dim myName As New StringBuilder(JSON)
            myName.Remove(index - 1, 2)
            JSON = myName.ToString
        End While
        Return JSON

    End Function

    Public Function UseSubfolder(ByVal Series As String, ByVal Season As String, ByVal Path As String) As String
        Dim newPath As String = Path + "\"

        If SubFolder_Value = SubFolder_automatic Or SubFolder_Value = SubFolder_automatic_old Then

            newPath = Path + "\" + Series + "\" + Season + "\"

        ElseIf SubFolder_Value = SubFolder_automatic2 Then

            newPath = Path + "\" + Series + "\"

        ElseIf SubFolder_Value = SubFolder_Nothing Then

            newPath = Path + "\"

        Else

            newPath = Path + "\" + SubFolder_Value + "\"

        End If
        Debug.WriteLine(newPath)

        Return newPath.Replace("\\", "\")
    End Function



    Public Sub WriteText(ByVal Pfad As String, ByVal Content As String)

        If Pfad.Length > 255 Then
            Pfad = "\\?\" + Pfad
        End If

        Dim utf8WithoutBom2 As New System.Text.UTF8Encoding(False)
        Using sink As New StreamWriter(Pfad, False, utf8WithoutBom2)
            sink.WriteLine(Content)
        End Using

        'File.WriteAllText(Pfad, Content, Encoding.UTF8)

    End Sub

    Public Function ReadText(ByVal Pfad As String) As String

        If Pfad.Length > 255 Then
            Pfad = "\\?\" + Pfad
        End If

        Return File.ReadAllText(Pfad, Encoding.UTF8)

    End Function

    Public Function GeräteID() As String
        Dim rnd As New Random
        Dim possible As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim HWID As String = Nothing

        For i As Integer = 0 To 15
            Dim ZufallsZahl As Integer = rnd.Next(1, 33)
            HWID = HWID + possible(ZufallsZahl)
        Next
        Return "CRD-Temp-File-" + HWID
    End Function

    Public Function GeräteID2() As String
        Dim rnd As New Random
        Dim possible As String = "56789abcdefghijklmnopqrstuvwxyz01234ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim HWID As String = Nothing

        For i As Integer = 0 To 15
            Dim ZufallsZahl As Integer = rnd.Next(1, 33)
            HWID = HWID + possible(ZufallsZahl)
        Next
        Return "CRD-Temp-File-" + HWID
    End Function

    Class TextBoxTraceListener
        Inherits TraceListener

        Private tBox As RichTextBox
        Dim lastmsg As String = Nothing


        Public Sub New(ByVal box As RichTextBox)
            Me.tBox = box
        End Sub

        Dim WithEvents BG As BackgroundWorker

        Sub RunBG(ByVal sender As Object, e As DoWorkEventArgs) Handles BG.DoWork
            Dim msg As String = CStr(e.Argument)
            If msg <> lastmsg Then
                lastmsg = msg
            Else
                Exit Sub
            End If

            Try
                tBox.Parent.Invoke(New MethodInvoker(Sub()
                                                         tBox.AppendText(msg)
                                                     End Sub))
            Catch ex As Exception

            End Try

        End Sub

        Sub StopBG(sender As Object, e As EventArgs)
            Dim T As Timer = CType(sender, Timer)
            If BG.IsBusy Then
                BG.CancelAsync()
                T.Dispose()
            Else
                T.Dispose()
            End If
        End Sub

        Public Overrides Sub Write(ByVal msg As String)

            Dim time As New Timer
            AddHandler time.Tick, AddressOf StopBG
            time.Interval = 500
            time.Start()
            BG = New BackgroundWorker
            BG.WorkerSupportsCancellation = True
            BG.RunWorkerAsync(msg)





        End Sub

        Public Overrides Sub WriteLine(ByVal msg As String)
            Write(msg & vbCrLf)
        End Sub
    End Class
    Public Function ShortingName(ByVal FullPath As String) As String

        Dim FileName As String = Path.GetFileNameWithoutExtension(FullPath)

        Dim Segements() As String = FileName.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)

        Dim ReturnName As String = Nothing

        For i As Integer = 0 To Segements.Count - 1
            If Segements(i).Count > 5 Then ' 6 chars or more, split at 5
                ReturnName = ReturnName + Segements(i).Substring(0, 3) + ". "


            ElseIf Segements(i).Count > 3 Then ' 4 or 5 can stay
                ReturnName = ReturnName + Segements(i) + " "
            End If
        Next

        If ReturnName = Nothing Then
            ReturnName = FullPath
        Else
            ReturnName = Path.GetDirectoryName(FullPath) + "\" + ReturnName + Path.GetExtension(FullPath)
        End If

        Return ReturnName
    End Function
    Public Function FFMPEG_Audio(ByVal file As String) As Integer

        Dim proc As New Process
        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader

        Dim cmd As String = "-i " + Chr(34) + file + Chr(34) 'start ffmpeg with command strFFCMD string
        Dim ffmpegOutput As String = Nothing
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        sr = proc.StandardError 'standard error is used by ffmpeg

        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau




        Do
            ffmpegOutput = ffmpegOutput + sr.ReadToEnd

        Loop Until proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish


        Dim Streams() As String = ffmpegOutput.Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)

        Dim Tracks As Integer = 0

        For i As Integer = 0 To Streams.Count - 1
            If CBool(InStr(Streams(i), ": Audio:")) Then
                Tracks = Tracks + 1

            End If
        Next

        Return Tracks

    End Function

    Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As IntPtr) As IntPtr

    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr


End Module
