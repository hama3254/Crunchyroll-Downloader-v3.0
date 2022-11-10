Imports Microsoft.Win32

Public Class Trackbar
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Main.HybridThread > 64 Then
            TrackBar1.Value = 64
        ElseIf Main.HybridThread < 2 Then
            TrackBar1.Value = 2
        Else
            TrackBar1.Value = Main.HybridThread
        End If
        Label1.Text = "Selecet thread count: " + TrackBar1.Value.ToString
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Label1.Text = "Selecet thread count: " + TrackBar1.Value.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Main.HybridThread = TrackBar1.Value
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Main.HybridThread = TrackBar1.Value
        My.Settings.HybridThread = Main.HybridThread

        Me.Close()
    End Sub
End Class