function Employee_Objects() {

}

Employee_Objects.prototype.populateEmployees = function () {


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.ajax({
                    url: "http://127.0.0.1:8080/GetAllEmployeeInformation",
                    dataType: "json",
                    type: "GET",
                    cache: false,
                    xhrFields: {
                        withCredentials: true
                    },
                    success: function (result) {
                        options.success(result);
                    }
                })
            },
            create: function (options) {
                $.ajax({
                    url: "http://127.0.0.1:8080/InsertEmployee?lastName=" + options.data.LastName + "&firstName=" + options.data.FirstName + "&salary=" + options.data.Salary,
                    dataType: "json",
                    type: "POST",
                    xhrFields: {
                        withCredentials: true
                    },
                    success: function (result) {
                        options.success(result);
                        var grid = $("#app").data("kendoGrid");
                        grid.dataSource.read();
                    },
                    error: function (result) {
                        options.error(result);
                    }
                })
            }
        },
        schema: {
            model: {
                id: "EmployeeId",
                fields: {
                    EmployeeId: { type: "number", editable: false, defaultValue: null },
                    FirstName: { type: "string", validation: { required: true } },
                    LastName: { type: "string", validation: { required: true } },
                    Salary: { type: "number", validation: { required: true }, defaultValue: 52000 } ,
                    TotalBenefitCost: { type: "number", editable: false, defaultValue: 1000 },
                    EmployeeBenefitCost: { type: "number", editable: false},
                    DependentsBenefitCost: { type: "number", editable: false},
                    Dependents: {
                        EmployeeId: { type: "number", editable: false},
                        FirstName: { type: "string" },
                        LastName: { type: "string" },
                        BenefitCost: { type: "number", editable: false, defaultValue: 500 },
                        Discount: { type: "number", editable: false}
                    }
                }
            }
        },
        pageSize: 20,
    });

    var selectedItem;

    $("#app").kendoGrid({
        dataSource: dataSource,
        height: 550,
        scrollable: true,
        sortable: true,
        filterable: true,
        pageable: {
            input: true,
            numeric: false
        },
        dataBound: function (e) {
            //if a row was previously selected, push updated dataItem to window
            if (selectedItem) {
                var dataItem = this.dataSource.get(selectedItem);
                this.select($('[data-uid=' + dataItem.uid + ']'));
                loadWindowContent(dataItem);
                selectedItem = undefined;
            }
        },
        editable: {
            mode: "popup",
            window: {
                resizable: true
            },
            template: $("#template").html()
        },
        edit: function (e) {
            if (e.model.isNew()) {
                e.container.kendoWindow("title", "Add New Employee");
            }
        },
        selectable: "single",
        toolbar: [{ name: "create", text: "Add New Employee"}],
        columns: [
            { field: "EmployeeId", title: "Employee Id", filterable: true },
            { field: "FirstName", title: "First Name", filterable: true },
            { field: "LastName", title: "Last Name", filterable: true },
            { field: "Salary", title: "Salary", format: "{0:c}", filterable: true },
            { field: "TotalBenefitCost", title: "Benefits Cost", format: "{0:c}", filterable: true},
            { command: [{ name: "See Details", click: showDetails }], width: "200px" }
        ]
    });

    var wnd = $("#details").kendoWindow({
        title: "Employee Details",
        modal: true,
        visible: false,
        resizable: true,
        width: 800
    }).data("kendoWindow");

    var detailsTemplate = kendo.template($("#detailsTemplate").html());

    function showDetails(e) {
        var grid = $("#app").data("kendoGrid");
        grid.select( $(e.currentTarget).closest("tr"));

        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        loadWindowContent(dataItem);
    }
        
    //loads window data and widgets 
    function loadWindowContent(dataItem) {

        dataItem.PaycheckEmployeeBenefitCost = Math.round((dataItem.EmployeeBenefitCost / 26) * 100) / 100;
        dataItem.PaycheckDependentBenefitCost = Math.round((dataItem.DependentsBenefitCost / 26) * 100) / 100;
        dataItem.PaycheckDiscount = Math.round((dataItem.Discount / 26) * 100) / 100;
        dataItem.PaycheckTotalBenefitCost = Math.round((dataItem.TotalBenefitCost / 26) * 100) / 100;
        wnd.content(detailsTemplate(dataItem));
        wnd.title("Employee: " + dataItem.FirstName + " " + dataItem.LastName);
        wnd.center().open();


        $(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });


        $(".benefitsTabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });


        $(".dependents").kendoGrid({
            dataSource: {
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: "http://127.0.0.1:8080/GetAllDependents",
                            dataType: "json",
                            type: "GET",
                            xhrFields: {
                                withCredentials: true
                            },
                            success: function (result) {
                                options.success(result);
                            }
                        })
                    },
                    create: function (options) {
                        $.ajax({
                            url: "http://127.0.0.1:8080/InsertDependent?employeeId=" + dataItem.EmployeeId + "&lastName=" + options.data.LastName + "&firstName=" + options.data.FirstName,
                            dataType: "json",
                            type: "POST",
                            xhrFields: {
                                withCredentials: true
                            },
                            success: function (result) {
                                options.success(result);
                                var grid = $("#app").getKendoGrid();
                                //capturing selected grid item in order to reopen window with correct row later
                                selectedItem = grid.dataItem(grid.select()).EmployeeId;
                                $('#app').data('kendoGrid').dataSource.read();  
                                wnd.close();
                            },
                            error: function (result) {
                                options.error(result);
                            }
                        })
                    }
                },
                filter: {
                    //workaround logic to filter datasource but be able to add new record
                    logic: "or",
                    filters: [
                        { field: "EmployeeId", operator: "eq", value: dataItem.EmployeeId },
                        { field: "DependentId", operator: "eq", value: 0 }
                    ]
                },
                schema: {
                    model: {
                        id: "DependentId",
                        fields: {
                            DependentId: { type: "number", editable: false },
                            EmployeeId: { type: "number", editable: false },
                            FirstName: { type: "string", validation: { required: true } },
                            LastName: { type: "string", validation: { required: true } },
                            BenefitCost: { type: "number", editable: false, defaultValue: 500 },
                            Discount: { type: "number", editable: false }
                        }
                    }
                },
                // must be set to false to filter datasource
                serverFiltering: false,

            },
            noRecords: {
                template: "<div style='padding: 5%'><strong> No Dependents Recorded </strong></div>"
            },
            dataBound: function (e) {
                //looping through data to add class to Benefit Cost field
                var dataItems = e.sender.dataSource.view();
                var columnIndex = this.wrapper.find(".k-grid-header [data-field=" + "BenefitCost" + "]").index();
                for (var j = 0; j < dataItems.length; j++) {
                    var discount = dataItems[j].get("BenefitCost")
                    var row = e.sender.tbody.find("[data-uid='" + dataItems[j].uid + "']");
                    var cell = row.children().eq(columnIndex);
                    if (dataItems[j].Discount > 0) {
                        cell.addClass("discountTrue");
                    }
                }
            },
            scrollable: false,
            editable: "inline",
            filterable: true,
            toolbar: [{ name: "create", text: "Add New Dependent" }],
            columns: [
                { field: "FirstName", title: "First Name", width: "100px" },
                { field: "LastName", title: "Last Name", width: "150px" },
                { field: "BenefitCost", title: "Benefit Cost", format: "{0:c}", width: "100px", headerTemplate: '<span title="Name Discount applied where text is green.">Benefit Cost</span>' },

                {
                    command: { name: "edit", text: "Edit" }, width: "150px"
                }
            ]
        });

    }

    
}
