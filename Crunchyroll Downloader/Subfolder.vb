Imports System.IO
Imports System.Text

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

        File.WriteAllText(Pfad, Content, Encoding.UTF8)

    End Sub

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

        Public Overrides Sub Write(ByVal msg As String)
            Try
                tBox.Parent.Invoke(New MethodInvoker(Sub()
                                                         If msg <> lastmsg Then
                                                             lastmsg = msg
                                                             tBox.AppendText(msg)

                                                         End If
                                                     End Sub))
            Catch ex As Exception
            End Try
        End Sub

        Public Overrides Sub WriteLine(ByVal msg As String)
            Write(msg & vbCrLf)
        End Sub
    End Class

End Module
