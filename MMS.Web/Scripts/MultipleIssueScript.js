var LotNo_val = "";
$(document).ready(function () {

    $(document).ajaxStart(function () {
        $(".loader-overlay").css("display", "block");
    });

    $(document).ajaxComplete(function () {
        $(".loader-overlay").css("display", "none");
    });
    $(function () {
        $('#txtPL').keyup(function () {
            filter(this);
        });
    });

    $(function () {
        $("#chkAll").change(function () {
            var chkAll = $(this).is(':checked');
            if (chkAll)
                $(".chkProduct").prop("checked", true);
            else
                $(".chkProduct").prop("checked", false);
        });
    });
    $(window).load(function () {
        $('.loader-overlay').fadeOut('fast');
    });
    $(function () {
        $('.loader-overlay').ajaxStart(function () {
            $(this).fadeIn('slow');
        }).ajaxStop(function () {
            $(this).stop().fadeOut('fast');
        });
    });
    $(".MaterialGeneral_").combobox();
    $("#divPiecesIssue").hide();
    $("#divWithoutPieces").show();
    $('#IssueSlipNo').prop('disabled', true);
    $('.General_IssueSlipNo').prop('disabled', true);
    $("#Date").datepicker({ dateFormat: "dd/mm/yy", maxDate: new Date() });
    var isSizeRangeMaster = false;

    $(function () {
        $("#MaterialType").change(function () {

            if ($(".MaterialGeneral_ option:selected").text() == "") {
                alert("Please select material name first");
                return false;
            }
            else {
                $.ajax({
                    url: '/MultipleIssue/GetCurrentStock',
                    type: "GET",
                    dataType: "JSON",
                    data: { MaterialID: $(".MaterialGeneral_").val(), MaterialType: $("#MaterialType").val() },
                    success: function (data) {
                        debugger;
                        if (data.BalanceStock != null) {
                            if (parseFloat(data.BalanceStock) > 0) {
                                $("#Add").show();
                                var count = 1;
                                $("#CurrentStock").val(data.BalanceStock).prop("disabled", true);
                                $('#sizeRangeTable tbody tr').each(function () {
                                    $(this).find("td:not(:first)").remove();
                                });
                                for (var i = 1; i <= data.SizeRangelist.length - 1; i++) {
                                    isSizeRangeMaster = true;
                                    $('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + data.SizeRangelist[i].SizeRange.trim() + ' </td> ');
                                    $('#sizeRangeTable tbody tr:last').append("<td class='' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty' onchange='CalculateTotal(this)' id='Qty" + i + "' value='0' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                                    count++;
                                }
                            }
                            else {
                                $("#CurrentStock").val(data.BalanceStock).prop("disabled", true);
                                $('#sizeRangeTable tbody tr').each(function () {
                                    $(this).find("td:not(:first)").remove();
                                });
                                for (var i = 1; i <= data.SizeRangelist.length - 1; i++) {
                                    isSizeRangeMaster = true;
                                    $('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + data.SizeRangelist[i].SizeRange.trim() + ' </td> ');
                                    $('#sizeRangeTable tbody tr:last').append("<td class='' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty' onchange='CalculateTotal(this)' id='Qty" + i + "' value='0' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                                    count++;
                                }
                                if ($("#CurrentStock").val() != "" && (parseFloat($("#CurrentStock").val()) < 0 || parseFloat($("#CurrentStock").val()) == 0)) {
                                    $("#Add").hide();
                                    alert("Stock is not avaible Please contact store department");
                                    return false;
                                }
                            }
                        }
                    }
                });
            }
        });
    })
    $(function () {
        $(".MaterialGeneral_").combobox({
            select: function (event, ui) {
                debugger;
                var internalOrderNo = "";
                if ($("#IssueType option:selected").text() == "Direct Issue" || $("#IssueType option:selected").text() == "Other Issue") {
                    var MaterialId = $('.MaterialGeneral_').val();
                    var issueNo = $("#IssueSlipNo").val();
                    if (issueNo == "") {
                        issueNo = 0;
                    }
                    if ($(".InternalValue_general option:selected").text() != "")
                    {
                        internalOrderNo=  $(".InternalValue_general option:selected").text();
                    }
                    if ($("#IssueDate").val() == "") {
                        alert("Please select issue date");
                        return false;
                    }
                    else {
                        var issueDate = $("#IssueDate").val();
                        $.ajax({
                            url: '/MultipleIssue/MultipleIssueMaterialName',
                            type: "GET",
                            dataType: "JSON",
                            data: { MaterialNameID: MaterialId, IssueSlipNo: issueNo, InternalOrderNo: internalOrderNo, IssueDate: issueDate },
                            success: function (data) {
                                debugger;
                                if (data.Message == "Already existed") {
                                    alert("This Material name already existed this issue slip no");
                                    return false;
                                }
                                else if (data.Message == "Please make a entry on approved price list") {
                                    alert("Please make a entry on approved price list");
                                    return false;
                                }
                                else {
                                    if (data.BalanceStock != null) {
                                        var stock = parseFloat(data.BalanceStock);
                                  
                                        if (parseFloat(data.BalanceStock) > 0) {
                                            var count = 1;
                                            $("#CurrentStock").val(data.BalanceStock).prop("disabled", true);
                                            $('#sizeRangeTable tbody tr').each(function () {
                                                $(this).find("td:not(:first)").remove();
                                            });
                                            for (var i = 0; i <= data.SizeRangelist.length - 1; i++) {
                                                isSizeRangeMaster = true;
                                                $('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + data.SizeRangelist[i].IssueSize1 + ' </td> ');
                                                $('#sizeRangeTable tbody tr:nth-child(2)').append("<td class='stockInHand' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' readonly class='currentstock' id='currentstock" + i + "' value=" + data.SizeRangelist[i].StockInHand + " style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                                                $('#sizeRangeTable tbody tr:last').append("<td class='Qty' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty' onchange='CalculateTotal(this," + data.SizeRangelist[i].StockInHand + ")' id='Qty" + i + "' value='0' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                                                count++;
                                            }
                                            $.each(data.Material, function (i, city) {

                                                $(".storeGeneral_direct").val(city.StoreMasterId);
                                                $(".categoryGeneral_").val(city.MaterialCategoryMasterId);
                                                $(".groupGeneral_").val(city.MaterialGroupMasterId);
                                                $(".color_Genral").val(city.ColorMasterId);
                                                $(".requiredType_general").val(city.Uom);
                                                $(".AlreadyUsedType_general").val(city.Uom);
                                                $(".CurrentIssuesType_general").val(city.Uom);
                                                $(".substance_color").val(city.SubstanceMasterId);
                                                $(".rate_general").val(city.Price);
                                                if (city.isLocal == true && city.isImport == true) {
                                                    var int = 1;
                                                    $("#MaterialType").html("");
                                                    $("#MaterialType").append($('<option></option>').val(0).html("Please select"));
                                                    for (i = 1; i <= 2; i++) {
                                                        var type = "";
                                                        if (i == 1) {
                                                            type = "Local";
                                                        }
                                                        else if (i == 2) {
                                                            type = "Direct Import";
                                                            i = 3;
                                                        }
                                                        $("#MaterialType").append(
                                                            $('<option></option>').val(i).html(type));
                                                    }
                                                }
                                                else if (city.isImportCustomer == true && city.isImport == true) {
                                                    var int = 1;
                                                    $("#MaterialType").html("");
                                                    $("#MaterialType").append($('<option></option>').val(0).html("Please select"));
                                                    for (i = 1; i <= 2; i++) {
                                                        var type = "";
                                                        if (i == 1) {
                                                            type = "Customer Import";
                                                            i = 2;
                                                        }
                                                        else if (i == 2) {
                                                            type = "Direct Import";
                                                            i = 3;
                                                        }
                                                        $("#MaterialType").append(
                                                            $('<option></option>').val(i).html(type));
                                                    }
                                                }
                                                else if (city.isImportCustomer == true && city.isLocal == true) {
                                                    var int = 1;
                                                    $("#MaterialType").html("");
                                                    $("#MaterialType").append($('<option></option>').val(0).html("Please select"));
                                                    for (i = 1; i <= 2; i++) {
                                                        var type = "";
                                                        if (i == 1) {
                                                            type = "Local";
                                                            i = 1;
                                                        }
                                                        else if (i == 2) {
                                                            type = "Customer Import";
                                                            i = 2;
                                                        }
                                                        $("#MaterialType").append(
                                                            $('<option></option>').val(i).html(type));
                                                    }
                                                }
                                                else if (city.isImportCustomer == true) {
                                                    $("#MaterialType").val(2);
                                                }
                                                else if (city.isLocal == true) {
                                                    $("#MaterialType").val(1);
                                                }
                                                else if (city.isImport == true) {
                                                    $("#MaterialType").val(3);
                                                }
                                                if (data.store.StoreName == "Leather Store" || data.store.StoreName == "Leather Store-Local" || data.store.StoreName == "Leather Stores-Import") {

                                                    $("#divWithoutPieces").hide();
                                                    $("#divPiecesIssue").show();
                                                    $(".RequiredType_cls").val(city.Uom);
                                                    $(".AlreadyUsedType_cls").val(city.Uom);
                                                    $(".CurrentIssuesType_cls").val(city.Uom);
                                                    $(".PiecesRequiredQtyType_cls").val(city.UomUnit);
                                                    $(".PiecesAlreadyIssueType_cls").val(city.UomUnit);
                                                    $(".PiecesCurrentType_cls").val(city.UomUnit);
                                                    $(".substance_color").val(city.SubstanceMasterId);
                                                }
                                                else {
                                                    $("#divWithoutPieces").show();
                                                    $("#divPiecesIssue").hide();
                                                    $("#RequiredType").val(city.Uom);
                                                    $("#AlreadyUsedType").val(city.Uom);
                                                    $("#CurrentIssuesType").val(city.Uom);
                                                    $("#PiecesRequiredQtyType").val(city.UomUnit);
                                                    $("#PiecesAlreadyIssueType").val(city.UomUnit);
                                                    $("#PiecesCurrentType").val(city.UomUnit);
                                                    $("#SubtanceID").val(city.SubstanceMasterId);
                                                }
                                            });
                                            $.each(data.approvedPriceList, function (i, PRICE) {
                                                $(".rate_general").val(PRICE.MRPPrice);
                                            });
                                        }
                                        else {
                                            $("#CurrentStock").val("0").prop("disabled", true);
                                            alert("Stock is not avaible Please contact store department");
                                            return false;
                                        }
                                    }
                                    else {
                                        $('#sizeRangeTable tbody tr').each(function () {
                                            $(this).find("td:not(:first)").remove();
                                        });
                                    }

                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $("#InternalOderID_").click(function () {
        orderid = $("#InternalOderID_").val();
        $.ajax({
            url: '/MultipleIssue/GetInternalOrderID',
            type: "GET",
            dataType: "JSON",
            data: { InternalOrderno: orderid },
            success: function (data) {
                $("#Style").val(internalOrderEntryForm.BomNo);

            }
        });
    });
    var opt = $('#my-dialog').dialog({
        autoOpen: false,
        autoResize: true,
        width: 666,
        height: 369,
        position: { my: 'top', at: 'top+50', of: window, position: 'fixed' },
        resizable: false,
        draggable: false,
        title: "",
        model: false,
        show: "show",
        hide: "hide",
        closeOnEscape: true,
        close: function () {
            $(this).dialog('close');
        }

    });
    $("#toggle").click(function () {
        $("#MaterialDescription").toggle();
    });
    $(".issueTypecls").change(function () {
        var types = "";
        var id = $("#IssueType").val();
        types = $("#IssueType option:selected").text().trim();
        if (types == "Order") {
            $("#IssueType").val(id);
            $("#General").hide();
            $("#Order").show();
        }

        else {
            if (types == "Other Issue") {
                $('#InternalOrderingFor option').filter('[value="1"],[value="2"],[value="3"],[value="4"],[value="5"]').remove();
            }
            $(".issueTypecls").val(id);
            $("#General").show();
            $("#Order").hide();
        }
    });
    $("#BtnSave").click(function () {
        debugger;
        if ($('#txtSIssueSlipNo').val() == "") {
            alert("Please Enter Issue Slip No."); $('#txtSIssueSlipNo').css('border-color', 'red'); $('#txtSIssueSlipNo').focus(); return false;
        }
        else {
            $('#txtSIssueSlipNo').css('border-color', '');
        }
        if ($('#ddlIssueType').val() == "") {
            alert("Please Enter Issue Type."); $('#ddlIssueType').css('border-color', 'red'); $('#ddlIssueType').focus(); return false;
        }
        else {
            $('#ddlIssueType').css('border-color', '');
        }
        if ($('#txtDate').val() == "") {
            alert("Please Enter Date."); $('#txtDate').css('border-color', 'red'); $('#txtDate').focus(); return false;
        }
        else {
            $('#txtDate').css('border-color', '');
        }
        if ($('#ddlConveyor').val() == "") {
            alert("Please Enter Conveyor."); $('#ddlConveyor').css('border-color', 'red'); $('#ddlConveyor').focus(); return false;
        }
        else {
            $('#ddlConveyor').css('border-color', '');
        }
        if ($('#InternalOrderId').val() == "") {
            alert("Please Enter Internal Order."); $('#InternalOrderId').css('border-color', 'red'); $('#InternalOrderId').focus(); return false;
        }
        else {
            $('#InternalOrderId').css('border-color', '');
        }

        if ($('#MachineNameId').val() == "") {
            alert("Please Enter Machine Name."); $('#MachineNameId').css('border-color', 'red'); $('#MachineNameId').focus(); return false;
        }
        else {
            $('#MachineNameId').css('border-color', '');
        }
        if ($('#txtStyleNo').val() == "") {
            alert("Please Enter Style No."); $('#txtStyleNo').css('border-color', 'red'); $('#txtStyleNo').focus(); return false;
        }
        else {
            $('#txtStyleNo').css('border-color', '');
        }
        if ($('#txtNoOfsetsPairs').val() == "") {
            alert("Please Enter No Of sets Pairs."); $('#txtNoOfsetsPairs').css('border-color', 'red'); $('#txtNoOfsetsPairs').focus(); return false;
        }
        else {
            $('#txtNoOfsetsPairs').css('border-color', '');
        }
        if ($('#StoresId').val() == "") {
            alert("Please Enter Stores."); $('#StoresId').css('border-color', 'red'); $('#StoresId').focus(); return false;
        }
        else {
            $('#StoresId').css('border-color', '');
        }
        if ($('#RateId').val() == "") {
            alert("Please Enter Rate."); $('#RateId').css('border-color', 'red'); $('#RateId').focus(); return false;
        }
        else {
            $('#RateId').css('border-color', '');
        }
        if ($('#CategoryId').val() == "") {
            alert("Please Enter Category."); $('#CategoryId').css('border-color', 'red'); $('#CategoryId').focus(); return false;
        }
        else {
            $('#CategoryId').css('border-color', '');
        }
        if ($('#SubstanceId').val() == "") {
            alert("Please Enter Substance."); $('#SubstanceId').css('border-color', 'red'); $('#SubstanceId').focus(); return false;
        }
        else {
            $('#SubstanceId').css('border-color', '');
        }
        if ($('#GroupId').val() == "") {
            alert("Please Enter Group."); $('#GroupId').css('border-color', 'red'); $('#GroupId').focus(); return false;
        }
        else {
            $('#GroupId').css('border-color', '');
        }
        if ($('#txtReqQty').val() == "") {
            alert("Please Enter Required Qty."); $('#txtReqQty').css('border-color', 'red'); $('#txtReqQty').focus(); return false;
        }
        else {
            $('#txtReqQty').css('border-color', '');
        }
        if ($('#MaterialId').val() == "") {
            alert("Please Enter Material."); $('#MaterialId').css('border-color', 'red'); $('#MaterialId').focus(); return false;
        }
        else {
            $('#MaterialId').css('border-color', '');
        }
        if ($('#ColourId').val() == "") {
            alert("Please Enter Colour."); $('#ColourId').css('border-color', 'red'); $('#ColourId').focus(); return false;
        }
        else {
            $('#ColourId').css('border-color', '');
        }
        if ($('#txtAlreadyIssued').val() == "") {
            alert("Please Enter Already Issued."); $('#txtAlreadyIssued').css('border-color', 'red'); $('#txtAlreadyIssued').focus(); return false;
        }
        else {
            $('#txtAlreadyIssued').css('border-color', '');
        }

        if ($('#txtCurrentIssues').val() == "") {
            alert("Please Enter Current Issues."); $('#txtCurrentIssues').css('border-color', 'red'); $('#txtCurrentIssues').focus(); return false;
        }
        else {
            $('#txtCurrentIssues').css('border-color', '');
        }
        if ($('#ddlCurrentSharesVal').val() == "") {
            alert("Please Enter Current Shares Value ."); $('#ddlCurrentSharesVal').css('border-color', 'red'); $('#ddlCurrentSharesVal').focus(); return false;
        }
        else {
            $('#ddlCurrentSharesVal').css('border-color', '');
        }
        if ($('#txtBalanceILOTSQFT').val() == "") {
            alert("Please Enter Balance LOT SQFT."); $('#txtBalanceILOTSQFT').css('border-color', 'red'); $('#txtBalanceILOTSQFT').focus(); return false;
        }
        else {
            $('#txtBalanceILOTSQFT').css('border-color', '');
        }
        if ($('#txtBalanceILOTSPCS').val() == "") {
            alert("Please Enter Balance LOTS PCS."); $('#txtBalanceILOTSPCS').css('border-color', 'red'); $('#txtBalanceILOTSPCS').focus(); return false;
        }
        else {
            $('#txtBalanceILOTSPCS').css('border-color', '');
        }
        if ($('#txtCurrentStock').val() == "") {
            alert("Please Enter Current Stock."); $('#txtCurrentStock').css('border-color', 'red'); $('#txtCurrentStock').focus(); return false;
        }
        else {
            $('#txtCurrentStock').css('border-color', '');
        }
        if ($('#txtCurrentStockUOM').val() == "") {
            alert("Please Enter Current Stock UOM."); $('#txtCurrentStockUOM').css('border-color', 'red'); $('#txtCurrentStockUOM').focus(); return false;
        }
        else {
            $('#txtCurrentStockUOM').css('border-color', '');
        }

        $.ajax({
            url: '/IssueSlip_Single/SaveDetails',
            type: "POST",
            dataType: "JSON",
            data: {
                IssuesSlipId: $("#IssuesSlipId").val(),
                IssuesTypeId: $("#ddlIssueType").val(),
                IssuesSlipNo: $("#IssueSlipNo").val(),
                Date: $("#txtDate").val(),
                ConveyorId: $('#ConveyorID').val(),
                Season: $('#Season').val(),
                BuyerMasterId: $('#BuyerMasterId').val(),
                BomStyle: $('#BomStyle').val(),
                InternalOderID: $('#InternalOderID').val(),
                MachineNameId: $('#MachineName').val(),
                StyleNo: $("#StyleNo").val(),
                NoOfSets_Pairs: $("#txtNoOfsetsPairs").val(),
                StoresID: $("#StoresID").val(),
                Rate: $("#Rate").val(),
                CategoryID: $("#CategoryID").val(),
                SubstanceId: $('#SubtanceID').val(),
                GroupID: $("#GroupID").val(),
                RequiredQTY: $("#RequiredQTY").val(),
                RequiredType: $("#RequiredType").val(),
                AlreadyUsed: $("#AlreadyUsed").val(),
                AlreadyUsedType: $("#AlreadyUsedType").val(),
                Material: $("#Material").val(),
                ColorID: $("#ColorID").val(),
                AlreadyIssued: $("#txtAlreadyIssued").val(),
                LotNo: $("#LotNo").val(),
                CurrentIssue: $("#CurrentIssue").val(),
                CurrentIssuesType: $("#CurrentIssuesType").val(),
                BalanceInThisListlot: $("#BalanceInThisListlot").val(),
                BalanceInthisListType: $("#BalanceInthisListType").val(),
                NoOfSetPairs: $("#NoOfSetPairs").val(),
                CurrentStock: $("#CurrentStock").val(),
                CurrentStockType: $("#CurrentStockType").val(),
                FreeStock: $("#FreeStock").val(),
                FreeStockType: $("#FreeStockType").val(),
                ReserverQty: $("#ReserverQty").val(),
                ReserverQtyType: $("#ReserverQtyType").val(),
                ClosingStockinAllStores: $("#ClosingStockinAllStores").val(),
                ClosingStockinAllStoredType: $("#ClosingStockinAllStoredType").val(),
                InTransitValue: $("#InTransitValue").val(),
                InTransitType: $("#InTransitType").val(),
                IssueDate: $('#IssueDate').val(),
                TotalQty: $('#TotalQty').val()
            },
            success: function (data) {

            }
        });
    });

    $(".CategoryName").change(function () {
        var CategoryID = $("#CategoryName").val();
        var StoresID = $("#StoreName").val();
        $.ajax({
            url: '/IssueSlip_Single/GetCategoryIDWithMaterialName',
            type: "GET",
            dataType: "JSON",
            data: {
                CategoryID: CategoryID,
                StoresID: StoresID
            },
            success: function (data) {
                $("#MaterialDescription").html("");
                $.each(data, function (i, city) {
                    $("#MaterialDescription").append(
                        $('<option></option>').val(city.Value).html(city.Text));

                });
            }
        });
    }
     );
    $(function () {
        $("#LotNo").change(function () {
            var number = $("#LotNo option:selected").text() + ",";
            LotNo_val += number;

        });
    });
    $("#IssueType").change(function () {

        if ($('#IssueType option:selected').text().trim() == "general") {
            $.ajax({
                url: '/MultipleIssue/GetIssueSlipNo',
                type: "GET",
                dataType: "JSON",
                success: function (data) {

                    var result = data;
                    $('.General_IssueSlipNo').val(result);
                    $('.General_IssueSlipNo').prop('disabled', true);
                }
            });
        }
        else if ($('#IssueType option:selected').text().trim() == "Direct Issue") {

            $("#internal_Order").hide();
            $("#internal_DirectIssue").show();
            $("#InternalValue").val("");
            $("#Direct_Style").hide();
            $("#internal_DirectIssue_Style").show();
            $("#DirectIssue_Style").val("");

            $.ajax({
                url: '/MultipleIssue/GetIssueSlipNo',
                type: "GET",
                dataType: "JSON",
                success: function (data) {

                    var result = data;
                    $('.General_IssueSlipNo').val(result);
                    $('.General_IssueSlipNo').prop('disabled', true);
                }
            });
        }
    });
    $(".multipleGeneral_").change(function () {
        var IO = $(".multipleGeneral_").val();
        $.ajax({
            url: '/MultipleIssue/GetStyle',
            type: "GET",
            dataType: "JSON",
            data: { orderEntry: IO },
            success: function (data) {
                console.log(data);
                $(".styleGeneral_").html(""); // clear before appending new list
                $('.styleGeneral_').append('<option value="0" selected="selected">Please Select</option>');
                $.each(data, function (i, city) {
                    $(".styleGeneral_").append(
                        $('<option></option>').val(city.Value).html(city.Text));
                });
            }
        });

    });
    $(".styleGeneral_").change(function () {
        var Style = $(".styleGeneral_").val();
        $.ajax({
            url: '/MultipleIssue/GetStore',
            type: "GET",
            dataType: "JSON",
            data: { style: Style },
            success: function (data) {
                console.log(data);
                $(".storeGeneral_").html(""); // clear before appending new list
                $('.storeGeneral_').append('<option value="0" >Please Select</option>');
                $.each(data, function (i, city) {
                    $(".storeGeneral_").append(
                        $('<option></option>').val(city.Value).html(city.Text));
                });
            }
        });

    });
    $("#BuyerMasterId").change(function () {
        var Buyer = $('#BuyerMasterId').val();
        $('.loader-overlay').show();
        $.ajax({
            url: '/Damage/FillSeason',
            dataType: "JSON",
            data: { Buyer: Buyer },
            success: function (cities) {
                if (cities != null && cities != "") {
                    $("#Season").html(""); // clear before appending new list
                    $('#Season').append('<option value="" selected="selected">Please Select</option>');
                    $.each(cities, function (i, city) {
                        $("#Season").append(
                            $('<option></option>').val(city.order).html(city.season));
                    });
                }
                $('.loader-overlay').hide();
            }
        });
    });
    $(function () {
        $(".seasoncls").change(function () {
            var Season = $('#Season option:selected').text();


            $('.loader-overlay').show();
            $.ajax({
                url: '/MultipleIssue/FillLotNo',
                dataType: "JSON",
                data: { Season: Season },
                success: function (cities) {
                    var result = cities;
                    if (result.length > 0) {
                        $('.LotNo_general').html("");
                        $('.LotNo_general').append('<option value="0">Please Select</option>');
                        $.each(cities, function (i, city) {
                            $(".LotNo_general").append(
                                $('<option></option>').val(city.Lot).html(city.Lot));
                        });
                        $(".LotNo_general").multiselect("refresh");
                    }
                    else {

                        $('.LotNo_general').html("");
                        $(".LotNo_general").multiselect("refresh");
                        alert("There is no order for this lot no");
                        return false;
                    }
                    $('.loader-overlay').hide();
                }

            });
        });
    });
    $(".storeGeneral_").change(function () {
        var store = $(".storeGeneral_").val();
        if (($(".storeGeneral_ option:selected").text().trim() == "Leather Store-Local" || $(".storeGeneral_ option:selected").text().trim() == "Leather Stores-Import" || $(".storeGeneral_ option:selected").text().trim() == "Leather Store")) {
            $("#divWithoutPieces").hide();
            $("#divPiecesIssue").show();
        }
        else {
            $("#divPiecesIssue").hide();
            $("#divWithoutPieces").show();
        }
        $.ajax({
            url: '/MultipleIssue/GetStoreCategory',
            type: "GET",
            dataType: "JSON",
            data: { store: store },
            success: function (data) {
                $(".categoryGeneral_").html(""); // clear before appending new list
                $('.categoryGeneral_').append('<option value="0" selected="selected">Please Select</option>');
                $.each(data, function (i, city) {
                    $(".categoryGeneral_").append(
                        $('<option></option>').val(city.Value).html(city.Text));
                });
            }
        });
    });
    $(".storeGeneral_direct").change(function () {
        debugger;
        var store = $(".storeGeneral_direct").val();

        if (($(".storeGeneral_direct option:selected").text().trim() == "Leather Store-Local" || $(".storeGeneral_ option:selected").text().trim() == "Leather Stores-Import" || $(".storeGeneral_ option:selected").text().trim() == "Leather Store")) {
            $("#divWithoutPieces").hide();
            $("#divPiecesIssue").show();
        }
        else {
            $("#divPiecesIssue").hide();
            $("#divWithoutPieces").show();
        }
        $.ajax({
            url: '/MultipleIssue/GetStoreCategory',
            type: "GET",
            dataType: "JSON",
            data: { store: store },
            success: function (data) {
                $(".categoryGeneral_").html(""); // clear before appending new list
                $('.categoryGeneral_').append('<option value="0" selected="selected">Please Select</option>');
                $.each(data, function (i, city) {
                    $(".categoryGeneral_").append(
                        $('<option></option>').val(city.Value).html(city.Text));
                });
            }
        });
    });
    $(".categoryGeneral_").change(function () {
        var store = $(".categoryGeneral_").val();
        $.ajax({
            url: '/MultipleIssue/GetGroup',
            type: "GET",
            dataType: "JSON",
            data: { category: store },
            success: function (data) {
                console.log(data);
                $(".groupGeneral_").html(""); // clear before appending new list
                $('.groupGeneral_').append('<option value="0" selected="selected">Please Select</option>');
                $.each(data, function (i, city) {
                    $(".groupGeneral_").append(
                        $('<option></option>').val(city.Value).html(city.Text));
                });
            }
        });

    });
    $(".groupGeneral_").change(function () {

        var Group = $(".groupGeneral_").val();
        $.ajax({
            url: '/MultipleIssue/GetMaterial',
            type: "GET",
            dataType: "JSON",
            data: { group: Group },
            success: function (data) {
                console.log(data);
                $(".MaterialGeneral_").html(""); // clear before appending new list
                $('.MaterialGeneral_').append('<option value="0" selected="selected">Please Select</option>');
                $.each(data, function (i, city) {
                    $(".MaterialGeneral_").append(
                        $('<option></option>').val(city.Value).html(city.Text));
                });
            }
        });
    });
    $('#CurrentIssue').change(function () {
        debugger;
       // alert("1");
        var requiredQTy = $('#RequiredQTY').val();
        var CurrentIssues = $('#CurrentIssue').val();
        if (parseFloat(CurrentIssues) > parseFloat(requiredQTy)) {
            alert("Please enter less than Required Qty.");
            return false;
        }
        var CurrentStock = $("#CurrentStock").val();
        if (parseFloat(CurrentIssues) > parseFloat(CurrentStock)) {
            alert("Current issue is exceeding more than current stock.");
            $('#CurrentIssue').val("");
            return false;
        }
    });
    $('.CurrentIssueGeneral_cls').change(function () {
        var requiredQTy = $('#RequiredQTY').val();
        var CurrentIssues = $('.CurrentIssueGeneral_cls').val();
        if (parseFloat(CurrentIssues) > parseFloat(requiredQTy)) {
            alert("Please enter less than Required Qty.");
            return false;
        }
        var CurrentStock = $("#CurrentStock").val();
        if (parseFloat(CurrentIssues) > parseFloat(CurrentStock)) {
            alert("Current issue is exceeding more than current stock.");
            $('.CurrentIssueGeneral_cls').val("");
            return false;
        }
    });
    var wholeJsonList = [];
    $("#allIssue").change(function () {

        $('.loader-overlay').show();
        if ($('.hasDatepicker2 ').val() == "") {
            $('.loader-overlay').hide();
            alert("Please select issue date.");
            $('#IssueDate').css('border-color', 'red');
            $('#IssueDate').focus(); return false;
        }
        else {
            $('#IssueDate').css('border-color', '');
        }
        var internalOrderArray = $('.multipleOrder_').val();
        var InternalOrderid = "";
        if (internalOrderArray != null && internalOrderArray.length > 0) {
            for (i = 0; i < internalOrderArray.length; i++) {
                if (internalOrderArray[i] != 0) {
                    InternalOrderid += internalOrderArray[i] + ",";
                }
            }
            InternalOrderid = InternalOrderid.slice(0, -1);
        }
        var lotno = $("#LotNo").val();
        $("#list-amended-material > tbody").find(".ColorError_issue").each(function () {
            $(this).removeClass('ColorError_issue');
        })
        $("#list-amended-material tbody > tr").each(function () {
            debugger;
            var stock = 0;
            var CurrentIssuestock = 0;
            stock = $(this).find('.BalanceStock').text().trim();
            CurrentIssuestock = $(this).find('.CurrentIssueValue').val().trim();

            if (((parseFloat(stock) >= parseFloat(CurrentIssuestock)) && parseFloat(stock) > 0 && parseFloat(CurrentIssuestock) > 0)) {
            }
            else if (((parseFloat(stock) < parseFloat(CurrentIssuestock)))) {
                $(this).closest('tr').addClass("ColorError_issue");
                //$(this).closest('tr').addClass('ColorError');
            }
            if (parseFloat(stock) <= 0) {
                $(this).closest('tr').addClass('ColorError_')
            }
            else if (parseFloat(CurrentIssuestock) <= 0) {

                $(this).closest('tr').addClass('ColorError_')
            }


        });
        wholeJsonList = [];
        var minusStock = 0;
        $("#list-amended-material tbody tr").each(function () {
            if ($(this).find('.CurrentIssueValue').val().trim() < 0) {
                minusStock++;
            }
        });

        if (minusStock != 0) {
            var r = confirm("Excess issue found! Continue to further");
            if (r == true) {

            } else {
                $('.loader-overlay').hide();
                return false;
            }

        }
        $("#list-amended-material tbody tr").each(function () {
            wholeJsonList.push({
                IssueSlipId: $(this).find('.IssueSlipId').text().trim(),
                IssueSlipFK: $(this).find('.IssueSlipFK').text().trim(),
                IssueSlipNo: $(this).find('.IssueSlipNo').text().trim(),
                OrderNo: $(this).find('.multipleOrder1_').text().trim(),
                LotNo: lotno.trim(),
                MaterialMasterId: $(this).find('.MaterialMasterId').text().trim(),
                StoreMasterId: $(this).find('.StoreMasterId').text().trim(),
                Style: $(this).find('.Style').text().trim(),
                IssueType: $(this).find('.NoOfSets').text().trim(),
                NoOfSets: $(this).find('.NoOfSets').text().trim(),
                GroupId: $(this).find('.GroupName').text().trim(),
                CategoryId: $(this).find('.CategoryName').text().trim(),
                StoreName: $(this).find('.StoreName').text().trim(),
                MaterialName: $(this).find('.MaterialDescription').text().trim(),
                ColourId: $(this).find('.Color').text().trim(),
                RequiredQty: $(this).find('.RequiredQty').text().trim(),
                AlredayIssued: $(this).find('.AlredayIssued').text().trim(),
                CurrentIssue: $(this).find('.CurrentIssueValue').val().trim(),
                Rate: $(this).find('.Piecies').text().trim(),
                IsChecked: $(this).find('.isMaterialckbox').is(':checked'),
                CurrentStock: $(this).find('.BalanceStock').text().trim(),
                BuyerOrderSlNo: $(this).find('.multipleOrder1_').text().trim(),
                MaterialTypes: $(this).find('.MaterialTypes').text().trim()

            });
        });

        if ($("#list-amended-material > tbody").find('.ColorError_issue').length > 0) {
            $('.loader-overlay').hide();
            alert("Please correct the current stock value");
            return false;
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/MultipleIssue/SaveAllIssue',
                data: {
                    AllIssue: JSON.stringify(wholeJsonList),
                    TotalQty: $('#TotalQty').val(),
                    IssueType: $(".issueTypecls").val(),
                    IssueDate: $(".hasDatepicker2").val(),
                    IssueSlipNo: $("#IssueSlipNo").val(),
                    InternalOderID: InternalOrderid,
                    StoreName: $("#StoreName").val(),
                    CategoryName: $("#CategoryName").val(),
                    Date: $("#Date").val(),
                    CuttingIssueTypeID: $("#CuttingIssueTypeID").val(),
                    BuyerMasterId: $("#BuyerMasterId").val(),
                    Season: $("#Season").val(),
                    LotNo: $("#LotNo").val(),
                    BomStyle: $("#BomStyle").val(),
                    InternalOderID_: $("#InternalOderID_").val(),
                    InternalOrderingFor: $("#InternalOrderingFor").val(),
                    OrderQty: $("#OrderQty").val(),
                    GroupName: $("#GroupName").val(),
                    ConveyorID: $("#ConveyorID").val()
                },
                success: function (data) {
                    $('.loader-overlay').hide();
                    if (data == "UpdatedSucessfully") {
                        alert('Updated Successfully.');
                        location.href = "/MultipleIssue/MuplitpleIssue";
                        return false;
                    }
                    else if (data == "Saved Sucessfully") {
                        alert('Saved Successfully.');
                        location.href = "/MultipleIssue/MuplitpleIssue";
                        return false;
                    }
                    else {
                        alert('Something went wrong.');
                        return false;
                    }
                }
            });
        }


    });
    $("#Add").click(function () {
        //validation();
        debugger;
        if (validation() != false) {
            var qtyCount = 0;
            var qtyArr = [];
            var size = "";
            $('#sizeRangeTable tbody tr').find('.SizeRangeRefValue').each(function () {

                size += $(this).text() + ",";

            });
            size = size.slice(0, -1);
            var Qty_ = "";
            $('#sizeRangeTable tbody tr').find('.Qty').each(function () {
                Qty_ += $(this).val() + ",";

            });
            Qty_ = Qty_.slice(0, -1);

            var add = 0;
            $("#sizeRangeTable .Qty").each(function () {
                add += Number($(this).val());
            });
            var CurrentIssue_ = 0;
            var orderno = "";
            var issueFor = "";
            var custmername = "";
            var PiecesRequiredQTY_ = "";
            var PiecesRequiredQtyType_ = "";
            var PiecesAlreadyIssueType_ = "";
            var PiecesCurrentIssue_ = "";
            var PiecesCurrentType_ = "";
            var PiecesAlreadyIssue_ = "";
            var PiecesRequiredQTY_ = "";
            var PiecesRequiredQTY_ = "";
            var RequiredType_ = "";
            var AlreadyUsedType_ = "";
            var CurrentIssuesType_ = "";
            var RequiredQty_ = "";
            var MaterialTypes_ = "";
            var BuyerOrderSlNo_ = "";
            var store = "";

            if (($("#IssueType option:selected").text() == "Direct Issue" || $("#IssueType option:selected").text() == "Other Issue" || ($("#StoreName option:selected").text() == "Leather Store" || $("#StoreName option:selected").text() == "Leather Store-Local" || $("#StoreName option:selected").text() == "Leather Stores-Import"))) {
                debugger;
                CurrentIssue_ = 0;
                CurrentIssue_ = $(".CurrentIssueGeneral_cls").val();
                MaterialTypes_ = $(".MaterialTypes").val();
                orderno = $(".InternalValue_general").val();
                issueFor = $(".InternalOrderingFor_cls").val();
                custmername = $(".BuyerMasterId_cls").val();
                PiecesRequiredQTY_ = $('.PiecesRequiredQTY_cls').val();
                PiecesRequiredQtyType_ = $('.PiecesRequiredQtyType_cls').val();
                PiecesAlreadyIssueType_ = $('.PiecesAlreadyIssueType_cls').val();
                PiecesCurrentIssue_ = $('.PiecesCurrentIssue_cls').val();
                PiecesCurrentType_ = $('.PiecesCurrentType_cls').val();
                PiecesAlreadyIssue_ = $('.PiecesAlreadyIssue_cls').val();
                RequiredType_ = $('.RequiredType_cls').val();
                AlreadyUsedType_ = $('.AlredayIssued_cls').val();
                CurrentIssuesType_ = $('.CurrentIssuesType_cls').val();
                RequiredQty_ = $('.RequiredQty_Cls').val();
                AlredayIssued_ = $('.AlredayIssued_cls').val();
                BuyerOrderSlNo_ = $(".multipleOrder1_").val();
                store = $('.storeGeneral_direct').val();
                if (CurrentIssue_ == 0 || CurrentIssue_ == null || CurrentIssue_ == "") {
                    CurrentIssue_ = $("#CurrentIssue").val();
                }
                if (RequiredQty_ == 0 || RequiredQty_ == null || RequiredQty_ == "") {
                    RequiredQty_ = $("#RequiredQty").val();
                }
                if (AlredayIssued_ == 0 || AlredayIssued_ == null || AlredayIssued_ == "") {
                    AlredayIssued_ = $("#AlredayIssued").val();
                }

            }
            else {
                CurrentIssue_ = 0;
                CurrentIssue_ = $("#CurrentIssue").val();
                orderno = $(".InternalValue_general").val();
                issueFor = $("#InternalOrderingFor").val();
                custmername = $("#BuyerMasterId").val();
                PiecesRequiredQTY_ = $('#PiecesRequiredQTY').val();
                PiecesRequiredQtyType_ = $('#PiecesRequiredQtyType').val();
                PiecesAlreadyIssueType_ = $('#PiecesAlreadyIssueType').val();
                PiecesCurrentIssue_ = $('#PiecesCurrentIssue').val();
                PiecesCurrentType_ = $('#PiecesCurrentType').val();
                PiecesAlreadyIssue_ = $('#PiecesAlreadyIssue').val();
                RequiredType_ = $('#RequiredType').val();
                AlreadyUsedType_ = $('#AlreadyUsedType').val();
                CurrentIssuesType_ = $('#CurrentIssuesType').val();
                RequiredQty_ = $('#RequiredQty').val();
                AlredayIssued_ = $('#AlredayIssued').val();
                MaterialTypes_ = $("#MaterialTypes").val();
                store = $('.storeGeneral_').val()
            }

            $('#TotalQty').val(add);
            $('#TotalQty').prop("disabled", true);
            if ($('#sizeRangeTable tbody tr').find('.SizeRangeRefValue').length != 0) {
                if (Number(CurrentIssue_) != add) {
                    alert("Current issues and SizeRanges Qty are differed");
                    return false;
                }
                else {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        url: '/MultipleIssue/SaveDirectIssue',
                        data: {
                            IssueSlipID: $('#IssueSlipID').val(),
                            IssueType: $('.issueTypecls').val(),
                            IssueSlipNo: $('.General_IssueSlipNo').val(),
                            InternalValue: $('.InternalValue_general option:selected').text(),
                            InternalOrderingFor: issueFor,
                            BuyerMasterId: custmername,
                            DirectIssue_Style: $('.DirectIssue_Style_general').val(),
                            StoreName: store,
                            CategoryName: $('.categoryGeneral_').val(),
                            GroupName: $('.groupGeneral_').val(),
                            MaterialDescription: $('.MaterialGeneral_').val(),
                            Piecies: $('.Piecies_genral').val(),
                            PieciesType: $('.PieciesType_general').val(),
                            LotNo: $('.LotNo_general').val(),
                            BalanceInThisListlot: $('.BalanceInThisListlot_general').val(),
                            BalanceInthisListType: $('.BalanceInthisListType_general').val(),
                            MachineName: $('.MachineName_general').val(),
                            NoOfSets: $('.NoOfSets_general').val(),
                            Color: $('.color_Genral').val(),
                            Rate: $('.rate_general').val(),
                            SubtanceID: $('.substance_color').val(),
                            Color: $('.color_Genral').val(),
                            RequiredQty: RequiredQty_,
                            AlredayIssued: AlredayIssued_,
                            CurrentIssue: CurrentIssue_,
                            RequiredType: RequiredType_,
                            AlreadyUsedType: AlreadyUsedType_,
                            CurrentIssuesType: CurrentIssuesType_,
                            ConveyorID: $('.conveyorid_general').val(),
                            PiecesRequiredQTY: PiecesRequiredQTY_,
                            PiecesRequiredQtyType: PiecesRequiredQtyType_,
                            PiecesAlreadyIssueType: PiecesAlreadyIssueType_,
                            PiecesCurrentIssue: PiecesCurrentIssue_,
                            PiecesCurrentType: PiecesCurrentType_,
                            PiecesAlreadyIssue: PiecesAlreadyIssue_,
                            MaterialTypes: MaterialTypes_,
                            ConveyorID: $('.conveyorid_general').val(),
                            IssueSlipID: $('#IssueSlipID').val(),
                            IssueSlipFK: $('#IssueSlipFK').val(),
                            IssueDate: $('#IssueDate').val(),
                            TotalQty: $('#TotalQty').val(),
                            Size: size,
                            Qty: Qty_,
                            MaterialType: $("#MaterialType").val(),
                            Season: $('.seasoncls_direct option:selected').val()
                        },
                        success: function (data) {
                            if (data.issue != null) {
                                alert("Saved Successfully");
                                rowCount = $('#list-amended-material_ tr').length;
                                if (data.issue.RequiredQty == null) {
                                    data.issue.RequiredQty = 0;
                                }
                                if (data.issue.AlredayIssued == null) {
                                    data.issue.AlredayIssued = 0;
                                }
                                $('#sizeRangeTable tbody tr').each(function () {
                                    $(this).find("td:not(:first)").remove();
                                });

                                $("#IssueSlipFK").val(data.issue.IssueSlipFK);
                                $("#list-amended-material_ tbody").html("");
                                var row = "";
                                $.each(data.issueSlipMaterialList, function (i, item) {
                                    $("#RemoveHead").css("display", "block");
                                    $("#EditHead").css("display", "block");
                                    if (item.AlredayIssued == null) {
                                        item.AlredayIssued = 0;
                                    }
                                    if (item.RequiredQty == null) {
                                        item.RequiredQty = 0;
                                    }
                                    if (item.CurrentIssue == null) {
                                        item.CurrentIssue = 0;
                                    }
                                    row += "<tr id=" + item.IssueSlipId + "><td><input type='button' value='Remove' onclick='Delete(" + item.IssueSlipId + ")' class='' />" + "</td>"
                                    row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IssueSlipId + ")' class='' />" + "</td>"
                                    row += "<td  style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + data.grnTypeMaster.IssueType + "</td>"
                                    row += "<td id='IssueSlipNo" + rowCount + "' class='General_IssueSlipNo'>" + data.Parent.IssueSlipNo + "</td>"
                                    row += "<td id='InternalValue" + rowCount + "' class='InternalValue_general'>" + item.Style + "</td>"
                                    row += "<td id='DirectIssue_Style" + rowCount + "' class='DirectIssue_Style_general'>" + item.DirectIssue_Style + "</td>"
                                    row += "<td id='IssueType" + rowCount + "' class='issueTypecls'>" + data.grnTypeMaster.IssueType + "</td>"
                                    row += "<td id='NoOfSets" + rowCount + "' class='NoOfSets_general'>" + item.NoOfSets + "</td>"
                                    row += "<td id='GroupName" + rowCount + "' class='groupGeneral_'>" + item.GroupId + "</td>"
                                    row += "<td id='CategoryName" + rowCount + "'  class='categoryGeneral_'>" + item.CategoryId + "</td>"
                                    row += "<td id='StoreName" + rowCount + "' class='storeGeneral_'>" + item.StoreName + "</td>"
                                    row += "<td id='MaterialDescription" + rowCount + "' class='MaterialGeneral_'>" + item.MaterialName + "</td>"
                                    row += "<td id='Color" + rowCount + "' class='color_Genral'>" + item.ColourId + "</td>"
                                    row += "<td id='RequiredQty" + rowCount + "' class='RequiredQty'>" + item.RequiredQty + "</td>"
                                    row += "<td id='AlredayIssued" + rowCount + "' class='AlredayIssued'>" + item.AlredayIssued + "</td>"
                                    row += "<td id='CurrentIssue" + rowCount + "' class='CurrentIssue'>" + item.CurrentIssue + "</td>"
                                    row += "<td style='' id='Rate" + rowCount + "' class='rate_general'>" + item.Rate + "</td>"
                                    row += "<td style='' id='Season" + rowCount + "' class='seasoncls_direct'>" + item.Season + "</td>"
                                    row += "<td style='display:none' id='TotalQty" + rowCount + "' class='TotalQty_genral'>" + item.TotalQty + "</td>"
                                    row += "<td style='display:none' id='MaterialType" + rowCount + "' class='MaterialType_genral'>" + item.MaterialType + "</td>"
                                    row += "<td style='display:none' id='Piecies" + rowCount + "' class='Piecies_genral'>" + item.Piecies + "</td>"
                                    row += "<td style='display:none' id='PieciesType" + rowCount + "' class='PieciesType_general'>" + item.PieciesType + "</td>"
                                    row += "<td style='display:none' id='LotNo" + rowCount + "' class='LotNo_general'>" + item.LotNo + "</td>"
                                    row += "<td style='display:none' id='BalanceInThisListlot" + rowCount + "' class='BalanceInThisListlot_general'>" + item.BalanceInThisListlot + "</td>"
                                    row += "<td style='display:none' id='BalanceInthisListType" + rowCount + "' class='BalanceInthisListType_general'>" + item.BalanceInthisListType + "</td>"
                                    row += "<td style='display:none' id='MachineName" + rowCount + "' class='MachineName_general'>" + item.MachineName + "</td>"
                                    row += "<td style='display:none' id='SubtanceID" + rowCount + "' class='substance_color'>" + item.SubtanceID + "</td>"
                                    row += "<td style='display:none' id='RequiredType" + rowCount + "' class='RequiredType'>" + item.ShipmentMode + "</td>"
                                    row += "<td style='display:none' id='AlreadyUsedType" + rowCount + "' class='AlreadyUsedType'>" + item.FreightAmount + "</td>"
                                    row += "<td style='display:none' id='StoreName" + rowCount + "' class='storeGeneral_'>" + item.StoreMasterId + "</td>"
                                    row += "<td style='display:none' id='CurrentIssuesType" + rowCount + "' class='CurrentIssuesType'>" + item.CurrentIssuesType + "</td>"
                                    row += "<td style='display:none' id='IssueDate" + rowCount + "' class='IssueDate'>" + item.IssueDate + "</td>"
                                    row += "<td style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + item.IssueType + "</td>"
                                    row += "<td style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + item.Season + "</td>"
                                    row += "<td style='display:none' id='ConveyorID" + rowCount + "'class='conveyorid_general'>" + item.ConveyorID + "</td></tr>"


                                });
                                $('#list-amended-material_ tbody').append(row);

                                if ('@Update' == 'False' || '@Edit' == 'False') {
                                    $('#list-amended-material_ tbody tr').each(function () {
                                        $(this).find("td:nth-child(2)").hide();
                                    });
                                    $('#list-amended-material_ thead tr').each(function () {
                                        $(this).find("td:nth-child(2)").hide();
                                    });

                                }
                                else {
                                    $('#list-amended-material_ tbody tr').each(function () {
                                        $(this).find("td:nth-child(1)").show();
                                    });
                                    $('#list-amended-material_ thead tr').each(function () {
                                        $(this).find("td:nth-child(2)").show();
                                    });
                                }
                                if ('@Delete' == 'False') {
                                    $('#list-amended-material_ tbody tr').each(function () {
                                        $(this).find("td:nth-child(1)").hide();
                                    });
                                    $('#list-amended-material_ thead tr').each(function () {
                                        $(this).find("td:nth-child(1)").hide();
                                    });
                                }
                                else {
                                    $('#list-amended-material_ tbody tr').each(function () {
                                        $(this).find("td:nth-child(1)").show();
                                    });
                                    $('#list-amended-material_ thead tr').each(function () {
                                        $(this).find("td:nth-child(1)").show();
                                    });
                                }
                                return false;
                            }

                        }
                    });
                    $('.storeGeneral_').val("");
                    $('.categoryGeneral_').val("");
                    $('.groupGeneral_').val("");
                    $('#MaterialMasterId_div .custom-combobox input').val('');
                    $('.rate_general').val("0");
                    $('.Piecies_genral').val("0");
                    $("#CurrentStock").val("0");
                    $('.PieciesType_general').val("0");
                    $('.LotNo_general').val("0");
                    $('.BalanceInThisListlot_general').val("0");
                    $('.BalanceInthisListType_general').val("0");
                    $('.MachineName_general').val(0);
                    $('.NoOfSets_general').val("0");
                    $('.color_Genral').val(0);
                    $('.substance_color').val(0);
                    $('#RequiredQty').val("0");
                    $('#AlredayIssued').val("0");
                    $('#TotalQty').val("");
                    $('#CurrentIssue').val("");
                    $('#RequiredType').val(0);
                    $('#AlreadyUsedType').val(0);
                    $('#CurrentIssuesType').val(0);
                    $('.conveyorid_general').val(0);
                }
            }

            else {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    url: '/MultipleIssue/SaveDirectIssue',
                    data: {
                        IssueType: $('.issueTypecls').val(),
                        IssueSlipNo: $('.General_IssueSlipNo').val(),
                        InternalValue: $('.InternalValue_general option:selected').text(),
                        DirectIssue_Style: $('.DirectIssue_Style_general').val(),
                        StoreName: store,
                        CategoryName: $('.categoryGeneral_').val(),
                        InternalOrderingFor: issueFor,
                        BuyerMasterId: custmername,
                        GroupName: $('.groupGeneral_').val(),
                        MaterialDescription: $('.MaterialGeneral_').val(),
                        Piecies: $('.Piecies_genral').val(),
                        PieciesType: $('.PieciesType_general').val(),
                        LotNo: $('.LotNo_general').val(),
                        BalanceInThisListlot: $('.BalanceInThisListlot_general').val(),
                        BalanceInthisListType: $('.BalanceInthisListType_general').val(),
                        MachineName: $('.MachineName_general').val(),
                        NoOfSets: $('.NoOfSets_general').val(),
                        Color: $('.color_Genral').val(),
                        Rate: $('.rate_general').val(),
                        SubtanceID: $('.substance_color').val(),
                        Color: $('.color_Genral').val(),
                        RequiredQty: RequiredQty_,
                        AlredayIssued: AlredayIssued_,
                        CurrentIssue: CurrentIssue_,
                        RequiredType: RequiredType_,
                        AlreadyUsedType: AlreadyUsedType_,
                        CurrentIssuesType: CurrentIssuesType_,
                        ConveyorID: $('.conveyorid_general').val(),
                        PiecesRequiredQTY: PiecesRequiredQTY_,
                        PiecesRequiredQtyType: PiecesRequiredQtyType_,
                        PiecesAlreadyIssueType: PiecesAlreadyIssueType_,
                        PiecesCurrentIssue: PiecesCurrentIssue_,
                        PiecesCurrentType: PiecesCurrentType_,
                        PiecesAlreadyIssue: PiecesAlreadyIssue_,
                        ConveyorID: $('.conveyorid_general').val(),
                        IssueSlipID: $('#IssueSlipID').val(),
                        IssueSlipFK: $('#IssueSlipFK').val(),
                        IssueDate: $('#IssueDate').val(),
                        TotalQty: $('#TotalQty').val(),
                        MaterialType: $('#MaterialType').val(),
                        Size: size,
                        Qty: Qty_,
                        Season: $('.seasoncls_direct option:selected').val()
                    },
                    success: function (data) {

                        if (data.issue != null) {
                            alert("Saved Successfully");

                            rowCount = $('#list-amended-material_ tr').length;
                            if (data.issue.RequiredQty == null) {
                                data.issue.RequiredQty = 0;
                            }
                            if (data.issue.AlredayIssued == null) {
                                data.issue.AlredayIssued = 0;
                            }
                            $('#sizeRangeTable tbody tr').each(function () {
                                $(this).find("td:not(:first)").remove();
                            });

                            $("#IssueSlipFK").val(data.issue.IssueSlipFK);
                            $("#list-amended-material_ tbody").html("");
                            var row = "";
                            $.each(data.issueSlipMaterialList, function (i, item) {
                                $("#RemoveHead").css("display", "block");
                                $("#EditHead").css("display", "block");
                                if (item.AlredayIssued == null) {
                                    item.AlredayIssued = 0;
                                }
                                if (item.RequiredQty == null) {
                                    item.RequiredQty = 0;
                                }
                                if (item.CurrentIssue == null) {
                                    item.CurrentIssue = 0;
                                }
                                row += "<tr id=" + item.IssueSlipId + "><td><input type='button' value='Remove' onclick='Delete(" + item.IssueSlipId + ")' class='' />" + "</td>"
                                row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IssueSlipId + ")' class='' />" + "</td>"
                                row += "<td  style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + data.grnTypeMaster.IssueType + "</td>"
                                row += "<td id='IssueSlipNo" + rowCount + "' class='General_IssueSlipNo'>" + item.IssueSlipNo + "</td>"
                                row += "<td id='InternalValue" + rowCount + "' class='InternalValue_general'>" + item.InternalValue + "</td>"
                                row += "<td id='DirectIssue_Style" + rowCount + "' class='DirectIssue_Style_general'>" + item.DirectIssue_Style + "</td>"
                                row += "<td id='IssueType" + rowCount + "' class='issueTypecls'>" + data.grnTypeMaster.IssueType + "</td>"
                                row += "<td id='NoOfSets" + rowCount + "' class='NoOfSets_general'>" + item.NoOfSets + "</td>"
                                row += "<td id='GroupName" + rowCount + "' class='groupGeneral_'>" + item.GroupId + "</td>"
                                row += "<td id='CategoryName" + rowCount + "'  class='categoryGeneral_'>" + item.CategoryId + "</td>"
                                row += "<td id='StoreName" + rowCount + "' class='storeGeneral_'>" + item.StoreName + "</td>"
                                row += "<td id='MaterialDescription" + rowCount + "' class='MaterialGeneral_'>" + item.MaterialName + "</td>"
                                row += "<td id='Color" + rowCount + "' class='color_Genral'>" + item.ColourId + "</td>"
                                row += "<td id='RequiredQty" + rowCount + "' class='RequiredQty'>" + item.RequiredQty + "</td>"
                                row += "<td id='AlredayIssued" + rowCount + "' class='AlredayIssued'>" + item.AlredayIssued + "</td>"
                                row += "<td id='CurrentIssue" + rowCount + "' class='CurrentIssue'>" + item.CurrentIssue + "</td>"
                                row += "<td style='' id='Rate" + rowCount + "' class='rate_general'>" + item.Rate + "</td>"
                                row += "<td style='' id='Season" + rowCount + "' class='seasoncls_direct'>" + item.Season + "</td>"
                                row += "<td style='display:none' id='TotalQty" + rowCount + "' class='TotalQty_genral'>" + item.TotalQty + "</td>"
                                row += "<td style='display:none' id='Piecies" + rowCount + "' class='Piecies_genral'>" + item.Piecies + "</td>"
                                row += "<td style='display:none' id='MaterialType" + rowCount + "' class='MaterialType_genral'>" + item.MaterialType + "</td>"
                                row += "<td style='display:none' id='PieciesType" + rowCount + "' class='PieciesType_general'>" + item.PieciesType + "</td>"
                                row += "<td style='display:none' id='LotNo" + rowCount + "' class='LotNo_general'>" + item.LotNo + "</td>"
                                row += "<td style='display:none' id='BalanceInThisListlot" + rowCount + "' class='BalanceInThisListlot_general'>" + item.BalanceInThisListlot + "</td>"
                                row += "<td style='display:none' id='BalanceInthisListType" + rowCount + "' class='BalanceInthisListType_general'>" + item.BalanceInthisListType + "</td>"
                                row += "<td style='display:none' id='MachineName" + rowCount + "' class='MachineName_general'>" + item.MachineName + "</td>"
                                row += "<td style='display:none' id='SubtanceID" + rowCount + "' class='substance_color'>" + item.SubtanceID + "</td>"
                                row += "<td style='display:none' id='RequiredType" + rowCount + "' class='RequiredType'>" + item.ShipmentMode + "</td>"
                                row += "<td style='display:none' id='AlreadyUsedType" + rowCount + "' class='AlreadyUsedType'>" + item.FreightAmount + "</td>"
                                row += "<td style='display:none' id='StoreName" + rowCount + "' class='storeGeneral_'>" + item.StoreMasterId + "</td>"
                                row += "<td style='display:none' id='CurrentIssuesType" + rowCount + "' class='CurrentIssuesType'>" + item.CurrentIssuesType + "</td>"
                                row += "<td style='display:none' id='IssueDate" + rowCount + "' class='IssueDate'>" + item.IssueDate + "</td>"
                                row += "<td style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + item.IssueType + "</td>"
                                row += "<td style='display:none' id='ConveyorID" + rowCount + "'class='conveyorid_general'>" + item.ConveyorID + "</td></tr>";
                            });
                            $('#list-amended-material_ tbody').append(row)
                            if ('@Update' == 'False' || '@Edit' == 'False') {
                                $('#list-amended-material_ tbody tr').each(function () {
                                    $(this).find("td:nth-child(2)").hide();
                                });
                                $('#list-amended-material_ thead tr').each(function () {
                                    $(this).find("td:nth-child(2)").hide();
                                });

                            }
                            else {
                                $('#list-amended-material_ tbody tr').each(function () {
                                    $(this).find("td:nth-child(1)").show();
                                });
                                $('#list-amended-material_ thead tr').each(function () {
                                    $(this).find("td:nth-child(2)").show();
                                });
                            }
                            if ('@Delete' == 'False') {
                                $('#list-amended-material_ tbody tr').each(function () {
                                    $(this).find("td:nth-child(1)").hide();
                                });
                                $('#list-amended-material_ thead tr').each(function () {
                                    $(this).find("td:nth-child(1)").hide();
                                });
                            }
                            else {
                                $('#list-amended-material_ tbody tr').each(function () {
                                    $(this).find("td:nth-child(1)").show();
                                });
                                $('#list-amended-material_ thead tr').each(function () {
                                    $(this).find("td:nth-child(1)").show();
                                });
                            }
                            return false;
                        }

                    }
                });
                $('.storeGeneral_').val("");
                $('.categoryGeneral_').val("");
                $('.groupGeneral_').val("");
                $('#MaterialMasterId_div .custom-combobox input').val('');
                $('.rate_general').val("0");
                $('.Piecies_genral').val("0");
                $('.PieciesType_general').val("0");
                $('.LotNo_general').val("0");
                $('.BalanceInThisListlot_general').val("0");
                $('.BalanceInthisListType_general').val("0");
                $('.MachineName_general').val(0);
                $('.NoOfSets_general').val("0");
                $('.color_Genral').val(0);
                $('.substance_color').val(0);
                $('#RequiredQty').val("0");
                $('#AlredayIssued').val("0");
                $('#TotalQty').val("");
                $('#CurrentIssue').val("");
                $('#RequiredType').val(0);
                $('#AlreadyUsedType').val(0);
                $('#CurrentIssuesType').val(0);
                $('.conveyorid_general').val(0);
            }

        }

    });
});
function ExapndDeatils(arg, arg1, arg2) {
    var chkArray = [];
    $(".chkProduct:checked").each(function () {
        chkArray.push($(this).val());
    });
    /* we join the array separated by the comma */
    var selected;
    selected = chkArray.join(',');
    var InternalOrderid = "";
    InternalOrderid = selected;
    //  InternalOrderid = InternalOrderid.slice(0, -1);
    $.ajax({
        url: '/MultipleIssue/ExpandDetails',
        type: "GET",
        dataType: "JSON",
        data: {
            OrderNo: arg1,
            MaterialName: arg,
            Color: arg2,
            OrderEntryID: InternalOrderid
        },
        success: function (data) {
            $("#sizeRangeTables").hide();
            $('#sizeRangeTables tbody tr').each(function () {
                $(this).find("td:not(:first)").remove();
            });
            for (var i = 0; i <= data.length - 1; i++) {
                isSizeRangeMaster = true;
                $('#sizeRangeTables tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + data[i].SizeRange + ' </td> ');
                $('#sizeRangeTables tbody tr:last').append("<td class='Rate' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty'  onchange='CalculateTotal(this)'  id='Qty" + i + "' value='" + data[i].Qty + "' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
            }
            $("#sizeRangeTables").show();
        }
    });
}
function Show() {
    $('.loader-overlay').show();
    if ($("#IssueType").val().trim() == "") {
        alert("Please select issue type");
        return false;
    }
    var chkArray = [];
    $(".chkProduct:checked").each(function () {
        chkArray.push($(this).val());
    });
    /* we join the array separated by the comma */
    var selected;
    selected = chkArray.join(',');
    var InternalOrderid = "";
    InternalOrderid = selected;
    if (InternalOrderid != null && InternalOrderid.length > 0) {

    }
    else {
        alert("Please select internal order no");
        return false;
    }

    $.ajax({
        url: '/MultipleIssue/GetInternalBuyeSlNoWithMaterialName',
        type: "GET",
        dataType: "JSON",
        data: {
            InternalOderID: InternalOrderid,
            IssueType: $("#IssueType").val(),
            IssueSlipNo: $("#IssueSlipNo").val(),
            StoreName: $("#StoreName option:selected").text(),
            CategoryName: $("#CategoryName option:selected").text(),
            LotNo: $('#LotNo').val(),
            Style: $('#Style').val(),
            CuttingIssueTypeID: $('#CuttingIssueTypeID').val(),
            Season: $('#Season').val(),
            BuyerMasterId: $('#BuyerMasterId').val(),
            BomStyle: $('#BomStyle').val(),
        },
        success: function (data) {
            $('.loader-overlay').hide();
            if (data.Message != null) {
                alert("Already issued:" + data.Message);
                return false;
            }
            else if (data.Nodata != null) {
                alert("There is no material selected orders.");
                return false;
            }
            else {
                $("#list-amended-material tbody").html("");
                var tabelquery = "";
                $.each(data.Material, function (i, item) {
                    if (parseFloat(item.CurrentIssue) < 0) {
                        item.CurrentIssue = 0;
                    }
                    if (item.IssueType == 1) {
                        item.IssueType = "Order";
                    }
                    else if (item.IssueType == 2) {
                        item.IssueType = "General";
                    }
                    if (parseFloat(item.BalanceStock) < 0) {
                        item.CurrentIssue = 0;
                    }
                    $("#RemoveHead").css("display", "block");
                    $("#EditHead").css("display", "block");
                    tabelquery += "<tr id=" + item.IssueSlipId + "> +'<td id='IssueSlipId' style='display:none' class='IssueSlipId' value=''>" + item.IssueSlipId + "</td>"
                    tabelquery += "<td id='IssueSlipFK' style='display:none' class='IssueSlipFK' value=''>" + item.IssueSlipFK + "</td>"
                    tabelquery += "<td id='Details_' name='Details_' class='Details_' value=''><input style='width: 49px;' type='button'  class='Details' onclick='ExapndDeatils( \"" + item.MaterialName + "\"," + item.OrderNo + "," + " \"" + item.ColourId + "\")' value='Details'></td>"
                    tabelquery += "<td id='IssueSlipNo'  name='IssueSlipNo' class='IssueSlipNo' value=''>" + $("#IssueSlipNo").val() + "</td>"
                    tabelquery += "<td id='InternalOderID1'name='InternalOderID1'  class='multipleOrder1_' value=''>" + item.OrderNo + "</td>"
                    tabelquery += "<td id='Style'name='Style'  class='Style' value=''>" + item.Style + "</td>"
                    tabelquery += "<td id='IssueType' name='IssueType' class='IssueType' value=''>" + item.IssueType + "</td>"
                    tabelquery += "<td id='MaterialTypes' name='MaterialTypes'  class='MaterialTypes' value=''>" + item.MaterialTypes + "</td>"
                    tabelquery += "<td id='GroupName' name='GroupName' class='GroupName' value=''>" + item.GroupId + "</td>"
                    tabelquery += "<td id='CategoryName' name='CategoryName' class='CategoryName' value=''>" + item.CategoryId + "</td>"
                    tabelquery += "<td id='StoreName' name='StoreName'  class='StoreName' value=''>" + item.StoreName + "</td>"
                    tabelquery += "<td id='MaterialDescription' name='MaterialDescription' class='MaterialDescription' value=''>" + item.MaterialName + "</td>"
                    tabelquery += "<td id='Color'  class='Color' name='Color' value=''>" + item.ColourId + "</td>"
                    tabelquery += "<td id='BalanceStock' name='BalanceStock' class='BalanceStock' value=''>" + item.BalanceStock + "</td>"
                    tabelquery += "<td id='RequiredQty' name='RequiredQty' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                    tabelquery += "<td id='AlredayIssued' name='AlredayIssued' class='AlredayIssued' value=''>" + item.AlredayIssued + "</td>"
                    tabelquery += "<td id='CurrentIssue' name='CurrentIssue' class='CurrentIssue' value=''><input style='width: 49px;' type='text' class='CurrentIssueValue' onchange=issueChange(" + item.BalanceStock + ",this.value) value='" + item.CurrentIssue + "'></td>"
                    tabelquery += "<td id='Rate'  class='Rate' name='Rate' value=''>" + item.Rate + "</td>"
                    tabelquery += "<td id='IsChecked' class='IsChecked'><input type='checkbox' name='isMaterialckbox' class='isMaterialckbox'></td>"
                    tabelquery += "<td style='display:none' id='StoreMasterId'  class='StoreMasterId' name='StoreMasterId' value=''>" + item.StoreMasterId + "</td>"
                    tabelquery += "<td style='display:none' id='MaterialMasterId'  class='MaterialMasterId' name='MaterialMasterId' value=''>" + item.MaterialMasterId + "</td></tr>"
                });
                $("#list-amended-material tbody").append(tabelquery);
            }
        }
    });
}
function RowClick(arg) {

    $('#list-amended-material_ tr#' + arg + '').remove();
    $.ajax({
        url: '/MultipleIssue/getIssueID',
        type: "GET",
        dataType: "JSON",
        data: { IssueSlipID: arg },
        success: function (cities) {
            //disabled and enabled
            debugger;
            if (cities.materialCategoryMaster != null) {
                if (cities.materialCategoryMaster.CategoryName != null && cities.materialCategoryMaster.CategoryName.trim() == "Leathers") {
                    $("#divPiecesIssue").css("display", "block");
                    $("#divWithoutPieces").css("display", "none");
                }
                else {
                    $("#divPiecesIssue").css("display", "none");
                    $("#divWithoutPieces").css("display", "block");
                }
            }
            if (cities.grnTypeMaster.IssueType == "Direct Issue" || cities.grnTypeMaster.IssueType == "Other Issue") {
                if (cities.grnTypeMaster.IssueType == "Other Issue") {
                    $('#InternalOrderingFor option').filter('[value="1"],[value="2"],[value="3"],[value="4"],[value="5"]').remove();
                }
                $("#internal_Order").hide();
                $("#internal_DirectIssue").show();
                $("#InternalValue").val("");
                $("#Direct_Style").hide();
                $("#internal_DirectIssue_Style").show();
                $("#DirectIssue_Style").val("");
            }
            else {
                $("#internal_Order").show();
                $("#internal_DirectIssue").hide();
                $("#InternalValue").val("");
                $("#Direct_Style").show();
                $("#internal_DirectIssue_Style").hide();
                $("#DirectIssue_Style").val("");
            }



            $('#IssueSlipID').val(cities.issueSlipeDetails.IssueSlipId);
            $('#IssueSlipFK').val(cities.issueSlipeDetails.IssueSlipFK);
            $('.storeGeneral_').val(cities.issueSlipeDetails.StoreMasterId);
            $('.categoryGeneral_').val(cities.issueSlipeDetails.CategoryId);
            $('.groupGeneral_').val(cities.issueSlipeDetails.GroupId);
            $('.Piecies_genral').val(cities.issueSlipeDetails.Piecies);
            $('.PieciesType_general').val(cities.issueSlipeDetails.PieciesType);
            $('.LotNo_general').val(cities.issueSlipeDetails.LotNo);
            $('.BalanceInThisListlot_general').val(cities.issueSlipeDetails.BalanceInThisListlot);
            $('.BalanceInthisListType_general').val(cities.issueSlipeDetails.BalanceInthisListType);
            $('.MachineName_general').val(cities.issueSlipeDetails.MachineName);
            $('.NoOfSets_general').val(cities.issueSlipeDetails.NoOfSets);
            $('.color_Genral').val(cities.issueSlipeDetails.ColourId);
            $('.substance_color').val(cities.issueSlipeDetails.StoreMasterId);
            $('#RequiredQty').val(cities.issueSlipeDetails.RequiredQty);
            $('#AlredayIssued').val(cities.issueSlipeDetails.AlredayIssued);
            debugger;
            if (cities.materialCategoryMaster.CategoryName.trim() == "Leathers") {
                $('.CurrentIssueGeneral_cls').val(cities.issueSlipeDetails.CurrentIssue);
            }
            else {
                $('#CurrentIssue').val(cities.issueSlipeDetails.CurrentIssue);
            }
            $('#RequiredType').val(cities.issueSlipeDetails.RequiredType);
            $("#MaterialType").val(cities.issueSlipeDetails.MaterialType);
            $('#AlreadyUsedType').val(cities.issueSlipeDetails.AlreadyUsedType);
            $('#CurrentIssuesType').val(cities.issueSlipeDetails.CurrentIssuesType);
            $('#PiecesRequiredQTY').val(cities.issueSlipeDetails.PiecesRequiredQTY);
            $('#PiecesRequiredQtyType').val(cities.issueSlipeDetails.PiecesRequiredQtyType);
            $('#PiecesAlreadyIssueType').val(cities.issueSlipeDetails.PiecesAlreadyIssueType);
            $('#PiecesCurrentIssue').val(cities.issueSlipeDetails.PiecesCurrentIssue);
            $('#PiecesCurrentType').val(cities.issueSlipeDetails.PiecesCurrentType);
            $('#IssueDate').val(cities.issueSlipeDetails.IssueDate);
            $('#TotalQty').val(cities.issueSlipeDetails.TotalQty);
            $('#PiecesAlreadyIssue').val(cities.issueSlipeDetails.PiecesAlreadyIssue);
            $('.rate_general').val(cities.issueSlipeDetails.Rate);
            $('.conveyorid_general').val(cities.issueSlipeDetails.ConveyorID);
            $('.DirectIssue_Style_general').val(cities.issueSlipeDetails.DirectIssue_Style);
            $('.InternalValue_general').val(cities.issueSlipeDetails.InternalValue);
            $('.General_IssueSlipNo').val(cities.issueSlipeDetails.IssueSlipNo);
            $('#MaterialMasterId_div .custom-combobox input').val('');
            $('.MaterialGeneral_').val(cities.issueSlipeDetails.MaterialMasterId);
            $('.seasoncls_direct').val(cities.issueSlipeDetails.Season);
            $('.MaterialGeneral_').combobox();
 
            $('.MaterialGeneral_').combobox('destroy');
            $('.MaterialGeneral_').combobox();
            $('#sizeRangeTable tbody tr').each(function () {
                $(this).find("td:not(:first)").remove();

            });
            var count = 1;
            for (var i = 0; i <= cities.SizeRange.length - 1; i++) {
                $('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + cities.SizeRange[i].SizeRange + ' </td> ');
                $('#sizeRangeTable tbody tr:nth-child(2)').append("<td class='stockInHand' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' readonly class='currentstock' id='currentstock" + i + "' value=" + cities.currentStocklist[i].StockInHand + " style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                $('#sizeRangeTable tbody tr:last').append("<td class='Qty' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty' onchange='CalculateTotal(this," + cities.currentStocklist[i].StockInHand + ")' id='Qty" + i + "' value='" + cities.SizeRange[i].Qty + "' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");


                //$('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + cities.SizeRange[i].SizeRange + ' </td> ');
                //$('#sizeRangeTable tbody tr:last').append("<td class='Rate' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty'  onchange='CalculateTotal(this)'  id='Qty" + i + "' value='" + cities.SizeRange[i].Qty + "' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                count++;
            }


        }
    });

}
function EditClick(arg) {
    debugger;
    $('#list-amended-material_ >tbody').find('tr#' + arg + '').remove();
    $.ajax({
        url: '/MultipleIssue/getIssueID',
        type: "GET",
        dataType: "JSON",
        data: { IssueSlipID: arg },
        success: function (cities) {                //disabled and enabled
            debugger;
            if (cities.materialCategoryMaster != null) {
                if (cities.materialCategoryMaster.CategoryName != null &&  cities.materialCategoryMaster.CategoryName.trim() == "Leathers") {
                    $("#divPiecesIssue").css("display", "block");
                    $("#divWithoutPieces").css("display", "none");
                }
                else {
                    $("#divPiecesIssue").css("display", "none");
                    $("#divWithoutPieces").css("display", "block");
                }
            }
            if (cities.grnTypeMaster.IssueType == "Direct Issue" || cities.grnTypeMaster.IssueType == "Other Issue") {
                if (cities.grnTypeMaster.IssueType == "Other Issue") {
                    $('#InternalOrderingFor option').filter('[value="1"],[value="2"],[value="3"],[value="4"],[value="5"]').remove();
                }
                $("#internal_Order").hide();
                $("#internal_DirectIssue").show();
                $("#InternalValue").val("");
                $("#Direct_Style").hide();
                $("#internal_DirectIssue_Style").show();
                $("#DirectIssue_Style").val("");
            }
            else {

                $('#CurrentIssue').val(cities.issueSlipeDetails.CurrentIssue);
                $("#internal_Order").show();
                $("#internal_DirectIssue").hide();
                $("#InternalValue").val("");
                $("#Direct_Style").show();
                $("#internal_DirectIssue_Style").hide();
                $("#DirectIssue_Style").val("");
            }
            $('#IssueSlipID').val(cities.issueSlipeDetails.IssueSlipId);
            //$("#IssueType_").val();issueTypecls 
            $('#IssueSlipFK').val(cities.issueSlipeDetails.IssueSlipFK);
            $('.storeGeneral_').val(cities.issueSlipeDetails.StoreMasterId);
            $('.categoryGeneral_').val(cities.issueSlipeDetails.CategoryId);
            $('.seasoncls_direct').val(cities.issueSlipeDetails.Season);
            $('.groupGeneral_').val(cities.issueSlipeDetails.GroupId);
            $('.Piecies_genral').val(cities.issueSlipeDetails.Piecies);
            $('.PieciesType_general').val(cities.issueSlipeDetails.PieciesType);
            $('.LotNo_general').val(cities.issueSlipeDetails.LotNo);
            $('.BalanceInThisListlot_general').val(cities.issueSlipeDetails.BalanceInThisListlot);
            $('.BalanceInthisListType_general').val(cities.issueSlipeDetails.BalanceInthisListType);
            $('.MachineName_general').val(cities.issueSlipeDetails.MachineName);
            $('.NoOfSets_general').val(cities.issueSlipeDetails.NoOfSets);
            $('.color_Genral').val(cities.issueSlipeDetails.ColourId);
            $('.substance_color').val(cities.issueSlipeDetails.StoreMasterId);
            $('.rate_general').val(cities.issueSlipeDetails.Rate);
            $('.conveyorid_general').val(cities.issueSlipeDetails.ConveyorID);
            $('.DirectIssue_Style_general').val(cities.issueSlipeDetails.DirectIssue_Style);
            $('.InternalValue_general').val(cities.issueSlipeDetails.InternalValue);
            $('.InternalValue_general').combobox('destroy');
            $('.InternalValue_general').combobox();
            $('.General_IssueSlipNo').val(cities.issueSlipeDetails.IssueSlipNo);
            $('#MaterialMasterId_div .custom-combobox input').val('');
            $('.MaterialGeneral_').val(cities.issueSlipeDetails.MaterialMasterId);
            $('.MaterialGeneral_').combobox('destroy');
            $('.MaterialGeneral_').combobox();
            $('#sizeRangeTable tbody tr').each(function () {
                $(this).find("td:not(:first)").remove();

            });
            var count = 1;
            for (var i = 0; i <= cities.SizeRange.length - 1; i++) {
                $('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + cities.SizeRange[i].SizeRange + ' </td> ');
                $('#sizeRangeTable tbody tr:nth-child(2)').append("<td class='stockInHand' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' readonly class='currentstock' id='currentstock" + i + "' value=" + cities.currentStocklist[i].StockInHand + " style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                $('#sizeRangeTable tbody tr:last').append("<td class='Qty' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty' onchange='CalculateTotal(this," + cities.currentStocklist[i].StockInHand + ")' id='Qty" + i + "' value='" + cities.SizeRange[i].Qty + "' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                //$('#sizeRangeTable tbody tr:first-child').append('<td class="SizeRangeRefValue" style="background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;"> ' + cities.SizeRange[i].SizeRange + ' </td> ');
                //$('#sizeRangeTable tbody tr:last').append("<td class='Rate' style='background-color: #ddd;padding-bottom: 3px;padding-left: 0;padding-top: 1px;width: 39px !important;'><input type='text' class='Qty'  onchange='CalculateTotal(this)'  id='Qty" + i + "' value='" + cities.SizeRange[i].Qty + "' style='padding-bottom: 0;padding-top: 0;width: 34px;' /</td>");
                count++;
            }
            //disabled and enabled
            if (cities.issueSlipeDetails != null) {
                if ((cities.issueSlipeDetails.StoreName == "Leather Store" || cities.issueSlipeDetails.StoreName == "Leather Store-Local" || cities.issueSlipeDetails.StoreName == "Leather Stores-Import")) {
                    $("#divWithoutPieces").hide();
                    $("#divPiecesIssue").show();
                    if (cities.issueSlipeDetails.AlredayIssued == null) {
                        cities.issueSlipeDetails.AlredayIssued = 0;
                    }
                    if (cities.issueSlipeDetails.RequiredQty == null) {
                        cities.issueSlipeDetails.RequiredQty = 0;
                    }
                    $('#IssueDate').val(cities.issueSlipeDetails.IssueDate);
                    if (cities.materialCategoryMaster.CategoryName.trim() == "Leathers") {
                        $('.CurrentIssueGeneral_cls').val(cities.issueSlipeDetails.CurrentIssue);
                    }
                    else {
                        $('.CurrentIssueGeneral').val(cities.issueSlipeDetails.CurrentIssue);
                    }

                    $('.RequiredQty_Cls').val(cities.issueSlipeDetails.RequiredQty);
                    $('.AlredayIssued_cls').val(cities.issueSlipeDetails.AlredayIssued);
                    $('.RequiredType_cls').val(cities.issueSlipeDetails.RequiredType);
                    $("#MaterialType").val(cities.issueSlipeDetails.MaterialType);
                    $('.AlreadyUsedType_cls').val(cities.issueSlipeDetails.AlreadyUsedType);
                    $('.CurrentIssuesType_cls').val(cities.issueSlipeDetails.CurrentIssuesType);
                    $('.PiecesRequiredQTY_cls').val(cities.issueSlipeDetails.PiecesRequiredQTY);
                    $('.PiecesRequiredQtyType_cls').val(cities.issueSlipeDetails.PiecesRequiredQtyType);
                    $('.PiecesAlreadyIssue_cls').val(cities.issueSlipeDetails.PiecesAlreadyIssue);
                    $('.PiecesAlreadyIssueType_cls').val(cities.issueSlipeDetails.PiecesAlreadyIssueType);
                    $('.PiecesCurrentIssue_cls').val(cities.issueSlipeDetails.PiecesCurrentIssue);
                    $('.CurrentIssuesType_cls').val(cities.issueSlipeDetails.PiecesCurrentType);
                    $('.InternalOrderingFor_cls').val(cities.issueSlipeDetails.InternalOrderingFor);
                    $('.BuyerMasterId_cls ').val(cities.issueSlipeDetails.BuyerMasterId);
                }
                else {
                    $("#divPiecesIssue").hide();
                    $("#divWithoutPieces").show();
                    $('#RequiredQty').val(cities.issueSlipeDetails.RequiredQty);
                    $('#CurrentIssue').val(cities.issueSlipeDetails.CurrentIssue);
                    $('#AlredayIssued').val(cities.issueSlipeDetails.AlredayIssued);
                    $('#RequiredType').val(cities.issueSlipeDetails.RequiredType);
                    $("#MaterialType").val(cities.issueSlipeDetails.MaterialType);
                    $('#AlreadyUsedType').val(cities.issueSlipeDetails.AlreadyUsedType);
                    $('#CurrentIssuesType').val(cities.issueSlipeDetails.CurrentIssuesType);
                    $('#PiecesRequiredQTY').val(cities.issueSlipeDetails.PiecesRequiredQTY);
                    $('#PiecesRequiredQtyType').val(cities.issueSlipeDetails.PiecesRequiredQtyType);
                    $('#PiecesAlreadyIssueType').val(cities.issueSlipeDetails.PiecesAlreadyIssueType);
                    $('#PiecesCurrentIssue').val(cities.issueSlipeDetails.PiecesCurrentIssue);
                    $('#PiecesCurrentType').val(cities.issueSlipeDetails.PiecesCurrentType);
                    $('#IssueDate').val(cities.issueSlipeDetails.IssueDate);
                    $('#TotalQty').val(cities.issueSlipeDetails.TotalQty);
                    $('#PiecesAlreadyIssue').val(cities.issueSlipeDetails.PiecesAlreadyIssue);
                    $('#InternalOrderingFor').val(cities.issueSlipeDetails.InternalOrderingFor);
                    $('#BuyerMasterId ').val(cities.issueSlipeDetails.BuyerMasterId);
                }

            }

        }
    });
}
function Delete(arg) {
    $('#list-amended-material tr#' + arg + '').remove();
    $.ajax({
        url: '/MultipleIssue/IssueslipItemDelete',
        type: "Post",
        dataType: "JSON",
        data: { IssueSlipId: arg },
        success: function (data) {
            var rowCount = $('#list-amended-material_ tr').length;
            if (data.status == "Success") {
                alert("Material deleted successfuly.");
                $("#list-amended-material_ tbody").html("");
                var row = "";
                $.each(data.MaterialList, function (i, item) {
                    $("#RemoveHead").css("display", "block");
                    $("#EditHead").css("display", "block");
                    if (item.AlredayIssued == null) {
                        item.AlredayIssued = 0;
                    }
                    row += "<tr id=" + item.IssueSlipId + "><td><input type='button' value='Remove' onclick='Delete(" + item.IssueSlipId + ")' class='' />" + "</td>"
                    row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IssueSlipId + ")' class='' />" + "</td>"
                    row += "<td  style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + data.grnTypeMaster.IssueType + "</td>"
                    row += "<td id='IssueSlipNo" + rowCount + "' class='General_IssueSlipNo'>" + item.IssueSlipNo + "</td>"
                    row += "<td id='InternalValue" + rowCount + "' class='InternalValue_general'>" + item.Style + "</td>"
                    row += "<td id='DirectIssue_Style" + rowCount + "' class='DirectIssue_Style_general'>" + item.DirectIssue_Style + "</td>"
                    row += "<td id='IssueType" + rowCount + "' class='issueTypecls'>" + data.grnTypeMaster.IssueType + "</td>"
                    row += "<td id='NoOfSets" + rowCount + "' class='NoOfSets_general'>" + item.NoOfSets + "</td>"
                    row += "<td id='GroupName" + rowCount + "' class='groupGeneral_'>" + item.GroupId + "</td>"
                    row += "<td id='CategoryName" + rowCount + "'  class='categoryGeneral_'>" + item.CategoryId + "</td>"
                    row += "<td id='StoreName" + rowCount + "' class='storeGeneral_'>" + item.StoreName + "</td>"
                    row += "<td id='MaterialDescription" + rowCount + "' class='MaterialGeneral_'>" + item.MaterialName + "</td>"
                    row += "<td id='Color" + rowCount + "' class='color_Genral'>" + item.ColourId + "</td>"
                    row += "<td id='RequiredQty" + rowCount + "' class='RequiredQty'>" + item.RequiredQty + "</td>"
                    row += "<td id='AlredayIssued" + rowCount + "' class='AlredayIssued'>" + item.AlredayIssued + "</td>"
                    row += "<td id='CurrentIssue" + rowCount + "' class='CurrentIssue'>" + item.CurrentIssue + "</td>"
                    row += "<td style='' id='Rate" + rowCount + "' class='rate_general'>" + item.Rate + "</td>"
                    row += "<td style='display:none' id='TotalQty" + rowCount + "' class='TotalQty_genral'>" + item.TotalQty + "</td>"
                    row += "<td style='display:none' id='MaterialType" + rowCount + "' class='MaterialType_genral'>" + item.MaterialType + "</td>"
                    row += "<td style='display:none' id='Piecies" + rowCount + "' class='Piecies_genral'>" + item.Piecies + "</td>"
                    row += "<td style='display:none' id='PieciesType" + rowCount + "' class='PieciesType_general'>" + item.PieciesType + "</td>"
                    row += "<td style='display:none' id='LotNo" + rowCount + "' class='LotNo_general'>" + item.LotNo + "</td>"
                    row += "<td style='display:none' id='BalanceInThisListlot" + rowCount + "' class='BalanceInThisListlot_general'>" + item.BalanceInThisListlot + "</td>"
                    row += "<td style='display:none' id='BalanceInthisListType" + rowCount + "' class='BalanceInthisListType_general'>" + item.BalanceInthisListType + "</td>"
                    row += "<td style='display:none' id='MachineName" + rowCount + "' class='MachineName_general'>" + item.MachineName + "</td>"
                    row += "<td style='display:none' id='SubtanceID" + rowCount + "' class='substance_color'>" + item.SubtanceID + "</td>"
                    row += "<td style='display:none' id='RequiredType" + rowCount + "' class='RequiredType'>" + item.ShipmentMode + "</td>"
                    row += "<td style='display:none' id='AlreadyUsedType" + rowCount + "' class='AlreadyUsedType'>" + item.FreightAmount + "</td>"
                    row += "<td style='display:none' id='StoreName" + rowCount + "' class='storeGeneral_'>" + item.StoreMasterId + "</td>"
                    row += "<td style='display:none' id='CurrentIssuesType" + rowCount + "' class='CurrentIssuesType'>" + item.CurrentIssuesType + "</td>"
                    row += "<td style='display:none' id='IssueDate" + rowCount + "' class='IssueDate'>" + item.IssueDate + "</td>"
                    row += "<td style='display:none' id='IssueType" + rowCount + "' class='issueTypecls'>" + item.IssueType + "</td>"
                    row += "<td style='display:none' id='ConveyorID" + rowCount + "'class='conveyorid_general'>" + item.ConveyorID + "</td></tr>"

                });
                $('#list-amended-material_ tbody').append(row);
                if ('@Update' == 'False' || '@Edit' == 'False') {
                    $('#list-amended-material_ tbody tr').each(function () {
                        $(this).find("td:nth-child(2)").hide();
                    });
                    $('#list-amended-material_ thead tr').each(function () {
                        $(this).find("td:nth-child(2)").hide();
                    });

                }
                else {
                    $('#list-amended-material_ tbody tr').each(function () {
                        $(this).find("td:nth-child(1)").show();
                    });
                    $('#list-amended-material_ thead tr').each(function () {
                        $(this).find("td:nth-child(2)").show();
                    });
                }
                if ('@Delete' == 'False') {
                    $('#list-amended-material_ tbody tr').each(function () {
                        $(this).find("td:nth-child(1)").hide();
                    });
                    $('#list-amended-material_ thead tr').each(function () {
                        $(this).find("td:nth-child(1)").hide();
                    });
                }
                else {
                    $('#list-amended-material_ tbody tr').each(function () {
                        $(this).find("td:nth-child(1)").show();
                    });
                    $('#list-amended-material_ thead tr').each(function () {
                        $(this).find("td:nth-child(1)").show();
                    });
                }
                return false;

            }
        }
    });
}
function GetLotNO() {
    $('.loader-overlay').show();
    var mrpSelectedText = "";
    var myDropDownListValues = $(".LotNo_general").multiselect("getChecked").map(function () {
        return $(this).val()
    }).get();
    var LotNo_val = "";
    for (i = 0; i < myDropDownListValues.length; i++) {

        if (myDropDownListValues[i].startsWith(",")) {
        }
        else {
            LotNo_val += myDropDownListValues[i] + ",";
        }
    }
    LotNo_val = LotNo_val.slice(0, -1);

    var seasonID = $("#Season").val()
    $.ajax({
        url: '/MultipleIssue/GetLotNoWithOrderNo',
        type: "GET",
        dataType: "JSON",
        data: {
            LotNo: LotNo_val, SeasonID: seasonID
        },
        success: function (data) {
            $('.loader-overlay').hide();
            if (data.length > 0) {
                $(".chkProduct").prop("checked", false);
                var li = "";
                for (var i = 0; i < data.length; i++) {
                    li = "";
                    if (data.length != 0) {
                        li = $('<li class="listItemPL" style="max-width: 380px; min-width: 380px; display: list-item;"><Label><input type="checkbox" name="AllcheckedProductsPL" class="chkProduct" value="' + data[i].Value + '"> ' + data[i].Text + ' </input></Label>')
                        $(".customDrop").append(li);
                    }
                }
            }

            else {
                alert("There is no order for this lot no");
                return false;
            }
        }
    });
}
function CalculateTotal(agr1, arg2) {
    debugger;
    var CurrentIssue_ = $("#CurrentIssue").val();
    if ($("#CurrentIssue").val() != "") {

        if (parseFloat(agr1.value) > parseFloat(arg2)) {
            $("#Add").hide();
            alert("Actual size current stock exceed issue quantity");
            return false;
        }
        else if (parseFloat(arg2) == 0 && parseFloat(agr1.value) != 0) {
            $("#Add").hide();
            alert("Actual size current stock is zero");
            return false;
        }
        else {
            $("#Add").show();
            var add = 0;
            $("#sizeRangeTable .Qty").each(function () {
                add += Number($(this).val());
            });

            $('#TotalQty').val(add);
            $('#TotalQty').prop("disabled", true);
            if (parseFloat(CurrentIssue_) < parseFloat(add)) {
                $("#Add").hide();
                alert("Current stock exceed issue qty");
                return false;
            }
            else {
                $("#Add").show();
            }
        }

    }
    else
    {
        $("#Add").hide();
        alert("Please add current issue quantity");
        return false;
    }

}
function validation() {

    debugger;
    if ($('#IssueDate').val() == "") {
        alert("Please select issue date."); $('#IssueDate').css('border-color', 'red'); $('#IssueDate').focus(); return false;
    }
    else {
        $('#IssueDate').css('border-color', '');
    }


    if ($('.General_IssueSlipNo').val() == "") {
        alert("Please Enter Issue Slip No."); $('.General_IssueSlipNo').css('border-color', 'red'); $('.General_IssueSlipNo').focus(); return false;
    }
    else {
        $('.General_IssueSlipNo').css('border-color', '');
    }
    if ($("#IssueType option:selected").text() != "Other Issue") {
        if ($(".storeGeneral_direct option:selected").text().trim() != "Maintenance Store") {
            if ($('.InternalValue_general').val() == "") {
                alert("Please Enter interanl order no."); $('.InternalValue_general').css('border-color', 'red'); $('.InternalValue_general').focus(); return false;
            }
            else {
                $('.InternalValue_general').css('border-color', '');
            }


            if ($('.DirectIssue_Style_general').val() == "") {
                alert("Please Enter style no."); $('.DirectIssue_Style_general').css('border-color', 'red'); $('.DirectIssue_Style_general').focus(); return false;
            }
            else {
                $('.DirectIssue_Style_general').css('border-color', '');
            }
            if ($('.seasoncls_direct').val() == "") {
                alert("Please Enter Season."); $('.seasoncls_direct').css('border-color', 'red'); $('.seasoncls_direct').focus(); return false;
            }
            else {
                $('.seasoncls_direct').css('border-color', '');
            }
        }
    }

    if ($('.storeGeneral_direct').val() == "") {
        alert("Please select store name."); $('.storeGeneral_').css('border-color', 'red'); $('.storeGeneral_').focus(); return false;
    }
    else {
        $('.storeGeneral_direct').css('border-color', '');
    }

    if ($('.categoryGeneral_').val() == "") {
        alert("Please select category name."); $('.categoryGeneral_').css('border-color', 'red'); $('.categoryGeneral_').focus(); return false;
    }
    else {
        $('.categoryGeneral_').css('border-color', '');
    }

    if ($('.groupGeneral_  option:selected').val() == "") {
        alert("Please select group name."); $('.groupGeneral_').css('border-color', 'red'); $('.groupGeneral_').focus(); return false;
    }
    else {
        $('.groupGeneral_').css('border-color', '');
    }

    if ($('.MaterialGeneral_').val() == "") {
        alert("Please material name."); $('.MaterialGeneral_').css('border-color', 'red'); $('.MaterialGeneral_').focus(); return false;
    }
    else {
        $('.MaterialGeneral_').css('border-color', '');
    }
    if ($('#MaterialType').val() == "" || $('#MaterialType').val() == "0") {
        alert("Please material type."); $('#MaterialType').css('border-color', 'red'); $('#MaterialType').focus(); return false;
    }
    else {
        $('#MaterialType').css('border-color', '');
    }
    if ($('.color_Genral').val() == "") {
        alert("Please select color."); $('.color_Genral').css('border-color', 'red'); $('.color_Genral').focus(); return false;
    }
    else {
        $('.color_Genral').css('border-color', '');
    }

    if ($('.rate_general').val() == "") {
        alert("Please Enter rate."); $('.rate_general').css('border-color', 'red'); $('.rate_general').focus(); return false;
    }
    else {
        $('.rate_general').css('border-color', '');
    }
    if ($('.issueTypecls  option:selected').first().text() == "Direct Issue" || $("#IssueType option:selected").text() == "Other Issue") {
        if ($('#StoreName  option:selected').text() == "Leather Store-Local" || $('#StoreName option:selected').text() == "Leather Stores-Import" || $('#StoreName option:selected').text().trim() == "Leather Store" || $('#StoreName option:selected').text() == "Leather Store") {
            if ($('.CurrentIssueGeneral').val() == "") {
                alert("Please enter current issue."); $('.CurrentIssueGeneral').css('border-color', 'red'); $('.CurrentIssueGeneral').focus(); return false;
            }
            else {
                $('.CurrentIssueGeneral').css('border-color', '');
            }
        }
        else {
            if (($('.storeGeneral_direct  option:selected').text() == "Leather Store-Local")) {
                if ($('.CurrentIssueGeneral_cls').val() == "") {
                    alert("Please enter current issue."); $('#CurrentIssue').css('border-color', 'red'); $('#CurrentIssue').focus(); return false;
                }
            }
            else {
                if ($('#CurrentIssue').val() == "") {
                    alert("Please enter current issue."); $('#CurrentIssue').css('border-color', 'red'); $('#CurrentIssue').focus(); return false;
                }
                //else if ($('.CurrentIssueGeneral_cls').val() == "") {
                //    alert("Please enter current issue."); $('#CurrentIssue').css('border-color', 'red'); $('#CurrentIssue').focus(); return false;
                //}
                else {
                    $('#CurrentIssue').css('border-color', '');
                }
            }
        }
    }
    else {
        if ($('#CurrentIssue').val() == "") {
            alert("Please enter current issue."); $('#CurrentIssue').css('border-color', 'red'); $('#CurrentIssue').focus(); return false;
        }
        else {
            $('#CurrentIssue').css('border-color', '');
        }
    }

}
function issueChange(arg, arg1) {
    var BalanceStockQtyQty = "";
    var CurrentIssue = "";
    BalanceStockQtyQty = arg;
    CurrentIssue = arg1;
    if (BalanceStockQtyQty == "") {
        BalanceStockQtyQty = 0;
    }
    if (parseFloat(CurrentIssue.trim()) > parseFloat(BalanceStockQtyQty.trim())) {
        alert("Please enter valid Current issue.");
        return false;
    }
    else {
        $("#allIssue").show();
    }
}
function filter(element) {
    var value = $(element).val().toLowerCase();
    $(".customDrop .listItemPL").each(function () {
        if ($(this).text().toLowerCase().indexOf(value) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
    $(".customDrop").show();
}
function ExapndSizerangeDeatils() {
    $("#sizeRangeTable").dialog({
        height: 140,
        modal: true
    });
    $("#sizeRangeTable").show();
}