#pragma checksum "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "896c457903b3c22189c29f866cd52c342c60df4d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vendors_Index), @"mvc.1.0.view", @"/Views/Vendors/Index.cshtml")]
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
#line 1 "D:\study\web projects\SPMS\SPMS\Views\_ViewImports.cshtml"
using SPMS;

#line default
#line hidden
#line 2 "D:\study\web projects\SPMS\SPMS\Views\_ViewImports.cshtml"
using SPMS.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"896c457903b3c22189c29f866cd52c342c60df4d", @"/Views/Vendors/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ac29d2e9cdae28d566b4e8af7fece0f75b0dbdf2", @"/Views/_ViewImports.cshtml")]
    public class Views_Vendors_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SPMS.Models.Vendor>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#line 3 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
  
    ViewData["Title"] = "Vendor List";

#line default
#line hidden
            WriteLiteral("\r\n<div class=\"p-4\">\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n            <h2 style=\"color:#177b8b\">Vendor List</h2>\r\n        </div>\r\n        <div class=\"col-6 text-right\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "896c457903b3c22189c29f866cd52c342c60df4d4661", async() => {
                WriteLiteral("<i class=\"fas fa-plus\"></i> &nbsp;Add New Vendor");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
    <br />
    <br />
    <div class=""table-responsive rounded p-3"" style=""background-color:#d6f1eb"">
        <table id=""tableData"" class=""table table-hover table-bordered"" style=""width:100%"">
            <thead class=""text-white text-center"" style=""background-color:#177b8b"">
                <tr>
                    <th>
                        ");
#line 23 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Vender_Name));

#line default
#line hidden
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#line 26 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Contact_Person_Name));

#line default
#line hidden
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#line 29 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Contect_Person_No));

#line default
#line hidden
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#line 36 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#line 40 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Vender_Name));

#line default
#line hidden
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#line 43 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Contact_Person_Name));

#line default
#line hidden
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#line 46 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Contect_Person_No));

#line default
#line hidden
            WriteLiteral("\r\n                        </td>\r\n                        <td class=\"text-center\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "896c457903b3c22189c29f866cd52c342c60df4d8569", async() => {
                WriteLiteral("<i class=\"far fa-edit\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 49 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                                                   WriteLiteral(item.Vender_Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td class=\"text-center\">\r\n                            <a");
            BeginWriteAttribute("onclick", " onclick=\"", 2067, "\"", 2117, 3);
            WriteAttributeValue("", 2077, "Delete(\'vendors/Delete/", 2077, 23, true);
#line 52 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
WriteAttributeValue("", 2100, item.Vender_Id, 2100, 15, false);

#line default
#line hidden
            WriteAttributeValue("", 2115, "\')", 2115, 2, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger text-white\"><i class=\"far fa-trash-alt\"></i></a>\r\n                        </td>\r\n                    </tr>\r\n");
#line 55 "D:\study\web projects\SPMS\SPMS\Views\Vendors\Index.cshtml"
                }

#line default
#line hidden
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        function Delete(delUrl) {
            swal({
                title: ""Are you sure you want to delete?"",
                text: ""You will not be able to restore data!"",
                icon: ""warning"",
                buttons: true,
                dangerMode: true
            }).then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: ""DELETE"",
                        url: delUrl,
                        success: function (data) {
                            if (data.success) {
                                window.location.reload();
                            }
                            else {
                                swal({
                                    text: data.message,
                                    icon: ""error""
                                });
                            }
                        }
                    });
                }
            });
        };
   ");
                WriteLiteral(" </script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SPMS.Models.Vendor>> Html { get; private set; }
    }
}
#pragma warning restore 1591
