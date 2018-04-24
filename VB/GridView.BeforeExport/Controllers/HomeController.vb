Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports DevExpress.Razor.Models
Imports System.Web.UI

Namespace DevExpress.Razor.Controllers
    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            Return BeforeExport()
        End Function
        Public Function BeforeExport() As ActionResult
            Return View("BeforeExport", NorthwindDataProvider.GetEmployees())
        End Function
        Public Function BeforeExportPartial() As ActionResult
            Return PartialView("BeforeExportPartial", NorthwindDataProvider.GetEmployees())
        End Function
        Public Function ExportTo() As ActionResult
            Return GridViewExtension.ExportToPdf(GridViewHelper.GetExportSettings(Request.Params("ExportColumnsNames")), NorthwindDataProvider.GetEmployees())
        End Function
    End Class

    Public NotInheritable Class GridViewHelper

        Private Sub New()
        End Sub

        Public Shared Function GetExportSettings(ByVal itemsNames As String) As GridViewSettings
            Dim gridVieewSettings As GridViewSettings = GetExportSettings()

            If Not String.IsNullOrEmpty(itemsNames) Then
                Dim names() As String = itemsNames.Split(";"c)
                gridVieewSettings.SettingsExport.BeforeExport = Sub(sender, e)
                    Dim gridView As MVCxGridView = TryCast(sender, MVCxGridView)
                    If sender Is Nothing Then
                        Return
                    End If

                    gridView.Columns.Clear()

                    For Each name In names
                        If String.IsNullOrEmpty(name) Then
                            Continue For
                        End If
                        gridView.Columns.Add(New MVCxGridViewColumn(name))
                    Next name
                End Sub
            End If

            Return gridVieewSettings
        End Function
        Public Shared Function GetExportSettings() As GridViewSettings
            Dim gridVieewSettings As New GridViewSettings()
            gridVieewSettings.Name = "gridView"
            gridVieewSettings.CallbackRouteValues = New With { _
                Key .Controller = "Home", _
                Key .Action = "BeforeExportPartial" _
            }

            gridVieewSettings.Columns.Add("FirstName")
            gridVieewSettings.Columns.Add("LastName")
            gridVieewSettings.Columns.Add("BirthDate")
            gridVieewSettings.Columns.Add("Title")

            Return gridVieewSettings
        End Function
    End Class
End Namespace
