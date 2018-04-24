Imports System.Web
Imports System.Linq
Imports System.Collections
Imports System.Data.Linq.Mapping

Namespace DevExpress.Razor.Models
    Public NotInheritable Class NorthwindDataProvider

        Private Sub New()
        End Sub
        Private Const NorthwindDataContextKey As String = "DXNorthwindDataContext"

        Public Shared ReadOnly Property DB() As NorthwindDataContext
            Get
                If HttpContext.Current.Items(NorthwindDataContextKey) Is Nothing Then
                    HttpContext.Current.Items(NorthwindDataContextKey) = New NorthwindDataContext()
                End If
                Return DirectCast(HttpContext.Current.Items(NorthwindDataContextKey), NorthwindDataContext)
            End Get
        End Property
        Public Shared Function GetEmployees() As IEnumerable
            Return From employee In DB.Employees _
                Select employee
        End Function
        Public Shared Function GetColumnsNames() As IEnumerable
            Dim model = (New AttributeMappingSource()).GetModel(GetType(NorthwindDataContext))
            Return From name In model.GetMetaType(GetType(Employee)).DataMembers _
                Select name.MappedName
        End Function
    End Class
End Namespace