//$(function () {
//    $('#id_of_your_textbox').blur(function () {
//        var value = $(this).val();
//        alert(value);
//    });
//});


//$(function () {
   
//    $("#Sortby").combobox();

//});
//$(function () {
//    $("#Sortby").combobox({
//        select: function () {
//            alert("inside material");

//        }
//    });
//});


    $(window).load(function () {
        //$('.loader-overlay').show();
        $('.loader-overlay').fadeOut('fast');
    });

    $(document).ready(function () {

         $.widget("custom.combobox", {
                            _create: function () {
                                this.wrapper = $("<span>")
                                  .addClass("custom-combobox")
                                  .insertAfter(this.element);

            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },

        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
              value = selected.val() ? selected.text() : "";

            this.input = $("<input>")
              .appendTo(this.wrapper)
              .val(value)
              .attr("title", "")
              .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
              .autocomplete({
                  delay: 0,
                  minLength: 0,
                  source: $.proxy(this, "_source")
              })
              .tooltip({
                  tooltipClass: "ui-state-highlight"
              });

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                },

                autocompletechange: "_removeIfInvalid"
            });
        },

        _createShowAllButton: function () {
            var input = this.input,
              wasOpen = false;

            $("<a>")
              .attr("tabIndex", -1)
              .attr("title", "Show All Items")
              .tooltip()
              .appendTo(this.wrapper)
              .button({
                  icons: {
                      primary: "ui-icon-triangle-1-s"
                  },
                  text: false
              })
              .removeClass("ui-corner-all")
              .addClass("custom-combobox-toggle ui-corner-right")
              .mousedown(function () {
                  wasOpen = input.autocomplete("widget").is(":visible");
              })
              .click(function () {
                  input.focus();

               
                  if (wasOpen) {
                      return;
                  }

                  // Pass empty string as value to search for, displaying all results
                  input.autocomplete("search", "");
              });
        },

        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },

        _removeIfInvalid: function (event, ui) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = this.input.val(),
              valueLowerCase = value.toLowerCase(),
              valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            // Remove invalid value
            this.input
              .val("")
              .attr("title", value + " didn't match any item")
              .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.autocomplete("instance").term = "";
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });

    if($('#BomId') == 0)
    {
        $("#MeanSize").val(121);
        $("#NoOfSets").val("1");
    }
    $('#Addrowfields tbody').remove();
    $("#Date").datepicker({ dateFormat: "dd/mm/yy" });

    $("#Wastage").change(function () {

        var sn = $("#SampleNorms").val();
        var w = $(this).val();
        var wq = ((parseFloat(sn) / 100) * parseFloat(w));
        var tn = parseFloat(sn) + wq;
        $("#WastageQty").val(wq.toFixed(4));
        $("#TotalNorms").val(tn.toFixed(4));
    });

    $("#WastagePercent").keypress(function () {
        var sn = $(".SampleNorms").val();
        var w = $(this).val();
        var wq = parseFloat(sn) * parseFloat(w) / 100;
        var tn = parseFloat(sn) + parseFloat(wq);
        $(".WastageQty").val(wq.toFixed(4));
        $(".TotalNormsQty").val(tn.toFixed(4));
    });


function callChange(arg) {
    var id = '';
    var id1 = '';
    id = $('#Addrowfields tbody tr .WastagePercent' + arg).val();
    id1 = $('#Addrowfields tbody tr .SampleNorms' + arg).val();

    var wastage = parseFloat(id);
    var norms = parseFloat(id1);
    var wq = parseFloat(norms) * parseFloat(wastage) / 100;
    var tn = parseFloat(norms) + parseFloat(wq);
    $('#Addrowfields tbody tr .WastageQty' + arg).val(wq);
    $('#Addrowfields tbody tr .TotalNormsQty' + arg).val(tn);




}

var SnoVal = 1;



$(function () {

    $('#CommnBOM1,#CommnBOM2,#CommnBOM3,#CommnBOM4,#CommnBOM5').change(function () {
        $('.loader-overlay').fadeIn('slow');
        $.ajax({
            type: 'GET',
            // dataType: 'HTML',
            url: '/BillOfMaterial/ParentMaterialList',
            data: {
                BomId: $(this).val(),
                BomNo:$("#BomNo").val(),
                Description:$("#Description").val(),
                BuyerMasterId:$("#BuyerMasterId").val(),
                BuyerModel:$("#BuyerModel").val(),
                MeanSize:$("#MeanSize").val(),
                Date:$("#Date").val(),
                LastBomNoEntered:$("#LastBomNoEntered").val(),
                LinkBomNo:$("#LinkBomNo").val(),
                CommnBOM1:$("#CommnBOM1").val(),
                CommnBOM2:$("#CommnBOM2").val(),
                CommnBOM3:$("#CommnBOM3").val(),
                CommnBOM4:$("#CommnBOM4").val(),
                CommnBOM5:$("#CommnBOM5").val(),
                OurNorms:$("#OurNorms").val(),
                OurNormsPercent:$("#OurNormsPercent").val(),
                PurchaseNorms:$("#PurchaseNorms").val(),
                PurchaseNormsPercent:$("#PurchaseNormsPercent").val(),
                IssueNorms:$("#IssueNorms").val(),
                IssueNormsPercent:$("#IssueNormsPercent").val(),
                CostingNorms:$("#CostingNorms").val(),
                CostingNormsPercent:$("#CostingNormsPercent").val(),

            },

            success: function (data) {
                debugger
                $('.loader-overlay').fadeOut('fast');

                $("#BomNo").val(data.Model.BomNo);
                $("#Description").val(data.Model.Description);
                $("#BuyerMasterId").val(data.Model.BuyerMasterId);
                $("#BuyerMasterId").val(data.Model.BuyerMasterId);
                $("#BuyerModel").val(data.Model.BuyerModel);
                $("#MeanSize").val(data.Model.MeanSize);
                // $("#ParentBomNo").val(data.Model.ParentBomNo);
                //$("#LinkBomNo").val(data.Model.LinkBomNo);
                $("#BuyerNorms").val(data.Model.BuyerNorms);
                $("#NoOfSets").val(data.Model.NoOfSets);
                $("#UomMasterId").val(data.Model.UomMasterId);
                $("#SupplierMasterId").val(data.Model.SupplierMasterId);
                $("#SizeScheduleMasterId").val(data.Model.SizeScheduleMasterId);
                $("#SizeRangeMasterId").val(data.Model.SizeRangeMasterId);
                debugger
                $('#list-materials tbody').html("")
                var rowCount = 0;
                $.each(data.Material, function (index, item) {
                    if(item.SubtranceRangeName==null)
                    {
                        item.SubtranceRangeName="";
                    }
                    if(item.SampleNorms==null)
                    {
                        item.SampleNorms="";
                    }

                    $('#list-materials tbody').append("<tr id=rowid_" + item.BOMMaterialID + "> <td style='display:none' class='BomNo'><span>" + item.BOMMaterialID + "</span><input type='hidden' class='BomId' id='bomid' value='" + item.BomId + "'></td>" +
                                                                                                            //"<td class='Description'><span>" + $('#Description').val() + "</span><input type='hidden' class='AddRowFields' value='" + $("#AddRowFields").val() + "'></td>" +
                                                                                                            "<td style='display:none' class='Description' id='Description'>" + item.Description + "</td>" +
                                                                                                            "<td style='display:none' class='BuyerMasterId' id='BuyerMasterId'>" + item.BuyerMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='BuyerModel' id='BuyerModel'>" + item.BuyerModel + "</td>" +
                                                                                                            "<td style='display:none' class='MeanSize' id='MeanSize'>" + item.MeanSize + "</td>" +
                                                                                                            "<td style='display:none' class='Date' id='Date'>" + item.Date + "</td>" +
                                                                                                            "<td style='display:none' class='ParentBomNo' id='ParentBomNo'>" + "" + "</td>" +
                                                                                                            "<td style='display:none' class='LinkBomNo' id='LinkBomNo'>" + "" + "</td>" +
                                                                                                            "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + item.CommnBOM1 + "</td>" +
                                                                                                            "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + item.BOMMaterialID + "</td>" +
                                                                                                               " <td title='Delete'  style='cursor:pointer;' class='Delete' id='Delete'><input type='button' value='Delete' style='cursor:pointer;' title='Delete' onclick='Deletebtn(" + item.BOMMaterialID+")' class='change_status change_status_  Delete input Edit input' /></td>" +
                                                                                                                  " <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><input type='button' value='Edit' style='cursor:pointer;' title='Edit' onclick='RowClick(" + item.BOMMaterialID + "," + item.BomId + ")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                           //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href='data.Material.BOMMaterialID'>Delete</a></td>" +
                                                                                                           // "<td><input type='button' value='Edit' onclick='RowClick(" + item.BOMMaterialID + "," + item.BomId + ")' class='' />" + "</td>" +
                                                                                                           // " <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href='data.Material.BOMMaterialID'>Delete</a></td>" +
                                                                                                            "<td class='MaterialCategoryMasterId_' id='MaterialCategoryMasterId_'>" + item.CategoryName + "</td>" +
                                                                                                            "<td class='MaterialName_' id='MaterialName_'>" + item.MaterialName + "</td>" +
                                                                                                            "<td class='MaterialGroupMasterId_' id='MaterialGroupMasterId_'>" + item.GroupName + "</td>" +
                                                                                                            "<td class='ColorMasterId_' id='ColorMasterId_'>" + item.ColorName + "</td>" +
                                                                                                            "<td class='SubstanceMasterId_' id='SubstanceMasterId_'>" + item.SubtranceRangeName + "</td>" +
                                                                                                            // "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + item.SizeScheduleMasterId + "</td>" +
                                                                                                            "<td class='SampleNorms' id='SampleNorms'>" + item.SampleNorms + "</td>" +
                                                                                                            //"<td class='Wastage' id='Wastage'>" + item.Wastage + "</td>" +
                                                                                                            //"<td class='WastageQty' id='WastageQty'>" + item.WastageQty + "</td>" +
                                                                                                                 "<td class='WastageQtyUOM' id='WastageQtyUOM'>" + item.WastageqtyUOM + "</td>" +
                                                                                                            "<td class='TotalNorms' id='TotalNorms'>" + item.TotalNorms + "</td>" +
                                                                                                            //"<td class='TotalNormsUOM' id='TotalNormsUOM'>" + item.TotalNormsUOM + "</td>" +
                                                                                                            "<td style='display:none' class='ComponentName' id='ComponentName'>" + item.ComponentName + "</td>" +
                                                                                                            "<td style='display:none' class='BuyerNorms' id='BuyerNorms'>" + item.BuyerNorms + "</td>" +
                                                                                                            "<td style='display:none' class='NoOfSets' id='NoOfSets'>" + item.NoOfSets + "</td>" +
                                                                                                            "<td style='display:none' class='UomMasterId' id='UomMasterId'>" + item.UomMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='SupplierMasterId' id='SupplierMasterId'>" + item.SupplierMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='SizeRangeMasterId' id='SizeRangeMasterId'>" + item.SizeRangeMasterId + "</td>" +
                                   // "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.BOM.SizeScheduleMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='OurNorms' id='OurNorms'>" + item.OurNorms + "</td>" +
                                                                                                            "<td style='display:none' class='OurNormsPercent' id='OurNormsPercent'>" + item.OurNormsPercent + "</td>" +
                                                                                                            "<td style='display:none' class='PurchaseNorms' id='PurchaseNorms'>" + item.PurchaseNorms + "</td>" +
                                                                                                            "<td style='display:none' class='PurchaseNormsPercent' id='PurchaseNormsPercent'>" + item.PurchaseNormsPercent + "</td>" +
                                                                                                            "<td style='display:none' class='IssueNorms' id='IssueNorms'>" + item.IssueNorms + "</td>" +
                                                                                                            "<td style='display:none' class='IssueNormsPercent' id='IssueNormsPercent'>" + item.IssueNormsPercent + "</td>" +
                                                                                                            "<td style='display:none' class='CostingNorms' id='CostingNorms'>" + item.CostingNorms + "</td>" +
                                                                                                             "<td style='display:none' class='Conversion' id='Conversion'>" + item.Conversion + "</td>" +
                                                                                                             "<td style='display:none'class='MaterialCategoryMasterId' id='MaterialCategoryMasterId'>" + item.MaterialCategoryMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='MaterialName' id='MaterialName'>" + item.MaterialName + "</td>" +
                                                                                                            "<td style='display:none' class='MaterialGroupMasterId' id='MaterialGroupMasterId'>" + item.MaterialGroupMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='ColorMasterId' id='ColorMasterId'>" + item.ColorMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='SubstanceMasterId' id='SubstanceMasterId'>" + item.SubstanceMasterId + "</td>" +
                                                                                                            "<td style='display:none' class='CostingNormsPercent' id='CostingNormsPercent'>" + item.CostingNormsPercent + "</td>" +
                                                                                                            "<td style='display:none' class='PreparedBy' id='PreparedBy'>" + item.PreparedBy + "</td>" +
                                                                                                            "<td style='display:none' class='WastageQtyUOM' id='WastageQtyUOM'>" + item.WastageQtyUOM + "</td>" +
                                                                                                            "<td style='display:none' class='VerifiedBy' id='VerifiedBy'>" + item.VerifiedBy + "</td>" +
                                                                                                            "<td style='display:none' class='CheckedBy' id='CheckedBy'>" + item.CheckedBy + "</td>"
                                                                                                            + "<td style='display:none' class='AddRowFields'>" + $('#AddRowFields').val() + "</td></tr>");
                    // $('#BOMMaterialID').val(data.Material.BOMMaterialID);
                    rowCount++;
                });

                // $("#NoOfSets").val(data.Model.NoOfSets);
            }
        });
    });
});

$(document).on('click', '#add-Compenent', function () {

    $("#Addrowfields").append("<tr class=''><td id='Sno' value=''>" + SnoVal + "</td><td class='width-141'><input type='text' id='Component' value=''/></td><td class='width-91'><input type='text' id='length' class='length' value='' onkeypress=' return isNumberKey(event)' /></td><td class='width-91'><input type='text' id='width' class='width' value='' onkeypress='return isNumberKey(event)' /></td><td class='width-91'><input type='text' id='Nos' class='Nos' value='' onkeypress='return isNumberKey(event)' /></td><td class='width-91'><input type='text' id='SampleNorms' class='SampleNorms" + SnoVal + "' onkeypress='return isNumberKey(event)' /></td><td class='width-91'><input type='text' id='WastagePercent' class='WastagePercent" + SnoVal + "' onkeypress='return isNumberKey(event)' onchange='callChange(" + SnoVal + ")' /></td></td><td class='width-91'><input type='text' id='WastageQty' class='WastageQty" + SnoVal + "' onkeypress='return isNumberKey(event)' /></td><td class='width-91'><input type='text' id='TotalNormsQty' class='TotalNormsQty" + SnoVal + "' onkeypress='return isNumberKey(event)' /><td><input type='button' value='Remove' class='removeSchedule btn btn-default btn-style' /></td></tr>");
    SnoVal++;
});
$(document).on('click', '.removeSchedule', function () {
    $(this).closest("tr").remove();
});

