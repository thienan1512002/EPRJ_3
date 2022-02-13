#pragma checksum "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57a6085a405b048d1b5862ac6605e0027d164748"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_EcomerceEquipDetails_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/EcomerceEquipDetails/Index.cshtml")]
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
#line 1 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\_ViewImports.cshtml"
using Clinic_web_app;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\_ViewImports.cshtml"
using Clinic_web_app.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57a6085a405b048d1b5862ac6605e0027d164748", @"/Areas/Admin/Views/EcomerceEquipDetails/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bc7f96fe8c344ee5a198c9e9381e083aa7b7862", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_EcomerceEquipDetails_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Clinic_web_app.Models.EcomerceEquipDetail>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<script src=""https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js""></script>

<div class=""content-wrapper"">
    <!-- Content Header (Page header) -->
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row mb-2"">
                <div class=""col-sm-6"">
                    <h1 class=""m-0"">Equipment Order Lists</h1>
                    <div class=""form-outline col-6 right"">
                        <input type=""search"" id=""myInput"" class=""form-control"" placeholder=""Enter name to search"" aria-label=""Search"" />
                    </div>
                </div><!-- /.col -->
                <div class=""col-sm-6"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                        <li class=""breadcrumb-item active"">Staff Account</li>
                    </ol>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-");
            WriteLiteral(@"- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <div class=""row"">
                <table class=""table"">
                    <thead class=""thead-dark"">
                        <tr>
                            <th>
                                Order No
                            </th>
                            <th>
                                Customer who buys
                            </th>
                            <th>
                                Product
                            </th>
                            <th>
                                Date Create Order
                            </th>
                            <th>
                                Status
                            </th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 59 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                         foreach (var item in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 63 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                               Write(item.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 66 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                               Write(item.OrderDetail.Customer.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 69 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Equipment.EquipmentName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 72 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.OrderDetail.OrderDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n");
#nullable restore
#line 75 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                      
                                        switch (item.OrderDetail.Status)
                                        {
                                            case "Pending":
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <span class=\"badge badge-primary\">");
#nullable restore
#line 80 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                                                 Write(item.OrderDetail.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 81 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                    break;
                                                }
                                            case "Decline":
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <span class=\"badge badge-danger\">");
#nullable restore
#line 85 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                                                Write(item.OrderDetail.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 86 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                    break;
                                                }
                                            case "Completed":
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <span class=\"badge badge-success\">");
#nullable restore
#line 90 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                                                 Write(item.OrderDetail.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 91 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                    break;
                                                }
                                        }
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                                <td>                                    \r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "57a6085a405b048d1b5862ac6605e0027d16474812448", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 97 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                                                              WriteLiteral(item.OrderId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("                                    \r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 100 "C:\Users\nhta1\OneDrive\Documents\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\EcomerceEquipDetails\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </tbody>
                </table>
            </div>
        </div>
    </section>
    <!-- /.content -->
</div>

<script>
    $(document).ready(function () {
        $(""#myInput"").on(""keyup"", function () {
            var value = $(this).val().toLowerCase();
            $(""#myTable tr"").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Clinic_web_app.Models.EcomerceEquipDetail>> Html { get; private set; }
    }
}
#pragma warning restore 1591
