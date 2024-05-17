
var uomvalue_Cone = 0;
$(document).ready(function () {
    $(".Indentno_Cls").multiselect();
    $(".Indentno_Cls").multiselect({
        noneSelectedText: "Please Select",
        classes: "selLocCsr"
    }); 
    $(function () {
        $("#StockValue").focus();
    })
    $("#IndentNoDirectPO_").hide();
    $('#StockValue').prop("disabled", true);
    $("#OrderClosedOn").datepicker({ dateFormat: "dd/mm/yy", maxDate: new Date() });
    $("#PoDate").datepicker({ dateFormat: "dd/mm/yy", maxDate: new Date() });
    $("#LastAmendmentDate").datepicker({ dateFormat: "dd/mm/yy", maxDate: new Date() });
    $("#ReqdDate").datepicker({ dateFormat: "dd/mm/yy" });
    $("#ETD").datepicker({ dateFormat: "dd/mm/yy", maxDate: new Date() });
    $("#ETA").datepicker({ dateFormat: "dd/mm/yy", maxDate: new Date() });
    $("#TickToCloseOrder").click(function () {
        if ($(this).prop("checked") == true) {
            $('#OrderClosedOn').prop("disabled", false);
        }
        else {
            $('#OrderClosedOn').prop("disabled", true);
        }
    });
    $('#list-amended-material').on('click', 'tr', function (event) {  
        var MaterialCategoryMasterId = $(this).closest('tr').find('.MaterialCategoryMasterId').text();
        var MaterialGroupMasterId = $(this).closest('tr').find('.MaterialGroupMasterId').text();
        var ColorMasterId = $(this).closest('tr').find('.ColorMasterId').text();
        var SubstanceMasterId = $(this).closest('tr').find('.SubstanceMasterId').text();
        var UomMasterId = $(this).closest('tr').find('.UomMasterId').text();
        var SupplierId = $(this).closest('tr').find('.SupplierId').text();
        var BuyerMasterId = $(this).closest('tr').find('.BuyerMasterId').text();
        var Price = $(this).closest('tr').find('.Price').text();
        var IndentQTY = $(this).closest('tr').find('.IndentQTY').text();
        var RequiredQty = $(this).closest('tr').find('.RequiredQty').text();
        var MaterialMasterID = $(this).closest('tr').find('.MaterialMasterID').text();
        var SizeRangeMasterID = $(this).closest('tr').find('.SizeRangeMasterID').text();
        
    });   
    $(".numeric").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        $(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    $(".numeric").bind("paste", function (e) {
        return false;
    });
    $(".numeric").bind("drop", function (e) {
        return false;
    });
    $('.allow_decimal').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
           ((event.which < 48 || event.which > 57) &&
           (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 2) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });
    $('.allow_decimal').bind("paste", function (e) {
        var text = e.originalEvent.clipboardData.getData('Text');
        if ($.isNumeric(text)) {
            if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
                e.preventDefault();
                $(this).val(text.substring(0, text.indexOf('.') + 3));
            }
        }
        else {
            e.preventDefault();
        }
    });
    $(function () {
        $(".input-group-addon").click(function () {

        });

        $('#Dis').blur(function () {
            var qty = $('#Dis').val();
            var exrate = $('#ExRate').val();
            var Value = ((parseFloat(qty) / 100) * parseFloat(exrate));
            var Value_ = (Value).toFixed(2);
            $('#DisAmount').val(Value_);
            var rate_ = parseFloat(exrate);
            var ExciseDutyAmount = $('#ExciseDutyAmount').val();
            var ExessAmount = $('#ExessAmount').val();
            var SHExessAmount = $('#SHExessAmount').val();
            var VATAmt = 0;
            var SurChargeAmt = $('#SurChargeAmt').val();
            if (ExciseDutyAmount == "") {
                ExciseDutyAmount = 0;
            }
            if (ExessAmount == "") {
                ExessAmount = 0;
            }
            if (SHExessAmount == "") {
                SHExessAmount = 0;
            }
            if (VATAmt == "") {
                VATAmt = 0;
            }
            if (SurChargeAmt == "") {
                SurChargeAmt = 0;
            }
            var amount = ((parseFloat(rate_) + parseFloat(ExciseDutyAmount) + parseFloat(ExessAmount) + parseFloat(SHExessAmount) + parseFloat(VATAmt) + parseFloat(SurChargeAmt)) - parseFloat(Value_));
            var freightTotal = $("#FreightCostTotal").val();
            if (freightTotal == "") {
                freightTotal = 0;
            }

            var GSTPer = $('#VAT').val();
            var Value = ((parseFloat(GSTPer) / 100) * parseFloat(amount));
            $('#VATAmt').val(Value.toFixed(3));



            $('#AmountTax').val(Math.round(parseFloat(amount) + parseFloat(freightTotal) + parseFloat(Value)));
        });
        $('#Exess').change(function () {

            var qty = $('#Exess').val();
            var exrate = $('#ExRate').val();
            var Value = ((parseFloat(qty) / 100) * parseFloat(exrate));
            var Value_ = (Value).toFixed(2);
            $('#ExessAmount').val(Value_);
            var rate_ = parseFloat(exrate);
            var ExciseDutyAmount = $('#ExciseDutyAmount').val();
            var DisAmount = $('#DisAmount').val();
            var SHExessAmount = $('#SHExessAmount').val();
            var VATAmt = $('#VATAmt').val();
            var SurChargeAmt = $('#SurChargeAmt').val();
            if (DisAmount == "") {
                DisAmount = 0;
            }
            if (ExciseDutyAmount == "") {
                ExciseDutyAmount = 0;
            }

            if (SHExessAmount == "") {
                SHExessAmount = 0;
            }
            if (VATAmt == "") {
                VATAmt = 0;
            }
            if (SurChargeAmt == "") {
                SurChargeAmt = 0;
            }
            var amount = ((parseFloat(Value_) + parseFloat(rate_) + parseFloat(ExciseDutyAmount) + parseFloat(SHExessAmount) + parseFloat(VATAmt) + parseFloat(SurChargeAmt)) - parseFloat(DisAmount));

            var freightTotal = $("#FreightCostTotal").val();
            if (freightTotal == "") {
                freightTotal = 0;
            }
            $('#AmountTax').val(Math.round(parseFloat(amount) + parseFloat(freightTotal)));

        });
        $('#ExciseDuty').change(function () {
            debugger;
            var qty = $('#ExciseDuty').val();
            var exrate = $('#ExRate').val();
            var Value = ((parseFloat(qty) / 100) * parseFloat(exrate));
            var Value_ = (Value).toFixed(2);
            $('#ExciseDutyAmount').val(Value_);
            var rate_ = parseFloat(exrate);
            var DisAmount = $('#DisAmount').val();
            var VATAmt = $('#VATAmt').val();
            var ExessAmount = $('#ExessAmount').val();
            var SHExessAmount = $('#SHExessAmount').val();
            var SurChargeAmt = $('#SurChargeAmt').val();

            if (DisAmount == "") {
                DisAmount = 0;
            }
            if (ExciseDutyAmount == "") {
                ExciseDutyAmount = 0;
            }
            if (ExessAmount == "") {
                ExessAmount = 0;
            }
            if (SHExessAmount == "") {
                SHExessAmount = 0;
            }
            if (SurChargeAmt == "") {
                SurChargeAmt = 0;
            }
            if (VATAmt == "") {
                VATAmt = 0;
            }

            var amount = ((parseFloat(Value_) + parseFloat(rate_) + parseFloat(VATAmt) + parseFloat(ExessAmount) + parseFloat(SHExessAmount) + parseFloat(SurChargeAmt)) - parseFloat(DisAmount));

            var freightTotal = $("#FreightCostTotal").val();
            if (freightTotal == "") {
                freightTotal = 0;
            }
            $('#AmountTax').val(Math.round(parseFloat(amount) + parseFloat(freightTotal)));
        });
        $('#SHExess').change(function () {

            var qty = $('#SHExess').val();
            var exrate = $('#ExRate').val();
            var Value = ((parseFloat(qty) / 100) * parseFloat(exrate));
            var Value_ = (Value).toFixed(2);
            $('#SHExessAmount').val(Value_);
            var rate_ = parseFloat(exrate);
            var DisAmount = $('#DisAmount').val();
            var ExciseDutyAmount = $('#ExciseDutyAmount').val();
            var ExessAmount = $('#ExessAmount').val();
            var SHExessAmount = $('#SHExessAmount').val();
            var VATAmt = $('#VATAmt').val();
            var SurChargeAmt = $('#SurChargeAmt').val();
            if (DisAmount == "") {
                DisAmount = 0;
            }
            if (ExciseDutyAmount == "") {
                ExciseDutyAmount = 0;
            }
            if (ExessAmount == "") {
                ExessAmount = 0;
            }
            if (SHExessAmount == "") {
                SHExessAmount = 0;
            }
            if (VATAmt == "") {
                VATAmt = 0;
            }
            if (SurChargeAmt == "") {
                SurChargeAmt = 0;
            }
            var amount = ((parseFloat(Value_) + parseFloat(rate_) + parseFloat(ExciseDutyAmount) + parseFloat(ExessAmount) + parseFloat(VATAmt) + parseFloat(SurChargeAmt)) - parseFloat(DisAmount));

            var freightTotal = $("#FreightCostTotal").val();
            if (freightTotal == "") {
                freightTotal = 0;
            }
            $('#AmountTax').val(Math.round(parseFloat(amount) + parseFloat(freightTotal)));
        });
        $('#SurCharge').change(function () {

            var qty = $('#SurCharge').val();
            var exrate = $('#ExRate').val();
            var Value = ((parseFloat(qty) / 100) * parseFloat(exrate));
            var Value_ = (Value).toFixed(2);
            $('#SurChargeAmt').val(Value_);
            var rate_ = parseFloat(exrate);
            var DisAmount = $('#DisAmount').val();
            var ExciseDutyAmount = $('#ExciseDutyAmount').val();
            var ExessAmount = $('#ExessAmount').val();
            var SHExessAmount = $('#SHExessAmount').val();
            var VATAmt = $('#VATAmt').val();

            if (DisAmount == "") {
                DisAmount = 0;
            }
            if (ExciseDutyAmount == "") {
                ExciseDutyAmount = 0;
            }
            if (ExessAmount == "") {
                ExessAmount = 0;
            }
            if (SHExessAmount == "") {
                SHExessAmount = 0;
            }
            if (VATAmt == "") {
                VATAmt = 0;
            }

            var amount = ((parseFloat(Value_) + parseFloat(rate_) + parseFloat(ExciseDutyAmount) + parseFloat(ExessAmount) + parseFloat(SHExessAmount) + parseFloat(VATAmt)) - parseFloat(DisAmount));

            var freightTotal = $("#FreightCostTotal").val();
            if (freightTotal == "") {
                freightTotal = 0;
            }
            $('#AmountTax').val(Math.round(parseFloat(amount) + parseFloat(freightTotal)));
        });
        $('#VAT').change(function () {

            var qty = $('#VAT').val();
            //var exrate = $('#AmountTax').val();
            var exrate = $('#ExRate').val();
            var ExciseDutyAmount = $('#ExciseDutyAmount').val();
            var Value = ((parseFloat(qty) / 100) * (parseFloat(exrate) + parseFloat(ExciseDutyAmount)));
            var Value = ((parseFloat(qty) / 100) * (parseFloat(exrate)));
            var Value_ = (Value).toFixed(2);
            $('#VATAmt').val(Value_);
            var rate_ = parseFloat(exrate);
            var DisAmount = 0;
            var ExessAmount = $('#ExessAmount').val();
            var SHExessAmount = $('#SHExessAmount').val();
            var SurChargeAmt = $('#SurChargeAmt').val();
            if (DisAmount == "") {
                DisAmount = 0;
            }
            if (ExciseDutyAmount == "") {
                ExciseDutyAmount = 0;
                $('#ExciseDutyAmount').val(0);
            }
            if (ExessAmount == "") {
                ExessAmount = 0;
                $('#ExessAmount').val(0);
            }
            if (SHExessAmount == "") {
                SHExessAmount = 0;
                $('#SHExessAmount').val(0);
            }
            if (SurChargeAmt == "") {
                SurChargeAmt = 0;
                $('#SurChargeAmt').val(0);
            }
            var amount = ((parseFloat(Value_) + parseFloat(rate_) + parseFloat(ExciseDutyAmount) + parseFloat(ExessAmount) + parseFloat(SHExessAmount) + parseFloat(SurChargeAmt)) - parseFloat(DisAmount));

            var freightTotal = $("#FreightCostTotal").val();
            if (freightTotal == "") {
                freightTotal = 0;
            }
            $('#AmountTax').val(Math.round(parseFloat(amount) + parseFloat(freightTotal)));
        });
    });
    $('#listep2').click(function () {
        $('#TableMaterials').hide();
    });
    $('#listep3').click(function () {
        $('#TableMaterials').hide();
    });
    $('#listep1').click(function () {
        $('#TableMaterials').show();
    });   
    $(function () {
        $("#ServiceTaxPer").blur(function () {
            var packForwardvalue = $("#PackForward").val();
            var serviceTaxPer = $("#ServiceTaxPer").val();
            if (packForwardvalue == "" || packForwardvalue == undefined) {
                packForwardvalue = 0;
            }
            if (serviceTaxPer == "" || serviceTaxPer == undefined) {
                serviceTaxPer = 0;
            }
            var ServiceTaxtotal = (packForwardvalue / 100) * serviceTaxPer;
            if (ServiceTaxtotal == "" || ServiceTaxtotal == undefined) {
                ServiceTaxtotal = 0;
            }
            $("#ServiceTaxNumbner").val(ServiceTaxtotal);
            var FreightAmt = $("#FreightAmt").val();
            var InsuranceAmount = $("#InsuranceAmount").val();
            var CustomsDutyType = $("#CustomsDutyType").val();
            var PackForward = $("#PackForward").val();
            var ServiceTaxNumbner = $("#ServiceTaxNumbner").val();
            if (FreightAmt == "" || FreightAmt == undefined) {
                FreightAmt = 0;
            }
            if (InsuranceAmount == "" || InsuranceAmount == undefined) {
                InsuranceAmount = 0;
            }
            if (CustomsDutyType == "" || CustomsDutyType == undefined) {
                CustomsDutyType = 0;
            }
            if (PackForward == "" || PackForward == undefined) {
                PackForward = 0;
            }
            if (ServiceTaxNumbner == "" || ServiceTaxNumbner == undefined) {
                ServiceTaxNumbner = 0;
            }
            var ServiceTaxtotal = parseFloat(ServiceTaxtotal);
            var FreightCostTotal = 0;
            FreightCostTotal = parseFloat(PackForward) + parseFloat(CustomsDutyType) + parseFloat(InsuranceAmount) + parseFloat(FreightAmt) + parseFloat(ServiceTaxNumbner);

            var amountTax = 0;
            amountTax = $("#AmountTax").val();
            if (amountTax == "") {
                amountTax = 0;
            }
            $("#FreightCostTotal").val(FreightCostTotal);
            var total = $("#FreightCostTotal").val();
            if (total == "" || total == undefined) {
                total = 0;
            }
            var DisAmount = 0;
            var ExciseDutyAmount = 0;
            var ExessAmount = 0;
            var SHExessAmount = 0;
            var VATAmt = 0;
            var SurChargeAmt = 0;
            if ($("#DisAmount").val() == "") {
                DisAmount = 0;
                $("#DisAmount").val(0);
            }
            if ($("#ExciseDutyAmount").val() == "") {
                ExciseDutyAmount = 0;
                $("#ExciseDutyAmount").val(0);
            }
            if ($("#ExessAmount").val() == "") {
                ExessAmount = 0;
                $("#ExessAmount").val(0);
            }
            if ($("#SHExessAmount").val() == "") {
                SHExessAmount = 0;
                $("#SHExessAmount").val(0);
            }
            if ($("#VATAmt").val() == "") {
                VATAmt = 0;
                $("#VATAmt").val(0);
            }
            if ($("#SurChargeAmt") == "") {
                SurChargeAmt = 0;
            }

            var qty_Value = 0;
            qty_Value = $("#ExRate").val();
            if (qty_Value == "") {
                qty_Value = 0;
            }
            else {
                qty_Value = qty_Value;
            }
            var Amount_Tax = ((parseFloat(ExciseDutyAmount) + parseFloat(ExessAmount) + parseFloat(SHExessAmount) + parseFloat(VATAmt) + parseFloat(SurChargeAmt)) - parseFloat(DisAmount));
            $("#AmountTax").val(Math.round(parseFloat(Amount_Tax) + parseFloat(total) + parseFloat(qty_Value)));
        });
    });
    $(function () {
        $("#FreightAmt").blur(function () {
            var total = calulationtotal();
            $("#AmountTax").val(total);

        });
    });
    $(function () {
        $("#InsuranceAmount").blur(function () {
            var total = calulationtotal();
            $("#AmountTax").val(total);
        });
    });
    $(function () {
        $("#CustomsDutyType").blur(function () {
            var total = calulationtotal();
            $("#AmountTax").val(total);
        });
    });
    $(function () {
        $("#PackForward").blur(function () {
            var total = calulationtotal();
            $("#AmountTax").val(total);
        });
    });
    $(function () {
        $('#OrderType').change(function () {
            if ($('#OrderType option:selected').text() == "General") {
                $("#Qty").prop("disabled", true);
                $.ajax({
                    url: '/Indent/GetLastIndentNO',
                    type: "GET",
                    dataType: "JSON",
                    success: function (data) {
                        $("#IndentNoDirectPO_").hide();
                        $("#IndentNo_").show();
                    },
                    error: function () {

                    }
                });
            }
            else if ($('#OrderType option:selected').text() == "Direct Po") {
                $("#Qty").prop("disabled", false);
                $("#IndentNoDirectPO_").show();
                $("#IndentNo_").hide();
            }
            else {
                $("#IndentNoDirectPO_").hide();
                $("#IndentNo_").show();
            }
        });
    });
    $(function () {
        $("#IONO").combobox();
    })

    if ('@Model.Edit' == "True") {

        $('#Grid').hide();
        $('#Next').hide();
        $('#back').show();
        $('#NextBtn').show();
        $('#backbtn').hide();

    }
    else {

        $('#Append').hide();
        $('#Grid').show();
    }
    $(".numericDecimal").keyup(function () {
        var $this = $(this);
        $this.val($this.val().replace(/[^\d.]/g, ''));
    });
    $(".numeric").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        $(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    $(".numeric").bind("paste", function (e) {
        return false;
    });
    $(".numeric").bind("drop", function (e) {
        return false;
    });
    var specialKeys = new Array();
    specialKeys.push(8);
    $(function () {

        $("#Store").combobox({
            select: function (event, ui) {
                var IndentNo = $('#IndentNo').val();
                var supplierid_ = $('#Supplier').val();
                var Storeid = $("#Store").val();
                var Category = $("#Category").val();
                if (supplierid_ == "") {
                    supplierid_ = 0;
                }
                if (Category == "") {
                    Category = 0;
                }
                $.ajax({
                    url: '/PurchaseOrder/GetCategoryWithMaterials',
                    type: "GET",
                    dataType: "JSON",
                    data: {
                        Indentid: IndentNo,
                        Supplierid: supplierid_,
                        Storeid: Storeid,
                        Category: Category
                    },
                    success: function (data) {
                        if (data.pono != "") {
                            $("#PoNo").val(data.pono);
                        }

                        var row = "";
                        $("#list-amended-material tbody").html("");
                        $.each(data.indent, function (i, item) {
                            if (item.SubstanceRange == null) {
                                item.SubstanceRange = "";
                            }
                            if (item.Color == null) {
                                item.Color = "";
                            }
                            if (item.RequiredQty == null) {
                                item.RequiredQty = "";
                            }
                            if (item.SampleNorms == null) {
                                item.SampleNorms = "";
                            }
                            if (item.Price == null) {
                                item.Price = "0";
                            }
                            $("#RemoveHead").css("display", "block");
                            $("#EditHead").css("display", "block");
                            row += "<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"
                            row += "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                            row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ")' class='' />" + "</td>"
                            if (item.IsPo == true) {
                                row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                            }
                            else {
                                row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO RAISE PO' class='' />" + "</td>"
                            }
                            row += "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                            row += "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                            row += "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                            row += "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                            row += "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                            row += "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                            row += "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                            row += "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                            row += "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                            row += "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                            row += "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                            row += "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                            row += "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                            row += "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                            row += "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                            row += "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                            row += "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"
                            row += "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                            row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                            row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"
                            row += "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                            row += "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>";
                        });
                        $("#list-amended-material tbody").append(row);
                    }
                });
            }
        });
    });
    $(function () {

        $("#Category").combobox({
            select: function (event, ui) {
                var MaterialCategoryMasterId_ = $('#Category').val();
                $.ajax({
                    url: '/MaterialMaster/FillCateogoryid',
                    type: "GET",
                    dataType: "JSON",
                    data: { MaterialCategoryMasterId: MaterialCategoryMasterId_ },
                    success: function (cities) {
                        $("#Groupname").html(""); // clear before appending new list
                        $.each(cities, function (i, city) {
                            $("#Groupname").append(
                                $('<option></option>').val(city.MaterialGroupMasterId).html(city.GroupName));
                        });
                    }
                });

                var IndentNo = $('#IndentNo').val();
                var supplierid_ = $('#Supplier').val();
                var Category = $("#Category").val();
                if (supplierid_ == "") {
                    supplierid_ = 0;
                }
                $.ajax({
                    url: '/PurchaseOrder/GetCategoryWithMaterials',
                    type: "GET",
                    dataType: "JSON",
                    data: {
                        Indentid: IndentNo,
                        Supplierid: supplierid_,
                        Category: Category
                    },
                    success: function (data) {
                        if (data.pono != "") {
                            $("#PoNo").val(data.pono);
                        }

                        $("#list-amended-material tbody").html("");
                        $.each(data.indent, function (i, item) {
                            $("#RemoveHead").css("display", "block");
                            $("#EditHead").css("display", "block");
                            $("#list-amended-material tbody").append("<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"

                        + "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                        + "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ")' class='' />" + "</td>"
                        + "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                        + "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                        + "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                        + "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                        + "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                        + "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                        + "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                        + "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"

                        + "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                          + "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                        + "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                          + "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                            + "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                        + "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                        + "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                        + "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                        + "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"

                            + "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                            + "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                            + "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"

                                + "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                            + "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>");
                        });
                    }
                });
            }
        });
    });
    $(function () {
        $("#Groupname").combobox({
            select: function (event, ui) {
                var MaterialGroupMasterId_ = $('#Groupname').val();
                $.ajax({
                    url: '/MaterialMaster/FillMaterialName',
                    type: "GET",
                    dataType: "JSON",
                    data: { MaterialGroupMasterId: MaterialGroupMasterId_ },
                    success: function (cities) {
                        $("#Material").html(""); // clear before appending new list
                        $.each(cities.Material, function (i, city) {
                            $("#Material").append(
                                $('<option></option>').val(city.Value).html(city.text));
                        });
                    }
                });


                var IndentNo = $('#IndentNo').val();
                var supplierid_ = $('#Supplier').val();
                if (supplierid_ == "") {
                    supplierid_ = 0;
                }
                $.ajax({
                    url: '/PurchaseOrder/GetGroupWithMaterials',
                    type: "GET",
                    dataType: "JSON",
                    data: {
                        Indentid: IndentNo,
                        Supplierid: supplierid_,
                        MaterialGroupMasterId: MaterialGroupMasterId_
                    },
                    success: function (data) {
                        if (data.pono != "") {
                            $("#PoNo").val(data.pono);
                        }

                        $("#list-amended-material tbody").html("");
                        $.each(data.indent, function (i, item) {
                            $("#RemoveHead").css("display", "block");
                            $("#EditHead").css("display", "block");
                            $("#list-amended-material tbody").append("<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"

                        + "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                        + "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ")' class='' />" + "</td>"
                        + "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                        + "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                        + "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                        + "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                        + "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                        + "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                        + "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                        + "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"

                        + "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                          + "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                        + "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                          + "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                            + "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                        + "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                        + "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                        + "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                        + "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"

                            + "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                            + "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                            + "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"

                                + "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                            + "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>");
                        });
                    }
                });
            }
        });
    });
    $(function () {
        $("#Material").combobox({

            select: function () {

                debugger;
                if ($("#OrderType option:selected").text() != "Direct Po") {
                    var MaterialNameId = $('#Material').val();
                    var SupplierNameId = $('#Supplier').val();
                    $("#Material").append("<option value='0'>Please Select </option>");
                    $.ajax({
                        url: '/MaterialMaster/PurchaseOrderFillMaterialNameBasedonColor',
                        type: "GET",
                        dataType: "JSON",
                        data: { MaterialNameID: MaterialNameId, SupplierNameId: SupplierNameId },
                        success: function (cities) {
                            if (cities.approvedPrice == null || cities.approvedPrice.length <= 0) {
                                alert("There is no data on approved price list.Pleaes make a entry!");
                                $("#save").hide();
                                return false;
                            }
                            else {
                                $("#save").show();
                                if (cities.store != null) {
                                    if (cities.store.StoreName == "Maintenance Store") {
                                        $("#MachinaryMaterialID").prop("disabled", false);
                                    }
                                    else {
                                        $("#MachinaryMaterialID").prop("disabled", true);
                                    }
                                }
                                $("#Supplier").html("");
                                $("#Supplier").append("<option value='0'>--Please Select--</option>");
                                $.each(cities.approvedPrice, function (i, approved) {
                                    $("#Supplier").append('<option value=' + approved.SupplierId + '>' + approved.SupplierDescription + '</option>');
                                });
                                if (cities.company != null) {
                                    $("#SupplierName").val(cities.company.SupplierName);
                                    $("#City").val(cities.company.City);
                                    $("#Address").val(cities.company.Address);
                                    $("#DeliverTerms").val(cities.company.DeliverTerms);
                                    $("#TermsConditions").val(cities.company.TermsConditions);
                                    $("#CompanyName").val(cities.company.CompanyName);
                                    $("#Phone").val(cities.company.Phone);
                                    $("#PaymentTerms").val(cities.company.PaymentTerms);
                                    $("#OtherTerms").val(cities.company.OtherTerms);
                                }

                                $.each(cities.Material, function (i, city) {
                                    $('#Color').val(city.ColorMasterId);
                                    $('#UOM').val(city.Uom);
                                    $('#UOM').val(city.Uom);
                                    $('#Category').val(city.MaterialCategoryMasterId);
                                    $("#Category").combobox('destroy');
                                    $("#Category").combobox();
                                    $('#Groupname').val(city.MaterialGroupMasterId);
                                    $("#Groupname").combobox('destroy');
                                    $("#Groupname").combobox();
                                    $('#Substance').val(city.SubstanceMasterId);
                                    $('#Currency').val(city.CurrencyMasterId);
                                    $("#Currency").combobox('destroy');
                                    $("#Currency").combobox();
                                    $('#FreightType').val(city.CurrencyMasterId);
                                    $('#InsureanceCurrency').val(city.CurrencyMasterId);
                                    $('#CustomsDuty').val(city.CurrencyMasterId);
                                    $('#PackForwardtype').val(city.CurrencyMasterId);
                                    $('#ServiceTaxType').val(city.CurrencyMasterId);
                                    $('#UOM').val(city.PrimaryUnit);
                                    $('#UOMValue1').val(city.PurchasePacket);
                                    uomvalue_Cone = city.PurchasePacket;
                                    $('#UOMType').val(city.PacketUnit);
                                    if (city.SubstanceMasterId == null || city.SubstanceMasterId == 0) {
                                        $("#Weight").val(0);
                                        $("#Weight").attr("disabled", "disabled");
                                        $("#Substance").prop("disabled", true);
                                    }
                                    else {
                                        $("#Weight").val(0);
                                        $("#Weight").removeAttr("disabled");
                                        $("#Substance").prop("disabled", false);
                                    }
                                });
                                var count = 1;
                                var totalQty = 0;
                                $("#sizeRangeTable tbody").html("");
                                for (var i = 0; i <= cities.SizeRange.length - 1; i++) {
                                    $("#sizeRangeTable tbody").append("<tr><td class='sizeVal'>" + cities.SizeRange[i].SizeRange + "</td><td><input class='qty_Total' type='text'  id='qtyId" + cities.SizeRange[i].Qty + "' style='width:50px;' onchange='CalculateTotal(this)' /></td><td><input type='text' class='sizeRate' id='Rate" + cities.SizeRange[i].Rate + "' value='" + cities.SizeRange[i].Rate + "' style='width:50px;' /></td></tr>");
                                    count++;
                                    totalQty += parseInt(totalQty);
                                }
                                $("#TotalCount").val(totalQty);
                            }
                        }
                    });
                }
                else {
                    var MaterialNameId = $('#Material').val();
                    $("#Material").append("<option value='0'>Please Select </option>");
                  var Pono=  $("#PoNo").val();
                    $.ajax({
                        url: '/MaterialMaster/DirectPoPurchaseOrderFillMaterialNameBasedonColor',
                        type: "GET",
                        dataType: "JSON",
                        data: { MaterialNameID: MaterialNameId, PONO: Pono },
                        success: function (cities) {
                            if (cities.Message == "Already Exists")
                            {
                                alert("Material already exists.");
                                return false;
                            }
                            if (cities.approvedPrice == null || cities.approvedPrice.length <= 0) {
                                alert("There is no data on approved price list.Pleaes make a entry!");
                                $("#save").hide();
                                return false;
                            }
                            else {
                                $("#save").show();

                                $("#Supplier").val("");
                                $("#Supplier").html("");
                                $("#Supplier").append("<option value='0'>--Please Select--</option>");
                                $.each(cities.approvedPrice, function (i, approved) {
                                    $("#Supplier").append('<option value=' + approved.SupplierId + '>' + approved.SupplierDescription + '</option>');

                                });
                                $("#Supplier").combobox();

                                if (cities.store != null) {
                                    if (cities.store.StoreName == "Maintenance Store") {
                                        $("#MachinaryMaterialID").prop("disabled", false);
                                    }
                                    else {
                                        $("#MachinaryMaterialID").prop("disabled", true);
                                    }
                                }
                                if (cities.company != null) {
                                    $("#SupplierName").val(cities.company.SupplierName);
                                    $("#City").val(cities.company.City);
                                    $("#Address").val(cities.company.Address);
                                    $("#DeliverTerms").val(cities.company.DeliverTerms);
                                    $("#TermsConditions").val(cities.company.TermsConditions);
                                    $("#CompanyName").val(cities.company.CompanyName);
                                    $("#Phone").val(cities.company.Phone);
                                    $("#PaymentTerms").val(cities.company.PaymentTerms);
                                    $("#OtherTerms").val(cities.company.OtherTerms);
                                }

                                $("#sizeRangeTable tbody").html("");
                                $.each(cities.Material, function (i, city) {
                                    $('#Color').val(city.ColorMasterId);
                                    $('#UOM').val(city.Uom);
                                    $('#UOM').val(city.Uom);
                                    $('#Rate_').val(city.Price);
                                    $('#Category').val(city.MaterialCategoryMasterId);
                                    $("#Category").combobox('destroy');
                                    $("#Category").combobox();
                                    $('#Groupname').val(city.MaterialGroupMasterId);
                                    $("#Groupname").combobox('destroy');
                                    $("#Groupname").combobox();
                                    $('#Substance').val(city.SubstanceMasterId);
                                    $('#Currency').val(city.CurrencyMasterId);
                                    $("#Currency").combobox('destroy');
                                    $("#Currency").combobox();
                                    $('#FreightType').val(city.CurrencyMasterId);
                                    $('#InsureanceCurrency').val(city.CurrencyMasterId);
                                    $('#CustomsDuty').val(city.CurrencyMasterId);
                                    $('#PackForwardtype').val(city.CurrencyMasterId);
                                    $('#ServiceTaxType').val(city.CurrencyMasterId);
                                    $('#UOM').val(city.PrimaryUnit);
                                    $('#UOMValue1').val(city.PurchasePacket);
                                   uomvalue_Cone = city.PurchasePacket;
                                    $('#UOMType').val(city.PacketUnit);
                                    if (city.SubstanceMasterId == null || city.SubstanceMasterId == 0) {
                                        $("#Weight").val(0);
                                        $("#Weight").attr("disabled", "disabled");
                                        $("#Substance").prop("disabled", true);
                                    }
                                    else {
                                        $("#Weight").val(0);
                                        $("#Weight").removeAttr("disabled");
                                        $("#Substance").prop("disabled", false);
                                    }
                                });
                                var count = 1;
                                var totalQty = 0;
                                $("#sizeRangeTable tbody").html("");
                                for (var i = 0; i <= cities.SizeRange.length - 1; i++) {
                                    $("#sizeRangeTable tbody").append("<tr><td class='sizeVal'>" + cities.SizeRange[i].SizeRange + "</td><td><input class='qty_Total' type='text'  id='qtyId" + cities.SizeRange[i].Qty + "' style='width:50px;' onchange='CalculateTotal(this)' /></td><td><input type='text' class='sizeRate' id='Rate" + cities.SizeRange[i].Rate + "' value='" + cities.SizeRange[i].Rate + "' style='width:50px;' /></td></tr>");
                                    count++;
                                    totalQty += parseInt(totalQty);
                                }
                                $("#TotalCount").val(totalQty);
                            }
                        }
                    });
                }
            }
        });
    });
    $(function () {
        $("#Qty").change(function () {
            var uomVlaue1 = 0;
            var qty = 0;
            qty = $("#Qty").val();
            if (qty == "") {
                qty = 0;
            }
            var rate_ = $("#Rate_").val();
            var totalValue = 0;
            totalValue = parseFloat(qty) * parseFloat(rate_);
            if (totalValue == "") {
                totalValue = 0;
            }
            $("#ExRate").val(totalValue);
            $("#UOMValue").val(qty);
            $("#ExRate").val(Math.round(totalValue));
            $("#AmountTax").val(Math.round(totalValue));
            uomVlaue1 = uomvalue_Cone;
            if (uomVlaue1 == "") {
                uomVlaue1 = 0;
            }
            var total = Math.round(uomVlaue1 * qty);
            $("#UOMValue1").val(total);

        });
    });
    $(function () {
        $("#Supplier").combobox({
            select: function (event, ui) {
                debugger;
                if ($('#OrderType option:selected').text() == "Direct Po") {
                    if (($('#Supplier').val() == "" || $('#Supplier').val() == 0)) {
                        alert("Please select supplier name");
                        return false;
                    }
                    else if ($("#Material").val() == "" || $("#Material").val() == 0) {
                        $('#Supplier').val("");
                        alert("Please select material name");
                        return false;
                    }
                    else {
                        $.ajax({
                            url: '/GRN_Detail/GetApprovedPriceDetails',
                            type: "GET",
                            dataType: "JSON",
                            contentType: "application/json; charset=utf-8",
                            data: { SupplierID: $('#Supplier').val(), MaterialID: $("#Material").val() },
                            success: function (data) {
                                $("#Rate_").val(data.approvedPrieList.PriceRs);
                            },
                            error: function () {
                            }
                        });
                    }
                }
                if ($('#OrderType option:selected').text() == "Order") {
                    //var IndentNo = $('.Indentno_Cls').val();
                    var chkArray = [];
                    $(".chkProduct:checked").each(function () {
                        chkArray.push($(this).val());
                    });
                    /* we join the array separated by the comma */
                    var selected;
                    selected = chkArray.join(',');
                    var IndentNo = "";
                    IndentNo = selected;
                    var supplierid_ = $('#Supplier').val();
                    if (supplierid_ == "") {
                        supplierid_ = 0;
                    }
                    $.ajax({
                        url: '/PurchaseOrder/GetIndentMaterials_',
                        type: "GET",
                        dataType: "JSON",
                        data: { Indentid: IndentNo, Supplierid: supplierid_ },
                        success: function (data) {
                            debugger;
                            if (data.pono != "") {
                                $("#PoNo").val(data.pono);
                            }
                            var row = "";
                            $("#list-amended-material tbody").html("");
                            $.each(data.indent, function (i, item) {
                                if (item.SubstanceRange == null) {
                                    item.SubstanceRange = "";
                                }
                                if (item.Color == null) {
                                    item.Color = "";
                                }
                                if (item.RequiredQty == null) {
                                    item.RequiredQty = "";
                                }
                                if (item.SampleNorms == null) {
                                    item.SampleNorms = "";
                                }
                                $("#RemoveHead").css("display", "block");
                                $("#EditHead").css("display", "block");
                                row += "<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"
                                row += "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                                row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ")' class='' />" + "</td>"
                                if (item.IsPo == true) {
                                    row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                                }
                                else {
                                    row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO RAISE PO' class='' />" + "</td>"
                                }
                                row + "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                                row + "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                                row + "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                                row + "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                                row + "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                                row + "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                                row + "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                                row + "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                                row + "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                                row + "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                                row + "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                                row + "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                                row + "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                                row + "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                                row + "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                                row + "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                                row + "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"
                                row + "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                                row + "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                                row + "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"
                                row + "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                                row + "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>";
                            });
                            $("#list-amended-material tbody").append(row);
                        }
                    });
                }
                else if ($('#OrderType option:selected').text() == "General") {

                    if (($('#Supplier').val() == "" || $('#Supplier').val() == 0)) {
                        alert("Please select supplier name");
                        return false;
                    }
                    else if ($("#Material").val() == "" || $("#Material").val() == 0) {
                        $('#Supplier').val("");
                        alert("Please select material name");
                        return false;
                    }
                    else {
                        $.ajax({
                            url: '/GRN_Detail/GetApprovedPriceDetails',
                            type: "GET",
                            dataType: "JSON",
                            contentType: "application/json; charset=utf-8",
                            data: { SupplierID: $('#Supplier').val(), MaterialID: $("#Material").val() },
                            success: function (data) {
                                $("#Rate_").val(data.approvedPrieList.PriceRs);
                                var qty = 0;
                                qty = $("#Qty").val();
                                if (qty == "") {
                                    qty = 0;
                                }
                                var rate_ = $("#Rate_").val();
                                var totalValue = 0;
                                totalValue = parseFloat(qty) * parseFloat(rate_);
                                if (totalValue == "") {
                                    totalValue = 0;
                                }
                                $("#ExRate").val(totalValue.toFixed(2));
                                $("#AmountTax").val(totalValue.toFixed(2));
                            },
                            error: function () {

                            }
                        });
                    }
                }
            }
        });
    });
   
});

