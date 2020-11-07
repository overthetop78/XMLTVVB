Imports System.Xml
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.Windows.Forms.Application

Module Mod_Grabber
    'Grabber 
    Public Property EPGTitle As String
    Public Property EPGSubTitle As String
    Public Property EPGStartTimeA As String
    Public Property EPGURLPage As String
    Public Property EPGURLImage As String
    Public Property EPGParentalRatings As String
    Public Property EPGSubTitle2 As String
    Public Property EPGSummary As String
    Public Property EPGEditorialTitle As String
    Public Property EPGGenre As String
    Public Property EPGChannelName As String
    Public Property EPGChannelID As String
    Public Property EPGBroadcastID As String
    Public Property EPGStartTime As String
    Public Property EPGEndTime As String
    Public Property EPGDuree As String
    Public Property EPGLogoChannel As String
    Public Property EPGLang As String
    Public Property EPGEpisode As String
    Public Property EPGProgRate As String
    Public Property EPGProgRateVal As String
    Public Property ChannelName As String
    Public Property EPGAudioChannel As String

    Sub CanalSatGrabChannel()
        'Permet de grabber toutes les chaines existantes dans le EPG Canal
        Mod_XML.CreationEnteteXML()
        Try
            'de 0 à 1000 pour voir
            For i = 1 To 1000
                ChannelName = Nothing
                EPGLogoChannel = Nothing
                Try
                    ' on teste chaque chaine et on voit ce qu'on récupere
                    Dim CSatURL As String = $"https://hodor.canalplus.pro/api/v2/mycanal/channels/{IDCSat}/{i}/broadcasts/day/"
                    Dim jsonRequest As WebRequest = WebRequest.Create(CSatURL + CStr("0"))
                    Dim jsonResponse As HttpWebResponse = CType(jsonRequest.GetResponse(), HttpWebResponse)
                    Dim jsonStream As Stream = jsonResponse.GetResponseStream()
                    Dim jsonReader As New StreamReader(jsonStream)
                    Dim json As String = jsonReader.ReadToEnd()
                    Dim jsonObject As JObject = JObject.Parse(json)
                    Dim jsonArray As JArray = jsonObject("timeSlices")
                    Dim jsonlist As List(Of JToken), ContextListTitle As String = Nothing
                    Dim ListDetail As JObject, ListInformations As JObject
                    For Each item As JObject In jsonArray
                        'Recupere une partie des programmes
                        jsonlist = item.SelectToken("contents").ToList
                        For ii = 0 To jsonlist.Count - 1
                            EPGURLPage = jsonlist(ii).Item("onClick").ToString
                            EPGURLPage = Mid(EPGURLPage, InStr(EPGURLPage, "URLPage") + 11)
                            EPGURLPage = Replace(EPGURLPage, vbCrLf, "")
                            EPGURLPage = Mid(EPGURLPage, 1, EPGURLPage.Length - 2)
                            Dim jsonArray2 As JObject = jsonArray.Last
                            If jsonArray2.ContainsKey("context") Then
                                Dim ListContext As JObject = jsonArray2.SelectToken("context")
                                Dim ContextListTitleToken As JToken = ListContext.SelectToken("context_list_title")
                                ContextListTitle = Mid(ContextListTitleToken, 1, InStr(ContextListTitleToken, " - "))
                            End If

                            ' Recuperer les info de la page
                            Dim JsonRequest2 As WebRequest = WebRequest.Create(EPGURLPage)
                            Dim jsonResponse2 As HttpWebResponse = CType(JsonRequest2.GetResponse(), HttpWebResponse)
                            Dim jsonStream2 As Stream = jsonResponse2.GetResponseStream()
                            Dim jsonReader2 As New StreamReader(jsonStream2)
                            Dim json2 As String = jsonReader2.ReadToEnd()
                            Dim jsonObject2 As JObject = JObject.Parse(json2)
                            If jsonObject2.HasValues = True Then
                                Dim ListTracking As JObject = jsonObject2.SelectToken("tracking")
                                Dim ListOmniture As JObject = ListTracking.SelectToken("omniture")
                                ListDetail = jsonObject2.SelectToken("detail")
                                ListInformations = ListDetail.SelectToken("informations")
                                If ListInformations.ContainsKey("URLLogoChannel") Then EPGLogoChannel = ListInformations.Item("URLLogoChannel").ToString Else EPGLogoChannel = ""
                                If ListOmniture.ContainsKey("channel_name") Then
                                    If ContextListTitle = Nothing Or ContextListTitle = "J " Then ChannelName = Trim(ListOmniture.SelectToken("channel_name").ToString) Else ChannelName = Trim(ContextListTitle)
                                    If ChannelName = "RMC Sport" Then
                                        If NumRMCSport = 0 Then NumRMCSport = 5 Else NumRMCSport = NumRMCSport + 1
                                        ChannelName = $"{ChannelName} Live {NumRMCSport}"
                                    End If
                                    If ChannelName.Contains("F3 ") Then Replace(ChannelName, "F3 ", "FRANCE 3 ")
                                    Form1.TextBox1.Text = $"{vbCrLf}Grab de la chaine: {ChannelName} ({i}) {vbCrLf}{vbCrLf}{Form1.TextBox1.Text}"
                                    DoEvents()
                                    Exit Try
                                Else
                                    If jsonObject2.ContainsKey("episodes") = True Then
                                        Dim ListEpisodes As JObject = jsonObject2.SelectToken("episodes")
                                        Dim ListContents As JToken = ListEpisodes.SelectToken("contents")
                                        Dim ListContentsToken As JObject = ListContents.First
                                        Dim ListContentsAvail = ListContentsToken.SelectToken("contentAvailability")
                                        Dim ListAvailability As JObject = ListContentsAvail.SelectToken("availabilities")
                                        Dim ListLive As JObject = ListAvailability.SelectToken("live")
                                        Dim ListLiveToken As JToken = ListLive.Last
                                        If ListLive.ContainsKey("broadcasts") Then
                                            Dim ListBroadcast As JArray = ListLive.SelectToken("broadcasts")
                                            Dim ListBroadcastToken = ListBroadcast.First
                                            If ContextListTitle = Nothing Or ContextListTitle = "J " Then ChannelName = Trim(ListBroadcastToken.Item("channelName").ToString) Else ChannelName = Trim(ContextListTitle)
                                            Form1.TextBox1.Text = $"{vbCrLf}Grab de la chaine: {ChannelName} ({i}) {vbCrLf}{vbCrLf}{Form1.TextBox1.Text}"
                                            DoEvents()
                                            Exit Try
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                Catch ex As Exception
                    'Erreur page introuvable
                    '                    Form1.TextBox1.Text = $"Aucune Chaine pour le numéro {i}.{vbCrLf} {ex.Message}{vbCrLf}{vbCrLf}{Form1.TextBox1.Text}"

                    'on continue 
                End Try
                'Creation du XML Nom de chaine 
                If ChannelName <> Nothing Then
                    Mod_XML.CreationChannelXML(ChannelName, EPGLogoChannel, "Canalsat")
                    CanalSatGrabber(i)
                End If

            Next
        Catch ex As Exception
            MsgBox($"{ex.Message}{vbCrLf}{ex.TargetSite}{vbCrLf}{ex.StackTrace}", vbApplicationModal + vbOKOnly, "Erreur")
        End Try
    End Sub

    Sub CanalSatGrabber(ChannelNum As String)
        '-------------------https://hodor.canalplus.pro/api/v2/mycanal/channels/96119d61cb9cb943ac658699affb2314/312/broadcasts/day/1/
        Try
            Dim CSatURL As String = $"https://hodor.canalplus.pro/api/v2/mycanal/channels/{IDCSat}/{ChannelNum}/broadcasts/day/"
            Dim Planning As String = Nothing
            'Grabber MyCanal jour 0 = aujourd'hui a jour 15 s'il existe
            Dim DayCounter As Integer = 0

            For DayCounter = 0 To 15
                Form1.TextBox1.Text = $":{DayCounter}{Form1.TextBox1.Text}"
                DoEvents()
                'Recup des infos dans une variable

                ' Create a request for URL Data.

                Dim jsonRequest As WebRequest = WebRequest.Create(CSatURL + CStr(DayCounter))
                'request a response from the webpage
                Dim jsonResponse As HttpWebResponse = CType(jsonRequest.GetResponse(), HttpWebResponse)
                'Get Data from requested URL

                Dim jsonStream As Stream = jsonResponse.GetResponseStream()
                'Read Stream for easy access
                Dim jsonReader As New StreamReader(jsonStream)
                'Read Content
                Dim json As String = jsonReader.ReadToEnd()


                Dim jsonObject As JObject = JObject.Parse(json)
                Dim jsonArray As JArray = jsonObject("timeSlices")
                Dim jsonlist As List(Of JToken)

                For Each item As JObject In jsonArray
                    'Recupere une partie des programmes
                    jsonlist = item.SelectToken("contents").ToList

                    For i = 0 To jsonlist.Count - 1
                        EPGTitle = Nothing
                        EPGSubTitle = Nothing
                        EPGStartTimeA = Nothing
                        EPGURLPage = Nothing
                        EPGURLImage = Nothing
                        EPGParentalRatings = Nothing
                        EPGSubTitle2 = Nothing
                        EPGSummary = Nothing
                        EPGEditorialTitle = Nothing
                        EPGGenre = Nothing
                        EPGChannelName = Nothing
                        EPGChannelID = Nothing
                        EPGBroadcastID = Nothing
                        EPGStartTime = Nothing
                        EPGEndTime = Nothing
                        EPGDuree = Nothing

                        'Ajout dans le XML (mais pour le moment on va mettre dans une variable )
                        'Teste si le titre existe
                        Dim TitleError As Boolean = False
                        Try
                            EPGTitle = jsonlist(i).Item("title").ToString
                        Catch ex As Exception
                            TitleError = True
                        End Try

                        Dim SubTitleError As Boolean = False
                        Try
                            EPGSubTitle = jsonlist(i).Item("subtitle").ToString
                            If TitleError = True Then EPGTitle = EPGSubTitle
                        Catch ex As Exception
                            SubTitleError = True
                            If TitleError = True And SubTitleError = True Then
                                EPGTitle = "none"
                                EPGSubTitle = "none"
                            End If
                        End Try

                        Dim StartTimeA As Double = jsonlist(i).Item("startTime").ToString / 1000
                        Dim DateStartTimeA As DateTime = epoch.AddSeconds(StartTimeA)
                        Dim LocalTime = Date.Today.ToLocalTime - Date.Today.ToUniversalTime
                        Dim TimeStartLocal As Date = DateStartTimeA + LocalTime
                        EPGStartTimeA = TimeStartLocal.ToString("F")

                        EPGURLPage = jsonlist(i).Item("onClick").ToString
                        EPGURLPage = Mid(EPGURLPage, InStr(EPGURLPage, "URLPage") + 11)
                        EPGURLPage = Replace(EPGURLPage, vbCrLf, "")
                        EPGURLPage = Mid(EPGURLPage, 1, EPGURLPage.Length - 2)
                        If EPGURLPage.Contains(",") Then
                            EPGURLPage = Replace(EPGURLPage, $"{Chr(34)}", "")
                            EPGURLPage = EPGURLPage.Substring(0, InStr(EPGURLPage, ",") - 1)
                        End If
                        ' Recuperer les info de la page
                        Dim JsonRequest2 As WebRequest = WebRequest.Create(EPGURLPage)
                        Dim jsonResponse2 As HttpWebResponse = CType(JsonRequest2.GetResponse(), HttpWebResponse)
                        Dim jsonStream2 As Stream = jsonResponse2.GetResponseStream()
                        Dim jsonReader2 As New StreamReader(jsonStream2)
                        Dim json2 As String = jsonReader2.ReadToEnd()
                        Dim jsonObject2 As JObject = JObject.Parse(json2)
                        Dim ListDetail As JObject, ListInformations As JObject
                        Dim ValeurRating As JToken = Nothing
                        If jsonObject2.HasValues = True Then
                            Dim ListTracking As JObject = jsonObject2.SelectToken("tracking")
                            Dim ListOmniture As JObject = ListTracking.SelectToken("omniture")
                            EPGGenre = ListOmniture.Item("genre").ToString

                            ListDetail = jsonObject2.SelectToken("detail")
                            ListInformations = ListDetail.SelectToken("informations")
                            EPGURLImage = ListInformations.Item("URLImage").ToString
                            Dim ListParentalRatings = ListInformations.SelectToken("parentalRatings")
                            ValeurRating = ListParentalRatings.First
                            ValeurRating = ValeurRating("value").ToString
                            EPGParentalRatings = CSA(CInt(ValeurRating))
                            'si c'est une série
                            If jsonObject2.ContainsKey("episodes") = True Then
                                Dim ListEpisodes As JObject = jsonObject2.SelectToken("episodes")
                                Dim ListContents As JToken = ListEpisodes.SelectToken("contents")
                                Dim ListContentsToken As JObject = ListContents.First
                                EPGSubTitle2 = ListContentsToken.SelectToken("title").ToString
                                If ListContentsToken.ContainsKey("summary") Then EPGSummary = ListContentsToken.SelectToken("summary").ToString
                                EPGEditorialTitle = ListContentsToken.SelectToken("editorialTitle").ToString
                                Dim ListContentsAvail = ListContentsToken.SelectToken("contentAvailability")
                                Dim ListAvailability As JObject = ListContentsAvail.SelectToken("availabilities")
                                Dim ListLive As JObject = ListAvailability.SelectToken("live")
                                Dim ListLiveToken As JToken = ListLive.Last
                                Try
                                    Dim ListBroadcast As JArray = ListLive.SelectToken("broadcasts")
                                    Dim ListBroadcastToken = ListBroadcast.First
                                    EPGChannelName = ListBroadcastToken.Item("channelName").ToString
                                    EPGChannelID = ListBroadcast.Item(0)("channelId").ToString
                                    EPGBroadcastID = ListBroadcast.Item(0)("broadcastId").ToString
                                    Dim StartTime As Double = ListBroadcast.Item(0)("startTime").ToString
                                    Dim DateStartTime As DateTime = epoch.AddSeconds(StartTime)
                                    EPGStartTime = DateStartTime.ToString("yyyyMMddHHmmss")
                                    Dim EndTime As Double = ListBroadcast.Item(0)("endTime").ToString
                                    Dim DateEndTime As DateTime = epoch.AddSeconds(EndTime)
                                    EPGEndTime = DateEndTime.ToString("yyyyMMddHHmmss")
                                    Dim Duration As Double = (EndTime - StartTime)
                                    Dim DateDuration As DateTime = epoch.AddSeconds(Duration)
                                    If DateDuration.Hour >= 1 Then EPGDuree = (DateDuration.Hour * 60) + DateDuration.Minute Else EPGDuree = DateDuration.Minute
                                Catch ex As Exception
                                    Try
                                        EPGChannelName = ListContentsToken.Item("channelName").ToString
                                        EPGChannelID = ListContentsToken.Item(0)("channelId").ToString
                                        EPGBroadcastID = ListContentsToken.Item(0)("broadcastId").ToString
                                    Catch ex2 As Exception
                                        EPGChannelName = ChannelName
                                        EPGChannelID = ""
                                        EPGBroadcastID = ""
                                        EPGStartTime = Nothing
                                        EPGEndTime = Nothing
                                        If EPGEditorialTitle.Contains("min") Then
                                            Dim DurationExtract As String = Mid(EPGEditorialTitle, InStrRev(EPGEditorialTitle, ",") + 1)
                                            DurationExtract = Replace(DurationExtract, "min", "")
                                            EPGDuree = Trim(DurationExtract)
                                        End If
                                    End Try

                                End Try


                                'sinon 
                            Else
                                If ListInformations.ContainsKey("summary") Then EPGSummary = ListInformations.SelectToken("summary").ToString
                                If ListInformations.ContainsKey("editorialTitle") Then EPGEditorialTitle = ListInformations.SelectToken("editorialTitle").ToString
                                If ListInformations.ContainsKey("duration") Then EPGDuree = ListInformations.SelectToken("duration").ToString / 60000

                            End If
                            If EPGStartTime = Nothing Then
                                If EPGEndTime = Nothing Then
                                    Dim StartTime As DateTime = epoch.AddSeconds(StartTimeA)
                                    EPGStartTime = StartTime.ToString("yyyyMMddHHmmss")
                                    If EPGDuree <> Nothing Then
                                        EPGEndTime = EPGStartTime + (EPGDuree * 60)
                                    End If
                                End If
                            End If
                        End If
                        EPGLang = ""
                        EPGEpisode = ""
                        EPGProgRate = "csa"
                        EPGProgRateVal = ValeurRating.ToString
                        EPGAudioChannel = ""
                        Mod_XML.CreationEPGChannelXML("Canalsat", ChannelName, EPGStartTime, EPGEndTime, UTC2, EPGLang, EPGTitle, EPGSubTitle, EPGSummary, EPGGenre, EPGEpisode, EPGDuree, EPGURLImage, EPGAudioChannel, EPGProgRate, EPGProgRateVal)
                        Form1.TextBox1.Text = $".{Form1.TextBox1.Text}"
                        DoEvents()
                    Next

                Next


                jsonStream.Close()
                jsonReader.Close()
            Next
        Catch ex As Exception
            'MsgBox($"{ex.Message}{vbCrLf}{ex.TargetSite}{vbCrLf}{ex.StackTrace}", vbApplicationModal + vbOKOnly, "Erreur")
            Exit Try
        End Try
    End Sub



    Function CSA(Valeur As Integer) As String
        Select Case Valeur
            Case 1
                Return "Tout Public"
            Case 2
                Return "Interdit -10 Ans"
            Case 3
                Return "Interdit -12 Ans"
            Case 4
                Return "Interdit -16 Ans"
            Case 5
                Return "Adulte"
            Case Else
                Form1.TextBox1.Text = $"Chaine: {ChannelName} - Programme:  {EPGTitle} Identifiant: {Valeur}{vbCrLf}{vbCrLf}{Form1.TextBox1.Text}"
                Return $"Inconnu Identifiant {Valeur}"
        End Select
    End Function


End Module
