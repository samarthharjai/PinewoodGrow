#pragma checksum "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f4bb52f78ccec39881925b5ca488dfabfc8a3af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Members_Details), @"mvc.1.0.view", @"/Views/Members/Details.cshtml")]
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
#line 1 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\_ViewImports.cshtml"
using PinewoodGrow;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\_ViewImports.cshtml"
using PinewoodGrow.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f4bb52f78ccec39881925b5ca488dfabfc8a3af", @"/Views/Members/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d40aafa2a956cb9f4837a45d08862015ed55438c", @"/Views/_ViewImports.cshtml")]
    public class Views_Members_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PinewoodGrow.Models.Member>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Download", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
  
	ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 7 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
Write(Html.DisplayFor(model => model.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<div>\r\n    <br />\r\n    <div class=\"table-responsive-lg\">\r\n        <table class=\"table table-striped\">\r\n            <thead>\r\n                <tr>\r\n                    <th scope=\"col\">");
#nullable restore
#line 15 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 16 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 17 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 18 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 19 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 20 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Address.FullAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 21 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Address.City));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 22 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Address.PostalCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 23 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Telephone));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n");
            WriteLiteral("                </tr>\r\n            </thead>\r\n            <tbody>\r\n                <tr>\r\n                    <td>");
#nullable restore
#line 29 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 30 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 31 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 32 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Gender.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 33 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 34 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Address.FullAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 35 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Address.City));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 36 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Address.PostalCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 37 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Telephone));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                </tr>
            </tbody>
        </table>
    </div>

    <br />
    <div class=""table-responsive-lg"">
        <table class=""table table-sm table-striped"">
            <thead>
                <tr>
                    <th>Family Situation</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th>");
#nullable restore
#line 54 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.MemberSituations));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>\r\n");
#nullable restore
#line 56 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                          
                            int sitCount = Model.MemberSituations.Count;
                            if (sitCount > 0)
                            {
                                string firstSit = Model.MemberSituations.FirstOrDefault().Situation.Name;
                                if (sitCount > 1)
                                {
                                    string sitList = firstSit;
                                    var s = Model.MemberSituations.ToList();
                                    for (int i = 1; i < sitCount; i++)
                                    {
                                        sitList += ", " + s[i].Situation.Name;
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <a tabindex=\"0\"");
            BeginWriteAttribute("class", " class=\"", 3236, "\"", 3244, 0);
            EndWriteAttribute();
            WriteLiteral(" role=\"button\" data-toggle=\"popover\"\r\n                                       data-trigger=\"focus\" title=\"Situations\" data-placement=\"bottom\"\r\n                                       data-content=\"");
#nullable restore
#line 71 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                Write(sitList);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                        ");
#nullable restore
#line 72 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                   Write(firstSit);

#line default
#line hidden
#nullable disable
            WriteLiteral("... <span class=\"badge badge-info\">");
#nullable restore
#line 72 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                                               Write(sitCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                    </a>\r\n");
#nullable restore
#line 74 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                }
                                else
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(firstSit);

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                             
                                }
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 84 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Income));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 85 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Income));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 88 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.FamilySize));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 89 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.FamilySize));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 92 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Household));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 93 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Household.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 96 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.CompletedBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 97 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.CompletedBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 100 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.CompletedOn));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 101 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.CompletedOn));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <div class=""table-responsive-lg"">
        <table class=""table table-striped"">
            <thead>
                <tr>
                    <th class=""table-responsive-lg"">Additaional Information</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th>");
