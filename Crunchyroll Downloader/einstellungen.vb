Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Text.RegularExpressions

Public Class Einstellungen
    Inherits MetroForm

    Dim Manager As MetroStyleManager = Main.Manager
    Dim LastVersionString As String = "v3.8-Beta"

    Private Sub Einstellungen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label6.Text = "You have: v" + Application.ProductVersion.ToString

        BackgroundWorker1.RunWorkerAsync()


        'CR_Anime_Folge = CR_Name_Staffel0_Folge1(1)
        'If GitHubLastTag1(0)


        Manager.Owner = Me
        Me.StyleManager = Manager


        ProfileTextBox.Text = Main.ProfileFolder
        TempTB.Text = Main.TempFolder
        LeadingZeroDD.SelectedIndex = Main.LeadingZero

        Bitrate_Funi.SelectedIndex = Main.Funimation_Bitrate

        CB_Ignore.SelectedIndex = Main.IgnoreSeason

        If Main.IncludeLangName = True Then
            CB_SoftSubSettings.SelectedIndex = 1
        Else
            CB_SoftSubSettings.SelectedIndex = 0
        End If

        If Main.LangNameType = 1 Then
            LangNameType_DD.SelectedIndex = 1
        ElseIf Main.LangNameType = 2 Then
            LangNameType_DD.SelectedIndex = 2
        Else
            LangNameType_DD.SelectedIndex = 0
        End If

        If Main.KodiNaming = True Then
            KodiSupport.Checked = True
        End If

        If Main.DarkModeValue = True Then
            DarkMode.Checked = True
            GroupBoxColor(Color.FromArgb(150, 150, 150))
            pictureBox1.Image = Main.CloseImg
        Else
            GroupBoxColor(Color.FromArgb(0, 0, 0))
            DarkMode.Checked = False
        End If

        TabControl1.SelectedIndex = 0
        For i As Integer = 0 To Main.SoftSubs.Count - 1
            If Main.SoftSubs(i) = "deDE" Then
                CBdeDE.Checked = True
            ElseIf Main.SoftSubs(i) = "enUS" Then
                CBenUS.Checked = True
            ElseIf Main.SoftSubs(i) = "ptBR" Then
                CBptBR.Checked = True
            ElseIf Main.SoftSubs(i) = "esLA" Then
                CBesLA.Checked = True
            ElseIf Main.SoftSubs(i) = "frFR" Then
                CBfrFR.Checked = True
            ElseIf Main.SoftSubs(i) = "arME" Then
                CBarME.Checked = True
            ElseIf Main.SoftSubs(i) = "ruRU" Then
                CBruRU.Checked = True
            ElseIf Main.SoftSubs(i) = "itIT" Then
                CBitIT.Checked = True
            ElseIf Main.SoftSubs(i) = "esES" Then
                CBesES.Checked = True
            End If
        Next
        For i As Integer = 0 To Main.SubFunimation.Count - 1
            If Main.SubFunimation(i) = "en" Then
                CB_fun_eng.Checked = True
            ElseIf Main.SubFunimation(i) = "es" Then
                CB_fun_es.Checked = True
            ElseIf Main.SubFunimation(i) = "pt" Then
                CB_fun_ptbr.Checked = True
            End If
            'If Main.SubFunimation(i) = "en" Then
            '    RB_eng.Checked = True
            'ElseIf Main.SubFunimation(i) = "es" Then
            '    RB_es.Checked = True
            'ElseIf Main.SubFunimation(i) = "pt" Then
            '    RB_pt.Checked = True
            'End If
        Next
        If Main.CR_NameMethode = 0 Then
            CR_Filename.SelectedIndex = 0
        ElseIf Main.CR_NameMethode = 1 Then
            CR_Filename.SelectedIndex = 1
        ElseIf Main.CR_NameMethode = 2 Then
            CR_Filename.SelectedIndex = 2
        ElseIf Main.CR_NameMethode = 3 Then
            CR_Filename.SelectedIndex = 3
        Else
            CR_Filename.SelectedIndex = 0
        End If

        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        If Main.MergeSubs = True Then
            MergeMP4.Checked = True
        End If

        If Main.HybridMode = True And Main.KeepCache = True Then
            DD_DLMode.SelectedIndex = 2
        ElseIf Main.HybridMode = True Then
            DD_DLMode.SelectedIndex = 1
        Else
            DD_DLMode.SelectedIndex = 0
        End If

        If Main.Funimation_srt = True Then
            CB_srt.Checked = True
        Else
            CB_srt.Checked = False
        End If

        If Main.Funimation_vtt = True Then
            CB_vtt.Checked = True
        Else
            CB_vtt.Checked = False
        End If


        If Main.HardSubFunimation = "en" Then
            CB_Fun_HardSubs.SelectedItem = "English"

        ElseIf Main.HardSubFunimation = "pt" Then
            CB_Fun_HardSubs.SelectedItem = "Português (Brasil)"

        ElseIf Main.HardSubFunimation = "es" Then
            CB_Fun_HardSubs.SelectedItem = "Español (LA)"

        Else
            CB_Fun_HardSubs.SelectedItem = "Disabled"
            'FunimationHardsub.Checked = True
        End If
        If Main.VideoFormat = ".mkv" Then
            CB_Format.SelectedItem = "MKV"
        ElseIf Main.VideoFormat = ".aac" Then
            CB_Format.SelectedItem = "AAC (Audio only)"
        Else
            CB_Format.SelectedItem = "MP4"
        End If

        If Main.DubFunimation = "english" Then
            Fun_Dub_Over.SelectedItem = "english"

        ElseIf Main.DubFunimation = "japanese" Then
            Fun_Dub_Over.SelectedItem = "japanese"

        ElseIf Main.DubFunimation = "portuguese(Brazil)" Then
            Fun_Dub_Over.SelectedItem = "portuguese(Brazil)"

        ElseIf Main.DubFunimation = "spanish(Mexico)" Then
            Fun_Dub_Over.SelectedItem = "spanish(Mexico)"

        Else
            Fun_Dub_Over.SelectedItem = "Disabled"
        End If


        Try
            GB_Resolution.Text = Main.GB_Resolution_Text
            GB_SubLanguage.Text = Main.GB_SubLanguage_Text
            DL_Count_simultaneous.Text = Main.DL_Count_simultaneousText
        Catch ex As Exception

        End Try

        If Main.Reso = 1080 Then
            A1080p.Checked = True
        ElseIf Main.Reso = 720 Then
            A720p.Checked = True
        ElseIf Main.Reso = 480 Then
            A480p.Checked = True
        ElseIf Main.Reso = 360 Then
            A360p.Checked = True
        ElseIf Main.Reso = 42 Then
            AAuto.Checked = True
        End If


        If Check_CB() = False Then
            ComboBox1.Items.Add(Main.CB_SuB_Nothing)
        End If
        If Main.SubSprache = "deDE" Then
            ComboBox1.SelectedItem = "Deutsch"
        ElseIf Main.SubSprache = "enUS" Then
            ComboBox1.SelectedItem = "English"
        ElseIf Main.SubSprache = "ptBR" Then
            ComboBox1.SelectedItem = "Português (Brasil)"
        ElseIf Main.SubSprache = "esLA" Then
            ComboBox1.SelectedItem = "Español (LA)"
        ElseIf Main.SubSprache = "frFR" Then
            ComboBox1.SelectedItem = "Français (France)"
        ElseIf Main.SubSprache = "arME" Then
            ComboBox1.SelectedItem = "العربية (Arabic)"
        ElseIf Main.SubSprache = "ruRU" Then
            ComboBox1.SelectedItem = "Русский (Russian)"
        ElseIf Main.SubSprache = "itIT" Then
            ComboBox1.SelectedItem = "Italiano (Italian)"
        ElseIf Main.SubSprache = "esES" Then
            ComboBox1.SelectedItem = "Español (España)"
        Else
            ComboBox1.SelectedItem = Main.CB_SuB_Nothing
        End If


        If DD_Season_Prefix.Items.Contains(Main.Season_Prefix) Then
            DD_Season_Prefix.SelectedItem = Main.Season_Prefix
        Else
            DD_Season_Prefix.Items.Add(Main.Season_Prefix)
            DD_Season_Prefix.SelectedItem = Main.Season_Prefix
        End If

        If DD_Episode_Prefix.Items.Contains(Main.Episode_Prefix) Then
            DD_Episode_Prefix.SelectedItem = Main.Episode_Prefix
        Else
            DD_Episode_Prefix.Items.Add(Main.Episode_Prefix)
            DD_Episode_Prefix.SelectedItem = Main.Episode_Prefix
        End If


        NumericUpDown2.Value = Main.ErrorTolerance
        NumericUpDown1.Value = Main.MaxDL
        TextBox1.Text = Main.Startseite

        If CBool(InStr(Main.ffmpeg_command, "-c copy")) Then
            FFMPEG_CommandP1.Text = "-c copy"
            FFMPEG_CommandP2.Enabled = False
            FFMPEG_CommandP3.Enabled = False
            FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
        ElseIf CBool(InStr(Main.ffmpeg_command, "-c:a copy ")) Then
            Dim ffmpegDisplayCurrent As String() = Main.ffmpeg_command.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)
            If ffmpegDisplayCurrent.Count > 8 Then
                FFMPEG_CommandP1.Text = ffmpegDisplayCurrent(0) + " " + ffmpegDisplayCurrent(1)
                FFMPEG_CommandP2.Text = ffmpegDisplayCurrent(2) + " " + ffmpegDisplayCurrent(3)
                FFMPEG_CommandP3.Text = ffmpegDisplayCurrent(4) + " " + ffmpegDisplayCurrent(5)
                FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
            Else
                FFMPEG_CommandP1.Text = ffmpegDisplayCurrent(0) + " " + ffmpegDisplayCurrent(1)
                FFMPEG_CommandP2.Text = "[no Preset]"
                FFMPEG_CommandP3.Text = ffmpegDisplayCurrent(2) + " " + ffmpegDisplayCurrent(3)
                FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
            End If


        Else

            Dim ffmpegDisplayCurrent As String() = Main.ffmpeg_command.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)
            FFMPEG_CommandP1.Text = ffmpegDisplayCurrent(0) + " " + ffmpegDisplayCurrent(1)
            FFMPEG_CommandP2.Text = ffmpegDisplayCurrent(2) + " " + ffmpegDisplayCurrent(3)
            FFMPEG_CommandP3.Text = ffmpegDisplayCurrent(4) + " " + ffmpegDisplayCurrent(5)
            FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
        End If


        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            ListViewAdd_True.Checked = CBool(Integer.Parse(rkg.GetValue("QueueMode").ToString))
        Catch ex As Exception
        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Dim ServerSetting As String = rkg.GetValue("ServerPort").ToString

            If ServerSetting = "0" Then
                http_support.Text = "add-on support disabled"
            Else
                http_support.Text = ServerSetting
            End If

        Catch ex As Exception
            http_support.Text = "add-on support disabled"

        End Try

        If Main.DefaultSubFunimation = "en" Then
            FunSubDef.SelectedItem = "English"
        ElseIf Main.DefaultSubFunimation = "pt" Then
            FunSubDef.SelectedItem = "Português (Brasil)"
        ElseIf Main.DefaultSubFunimation = "es" Then
            FunSubDef.SelectedItem = "Español (LA)"
        Else
            FunSubDef.SelectedItem = "[Disabled]"
            'FunimationHardsub.Checked = True
        End If



        If Main.DefaultSubCR = "deDE" Then
            CR_SoftSubDefault.SelectedItem = "Deutsch"
        ElseIf Main.DefaultSubCR = "enUS" Then
            CR_SoftSubDefault.SelectedItem = "English"
        ElseIf Main.DefaultSubCR = "ptBR" Then
            CR_SoftSubDefault.SelectedItem = "Português (Brasil)"
        ElseIf Main.DefaultSubCR = "esLA" Then
            CR_SoftSubDefault.SelectedItem = "Español (LA)"
        ElseIf Main.DefaultSubCR = "frFR" Then
            CR_SoftSubDefault.SelectedItem = "Français (France)"
        ElseIf Main.DefaultSubCR = "arME" Then
            CR_SoftSubDefault.SelectedItem = "العربية (Arabic)"
        ElseIf Main.DefaultSubCR = "ruRU" Then
            CR_SoftSubDefault.SelectedItem = "Русский (Russian)"
        ElseIf Main.DefaultSubCR = "itIT" Then
            CR_SoftSubDefault.SelectedItem = "Italiano (Italian)"
        ElseIf Main.DefaultSubCR = "esES" Then
            CR_SoftSubDefault.SelectedItem = "Español (España)"
        Else
            CR_SoftSubDefault.SelectedItem = "[Disabled]"
        End If

    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")

        Main.LeadingZero = LeadingZeroDD.SelectedIndex
        rk.SetValue("LeadingZero", LeadingZeroDD.SelectedIndex, RegistryValueKind.String)

        Main.Funimation_Bitrate = Bitrate_Funi.SelectedIndex
        rk.SetValue("Funimation_Bitrate", Bitrate_Funi.SelectedIndex, RegistryValueKind.String)


        If http_support.Text = "add-on support disabled" Then
            rk.SetValue("ServerPort", 0, RegistryValueKind.String)
            Main.StartServer = CInt(False)

        Else
            Dim Port As Integer = 0
            Try
                Port = CInt(http_support.Text)
                rk.SetValue("ServerPort", Port, RegistryValueKind.String)
                'Main.StartServer = False
            Catch ex As Exception
                'If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                'Else
                '    AAuto.Checked = False
                '    A360p.Checked = True
                'End If
                MsgBox("The add-on support Port can only be numbers!", MsgBoxStyle.Exclamation)
                Exit Sub
            End Try
            If Main.StartServer = Port Then
            Else
                MsgBox("The add-on support needs a restart of the downloader.", MsgBoxStyle.Information)
            End If
        End If






        Main.IgnoreSeason = CB_Ignore.SelectedIndex
        rk.SetValue("IgnoreS1", CB_Ignore.SelectedIndex, RegistryValueKind.String)


        If KodiSupport.Checked = True Then
            Main.KodiNaming = True
            rk.SetValue("KodiSupport", 1, RegistryValueKind.String)
        Else
            Main.KodiNaming = False
            rk.SetValue("KodiSupport", 0, RegistryValueKind.String)
        End If

        '  MsgBox(Name_season.Text)
        If CBool(InStr(TextBox1.Text, "https://")) Then
            Main.Startseite = TextBox1.Text
            rk.SetValue("Startseite", Main.Startseite, RegistryValueKind.String)
        ElseIf TextBox1.Text = Nothing Then
            Main.Startseite = "https://www.crunchyroll.com/"
            rk.SetValue("Startseite", Main.Startseite, RegistryValueKind.String)
        Else

        End If
        If DD_Season_Prefix.Text = "[default season prefix]" Then
            Main.Season_Prefix = "[default season prefix]"
            rk.SetValue("Prefix_S", "[default season prefix]", RegistryValueKind.String)
        Else
            Main.Season_Prefix = DD_Season_Prefix.Text
            rk.SetValue("Prefix_S", DD_Season_Prefix.Text, RegistryValueKind.String)
        End If

        If DD_Episode_Prefix.Text = "[default episode prefix]" Then
            Main.Episode_Prefix = "[default episode prefix]"
            rk.SetValue("Prefix_E", "[default episode prefix]", RegistryValueKind.String)
        Else
            Main.Episode_Prefix = DD_Episode_Prefix.Text
            rk.SetValue("Prefix_E", DD_Episode_Prefix.Text, RegistryValueKind.String)
        End If

        If A1080p.Checked Then
            Main.Reso = 1080
            rk.SetValue("Resu", 1080, RegistryValueKind.String)
        ElseIf A720p.Checked Then
            Main.Reso = 720
            rk.SetValue("Resu", 720, RegistryValueKind.String)
        ElseIf A360p.Checked Then
            Main.Reso = 360
            rk.SetValue("Resu", 360, RegistryValueKind.String)
        ElseIf A480p.Checked Then
            Main.Reso = 480
            rk.SetValue("Resu", 480, RegistryValueKind.String)
        ElseIf AAuto.Checked Then
            Main.Reso = 42
            rk.SetValue("Resu", 42, RegistryValueKind.String)
        End If

        If ComboBox1.SelectedItem.ToString = "English" Then
            Main.SubSprache = "enUS"
            rk.SetValue("Sub", "enUS", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Deutsch" Then
            Main.SubSprache = "deDE"
            rk.SetValue("Sub", "deDE", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Português (Brasil)" Then
            Main.SubSprache = "ptBR"
            rk.SetValue("Sub", "ptBR", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Español (LA)" Then
            Main.SubSprache = "esLA"
            rk.SetValue("Sub", "esLA", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Français (France)" Then
            Main.SubSprache = "frFR"
            rk.SetValue("Sub", "frFR", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "العربية (Arabic)" Then
            Main.SubSprache = "arME"
            rk.SetValue("Sub", "arME", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Русский (Russian)" Then
            Main.SubSprache = "ruRU"
            rk.SetValue("Sub", "ruRU", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Italiano (Italian)" Then
            Main.SubSprache = "itIT"
            rk.SetValue("Sub", "itIT", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Español (España)" Then
            Main.SubSprache = "esES"
            rk.SetValue("Sub", "esES", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = Main.CB_SuB_Nothing Then
            Main.SubSprache = "None"
            rk.SetValue("Sub", "None", RegistryValueKind.String)

        End If

        If CR_SoftSubDefault.SelectedItem.ToString = "English" Then
            Main.DefaultSubCR = "enUS"
            rk.SetValue("DefaultSubCR", "enUS", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Deutsch" Then
            Main.DefaultSubCR = "deDE"
            rk.SetValue("DefaultSubCR", "deDE", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Português (Brasil)" Then
            Main.DefaultSubCR = "ptBR"
            rk.SetValue("DefaultSubCR", "ptBR", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Español (LA)" Then
            Main.DefaultSubCR = "esLA"
            rk.SetValue("DefaultSubCR", "esLA", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Français (France)" Then
            Main.DefaultSubCR = "frFR"
            rk.SetValue("DefaultSubCR", "frFR", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "العربية (Arabic)" Then
            Main.DefaultSubCR = "arME"
            rk.SetValue("DefaultSubCR", "arME", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Русский (Russian)" Then
            Main.DefaultSubCR = "ruRU"
            rk.SetValue("DefaultSubCR", "ruRU", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Italiano (Italian)" Then
            Main.DefaultSubCR = "itIT"
            rk.SetValue("DefaultSubCR", "itIT", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Español (España)" Then
            Main.DefaultSubCR = "esES"
            rk.SetValue("DefaultSubCR", "esES", RegistryValueKind.String)

        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "[Disabled]" Then
            Main.DefaultSubCR = "None"
            rk.SetValue("DefaultSubCR", "Disabled", RegistryValueKind.String)

        End If

        If CR_Filename.Text = "[episode number]" Then
            Main.CR_NameMethode = 0
            rk.SetValue("CR_NameMethode", 0, RegistryValueKind.String)
        ElseIf CR_Filename.Text = "[episode name]" Then
            Main.CR_NameMethode = 1
            rk.SetValue("CR_NameMethode", 1, RegistryValueKind.String)
        ElseIf CR_Filename.Text = "[episode number] [episode name]" Then
            Main.CR_NameMethode = 2
            rk.SetValue("CR_NameMethode", 2, RegistryValueKind.String)
        ElseIf CR_Filename.Text = "[episode name] [episode number]" Then
            Main.CR_NameMethode = 3
            rk.SetValue("CR_NameMethode", 3, RegistryValueKind.String)
        End If

        If CB_Format.Text = "MKV" Then
            Main.VideoFormat = ".mkv"
            Main.MergeSubsFormat = "copy"
            rk.SetValue("VideoFormat", ".mkv", RegistryValueKind.String)
        ElseIf CB_Format.Text = "AAC (Audio only)" Then
            Main.VideoFormat = ".aac"
            Main.MergeSubsFormat = "mov_text"
            rk.SetValue("VideoFormat", ".aac", RegistryValueKind.String)
        Else
            Main.VideoFormat = ".mp4"
            Main.MergeSubsFormat = "mov_text"
            rk.SetValue("VideoFormat", ".mp4", RegistryValueKind.String)
        End If

        If MergeMP4.Checked = True Then
            Main.MergeSubs = True
            rk.SetValue("MergeSubs", "1", RegistryValueKind.String)
        Else
            Main.MergeSubs = False
            rk.SetValue("MergeSubs", "0", RegistryValueKind.String)
        End If


        If DD_DLMode.SelectedIndex = 2 Then
            Main.HybridMode = True
            Main.KeepCache = True
            rk.SetValue("HybridMode", "1", RegistryValueKind.String)
        ElseIf DD_DLMode.SelectedIndex = 1 Then
            Main.HybridMode = True
            Main.KeepCache = False
            rk.SetValue("HybridMode", "1", RegistryValueKind.String)
        Else
            Main.HybridMode = False
            Main.KeepCache = False
            rk.SetValue("HybridMode", "0", RegistryValueKind.String)
        End If

        If Main.KeepCache = True Then
            rk.SetValue("Keep_Cache", 1, RegistryValueKind.String)
        Else
            rk.SetValue("Keep_Cache", 0, RegistryValueKind.String)
        End If

#Region "funimation"




        Main.DubFunimation = Fun_Dub_Over.SelectedItem.ToString

        rk.SetValue("FunimationDub", Fun_Dub_Over.SelectedItem.ToString, RegistryValueKind.String)


        If CB_Fun_HardSubs.SelectedItem.ToString = "Disabled" Then
            Main.HardSubFunimation = "Disabled"
            rk.SetValue("FunimationHardsub", "Disabled", RegistryValueKind.String)

        ElseIf CB_Fun_HardSubs.SelectedItem.ToString = "English" Then
            Main.HardSubFunimation = "en"
            rk.SetValue("FunimationHardsub", "en", RegistryValueKind.String)

        ElseIf CB_Fun_HardSubs.SelectedItem.ToString = "Português (Brasil)" Then
            Main.HardSubFunimation = "pt"
            rk.SetValue("FunimationHardsub", "pt", RegistryValueKind.String)

        ElseIf CB_Fun_HardSubs.SelectedItem.ToString = "Español (LA)" Then
            Main.HardSubFunimation = "es"
            rk.SetValue("FunimationHardsub", "es", RegistryValueKind.String)

        End If

        If FunSubDef.SelectedItem.ToString = "[Disabled]" Then
            Main.DefaultSubFunimation = "Disabled"
            rk.SetValue("DefaultSubFunimation", "Disabled", RegistryValueKind.String)

        ElseIf FunSubDef.SelectedItem.ToString = "English" Then
            Main.DefaultSubFunimation = "en"
            rk.SetValue("DefaultSubFunimation", "en", RegistryValueKind.String)

        ElseIf FunSubDef.SelectedItem.ToString = "Português (Brasil)" Then
            Main.DefaultSubFunimation = "pt"
            rk.SetValue("DefaultSubFunimation", "pt", RegistryValueKind.String)

        ElseIf FunSubDef.SelectedItem.ToString = "Español (LA)" Then
            Main.DefaultSubFunimation = "es"
            rk.SetValue("DefaultSubFunimation", "es", RegistryValueKind.String)

        End If

        Main.SubFunimation.Clear()
        If CB_fun_eng.Checked = True Then
            Main.SubFunimation.Add("en")
        End If

        If CB_fun_es.Checked = True Then
            Main.SubFunimation.Add("es")

        End If

        If CB_fun_ptbr.Checked = True Then
            Main.SubFunimation.Add("pt")
        End If

        If Main.SubFunimation.Count > 0 And CB_vtt.Checked = False And CB_srt.Checked = False Then
            CB_vtt.Checked = True
        End If


        If CB_srt.Checked = True Then
            Main.Funimation_srt = True
            rk.SetValue("Funimation_srt", "1", RegistryValueKind.String)
        Else
            Main.Funimation_srt = False
            rk.SetValue("Funimation_srt", "0", RegistryValueKind.String)
        End If
        If CB_vtt.Checked = True Then
            Main.Funimation_vtt = True
            rk.SetValue("Funimation_vtt", "1", RegistryValueKind.String)
        Else
            Main.Funimation_vtt = False
            rk.SetValue("Funimation_vtt", "0", RegistryValueKind.String)
        End If


        Dim FunimationSaveString As String = Nothing
        For ii As Integer = 0 To Main.SubFunimation.Count - 1
            If FunimationSaveString = Nothing Then
                FunimationSaveString = Main.SubFunimation(ii)
            Else
                FunimationSaveString = FunimationSaveString + "," + Main.SubFunimation(ii)
            End If
        Next
        If FunimationSaveString = Nothing Then
            FunimationSaveString = "none"
        End If
        rk.SetValue("Fun_Sub", FunimationSaveString, RegistryValueKind.String)
#End Region





        Dim ffpmeg_cmd As String = Nothing
        If FFMPEG_CommandP1.Text = "-c copy" Then
            ffpmeg_cmd = " " + FFMPEG_CommandP1.Text + " " + FFMPEG_CommandP4.Text
        ElseIf FFMPEG_CommandP2.Text = "[no Preset]" Then

            ffpmeg_cmd = " " + FFMPEG_CommandP1.Text + " " + FFMPEG_CommandP3.Text + " " + FFMPEG_CommandP4.Text
        Else

            ffpmeg_cmd = " " + FFMPEG_CommandP1.Text + " " + FFMPEG_CommandP2.Text + " " + FFMPEG_CommandP3.Text + " " + FFMPEG_CommandP4.Text
        End If

        rk.SetValue("ffmpeg_command", ffpmeg_cmd, RegistryValueKind.String)
        Main.ffmpeg_command = ffpmeg_cmd

        If CBool(InStr(FFMPEG_CommandP1.Text, "nvenc")) Then
            If NumericUpDown1.Value > 2 Then
                NumericUpDown1.Value = 2
            End If

        ElseIf CBool(InStr(FFMPEG_CommandP1.Text, "libx26")) Then
            If NumericUpDown1.Value > 1 Then
                NumericUpDown1.Value = 1
            End If
        End If
        rk.SetValue("SL_DL", NumericUpDown1.Value, RegistryValueKind.String)
        Main.MaxDL = CInt(NumericUpDown1.Value)

        rk.SetValue("ErrorTolerance", NumericUpDown2.Value, RegistryValueKind.String)
        Main.ErrorTolerance = CInt(NumericUpDown2.Value)

        If ListViewAdd_True.Checked = True Then
            rk.SetValue("QueueMode", 1, RegistryValueKind.String)
            Main.UseQueue = True
        ElseIf ListViewAdd_True.Checked = False Then
            rk.SetValue("QueueMode", 0, RegistryValueKind.String)
            Main.UseQueue = False
        End If


#Region "sof subs"
        Main.SoftSubs.Clear()
        If CBdeDE.Checked = True Then
            Main.SoftSubs.Add("deDE")
        End If
        If CBenUS.Checked = True Then
            Main.SoftSubs.Add("enUS")
        End If
        If CBptBR.Checked = True Then
            Main.SoftSubs.Add("ptBR")
        End If
        If CBesLA.Checked = True Then
            Main.SoftSubs.Add("esLA")
            Main.SoftSubs.Add("es-419")
        End If
        If CBfrFR.Checked = True Then
            Main.SoftSubs.Add("frFR")
        End If
        If CBarME.Checked = True Then
            Main.SoftSubs.Add("arME")
            Main.SoftSubs.Add("ar-SA")
        End If
        If CBruRU.Checked = True Then
            Main.SoftSubs.Add("ruRU")
        End If
        If CBitIT.Checked = True Then
            Main.SoftSubs.Add("itIT")
        End If
        If CBesES.Checked = True Then
            Main.SoftSubs.Add("esES")
        End If

        Dim SaveString As String = Nothing
        For ii As Integer = 0 To Main.SoftSubs.Count - 1
            If SaveString = Nothing Then
                SaveString = Main.SoftSubs(ii)
            Else
                SaveString = SaveString + "," + Main.SoftSubs(ii)
            End If
        Next
        If SaveString = Nothing Then
            SaveString = "none"
        End If
        rk.SetValue("AddedSubs", SaveString, RegistryValueKind.String)
#End Region

        If CB_SoftSubSettings.SelectedIndex = 0 Then
            Main.IncludeLangName = False
            rk.SetValue("IncludeLangName", "0", RegistryValueKind.String)

        Else
            Main.IncludeLangName = True
            rk.SetValue("IncludeLangName", "1", RegistryValueKind.String)

        End If

        If LangNameType_DD.SelectedIndex = 1 Then
            Main.LangNameType = 1
            rk.SetValue("LangNameType", "1", RegistryValueKind.String)
        ElseIf LangNameType_DD.SelectedIndex = 2 Then
            Main.LangNameType = 2
            rk.SetValue("LangNameType", "2", RegistryValueKind.String)
        Else
            Main.LangNameType = 0
            rk.SetValue("LangNameType", "0", RegistryValueKind.String)
        End If



        Me.Close()
    End Sub

    Private Function Check_CB() As Boolean
        Dim C As Boolean = False
        For i As Integer = 0 To ComboBox1.Items.Count - 1
            If ComboBox1.Items.Item(i).ToString = Main.CB_SuB_Nothing Then
                C = True
                Exit For
            End If
        Next
        Return C
    End Function





    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        Me.Close()
    End Sub
#Region "UI"

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox1.MouseEnter

        pictureBox1.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave

        pictureBox1.Image = Main.CloseImg
    End Sub

    Private Sub Btn_Save_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Save.MouseEnter, Btn_Save.GotFocus
        Btn_Save.Image = My.Resources.crdSettings_Button_SafeExit_hover
    End Sub

    Private Sub Btn_Save_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Save.MouseLeave, Btn_Save.LostFocus
        Btn_Save.Image = My.Resources.crdSettings_Button_SafeExit
    End Sub


    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox1.DrawItem, CB_Fun_HardSubs.DrawItem, Fun_Dub_Over.DrawItem
        Dim CB As ComboBox = CType(sender, ComboBox)
        CB.BackColor = Color.White
        If e.Index >= 0 Then
            Using st As New StringFormat With {.Alignment = StringAlignment.Center}
                ' e.DrawBackground()
                ' e.DrawFocusRectangle()
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds)
                e.Graphics.DrawString(CB.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If
    End Sub






    Private Sub AAuto_Click(sender As Object, e As EventArgs) Handles AAuto.Click
        If MergeMP4.Checked = True Then
            If AAuto.Checked = True Then
                If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Else
                    AAuto.Checked = False
                    A360p.Checked = True
                End If
            End If
            'ElseIf HybridMode_CB.Checked = True Then
            '    If AAuto.Checked = True Then
            '        MsgBox("Resolution '[Auto]' and 'Hybride Mode' does not work together", MsgBoxStyle.Information)
            '        AAuto.Checked = False
            '        A1080p.Checked = True
            '    End If
        End If
    End Sub

    Private Sub MergeMP4_Click(sender As Object, e As EventArgs) Handles MergeMP4.Click
        If MergeMP4.Checked = True Then
            If AAuto.Checked = True Then
                If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Else
                    MergeMP4.Checked = False
                End If
            End If
        End If
    End Sub












    Private Sub ListC1_Click(sender As Object, e As EventArgs) Handles ListC1.Click, ListC2.Click, ListC3.Click, ListC4.Click, ListC5.Click, ListC6.Click, ListC7.Click
        Dim Button As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        If Button.Text = "-c copy" Then
            FFMPEG_CommandP1.Text = "-c copy"
            FFMPEG_CommandP2.Enabled = False
            FFMPEG_CommandP3.Enabled = False
        Else
            FFMPEG_CommandP1.Text = Button.Text
            FFMPEG_CommandP2.Enabled = True
            FFMPEG_CommandP3.Enabled = True
        End If

    End Sub

    Private Sub ListP1_Click(sender As Object, e As EventArgs) Handles ListP1.Click, ListP2.Click, ListP3.Click
        Dim Button As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        FFMPEG_CommandP2.Text = Button.Text
        FFMPEG_CommandP2.Enabled = True
        FFMPEG_CommandP3.Enabled = True
    End Sub

    Private Sub ListBit1_Click(sender As Object, e As EventArgs) Handles ListBit_7000.Click, ListBit_6500.Click, ListBit_6000.Click, ListBit_5500.Click, ListBit_5000.Click, ListBit_4500.Click, ListBit_4000.Click, ListBit_3500.Click, ListBit_3000.Click, ListBit_2500.Click, ListBit_2000.Click, ListBit_1500.Click, ListBit_1000.Click
        Dim Button As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        FFMPEG_CommandP3.Text = Button.Text
        FFMPEG_CommandP2.Enabled = True
        FFMPEG_CommandP3.Enabled = True
    End Sub



    Private Sub Label7_Click(sender As Object, e As EventArgs)
        Process.Start("https://github.com/cefsharp/CefSharp")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.ffmpeg.org/about.html")
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)
        Process.Start("https://github.com/hama3254/metroframework-modern-ui")
    End Sub



    Sub GroupBoxColor(ByVal color As Color)
        NumericUpDown1.ForeColor = color
        NumericUpDown2.ForeColor = color
        FFMPEG_CommandP1.ForeColor = color
        FFMPEG_CommandP2.ForeColor = color
        FFMPEG_CommandP3.ForeColor = color
        FFMPEG_CommandP4.ForeColor = color
        SoftSubs.ForeColor = color
        GB_SubLanguage.ForeColor = color
        DL_Count_simultaneous.ForeColor = color
        GB_Resolution.ForeColor = color
        GB_Filename_Pre.ForeColor = color
        GroupBox1.ForeColor = color
        GroupBox2.ForeColor = color
        GroupBox3.ForeColor = color
        GroupBox4.ForeColor = color
        GroupBox5.ForeColor = color
        GroupBox6.ForeColor = color
        GroupBox7.ForeColor = color
        GroupBox8.ForeColor = color
        GroupBox9.ForeColor = color
        GroupBox10.ForeColor = color
        GroupBox11.ForeColor = color
        GroupBox12.ForeColor = color
        GroupBox13.ForeColor = color
        GroupBox14.ForeColor = color
        GroupBox15.ForeColor = color
        GroupBox16.ForeColor = color
        GroupBox17.ForeColor = color
        GroupBox18.ForeColor = color
    End Sub





    Private Sub DarkMode_CheckedChanged(sender As Object, e As EventArgs) Handles DarkMode.CheckedChanged
        Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")

        If DarkMode.Checked = True Then
            rk.SetValue("Dark_Mode", 1, RegistryValueKind.String)
            Manager.Theme = MetroThemeStyle.Dark
            GroupBoxColor(Color.FromArgb(150, 150, 150))
            NumericUpDown1.BackColor = Color.FromArgb(17, 17, 17)
            NumericUpDown2.BackColor = Color.FromArgb(17, 17, 17)
            Main.DarkMode()
            Main.DarkModeValue = True
            pictureBox1.Image = Main.CloseImg
        Else
            Main.DarkModeValue = False
            rk.SetValue("Dark_Mode", 0, RegistryValueKind.String)
            Manager.Theme = MetroThemeStyle.Light
            Main.LightMode()
            GroupBoxColor(Color.FromArgb(0, 0, 0))
            NumericUpDown1.BackColor = Color.FromArgb(243, 243, 243)
            NumericUpDown2.BackColor = Color.FromArgb(243, 243, 243)
            pictureBox1.Image = Main.CloseImg
        End If
    End Sub


    Private Sub Server_Click(sender As Object, e As EventArgs)
        'If Server.Checked = True Then
        '    MsgBox("This feature requires a restart of the downloader", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            client0.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))

            Dim str0 As String = client0.DownloadString("https://api.github.com/repos/hama3254/Crunchyroll-Downloader-v3.0/releases")

            Dim GitHubLastIsPre() As String = str0.Split(New String() {Chr(34) + "prerelease" + Chr(34) + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim LastNonPreRelase As Integer = 0

            For i As Integer = 1 To GitHubLastIsPre.Count - 1
                Dim GitHubLastIsPre1() As String = GitHubLastIsPre(i).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)

                If GitHubLastIsPre1(0) = "false" Then
                    LastNonPreRelase = i
                    Exit For
                End If
            Next

            Dim GitHubLastTag() As String = str0.Split(New String() {Chr(34) + "tag_name" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim GitHubLastTag1() As String = GitHubLastTag(LastNonPreRelase).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)

            LastVersionString = GitHubLastTag1(0)

            'Debug.WriteLine(GitHubLastTag1(0))

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub




    Private Sub CB_Fun_HardSubs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Fun_HardSubs.SelectedIndexChanged
        If CB_Fun_HardSubs.SelectedIndex = 0 Then
        Else
            If Main.HardSubFunimation = "Disabled" Then
                If FFMPEG_CommandP1.Text = "-c copy" Then
                    If MessageBox.Show("Funimation hard subtitle are post-processed." + vbNewLine + "This cost a lot of performance and it should not more than one download run at the time!", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                    Else
                        CB_Fun_HardSubs.SelectedIndex = 0
                    End If
                End If
            End If

        End If

        'MetroMessageBox.Show(Me, "Test", "Test Box", MessageBoxButtons.YesNo, MessageBoxIcon.None, 150)
        'MetroMessageBox.Show(Me, "Test", "Test Box", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 150, MetroThemeStyle.Dark)

    End Sub

    Private Sub CB_fun_eng_CheckedChanged(sender As Object, e As EventArgs) Handles CB_fun_eng.CheckedChanged
        If CB_fun_eng.Checked = True Then
            FunSubDef.Items.Add("English")
        Else
            FunSubDef.Items.Remove("English")
            If FunSubDef.Text = "English" Then
                FunSubDef.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CB_fun_es_CheckedChanged(sender As Object, e As EventArgs) Handles CB_fun_es.CheckedChanged
        If CB_fun_es.Checked = True Then
            FunSubDef.Items.Add("Español (LA)")
        Else
            FunSubDef.Items.Remove("Español (LA)")
            If FunSubDef.Text = "Español (LA)" Then
                FunSubDef.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CB_fun_ptbr_CheckedChanged(sender As Object, e As EventArgs) Handles CB_fun_ptbr.CheckedChanged
        If CB_fun_ptbr.Checked = True Then
            FunSubDef.Items.Add("Português (Brasil)")
        Else
            FunSubDef.Items.Remove("Português (Brasil)")
            If FunSubDef.Text = "Português (Brasil)" Then
                FunSubDef.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

#Region "CR_default soft sub"


    Private Sub CBesES_CheckedChanged(sender As Object, e As EventArgs) Handles CBesES.CheckedChanged
        If CBesES.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBesES.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBesES.Text)
            If CR_SoftSubDefault.Text = CBesES.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBitIT_CheckedChanged(sender As Object, e As EventArgs) Handles CBitIT.CheckedChanged
        If CBitIT.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBitIT.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBitIT.Text)
            If CR_SoftSubDefault.Text = CBitIT.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBruRU_CheckedChanged(sender As Object, e As EventArgs) Handles CBruRU.CheckedChanged
        If CBruRU.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBruRU.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBruRU.Text)
            If CR_SoftSubDefault.Text = CBruRU.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBarME_CheckedChanged(sender As Object, e As EventArgs) Handles CBarME.CheckedChanged
        If CBarME.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBarME.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBarME.Text)
            If CR_SoftSubDefault.Text = CBarME.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBfrFR_CheckedChanged(sender As Object, e As EventArgs) Handles CBfrFR.CheckedChanged
        If CBfrFR.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBfrFR.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBfrFR.Text)
            If CR_SoftSubDefault.Text = CBfrFR.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBesLA_CheckedChanged(sender As Object, e As EventArgs) Handles CBesLA.CheckedChanged
        If CBesLA.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBesLA.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBesLA.Text)
            If CR_SoftSubDefault.Text = CBesLA.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBptBR_CheckedChanged(sender As Object, e As EventArgs) Handles CBptBR.CheckedChanged
        If CBptBR.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBptBR.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBptBR.Text)
            If CR_SoftSubDefault.Text = CBptBR.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBdeDE_CheckedChanged(sender As Object, e As EventArgs) Handles CBdeDE.CheckedChanged
        If CBdeDE.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBdeDE.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBdeDE.Text)
            If CR_SoftSubDefault.Text = CBdeDE.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CBenUS_CheckedChanged(sender As Object, e As EventArgs) Handles CBenUS.CheckedChanged
        If CBenUS.Checked = True Then
            CR_SoftSubDefault.Items.Add(CBenUS.Text)
        Else
            CR_SoftSubDefault.Items.Remove(CBenUS.Text)
            If CR_SoftSubDefault.Text = CBenUS.Text Then
                CR_SoftSubDefault.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub


    'Private Sub HybridMode_CB_Click(sender As Object, e As EventArgs)
    '    If HybridMode_CB.Checked = True Then

    '        If MessageBox.Show("Should the cached data be kept?" + vbNewLine + "Press 'No' to free the space after downloading.", "Keep cached files?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '            Main.KeepCache = True
    '        Else
    '            Main.KeepCache = False
    '        End If

    '        If AAuto.Checked = True Then
    '            MsgBox("Resolution '[Auto]' and 'Hybride Mode' does not work together", MsgBoxStyle.Information)
    '            HybridMode_CB.Checked = False
    '        End If
    '    End If
    'End Sub

    Private Sub MetroLink1_Click(sender As Object, e As EventArgs)
        Process.Start("https://github.com/hama3254/Crunchyroll-Downloader-v3.0/discussions/276")
    End Sub


    Private Sub TabPage7_Enter(sender As Object, e As EventArgs) Handles TabPage7.Enter
        LastVersion.Text = "last release: " + LastVersionString
    End Sub

    Private Sub CB_Format_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Format.SelectedIndexChanged
        If CB_Format.Text = "AAC (Audio only)" Then
            If MergeMP4.Checked = True Then
                MsgBox("Merged subs are not avalible with audio only!", MsgBoxStyle.Information)
            End If
            MergeMP4.Checked = False
        End If
    End Sub

    Private Sub MergeMP4_CheckedChanged(sender As Object, e As EventArgs) Handles MergeMP4.CheckedChanged
        If CB_Format.Text = "AAC (Audio only)" Then
            If MergeMP4.Checked = True Then
                MsgBox("Merged subs are not avalible with audio only!", MsgBoxStyle.Information)
            End If
            MergeMP4.Checked = False
        End If
    End Sub

    Private Sub ProfileTextBox_Click(sender As Object, e As EventArgs) Handles ProfileTextBox.Click

        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then

            Main.ProfileFolder = FolderBrowserDialog1.SelectedPath
            ProfileTextBox.Text = FolderBrowserDialog1.SelectedPath
            Dim rk0 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
            rk0.SetValue("ProfilFolder", Main.ProfileFolder, RegistryValueKind.String)


        End If
    End Sub

    Private Sub DD_DLMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DD_DLMode.SelectedIndexChanged

        If DD_DLMode.SelectedIndex > 0 Then
            TempTB.Enabled = True
        Else
            TempTB.Enabled = False
        End If

    End Sub

    Private Sub TempTB_Click(sender As Object, e As EventArgs) Handles TempTB.Click

        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then

            Main.TempFolder = FolderBrowserDialog1.SelectedPath
            TempTB.Text = FolderBrowserDialog1.SelectedPath
            Dim rk0 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
            rk0.SetValue("TempFolder", Main.TempFolder, RegistryValueKind.String)


        End If

    End Sub








#End Region







#End Region
End Class