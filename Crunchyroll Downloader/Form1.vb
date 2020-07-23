Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If InStr(RichTextBox1.Text, ".srt") Then
            Dim SubTitle1() As String = RichTextBox1.Text.Split(New String() {".srt"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SubTitle2() As String = SubTitle1(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            TextBox1.Text = SubTitle2(SubTitle2.Count - 1) + ".srt"
        Else
            TextBox1.Text = "nope"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim SplittString As String = Nothing
        If InStr(RichTextBox1.Text, My.Resources.Funimation_Subtitle_String) Then
            SplittString = My.Resources.Funimation_Subtitle_String
        ElseIf InStr(RichTextBox1.Text, My.Resources.Funimation_Subtitle_String2) Then
            SplittString = My.Resources.Funimation_Subtitle_String2
        Else
            'some error
        End If
        Dim SubTitle1() As String = RichTextBox1.Text.Split(New String() {SplittString}, System.StringSplitOptions.RemoveEmptyEntries)
        MsgBox(SubTitle1.Count.ToString)
        Dim Subs_in_srt As New List(Of String)
        Dim Subs_in_vtt As New List(Of String)
        Dim Subs_in_dfxp As New List(Of String)

        For i As Integer = 0 To SubTitle1.Count - 1
            Dim SubTitle2() As String = SubTitle1(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            ComboBox1.Items.Add(SubTitle2(SubTitle2.Count - 1))
            'If InStr(SubTitle2(SubTitle2.Count - 1), ".srt") Then
            '    Subs_in_srt.Add(SubTitle2(SubTitle2.Count - 1))
            'ElseIf InStr(SubTitle2(SubTitle2.Count - 1), ".vtt") Then
            '    Subs_in_vtt.Add(SubTitle2(SubTitle2.Count - 1))
            'ElseIf InStr(SubTitle2(SubTitle2.Count - 1), ".dfxp") Then
            '    Subs_in_dfxp.Add(SubTitle2(SubTitle2.Count - 1))
            'End If
        Next
        'Dim UsedSub As String = Nothing
        'If Subs_in_srt.Count > 0 Then
        '    ComboBox1.Items.Add(Subs_in_srt.Item(0))
        'ElseIf Subs_in_vtt.Count > 0 Then
        '    ComboBox1.Items.Add(Subs_in_vtt.Item(0))
        'ElseIf Subs_in_dfxp.Count > 0 Then
        '    ComboBox1.Items.Add(Subs_in_dfxp.Item(0))
        'End If
    End Sub
End Class