function isNumberKey(evt) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;

    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function RowClick(arg, arg1, arg2, arg3) {
    debugger;
    $('#list-amended-material tr#' + arg + '').remove()
    var pono = $('#PoNo').val();
    // var indentno = $('#IndentNo').val()   
    var supplier = $("#Supplier").val();
    debugger;
    var mrpSelectedText = ""; 
    var Indentid = "";  
    var chkArray = [];
    $(".chkProduct:checked").each(function () {
        chkArray.push($(this).val());
    });
    /* we join the array separated by the comma */
    var selected;
    selected = chkArray.join(',');
    var IndentNo = "";
    IndentNo = selected;
    Indentid = IndentNo;
    if ($('#OrderType option:selected').text() != "General") {
        $.ajax({
            type: 'POST',
            url: '/Indent/PurchaseOrderRowClickMaterialList',
            data: { IndentMaterialID: arg, BOMID: arg1, MRPNO: $('#MRPNO option:selected').text() },
            success: function (data) {
                if (data != null & data.Message != null) {
                    alert(data.Message);
                    return false;
                }
                else {
                    if (data.indent != null)
                    {
                        $('#IndentMaterialID').val(data.indent.IndentMaterialID)
                        $('#BOMMaterialID').val(data.indent.BOMMaterialID);
                        $('#BomNo').val(data.indent.BomNo);
                        $('#IndentNo').val(data.indent.IndentID);
                        $('#SeasonId').val(data.indent.SeasonId);
                        $('#BuyerMasterId').val(data.indent.BuyerMasterId);                       
                       // $('#Supplier').val(data.approvedPriceList.SupplierId);
                        $('#StoreStock').val(data.indent.StoreStock);
                        $('#SeasonId').val(data.indent.BuyerSeason);
                        $('#FreeStock').val(data.indent.FreeStock);
                        $('#ExRate').val(data.indent.Value);
                    }
                    $("#Supplier").html("");
                    $("#Supplier").append("<option value='0'>--Please Select--</option>");
                    $.each(data.approvedPriceList, function (i, approved) {
                        $("#Supplier").append('<option value=' + approved.SupplierId + '>' + approved.SupplierName + '</option>');
                    });
                    $('#Qty').val(arg3);
                    $('#IntendQty').val(arg2);
                    $('#BOMMaterialID').val(arg);
                    $('#BOMID').val(arg1);
                    $('#StockValue').val(data.stockTotal_);
                    $('#StockValue').prop("disabled", true);                    
                    $("#Supplier").combobox('destroy');
                    $("#Supplier").combobox();
                    if (data.approvedPriceList != null && data.approvedPriceList != undefined)
                    {
                        $('#Rate_').val(data.approvedPriceList.PriceRs);
                    }                  
                    if (data.InternalOrderEntryForm != null)
                    {
                        $('#IONO').val(data.InternalOrderEntryForm.OrderEntryId);
                        $("#IONO").combobox('destroy');
                        $("#IONO").combobox();
                    }   
                    $("#PoNo").val(pono);
                    $('#Substance').val(data.Material.SubstanceMasterId);
                    $('#Category').val(data.Material.MaterialCategoryMasterId);
                    $("#Category").combobox('destroy');
                    $("#Category").combobox();
                    $('#Groupname').val(data.Material.MaterialGroupMasterId);
                    $("#Groupname").combobox('destroy');
                    $("#Groupname").combobox();
                    $('#Material').val(data.Material.MaterialMasterId);
                    $("#Material").combobox('destroy');
                    $("#Material").combobox();
                    $('select[name=Color]').val(data.Material.ColorMasterId);
                    $('select[name=UOM]').val(data.Material.UomMasterId);
                    if (data.company != null) {
                        $("#SupplierName").val(data.approvedPriceList.SupplierName);
                        $("#City").val(data.company.City);
                        $("#Address").val(data.company.Address);
                        $("#DeliverTerms").val(data.company.DeliverTerms);
                        $("#TermsConditions").val(data.company.TermsConditions);
                        $("#CompanyName").val(data.company.CompanyName);
                        $("#Phone").val(data.company.Phone);
                        $("#PaymentTerms").val(data.company.PaymentTerms);
                        $("#OtherTerms").val(data.company.OtherTerms);
                    }
                    if (data.store != null && data.store.StoreName != null) {
                        if (data.store.StoreName == "Maintenance Store") {
                            $("#MachinaryMaterialID").prop("disabled", false);
                        }
                        else {
                            $("#MachinaryMaterialID").prop("disabled", true);
                        }
                    }

                    var d = new Date(),
         date1 = (d.getUTCDate() + '/' + (d.getUTCMonth() + 1) + '/' + d.getUTCFullYear());
                    $('#LastAmendmentDate').val(date1).find('.datepicker').datepicker();
                    $('#PoDate').val(date1).find('.datepicker').datepicker();
                    $('#ETD').val(date1).find('.datepicker').datepicker();
                    $('#ETA').val(date1).find('.datepicker').datepicker();
                }
            }
        });
    }
    else if ($('#OrderType option:selected').text() == "General" || $('#OrderType option:selected').text() == "Order") {
        $.ajax({
            type: 'POST',
            url: '/Indent/GeneralPurchaseOrderRowClickMaterialList',
            data: { IndentMaterialID: arg, BOMID: 0, IndentId: Indentid },
            success: function (data) {
                debugger;
                if (data != null & data.Message != null) {
                    alert(data.Message);
                    return false;
                }
                else if (data.approvedPriceList == null || data.approvedPriceList.length <= 0) {
                    alert("There is no data on approved price list.Pleaes make a entry!");
                    $("#save").hide();
                    return false;
                }
                else {
                    if (data.indent!=null&& data.indent.IsPo != true) {
                        $("#Supplier").html("");
                        $("#Supplier").append("<option value='0'>--Please Select--</option>");
                        $.each(data.approvedPriceList, function (i, approved) {
                            $("#Supplier").append('<option value=' + approved.SupplierId + '>' + approved.SupplierName + '</option>');
                        });
                    }
                    else {                       
                        $("#Supplier").html("");
                        $("#Supplier").append("<option value='0'>--Please Select--</option>");
                        $.each(data.approvedPriceList, function (i, approved) {
                            $("#Supplier").append('<option value=' + approved.SupplierId + '>' + approved.SupplierName + '</option>');
                        });
                    }
                    if (data.company != null) {
                        $("#SupplierName").val(data.company.SupplierName);
                        $("#City").val(data.company.City);
                        $("#Address").val(data.company.Address);
                        $("#DeliverTerms").val(data.company.DeliverTerms);
                        $("#TermsConditions").val(data.company.TermsConditions);
                        $("#CompanyName").val(data.company.CompanyName);
                        $("#Phone").val(data.company.Phone);
                        $("#PaymentTerms").val(data.company.PaymentTerms);
                        $("#OtherTerms").val(data.company.OtherTerms);
                    }
                    if (data.indent != null)
                    {
                        $('#IndentMaterialID').val(data.indent.IndentMaterialID)
                        $('#BOMMaterialID').val(data.indent.BOMMaterialID);
                        $('#BomNo').val(data.indent.BomNo);
                        $('#IndentNo').val(data.indent.IndentID);
                        $("#IndentNo").combobox('destroy');
                        $("#IndentNo").combobox();
                        $('#SeasonId').val(data.indent.SeasonId);
                        $('#BuyerMasterId').val(data.indent.BuyerMasterId);
                        $('#StoreStock').val(data.indent.StoreStock);
                        $('#SeasonId').val(data.indent.BuyerSeason);
                        $('#IONO').val(data.InternalOrderEntryForm.OrderEntryId);
                        $("#IONO").combobox('destroy');
                        $("#IONO").combobox();
                        $('#FreeStock').val(data.indent.FreeStock);
                        $('#ExRate').val(data.indent.Value);
                    }                 
                    $('#BOMMaterialID').val(arg);
                    $('#BOMID').val(arg1);
                    if (data.stockTotal_ == null)
                    {
                        data.stockTotal_ = 0;
                    }
                    $('#StockValue').val(data.stockTotal_);
                    $('#StockValue').prop("disabled", true);                    
                    $('#Qty').val(arg2);
                    $('#IntendQty').val(arg2);
                    if (data.approvedPriceList != null)
                    {
                        $('#Rate_').val(data.approvedPriceList.PriceRs);
                    }                 
                    $('#Qty').prop("disabled", true);
                    $('#IntendQty').prop("disabled", true);
                    $('#Rate_').prop("disabled", true);
                    $('#PendingQty').prop("disabled", true);                  
                    $("#PoNo").val(pono);
                    $('#Substance').val(data.Material.SubstanceMasterId);
                   // $('#Color').val(data.Material.ColorMasterId);
                    $('#Category').val(data.Material.MaterialCategoryMasterId);
                    $("#Category").combobox('destroy');
                    $("#Category").combobox();
                    $('#Groupname').val(data.Material.MaterialGroupMasterId);
                    $("#Groupname").combobox('destroy');
                    $("#Groupname").combobox();
                    $('#Store').val(data.Material.StoreMasterId);
                    $("#Store").combobox('destroy');
                    $("#Store").combobox();
                    $('select[name=Color]').val(data.Material.ColorMasterId);
                    $('#Material').val(data.Material.MaterialMasterId);
                    $("#Material").combobox('destroy');
                    $("#Material").combobox();                  
                    $('#Currency').val(data.Material.CurrencyMasterId);
                    $("#Currency").combobox('destroy');
                    $("#Currency").combobox();
                    $('#FreightType').val(data.Material.CurrencyMasterId);
                    $('#InsureanceCurrency').val(data.Material.CurrencyMasterId);
                    $('#CustomsDuty').val(data.Material.CurrencyMasterId);
                    $('#PackForwardtype').val(data.Material.CurrencyMasterId);
                    $('#ServiceTaxType').val(data.Material.CurrencyMasterId);
                    $('#UOM').val(data.Material.PrimaryUnit);
                    $('#UOMValue1').val(data.Material.PurchasePacket);
                    uomvalue_Cone = data.Material.PurchasePacket;
                    $("#UOMValue").val(arg2);
                    $('#UOMType').val(data.Material.PacketUnit);
                    if (data.Material.SubstanceMasterId == null || data.Material.SubstanceMasterId == 0) {
                        $("#Weight").val(0);
                        $("#Weight").attr("disabled", "disabled");
                        $("#Substance").prop("disabled", true);
                    }
                    else {
                        $("#Weight").val(0);
                        $("#Weight").removeAttr("disabled");
                        $("#Substance").prop("disabled", false);
                    }
                    if (data.store != null && data.store.StoreName != null) {
                        if (data.store.StoreName.trim() == "Maintenance Store") {
                            $("#MachinaryMaterialID").prop("disabled", false);
                        }
                        else {
                            $("#MachinaryMaterialID").prop("disabled", true);
                        }
                    }
                    var d = new Date(),
         date1 = (d.getUTCDate() + '/' + (d.getUTCMonth() + 1) + '/' + d.getUTCFullYear());
                    $('#LastAmendmentDate').val(date1).find('.datepicker').datepicker();
                    $('#PoDate').val(date1).find('.datepicker').datepicker();
                    $('#ETD').val(date1).find('.datepicker').datepicker();
                    $('#ETA').val(date1).find('.datepicker').datepicker();
                    $("#sizeRangeTable tbody").html("");
                    var count = 1;
                    var totalQty = 0;
                    for (var i = 0; i <= data.SizeRange.length - 1; i++) {
                        if (data.SizeRange[i].Rate == null) {
                            data.SizeRange[i].Rate = "";
                        }
                        $("#sizeRangeTable tbody").append("<tr><td class='sizeVal'>" + data.SizeRange[i].Size + "</td><td><input class='qty_Total' type='text'  id='qtyId" + data.SizeRange[i].quantity + "' value='" + data.SizeRange[i].quantity + "' style='width:50px;' onchange='CalculateTotal(this)' /></td><td><input type='text' class='sizeRate' id='Rate" + data.SizeRange[i].Rate + "' value='" + data.SizeRange[i].Rate + "' style='width:50px;' /></td></tr>");
                        count++;
                        totalQty += parseInt(data.SizeRange[i].quantity);
                    }
                    $('.sizeRate').prop('disabled', true);
                    $('.qty_Total').prop('disabled', true);
                    $("#TotalCount").text(totalQty);
                    if (data.purchaerOrder != null) {
                        $("#PoQty").val(data.purchaerOrder.PoQty);
                        $("#PendingQty").val(data.purchaerOrder.PendingQty);
                        $("#Rate_").val(data.purchaerOrder.Rate);
                        $("#AmountTax").val(data.purchaerOrder.AmountTax);
                        $("#VAT").val(data.purchaerOrder.VAT);
                        $("#VATAmt").val(data.purchaerOrder.VATAmt);
                        $("#Supplier").val(data.purchaerOrder.Supplier);
                        $("#Supplier").combobox('destroy');
                        $("#Supplier").combobox();
                        $("#TaxType").val(data.purchaerOrder.TaxType);
                        $("#PoType").val(data.purchaerOrder.PoType);
                        $("#InsuranceDetails").val(data.purchaerOrder.InsuranceDetails);
                        $("#RefInfo").val(data.purchaerOrder.RefInfo);
                        $("#Form").val(data.purchaerOrder.Form);
                        $("#FormName").val(data.purchaerOrder.FormName);
                    }
                }
            }
        });
    }
}
function PO_RowClick(arg) {
    debugger;

    $('#list-amended-material tr#' + arg + '').remove()
    var pono = $('#PoNo').val();
    var indentno = $('#IndentNo').val()
    var supplier = $("#Supplier").val();
    if ($('#OrderType option:selected').text() != "General") {
        $.ajax({
            type: 'GET',
            url: '/PurchaseOrder/GetPurchaseOrderDetails',
            data: { PoOrderID: arg },
            success: function (data) {
                if (data != null & data.Message != null) {
                    alert(data.Message);
                    return false;
                }
                else {
                    if (data.PurchaseOrder != null) {
                        $('#Supplier').val(data.PurchaseOrder.Supplier);
                        $("#Supplier").combobox('destroy');
                        $("#Supplier").combobox();
                    }
                    if (data.company != null) {
                        $("#SupplierName").val(data.company.SupplierName);
                        $("#City").val(data.company.City);
                        $("#Address").val(data.company.Address);
                        $("#DeliverTerms").val(data.company.DeliverTerms);
                        $("#TermsConditions").val(data.company.TermsConditions);
                        $("#CompanyName").val(data.company.CompanyName);
                        $("#Phone").val(data.company.Phone);
                        $("#PaymentTerms").val(data.company.PaymentTerms);
                        $("#OtherTerms").val(data.company.OtherTerms);
                    }
                    $('#IndentMaterialID').val(data.PurchaseOrder.IndentMaterialID)
                    $('#BOMMaterialID').val(data.indentMaterial.BOMMaterialID);
                    $('#BOMMaterialID').val(data.indentMaterial.BOMID);
                    $('#PoOrderId').val(arg);
                    $('#StockValue').val(data.PurchaseOrder.StockValue);
                    $('#StockValue').prop("disabled", true);
                    $('#MaterialType').val(data.PurchaseOrder.MaterialType);
                    $('#IndentNoDirectPO').val(data.PurchaseOrder.IndentNoDirectPO);
                    $("#IndentNoDirectPO_").show();
                    $("#IndentNo_").hide();
                    $('#SeasonId').val(data.indentMaterial.SeasonId);
                    $('#BuyerMasterId').val(data.indentMaterial.BuyerMasterId);
                    $('#Qty').val(data.PurchaseOrder.Qty);
                    $('#PoQty').val(data.PurchaseOrder.PoQty);
                    $('#PendingQty').val(data.PurchaseOrder.PendingQty);
                    $('#IntendQty').val(data.PurchaseOrder.Qty);
                    $('#Rate_').val(data.PurchaseOrder.Rate_);
                    $('#StoreStock').val(data.indentMaterial.StoreStock);
                    $('#SeasonId').val(data.indentMaterial.BuyerSeason);
                    $('#FreeStock').val(data.PurchaseOrder.FreeStock);
                    $('#ExRate').val(data.PurchaseOrder.ExRate);
                    $("#PoNo").val(pono);
                    $('#Substance').val(data.indentMaterial.SubstanceMasterId);
                    $('#TaxType').val(data.PurchaseOrder.TaxType);
                    $('#Category').val(data.PurchaseOrder.Category);
                    $("#Category").combobox('destroy');
                    $("#Category").combobox();
                    $('#Groupname').val(data.PurchaseOrder.Groupname);
                    $("#Groupname").combobox('destroy');
                    $("#Groupname").combobox();
                    $('#Material').val(data.PurchaseOrder.Material);
                    $("#Material").combobox('destroy');
                    $("#Material").combobox();
                    $('select[name=Color]').val(data.PurchaseOrder.Color);

                    $('#Currency').val(data.PurchaseOrder.Currency);
                    $("#Currency").combobox('destroy');
                    $("#Currency").combobox();
                    $('#FreightType').val(data.PurchaseOrder.FreightType);
                    $('#InsureanceCurrency').val(data.PurchaseOrder.InsureanceCurrency);
                    $('#CustomsDuty').val(data.PurchaseOrder.CustomsDuty);
                    $('#PackForwardtype').val(data.PurchaseOrder.PackForwardtype);
                    $('#ServiceTaxType').val(data.PurchaseOrder.ServiceTaxType);
                    $('#UOM').val(data.PurchaseOrder.UOM);
                    $('#UOMValue1').val(data.PurchaseOrder.UOMValue1);
                    $("#UOMValue").val(data.PurchaseOrder.UOMValue);
                    $('#UOMType').val(data.PurchaseOrder.UOMType);
                    $("#AmountTax").val(data.PurchaseOrder.AmountTax);
                    $("#Dis").val(data.PurchaseOrder.Dis);
                    $("#ExciseDuty").val(data.PurchaseOrder.ExciseDuty);
                    $("#Exess").val(data.PurchaseOrder.Exess);
                    $("#SHExess").val(data.PurchaseOrder.SHExess);
                    $("#VAT").val(data.PurchaseOrder.VAT);
                    $("#SurCharge").val(data.PurchaseOrder.SurCharge);
                    $("#DisAmount").val(data.PurchaseOrder.DisAmount);
                    $("#ExciseDutyAmount").val(data.PurchaseOrder.ExciseDutyAmount);
                    $("#ExessAmount").val(data.PurchaseOrder.ExessAmount);
                    $("#SHExessAmount").val(data.PurchaseOrder.SHExessAmount);
                    $("#VATAmt").val(data.PurchaseOrder.VATAmt);
                    $("#SurChargeAmt").val(data.PurchaseOrder.SurChargeAmt);
                    $("#MRPMargin").val(data.PurchaseOrder.MRPMargin);
                    $("#MRPPrice").val(data.PurchaseOrder.MRPPrice);
                    $("#AccessibleValue").val(data.PurchaseOrder.AccessibleValue);
                    $("#StockValue").val(data.PurchaseOrder.StockValue);
                    $("#FrightType").val(data.PurchaseOrder.FrightType);
                    $("#Remarks").val(data.PurchaseOrder.Remarks);
                    $("#IONO").val(data.PurchaseOrder.IONO);
                    $("#Weight").val(data.PurchaseOrder.Weight);
                    $("#OtherSpecification").val(data.PurchaseOrder.OtherSpecification);
                    $("#PoType").val(data.PurchaseOrder.PoType);
                    $("#Weight").val(data.PurchaseOrder.TaxType);
                    $("#FreightType").val(data.PurchaseOrder.FreightType);
                    $("#InsureanceCurrency").val(data.PurchaseOrder.InsureanceCurrency);
                    $("#CustomsDuty").val(data.PurchaseOrder.CustomsDuty);
                    $("#PackForwardtype").val(data.PurchaseOrder.PackForwardtype);
                    $("#FreightAmt").val(data.PurchaseOrder.FreightAmt);
                    $("#InsuranceAmount").val(data.PurchaseOrder.InsuranceAmount);
                    $("#CustomsDutyType").val(data.PurchaseOrder.CustomsDutyType);
                    $("#PackForward").val(data.PurchaseOrder.PackForward);
                    $("#ServiceTaxPer").val(data.PurchaseOrder.ServiceTaxPer);
                    $("#ServiceTaxType").val(data.PurchaseOrder.ServiceTaxType);
                    $("#ServiceTaxNumbner").val(data.PurchaseOrder.ServiceTaxNumbner);
                    $("#FreightCostTotal").val(data.PurchaseOrder.FreightCostTotal);
                    $("#FormName").val(data.PurchaseOrder.FormName);
                    $("#Form").val(data.PurchaseOrder.Form);
                    $("#RefInfo").val(data.PurchaseOrder.RefInfo);
                    $("#UnitId").val(data.PurchaseOrder.UnitId);
                    $("#OrderType").val(data.PurchaseOrder.OrderType);
                    $("#Currency").val(data.PurchaseOrder.Currency);
                    $("#InsuranceDetails").val(data.PurchaseOrder.InsuranceDetails);
                    if (data.Material.SubstanceMasterId == null || data.Material.SubstanceMasterId == 0) {
                        $("#Weight").val(0);
                        $("#Weight").attr("disabled", "disabled");
                        $("#Substance").prop("disabled", true);
                    }
                    else {
                        $("#Weight").val(0);
                        $("#Weight").removeAttr("disabled");
                        $("#Substance").prop("disabled", false);
                    }
                    if (data.store != null) {
                        if (data.store.StoreName.trim() == "Maintenance Store") {
                            $("#MachinaryMaterialID").prop("disabled", false);
                        }
                        else {
                            $("#MachinaryMaterialID").prop("disabled", true);
                        }
                    }
                    var d = new Date(),
         date1 = (d.getUTCDate() + '/' + (d.getUTCMonth() + 1) + '/' + d.getUTCFullYear());
                    $('#LastAmendmentDate').val(data.PurchaseOrder.LastAmendmentDate);
                    $('#PoDate').val(data.PurchaseOrder.PoDate);
                    $('#ETD').val(data.PurchaseOrder.ETD);
                    $('#ETA').val(data.PurchaseOrder.ETA);
                    $('#ReqdDate').val(data.PurchaseOrder.ReqdDate);
                    $("#sizeRangeTable tbody").html("");
                    var count = 1;
                    var totalQty = 0;
                    $("#sizeRangeTable tbody").html("");
                    var count = 1;
                    var totalQty = 0;
                    for (var i = 0; i <= data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList.length - 1; i++) {
                        if (data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate == null) {
                            data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate = "";
                        }
                        $("#sizeRangeTable tbody").append("<tr><td class='sizeVal'>" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Size + "</td><td><input class='qty_Total' type='text'  id='qtyId" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].quantity + "' value='" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].quantity + "' style='width:50px;' onchange='CalculateTotal(this)' /></td><td><input type='text' class='sizeRate' id='Rate" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate + "' value='" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate + "' style='width:50px;' /></td></tr>");
                        count++;
                        totalQty += parseInt(data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].quantity);
                    }
                    $('.sizeRate').prop('disabled', true);
                    $('.qty_Total').prop('disabled', true);
                    $("#TotalCount").text(totalQty);
                }
            }
        });
    }
    else if ($('#OrderType option:selected').text() == "General") {
        $.ajax({
            type: 'GET',
            url: '/PurchaseOrder/GetPurchaseOrderDetails',
            data: { PoOrderID: arg },
            success: function (data) {
                debugger;
                if (data != null & data.Message != null) {
                    alert(data.Message);
                    return false;
                }
                else {
                    $('#IndentMaterialID').val(data.PurchaseOrder.IndentMaterialID)
                    $('#BOMMaterialID').val(data.indentMaterial.BOMMaterialID);
                    $('#BOMMaterialID').val(data.indentMaterial.BOMID);
                    $('#PoOrderId').val(arg);
                    $('#StockValue').val(data.PurchaseOrder.StockValue);
                    $('#StockValue').prop("disabled", true);
                    if (data.company != null) {
                        $("#SupplierName").val(data.company.SupplierName);
                        $("#City").val(data.company.City);
                        $("#Address").val(data.company.Address);
                        $("#DeliverTerms").val(data.company.DeliverTerms);
                        $("#TermsConditions").val(data.company.TermsConditions);
                        $("#CompanyName").val(data.company.CompanyName);
                        $("#Phone").val(data.company.Phone);
                        $("#PaymentTerms").val(data.company.PaymentTerms);
                        $("#OtherTerms").val(data.company.OtherTerms);
                    }
                    var chkArray = [];
                    var array = data.PurchaseOrder.IndentNo.split(",");
                    for (var i = 0; i < array.length; i++) {
                        $(".chkProduct").each(function () {
                            if (array[i] == $(this).val()) {
                                $(this).prop('checked', true);
                            }
                        });
                    }
                   
                    $("#IndentNoDirectPO_").hide();
                    $("#IndentNo_").show();
                    $('#SeasonId').val(data.indentMaterial.SeasonId);
                    $('#BuyerMasterId').val(data.indentMaterial.BuyerMasterId);
                    $('#Qty').val(data.PurchaseOrder.Qty);
                    $('#PoQty').val(data.PurchaseOrder.PoQty);
                    $('#PendingQty').val(data.PurchaseOrder.PendingQty);
                    $('#IntendQty').val(data.PurchaseOrder.Qty);
                    $('#Rate_').val(data.PurchaseOrder.Rate_);
                    $('#Qty').prop("disabled", true);
                    $('#IntendQty').prop("disabled", true);
                    $('#Rate_').prop("disabled", true);
                    $('#PendingQty').prop("disabled", true);
                    $('#StoreStock').val(data.indentMaterial.StoreStock);
                    $('#SeasonId').val(data.indentMaterial.BuyerSeason);
                    $('#FreeStock').val(data.PurchaseOrder.FreeStock);
                    $('#ExRate').val(data.PurchaseOrder.ExRate);
                    $("#PoNo").val(pono);
                    $('#Substance').val(data.indentMaterial.SubstanceMasterId);
                    $('#TaxType').val(data.PurchaseOrder.TaxType);
                    $('#Category').val(data.PurchaseOrder.Category);
                    $("#Category").combobox('destroy');
                    $("#Category").combobox();
                    $('#Groupname').val(data.PurchaseOrder.Groupname);
                    $("#Groupname").combobox('destroy');
                    $("#Groupname").combobox();
                    $('#Supplier').val(data.PurchaseOrder.Supplier);
                    $("#Supplier").combobox('destroy');
                    $("#Supplier").combobox();
                    $('#Material').val(data.PurchaseOrder.Material);
                    $("#Material").combobox('destroy');
                    $("#Material").combobox();
                    $('select[name=Color]').val(data.PurchaseOrder.Color);
                    $('#Currency').val(data.PurchaseOrder.Currency);
                    $("#Currency").combobox('destroy');
                    $("#Currency").combobox();
                    $('#FreightType').val(data.PurchaseOrder.FreightType);
                    $('#InsureanceCurrency').val(data.PurchaseOrder.InsureanceCurrency);
                    $('#CustomsDuty').val(data.PurchaseOrder.CustomsDuty);
                    $('#PackForwardtype').val(data.PurchaseOrder.PackForwardtype);
                    $('#ServiceTaxType').val(data.PurchaseOrder.ServiceTaxType);
                    $('#UOM').val(data.PurchaseOrder.UOM);
                    $('#UOMValue1').val(data.PurchaseOrder.UOMValue1);
                    $("#UOMValue").val(data.PurchaseOrder.UOMValue);
                    $('#UOMType').val(data.PurchaseOrder.UOMType);
                    $("#AmountTax").val(data.PurchaseOrder.AmountTax);
                    $("#Dis").val(data.PurchaseOrder.Dis);
                    $("#ExciseDuty").val(data.PurchaseOrder.ExciseDuty);
                    $("#Exess").val(data.PurchaseOrder.Exess);
                    $("#SHExess").val(data.PurchaseOrder.SHExess);
                    $("#VAT").val(data.PurchaseOrder.VAT);
                    $("#SurCharge").val(data.PurchaseOrder.SurCharge);
                    $("#DisAmount").val(data.PurchaseOrder.DisAmount);
                    $("#ExciseDutyAmount").val(data.PurchaseOrder.ExciseDutyAmount);
                    $("#ExessAmount").val(data.PurchaseOrder.ExessAmount);
                    $("#SHExessAmount").val(data.PurchaseOrder.SHExessAmount);
                    $("#VATAmt").val(data.PurchaseOrder.VATAmt);
                    $("#SurChargeAmt").val(data.PurchaseOrder.SurChargeAmt);
                    $("#MRPMargin").val(data.PurchaseOrder.MRPMargin);
                    $("#MRPPrice").val(data.PurchaseOrder.MRPPrice);
                    $("#AccessibleValue").val(data.PurchaseOrder.AccessibleValue);
                    $("#StockValue").val(data.PurchaseOrder.StockValue);
                    $("#FrightType").val(data.PurchaseOrder.FrightType);
                    $("#Remarks").val(data.PurchaseOrder.Remarks);
                    $("#IONO").val(data.PurchaseOrder.IONO);
                    $("#Weight").val(data.PurchaseOrder.Weight);
                    $("#OtherSpecification").val(data.PurchaseOrder.OtherSpecification);
                    $("#PoType").val(data.PurchaseOrder.PoType);
                    $("#Weight").val(data.PurchaseOrder.TaxType);
                    $("#FreightType").val(data.PurchaseOrder.FreightType);
                    $("#InsureanceCurrency").val(data.PurchaseOrder.InsureanceCurrency);
                    $("#CustomsDuty").val(data.PurchaseOrder.CustomsDuty);
                    $("#PackForwardtype").val(data.PurchaseOrder.PackForwardtype);
                    $("#FreightAmt").val(data.PurchaseOrder.FreightAmt);
                    $("#InsuranceAmount").val(data.PurchaseOrder.InsuranceAmount);
                    $("#CustomsDutyType").val(data.PurchaseOrder.CustomsDutyType);
                    $("#PackForward").val(data.PurchaseOrder.PackForward);
                    $("#ServiceTaxPer").val(data.PurchaseOrder.ServiceTaxPer);
                    $("#ServiceTaxType").val(data.PurchaseOrder.ServiceTaxType);
                    $("#ServiceTaxNumbner").val(data.PurchaseOrder.ServiceTaxNumbner);
                    $("#FreightCostTotal").val(data.PurchaseOrder.FreightCostTotal);
                    $("#FormName").val(data.PurchaseOrder.FormName);
                    $("#Form").val(data.PurchaseOrder.Form);
                    $("#RefInfo").val(data.PurchaseOrder.RefInfo);
                    $("#UnitId").val(data.PurchaseOrder.UnitId);
                    $("#OrderType").val(data.PurchaseOrder.OrderType);
                    $("#Currency").val(data.PurchaseOrder.Currency);
                    $("#InsuranceDetails").val(data.PurchaseOrder.InsuranceDetails);
                    if (data.Material.SubstanceMasterId == null || data.Material.SubstanceMasterId == 0) {
                        $("#Weight").val(0);
                        $("#Weight").attr("disabled", "disabled");
                        $("#Substance").prop("disabled", true);
                    }
                    else {
                        $("#Weight").val(0);
                        $("#Weight").removeAttr("disabled");
                        $("#Substance").prop("disabled", false);
                    }
                    if (data.store != null && data.store.StoreName != null) {
                        if (data.store.StoreName.trim() == "Maintenance Store") {
                            $("#MachinaryMaterialID").prop("disabled", false);
                        }
                        else {
                            $("#MachinaryMaterialID").prop("disabled", true);
                        }
                    }
                    var d = new Date(),
         date1 = (d.getUTCDate() + '/' + (d.getUTCMonth() + 1) + '/' + d.getUTCFullYear());
                    $('#LastAmendmentDate').val(data.PurchaseOrder.LastAmendmentDate);
                    $('#PoDate').val(data.PurchaseOrder.PoDate);
                    $('#ETD').val(data.PurchaseOrder.ETD);
                    $('#ETA').val(data.PurchaseOrder.ETA);
                    $('#ReqdDate').val(data.PurchaseOrder.ReqdDate);
                    $("#sizeRangeTable tbody").html("");
                    var count = 1;
                    var totalQty = 0;
                    $("#sizeRangeTable tbody").html("");
                    var count = 1;
                    var totalQty = 0;
                    for (var i = 0; i <= data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList.length - 1; i++) {
                        if (data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate == null) {
                            data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate = "";
                        }
                        $("#sizeRangeTable tbody").append("<tr><td class='sizeVal'>" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Size + "</td><td><input class='qty_Total' type='text'  id='qtyId" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].quantity + "' value='" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].quantity + "' style='width:50px;' onchange='CalculateTotal(this)' /></td><td><input type='text' class='sizeRate' id='Rate" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate + "' value='" + data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].Rate + "' style='width:50px;' /></td></tr>");
                        count++;
                        totalQty += parseInt(data.PurchaseOrder.PurchaseOrderSizeRangeQuantityList[i].quantity);
                    }
                    $('.sizeRate').prop('disabled', true);
                    $('.qty_Total').prop('disabled', true);
                    $("#TotalCount").text(totalQty);
                }
            }
        });
    }

}
function HideShowEditDelete() {
    if ('@Update' == 'False' || '@Edit' == 'False') {
        $('#list-amended-material tbody tr').each(function () {
            $(this).find(".Edit").addClass('not-allowed').removeAttr('onclick');
        });
    }
    else {
        $('#list-amended-material  tbody tr').each(function () {
            // $(this).find(".Edit").removeClass('not-allowed').attr('onclick');
        });
    }
    if ('@Delete' == 'False') {
        $('#list-amended-material  tbody tr').each(function () {
            $(this).find(".Delete").addClass('not-allowed').removeAttr('onclick');
        });
    }
    else {
        $('#list-amended-material tbody tr').each(function () {
            // $(this).find(".Delete").removeClass('not-allowed').attr('onclick');
        });
    }
}
function CalculateTotal(agr1) {

    var buyerTotalcnt = 0;
    var add = 0;
    $("#sizeRangeTable .qty_Total").each(function () {
        add += Number($(this).val());
    });
    if ($('#TotalCount').val() == "") {
        buyerTotalcnt = 0;
    }
    else {
        buyerTotalcnt = $('#TotalCount').val();
    }
    $('#TotalCount').text(add);
}
function Delete(arg) {
    debugger
    var answer = confirm("Are you sure want to delete?");
    if (answer) {
        $.ajax({
            type: 'POST',
            url: '/PurchaseOrder/DirectPoDelete',
            data: { PoOrderId: arg },
            success: function (data) {
                if (data.Status == "Success") {
                    alert('Deleted Successfully.');
                    location.href = "/PurchaseOrder/PurchaseOrderDetails?PoOrderId=" + data.Id;
                    return false;
                }
                else {
                    alert('deletion failed.');
                }

            },
        });
    }
    else {
        return false;
    }
}
function Close() {
    var answer = confirm("Are you sure want to Close this Page?");
    if (answer) {
        location.href = "/PurchaseOrder/PurchaseOrder";

        return false;
    }
    else {
        return false;
    }
}
function Search() {
    $.ajax({
        type: "POST",
        url: ' @Url.Action("Search", "PurchaseOrder")',
        data: { filter: $('#SeaarchID').val() },
        success: function (data) {
            $(".veh-tablewrap").html(data);
            $(".veh-tablewrap").show();
            $('#DivShow').hide();
        }
    });
}
function Cancel() {
    var answer = confirm("Are you sure want to Cancel?");
    if (answer) {
        location.href = "/PurchaseOrder/PurchaseOrder";

        return false;
    }
    else {
        return false;
    }
}
function Save(obj) {
    Validation();
    if (Validation() != false) {
        var sizeRangeDeliveryArr = [];
        $('#list_SizeRange tbody tr').each(function () {
            sizeRangeDeliveryArr.push(
                    {
                        Sno: $(this).find('td:first').html(),
                        Material: $(this).find('.MaterialVal').val(),
                        quantity: $(this).find('.QtyVal').val(),
                        Date: $(this).find('.Date_Picker').val()
                    });

        });
        $('#SizeRangeDeliery').val(JSON.stringify(sizeRangeDeliveryArr));
        var sizeRangeArr = [];
        $('#sizeRangeTable tbody tr').each(function () {
            sizeRangeArr.push(
                    {
                        Size: $(this).find('td:first').html(),
                        quantity: $(this).find('input[type=text]').val(),
                        Rate: $(this).find(':last-child').text()
                    });

        });
        $('#StockValue').prop("disabled", false);
        var value = $('#StockValue').val().trim();
        $('#StockValue').prop("disabled", true);
        $('#SizeRangeDetails').val(JSON.stringify(sizeRangeArr));
        $("#Rate_").prop("disabled", false);
        $("#PendingQty").prop("disabled", false);
        $("#Qty").prop("disabled", false);

        var chkArray = [];
        $(".chkProduct:checked").each(function () {
            chkArray.push($(this).val());
        });
        /* we join the array separated by the comma */
        var selected;
        selected = chkArray.join(',');
        var IndentNo_ = "";
        IndentNo_ = selected;      
        $.ajax({
            type: 'POST',
            url: '/PurchaseOrder/PurchaseOrderSerialize',
            data: $("form").serialize() + '&=StockValue' + value + '&=FrightType' + $("#FrightType").val() + '&=UOMValue1' + $("#UOMValue1").val() + '&=UOMValue' + $("#UOMValue").val() + '&=PoNo' + $("#PoNo").val() + '&=OrderType' + $("#OrderType").val() + '&=PoOrderId' + $("#PoOrderId").val() + '&=MaterialType' + $("#MaterialType").val() + '&=Rate_' + $("#Rate_").val() + '&=PendingQty' + $("#PendingQty").val() + '&=Qty' + $("#Qty").val() + "&IndentNo=" + IndentNo_,

            success: function (data) {
                debugger
                $("#PoOrderId").val("");
                $("#Rate_").prop("disabled", true);
                if (data.orderType == "Direct Po") {
                    alert("Saved Successfully");
                    $("#Rate").val("");
                    var row = "";
                    $("#sizeRangeTable tbody").html("");
                    $('#SizeRangeDetails').val("");
                    $("#list-amended-material tbody").html("");
                    $.each(data.IndentMaterial, function (i, item) {
                        $("#RemoveHead").css("display", "block");
                        $("#EditHead").css("display", "block");
                        if (item.SubstanceName == null || item.SubstanceName == "undefined") {
                            item.SubstanceName = "";
                        }
                        row += "<tr id=" + item.PoOrderId + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.PoOrderId + "</td>"
                        row += "<td><input type='button' value='Remove' onclick='Delete(" + item.PoOrderId + ")' class='' />" + "</td>"
                        row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='PO_RowClick(" + item.PoOrderId + ")' class='' />" + "</td>"
                        if (item.IsPo == true) {
                            row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                        }
                        else {
                            row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO BE RAISE PO' class='' />" + "</td>"
                        }
                        row += "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.GroupName + "</td>"
                        row += "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.Color + "</td>"
                        row += "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceName + "</td>"
                        row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + "" + "</td>"
                        row += "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + "" + "</td>"
                        row += "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + "" + "</td>";
                        row += "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + "" + "</td>"
                        row += "<td id='MRPNO' style='' class='MRPNO' value=''>" + "" + "</td>"
                        row += "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                        row += "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                        row += "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceName + "</td>"
                        row += "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                        row += "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                        row += "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                        row += "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                        row += "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                        row += "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                        row += "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                        row += "<td id='IndentID' style='' class='IndentID' value=''>" + "" + "</td>"
                        row += "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + "" + "</td>"
                        row += "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + "" + "</td>"
                        row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + "" + "</td></tr>";

                    });
                    $("#sizeRangeTable tbody").html("");
                    $("#list-amended-material tbody").append(row);
                    HideShowEditDelete();
                    return false;
                }
                else if (data.Response == "This material is already PO is raised.") {
                    alert("This material is already PO is raised.");
                    return false;
                }
                else {
                    if (data.Response == "Saved Successfully") {
                        var row = "";
                        $("#list-amended-material tbody").html("");
                        $("#sizeRangeTable tbody").html("");
                        $.each(data.IndentMaterial, function (i, item) {
                            $("#RemoveHead").css("display", "block");
                            $("#EditHead").css("display", "block");
                            if (item.SubstanceRange == null) {
                                item.SubstanceRange = "";
                            }
                            if (item.SampleNorms == null) {
                                item.SampleNorms = "";
                            }
                            if (item.RequiredQty == null) {
                                item.RequiredQty = "";
                            }
                            if (item.Color == null) {
                                item.Color = "";
                            }
                            row += "<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"
                            row += "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                            row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ")' class='' />" + "</td>"
                            if (item.IsPo == true) {
                                row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                            }
                            else {
                                row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO RAISE PO' class='' />" + "</td>"
                            }
                            row += "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                            row += "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                            row += "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"
                            row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"
                            row += "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                            row += "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td>";
                            row += "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                            row += "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                            row += "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                            row += "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                            row += "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                            row += "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                            row += "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                            row += "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                            row += "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                            row += "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                            row += "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                            row += "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                            row += "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                            row += "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                            row += "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                            row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td></tr>";

                        });
                        $("#list-amended-material tbody").append(row);
                        $.each(data.SizeRangeQtyRate, function (i, item) {
                            $("#sizeRangeTable tbody").append("<tr class=''><td id='Size' class='Size' value=''>" + item.SizeRange + "</td>"
                                   + "<td class='width-141'><input type='text' id='quantity' class='quantity' value=" + item.Qty + "/></td>"
                                   + "<td class='width-91'><input type='text' id='Rate' class='Rate' value=" + item.Rate + " ></td></tr>");
                        });
                        HideShowEditDelete();
                        alert("Saved Successfully");
                        window.location.href = "PurchaseOrderDetails?id=" + data.purchaseOrder.PoNo;
                        return false;

                    }
                    else if (data.Response == "Alreday Exist") {
                        alert("Already Exist.");
                        return false;
                    }
                    else {
                        alert(data.Response);
                        return false;
                    }
                }

            },
            error: function (ex) {
                debugger;
                alert('Error Please contact Administrator.');
                return false;
            }
        });
        $("#PoQty").val("");
        $("#PendingQty").val("");
        $("#ExRate").val("");
        $("#Dis").val("0");
        $("#DisAmount").val("0");
        $("#ExciseDuty").val("0");
        $("#ExciseDuty").val("0");
        $("#ExciseDutyAmount").val("0");
        $("#Exess").val("0");
        $("#ExessAmount").val("0");
        $("#SHExess").val("0");
        $("#SHExessAmount").val("0");
        $("#VAT").val("0");
        $("#VATAmt").val("0");
        $("#SurCharge").val("0");
        $("#SurChargeAmt").val("0");
        $("#AmountTax").val("0");
        $("#Remarks").val("");
        $("#MRPMargin").val("0");
        $("#MRPPrice").val("0");
        $("#AccessibleValue").val("0");
        $("#StockValue").val("");
        $("#FrightType").val(0);
        $("#MachinaryMaterialID").val("");
        $("#UOMValue").val("");
        $("#UOM").val("");
        $("#UOMValue1").val("");
        $("#UOMType").val("");
        $("#Color").val("");
        $("#Qty").val("");
        $("#Rate_").val("");
        $("#Substance").val("");
        $("#Weight").val("0");
        var supplierid = $("#Supplier").val();
        $("#OtherSpecification").val("");
        $("#Qty").val("");
        $("#IndentNoDirectPO").val("");
        $(".custom-combobox-input").val("");
    }

}
function Validation() {

    if ($('#PoNo').val() == "") {
        alert("Please Enter PoNo.")
        $('#PoNo').css('border-color', 'red');
        $('#PoNo').focus();
        return false;
    }
    else {
        $('#PoNo').css('border-color', '');
    }
    if ($('#LastAmendmentDate').val() == "") {
        alert("Please Select Last Amendment Date.")
        $('#LastAmendmentDate').css('border-color', 'red');
        $('#LastAmendmentDate').focus();
        return false;
    }
    else {
        $('#LastAmendmentDate').css('border-color', '');
    }

    if ($('#PoDate').val() == "") {
        alert("Please Select Po Date.")
        $('#PoDate').css('border-color', 'red');
        $('#PoDate').focus();
        return false;
    }
    else {
        $('#PoDate').css('border-color', '');
    }
    if ($('#ETD').val() == "") {
        alert("Please Select Po Date.")
        $('#ETD').css('border-color', 'red');
        $('#ETD').focus();
        return false;
    }
    else {
        $('#ETD').css('border-color', '');
    }

    if ($('#ETA').val() == "") {
        alert("Please Select Po Date.")
        $('#ETA').css('border-color', 'red');
        $('#ETA').focus();
        return false;
    }
    else {
        $('#ETA').css('border-color', '');
    }
    if ($('#Currency').val() == "") {
        alert("Please Select Currency.")
        $('#Currency').css('border-color', 'red');
        $('#Currency').focus();
        return false;
    }
    else {
        $('#Currency').css('border-color', '');
    }

    if ($('#Supplier').val() == "") {
        alert("Please Select Supplier.")
        $('#Supplier').css('border-color', 'red');
        $('#Supplier').focus();
        return false;
    }
    else {
        $('#Supplier').css('border-color', '');
    }
    if ($('#TaxType').val() == "") {
        alert("Please Select Tax Type.")
        $('#TaxType').css('border-color', 'red');
        $('#TaxType').focus();
        return false;
    }
    else {
        $('#TaxType').css('border-color', '');
    }
    if ($('#UnitId').val() == "") {
        alert("Please Select Unit.")
        $('#UnitId').css('border-color', 'red');
        $('#UnitId').focus();
        return false;
    }
    else {
        $('#UnitId').css('border-color', '');
    }
    if ($('#PoType').val() == "" || $('#PoType').val() == "0") {
        alert("Please select order by.")
        $('#PoType').css('border-color', 'red');
        $('#PoType').focus();
        return false;
    }
    else {
        $('#PoType').css('border-color', '');
    }
    if ($('#Form').val() == "") {
        alert("Please select Form.")
        $('#Form').css('border-color', 'red');
        $('#Form').focus();
        return false;
    }
    else {
        $('#Form').css('border-color', '');
    }
    if (($('#OrderType option:selected').text() != "General" && '@Model' != null && '@Model.IndentNo' == null && '@Model.IndentNo' == "")) {
        if ($('#IndentNo').val() == "") {
            alert("Please Select Indent No.")
            $('#IndentNo').css('border-color', 'red');
            $('#IndentNo').focus();
            return false;
        }
        else {
            $('#IndentNo').css('border-color', '');
        }
    }
    if ($('#MachinaryMaterialID').is(':disabled') != true) {
        if ($('#MachinaryMaterialID').val() == "") {
            alert("Please select Machine name.")
            $('#MachinaryMaterialID').css('border-color', 'red');
            $('#MachinaryMaterialID').focus();
            return false;
        }
        else {
            $('#MachinaryMaterialID').css('border-color', '');
        }
    }
    if ($('#Weight').is(':disabled') != true) {
        if ($('#Weight').val() == "") {
            alert("Please Enter Weight.")
            $('#Weight').css('border-color', 'red');
            $('#Weight').focus();
            return false;
        }
        else {
            $('#Weight').css('border-color', '');
        }
    }
    if ($('#ServiceTaxPer').val() == "") {
        alert("Please select Service Tax per.")
        $('#ServiceTaxPer').css('border-color', 'red');
        $('#ServiceTaxPer').focus();
        return false;
    }
    else {
        $('#ServiceTaxPer').css('border-color', '');
    }
    if ($('#ServiceTaxType').val() == "") {
        alert("Please select Service Tax Type.")
        $('#ServiceTaxType').css('border-color', 'red');
        $('#ServiceTaxType').focus();
        return false;
    }
    else {
        $('#ServiceTaxType').css('border-color', '');
    }
    if ($('#ServiceTaxNumbner').val() == "") {
        alert("Please Enter Service Tax amt.")
        $('#ServiceTaxNumbner').css('border-color', 'red');
        $('#ServiceTaxNumbner').focus();
        return false;
    }
    else {
        $('#ServiceTaxNumbner').css('border-color', '');
    }

}
function poQtyChange(arg) {
    debugger;
    var pending_total = 0;
    var indentQty = $("#Qty").val();
    var poQty = $("#PoQty").val();
    $("#VAT").val("0");
    $("#VATAmt").val("0");
    var pendingQtytoadd = $("#PendingQty").val();
    var pendingQty = $("#PendingQty").val();
    var materialID = $("#Material").val();
   // var indentno = $("#IndentNo").val();
   // var indentno = "";
    var chkArray = [];
    $(".chkProduct:checked").each(function () {
        chkArray.push($(this).val());
    });
    /* we join the array separated by the comma */
    var selected;
    selected = chkArray.join(',');
    var IndentNo = "";
    IndentNo = selected;
    //Indentid = IndentNo;
    debugger;
    if ($('#OrderType option:selected').text() != "Direct Po") {  
        $.ajax({          
            url: '/PurchaseOrder/GetIndentPOQty',
            type: "GET",         
            dataType: "JSON",
            data: { Indentid: IndentNo, MaterialID: materialID },
            success: function (data) {
                debugger;
                $("#PendingQty").val(data.PoPending);
                pending_total = data.PoPending;
                $("#PendingQty").prop("disabled", true);
             
                if ((parseFloat(pending_total) == "" || parseFloat(pending_total) == 0) && data.isExist == true) {
                    pendingQty = 0;
                    pendingQty = parseFloat(indentQty) - parseFloat(poQty);
                    $("#PendingQty").val(pendingQty.toFixed(3));                 
                    return false;
                }
                else if ((parseFloat(pending_total) == "" || parseFloat(pending_total) == 0) && data.isExist == false) {
                    pendingQty = 0;
                    pendingQty = parseFloat(indentQty) - parseFloat(poQty);
                    $("#PendingQty").val(pendingQty.toFixed(3));
                }
                else {
                    var AlreadyPoraisedQty = parseFloat(indentQty) - parseFloat(pending_total);
                    var PoraisedQty = 0;
                    $("#PendingQty").val(pending_total);
                    PoraisedQty = (parseFloat(indentQty) - (parseFloat(AlreadyPoraisedQty) + parseFloat(poQty)));
                    if (parseFloat(PoraisedQty) > parseFloat(indentQty) || parseFloat(PoraisedQty) < 0) {
                        alert("Indent qty and Po raised qty is huge");
                        $("#save").hide();
                        $('#PoQty').css('border-color', 'red');
                        $('#PoQty').focus();
                        return false;
                    }
                    else {
                        $("#save").show();
                        $('#PoQty').css('border-color', '');
                        PoraisedQty = 0;
                        PoraisedQty = (parseFloat(indentQty) - (parseFloat(AlreadyPoraisedQty) + parseFloat(poQty)));
                        pendingQty = parseFloat(PoraisedQty);
                        if (pendingQty < 0) {
                            pendingQty = 0;
                        }
                        $("#PendingQty").val(pendingQty.toFixed(3));
                    }

                }
            }
        });
       // pendingQty = 0;       
       // var totalPendingQty = 0;
    }
    var totalPendingQty = 0;
    var uomVlaue1 = 0;
    var qty = 0;
    qty = $("#PoQty").val();
    if (qty == "") {
        qty = 0;
    }
    var rate_ = $("#Rate_").val();
    var totalValue = 0;
    totalValue = parseFloat(qty) * parseFloat(rate_);
    if (totalValue == "") {
        totalValue = 0;
    }
    $("#ExRate").val(totalValue);
    $("#UOMValue").val(qty);
    if ($('#Currency option:selected').text() == "Dollar") {
        $("#ExRate").val(totalValue);
        $("#AmountTax").val(totalValue);
    }
   else {
        $("#ExRate").val(Math.round(totalValue));
        $("#AmountTax").val(Math.round(totalValue));
    }
    uomVlaue1 = uomvalue_Cone;
    if (uomVlaue1 == "") {
        uomVlaue1 = 0;
    }
    var total = Math.round(uomVlaue1 * qty);
    $("#UOMValue1").val(total);
    var pendingQty_ = parseFloat(indentQty) - parseFloat(poQty) ;
    $("#PendingQty").val(pendingQty_);
}
function calulationtotal() {
    var FreightAmt = $("#FreightAmt").val();
    var InsuranceAmount = $("#InsuranceAmount").val();
    var CustomsDutyType = $("#CustomsDutyType").val();
    var PackForward = $("#PackForward").val();
    var ServiceTaxNumbner = $("#ServiceTaxNumbner").val();
    if (FreightAmt == "" || FreightAmt == undefined) {
        FreightAmt = 0;
    }
    if (InsuranceAmount == "" || InsuranceAmount == undefined) {
        InsuranceAmount = 0;
    }
    if (CustomsDutyType == "" || CustomsDutyType == undefined) {
        CustomsDutyType = 0;
    }
    if (PackForward == "" || PackForward == undefined) {
        PackForward = 0;
    }
    if (ServiceTaxNumbner == "" || ServiceTaxNumbner == undefined) {
        ServiceTaxNumbner = 0;
    }
    var FreightCostTotal = 0;
    FreightCostTotal = parseFloat(PackForward) + parseFloat(CustomsDutyType) + parseFloat(InsuranceAmount) + parseFloat(FreightAmt) + parseFloat(ServiceTaxNumbner);

    var amountTax = 0;
    amountTax = $("#AmountTax").val();
    if (amountTax == "") {
        amountTax = 0;
    }
    $("#FreightCostTotal").val(FreightCostTotal);
    var total = $("#FreightCostTotal").val();
    if (total == "" || total == undefined) {
        total = 0;
    }
    var DisAmount = 0;
    var ExciseDutyAmount = 0;
    var ExessAmount = 0;
    var SHExessAmount = 0;
    var VATAmt = 0;
    var SurChargeAmt = 0;
    if ($("#DisAmount").val() == "") {
        DisAmount = 0;
        $("#DisAmount").val(0);
    }
    if ($("#ExciseDutyAmount").val() == "") {
        ExciseDutyAmount = 0;
        $("#ExciseDutyAmount").val(0);
    }
    if ($("#ExessAmount").val() == "") {
        ExessAmount = 0;
        $("#ExessAmount").val(0);
    }
    if ($("#SHExessAmount").val() == "") {
        SHExessAmount = 0;
        $("#SHExessAmount").val(0);
    }
    if ($("#VATAmt").val() == "") {
        VATAmt = 0;
        $("#VATAmt").val(0);
    }
    if ($("#SurChargeAmt") == "") {
        SurChargeAmt = 0;
    }

    var qty_Value = 0;
    qty_Value = $("#ExRate").val();
    if (qty_Value == "") {
        qty_Value = 0;
    }
    else {
        qty_Value = qty_Value;
    }
    var Amount_Tax = ((parseFloat(ExciseDutyAmount) + parseFloat(ExessAmount) + parseFloat(SHExessAmount) + parseFloat(VATAmt) + parseFloat(SurChargeAmt)) - parseFloat(DisAmount));
    $("#AmountTax").val(Math.round(parseFloat(Amount_Tax) + parseFloat(total) + parseFloat(qty_Value)));


}
function Indentshow() {
    debugger;
    if ($('#OrderType option:selected').text() == "Direct Po") {
        var chkArray = [];
        $(".chkProduct:checked").each(function () {
            chkArray.push($(this).val());
        });
        /* we join the array separated by the comma */
        var selected;
        selected = chkArray.join(',');
        var IndentNo = "";
        IndentNo = selected;
        if (IndentNo != null && IndentNo.length > 0) {

        }
        else {
            alert("Please select indent no");
            return false;
        }
        //var IndentNo = $('.Indentno_Cls').val();
        var supplierid_ = $('#Supplier').val();
        if (supplierid_ == "") {
            supplierid_ = 0;
        }
        $.ajax({
            url: '/PurchaseOrder/GetIndentMaterials_',
            type: "GET",
            dataType: "JSON",
            data: { Indentid: IndentNo, Supplierid: supplierid_ },
            success: function (data) {
                if (data.pono != "") {
                    $("#PoNo").val(data.pono);
                }
                var row = "";
                $("#list-amended-material tbody").html("");
                $.each(data.indentMaterials, function (i, item) {
                    if (item.SubstanceRange == null) {
                        item.SubstanceRange = "";
                    }
                    if (item.Color == null) {
                        item.Color = "";
                    }
                    if (item.RequiredQty == null) {
                        item.RequiredQty = "";
                    }
                    if (item.SampleNorms == null) {
                        item.SampleNorms = "";
                    }
                    if (item.Price == null) {
                        item.Price = 0;
                    }
                    $("#RemoveHead").css("display", "block");
                    $("#EditHead").css("display", "block");
                    row += "<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"
                    row += "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                    row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ")' class='' />" + "</td>"

                    if (item.IsPo == true) {
                        row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                    }
                    else {
                        row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO RAISE PO' class='' />" + "</td>"
                    }
                    row + "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                    row += "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                    row += "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                    row += "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                    row += "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                    row += "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                    row += "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                    row += "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                    row += "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                    row += "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                    row += "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                    row += "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                    row += "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                    row += "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                    row += "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                    row += "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                    row += "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"
                    row += "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                    row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                    row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"
                    row += "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                    row += "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>";
                });
                $("#list-amended-material tbody").append(row);
            }
        });
    }
    else if ($('#OrderType option:selected').text() == "Order" || $('#OrderType option:selected').text() == "General") {
       
        var chkArray = [];
        $(".chkProduct:checked").each(function () {
            chkArray.push($(this).val());
        });
        /* we join the array separated by the comma */
        var selected;
        selected = chkArray.join(',');
        var IndentNo = "";
        IndentNo = selected;
        if (IndentNo != null && IndentNo.length > 0) {

        }
        else {
            alert("Please select indent no");
            return false;
        }
        var supplierid_ = $('#Supplier').val();
        if (supplierid_ == "") {
            supplierid_ = 0;
        }
        $.ajax({
            url: '/PurchaseOrder/GetIndentMaterials_',
            type: "GET",
            dataType: "JSON",
            contentType: 'application/json; charset=utf-8',
            data: { Indentid: IndentNo, Supplierid: supplierid_ },
            success: function (data) {
                debugger;
                if (data.pono != "") {
                    $("#PoNo").val(data.pono);
                }
                var row = "";
                $("#list-amended-material tbody").html("");
                $.each(data.IndentMaterials, function (key, item) {
                    if (item.SubstanceRange == null) {
                        item.SubstanceRange = "";
                    }
                    if (item.Color == null) {
                        item.Color = "";
                    }
                    if (item.RequiredQty == null) {
                        item.RequiredQty = "";
                    }
                    if (item.SampleNorms == null) {
                        item.SampleNorms = "";
                    }
                    if (item.Price == null) {
                        item.Price = 0;
                    }
                    if (item.MRPNO == null) {
                        item.MRPNO = "";
                    }
                    if (item.IndentID == null) {
                        item.IndentID = "";
                    }
                    $("#RemoveHead").css("display", "block");
                    $("#EditHead").css("display", "block");
                    row += "<tr id=" + item.MaterialMasterID + " class='rowevent'> <td id='BOMID' style='display:none' class='BOMID' value=''>" + 0 + "</td>"
                    row += "<td><input type='button' value='Remove' onclick='Delete(" + item.MaterialMasterID + ")' class='' />" + "</td>"
                    row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.MaterialMasterID + "," + 0 + "," + item.IndentQTY + "," + item.RequiredQty + ")' class='' />" + "</td>"
                    if (item.IsPo == true) {
                        row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                    }
                    else {
                        row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO RAISE PO' class='' />" + "</td>"
                    }
                    row += "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                  
                    row += "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                    row += "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                    row += "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                    row += "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                    row += "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                    row += "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                    row += "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                    row += "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                    row += "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                    row += "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                    row += "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                    row += "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                    row += "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                    row += "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                    row += "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                    row += "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"
                    row += "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                    row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                    row += "<td id='MaterialMasterID' style='display:none' class='MaterialMasterID' value=''>" + item.MaterialMasterID + "</td>"
                    row += "<td id='SizeRangeMasterID' style='display:none' class='SizeRangeMasterID' value=''>" + item.SizeRangeMasterID + "</td>"
                    row += "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                    row += "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>";
                });
                $("#list-amended-material tbody").append(row);
            }
        });
    }
    else if ($('#OrderType option:selected').text() == "General") {
        var chkArray = [];
        $(".chkProduct:checked").each(function () {
            chkArray.push($(this).val());
        });
        /* we join the array separated by the comma */
        var selected;
        selected = chkArray.join(',');
        var IndentNo = "";
        IndentNo = selected;
        if (IndentNo != null && IndentNo.length > 0) {

        }
        else {
            alert("Please select indent no");
            return false;
        }
        var supplierid_ = $('#Supplier').val();
        if (supplierid_ == "") {
            supplierid_ = 0;
        }
        $.ajax({
            url: '/PurchaseOrder/GetIndentMaterials_',
            type: "GET",
            dataType: "JSON",
            data: { Indentid: IndentNo, Supplierid: supplierid_ },
            success: function (data) {
                var row = "";
                if (data.pono != "") {
                    $("#PoNo").val(data.pono);
                }
                $("#list-amended-material tbody").html("");
                $.each(data.indentMaterials, function (i, item) {
                    if (item.SubstanceRange == null) {
                        item.SubstanceRange = "";
                    }
                    if (item.Color == null) {
                        item.Color = "";
                    }
                    if (item.RequiredQty == null) {
                        item.RequiredQty = "";
                    }
                    if (item.SampleNorms == null) {
                        item.SampleNorms = "";
                    }
                    if (item.Price == null) {
                        item.Price = "0";
                    }
                    $("#RemoveHead").css("display", "block");
                    $("#EditHead").css("display", "block");
                    row += "<tr id=" + item.IndentMaterialID + "> <td id='BOMID' style='display:none' class='BOMID' value=''>" + item.BOMID + "</td>"

                    row += "<td><input type='button' value='Remove' onclick='Delete(" + item.IndentMaterialID + ")' class='' />" + "</td>"
                    row += "<td><input type='button' name='Edit' id='EditRow' value='Edit' onclick='RowClick(" + item.IndentMaterialID + "," + item.BOMID + ","+ 0+","+0+")' class='' />" + "</td>"
                    if (item.IsPo == true) {
                        row += "<td><input type='button' name='RAISEDPO' id='EditRow' value='RAISED PO' class='' />" + "</td>"
                    }
                    else {
                        row += "<td><input type='button' name='RAISEPO' id='RAISEPO' value='NEED TO RAISE PO' class='' />" + "</td>"
                    }
                    row += "<td id='BOMMaterialID' style='display:none' class='BOMMaterialID' value=''>" + item.BOMMaterialID + "</td>"
                    row += "<td id='MRPNO' style='' class='MRPNO' value=''>" + item.MRPNO + "</td>"
                    row += "<td id='CategoryName'  class='CategoryName' value=''>" + item.CategoryName + "</td>"
                    row += "<td id='GroupName'  class='GroupName' value=''>" + item.GroupName + "</td>"
                    row += "<td id='SubstanceRange'  class='SubstanceRange' value=''>" + item.SubstanceRange + "</td>"
                    row += "<td id='MaterialDescription'  class='MaterialDescription' value=''>" + item.MaterialDescription + "</td>"
                    row += "<td id='Color'  class='Color' value=''>" + item.Color + "</td>"
                    row += "<td id='SampleNorms' class='SampleNorms' value=''>" + item.SampleNorms + "</td>"
                    row += "<td id='RequiredQty' style='' class='RequiredQty' value=''>" + item.RequiredQty + "</td>"
                    row += "<td id='IndentQTY' style='' class='IndentQTY' value=''>" + item.IndentQTY + "</td>"
                    row += "<td id='Price' style='' class='Price' value=''>" + item.Price + "</td>"
                    row += "<td id='Value' style='' class='Value' value=''>" + item.Value + "</td>"
                    row += "<td id='IndentID' style='' class='IndentID' value=''>" + item.IndentID + "</td>"
                    row += "<td id='MaterialCategoryMasterId' style='display:none' class='MaterialCategoryMasterId' value=''>" + item.MaterialCategoryMasterId + "</td>"
                    row += "<td id='MaterialGroupMasterId' style='display:none' class='MaterialGroupMasterId' value=''>" + item.MaterialGroupMasterId + "</td>"
                    row += "<td id='ColorMasterId' style='display:none'  class='ColorMasterId' value=''>" + item.ColorMasterId + "</td>"
                    row += "<td id='SubstanceMasterId' style='display:none' class='SubstanceMasterId' value=''>" + item.SubstanceMasterId + "</td>"
                    row += "<td id='UomMasterId' style='display:none' class='UomMasterId' value=''>" + item.WastageQtyUOM + "</td>"
                    row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierId + "</td>"
                    row += "<td id='SupplierId' style='display:none' class='SupplierId' value=''>" + item.SupplierName + "</td>"
                    row += "<td id='BuyerMasterId' style='display:none' class='BuyerMasterId' value=''>" + item.BuyerMasterId + "</td>"
                    row += "<td id='OrderEntryId' style='display:none' class='OrderEntryId' value=''>" + item.OrderEntryId + "</td></tr>";//);
                });
                $("#list-amended-material tbody").append(row);
            }
        });
    }

}