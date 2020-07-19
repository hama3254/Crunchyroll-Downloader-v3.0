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
End Class