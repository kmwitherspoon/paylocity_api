<%@ Page Language="C#" Inherits="PaylocityAPI.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <!-- Common Kendo UI CSS for web widgets and widgets for data visualization. -->
    <link href="kendo/styles/kendo.common.min.css" rel="stylesheet" />

    <!-- (Optional) RTL CSS for Kendo UI widgets for the web. Include only in right-to-left applications. -->
    <link href="kendo/styles/kendo.rtl.min.css" rel="stylesheet" type="text/css" />

    <!-- Default Kendo UI theme CSS for web widgets and widgets for data visualization. -->
    <link href="kendo/styles/kendo.common-material.min.css" rel="stylesheet" />
     <link href="kendo/styles/kendo.material.min.css" rel="stylesheet" />

    <!-- (Optional) Kendo UI Hybrid CSS. Include only if you will use the mobile devices features. -->
    <link href="kendo/styles/kendo.default.mobile.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery JavaScript -->
    <script src="kendo/js/jquery.min.js"></script>

    <!-- Kendo UI combined JavaScript -->
    <script src="kendo/js/kendo.all.min.js"></script>
    <script type="text/javascript" src="./PaylocityUI/JS/Employees.js" mce_src="./PaylocityUI/JS/Employees.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Lato&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Anton&display=swap" rel="stylesheet">
        
    <script type="text/javascript">
        $(window).load(function() {
            var employeeObjects = new Employee_Objects();
            employeeObjects.populateEmployees();
            $("#navigation").kendoMenu();
        });
    </script>
   <!-- Beggining Template -->
    <script type="text/x-kendo-template" id="detailsTemplate">
        <div class="tabstrip">
            <ul>
                <li class="k-state-active">
                   Dependents
                </li>
                <li>
                    Benefits Breakdown
                </li>
            </ul>
            <div>
                <div class="dependents"></div>
            </div>
            <div>
                <div class='employee-details'>
                    <div class='benefitsTabstrip'>
                        <ul>
                            <li class="k-state-active">
                               Yearly
                            </li>
                            <li>
                                Per Paycheck
                            </li>
                        </ul>
                        <div>
                            <ul class='yearlyBenefitDetails'>
                                <li><label>Employee Benefits Cost: </label> $#= EmployeeBenefitCost #</li>
                                <li><label>Dependent Benefits Cost: </label> $#= DependentsBenefitCost #</li>
                                <li><label>Name Discount: </label> $#= Discount #</li>
                                <li><label>Total Benefits Cost: </label> $#= TotalBenefitCost #</li>
                            </ul>
                        </div>
                        <div>
                            <ul class='paycheckBenefitDetails'>
                                <li><label>Employee Benefits Cost: </label> $#= PaycheckEmployeeBenefitCost #</li>
                                <li><label>Dependent Benefits Cost: </label> $#= PaycheckDependentBenefitCost #</li>
                                <li><label>Name Discount: </label> $#= PaycheckDiscount #</li>
                                <li><label>Total Benefits Cost: </label> $#= PaycheckTotalBenefitCost #</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </script>
   <!-- End of Template -->
        
    <title>Schrute Industries</title>
    </head>
    <body>
        <header>
            <nav>
              <a id="logo" href="." rel="home">SCHRUTE INDUSTRIES</a>
              <ul id="navigation">
                <li>
                    <a class="home" href="/">Home</a>
                </li>
              </ul>
            </nav>
        </header>
        
       <div class="main">
            <div id="app"></div>
            <div id="details"></div>
        </div>
    <style>
        header {
            font-family: 'Anton', sans-serif;
            font-size: 30px;
            
        }
        
        #logo {
            text-decoration: none;
            color: #3343a4;
        }
            
        .home{
            font-size: 25px;
        }
        
        #navigation {
            color: #3343a4 !important;
        }

        body {
            font-family: 'Lato', sans-serif;
        }
        /* need edit functionality in order to cancel on Add */
        .dependents .k-grid-edit{
            display: none !important
        }
        .yearlyBenefitDetails{
            list-style: none;
        }
            
        .paycheckBenefitDetails{
            list-style: none;
        }
        .discountTrue{
            color: green;
            font-weight: bold;
        }
        
        .yearlyBenefitDetails li {
            padding: 10px 10px 10px 0px;
        }
            
        .paycheckBenefitDetails li {
            padding: 10px 10px 10px 0px;
        }
        
    </style>
        
</body>
</html>
