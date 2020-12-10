Module Subfolder
    Public SubFolder_automatic As String = "[automatic by Series and Season]"
    Public SubFolder_automatic2 As String = "[automatic by Series]"
    Public SubFolder_Nothing As String = "[ ignore subfolder ]"

    Public SubFolder_Value As String = "[ ignore subfolder ]"


    Public Function UseSubfolder(ByVal Series As String, ByVal Season As String, ByVal Path As String)
        Dim newPath As String = Path + "\"

        If SubFolder_Value = SubFolder_automatic Then

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

End Module
