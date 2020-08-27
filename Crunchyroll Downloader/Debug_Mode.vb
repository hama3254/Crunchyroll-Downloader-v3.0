Public Class Debug_Mode
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        ComboBox1.Items.Clear()
        For i As Integer = 0 To Main.m3u8List.Count - 1
            ComboBox1.Items.Add(Main.m3u8List.Item(i))
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        RichTextBox1.Text = ComboBox1.Text
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = ".txt" Then
            ComboBox1.Items.Clear()
            For i As Integer = 0 To Main.txtList.Count - 1
                ComboBox1.Items.Add(Main.txtList.Item(i))
            Next
        ElseIf ComboBox2.Text = ".m3u8" Then
            ComboBox1.Items.Clear()
            For i As Integer = 0 To Main.m3u8List.Count - 1
                ComboBox1.Items.Add(Main.m3u8List.Item(i))
            Next
        ElseIf ComboBox2.Text = ".mpd" Then
            ComboBox1.Items.Clear()
            For i As Integer = 0 To Main.mpdList.Count - 1
                ComboBox1.Items.Add(Main.mpdList.Item(i))
            Next
        End If
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim HTMLString As String = RichTextBox1.Text
        Dim HTMLSplit() As String = HTMLString.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To HTMLSplit.Count - 1
            If InStr(HTMLSplit(i), ".mpd?") Then
                Dim URLPart2() As String = HTMLSplit(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLPart2Split2() As String = URLPart2(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLPart1() As String = HTMLSplit(i).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                RichTextBox2.Text = "https://" + URLPart1(1) + URLPart2Split2(0)
                'MsgBox(Main.NonCR_URL)
                'RichTextBox1.Text = RichTextBox1.Text + vbNewLine + URL_Final
                Exit For
            ElseIf InStr(HTMLSplit(i), ".m3u8?") Then
                Dim URLPart2() As String = HTMLSplit(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLPart2Split2() As String = URLPart2(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLPart1() As String = HTMLSplit(i).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                RichTextBox2.Text = "https://" + URLPart1(1) + URLPart2Split2(0)
                'MsgBox(Main.NonCR_URL)
                'RichTextBox1.Text = RichTextBox1.Text + vbNewLine + URL_Final
                Exit For
            ElseIf InStr(HTMLSplit(i), ".txt?") Then
                Dim URLPart2() As String = HTMLSplit(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLPart2Split2() As String = URLPart2(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim URLPart1() As String = HTMLSplit(i).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                RichTextBox2.Text = "https://" + URLPart1(1) + URLPart2Split2(0)
                'MsgBox(Main.NonCR_URL)
                'RichTextBox1.Text = RichTextBox1.Text + vbNewLine + URL_Final
                Exit For
            End If
        Next
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        'Main.FFMPEG_Reso(RichTextBox2.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'MsgBox(Main.ResoAvalibe)
    End Sub
End Class