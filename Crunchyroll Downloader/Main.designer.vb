<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits MetroFramework.Forms.MetroForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.TheTextBox = New System.Windows.Forms.RichTextBox()
        Me.ConsoleBar = New System.Windows.Forms.PictureBox()
        Me.MetroStyleExtender1 = New MetroFramework.Components.MetroStyleExtender(Me.components)
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.Btn_add = New System.Windows.Forms.Button()
        Me.Btn_Browser = New System.Windows.Forms.Button()
        Me.Btn_Settings = New System.Windows.Forms.Button()
        Me.Btn_min = New System.Windows.Forms.Button()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToggleDebugModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Funimation_Token = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckCRBetaTokenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddonHTMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer3OffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ThreadCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.MsgBoxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CRCookieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearAllSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItemBoundsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DummyItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ScanTimeout = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConsoleBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.balken
        resources.ApplyResources(Me.PictureBox5, "PictureBox5")
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.TabStop = False
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 3000
        '
        'Timer3
        '
        Me.Timer3.Interval = 1000
        '
        'TheTextBox
        '
        Me.TheTextBox.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.TheTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TheTextBox, "TheTextBox")
        Me.TheTextBox.Name = "TheTextBox"
        '
        'ConsoleBar
        '
        Me.ConsoleBar.BackColor = System.Drawing.Color.Transparent
        Me.ConsoleBar.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.balken
        resources.ApplyResources(Me.ConsoleBar, "ConsoleBar")
        Me.ConsoleBar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConsoleBar.Name = "ConsoleBar"
        Me.ConsoleBar.TabStop = False
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
        '
        'Timer4
        '
        Me.Timer4.Enabled = True
        Me.Timer4.Interval = 2500
        '
        'Btn_add
        '
        Me.Btn_add.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Btn_add, "Btn_add")
        Me.Btn_add.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_add.FlatAppearance.BorderSize = 0
        Me.Btn_add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_add.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_add
        Me.Btn_add.Name = "Btn_add"
        Me.Btn_add.UseVisualStyleBackColor = False
        '
        'Btn_Browser
        '
        Me.Btn_Browser.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Btn_Browser, "Btn_Browser")
        Me.Btn_Browser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Browser.FlatAppearance.BorderSize = 0
        Me.Btn_Browser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Browser.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_browser
        Me.Btn_Browser.Name = "Btn_Browser"
        Me.Btn_Browser.UseVisualStyleBackColor = False
        '
        'Btn_Settings
        '
        Me.Btn_Settings.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Btn_Settings, "Btn_Settings")
        Me.Btn_Settings.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Settings.FlatAppearance.BorderSize = 0
        Me.Btn_Settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Settings.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_settings
        Me.Btn_Settings.Name = "Btn_Settings"
        Me.Btn_Settings.UseVisualStyleBackColor = False
        '
        'Btn_min
        '
        Me.Btn_min.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Btn_min, "Btn_min")
        Me.Btn_min.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_min.FlatAppearance.BorderSize = 0
        Me.Btn_min.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_min.ForeColor = System.Drawing.Color.Transparent
        Me.Btn_min.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_mini
        Me.Btn_min.Name = "Btn_min"
        Me.Btn_min.UseVisualStyleBackColor = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Btn_Close, "Btn_Close")
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.FlatAppearance.BorderSize = 0
        Me.Btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.ForeColor = System.Drawing.Color.Transparent
        Me.Btn_Close.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToggleDebugModeToolStripMenuItem, Me.OpenSettingsToolStripMenuItem, Me.Funimation_Token, Me.CheckCRBetaTokenToolStripMenuItem, Me.AddonHTMLToolStripMenuItem, Me.Timer3OffToolStripMenuItem, Me.ThreadCount, Me.MsgBoxToolStripMenuItem, Me.CRCookieToolStripMenuItem, Me.ClearAllSettingsToolStripMenuItem, Me.ItemBoundsToolStripMenuItem, Me.DummyItemToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.ContextMenuStrip1, "ContextMenuStrip1")
        '
        'ToggleDebugModeToolStripMenuItem
        '
        Me.ToggleDebugModeToolStripMenuItem.Name = "ToggleDebugModeToolStripMenuItem"
        resources.ApplyResources(Me.ToggleDebugModeToolStripMenuItem, "ToggleDebugModeToolStripMenuItem")
        '
        'OpenSettingsToolStripMenuItem
        '
        Me.OpenSettingsToolStripMenuItem.Name = "OpenSettingsToolStripMenuItem"
        resources.ApplyResources(Me.OpenSettingsToolStripMenuItem, "OpenSettingsToolStripMenuItem")
        '
        'Funimation_Token
        '
        Me.Funimation_Token.Name = "Funimation_Token"
        resources.ApplyResources(Me.Funimation_Token, "Funimation_Token")
        '
        'CheckCRBetaTokenToolStripMenuItem
        '
        Me.CheckCRBetaTokenToolStripMenuItem.Name = "CheckCRBetaTokenToolStripMenuItem"
        resources.ApplyResources(Me.CheckCRBetaTokenToolStripMenuItem, "CheckCRBetaTokenToolStripMenuItem")
        '
        'AddonHTMLToolStripMenuItem
        '
        Me.AddonHTMLToolStripMenuItem.Name = "AddonHTMLToolStripMenuItem"
        resources.ApplyResources(Me.AddonHTMLToolStripMenuItem, "AddonHTMLToolStripMenuItem")
        '
        'Timer3OffToolStripMenuItem
        '
        Me.Timer3OffToolStripMenuItem.Name = "Timer3OffToolStripMenuItem"
        resources.ApplyResources(Me.Timer3OffToolStripMenuItem, "Timer3OffToolStripMenuItem")
        '
        'ThreadCount
        '
        Me.ThreadCount.Name = "ThreadCount"
        resources.ApplyResources(Me.ThreadCount, "ThreadCount")
        '
        'MsgBoxToolStripMenuItem
        '
        Me.MsgBoxToolStripMenuItem.Name = "MsgBoxToolStripMenuItem"
        resources.ApplyResources(Me.MsgBoxToolStripMenuItem, "MsgBoxToolStripMenuItem")
        '
        'CRCookieToolStripMenuItem
        '
        Me.CRCookieToolStripMenuItem.Name = "CRCookieToolStripMenuItem"
        resources.ApplyResources(Me.CRCookieToolStripMenuItem, "CRCookieToolStripMenuItem")
        '
        'ClearAllSettingsToolStripMenuItem
        '
        Me.ClearAllSettingsToolStripMenuItem.Name = "ClearAllSettingsToolStripMenuItem"
        resources.ApplyResources(Me.ClearAllSettingsToolStripMenuItem, "ClearAllSettingsToolStripMenuItem")
        '
        'ItemBoundsToolStripMenuItem
        '
        Me.ItemBoundsToolStripMenuItem.Name = "ItemBoundsToolStripMenuItem"
        resources.ApplyResources(Me.ItemBoundsToolStripMenuItem, "ItemBoundsToolStripMenuItem")
        '
        'DummyItemToolStripMenuItem
        '
        Me.DummyItemToolStripMenuItem.Name = "DummyItemToolStripMenuItem"
        resources.ApplyResources(Me.DummyItemToolStripMenuItem, "DummyItemToolStripMenuItem")
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'ScanTimeout
        '
        Me.ScanTimeout.Interval = 1000
        '
        'Main
        '
        Me.ApplyImageInvert = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.Btn_min)
        Me.Controls.Add(Me.Btn_Settings)
        Me.Controls.Add(Me.Btn_Browser)
        Me.Controls.Add(Me.Btn_add)
        Me.Controls.Add(Me.TheTextBox)
        Me.Controls.Add(Me.ConsoleBar)
        Me.Controls.Add(Me.PictureBox5)
        Me.ForeColor = System.Drawing.Color.Black
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.Resizable = True
        Me.Style = MetroFramework.MetroColorStyle.Orange
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConsoleBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents TheTextBox As RichTextBox
    Friend WithEvents ConsoleBar As PictureBox
    Friend WithEvents MetroStyleExtender1 As MetroFramework.Components.MetroStyleExtender
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
    Friend WithEvents Timer4 As Timer
    Friend WithEvents Btn_add As Button
    Friend WithEvents Btn_Browser As Button
    Friend WithEvents Btn_Settings As Button
    Friend WithEvents Btn_min As Button
    Friend WithEvents Btn_Close As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToggleDebugModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Funimation_Token As ToolStripMenuItem
    Friend WithEvents CheckCRBetaTokenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddonHTMLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer3OffToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ThreadCount As ToolStripMenuItem
    Friend WithEvents MsgBoxToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CRCookieToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearAllSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ItemBoundsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DummyItemToolStripMenuItem As ToolStripMenuItem
    Public WithEvents Panel1 As Panel
    Friend WithEvents ScanTimeout As Timer
End Class
