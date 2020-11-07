Imports System.Xml
Imports System.Text

Friend Module Mod_XML
    Sub CreationEnteteXML()
        'Déclaration XML et ajout dans le fichier
        Dim Decl As XmlDeclaration = XmlDoc.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, Nothing)
        XmlDoc.AppendChild(Decl)
        'Type de document <!DOCTYPE tv SYSTEM "xmltv.dtd">, Lecture du document xmltv.dtd dans une variable pour qu'elle soit lu dans le DocType. Puis on l'ajoute dans notre XML 
        Dim DocType As XmlDocumentType = XmlDoc.CreateDocumentType("tv", Nothing, "xmltv.dtd", xmltvdtd)
        XmlDoc.AppendChild(DocType)

        Dim Attr As XmlAttribute = Nothing
        Dim Elem As XmlElement = Nothing
        Dim Noeud As XmlNode = Nothing
        Dim Noeuds As XmlNodeList = Nothing

        'Début du balisage Root du nom de tv avec les attributs ajoutés 
        DocRoot = XmlDoc.CreateElement("tv")
        DocRoot.SetAttribute("generator-info-url", "http://informaweb.freeboxos.fr")
        DocRoot.SetAttribute("generator-info-name", "XMLTV")
        DocRoot.SetAttribute("source-data-url", "http://informaweb.freeboxos.fr")
        DocRoot.SetAttribute("source-info-url", "http://informaweb.freeboxos.fr")
        DocRoot.SetAttribute("source-info-name", "XMLTV Informaweb")
        XmlDoc.AppendChild(DocRoot)
    End Sub

    Sub CreationChannelXML(NomChaine As String, IMGChaine As String, GrabName As String)
        
        Dim DocChan As XmlElement = XmlDoc.CreateElement("channel")
        DocChan.SetAttribute("id", ChannelName)
        DocRoot.PrependChild(DocChan)
        Dim ChanName As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "display-name", Nothing)
        ChanName.InnerText = ChannelName
        DocChan.AppendChild(ChanName)
        Dim ChanIcon As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "icon", Nothing)
        Dim Attri As XmlAttribute = XmlDoc.CreateAttribute("src")
        Attri.Value = IMGChaine
        ChanIcon.Attributes.SetNamedItem(Attri)
        DocChan.AppendChild(ChanIcon)

        XmlDoc.Save($"{GrabName}.xml")
    End Sub

    Sub CreationEPGChannelXML(GrabName As String, NomChaine As String, StartTime As String, EndTime As String, LocalTime As String, Lang As String, Programme As String, ProgrammeSub As String, Description As String,
                              ProgrammeCat As String, EpisodeVal As String, Duree As String, ProgrammeIMG As String, AudioChannels As String, ProgrammeRate As String, ProgRatingVal As String)
        '<programme start = "20201101001000 +0100" Stop="20201101010000 +0100" channel="C2.api.telerama.fr">
        '   <title>New York, section criminelle</title>
        '   <sub-title>La mort vous va si bien</sub-title>
        '   <desc lang = "fr" > Saison : 4 - Episode:2 - Un célèbre photographe a été retrouvé mort, assassiné. Goren et Eames, chargés de l'enquête, se rendent rapidement compte que l'homme n'était pas un artiste comme les autres. Son sujet de prédilection était la mort, celles de jeunes femmes plus précisément, qu'il photographiait dans d'étranges positions. Les cadavres exposés dans ses oeuvres sont-ils réels ? Et si oui, où les a-t-il cachés ? Plusieurs disparitions jamais élucidées semblent tout à coup éclairées d'un jour nouveau. Goren et Eames auraient-ils sous les yeux la dépouille d'un tueur en série ? Et qui aurait ainsi rendu justice en éliminant le psychopathe ?...</desc>
        '   <category lang = "fr" > série policière</category>
        '   <length units = "minutes" > 50</length>
        '   <icon src = "https://television.telerama.fr/sites/tr_master/files/sheet_media/media/de7506f5-df5d-49a0-8722-55fc596c0456.jpg" />
        '   <episode-num system="xmltv_ns">3.1.</episode-num>
        '   <audio>
        '       <stereo>bilingual</stereo>
        '   </audio>
        '   <previously-shown />
        '   <rating system = "CSA" >
        '       <value>Tout public</value>
        '    </rating>
        '</programme>
        'Ajout du programme de la chaine dans la liste 
        Dim ChanProg As XmlElement = XmlDoc.CreateElement("programme")
        ChanProg.SetAttribute("channel", NomChaine)
        ChanProg.SetAttribute("stop", $"{EndTime} {LocalTime}")
        ChanProg.SetAttribute("start", $"{StartTime} {LocalTime}")
        DocRoot.AppendChild(ChanProg)

        Dim ProgTitle As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "title", Nothing)
        Dim TitleAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        TitleAttr.Value = Lang
        ProgTitle.Attributes.SetNamedItem(TitleAttr)
        ProgTitle.InnerText = Programme
        ChanProg.AppendChild(ProgTitle)

        Dim ProgSub As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "sub-title", Nothing)
        Dim SubAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        SubAttr.Value = Lang
        ProgSub.Attributes.SetNamedItem(SubAttr)
        ProgSub.InnerText = ProgrammeSub
        ChanProg.AppendChild(ProgSub)

        Dim ProgDesc As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "desc", Nothing)
        Dim DescAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        DescAttr.Value = Lang
        ProgDesc.Attributes.SetNamedItem(DescAttr)
        ProgDesc.InnerText = Description
        ChanProg.AppendChild(ProgDesc)

        Dim ProgCat As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "category", Nothing)
        Dim CatAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        CatAttr.Value = Lang
        ProgCat.Attributes.SetNamedItem(CatAttr)
        ProgCat.InnerText = ProgrammeCat
        ChanProg.AppendChild(ProgCat)

        Dim ProgEpisodeNum As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "episode-num", Nothing)
        Dim EpisodeNumAttr As XmlAttribute = XmlDoc.CreateAttribute("system")
        EpisodeNumAttr.Value = "xmltv_ns"
        ProgEpisodeNum.Attributes.SetNamedItem(EpisodeNumAttr)
        ProgEpisodeNum.InnerText = EpisodeVal
        ChanProg.AppendChild(ProgEpisodeNum)

        Dim ProgLength As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "length", Nothing)
        Dim LengthAttr As XmlAttribute = XmlDoc.CreateAttribute("units")
        LengthAttr.Value = "minutes"
        ProgLength.Attributes.SetNamedItem(LengthAttr)
        ProgLength.InnerText = Duree
        ChanProg.AppendChild(ProgLength)

        Dim ProgIcon As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "icon", Nothing)
        Dim IconAttr As XmlAttribute = XmlDoc.CreateAttribute("src")
        IconAttr.Value = ProgrammeIMG
        ProgIcon.Attributes.SetNamedItem(IconAttr)
        ChanProg.AppendChild(ProgIcon)

        Dim ProgAudio As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "audio", Nothing)
        ChanProg.AppendChild(ProgAudio)

        Dim AudioChan As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "stereo", Nothing)
        AudioChan.InnerText = AudioChannels
        ProgAudio.AppendChild(AudioChan)

        Dim ProgPrev As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "previously-shown", Nothing)
        ChanProg.AppendChild(ProgPrev)

        Dim ProgRating As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "rating", Nothing)
        Dim RatingAttr As XmlAttribute = XmlDoc.CreateAttribute("system")
        RatingAttr.Value = ProgrammeRate
        ProgRating.Attributes.SetNamedItem(RatingAttr)
        ChanProg.AppendChild(ProgRating)

        Dim RatingValue As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "value", Nothing)
        RatingValue.InnerText = ProgRatingVal
        ProgRating.AppendChild(RatingValue)

        XmlDoc.Save($"{GrabName}.xml")

    End Sub

    Sub CreationXML()
        'création d'une nouvelle instance du membre xmldocument
        '      Dim XmlDoc As XmlDocument = New XmlDocument
        'Déclaration XML et ajout dans le fichier
        Dim Decl As XmlDeclaration = XmlDoc.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, Nothing)
        XmlDoc.AppendChild(Decl)
        'Type de document <!DOCTYPE tv SYSTEM "xmltv.dtd">, Lecture du document xmltv.dtd dans une variable pour qu'elle soit lu dans le DocType. Puis on l'ajoute dans notre XML 
        Dim DocType As XmlDocumentType = XmlDoc.CreateDocumentType("tv", Nothing, "xmltv.dtd", xmltvdtd)
        XmlDoc.AppendChild(DocType)

        Dim Attr As XmlAttribute = Nothing
        Dim Elem As XmlElement = Nothing
        Dim Noeud As XmlNode = Nothing
        Dim Noeuds As XmlNodeList = Nothing

        'Début du balisage Root du nom de tv avec les attributs ajoutés 
        Dim DocRoot As XmlElement = XmlDoc.CreateElement("tv")
        DocRoot.SetAttribute("generator-info-url", "http://informaweb.freeboxos.fr")
        DocRoot.SetAttribute("generator-info-name", "XMLTV")
        DocRoot.SetAttribute("source-data-url", "http://informaweb.freeboxos.fr")
        DocRoot.SetAttribute("source-info-url", "http://informaweb.freeboxos.fr")
        DocRoot.SetAttribute("source-info-name", "XMLTV Informaweb")
        XmlDoc.AppendChild(DocRoot)

        '----------- TEST de boucle pour ajouter des chaines 
        Dim i As Integer = 10
        While i > 0
            '------- TEST Boucle
            'Début du balisage de Chaine TV (faire une fonction pour ajouter les chaines) id = identificateur chaine pour EPG
            Dim DocChan As XmlElement = XmlDoc.CreateElement("channel")
            DocChan.SetAttribute("id", "TF1.fr")
            DocRoot.AppendChild(DocChan)

            'Ajout du noeud du Nom de la chaine et ecrit sur le XML
            Dim ChanName As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "display-name", Nothing)
            ChanName.InnerText = "TF1"
            DocChan.AppendChild(ChanName)

            'Ajout du noeud du lien logo en Attribut du noeud 
            Dim ChanIcon As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "icon", Nothing)
            Dim Attri As XmlAttribute = XmlDoc.CreateAttribute("src")
            Attri.Value = "http://informaweb.freeboxos.fr/iptv/logos_tv/TF1.png"
            ChanIcon.Attributes.SetNamedItem(Attri)
            DocChan.AppendChild(ChanIcon)
            '------- TEST Boucle
            i = i - 1
        End While
        '----------- TEST Boucle

        'Ajout du programme de la chaine dans la liste 
        Dim ChanProg As XmlElement = XmlDoc.CreateElement("programme")
        ChanProg.SetAttribute("channel", "TF1")
        ChanProg.SetAttribute("stop", "20200531075400 +0200")
        ChanProg.SetAttribute("start", "20200531070000 +0200")
        DocRoot.AppendChild(ChanProg)

        Dim ProgTitle As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "title", Nothing)
        Dim TitleAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        TitleAttr.Value = "fr"
        ProgTitle.Attributes.SetNamedItem(TitleAttr)
        ProgTitle.InnerText = "Chicago Fire"
        ChanProg.AppendChild(ProgTitle)

        Dim ProgSub As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "sub-title", Nothing)
        Dim SubAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        SubAttr.Value = "fr"
        ProgSub.Attributes.SetNamedItem(SubAttr)
        ProgSub.InnerText = "Adieu Hallie"
        ChanProg.AppendChild(ProgSub)

        Dim ProgDesc As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "desc", Nothing)
        Dim DescAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        DescAttr.Value = "fr"
        ProgDesc.Attributes.SetNamedItem(DescAttr)
        ProgDesc.InnerText = $"Saison 1 Episode 23{vbCrLf}
            Lors de la veillée mortuaire de Hallie, Matthew se propose pour accompagner le sergent Voight et son équipe afin de retrouver le tueur quand il apprend qu'elle était morte avant le début de l'incendie, n'hésitant pas à mettre sa vie en danger pour cela. Peter est toujours bouleversé par la confession de Gabriela, ce qui pourrait avoir un impact négatif sur leur relation. L'impatience de Leslie ne cesse de grandir alors que s'approche le moment de son insémination. Pendant ce temps, tandis que se poursuivent les préparatifs de la grande ouverture du Molly, Otis convainc ses partenaires de le laisser engager sa cousine russe comme serveuse..."
        ChanProg.AppendChild(ProgDesc)

        Dim ProgCat As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "category", Nothing)
        Dim CatAttr As XmlAttribute = XmlDoc.CreateAttribute("lang")
        CatAttr.Value = "fr"
        ProgCat.Attributes.SetNamedItem(CatAttr)
        ProgCat.InnerText = "Série  Dramatique"
        ChanProg.AppendChild(ProgCat)

        Dim ProgEpisodeNum As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "episode-num", Nothing)
        Dim EpisodeNumAttr As XmlAttribute = XmlDoc.CreateAttribute("system")
        EpisodeNumAttr.Value = "xmltv_ns"
        ProgEpisodeNum.Attributes.SetNamedItem(EpisodeNumAttr)
        ProgEpisodeNum.InnerText = "0.22."
        ChanProg.AppendChild(ProgEpisodeNum)

        Dim ProgLength As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "length", Nothing)
        Dim LengthAttr As XmlAttribute = XmlDoc.CreateAttribute("units")
        LengthAttr.Value = "minutes"
        ProgLength.Attributes.SetNamedItem(LengthAttr)
        ProgLength.InnerText = "50"
        ChanProg.AppendChild(ProgLength)

        Dim ProgIcon As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "icon", Nothing)
        Dim IconAttr As XmlAttribute = XmlDoc.CreateAttribute("src")
        IconAttr.Value = "https://television.telerama.fr/sites/tr_master/files/sheet_media/media/546f5f3f-36f9-4435-a9db-d14c3a95d119.jpg"
        ProgIcon.Attributes.SetNamedItem(IconAttr)
        ChanProg.AppendChild(ProgIcon)

        Dim ProgAudio As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "audio", Nothing)
        ChanProg.AppendChild(ProgAudio)

        Dim AudioChan As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "stereo", Nothing)
        AudioChan.InnerText = "bilingual"
        ProgAudio.AppendChild(AudioChan)

        Dim ProgPrev As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "previously-shown", Nothing)
        ChanProg.AppendChild(ProgPrev)

        Dim ProgRating As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "rating", Nothing)
        Dim RatingAttr As XmlAttribute = XmlDoc.CreateAttribute("system")
        RatingAttr.Value = "csa"
        ProgRating.Attributes.SetNamedItem(RatingAttr)
        ChanProg.AppendChild(ProgRating)

        Dim RatingValue As XmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "value", Nothing)
        RatingValue.InnerText = "-TP"
        ProgRating.AppendChild(RatingValue)





        '   <audio>
        '       <stereo>bilingual</stereo>
        '   </audio>
        '   <previously-shown />
        '   <rating system="CSA">
        '       <value>Tout public</value>
        '   </rating>
        '</programme>


        'Enregistrement du fichier 
        XmlDoc.Save("OutDocument.xml")


    End Sub
End Module
