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

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class