#nullable restore
#line 117 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Notes));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 118 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Notes));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 121 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.MemberDietaries));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>\r\n");
#nullable restore
#line 123 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                          
                            int condCount = Model.MemberDietaries.Count;
                            if (condCount > 0)
                            {
                                string firstCond = Model.MemberDietaries.FirstOrDefault().Dietary.Name;
                                if (condCount > 1)
                                {
                                    string condList = firstCond;
                                    var c = Model.MemberDietaries.ToList();
                                    for (int i = 1; i < condCount; i++)
                                    {
                                        condList += ", " + c[i].Dietary.Name;
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <a tabindex=\"0\"");
            BeginWriteAttribute("class", " class=\"", 6361, "\"", 6369, 0);
            EndWriteAttribute();
            WriteLiteral(" role=\"button\" data-toggle=\"popover\"\r\n                                       data-trigger=\"focus\" title=\"Dietart Conditions\" data-placement=\"bottom\"\r\n                                       data-content=\"");
#nullable restore
#line 138 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                Write(condList);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                        ");
#nullable restore
#line 139 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                   Write(firstCond);

#line default
#line hidden
#nullable disable
            WriteLiteral("... <span class=\"badge badge-info\">");
#nullable restore
#line 139 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                                                Write(condCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                    </a>\r\n");
#nullable restore
#line 141 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                }
                                else
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 144 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               Write(firstCond);

#line default
#line hidden
#nullable disable
#nullable restore
#line 144 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                              
                                }
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 151 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.MemberDocuments));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>\r\n");
#nullable restore
#line 153 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                          
                            int fileCount = Model.MemberDocuments.Count;
                            if (fileCount > 0)
                            {
                                var firstFile = Model.MemberDocuments.FirstOrDefault(); ;
                                if (fileCount > 1)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <a");
            BeginWriteAttribute("class", " class=\"", 7559, "\"", 7567, 0);
            EndWriteAttribute();
            WriteLiteral(" role=\"button\" data-toggle=\"collapse\"");
            BeginWriteAttribute("href", " href=\"", 7605, "\"", 7636, 2);
            WriteAttributeValue("", 7612, "#collapseDocs", 7612, 13, true);
#nullable restore
#line 160 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
WriteAttributeValue("", 7625, Model.ID, 7625, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" aria-expanded=\"false\"");
            BeginWriteAttribute("aria-controls", " aria-controls=\"", 7659, "\"", 7698, 2);
            WriteAttributeValue("", 7675, "collapseDocs", 7675, 12, true);
#nullable restore
#line 160 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
WriteAttributeValue("", 7687, Model.ID, 7687, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <span class=\"badge badge-info\">");
#nullable restore
#line 161 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                                  Write(fileCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> Documents...\r\n                                    </a>\r\n                                    <div class=\"collapse\"");
            BeginWriteAttribute("id", " id=\"", 7904, "\"", 7932, 2);
            WriteAttributeValue("", 7909, "collapseDocs", 7909, 12, true);
#nullable restore
#line 163 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
WriteAttributeValue("", 7921, Model.ID, 7921, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 164 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                          
                                            foreach (var d in Model.MemberDocuments)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5f4bb52f78ccec39881925b5ca488dfabfc8a3af25816", async() => {
#nullable restore
#line 167 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                                                         Write(d.FileName);

#line default
#line hidden
#nullable disable
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
#line 167 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                                           WriteLiteral(d.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" <br />\r\n");
#nullable restore
#line 168 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                            }
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </div>\r\n");
#nullable restore
#line 171 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5f4bb52f78ccec39881925b5ca488dfabfc8a3af28878", async() => {
#nullable restore
#line 174 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                                                     Write(firstFile.FileName);

#line default
#line hidden
#nullable disable
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
#line 174 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                                               WriteLiteral(firstFile.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 175 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                                }
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 181 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Consent));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 182 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Consent));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row\" style=\"margin: 0 auto; padding: 10px; justify-content:center;\">\r\n    <div class=\"form-group\" style=\"padding-right: 20px\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5f4bb52f78ccec39881925b5ca488dfabfc8a3af32474", async() => {
                WriteLiteral("Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5f4bb52f78ccec39881925b5ca488dfabfc8a3af33767", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 194 "C:\Users\Aidan\Documents\GitHub\PinewoodGrow\Views\Members\Details.cshtml"
                               WriteLiteral(Model.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\t<script type=\"text/javascript\">\r\n        $(function () {\r\n            $(\'[data-toggle=\"popover\"]\').popover();\r\n        });\r\n\t</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PinewoodGrow.Models.Member> Html { get; private set; }
    }
}
#pragma warning restore 1591
