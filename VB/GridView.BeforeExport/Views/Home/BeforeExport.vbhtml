@Imports GridView.BeforeExport
@Imports System.Web.UI.WebControls
@Code
    ViewBag.Title = "Home Page"
End Code
<script type="text/javascript">
    function ExportToPDF() {
        var names = GetSelectedItemsNames();
        if (!names) {
            alert("Please, choose columns for export of grid");
            return;
        }

        document.getElementById("ExportColumnsNames").value = names;
        document.forms[0].submit();
    }
    function GetSelectedItemsNames() {
        var selectedItems = columnNames.GetSelectedValues();
        var result = "";
        for (var index = 0; index < selectedItems.length; index++) {
            result += selectedItems[index] + ";";
        }
        return result;
    }
</script>
@Code
    Dim exportColumnsNames As String() =
        If(ViewData("ExportColumnsNames") = Nothing,
            New String() {"FirstName", "LastName", "HireDate"},
            DirectCast(ViewData("ExportColumnsNames"), String())
        )
End Code
@Using Html.BeginForm("ExportTo", "Home")
    @<text>
        <div style="padding: 10px;">
            <br />
            @Html.DevExpress().Button( _
                Sub(settings)
                    settings.Name = "exportToPdf"
                    settings.Text = "Export to PDF"
                    settings.ClientSideEvents.Click = "function(s, e) { ExportToPDF(); }"
                End Sub
            ).GetHtml()
            @Html.Hidden("ExportColumnsNames", Nothing)
        </div>
        <div style="float: left;">
            @Html.DevExpress().Label( _
                Sub(settings)
                    settings.Name = "title"
                    settings.Text = "Export columns: "
                End Sub
            ).GetHtml()
        </div>
        <div style="float: left; padding-left: 10px; padding-right: 10px;">
            @Html.DevExpress().ListBox( _
                Sub(settings)
                    settings.Name = "columnNames"
                    settings.Properties.SelectionMode = ListEditSelectionMode.CheckColumn
                    settings.Height = Unit.Pixel(225)
       
                    For Each name As String In NorthwindDataProvider.GetColumnsNames()
                        settings.Properties.Items.Add(name)
                        settings.Properties.Items.FindByText(name).Selected = exportColumnsNames.Contains(name)
                    Next
                End Sub
            ).GetHtml()
        </div>
        <div>        
            @Html.Partial("BeforeExportPartial", NorthwindDataProvider.GetEmployees())
        </div>
    </text>
End Using
