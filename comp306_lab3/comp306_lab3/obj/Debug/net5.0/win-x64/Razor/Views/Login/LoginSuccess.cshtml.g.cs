#pragma checksum "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "a89b770679a55f2ddeee68caa5c913c5ea9f7adf8226f2b2d28717a2e03efdb0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_LoginSuccess), @"mvc.1.0.view", @"/Views/Login/LoginSuccess.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\_ViewImports.cshtml"
using comp306_lab3

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\_ViewImports.cshtml"
using comp306_lab3.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a89b770679a55f2ddeee68caa5c913c5ea9f7adf8226f2b2d28717a2e03efdb0", @"/Views/Login/LoginSuccess.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"3d8cfd648b4755fd729a7bfbe2223e0286b1e7827ec295767aa6fc8f722221f6", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Login_LoginSuccess : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<comp306_lab3.Models.UserModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n<div>\r\n    <h4>Hello ");
            Write(
#nullable restore
#line 4 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
               Model.Username

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
            Write(
#nullable restore
#line 8 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
             Html.DisplayNameFor(model => model.Id)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <!-- ");
            Write(
#nullable restore
#line 11 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
                  Html.DisplayFor(model => model.Id)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" -->\r\n            ");
            Write(
#nullable restore
#line 12 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
             Model.Id

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            Write(
#nullable restore
#line 15 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
             Html.DisplayNameFor(model => model.Username)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            Write(
#nullable restore
#line 18 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
             Html.DisplayFor(model => model.Username)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            Write(
#nullable restore
#line 21 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
             Html.DisplayNameFor(model => model.Password)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            Write(
#nullable restore
#line 24 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
             Html.DisplayFor(model => model.Password)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            Write(
#nullable restore
#line 29 "C:\Users\Benton\source\repos\comp306_lab3\comp306_lab3\Views\Login\LoginSuccess.cshtml"
     Html.ActionLink("Edit", "Edit", new { /* id = Model.PrimaryKey */ })

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a89b770679a55f2ddeee68caa5c913c5ea9f7adf8226f2b2d28717a2e03efdb06733", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<comp306_lab3.Models.UserModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
