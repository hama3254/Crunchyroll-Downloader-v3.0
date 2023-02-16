Public Class CRD_Classes

End Class

#Region "funimation"


Public Class FunimationOverview
    Public ID As String
    Public Title As String
    Public Slug As String
    Public Sub New(ByVal Slug As String, ByVal ID As String, ByVal Title As String)
        Me.ID = ID
        Me.Title = Title
        Me.Slug = Slug
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.Slug, Me.ID, Me.Title)
    End Function
End Class

Public Class FunimationSubs
    Public LangugageCode As String
    Public Url As String
    Public Format As String
    Public Sub New(ByVal LangugageCode As String, ByVal Format As String, ByVal Url As String)
        Me.Url = Url
        Me.LangugageCode = LangugageCode
        Me.Format = Format
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.LangugageCode, Me.Format, Me.Url)
    End Function
End Class

Public Class FunimationStream
    Public audioLanguage As String
    Public Url As String
    Public version As String
    Public Primary As Boolean
    Public Sub New(ByVal audioLanguage As String, ByVal version As String, ByVal Url As String, ByVal Primary As Boolean)
        Me.Primary = Primary
        Me.Url = Url
        Me.audioLanguage = audioLanguage
        Me.version = version
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.audioLanguage, Me.version, Me.Url)
    End Function
End Class
#End Region
#Region "CR"
Public Class CR_Beta_Stream
    'Public audioLanguage As String
    Public Url As String
    Public subLang As String
    Public Format As String
    'ByVal audioLanguage As String, 
    Public Sub New(ByVal subLang As String, ByVal Format As String, ByVal Url As String)
        Me.subLang = subLang
        Me.Url = Url.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
        Me.Format = Format
    End Sub
    'Me.audioLanguage,
    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.subLang, Me.Format, Me.Url)
    End Function

End Class

Public Class CR_Subtiles
    Public Url As String
    Public SubLangValue As String
    Public SubLangName As String
    Public DefaultSub As Boolean
    Public Index As String
    Public Sub New(ByVal SubLangValue As String, ByVal SubLangName As String, ByVal Url As String, ByVal Index As String, ByVal DefaultSub As Boolean)
        Me.SubLangValue = SubLangValue
        Me.SubLangName = SubLangName
        Me.Url = Url
        Me.Index = Index
        Me.DefaultSub = DefaultSub
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.SubLangValue, Me.SubLangName, Me.Url, Me.Index, Me.DefaultSub.ToString)
    End Function

End Class
Public Class UrlJson

    Public Url As String
    Public Content As String
    Public Sub New(ByVal Url As String, ByVal Content As String)
        Me.Url = Url
        Me.Content = Content

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Url, Me.Content)
    End Function
End Class

Public Class CR_Seasons

    Public guid As String
    Public audio_locale As String
    Public Auth As String
    Public Sub New(ByVal guid As String, ByVal audio_locale As String, ByVal Auth As String)
        Me.guid = guid
        Me.audio_locale = audio_locale
        Me.Auth = Auth

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.guid, Me.audio_locale)
    End Function
End Class


#End Region


Public Class ServerResponse

    Public Type As String
    Public Content As String
    Public Sub New(ByVal Content As String, ByVal Type As String)
        Me.Content = Content
        Me.Type = Type

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Content, Me.Type)
    End Function
End Class


Public Class NameValuePair

    Public Name As String
    Public CR_Value As String
    Public FM_Value As String
    Public Sub New(ByVal Name As String, ByVal CR_Value As String, ByVal FM_Value As String)
        Me.Name = Name
        Me.CR_Value = CR_Value
        Me.FM_Value = FM_Value
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Name, Me.CR_Value, Me.FM_Value)
    End Function
End Class