function validation() {
  
    if ($('#BomNo').val() == "") {
        alert("Please Enter BomNo.")
        $('#BomNo').css('border-color', 'red');
        $('#BomNo').focus();
      
        return false;
    }
    else {
        $('#BomNo').css('border-color', '');
       
        return true;
    }
    if ($('#Description').val() == "") {
        alert("Please Enter Description.")
        $('#Description').css('border-color', 'red');
        $('#Description').focus();
        return false;
    }
    else {
        $('#Description').css('border-color', '');
        return true;
    }
    if ($('#BuyerMasterId').val() == "") {
        $('#BuyerMasterId').css('border-color', 'red');
        $('#BuyerMasterId').focus();
        alert("Please Select BuyerMasterId.")
        return false;
    }
    else {
        $('#BuyerMasterId').css('border-color', '');
        return true;
    }

    if ($('#MeanSize').val() == "") {
        alert("Please Enter Last MeanSize.")
        $('#MeanSize').css('border-color', 'red');
        $('#MeanSize').focus();
        return false;
    }
    else {
        $('#MeanSize').css('border-color', '');
        return true;
    }



    if ($('#Date').val() == "") {
        alert("Please Enter Date.")
        $('#Date').css('border-color', 'red');
        $('#Date').focus();
        return false;
    }
    else {
        $('#Date').css('border-color', '');
        return true;
    }

    if ($('#MaterialCategoryMasterId').val() == "") {
        alert("Please select category.")
        $('#MaterialCategoryMasterId').css('border-color', 'red');
        $('#MaterialCategoryMasterId').focus();
        return false;
    }
    else {
        $('#MaterialCategoryMasterId').css('border-color', '');
        return true;
    }


    if ($('#MaterialName').val() == "" && $('#MaterialName').val()==0) {
        alert("Please select Material Name.")
        $('#MaterialName').css('border-color', 'red');
        $('#MaterialName').focus();
        return false;
    }
    else {
        $('#MaterialName').css('border-color', '');
        return true;
    }


    if ($('#BuyerModel').val() == "") {
        alert("Please Select BuyerModel.")
        $('#BuyerModel').css('border-color', 'red');
        $('#BuyerModel').focus();
        return false;
    }
    else {
        $('#BuyerModel').css('border-color', '');
        return true;
    }

    if ($('#MeanSize').val() == "") {
        alert("Please Enter Last MeanSize.")
        $('#MeanSize').css('border-color', 'red');
        $('#MeanSize').focus();
        return false;
    }
    else {
        $('#MeanSize').css('border-color', '');
        return true;
    }

    if($("#LinkBomNo").prop('disabled')!=true)
    {
        if ($('#LinkBomNo').val() == "") {
            alert("Please Enter Order LinkBomNo.")
            $('#LinkBomNo').css('border-color', 'red');
            $('#LinkBomNo').focus();
            return false;
        }
        else {
            $('#LinkBomNo').css('border-color', '');
            return true;
        }
    }

    if ($('#MaterialCategoryMasterId').val() == "") {
        alert("Please Enter Category.")
        $('#MaterialCategoryMasterId').css('border-color', 'red');
        $('#MaterialCategoryMasterId').focus();
        return false;
    }
    else {
        $('#MaterialCategoryMasterId').css('border-color', '');
        return true;
    }

    if ($('#MaterialGroupMasterId').val() == "") {
        alert("Please Enter Group.")
        $('#MaterialGroupMasterId').css('border-color', 'red');
        $('#MaterialGroupMasterId').focus();
        return false;
    }
    else {
        $('#MaterialGroupMasterId').css('border-color', '');
        return true;
    }



    if ($('#ColorMasterId').val() == "") {
        alert("Please Enter Color.")
        $('#ColorMasterId').css('border-color', 'red');
        $('#ColorMasterId').focus();
        return false;
    }
    else {
        $('#ColorMasterId').css('border-color', '');
        return true;
    }
    if($('#SubstanceMasterId').prop('disabled')==false)
    {
        if ($('#SubstanceMasterId').val() == "") {
            alert("Please Enter Substance.")
            $('#SubstanceMasterId').css('border-color', 'red');
            $('#SubstanceMasterId').focus();
            return false;
        }
        else {
            $('#SubstanceMasterId').css('border-color', '');
            return true;
        }
    }


    if ($('#TotalNorms').val() == "") {
        alert("Please Enter TotalNorms.")
        $('#TotalNorms').css('border-color', 'red');
        $('#TotalNorms').focus();
        return false;
    }
    else {
        $('#TotalNorms').css('border-color', '');
        return true;
    }
    if ($('#WastageQtyUOM').val() == "") {

        $('#WastageQtyUOM').css('border-color', 'red');
        $('#WastageQtyUOM').focus();
        return false;
    }
    else {
        $('#WastageQtyUOM').css('border-color', '');
        return true;
    }


    if ($('#NoOfSets').val() == "") {
        alert("Please Enter NoOfSets.")
        $('#NoOfSets').css('border-color', 'red');
        $('#NoOfSets').focus();
        return false;
    }
    else {
        $('#NoOfSets').css('border-color', '');
        return true;
    }
    //if ($("#SizeRangeMasterId").prop('disabled') == false) {
    //    if ($('#SizeRangeMasterId').val() == "") {
    //        alert("Please Enter SizeRange")
    //        $('#SizeRangeMasterId').css('border-color', 'red');
    //        $('#SizeRangeMasterId').focus();
    //        return false;
    //    }
    //    else {
    //        $('#SizeRangeMasterId').css('border-color', '');
    //    }
    //}
    //else {
    //    $('#SizeRangeMasterId').val('');
    //}


    //if ($('#UomMasterId').val() == "") {
    //    $('#UomMasterId').css('border-color', 'red');
    //    $('#UomMasterId').focus();
    //    alert("Please Enter Uom.")
    //    return false;
    //}
    //else {
    //    $('#UomMasterId').css('border-color', '');
    //}



}





function DisplaySizeRange(){
    $('.loader-overlay').fadeIn('slow');

    if($("#Displaycheck").is(":checked")==true && $('#SizeRangeMasterId').val()!=0) {
        $.ajax({
            type: "GET",
            dataType: "JSON",
            url: '@Url.Action("GetSizeRange", "BuyerOrderEntryForm")',
            data: { SizeRangeMasterId: $('#SizeRangeMasterId').val() },
            success: function (data) {
                $('.loader-overlay').fadeOut('fast');
                var count = 1;
                $('#sizeRangeTable tbody tr').each(function () {
                    $(this).find("td:not(:first)").remove();

                });
                for (var i = 0; i <= data.length; i++) {

                    $('#sizeRangeTable tbody tr:first-child').append('<td  style="background-color: #ddd;padding-bottom: 3px;width: auto !important;padding:0 0px;" class="SizeRangeRefValue"> ' + data[i].SizeRangeRefValue + ' </td> ');
                    $('#sizeRangeTable tbody tr:last').append("<td><input type='checkbox' name='size_chk' id='size_chk"+i+"' class='checked_checkbox'  style=''></input> <label for='size_chk"+i+"' style='' class='lbl-ch'></label></td>");
                    count++;
                }
            }
        });
    }
    else
    {
        $('#sizeRangeTable tbody tr').each(function(){
            $(this).find("td:not(:first)").remove();

        });
        $('.loader-overlay').fadeOut('fast');
    }

}

    var hostname="";
    $(function () {
        $("#MaterialName").combobox();
        $("#MaterialGroupMasterId").combobox();
        $("#MaterialCategoryMasterId").combobox();
        $("#ParentBomNo").combobox();

    });
    

$(function () {
    $("#ParentBomNo").combobox({
        select: function() {
            $('.loader-overlay').fadeIn('slow');
            if($("#ParentBomNo option:selected").text().trim()==$("#BomNo").val().trim()||$("#BomNo").val().trim()=="")
            {
                $('.loader-overlay').fadeOut('fast');
                alert("Please enter correct bom.")
                $('#BomNo').css('border-color', 'red');
                $('#BomNo').focus();
                $("#ParentBomNo").val("")
                $("#ParentBomNo").combobox('destroy');
                $("#ParentBomNo").combobox();
                return false;
            }
            else {
                // alert(123);
                $("#HiddenParentBOMID").val($(this).val());
                $('#BomNo').css('border-color', '');
                $.ajax({
                    type: 'GET',
                    // dataType: 'HTML',
                    url: '/BillOfMaterial/ParentBOMMaterialList',
                    data: {
                        BomId: $(this).val(),
                        BomNo:$("#BomNo").val(),
                        Description:$("#Description").val(),
                        BuyerMasterId:$("#BuyerMasterId").val(),
                        BuyerModel:$("#BuyerModel").val(),
                        MeanSize:$("#MeanSize").val(),
                        Date:$("#Date").val(),
                        LastBomNoEntered:$("#LastBomNoEntered").val(),
                        LinkBomNo:$("#LinkBomNo").val(),
                        CommnBOM1:$("#CommnBOM1").val(),
                        CommnBOM2:$("#CommnBOM2").val(),
                        CommnBOM3:$("#CommnBOM3").val(),
                        CommnBOM4:$("#CommnBOM4").val(),
                        CommnBOM5:$("#CommnBOM5").val(),
                        OurNorms:$("#OurNorms").val(),
                        OurNormsPercent:$("#OurNormsPercent").val(),
                        PurchaseNorms:$("#PurchaseNorms").val(),
                        PurchaseNormsPercent:$("#PurchaseNormsPercent").val(),
                        IssueNorms:$("#IssueNorms").val(),
                        IssueNormsPercent:$("#IssueNormsPercent").val(),
                        CostingNorms:$("#CostingNorms").val(),
                        CostingNormsPercent:$("#CostingNormsPercent").val(),

                    },
                    success: function (data) {
                        $('.loader-overlay').fadeOut('fast');

                        $("#BomNo").val(data.Model.BomNo);
                        $("#Description").val(data.Model.Description);
                        $("#BuyerMasterId").val(data.Model.BuyerMasterId);
                        $("#BuyerMasterId").val(data.Model.BuyerMasterId);
                        $("#BuyerModel").val(data.Model.BuyerModel);
                        $("#MeanSize").val(data.Model.MeanSize);
                        //$("#ParentBomNo").val(data.Model.ParentBomNo);
                        $("#LinkBomNo").val(data.Model.LinkBomNo);
                        $("#BuyerNorms").val(data.Model.BuyerNorms);
                        $("#NoOfSets").val(data.Model.NoOfSets);
                        $("#UomMasterId").val(data.Model.UomMasterId);
                        $("#SupplierMasterId").val(data.Model.SupplierMasterId);
                        $("#SizeScheduleMasterId").val(data.Model.SizeScheduleMasterId);
                        $("#SizeRangeMasterId").val(data.Model.SizeRangeMasterId);

                        var rowCount = 0;

                        $('#list-materials tbody').html("")
                        $.each(data.Material, function (index, item) {
                            $('#list-materials tbody').append("<tr id=rowid_" + item.BOMMaterialID + "> <td style='display:none' class='BomNo'><span>" + item.BOMMaterialID + "</span><input type='hidden' class='BomId' id='bomid' value='" + item.BomId + "'></td>" +
                                                                                                                    //"<td class='Description'><span>" + $('#Description').val() + "</span><input type='hidden' class='AddRowFields' value='" + $("#AddRowFields").val() + "'></td>" +
                                                                                                                    "<td style='display:none' class='Description' id='Description'>" + item.Description + "</td>" +
                                                                                                                    "<td style='display:none' class='BuyerMasterId' id='BuyerMasterId'>" + item.BuyerMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='BuyerModel' id='BuyerModel'>" + item.BuyerModel + "</td>" +
                                                                                                                    "<td style='display:none' class='MeanSize' id='MeanSize'>" + item.MeanSize + "</td>" +
                                                                                                                    "<td style='display:none' class='Date' id='Date'>" + item.Date + "</td>" +
                                                                                                                    "<td style='display:none' class='ParentBomNo' id='ParentBomNo'>" + item.ParentBomNo + "</td>" +
                                                                                                                    "<td style='display:none' class='LinkBomNo' id='LinkBomNo'>" + item.LinkBomNo + "</td>" +
                                                                                                                    "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + item.CommnBOM1 + "</td>" +
                                                                                                                    "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + item.BOMMaterialID + "</td>" +
                                                                                                                       " <td title='Delete'  style='cursor:pointer;' class='Delete' id='Delete'><input type='button' value='Delete' style='cursor:pointer;' title='Delete' onclick='Deletebtn(" + item.BOMMaterialID+")' class='change_status change_status_  Delete input Edit input' /></td>" +
                                                                                                                          " <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><input type='button' value='Edit' style='cursor:pointer;' title='Edit'  onclick='RowClick(" + item.BOMMaterialID + "," + item.BomId + ")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                                   //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href='data.Material.BOMMaterialID'>Delete</a></td>" +
                                                                                                                   // "<td><input type='button' value='Edit' onclick='RowClick(" + item.BOMMaterialID + "," + item.BomId + ")' class='' />" + "</td>" +
                                                                                                                   // " <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href='data.Material.BOMMaterialID'>Delete</a></td>" +
                                                                                                                    "<td class='MaterialCategoryMasterId_' id='MaterialCategoryMasterId_'>" + item.CategoryName + "</td>" +
                                                                                                                    "<td class='MaterialName_' id='MaterialName_'>" + item.MaterialName + "</td>" +
                                                                                                                    "<td class='MaterialGroupMasterId_' id='MaterialGroupMasterId_'>" + item.GroupName + "</td>" +
                                                                                                                    "<td class='ColorMasterId_' id='ColorMasterId_'>" + item.ColorName + "</td>" +
                                                                                                                    "<td class='SubstanceMasterId_' id='SubstanceMasterId_'>" + item.SubtranceRangeName + "</td>" +
                                                                                                                    // "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + item.SizeScheduleMasterId + "</td>" +
                                                                                                                    "<td class='SampleNorms' id='SampleNorms'>" + item.SampleNorms + "</td>" +
                                                                                                                    //"<td class='Wastage' id='Wastage'>" + item.Wastage + "</td>" +
                                                                                                                    //"<td class='WastageQty' id='WastageQty'>" + item.WastageQty + "</td>" +
                                                                                                                         "<td class='WastageQtyUOM' id='WastageQtyUOM'>" + item.WastageqtyUOM + "</td>" +
                                                                                                                    "<td class='TotalNorms' id='TotalNorms'>" + item.TotalNorms + "</td>" +
                                                                                                                    //"<td class='TotalNormsUOM' id='TotalNormsUOM'>" + item.TotalNormsUOM + "</td>" +
                                                                                                                    "<td style='display:none' class='ComponentName' id='ComponentName'>" + item.ComponentName + "</td>" +
                                                                                                                    "<td style='display:none' class='BuyerNorms' id='BuyerNorms'>" + item.BuyerNorms + "</td>" +
                                                                                                                    "<td style='display:none' class='NoOfSets' id='NoOfSets'>" + item.NoOfSets + "</td>" +
                                                                                                                    "<td style='display:none' class='UomMasterId' id='UomMasterId'>" + item.UomMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='SupplierMasterId' id='SupplierMasterId'>" + item.SupplierMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='SizeRangeMasterId' id='SizeRangeMasterId'>" + item.SizeRangeMasterId + "</td>" +
                                           // "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.BOM.SizeScheduleMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='OurNorms' id='OurNorms'>" + item.OurNorms + "</td>" +
                                                                                                                    "<td style='display:none' class='OurNormsPercent' id='OurNormsPercent'>" + item.OurNormsPercent + "</td>" +
                                                                                                                    "<td style='display:none' class='PurchaseNorms' id='PurchaseNorms'>" + item.PurchaseNorms + "</td>" +
                                                                                                                    "<td style='display:none' class='PurchaseNormsPercent' id='PurchaseNormsPercent'>" + item.PurchaseNormsPercent + "</td>" +
                                                                                                                    "<td style='display:none' class='IssueNorms' id='IssueNorms'>" + item.IssueNorms + "</td>" +
                                                                                                                    "<td style='display:none' class='IssueNormsPercent' id='IssueNormsPercent'>" + item.IssueNormsPercent + "</td>" +
                                                                                                                    "<td style='display:none' class='CostingNorms' id='CostingNorms'>" + item.CostingNorms + "</td>" +
                                                                                                                     "<td style='display:none' class='Conversion' id='Conversion'>" + item.Conversion + "</td>" +
                                                                                                                     "<td style='display:none'class='MaterialCategoryMasterId' id='MaterialCategoryMasterId'>" + item.MaterialCategoryMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='MaterialName' id='MaterialName'>" + item.MaterialName + "</td>" +
                                                                                                                    "<td style='display:none' class='MaterialGroupMasterId' id='MaterialGroupMasterId'>" + item.MaterialGroupMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='ColorMasterId' id='ColorMasterId'>" + item.ColorMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='SubstanceMasterId' id='SubstanceMasterId'>" + item.SubstanceMasterId + "</td>" +
                                                                                                                    "<td style='display:none' class='CostingNormsPercent' id='CostingNormsPercent'>" + item.CostingNormsPercent + "</td>" +
                                                                                                                    "<td style='display:none' class='PreparedBy' id='PreparedBy'>" + item.PreparedBy + "</td>" +
                                                                                                                    "<td style='display:none' class='WastageQtyUOM' id='WastageQtyUOM'>" + item.WastageQtyUOM + "</td>" +
                                                                                                                    "<td style='display:none' class='VerifiedBy' id='VerifiedBy'>" + item.VerifiedBy + "</td>" +
                                                                                                                    "<td style='display:none' class='CheckedBy' id='CheckedBy'>" + item.CheckedBy + "</td>"
                                                                                                                    + "<td style='display:none' class='AddRowFields'>" + $('#AddRowFields').val() + "</td></tr>");
                            // $('#BOMMaterialID').val(data.Material.BOMMaterialID);
                            rowCount++;
                        });

                        // $("#NoOfSets").val(data.Model.NoOfSets);
                    }
                });
            }


        }
    });
});
function Cancel() {
    location.href = "../BillOfMaterial/BOMMaterialListGrid";
}

