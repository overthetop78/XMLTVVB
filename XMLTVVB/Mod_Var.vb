Imports System.Xml
Imports System.IO

Module Mod_Var
    'Liste des Variables
    Public Const dblquote As String = Chr(34)
    Public Const urlsite As String = "http://informaweb.freeboxos.fr"
    Public Const srcinfo = "Informaweb XMLTV"
    Public Const geninfoname = "Informaweb XMLTV"
    Public Const geninfourl As String = "http://informaweb.freeboxos.fr"
    Public xmltvdtd As String = File.ReadAllText(Application.StartupPath & "\xmltv.dtd")
    Public XMLDocType As XmlDocumentType = Nothing
    Public XMLNomChaine As XmlElement
    Public XmlDoc As XmlDocument = New XmlDocument, DocRoot As XmlElement
    Public NomChaine As String = Nothing
    Public epoch As New DateTime(1970, 1, 1)
    Public UTC2 As String = Replace(Date.Today.ToString("%K"), ":", "")
    Public Const IDCSat As String = "96119d61cb9cb943ac658699affb2314"
    Public NumRMCSport As Integer = 0




End Module
