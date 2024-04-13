Option Strict On

Imports System.Net
Imports MetroFramework
Imports MetroFramework.Components

Public Class Queue

    Dim Manager As MetroStyleManager = Main.Manager
    Dim bs As BindingSource = New BindingSource

    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Manager.Owner = Me
        Me.StyleManager = Manager
        ListBox1.BackColor = Main.BackColorValue
        ListBox1.ForeColor = Main.ForeColorValue
        bs.DataSource = Main.ListBoxList
        ListBox1.DataSource = bs

        Btn_min.Image = Main.MinImg
        Btn_Close.Image = Main.CloseImg

    End Sub


    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub


    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter, Btn_Close.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Close.Image = My.Resources.main_close_dark_hover
        Else
            Btn_Close.Image = My.Resources.main_close_hover
        End If
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave, Btn_Close.LostFocus
        Btn_Close.Image = Main.CloseImg
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles UpdateListTimer.Tick
        If Main.ListBoxList.Count <> ListBox1.Items.Count Then
            bs.ResetBindings(False)
        End If

    End Sub

    Private Sub Queue_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Btn_Close.Location = New Point(Me.Width - 36, 1)
        Btn_min.Location = New Point(Me.Width - 67, 1)
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub RunQueue_CheckedChanged(sender As Object, e As EventArgs) Handles RunQueue.CheckedChanged
        RunQueueTimer.Enabled = RunQueue.Checked
        Main.RunningQueue = RunQueue.Checked
    End Sub

    Private Sub RunQueueTimer_Tick(sender As Object, e As EventArgs) Handles RunQueueTimer.Tick

        Try
            Dim ItemFinshedCount As Integer = 0
            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Main.Panel1.Controls.OfType(Of CRD_List_Item))

            For i As Integer = 0 To Item.Count - 1
                Debug.WriteLine(Item(i).GetIsStatusFinished().ToString)
                If Item(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next

            Main.RunningDownloads = Item.Count - ItemFinshedCount

        Catch ex As Exception
            Main.RunningDownloads = Main.Panel1.Controls.Count
        End Try

        If Main.RunningDownloads < Main.MaxDL Then
            If Main.ListBoxList.Count > 0 Then


                Dim UriUsed As String = ListBox1.GetItemText(Main.ListBoxList(0))

                    If Main.Grapp_RDY = True Then
                        Main.Grapp_RDY = False
                        Main.Text = "Status: loading ..."
                        Main.LoadBrowser(UriUsed)
                        Main.ListBoxList.Remove(UriUsed)
                        Main.b = False
                    End If
                End If


        End If
    End Sub



    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If RunQueue.Checked = True Then
            MessageBox.Show("Please stop the queue before removing entries", "Unable to comply.")
        Else
            Dim UriUsed As String = ListBox1.Text
            Main.ListBoxList.Remove(UriUsed)
        End If
    End Sub
End Class