function isNumberKey(evt) {
    
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

$("#Conversion").blur(function () {
    var Total_buyerNormsPercentage = 0.00;
    var style = $('#Conversion').val();
    if (style != "") {
        var ConvertedNorms = parseFloat(style) * 10.76;
        ConvertedNorms = ConvertedNorms.toFixed(4);
        var ourNormsPercentage = $("#OurNormsPercent").val();
        var buyerNormsPercentage = ((parseFloat(ConvertedNorms) / 100) * parseFloat(ourNormsPercentage));
        $('#BuyerNorms').val(ConvertedNorms);
        if ($('#OurNorms option:selected').text().trim() == "Increase") {
            Total_buyerNormsPercentage = parseFloat(ConvertedNorms) + parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
        else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
            Total_buyerNormsPercentage = parseFloat(ConvertedNorms) - parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
    }
});
$("#SampleNorms").blur(function () {
    var Total_buyerNormsPercentage = 0.00;
    var ConvertedNorms = $('#SampleNorms').val();
    if (ConvertedNorms != "") {
        var ourNormsPercentage = $("#OurNormsPercent").val();
        var buyerNormsPercentage = ((parseFloat(ConvertedNorms) / 100) * parseFloat(ourNormsPercentage));
        $('#BuyerNorms').val(buyerNormsPercentage.toFixed(4));
        if ($('#OurNorms option:selected').text().trim() == "Increase") {
            Total_buyerNormsPercentage = parseFloat(ConvertedNorms) + parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
        else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
            Total_buyerNormsPercentage = parseFloat(ConvertedNorms) - parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
    }
});
$("#TotalNorms").blur(function () {
    if($("#MaterialCategoryMasterId option:selected").text()=="Leathers")
    {

    }
    else{
        $("#Conversion").val('');
        $("#BuyerNorms").val('');
    }
       
});
$("#BuyerNorms").blur(function () {
      
    var Total_buyerNormsPercentage = 0.00;
    var ConvertedNorms = $('#BuyerNorms').val();
    if (ConvertedNorms != "") {
        $('#TotalNorms').val('');
        var ourNormsPercentage = $("#OurNormsPercent").val();
        var buyerNormsPercentage = ((parseFloat(ConvertedNorms) / 100) * parseFloat(ourNormsPercentage));
        if ($('#OurNorms option:selected').text().trim() == "Increase") {
            Total_buyerNormsPercentage = parseFloat(ConvertedNorms) + parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
        else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
            Total_buyerNormsPercentage = parseFloat(ConvertedNorms) - parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
    }
});
$("#IssueNormsPercent").blur(function () { 
      
    var endurea= '@(System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString())';    
      
    if(hostname==endurea)
    {
        var Total_buyerNormsPercentage =0;
        var norms=  $("#IssueNormsPercent").val();
        var buyernorms=  $("#BuyerNorms").val();
        var buyerNormsPercentage = ((parseFloat(buyernorms) / 100) * parseFloat(norms));
        if ($('#IssueNorms option:selected').text().trim() == "Increase") {
            Total_buyerNormsPercentage = parseFloat(buyernorms) + parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
        else if ($('#IssueNorms option:selected').text().trim() == "Decrease") {
            Total_buyerNormsPercentage = parseFloat(buyernorms) - parseFloat(buyerNormsPercentage);
            $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
        }
        else if ($('#IssueNorms option:selected').text().trim() == "Same") {                
            $('#TotalNorms').val(parseFloat(buyernorms).toFixed(4));
        }
    }
        

});
$("#BomNo").blur(function () {
    
    var style = $('#BomNo').val();
    var letterNumber = /^[a-zA-Z-,]+(\s{0,1}[a-zA-Z-, ])*$/;
   

    if((style.match(letterNumber)))
    {
       
        $('#Description').val(style);
        $("#LinkBomNo").val("");
        $("#LinkBomNo").prop('disabled', true);
    }
    else{
        $("#LinkBomNo").prop('disabled', false);
       
        $.ajax({
            type: 'GET',
            url:'/BillOfMaterial/CheckBomNo' ,
            data: { BomNo: style },
            success: function (data) {
                if (data.Message == "Already Existed") {
                    $('#Description').val(style);
                    $("#LinkBomNo").val("");
                    $("#add-Material").hide();
                    $("#BtnSave").hide();
                    alert("Already Existed please try another BOM.");
                    return false;
                }
                else {
                    if (data.productStyleMaster != null) {
                        $("#LinkBomNo").val(data.productStyleMaster.ProductOrBuyerStyleId);
                    }
                    else {
                        $("#LinkBomNo").val("");
                    }
                    $('#Description').val(style);
                    $("#add-Material").show();
                    $("#BtnSave").show();
                }

            },
        });
    }
});

  
$(function () {
    
    $("#MaterialCategoryMasterId").combobox({
        select: function () {
           
            var MaterialCategoryMasterId_ = $('#MaterialCategoryMasterId').val();
            $.ajax({
                url: '/MaterialMaster/FillCateogoryid',
                type: "GET",
                dataType: "JSON",
                data: { MaterialCategoryMasterId: MaterialCategoryMasterId_ },
                success: function (cities) {
                    $("#MaterialGroupMasterId").html(""); // clear before appending new list
                    $.each(cities, function (i, city) {
                        $("#MaterialGroupMasterId").append(
                            $('<option></option>').val(city.MaterialGroupMasterId).html(city.GroupName));
                    });
                }
            });
        }
    });
});

$(function () {
    $("#MaterialGroupMasterId").combobox({
        select: function() {
            var MaterialGroupMasterId_ = $('#MaterialGroupMasterId').val();
            var groupname=  $('#MaterialGroupMasterId option:selected').text();
            $.ajax({
                url: '/MaterialMaster/FillMaterialName',
                type: "GET",
                dataType: "JSON",
                data: { MaterialGroupMasterId: MaterialGroupMasterId_ },
                success: function (cities) {
                    hostname="";
                    hostname="http://"+cities.host;                       
                    var endurea= '@(System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString())';
                      
                    if (cities.IsSubstance == true) {
                        $("#SubstanceMasterId").prop('disabled', false);
                    }
                    else {
                        $("#SubstanceMasterId").prop('disabled', true);
                        $('#SubstanceMasterId').val("");
                    }
                    if (cities.IsSize == false) {
                        $("#SizeRangeMasterId").prop('disabled', true);
                        $("#SizeScheduleMasterId").prop('disabled', true);
                        $("#SizeRangeMasterId").val("");
                        $("#SizeScheduleMasterId").val("");
                    }
                    else {
                        $("#SizeRangeMasterId").prop('disabled', false);
                        $("#SizeScheduleMasterId").prop('disabled', false);
                    }
                    if (cities.normDetails != null && cities.normDetails.BuyerNameid == parseInt($('#BuyerMasterId').val())) {
                        $('#TotalNorms').val("");
                        $("#OurNormsPercent").prop("disabled", true);
                        $("#CostingNormsPercent").prop("disabled", true);
                        $("#PurchaseNormsPercent").prop("disabled", true);
                        $("#IssueNormsPercent").prop("disabled", true);

                        $("#OurNorms").val(cities.normDetails.OurNorms);
                        $("#OurNormsPercent").val(cities.normDetails.NormsPercentage);

                        $("#PurchaseNorms").val(cities.normDetails.PurchaseNormsid);
                        $("#PurchaseNormsPercent").val(cities.normDetails.NormsPercentage1);

                        $("#IssueNorms").val(cities.normDetails.IssueNormsid);
                        $("#IssueNormsPercent").val(cities.normDetails.NormsPercentage2);

                        $("#CostingNorms").val(cities.normDetails.CostingNorms);
                        $("#CostingNormsPercent").val(cities.normDetails.NormsPercentage3);
                        if (cities.normDetails.BuyerOption == 1) {
                            $("#Conversion").prop("disabled", false);


                            $("#BuyerNorms").prop("disabled", false);
                            $("#TotalNorms").prop("disabled", true);
                            $("#SampleNorms").prop("disabled", true);
                            var ourNormsPercentage = $("#OurNormsPercent").val();
                            var BuyerNorms = $('#BuyerNorms').val();
                            var buyerNormsPercentage = ((parseFloat(ConvertedNorms) / 100) * parseFloat(ourNormsPercentage));
                            if ($('#OurNorms option:selected').text().trim() == "Increase") {
                                Total_buyerNormsPercentage = parseFloat(ConvertedNorms) + parseFloat(buyerNormsPercentage);
                                $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                            }
                            else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
                                Total_buyerNormsPercentage = parseFloat(ConvertedNorms) - parseFloat(buyerNormsPercentage);
                                $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                            }
                            $('#TotalNorms').val("");
                        }
                        else if (cities.normDetails.BuyerOption == 2) {
                            $('#TotalNorms').val("");
                            $("#SampleNorms").prop("disabled", false);
                            var ourNormsPercentage = $("#OurNormsPercent").val();
                            var ConvertedNorms = $('#SampleNorms').val();
                            if (ConvertedNorms != "") {
                                var buyerNormsPercentage = ((parseFloat(ConvertedNorms) / 100) * parseFloat(ourNormsPercentage));
                                $("#BuyerNorms").val(buyerNormsPercentage.toFixed(4));
                                if ($('#OurNorms option:selected').text().trim() == "Increase") {
                                    Total_buyerNormsPercentage = parseFloat(ConvertedNorms) + parseFloat(buyerNormsPercentage);
                                    $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                                }
                                else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
                                    Total_buyerNormsPercentage = parseFloat(ConvertedNorms) - parseFloat(buyerNormsPercentage);
                                    $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                                }
                            }
                            $("#TotalNorms").prop("disabled", true);
                            if(hostname!=null &&hostname==endurea &&groupname=="Upper Leather")
                            {
                                $("#BuyerNorms").prop("disabled", false);
                            }
                            else
                            {
                                $("#BuyerNorms").prop("disabled", true);
                            }

                        }
                        else if (cities.normDetails.BuyerOption == 3) {

                            $("#BuyerNorms").val("");
                            $("#Conversion").prop("disabled", true);
                            $("#SampleNorms").prop("disabled", true);
                            if(hostname!=null &&hostname==endurea &&groupname=="Upper Leather")
                            {

                                $("#BuyerNorms").prop("disabled", false);
                            }
                            else
                            {
                                $("#BuyerNorms").prop("disabled", true);
                            }
                            $("#TotalNorms").prop("disabled", false);
                        }
                    }
                    else if (cities.normDetails != null && cities.normDetails.BuyerNameid != parseInt($('#BuyerMasterId').val())) {

                        $("#BuyerNorms").val("");
                        $("#SampleNorms").val("");
                        $("#Conversion").prop("disabled", true);
                        $("#SampleNorms").prop("disabled", true);
                        $("#TotalNorms").prop("disabled", false);
                        if(hostname!=null && hostname==endurea && groupname=="Upper Leather")
                        {
                            $("#BuyerNorms").prop("disabled", false);
                        }
                        else
                        {
                            $("#BuyerNorms").prop("disabled", true);
                        }

                    }
                    else if (cities.normDetails == null) {

                        $("#BuyerNorms").val("");
                        $("#SampleNorms").val("");
                        $("#Conversion").prop("disabled", true);
                        $("#SampleNorms").prop("disabled", true);
                        $("#TotalNorms").prop("disabled", false);
                        if(hostname!=null&&hostname==endurea && groupname=="Upper Leather")
                        {
                            $("#BuyerNorms").prop("disabled", false);
                        }
                        else
                        {
                            $("#BuyerNorms").prop("disabled", true);
                        }
                    }                     
                    if(hostname!=null &&hostname==endurea && groupname=="Upper Leather")
                    {                           
                        $("#BuyerNorms").prop("disabled", false);
                    }
                    else
                    {                           
                        $("#BuyerNorms").prop("disabled", true);
                    }
                    $("#MaterialName").html(""); // clear before appending new list
                    $.each(cities.Material, function (i, city) {
                        $("#MaterialName").append(
                            $('<option></option>').val(city.Value).html(city.Text));
                    });
                }
            });
        }
    });
});
$(function () {

    $("#MaterialName").combobox({
       
        select: function() {

            var MaterialGroupMasterId_ = $('#BuyerMasterId').val();
            if(MaterialGroupMasterId_==""||MaterialGroupMasterId_==0)
            {
                $('#MaterialName').val("");                          
                $("#MaterialName").combobox('destroy');
                $("#MaterialName").combobox();
                alert("Please select buyername");
                return false;
            }
            var materialId = $('#MaterialName').val();
            $.ajax({
                url: '/MaterialMaster/FillMaterialNameBasedonColor',
                type: "GET",
                dataType: "JSON",
                data: { MaterialNameID: materialId,buyername:$("#BuyerMasterId").val() },
                success: function (cities) {                        
                    $.each(cities.distinctList, function (i, city) {

                        if($('#SubstanceMasterId').prop('disabled')==false)
                        {
                            $('#SubstanceMasterId').val(city.SubstanceMasterId);
                        }
                        else{
                            $('#SubstanceMasterId').prop('disabled',true);
                        }                           
                        $('#MaterialCategoryMasterId').val(city.MaterialCategoryMasterId);                          
                        $("#MaterialCategoryMasterId").combobox('destroy');
                        $("#MaterialCategoryMasterId").combobox();
                        $('#MaterialGroupMasterId').val(city.MaterialGroupMasterId);                         
                        $("#MaterialGroupMasterId").combobox('destroy');
                        $("#MaterialGroupMasterId").combobox();    
                        document.getElementById("ColorMasterId").disabled = true;
                        if (city.SizeRangeMasterId != null && city.SizeRangeMasterId != 0 && $('#SizeRangeMasterId').prop('disabled') == false) {
                            $('#SizeRangeMasterId').val(city.SizeRangeMasterId);                              
                        }
                        else if (city.SizeRangeMasterId==null||city.SizeRangeMasterId==0) {
                            $("#SizeRangeMasterId").prop('disabled', true);
                            $("#SizeScheduleMasterId").prop('disabled', true);
                            $("#SizeRangeMasterId").val("");
                            $("#SizeScheduleMasterId").val("");
                        }
                        else {
                            $("#SizeRangeMasterId").prop('disabled', false);
                            $("#SizeScheduleMasterId").prop('disabled', false);
                        }
                        //else if(city.SizeRangeMasterId==null)
                        //{
                        //    $('#SizeRangeMasterId').val("");
                        //    $("#SizeScheduleMasterId").val("");
                        //    $("#SizeRangeMasterId").prop('disabled', true);
                        //    $("#SizeScheduleMasterId").prop('disabled', true);
                        //}
                        //else if(city.SizeRangeMasterId==null||$('#SizeRangeMasterId').prop('disabled')==true) {
                        //    $("#SizeRangeMasterId").prop('disabled', true);
                        //    $("#SizeScheduleMasterId").prop('disabled', true);
                        //    $("#SizeRangeMasterId").val("");
                        //    $("#SizeScheduleMasterId").val("");
                        //}                         
                        $('#ColorMasterId').val(city.ColorMasterId);                           
                        $('#WastageQtyUOM').val(city.Uom);                           
                    });
                    if(cities.materialCategoryMaster!=null && cities.materialCategoryMaster.CategoryName=="Leathers")
                    {                           
                        $("#Conversion").prop("disabled", false);
                        $("#BuyerNorms").prop("disabled", false);
                        $("#TotalNorms").prop("disabled", false);
                        $("#SampleNorms").prop("disabled", true);
                        $("#SizeRangeMasterId").prop('disabled', true);
                        $("#SizeScheduleMasterId").prop('disabled', true);
                        $("#SizeRangeMasterId").val("");
                        $("#SizeScheduleMasterId").val("");
                        $('#SubstanceMasterId').prop('disabled', true);
                        $('#SubstanceMasterId').val(city.SubstanceMasterId);
                    }
                    else{                           
                        $("#Conversion").prop("disabled", true);
                        $("#BuyerNorms").prop("disabled", false);
                        $("#TotalNorms").prop("disabled", false);
                        $("#SampleNorms").prop("disabled", true);
                        $("#SizeRangeMasterId").prop('disabled', false);
                        $("#SizeScheduleMasterId").prop('disabled', false); 
                        $('#SubstanceMasterId').prop('disabled', true);
                    }
                    if(cities.norms!=null)
                    {
                        $('#TotalNorms').val("");  
                        $("#OurNorms").val(cities.norms.OurNorms);
                        $("#OurNormsPercent").val(cities.norms.NormsPercentage);
                        $("#PurchaseNorms").val(cities.norms.PurchaseNormsid);
                        $("#PurchaseNormsPercent").val(cities.norms.NormsPercentage1);
                        $("#IssueNorms").val(cities.norms.IssueNormsid);
                        $("#IssueNormsPercent").val(cities.norms.NormsPercentage2);
                        $("#CostingNorms").val(cities.norms.CostingNorms);
                        $("#CostingNormsPercent").val(cities.norms.NormsPercentage3);                                
                    }

                }
            });
        }
    });
});


$(document).on('click', '#add-Material', function () {

    var style = $('#BomNo').val();
    var letterNumber = /^[a-zA-Z-,]+(\s{0,1}[a-zA-Z-, ])*$/;
    if((style.match(letterNumber)))
    {
        $('#Description').val(style);
        $("#LinkBomNo").val("");
        $("#LinkBomNo").prop('disabled', true);
    }
    else{
        $("#LinkBomNo").prop('disabled', false);
    }
    validation();
    if (validation() != false) {
        $('#BtnSave').hide();
        if (confirm("Are you sure you want to add material new this?")) {
            $('#BOMMaterialID').val("");

        }
        else {
            // return false;
        }
        var conversion_= $("#Conversion").val();
        $("#Conversion").val("");
        var rowCount = $('#list-materials tr').length;
        var systemUserValue = '';
        var Sno = '';
        var compnt = '';
        var _length = '';
        var width = '';
        var nos = '';
        var samplenms = '';
        var wstg = '';
        var wstqty = '';
        var wsttotalNorms = '';
        $('#Addrowfields tbody tr').each(function () {
            Sno = '';
            compnt = '';
            _length = '';
            width = '';
            nos = '';
            samplenms = '';
            wstg = '';
            wstqty = '';
            wsttotalNorms = '';
            $(this).find("td").each(function () {
                Sno = $('#Sno').val();
                if (Sno != undefined && Sno.trim().length > 0) {
                    systemUserValue += Sno + ',';
                }
                compnt = $(this).find('#Component').val();
                if (compnt != undefined && compnt.trim().length > 0) {
                    systemUserValue += compnt + ',';
                }
                _length = $(this).find('#length').val();
                if (_length != undefined && _length.trim().length > 0) {
                    systemUserValue += _length + ',';
                }
                width = $(this).find('#width').val();
                if (width != undefined && width.trim().length > 0) {
                    systemUserValue += width + ',';

                }
                nos = $(this).find('#Nos').val();
                if (nos != undefined && nos.trim().length > 0) {
                    systemUserValue += nos + ',';
                }

                samplenms = $(this).find('#SampleNorms').val();
                if (samplenms != undefined && samplenms.trim().length > 0) {
                    systemUserValue += samplenms + ',';
                }
                wstg = $(this).find('#WastagePercent').val();
                if (wstg != undefined && wstg.trim().length > 0) {
                    systemUserValue += wstg + ',';
                }
                wstqty = $(this).find('#WastageQty').val();
                if (wstqty != undefined && wstqty.trim().length > 0) {
                    systemUserValue += wstqty + ',';
                }
                wsttotalNorms = $(this).find('#TotalNormsQty').val();
                if (wsttotalNorms != undefined && wsttotalNorms.trim().length > 0) {
                    systemUserValue += wsttotalNorms;
                }

            });
            systemUserValue += '#';

        });
        var form = systemUserValue;
        $('#AddRowFields').val(form);
        $('#BOMMaterialID').val($('#BOMMaterialID').val())
        var CommnBOM = "";
        if ($('#CommnBOM1').val() != "") {
            CommnBOM = $('#CommnBOM1').val();
        }
        if ($('#CommnBOM2').val() != "") {
            CommnBOM += "," + $('#CommnBOM2').val();
        }
        if ($('#CommnBOM3').val() != "") {
            CommnBOM += "," + $('#CommnBOM3').val();
        }
        if ($('#CommnBOM4').val() != "") {
            CommnBOM += "," + $('#CommnBOM4').val(); S
        }
        if ($('#CommnBOM5').val() != "") {
            CommnBOM += "," + $('#CommnBOM5').val();
        }
        //Display size range get implementation
        var CheckBoxIschecked_ = "";
        $('#sizeRangeTable tbody tr').find('.checked_checkbox').each(function () {
            CheckBoxIschecked_ += $(this).is(':checked') + ",";
        });
        CheckBoxIschecked_ = CheckBoxIschecked_.slice(0, -1);
        var CheckBoxsize_ = "";
        $('#sizeRangeTable tbody tr').find('.SizeRangeRefValue').each(function () {
            CheckBoxsize_ += $(this).text().trim() + ",";
        });
        $("#OurNorms").prop('disabled', false);
        $("#OurNormsPercent").prop('disabled', false);
        $("#PurchaseNorms").prop('disabled', false);
        $("#PurchaseNormsPercent").prop('disabled', false);
        $("#IssueNorms").prop('disabled', false);
        $("#IssueNormsPercent").prop('disabled', false);
        $("#CostingNorms").prop('disabled', false);
        $("#CostingNormsPercent").prop('disabled', false);
        CheckBoxsize_ = CheckBoxsize_.slice(0, -1);
        var rowCount = $('#list-materials tr').length - 1;
        if (rowCount == 0) {
            if (validation()) {
                $('.loader-overlay').show();
                $('.loader-overlay').fadeIn('slow');
                var forms_ = $(".form-inline").serialize() + '&BOMMaterialID=' + $('#BOMMaterialID').val()+'&CheckBoxsize='+CheckBoxsize_+'&CheckBoxIschecked='+CheckBoxIschecked_+ '&ParentBomNo=' + $('#ParentBomNo').val()+'&SizeScheduleMasterId='+ $("#SizeScheduleMasterId").val();
                if ($("#list-materials tbody tr").length == 0) {
                    $.ajax({
                        type: 'POST',
                        url: '/BillOfMaterial/BillOfMaterialDetails',
                        data: {
                            forms: forms_,
                            Grid: form,
                            BOMMaterialID_: $('#BOMMaterialID').val(),
                            BomGridlst: '@Model.BomGridlst',
                            CheckBoxsize:CheckBoxsize_,
                            CheckBoxIschecked:CheckBoxIschecked_
                        },
                        success: function (data) {
                            $("#OurNorms").prop('disabled', true);
                            $("#OurNormsPercent").prop('disabled', true);
                            $("#PurchaseNorms").prop('disabled', true);
                            $("#PurchaseNormsPercent").prop('disabled', true);
                            $("#IssueNorms").prop('disabled', true);
                            $("#IssueNormsPercent").prop('disabled', true);
                            $("#CostingNorms").prop('disabled', true);
                            $("#CostingNormsPercent").prop('disabled', true);
                            $('.loader-overlay').fadeOut('fast');
                            $('.loader-overlay').hide();
                            if (data.AlertMessage == "Already Existed") {
                                alert("Already  Existed this BOM.");
                                return false;
                            }
                            else if (data.AlertMessage == "Saved Successfully") {
                                alert("Saved Successfully.");
                                if (data.BOM.BomId != 0) {
                                    $(this).addClass("curChangeableRow"),
                                   $(this).siblings().removeClass("curChangeableRow"),
                                    $("#materialCategory .custom-combobox input").val('');                                      
                                    $("#Addrowfields > tbody").html("");
                                    $("#materialGroupID_ .custom-combobox input").val('');

                                    $("#MaterialCategoryMasterId").click(function () {
                                        $("#MaterialCategoryMasterId").toggle();
                                    });
                                    $("#MaterialGroupMasterId").click(function () {
                                        $("#MaterialGroupMasterId").toggle();
                                    });
                                    $("#MaterialName").click(function () {
                                        $("#MaterialName").toggle();
                                    });
                                    $("#MaterialCategoryMasterId").combobox();
                                    $("#MaterialGroupMasterId").combobox();
                                    $("#MaterialName").combobox();
                                    $("#SizeScheduleMasterId").val('');
                                    $("#SubstanceMasterId").val('');
                                    $("#masterialName_ .custom-combobox input").val('');
                                    $("#ColorMasterId").val('');
                                    $("#SampleNorms").val('');
                                    $("#Wastage").val('');
                                    $("#WastageQty").val('');
                                    $("#WastageQtyUOM").val('');
                                    $("#TotalNorms").val('');
                                    $("#TotalNormsUOM").val('');
                                    $("#ComponentName").val('');
                                    // $("#NoOfSets").val('');
                                    $("#SupplierMasterId").val('');
                                    $("#BuyerNorms").val('');
                                    $("#UomMasterId").val('');
                                    $("#SizeRangeMasterId").val('');
                                    $("#SizeScheduleMasterId").val('');
                                    $("#OurNorms").val('');
                                    $("#PurchaseNorms").val('');
                                    $("#IssueNorms").val('');
                                    $("#CostingNorms").val('');
                                    $("#OurNormsPercent").val('');
                                    $("#PurchaseNormsPercent").val('');
                                    $("#IssueNormsPercent").val('');
                                    $("#CostingNormsPercent").val('');
                                    $("#Addrowfields > tbody").html("");
                                    //console.log(data.Material);
                                    //console.log(data.Material.MaterialCategoryMasterId);
                                    var BOMMateriid = data.Material.BOMMaterialID;
                                    var BOMid = data.BOM.BomId;
                                    //'callChange(" + SnoVal + ")'
                                    if(data.Material.SubtranceRangeName==null)
                                    {
                                        data.Material.SubtranceRangeName="";
                                    }
                                    if(data.Material.SampleNorms==null)
                                    {
                                        data.Material.SampleNorms="";
                                    }

                                    $('#list-materials tbody').append("<tr id=" + rowCount + "><td style='display:none' class='BomNo'><span>" + data.Material.BOMMaterialID + "</span><input type='hidden' class='BomId' value='" + data.BOM.BomId + "'></td>" +
                                                                                                             //"<td class='Description'><span>" + $('#Description').val() + "</span><input type='hidden' class='AddRowFields' value='" + $("#AddRowFields").val() + "'></td>" +
                                                                                                             "<td style='display:none' class='Description' id='Description'>" + data.BOM.Description + "</td>" +
                                                                                                             "<td style='display:none' class='BuyerMasterId' id='BuyerMasterId'>" + data.BOM.BuyerMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='BuyerModel' id='BuyerModel'>" + data.BOM.BuyerModel + "</td>" +
                                                                                                             "<td style='display:none' class='MeanSize' id='MeanSize'>" + data.BOM.MeanSize + "</td>" +
                                                                                                             "<td style='display:none' class='Date' id='Date'>" + data.BOM.Date + "</td>" +
                                                                                                             "<td style='display:none' class='ParentBomNo' id='ParentBomNo'>" + data.BOM.ParentBomNo + "</td>" +
                                                                                                             "<td style='display:none' class='LinkBomNo' id='LinkBomNo'>" + data.BOM.LinkBomNo + "</td>" +
                                                                                                             "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + data.BOM.CommnBOM1 + "</td>" +
                                                                                                             "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + data.Material.BOMMaterialID + "</td>" +
                                                                                                              " <td title='Delete'  style='cursor:pointer;' class='Delete' id='Delete'><input type='fton' value='Delete' style='cursor:pointer;' title='Delete' onclick='Deletebtn(" + data.Material.BOMMaterialID+")' class='change_status change_status_  Delete input Edit input' /></td>" +
                                                                                                              " <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><input type='button' value='Edit' style='cursor:pointer;' title='Edit' onclick='RowClick(" + data.Material.BOMMaterialID + "," + data.BOM.BomId + ")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                            //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete' onclick='Delete(" + item.IndentMaterialID + ")'><a href='data.Material.BOMMaterialID'>Delete</a></td>" +
                                                                                                            // " <td title='Edit' style='cursor:pointer;' class='Edit' id='Edit'><a href='data.Material.BOMMaterialID'>Edit</a></td>" +
                                                                                                             "<td class='MaterialCategoryMasterId' id='MaterialCategoryMasterId'>" + data.Material.MaterialCategoryMasterId + "</td>" +
                                                                                                             "<td class='MaterialName' id='MaterialName'>" + data.Material.MaterialName + "</td>" +
                                                                                                             "<td class='MaterialGroupMasterId' id='MaterialGroupMasterId'>" + data.Material.MaterialGroupMasterId + "</td>" +
                                                                                                             "<td class='ColorMasterId' id='ColorMasterId'>" + data.Material.ColorMasterId + "</td>" +
                                                                                                             "<td class='SubstanceMasterId' id='SubstanceMasterId'>" + data.Material.SubstanceMasterId + "</td>" +
                                                                                                             "<td class='SampleNorms' id='SampleNorms'>" + data.Material.SampleNorms + "</td>" +
                                                                                                             "<td class='Wastage' id='Wastage'>" + data.Material.Wastage + "</td>" +
                                                                                                             "<td class='WastageQty' id='WastageQty'>" + data.Material.WastageQty + "</td>" +
                                                                                                             "<td class='WastageQtyUOM' id='WastageQtyUOM'>" + data.Material.WastageQtyUOM + "</td>" +
                                                                                                             "<td class='TotalNorms' id='TotalNorms'>" + data.Material.TotalNorms + "</td>" +
                                                                                                             "<td class='TotalNormsUOM' id='TotalNormsUOM'>" + data.Material.TotalNormsUOM + "</td>" +
                                                                                                             "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.Material.SizeScheduleMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='ComponentName' id='ComponentName'>" + data.BOM.ComponentName + "</td>" +
                                                                                                             "<td style='display:none' class='BuyerNorms' id='BuyerNorms'>" + data.BOM.BuyerNorms + "</td>" +
                                                                                                             "<td style='display:none' class='NoOfSets' id='NoOfSets'>" + data.BOM.NoOfSets + "</td>" +
                                                                                                             "<td style='display:none' class='UomMasterId' id='UomMasterId'>" + data.BOM.UomMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='SupplierMasterId' id='SupplierMasterId'>" + data.Material.SupplierMasterID + "</td>" +
                                                                                                             "<td style='display:none' class='SizeRangeMasterId' id='SizeRangeMasterId'>" + data.BOM.SizeRangeMasterId + "</td>" +
                                    // "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.BOM.SizeScheduleMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='OurNorms' id='OurNorms'>" + data.BOM.OurNorms + "</td>" +
                                                                                                             "<td style='display:none' class='OurNormsPercent' id='OurNormsPercent'>" + data.BOM.OurNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='PurchaseNorms' id='PurchaseNorms'>" + data.BOM.PurchaseNorms + "</td>" +
                                                                                                             "<td style='display:none' class='PurchaseNormsPercent' id='PurchaseNormsPercent'>" + data.BOM.PurchaseNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='IssueNorms' id='IssueNorms'>" + data.BOM.IssueNorms + "</td>" +
                                                                                                             "<td style='display:none' class='IssueNormsPercent' id='IssueNormsPercent'>" + data.BOM.IssueNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='CostingNorms' id='CostingNorms'>" + data.BOM.CostingNorms + "</td>" +
                                                                                                             "<td style='display:none' class='CostingNormsPercent' id='CostingNormsPercent'>" + data.BOM.CostingNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='PreparedBy' id='PreparedBy'>" + data.BOM.PreparedBy + "</td>" +
                                                                                                             "<td style='display:none' class='VerifiedBy' id='VerifiedBy'>" + data.BOM.VerifiedBy + "</td>" +
                                                                                                             "<td style='display:none' class='CheckedBy' id='CheckedBy'>" + data.BOM.CheckedBy + "</td>"
                                                                                                             + "<td style='display:none' class='AddRowFields'>" + $('#AddRowFields').val() + "</td></tr>");
                                    $('#BOMMaterialID').val(data.Material.BOMMaterialID)

                                }
                                return false;
                            }
                            else if (data.AlertMessage == "Updated Successfully") {
                                alert("within update success validation");
                                if (data.BOM.BomId != 0) {
                                    $(this).addClass("curChangeableRow"),
                                   $(this).siblings().removeClass("curChangeableRow"),
                                    $("#materialCategory .custom-combobox input").val('');
                                    $("#Addrowfields > tbody").html("");
                                    $("#materialGroupID_ .custom-combobox input").val('');                                        
                                    $("#SizeScheduleMasterId").val('');
                                    $("#SubstanceMasterId").val('');
                                    $("#masterialName_ .custom-combobox input").val('');
                                    $("#ColorMasterId").val('');
                                    $("#SampleNorms").val('');
                                    $("#Wastage").val('');
                                    $("#WastageQty").val('');
                                    $("#WastageQtyUOM").val('');
                                    $("#TotalNorms").val('');
                                    $("#TotalNormsUOM").val('');
                                    $("#ComponentName").val('');
                                    //$("#NoOfSets").val('');
                                    $("#SupplierMasterId").val('');
                                    $("#BuyerNorms").val('');
                                    $("#UomMasterId").val('');
                                    $("#SizeRangeMasterId").val('');
                                    $("#SizeScheduleMasterId").val('');
                                    $("#OurNorms").val('');
                                    $("#PurchaseNorms").val('');
                                    $("#IssueNorms").val('');
                                    $("#CostingNorms").val('');
                                    $("#OurNormsPercent").val('');
                                    $("#PurchaseNormsPercent").val('');
                                    $("#IssueNormsPercent").val('');
                                    $("#CostingNormsPercent").val('');
                                    $("#Addrowfields > tbody").html("");
                                    var BOMMateriid = data.Material.BOMMaterialID;
                                    alert("after update:" + BOMMateriid);
                                    alert("totalnorms" + data.Material.TotalNorms);
                                    var BOMid = data.BOM.BomId;
                                    $('#list-materials tbody').append("<tr id=" + rowCount + "><td style='display:none' class='BomNo'><span>" + data.Material.BOMMaterialID + "</span><input type='hidden' class='BomId' value='" + data.BOM.BomId + "'></td>" +
                                                                                                             //"<td class='Description'><span>" + $('#Description').val() + "</span><input type='hidden' class='AddRowFields' value='" + $("#AddRowFields").val() + "'></td>" +
                                                                                                             "<td style='display:none' class='Description' id='Description'>" + data.BOM.Description + "</td>" +
                                                                                                             "<td style='display:none' class='BuyerMasterId' id='BuyerMasterId'>" + data.BOM.BuyerMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='BuyerModel' id='BuyerModel'>" + data.BOM.BuyerModel + "</td>" +
                                                                                                             "<td style='display:none' class='MeanSize' id='MeanSize'>" + data.BOM.MeanSize + "</td>" +
                                                                                                             "<td style='display:none' class='Date' id='Date'>" + data.BOM.Date + "</td>" +
                                                                                                             "<td style='display:none' class='ParentBomNo' id='ParentBomNo'>" + data.BOM.ParentBomNo + "</td>" +
                                                                                                             "<td style='display:none' class='LinkBomNo' id='LinkBomNo'>" + data.BOM.LinkBomNo + "</td>" +
                                                                                                             "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + data.BOM.CommnBOM1 + "</td>" +
                                                                                                             "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + data.Material.BOMMaterialID + "</td>" +
                                                                                                              " <td title='Delete'  style='cursor:pointer;' class='Delete' id='Delete'><input type='button' value='Delete' style='cursor:pointer;' title='Delete' onclick='Deletebtn(" + data.Material.BOMMaterialID+")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                              " <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><input type='button' value='Edit' style='cursor:pointer;' title='Edit' onclick='RowClick(" + data.Material.BOMMaterialID + "," + data.BOM.BomId + ")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                            //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete' onclick='Delete(" + item.IndentMaterialID + ")'><a href='data.Material.BOMMaterialID'>Delete</a></td>" +
                                                                                                             //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href="+data.BOMMaterialID+" id="+data.BOMMaterialID+" class='change_status change_status_'>Delete</a></td>" +
                                                                                                             //" <td title='Edit' style='cursor:pointer;' class='Edit' id='Edit'><a href='data.Material.BOMMaterialID'>Edit</a></td>" +
                                                                                                             "<td class='MaterialCategoryMasterId' id='MaterialCategoryMasterId'>" + data.Material.MaterialCategoryMasterId + "</td>" +
                                                                                                             "<td class='MaterialName' id='MaterialName'>" + data.Material.MaterialName + "</td>" +
                                                                                                             "<td class='MaterialGroupMasterId' id='MaterialGroupMasterId'>" + data.Material.MaterialGroupMasterId + "</td>" +
                                                                                                             "<td class='ColorMasterId' id='ColorMasterId'>" + data.Material.ColorMasterId + "</td>" +
                                                                                                             "<td class='SubstanceMasterId' id='SubstanceMasterId'>" + data.Material.SubstanceMasterId + "</td>" +
                                                                                                             "<td class='SampleNorms' id='SampleNorms'>" + data.Material.SampleNorms + "</td>" +
                                                                                                             "<td class='Wastage' id='Wastage'>" + data.Material.Wastage + "</td>" +
                                                                                                             "<td class='WastageQty' id='WastageQty'>" + data.Material.WastageQty + "</td>" +
                                                                                                             "<td class='WastageQtyUOM' id='WastageQtyUOM'>" + data.Material.WastageQtyUOM + "</td>" +
                                                                                                             "<td class='TotalNorms' id='TotalNorms'>" + data.Material.TotalNorms + "</td>" +
                                                                                                             "<td class='TotalNormsUOM' id='TotalNormsUOM'>" + data.Material.TotalNormsUOM + "</td>" +
                                                                                                             "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.Material.SizeScheduleMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='ComponentName' id='ComponentName'>" + data.BOM.ComponentName + "</td>" +
                                                                                                             "<td style='display:none' class='BuyerNorms' id='BuyerNorms'>" + data.BOM.BuyerNorms + "</td>" +
                                                                                                             "<td style='display:none' class='NoOfSets' id='NoOfSets'>" + data.BOM.NoOfSets + "</td>" +
                                                                                                             "<td style='display:none' class='UomMasterId' id='UomMasterId'>" + data.BOM.UomMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='SupplierMasterId' id='SupplierMasterId'>" + data.Material.SupplierMasterID + "</td>" +
                                                                                                             "<td style='display:none' class='SizeRangeMasterId' id='SizeRangeMasterId'>" + data.BOM.SizeRangeMasterId + "</td>" +
                                    // "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.BOM.SizeScheduleMasterId + "</td>" +
                                                                                                             "<td style='display:none' class='OurNorms' id='OurNorms'>" + data.BOM.OurNorms + "</td>" +
                                                                                                             "<td style='display:none' class='OurNormsPercent' id='OurNormsPercent'>" + data.BOM.OurNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='PurchaseNorms' id='PurchaseNorms'>" + data.BOM.PurchaseNorms + "</td>" +
                                                                                                             "<td style='display:none' class='PurchaseNormsPercent' id='PurchaseNormsPercent'>" + data.BOM.PurchaseNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='IssueNorms' id='IssueNorms'>" + data.BOM.IssueNorms + "</td>" +
                                                                                                             "<td style='display:none' class='IssueNormsPercent' id='IssueNormsPercent'>" + data.BOM.IssueNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='CostingNorms' id='CostingNorms'>" + data.BOM.CostingNorms + "</td>" +
                                                                                                             "<td style='display:none' class='CostingNormsPercent' id='CostingNormsPercent'>" + data.BOM.CostingNormsPercent + "</td>" +
                                                                                                             "<td style='display:none' class='PreparedBy' id='PreparedBy'>" + data.BOM.PreparedBy + "</td>" +
                                                                                                             "<td style='display:none' class='VerifiedBy' id='VerifiedBy'>" + data.BOM.VerifiedBy + "</td>" +
                                                                                                             "<td style='display:none' class='CheckedBy' id='CheckedBy'>" + data.BOM.CheckedBy + "</td>"
                                                                                                             + "<td style='display:none' class='AddRowFields'>" + $('#AddRowFields').val() + "</td></tr>");
                                    $('#BOMMaterialID').val(data.Material.BOMMaterialID)
                                       
                                }
                                alert("Updated Successfully.");
                                return false;
                            }
                        }
                    });

                }
            }
        }
        else {

            var forms_ = $(".form-inline").serialize() + '&BOMMaterialID=' + $('#BOMMaterialID').val() + '&CheckBoxsize='+CheckBoxsize_ +'&CheckBoxIschecked='+CheckBoxIschecked_+ '&ParentBomNo=' + $('#ParentBomNo').val()+'&SizeScheduleMasterId='+ $("#SizeScheduleMasterId").val();
            $('.loader-overlay').show();
            $('.loader-overlay').fadeIn('fast');
            $("#OurNorms").prop('disabled', false);
            $("#OurNormsPercent").prop('disabled', false);
            $("#PurchaseNorms").prop('disabled', false);
            $("#PurchaseNormsPercent").prop('disabled', false);
            $("#IssueNorms").prop('disabled', false);
            $("#IssueNormsPercent").prop('disabled', false);
            $("#CostingNorms").prop('disabled', false);
            $("#CostingNormsPercent").prop('disabled', false);
            $.ajax({
                type: 'POST',
                url: '/BillOfMaterial/BillOfMaterialDetails',
                data: {
                   
                        Grid: form,
            forms: forms_,
            BOMMaterialID_: $('#BOMMaterialID').val(),
            BomId: $('#BomId').val(),
            BomNo: $('#BomNo').val(),
            Description: $('#Description').val(),
            BuyerMasterId: $('#BuyerMasterId').val(),
            BuyerModel: $("#BuyerModel").val(),
            MeanSize: $("#MeanSize").val(),
            Date: $("#Date").val(),
            ParentBomNo: $("#ParentBomNo").val(),
            LastBomNoEntered: $("#LastBomNoEntered").val(),
            LinkBomNo: $("#LinkBomNo").val(),
            Ammendment: $("#Ammendment").val(),
            CommnBOM1: $('#CommnBOM1').val(),
            CommnBOM2: $('#CommnBOM2').val(),
            CommnBOM3: $('#CommnBOM3').val(),
            CommnBOM4: $('#CommnBOM4').val(),
            CommnBOM5: $('#CommnBOM5').val(),
            PreparedBy: $("#PreparedBy").val(),
            VerifiedBy: $("#VerifiedBy").val(),
            CheckedBy: $("#CheckedBy").val(),
            MaterialName: $("#MaterialName").val(),
            MaterialCategoryMasterId: $("#MaterialCategoryMasterId").val(),
            MaterialGroupMasterId: $("#MaterialGroupMasterId").val(),
            ColorMasterId: $("#ColorMasterId").val(),
            SubstanceMasterId: $("#SubstanceMasterId").val(),
            SizeScheduleMasterId: $("#SizeScheduleMasterId").val(),
            SampleNorms: $("#SampleNorms").val(),
            Wastage: $("#Wastage").val(),
            SupplierMasterId: $("#SupplierMasterId").val(),
            UomMasterId: $("#UomMasterId").val(),
            SizeRangeMasterId: $("#SizeRangeMasterId").val(),
            // SizeScheduleMasterId: $("#SizeScheduleMasterId").val(),
            WastageQty: $("#WastageQty").val(),
            WastageQtyUOM: $("#WastageQtyUOM").val(),
            TotalNorms: $("#TotalNorms").val(),
            TotalNormsUOM: $("#TotalNormsUOM").val(),
            ComponentName: $("#ComponentName").val(),
            NoOfSets: $("#NoOfSets").val(),
            BuyerNorms: $("#BuyerNorms").val(),
            OurNorms: $("#OurNorms").val(),
            OurNormsPercent: $("#OurNormsPercent").val(),
            PurchaseNorms: $("#PurchaseNorms").val(),
            PurchaseNormsPercent: $("#PurchaseNormsPercent").val(),
            IssueNorms: $("#IssueNorms").val(),
            IssueNormsPercent: $("#IssueNormsPercent").val(),
            CostingNorms: $("#CostingNorms").val(),
            Conversion: conversion_,
            CostingNormsPercent: $("#CostingNormsPercent").val(),
            BomGridlst: '@Model.BomGridlst',
            CheckBoxsize:CheckBoxsize_,
            CheckBoxIschecked:CheckBoxIschecked_,
            HiddenParentBOMID: $("#HiddenParentBOMID").val()

        },
        success: function (data) {
            $("#OurNorms").prop('disabled', true);
            $("#OurNormsPercent").prop('disabled', true);
            $("#PurchaseNorms").prop('disabled', true);
            $("#PurchaseNormsPercent").prop('disabled', true);
            $("#IssueNorms").prop('disabled', true);
            $("#IssueNormsPercent").prop('disabled', true);
            $("#CostingNorms").prop('disabled', true);
            $("#CostingNormsPercent").prop('disabled', true);
            $('.loader-overlay').fadeOut('fast');
            $('.loader-overlay').hide();
            if (data.AlertMessage == "Already Existed") {
                alert("Already  Existed this BOM.");
                return false;
            }
            else if (data.AlertMessage == "Saved Successfully") {
                if (data.BOM.BomId != 0) {
                    $('#sizeRangeTable tbody tr').each(function(){
                        $(this).find("td:not(:first)").remove();

                    });
                    $("#Displaycheck").prop('checked',false);
                    $("#materialCategory .custom-combobox input").val('');
                    $("#MaterialCategoryMasterId").val('');
                    $("#MaterialGroupMasterId").val('');
                    $("#MaterialName").val('');
                    $("#Addrowfields > tbody").html("");
                    $("#materialGroupID_ .custom-combobox input").val('');
                    $("#SizeScheduleMasterId").val('');
                    $("#SubstanceMasterId").val('');
                    $("#masterialName_ .custom-combobox input").val('');
                    $("#ColorMasterId").val('');
                    $("#SampleNorms").val('');
                    $("#Wastage").val('');
                    $("#WastageQty").val('');
                    $("#WastageQtyUOM").val('');
                    $("#TotalNorms").val('');
                    $("#TotalNormsUOM").val('');
                    $("#ComponentName").val('');
                    // $("#NoOfSets").val('');
                    $("#SupplierMasterId").val('');
                    $("#BuyerNorms").val('');
                    $("#UomMasterId").val('');
                    $("#SizeRangeMasterId").val('');
                    $("#SizeScheduleMasterId").val('');
                    $("#OurNorms").val('');
                    $("#PurchaseNorms").val('');
                    $("#IssueNorms").val('');
                    $("#CostingNorms").val('');
                    $("#OurNormsPercent").val('');
                    $("#PurchaseNormsPercent").val('');
                    $("#IssueNormsPercent").val('');
                    $("#CostingNormsPercent").val('');
                    $("#Addrowfields > tbody").html("");

                    $('#list-materials tbody').append("<tr id=rowid_" + data.Material.BOMMaterialID + "><td style='display:none' class='BomNo'><span>" + data.Material.BOMMaterialID + "</span><input type='hidden' class='BomId' value='" + data.BOM.BomId + "'></td>" +
                                                                                                      //"<td class='Description'><span>" + $('#Description').val() + "</span><input type='hidden' class='AddRowFields' value='" + $("#AddRowFields").val() + "'></td>" +
                                                                                                      "<td style='display:none' class='Description' id='Description'>" + data.BOM.Description + "</td>" +
                                                                                                      "<td style='display:none' class='BuyerMasterId' id='BuyerMasterId'>" + data.BOM.BuyerMasterId + "</td>" +
                                                                                                      "<td style='display:none' class='BuyerModel' id='BuyerModel'>" + data.BOM.BuyerModel + "</td>" +
                                                                                                      "<td style='display:none' class='MeanSize' id='MeanSize'>" + data.BOM.MeanSize + "</td>" +
                                                                                                      "<td style='display:none' class='Date' id='Date'>" + data.BOM.Date + "</td>" +
                                                                                                      "<td style='display:none' class='ParentBomNo' id='ParentBomNo'>" + data.BOM.ParentBomNo + "</td>" +
                                                                                                      "<td style='display:none' class='LinkBomNo' id='LinkBomNo'>" + data.BOM.LinkBomNo + "</td>" +
                                                                                                      "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + data.BOM.CommnBOM1 + "</td>" +
                                                                                                      "<td style='display:none' class='BOMMaterialID' id='BOMMaterialID'>" + data.Material.BOMMaterialID + "</td>" +
                                                                                                      //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href="+data.BOMMaterialID+" id="+data.BOMMaterialID+" class='change_status change_status_'>Delete</a></td>" +
                                                                                                      " <td title='Delete'  style='cursor:pointer;' class='Delete' id='Delete'><input type='button' value='Delete' style='cursor:pointer;' title='Delete' onclick='Deletebtn('" + data.Material.BOMMaterialID+"')' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                      " <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><input type='button' value='Edit' style='cursor:pointer;' title='Edit' onclick='RowClick(" + data.Material.BOMMaterialID + "," + data.BOM.BomId + ")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                      "<td class='MaterialCategoryMasterId' id='MaterialCategoryMasterId'>" + data.Category + "</td>" +
                                                                                                      "<td class='MaterialName' id='MaterialName'>" + data.Material_ + "</td>" +
                                                                                                      "<td class='MaterialGroupMasterId' id='MaterialGroupMasterId'>" + data.Group + "</td>" +
                                                                                                      "<td class='ColorMasterId' id='ColorMasterId'>" + data.Color + "</td>" +
                                                                                                      "<td class='SubstanceMasterId' id='SubstanceMasterId'>" + data.Subtance + "</td>" +
                                                                                                      "<td class='SampleNorms' id='SampleNorms'>" + data.Material.SampleNorms + "</td>" +
                                                                                                      //"<td class='Wastage' id='Wastage'>" + data.Material.Wastage + "</td>" +
                                                                                                      //"<td class='WastageQty' id='WastageQty'>" + data.Material.WastageQty + "</td>" +
                                                                                                      "<td class='WastageQtyUOM' id='WastageQtyUOM'>" + data.WasteUOm + "</td>" +
                                                                                                      "<td class='TotalNorms' id='TotalNorms'>" + data.Material.TotalNorms + "</td>" +
                                                                                                      //"<td class='TotalNormsUOM' id='TotalNormsUOM'>" + data.TotalUOM + "</td>" +

                                                                                                      "<td style='display:none' class='ComponentName' id='ComponentName'>" + data.BOM.ComponentName + "</td>" +
                                                                                                      "<td style='display:none' class='BuyerNorms' id='BuyerNorms'>" + data.BOM.BuyerNorms + "</td>" +
                                                                                                      "<td style='display:none' class='NoOfSets' id='NoOfSets'>" + data.BOM.NoOfSets + "</td>" +
                                                                                                      "<td style='display:none' class='UomMasterId' id='UomMasterId'>" + data.BOM.UomMasterId + "</td>" +
                                                                                                      "<td style='display:none' class='SupplierMasterId' id='SupplierMasterId'>" + data.Material.SupplierMasterID + "</td>" +
                                                                                                      "<td style='display:none' class='SizeRangeMasterId' id='SizeRangeMasterId'>" + data.BOM.SizeRangeMasterId + "</td>" +
                                                                                                       "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.Material.SizeScheduleMasterId + "</td>" +
                                                                                                      "<td style='display:none' class='OurNorms' id='OurNorms'>" + data.BOM.OurNorms + "</td>" +
                                                                                                      "<td style='display:none' class='OurNormsPercent' id='OurNormsPercent'>" + data.BOM.OurNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='PurchaseNorms' id='PurchaseNorms'>" + data.BOM.PurchaseNorms + "</td>" +
                                                                                                      "<td style='display:none' class='PurchaseNormsPercent' id='PurchaseNormsPercent'>" + data.BOM.PurchaseNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='IssueNorms' id='IssueNorms'>" + data.BOM.IssueNorms + "</td>" +
                                                                                                      "<td style='display:none' class='IssueNormsPercent' id='IssueNormsPercent'>" + data.BOM.IssueNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='CostingNorms' id='CostingNorms'>" + data.BOM.CostingNorms + "</td>" +
                                                                                                       "<td style='display:none' class='Conversion' id='Conversion'>" + data.Material.Conversion + "</td>" +
                                                                                                      "<td style='display:none' class='CostingNormsPercent' id='CostingNormsPercent'>" + data.BOM.CostingNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='PreparedBy' id='PreparedBy'>" + data.BOM.PreparedBy + "</td>" +

                     "<td style='display:none' class='VerifiedBy' id='VerifiedBy'>" + data.BOM.VerifiedBy + "</td>" +
                                                                                                      "<td style='display:none' class='CheckedBy' id='CheckedBy'>" + data.BOM.CheckedBy + "</td>"
                                                                                                      + "<td style='display:none' class='AddRowFields'>" + $('#AddRowFields').val() + "</td></tr>");
                    $('#BOMMaterialID').val(data.Material.BOMMaterialID)
                    //$("#Addrowfields > tbody").html("");
                    //$.each(data.Grid, function (i, item) {
                    //    $("#Addrowfields tbody").append("<tr class=''><td id='Sno' class='Sno' value=''>" + item.Sno + "</td>"
                    //       + "<td class='width-141'><input type='text' id='Component' class='Component' value=" + item.Component + "/></td>"
                    //       + "<td class='width-91'><input type='text' id='length' class='length' value=" + item.Length + " onkeypress=' return isNumberKey(event)' /></td>"
                    //       + "<td class='width-91'><input type='text' id='width' class='width' value=" + item.Width + " onkeypress='return isNumberKey(event)' /></td>"
                    //        + "<td class='width-91'><input type='text' id='Nos' class='Nos' value=" + item.Nos + " onkeypress='return isNumberKey(event)' /></td>"
                    //        + "<td class='width-91'><input type='text' id='SampleNorms' class='SampleNorms' value=" + item.SampleNorms + " onkeypress='return isNumberKey(event)' /></td>"
                    //        + "<td class='width-91'><input type='text' id='WastagePercent' class='WastagePercent' value=" + item.WastagePercent + " onkeypress='return isNumberKey(event)' /></td>"
                    //         + "<td class='width-91'><input type='text' id='WastageQty' class='WastageQty' value=" + item.WastageQtyGrid + " onkeypress='return isNumberKey(event)' /></td>"
                    //          + "<td class='width-91'><input type='text' id='TotalNormsQty' class='TotalNormsQty' value=" + item.TotalNormsQty + " onkeypress='return isNumberKey(event)' /></td> </tr>");
                    //});
                }
                alert("Saved Successfully.");
                return false;
            }
            else if (data.AlertMessage == "Updated Successfully") {

                if (data.BOM.BomId != 0) {
                    $('#sizeRangeTable tbody tr').each(function(){
                        $(this).find("td:not(:first)").remove();
                    });
                    $("#Displaycheck").prop('checked',false);
                    $("#materialCategory .custom-combobox input").val('');
                    $("#MaterialCategoryMasterId").val('');
                    $("#MaterialGroupMasterId").val('');
                    $("#MaterialName").val('');
                    $("#Addrowfields > tbody").html("");
                    $("#materialGroupID_ .custom-combobox input").val('');
                    $("#SizeScheduleMasterId").val('');
                    $("#SubstanceMasterId").val('');
                    $("#masterialName_ .custom-combobox input").val('');
                    $("#ColorMasterId").val('');
                    $("#SampleNorms").val('');
                    $("#Wastage").val('');
                    $("#WastageQty").val('');
                    $("#WastageQtyUOM").val('');
                    $("#TotalNorms").val('');
                    $("#TotalNormsUOM").val('');
                    $("#ComponentName").val('');
                    // $("#NoOfSets").val('');
                    $("#SupplierMasterId").val('');
                    $("#BuyerNorms").val('');
                    $("#UomMasterId").val('');
                    $("#SizeRangeMasterId").val('');
                    $("#SizeScheduleMasterId").val('');
                    $("#OurNorms").val('');
                    $("#PurchaseNorms").val('');
                    $("#IssueNorms").val('');
                    $("#CostingNorms").val('');
                    $("#OurNormsPercent").val('');
                    $("#PurchaseNormsPercent").val('');
                    $("#IssueNormsPercent").val('');
                    $("#CostingNormsPercent").val('');
                    $("#Addrowfields > tbody").html("");
                    $('#list-materials tbody').append("<tr id=rowid_" + data.Material.BOMMaterialID + "><td style='display:none' class='BomNo'><span>" + data.Material.BOMMaterialID + "</span><input type='hidden' class='BomId' value='" + data.BOM.BomId + "'></td>" +
                                                                                                      //"<td class='Description'><span>" + $('#Description').val() + "</span><input type='hidden' class='AddRowFields' value='" + $("#AddRowFields").val() + "'></td>" +
                                                                                                      "<td style='display:none' class='Description' id='Description'>" + data.BOM.Description + "</td>" +
                                                                                                      "<td style='display:none' class='BuyerMasterId' id='BuyerMasterId'>" + data.BOM.BuyerMasterId + "</td>" +
                                                                                                      "<td style='display:none' class='BuyerModel' id='BuyerModel'>" + data.BOM.BuyerModel + "</td>" +
                                                                                                      "<td style='display:none' class='MeanSize' id='MeanSize'>" + data.BOM.MeanSize + "</td>" +
                                                                                                      "<td style='display:none' class='Date' id='Date'>" + data.BOM.Date + "</td>" +
                                                                                                      "<td style='display:none' class='ParentBomNo' id='ParentBomNo'>" + data.BOM.ParentBomNo + "</td>" +
                                                                                                      "<td style='display:none' class='LinkBomNo' id='LinkBomNo'>" + data.BOM.LinkBomNo + "</td>" +
                                                                                                      "<td style='display:none' class='CommnBOM1' id='CommnBOM1'>" + data.BOM.CommnBOM1 + "</td>" +
                                                                                                      "<td style='display:none' class='BOMMaterialID' id='BOMMaterialID'>" + data.Material.BOMMaterialID + "</td>" +
                                                                                                        " <td title='Delete'  style='cursor:pointer;' class='Delete' id='Delete'><input type='button' value='Delete' style='cursor:pointer;' title='Delete' onclick='Deletebtn(" + data.Material.BOMMaterialID+")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                      " <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><input type='button' value='Edit' style='cursor:pointer;' title='Edit' onclick='RowClick(" + data.Material.BOMMaterialID + "," + data.BOM.BomId + ")' class='change_status change_status_ Delete input Edit input' /></td>" +
                                                                                                      //" <td title='Delete' style='cursor:pointer;' class='Delete' id='Delete'><a href="+data.BOMMaterialID+" id="+data.BOMMaterialID+" class='change_status change_status_'>Delete</a></td>" +
                                                                                                      //" <td title='Edit'  style='cursor:pointer;' class='Edit' id='Edit'><a href='data.Material.BOMMaterialID'>Edit</a></td>" +
                                                                                                      "<td class='MaterialCategoryMasterId' id='MaterialCategoryMasterId'>" + data.Category + "</td>" +
                                                                                                      "<td class='MaterialName' id='MaterialName'>" + data.Material_ + "</td>" +
                                                                                                      "<td class='MaterialGroupMasterId' id='MaterialGroupMasterId'>" + data.Group + "</td>" +
                                                                                                      "<td class='ColorMasterId' id='ColorMasterId'>" + data.Color + "</td>" +
                                                                                                      "<td class='SubstanceMasterId' id='SubstanceMasterId'>" + data.Subtance + "</td>" +
                                                                                                      "<td class='SampleNorms' id='SampleNorms'>" + data.Material.SampleNorms + "</td>" +
                                                                                                      //"<td class='Wastage' id='Wastage'>" + data.Material.Wastage + "</td>" +
                                                                                                      //"<td class='WastageQty' id='WastageQty'>" + data.Material.WastageQty + "</td>" +
                                                                                                      "<td class='WastageQtyUOM' id='WastageQtyUOM'>" + data.WasteUOm + "</td>" +
                                                                                                      "<td class='TotalNorms' id='TotalNorms'>" + data.Material.TotalNorms + "</td>" +
                                                                                                      //"<td class='TotalNormsUOM' id='TotalNormsUOM'>" + data.TotalUOM + "</td>" +

                                                                                                      "<td style='display:none' class='ComponentName' id='ComponentName'>" + data.BOM.ComponentName + "</td>" +
                                                                                                      "<td style='display:none' class='BuyerNorms' id='BuyerNorms'>" + data.BOM.BuyerNorms + "</td>" +
                                                                                                      "<td style='display:none' class='NoOfSets' id='NoOfSets'>" + data.BOM.NoOfSets + "</td>" +
                                                                                                      "<td style='display:none' class='UomMasterId' id='UomMasterId'>" + data.BOM.UomMasterId + "</td>" +
                                                                                                      "<td style='display:none' class='SupplierMasterId' id='SupplierMasterId'>" + data.Material.SupplierMasterID + "</td>" +
                                                                                                      "<td style='display:none' class='SizeRangeMasterId' id='SizeRangeMasterId'>" + data.BOM.SizeRangeMasterId + "</td>" +
                                                                                                       "<td style='display:none' class='SizeScheduleMasterId' id='SizeScheduleMasterId'>" + data.Material.SizeScheduleMasterId + "</td>" +
                                                                                                      "<td style='display:none' class='OurNorms' id='OurNorms'>" + data.BOM.OurNorms + "</td>" +
                                                                                                      "<td style='display:none' class='OurNormsPercent' id='OurNormsPercent'>" + data.BOM.OurNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='PurchaseNorms' id='PurchaseNorms'>" + data.BOM.PurchaseNorms + "</td>" +
                                                                                                      "<td style='display:none' class='PurchaseNormsPercent' id='PurchaseNormsPercent'>" + data.BOM.PurchaseNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='IssueNorms' id='IssueNorms'>" + data.BOM.IssueNorms + "</td>" +
                                                                                                      "<td style='display:none' class='IssueNormsPercent' id='IssueNormsPercent'>" + data.BOM.IssueNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='CostingNorms' id='CostingNorms'>" + data.BOM.CostingNorms + "</td>" +
                                                                                                        "<td style='display:none' class='Conversion' id='Conversion'>" + data.Material.Conversion + "</td>" +
                                                                                                      "<td style='display:none' class='CostingNormsPercent' id='CostingNormsPercent'>" + data.BOM.CostingNormsPercent + "</td>" +
                                                                                                      "<td style='display:none' class='PreparedBy' id='PreparedBy'>" + data.BOM.PreparedBy + "</td>" +
                                                                                                      "<td style='display:none' class='VerifiedBy' id='VerifiedBy'>" + data.BOM.VerifiedBy + "</td>" +
                                                                                                      "<td style='display:none' class='CheckedBy' id='CheckedBy'>" + data.BOM.CheckedBy + "</td>"
                                                                                                      + "<td style='display:none' class='AddRowFields'>" + $('#AddRowFields').val() + "</td></tr>");
                    $('#BOMMaterialID').val(data.Material.BOMMaterialID)
                    //$("#Addrowfields > tbody").html("");
                    //$.each(data.Grid, function (i, item) {
                    //    $("#Addrowfields tbody").append("<tr class=''><td id='Sno' class='Sno' value=''>" + item.Sno + "</td>"
                    //       + "<td class='width-141'><input type='text' id='Component' class='Component' value=" + item.Component + "/></td>"
                    //       + "<td class='width-91'><input type='text' id='length' class='length' value=" + item.Length + " onkeypress=' return isNumberKey(event)' /></td>"
                    //       + "<td class='width-91'><input type='text' id='width' class='width' value=" + item.Width + " onkeypress='return isNumberKey(event)' /></td>"
                    //        + "<td class='width-91'><input type='text' id='Nos' class='Nos' value=" + item.Nos + " onkeypress='return isNumberKey(event)' /></td>"
                    //        + "<td class='width-91'><input type='text' id='SampleNorms' class='SampleNorms' value=" + item.SampleNorms + " onkeypress='return isNumberKey(event)' /></td>"
                    //        + "<td class='width-91'><input type='text' id='WastagePercent' class='WastagePercent' value=" + item.WastagePercent + " onkeypress='return isNumberKey(event)' /></td>"
                    //         + "<td class='width-91'><input type='text' id='WastageQty' class='WastageQty' value=" + item.WastageQtyGrid + " onkeypress='return isNumberKey(event)' /></td>"
                    //          + "<td class='width-91'><input type='text' id='TotalNormsQty' class='TotalNormsQty' value=" + item.TotalNormsQty + " onkeypress='return isNumberKey(event)' /></td> </tr>");
                    //});
                }
                alert("Updated Successfully");
                return false;
            }
            if (data == "Material Existed") {
                alert("Already  Existed this Material");
                return false;
            }
        }
    });
}

}
});
function BomSave() {
    var rowCount = $('#list-materials tr').length - 1;
    if (rowCount == 0) {

        if (validation()) {


            var forms_ = $(".form-inline").serialize();

            if ($("#list-materials tbody tr").length == 0) {

                console.log(forms_);
                console.log(form);
                $.ajax({
                    type: 'POST',
                    url: '/BillOfMaterial/BillOfMaterialDetails',
                    data: { forms: forms_, Grid: $('.AddRowFields').text(), BomGridlst: '@Model.BomGridlst' },
                    success: function (data) {
                        if (data == true) {
                            if ($("#BtnSave").val() == "Save") {
                                alert('Saved Successfully.');
                            }
                            else {
                                alert('Updated Successfully.');
                            }
                            location.href = "/BillOfMaterial/BOMMaterialListGrid";
                            return false;
                        }
                        else {
                            alert('Save process Failed.');
                            location.href = "/BillOfMaterial/BOMMaterialListGrid";
                            return false;
                        }
                    }
                });

            }
        }
    }
    else {

        var wholeJsonList = [];
        $('#list-materials tbody tr').each(function () {
            $(this).addClass("curChangeableRow"),
           $(this).siblings().removeClass("curChangeableRow"),
          $('.curChangeableRow').hide(),
wholeJsonList.push(
    {

        forms: forms_,
        Grid: $('.AddRowFields').text(),
        BomGridlst: '@Model.BomGridlst',
        BomNo: $(this).find('span').html(),
        Description: $(this).find('.Description').html(),
        BuyerMasterId: $(this).find('.BuyerMasterId').html(),
        BuyerModel: $(this).find('.BuyerModel').html(),
        MeanSize: $(this).find('.MeanSize').html(),
        Date: $(this).find('.Date').html(),
        ParentBomNo: $(this).find('.ParentBomNo').html(),
        LinkBomNo: $(this).find('.LinkBomNo').html(),
        CommnBOM1: $(this).find('.CommnBOM1').html(),
        MaterialCategoryMasterId: $(this).find('.MaterialCategoryMasterId').html(),
        MaterialName: $(this).find('.MaterialName').html(),
        MaterialGroupMasterId: $(this).find('.MaterialGroupMasterId').html(),
        ColorMasterId: $(this).find('.ColorMasterId').html(),
        SubstanceMasterId: $(this).find('.SubstanceMasterId').html(),
        SampleNorms: $(this).find('.SampleNorms').html(),
        Wastage: $(this).find('.Wastage').html(),
        WastageQty: $(this).find('.WastageQty').html(),
        WastageQtyUOM: $(this).find('.WastageQtyUOM').html(),
        TotalNorms: $(this).find('.TotalNorms').html(),
        TotalNormsUOM: $(this).find('.TotalNormsUOM').html(),
        ComponentName: $(this).find('.ComponentName').html(),
        BuyerNorms: $(this).find('.BuyerNorms').html(),
        NoOfSets: $(this).find('.NoOfSets').html(),
        UomMasterId: $(this).find('.UomMasterId').html(),
        SupplierMasterId: $(this).find('.SupplierMasterId').html(),
        SizeRangeMasterId: $(this).find('.SizeRangeMasterId').html(),
        SizeScheduleMasterId: $(this).find('.SizeScheduleMasterId').html(),
        Conversion: $(this).find('.Conversion').html(),
        OurNorms: $(this).find('.OurNorms').html(),
        OurNormsPercent: $(this).find('.OurNormsPercent').html(),
        PurchaseNorms: $(this).find('.PurchaseNorms').html(),
        PurchaseNormsPercent: $(this).find('.PurchaseNormsPercent').html(),
        IssueNorms: $(this).find('.IssueNorms').html(),
        IssueNormsPercent: $(this).find('.IssueNormsPercent').html(),
        CostingNorms: $(this).find('.CostingNorms').html(),
        CostingNormsPercent: $(this).find('.CostingNormsPercent').html(),
        PreparedBy: $(this).find('.PreparedBy').html(),
        VerifiedBy: $(this).find('.VerifiedBy').html(),
        CheckedBy: $(this).find('.CheckedBy').html(),
    });
        });

        var wholeJsonListLength = wholeJsonList.length;
        var i = 0;
        for (i = 0; i <= wholeJsonListLength - 1; i++) {
            $.ajax({
                type: 'POST',
                url: '/BillOfMaterial/BillOfMaterialDetails',
                data: wholeJsonList[i],
                success: function (data) {
                    $('#BomId').val(data.BomId);
                    console.log(data.BomId);
                    return false;

                }
            });
        }
    }
}


   

});



    function RowClick(arg, arg1) {
        alert(arg);
        alert(arg1);
        
        var Removerow = "rowid_" + arg;
        //  alert(Removerow);
        $('#' + Removerow + '').remove();
        $('#add-Material').show();
        $('.material_BOM').show();
        $('#Removerow')
        $('#Addrowfields tbody tr').remove();
        $(this).addClass("curChangeableRow"),
        $(this).siblings().removeClass("curChangeableRow"),
        $('.curChangeableRow').hide(),
        $.ajax({
            type: 'POST',
            url: '/BillOfMaterial/GetBOMMaterilsList',
            data: { BOMMaterialID: arg, BOMID: arg1, BuyerMasterId: $("#BuyerMasterId").val() },
            success: function (data) {
                if (data.BOM.BOMID != 0) {
                    $('#BOMMaterialID').val(data.Material.BOMMaterialID);
                    $('#BOMMaterialID').val(arg);
                    $('#BomNo').val(data.BOM.BomNo);
                    $('#Description').val(data.BOM.Description);
                    $('#BuyerMasterId').val(data.BOM.BuyerMasterId);
                    $('#BuyerModel').val(data.BOM.BuyerModel);
                    $('#MeanSize').val(data.BOM.MeanSize);
                    $('#Conversion').val(data.Material.Conversion);
                    //  $('#Date').val(data.BOM.Date.toString());
                    $('#ParentBomNo').val(data.BOM.ParentBomNo);
                    $('#LastBomNoEntered').val(data.BOM.LastBomNoEntered);
                    $('#LinkBomNo').val(data.BOM.LinkBomNo);
                    $('#Ammendment').val(data.BOM.Ammendment);
                    $('#CommonBomNo').val(data.BOM.CommonBomNo);
                    $('#CommnBOM1').val(data.BOM.CommnBOM1);
                    $('#CommnBOM2').val(data.BOM.CommnBOM2);
                    $('#CommnBOM3').val(data.BOM.CommnBOM3);
                    $('#CommnBOM4').val(data.BOM.CommnBOM4);
                    $('#CommnBOM5').val(data.BOM.CommnBOM5);
                    $('#PreparedBy').val(data.BOM.PreparedBy);
                    $('#VerifiedBy').val(data.BOM.VerifiedBy);
                    $('#CheckedBy').val(data.BOM.CheckedBy);
                    $('#ComponentName').val(data.BOM.ComponentName);
                    $('#BuyerNorms').val(data.BOM.BuyerNorms);
                    $('#NoOfSets').val(data.BOM.NoOfSets);
                    $('#UomMasterId').val(data.BOM.UomMasterId);
                    $('#SupplierMasterId').val(data.Material.SupplierMasterID);
                    $('#SizeRangeMasterId').val(data.BOM.SizeRangeMasterId);
                    $('#SizeScheduleMasterId').val(data.Material.SizeScheduleMasterId);
                    //from bom to populate below columns
                    if (data.BOM.IssueNorms != null) {
                        $('#OurNorms').val(data.BOM.OurNorms);
                        $('#OurNormsPercent').val(data.BOM.OurNormsPercent);
                        $('#PurchaseNorms').val(data.BOM.PurchaseNorms);
                        $('#PurchaseNormsPercent').val(data.BOM.PurchaseNormsPercent);
                        $('#IssueNorms').val(data.BOM.IssueNorms);
                        $('#IssueNormsPercent').val(data.BOM.IssueNormsPercent);
                        $('#CostingNorms').val(data.BOM.CostingNorms);
                        $('#CostingNormsPercent').val(data.BOM.CostingNormsPercent);
                    }
                        //later changes sow from bommaterial to populate below columns
                    else {
                        $('#OurNorms').val(data.Material.OurNorms);
                        $('#OurNormsPercent').val(data.Material.OurNormsPercent);
                        $('#PurchaseNorms').val(data.Material.PurchaseNorms);
                        $('#PurchaseNormsPercent').val(data.Material.PurchaseNormsPercent);
                        $('#IssueNorms').val(data.Material.IssueNorms);
                        $('#IssueNormsPercent').val(data.Material.IssueNormsPercent);
                        $('#CostingNorms').val(data.Material.CostingNorms);
                        $('#CostingNormsPercent').val(data.Material.CostingNormsPercent);
                    }

                    //$("#materialCategory .custom-combobox input").val('');
                    //$("#materialCategory .custom-combobox input").hide();
                    ////  $("#Addrowfields > tbody").html("");
                    //$("#materialGroupID_ .custom-combobox input").val('');
                    //$("#materialGroupID_ .custom-combobox input").hide();
                    //// $("#SizeScheduleMasterId").val('');
                    //// $("#SubstanceMasterId").val('');
                    //$("#masterialName_ .custom-combobox input").val('');
                    //$("#masterialName_ .custom-combobox input").hide();
                    $('#MaterialCategoryMasterId').val(data.Material.MaterialCategoryMasterId);
                    $("#MaterialCategoryMasterId").combobox('destroy');
                    $("#MaterialCategoryMasterId").combobox();
                    $('#MaterialGroupMasterId').val(data.Material.MaterialGroupMasterId);
                    $("#MaterialGroupMasterId").combobox('destroy');
                    $("#MaterialGroupMasterId").combobox();
                    $('#SubstanceMasterId').val(data.Material.SubstanceMasterId);
                    $('#MaterialName').val(data.Material.MaterialName);
                    $("#MaterialName").combobox('destroy');
                    $("#MaterialName").combobox();
                    $('#ColorMasterId').val(data.Material.ColorMasterId);
                    $('#SampleNorms').val(data.Material.SampleNorms);
                    $('#Wastage').val(data.Material.Wastage);
                    $('#WastageQty').val(data.Material.WastageQty);
                    $('#WastageQtyUOM').val(data.Material.WastageQtyUOM);
                    $('#TotalNorms').val(data.Material.TotalNorms);
                    $('#BuyerNorms').val(data.Material.BuyerNorms);
                    $('#TotalNormsUOM').val(data.Material.TotalNormsUOM);
                    if (data.MaterialGroup.IsSubstance == true) {
                        $("#SubstanceMasterId").prop('disabled', false);
                    }
                    else {
                        $("#SubstanceMasterId").prop('disabled', true);
                    }
                    if (data.MaterialGroup.IsSize == false) {
                        $("#SizeRangeMasterId").prop('disabled', true);
                        $("#SizeScheduleMasterId").prop('disabled', true);
                        $("#SizeRangeMasterId").val("");
                        $("#SizeScheduleMasterId").val("");
                    }
                    else {
                        $("#SizeRangeMasterId").prop('disabled', false);
                        $("#SizeScheduleMasterId").prop('disabled', false);
                    }
                    if (data.Norms != null && data.Norms.BuyerNameid == parseInt($('#BuyerMasterId').val())) {

                        $("#OurNormsPercent").prop("disabled", false);
                        $("#CostingNormsPercent").prop("disabled", false);
                        $("#PurchaseNormsPercent").prop("disabled", false);
                        $("#IssueNormsPercent").prop("disabled", false);
                        if (data.materialCategoryMaster.CategoryName == "Leathers") {
                            $("#Conversion").prop("disabled", false);
                            $("#BuyerNorms").prop("disabled", false);
                            $("#TotalNorms").prop("disabled", false);
                            $("#SampleNorms").prop("disabled", true);
                            var ourNormsPercentage = $("#OurNormsPercent").val();
                            var BuyerNorms = $('#BuyerNorms').val();
                            var buyerNormsPercentage = ((parseFloat(BuyerNorms) / 100) * parseFloat(ourNormsPercentage));

                            if ($('#OurNorms option:selected').text().trim() == "Increase") {

                                Total_buyerNormsPercentage = parseFloat(BuyerNorms) + parseFloat(buyerNormsPercentage);
                                $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                            }
                            else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
                                Total_buyerNormsPercentage = parseFloat(BuyerNorms) - parseFloat(buyerNormsPercentage);
                                $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                            }

                        }
                        else if (data.materialCategoryMaster.CategoryName != "Leathers") {

                            $("#SampleNorms").prop("disabled", false);
                            var ourNormsPercentage = $("#OurNormsPercent").val();
                            var ConvertedNorms = $('#SampleNorms').val();
                            if (ConvertedNorms != "") {
                                var buyerNormsPercentage = ((parseFloat(ConvertedNorms) / 100) * parseFloat(ourNormsPercentage));
                                $("#BuyerNorms").val(buyerNormsPercentage.toFixed(4));
                                if ($('#OurNorms option:selected').text().trim() == "Increase") {
                                    Total_buyerNormsPercentage = parseFloat(ConvertedNorms) + parseFloat(buyerNormsPercentage);
                                    $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                                }
                                else if ($('#OurNorms option:selected').text().trim() == "Decrease") {
                                    Total_buyerNormsPercentage = parseFloat(ConvertedNorms) - parseFloat(buyerNormsPercentage);
                                    $('#TotalNorms').val(Total_buyerNormsPercentage.toFixed(4));
                                }
                            }
                            $("#Conversion").prop("disabled", true);
                            $("#BuyerNorms").prop("disabled", false);
                            $("#TotalNorms").prop("disabled", false);
                            $("#SampleNorms").prop("disabled", true);
                        }
                        else {

                            $("#BuyerNorms").val("");
                            $("#Conversion").prop("disabled", true);
                            $("#SampleNorms").prop("disabled", true);
                            $("#BuyerNorms").prop("disabled", true);
                            $("#TotalNorms").prop("disabled", false);
                        }
                    }
                    else if (data.Norms != null && data.Norms.BuyerNameid != parseInt($('#BuyerMasterId').val())) {

                        $("#BuyerNorms").val(data.Material.BuyerNorms);
                        $("#SampleNorms").val("");
                        $("#Conversion").prop("disabled", true);
                        $("#SampleNorms").prop("disabled", true);
                        $("#TotalNorms").prop("disabled", false);
                        $("#BuyerNorms").prop("disabled", true);
                    }
                    else if (data.Norms == null) {
                        $("#SampleNorms").val("");
                        $("#Conversion").prop("disabled", true);
                        $("#SampleNorms").prop("disabled", true);
                        $("#TotalNorms").prop("disabled", false);
                        $("#BuyerNorms").prop("disabled", false);
                    }
                    var count = 1;
                    if (data.MaterialGroup.IsSize == true) {
                        $('#Displaycheck').prop('checked', true);
                        for (var i = 0; i <= data.DisplaySize.length - 1; i++) {

                            $('#sizeRangeTable tbody tr:first-child').append('<td  style="background-color: #ddd;padding-bottom: 3px;width: auto !important;padding:0 0px;" class="SizeRangeRefValue"> ' + data.DisplaySize[i].SizeRange + ' </td> ');
                            if (data.DisplaySize[i].SizeIsChecked == true) {
                                $('#sizeRangeTable tbody tr:last').append("<td><input type='checkbox' checked='checked' name='size_chk' id='size_chk" + i + "' class='checked_checkbox'  style=''></input> <label for='size_chk" + i + "' style='' class='lbl-ch'></label></td>");
                            }
                            else {
                                $('#sizeRangeTable tbody tr:last').append("<td><input type='checkbox' name='size_chk' id='size_chk" + i + "' class='checked_checkbox'  style=''></input> <label for='size_chk" + i + "' style='' class='lbl-ch'></label></td>");
                            }
                            count++;
                        }
                    }
                    else {
                        $('#Displaycheck').prop('checked', false);
                        $('#sizeRangeTable tbody tr').each(function () {
                            $(this).find("td:not(:first)").remove();
                        });
                    }
                    $.each(data.Grid, function (i, item) {
                        $("#Addrowfields tbody").append("<tr class='Append'><td id='Sno' class='Sno' value=''>" + item.Sno + "</td>"
                        + "<td class='width-141'><input type='text' id='Component' class='Component' value=" + item.Component + "></td>"
                        + "<td class='width-91'><input type='text' id='length' class='length' value=" + item.Length + " onkeypress=' return isNumberKey(event)' /></td>"
                        + "<td class='width-91'><input type='text' id='width' class='width' value=" + item.Width + " onkeypress='return isNumberKey(event)' /></td>"
                        + "<td class='width-91'><input type='text' id='Nos' class='Nos' value=" + item.Nos + " onkeypress='return isNumberKey(event)' /></td>"
                        + "<td class='width-91'><input type='text' id='SampleNorms' class='SampleNorms' value=" + item.SampleNorms + " onkeypress='return isNumberKey(event)' /></td>"
                        + "<td class='width-91'><input type='text' id='WastagePercent' class='WastagePercent' value=" + item.WastagePercent + " onkeypress='return isNumberKey(event)' /></td>"
                        + "<td class='width-91'><input type='text' id='WastageQty' class='WastageQty' value=" + item.WastageQtyGrid + " onkeypress='return isNumberKey(event)' /></td>"
                        + "<td class='width-91'><input type='text' id='TotalNormsQty' class='TotalNormsQty' value=" + item.TotalNormsQty + " onkeypress='return isNumberKey(event)' /></td>"
                        + "<td><input type='button' value='Remove' class='removeSchedule btn btn-default btn-style' /></td></tr>");
                    });

                }
            }

        });
        //$("#MaterialName").combobox();
        //$("#toggle").click(function () {
        //    $("#MaterialName").toggle();
        //});
        //$("#MaterialGroupMasterId").combobox();
        //$("#toggle").click(function () {
        //    $("#MaterialGroupMasterId").toggle();
        //});
        //$("#MaterialCategoryMasterId").combobox();
        //$("#toggle").click(function () {
        //    $("#MaterialCategoryMasterId").toggle();
        //});
    }


    function Deletebtn(arg) {
        alert("delete");
        var id = $('.change_status').attr("id");

        if (confirm("Are you sure you want to delete this material?")) {
            $.ajax({
                url: '/BillOfMaterial/BOMMaterialDelete',
                type: "GET",
                dataType: "JSON",
                data: { BOMMaterialID: arg },
                success: function (data) {
                    if (data.status.trim() == "Success") {
                        location.href = 'BillOfMaterialDetails?BOMID=' + data.BOMID;
                        alert("Deleted Successfully.");
                        return false;
                    }
                    else {
                        alert("Deleted failed.")
                        return false;
                    }
                }
            });
        }
        else {
            return false;
        }

    }

    function Save() {

      
        var systemUserValue = '';
        var Sno = '';
        var compnt = '';
        var _length = '';
        var width = '';
        var nos = '';
        var samplenms = '';
        var wstg = '';
        var wstqty = '';
        var wsttotalNorms = '';
        $('#Addrowfields tbody tr').each(function () {
            Sno = '';
            compnt = '';
            _length = '';
            width = '';
            nos = '';
            samplenms = '';
            wstg = '';
            wstqty = '';
            wsttotalNorms = '';
            $(this).find("td").each(function () {
                Sno = $('#Sno').val();
                if (Sno != undefined && Sno.trim().length > 0) {
                    systemUserValue += Sno + ',';
                }
                compnt = $(this).find('#Component').val();
                if (compnt != undefined && compnt.trim().length > 0) {
                    systemUserValue += compnt + ',';
                }
                _length = $(this).find('#length').val();
                if (_length != undefined && _length.trim().length > 0) {
                    systemUserValue += _length + ',';
                }
                width = $(this).find('#width').val();
                if (width != undefined && width.trim().length > 0) {
                    systemUserValue += width + ',';

                }
                nos = $(this).find('#Nos').val();
                if (nos != undefined && nos.trim().length > 0) {
                    systemUserValue += nos + ',';
                }

                samplenms = $(this).find('#SampleNorms').val();
                if (samplenms != undefined && samplenms.trim().length > 0) {
                    systemUserValue += samplenms + ',';
                }
                wstg = $(this).find('#WastagePercent').val();
                if (wstg != undefined && wstg.trim().length > 0) {
                    systemUserValue += wstg + ',';
                }
                wstqty = $(this).find('#WastageQty').val();
                if (wstqty != undefined && wstqty.trim().length > 0) {
                    systemUserValue += wstqty + ',';
                }
                wsttotalNorms = $(this).find('#TotalNormsQty').val();
                if (wsttotalNorms != undefined && wsttotalNorms.trim().length > 0) {
                    systemUserValue += wsttotalNorms;
                }

            });
            systemUserValue += '#';

        });
        var form = systemUserValue;
        console.log($('.AddRowFields').val());
        $('#AddRowFields').val(form);
        var CommnBOM = "";
        if ($('#CommnBOM1').val() != "") {
            CommnBOM = $('#CommnBOM1').val();
        }
        if ($('#CommnBOM2').val() != "") {
            CommnBOM += "," + $('#CommnBOM2').val();
        }
        if ($('#CommnBOM3').val() != "") {
            CommnBOM += "," + $('#CommnBOM3').val();
        }
        if ($('#CommnBOM4').val() != "") {
            CommnBOM += "," + $('#CommnBOM4').val();
        }
        if ($('#CommnBOM5').val() != "") {
            CommnBOM += "," + $('#CommnBOM5').val();
        }
        var rowCount = $('#list-materials tr').length - 1;

        
        if (rowCount == 0) {

            if (validation()) {


                var forms_ = $(".form-inline").serialize();
                var len = $("#list-materials tbody tr").length;
                if ($("#list-materials tbody tr").length == 0) {
                    alert(len);
                    $.ajax({
                        type: 'POST',
                        url: '/BillOfMaterial/BillOfMaterialDetails',
                        data: { forms: forms_, Grid: $('.AddRowFields').text(), BomGridlst: '@Model.BomGridlst' },
                        success: function (data) {
                            if (data == true) {
                                if ($("#BtnSave").val() == "Save") {
                                    alert('Saved Successfully.');
                                }
                                else {
                                    alert('Updated Successfully.');
                                }

                                location.href = "/BillOfMaterial/BOMMaterialListGrid";
                                return false;
                            }
                            else {
                                alert('Save process Failed.');
                                location.href = "/BillOfMaterial/BOMMaterialListGrid";
                                return false;
                            }
                        }
                    });

                }
            }
        }
        else {

            var wholeJsonList = [];
            console.log(forms_);
            console.log($('#AddRowFields').val());
            console.log($('#AddRowFields').val(form))
            $('#list-materials tbody tr').each(function () {
                $(this).addClass("curChangeableRow"),
               $(this).siblings().removeClass("curChangeableRow"),
              $('.curChangeableRow').hide(),

    wholeJsonList.push(
        {

            forms: forms_,
            Grid: $('.AddRowFields').text(),
            BomGridlst: '@Model.BomGridlst',
            BomNo: $(this).find('span').html(),
            Description: $(this).find('.Description').html(),
            BuyerMasterId: $(this).find('.BuyerMasterId').html(),
            BuyerModel: $(this).find('.BuyerModel').html(),
            MeanSize: $(this).find('.MeanSize').html(),
            Date: $(this).find('.Date').html(),
            ParentBomNo: $(this).find('.ParentBomNo').html(),
            LinkBomNo: $(this).find('.LinkBomNo').html(),
            CommnBOM1: $(this).find('.CommnBOM1').html(),
            MaterialCategoryMasterId: $(this).find('.MaterialCategoryMasterId').html(),
            MaterialName: $(this).find('.MaterialName').html(),
            MaterialGroupMasterId: $(this).find('.MaterialGroupMasterId').html(),
            ColorMasterId: $(this).find('.ColorMasterId').html(),
            SubstanceMasterId: $(this).find('.SubstanceMasterId').html(),
            SampleNorms: $(this).find('.SampleNorms').html(),
            Wastage: $(this).find('.Wastage').html(),
            WastageQty: $(this).find('.WastageQty').html(),
            WastageQtyUOM: $(this).find('.WastageQtyUOM').html(),
            TotalNorms: $(this).find('.TotalNorms').html(),
            TotalNormsUOM: $(this).find('.TotalNormsUOM').html(),
            ComponentName: $(this).find('.ComponentName').html(),
            BuyerNorms: $(this).find('.BuyerNorms').html(),
            NoOfSets: $(this).find('.NoOfSets').html(),
            UomMasterId: $(this).find('.UomMasterId').html(),
            SupplierMasterId: $(this).find('.SupplierMasterId').html(),
            SizeRangeMasterId: $(this).find('.SizeRangeMasterId').html(),
            SizeScheduleMasterId: $(this).find('.SizeScheduleMasterId').html(),
            OurNorms: $(this).find('.OurNorms').html(),
            OurNormsPercent: $(this).find('.OurNormsPercent').html(),
            PurchaseNorms: $(this).find('.PurchaseNorms').html(),
            PurchaseNormsPercent: $(this).find('.PurchaseNormsPercent').html(),
            IssueNorms: $(this).find('.IssueNorms').html(),
            IssueNormsPercent: $(this).find('.IssueNormsPercent').html(),
            CostingNorms: $(this).find('.CostingNorms').html(),
            CostingNormsPercent: $(this).find('.CostingNormsPercent').html(),
            PreparedBy: $(this).find('.PreparedBy').html(),
            VerifiedBy: $(this).find('.VerifiedBy').html(),
            CheckedBy: $(this).find('.CheckedBy').html(),

        });
            });
            var value = $(this).find('.TotalNorms').html();
            alert("totalnorm" + value);
            var wholeJsonListLength = wholeJsonList.length;

            var i = 0;
            for (i = 0; i <= wholeJsonListLength - 1; i++) {
                $.ajax({
                    type: 'POST',
                    url: '/BillOfMaterial/BillOfMaterialDetails',

                    data: wholeJsonList[i],
                    success: function (data) {
                        if (data == true) {
                            if ($("#BtnSave").val() == "Save") {
                                alert('Saved Successfully.');
                            }
                            else {
                                alert('Updated Successfully.');
                            }

                            location.href = "/BillOfMaterial/BOMMaterialListGrid";
                            return false;
                        }
                        else {
                            alert('Save process Failed.');
                            location.href = "/BillOfMaterial/BOMMaterialListGrid";
                            return false;
                        }
                    }
                });
            }
        }

    }