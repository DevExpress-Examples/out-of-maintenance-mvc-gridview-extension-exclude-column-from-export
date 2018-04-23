Imports System.Data.Linq.Mapping

Public NotInheritable Class NorthwindDataProvider
    Private Sub New()
    End Sub
    Const NorthwindDataContextKey As String = "DXNorthwindDataContext"

    Public Shared ReadOnly Property DB() As NorthwindDataContext
        Get
            If HttpContext.Current.Items(NorthwindDataContextKey) Is Nothing Then
                HttpContext.Current.Items(NorthwindDataContextKey) = New NorthwindDataContext()
            End If
            Return DirectCast(HttpContext.Current.Items(NorthwindDataContextKey), NorthwindDataContext)
        End Get
    End Property
    Public Shared Function GetEmployees() As IEnumerable
        Return From employee In DB.Employees Select employee
    End Function
    Public Shared Function GetColumnsNames() As IEnumerable
        Dim model = New AttributeMappingSource().GetModel(GetType(NorthwindDataContext))
        Return From name In model.GetMetaType(GetType(Employee)).DataMembers Select name.MappedName
    End Function
End Class