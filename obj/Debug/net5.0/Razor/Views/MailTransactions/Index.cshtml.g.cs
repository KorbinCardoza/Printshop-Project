#pragma checksum "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e2cd70f5c8dfc663a418cc28e0b41b83b8dd0c90"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MailTransactions_Index), @"mvc.1.0.view", @"/Views/MailTransactions/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\kcardoza\Documents\printshop\Views\_ViewImports.cshtml"
using PrintShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\kcardoza\Documents\printshop\Views\_ViewImports.cshtml"
using PrintShop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2cd70f5c8dfc663a418cc28e0b41b83b8dd0c90", @"/Views/MailTransactions/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3da70890ed0dc390fd0adfba6dbfec9a0ef82143", @"/Views/_ViewImports.cshtml")]
    public class Views_MailTransactions_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PrintShop.Models.Entity.MailTran>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateMailMeter", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("runaat", new global::Microsoft.AspNetCore.Html.HtmlString("server"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
  
    ViewData["Title"] = "Mail";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<link rel=""stylesheet"" type=""text/css"" href=""https://cdn.datatables.net/1.11.3/css/jquery.dataTables.css"">
    <script src=""https://code.jquery.com/jquery-3.6.0.js"" integrity=""sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk="" crossorigin=""anonymous""></script>
    <script src=""https://kit.fontawesome.com/5b2bd23d5f.js"" crossorigin=""anonymous""></script>
    <script type=""text/javascript"" charset=""utf8"" src=""https://cdn.datatables.net/1.11.3/js/jquery.dataTables.js""></script>
   <script>

   $(document).ready( function () {
    $('#filterTable').DataTable();
} );
</script>
");
            WriteLiteral("<h1  style=\"text-align: center\">Mail</h1>\r\n\r\n<p  style=\"text-align: center\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e2cd70f5c8dfc663a418cc28e0b41b83b8dd0c906004", async() => {
                WriteLiteral("Create New Mail Transaction");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n");
#nullable restore
#line 28 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
   
            var roles = @User.Claims.ToList();
            foreach(var r in roles)
            {
                if(r.Value == "Admin")
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("<p  style=\"text-align: center\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e2cd70f5c8dfc663a418cc28e0b41b83b8dd0c907662", async() => {
                WriteLiteral("Update Mail Meter");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n");
#nullable restore
#line 37 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                }}

#line default
#line hidden
#nullable disable
            WriteLiteral("<div style=\"text-align: center\">\r\n    <p>Please enter what you are searching for and click search</p>\r\n");
#nullable restore
#line 40 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
 using (Html.BeginForm("Index","MailTransactions", FormMethod.Post))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <label for=\"start\">Start date:</label>\r\n                <input type=\"date\" id=\"start\" name=\"tripstart\"                \r\n                 min=\"2015-01-01\"");
            BeginWriteAttribute("max", " max=\"", 1683, "\"", 1725, 1);
#nullable restore
#line 44 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
WriteAttributeValue("", 1689, DateTime.Now.ToString("yyyy-MM-dd"), 1689, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
            WriteLiteral("                <label for=\"start\">End date:</label>\r\n                <input type=\"date\" id=\"end\" name=\"tripend\"               \r\n                 min=\"2015-01-01\"");
            BeginWriteAttribute("max", " max=\"", 1893, "\"", 1935, 1);
#nullable restore
#line 48 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
WriteAttributeValue("", 1899, DateTime.Now.ToString("yyyy-MM-dd"), 1899, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 50 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
          Write(Html.DropDownList("action", new SelectList(ViewBag.Actions), "All Actions"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
          Write(Html.DropDownList("Activ", new SelectList(ViewBag.Activities), "All Activities"));

#line default
#line hidden
#nullable disable
            WriteLiteral("               <input type=\"submit\" class=\"btn btn-primary btn-sm\" value=\"Search\" />\r\n");
#nullable restore
#line 54 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
              
            }              

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e2cd70f5c8dfc663a418cc28e0b41b83b8dd0c9011542", async() => {
                WriteLiteral(@"
            <table id=""filterTable"" class=""display"" style=""border-collapse: separate; border-spacing: 1em; white-space: nowrap;"">
                <thead>
                    <tr>
                        <th>Activity</th>
                        <th>ProjectNumber</th>
                        <th>Task Number</th>
                        <th>Cost Center</th>
                        <th>Meter Start</th>
                        <th>Meter End</th>
                        <th>Cost</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 72 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
         
    if(Model != null)
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <tr>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 78 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(item.Activity);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 81 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.ProjectNumber));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 84 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.TaskNbr));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 87 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.CostCntr));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 90 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.MailMeterBegin));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 93 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.MailMeterEnd));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 96 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Cost));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td style = \"background-color:white\">\r\n                                ");
#nullable restore
#line 99 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Action));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 102 "C:\Users\kcardoza\Documents\printshop\Views\MailTransactions\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </tbody>\r\n            </table>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PrintShop.Models.Entity.MailTran>> Html { get; private set; }
    }
}
#pragma warning restore 1591
