#pragma checksum "D:\T1.2009.A0\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\EquipmentForClinic.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5d4b23a538919fc6a5e8cfb1b2291800aa98bfe8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Home_EquipmentForClinic), @"mvc.1.0.view", @"/Areas/Admin/Views/Home/EquipmentForClinic.cshtml")]
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
#line 1 "D:\T1.2009.A0\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\_ViewImports.cshtml"
using Clinic_web_app;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\T1.2009.A0\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\_ViewImports.cshtml"
using Clinic_web_app.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d4b23a538919fc6a5e8cfb1b2291800aa98bfe8", @"/Areas/Admin/Views/Home/EquipmentForClinic.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bc7f96fe8c344ee5a198c9e9381e083aa7b7862", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Home_EquipmentForClinic : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\T1.2009.A0\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\EquipmentForClinic.cshtml"
  
    ViewData["Title"] = "EquipmentForClinic";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""content-wrapper"">
    <!-- Content Header (Page header) -->
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row mb-2"">
                <div class=""col-sm-6"">
                    <h1 class=""m-0"">Equipment For Clinic</h1>
                </div><!-- /.col -->
                <div class=""col-sm-6"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                        <li class=""breadcrumb-item active"">Equipment For Clinic</li>
                    </ol>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <div class=""row"">
                <table class=""table table-hover"">
                    <thead class=""thead-dark"">
                      ");
            WriteLiteral(@"  <tr>
                            <td>Equipment Id</td>
                            <td>Image</td>
                            <td>Type</td>
                            <td>Equipment Name</td>
                            <td>Brand Name</td>
                            <td>Price</td>
                            <td>Quantity</td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </section>
    <!-- /.content -->
</div>

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591