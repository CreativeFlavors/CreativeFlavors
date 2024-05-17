/* JS files  */
$(document).ready(function () {

    if ($("#Addrowfields tbody").length == 0) {
        $("#Addrowfields").append("<tbody></tbody>");
    }
	$("#component-check").click(function(){	
	if($('#component-check').is(':checked'))
	{
	$(".visbile-hide-show-sec").css({"display":"block"});
	}
	else
	{
	$(".visbile-hide-show-sec").css({"display":"none"})
	}
});

	$('#add-field').click(function(){

	    var id = 0;
	    id = $("#Addrowfields").closest('table').find('tr:last td:first').find('#Sno').val();
	    if (id == "undefined" || id == undefined) {
	        id = 0;
	    }
	    alert();
	    IncrementVal = parseInt(id) + 1;
	   
	    
	    var data = $("#Addrowfields tbody").append(
              "<tr id='appendvalue' class='test' >" +
              "<td><input type='text'id='Sno' value='" + IncrementVal + "' class='numeric' disabled='disabled' onclick='Numeric()' /></td>" +
              "<td><input type='text' id='Component'  /></td>" +
              "<td><input type='text' id='length' onclick='Numeric()'/></td>" +
              "<td><input type='text' id='width' onclick='Numeric()'/></td>" +
              "<td><input type='text' id='Nos' onclick='Numeric()'/></td>" +
              "<td><input type='text' id='SampleNorms' onclick='Numeric()'/></td>" +
              "<td><input type='text' id='WastagePercent' onclick='Numeric()'/></td>" +
              "<td><input type='text' id='WastageQty' onclick='Numeric()'/></td>" +
              "<td><input type='button' value='Delete' class='btn btn-default btn-style width-78' id='btnDelete' onclick='GridDelete()'/></td>" +
              "</tr>");
	     

	if($('#component-check').is(':checked'))
	{
//$('#list-components tbody tr:last').after('<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>');
}
else
{
//$('#list-materials tbody tr:last').after('<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>');
}
});
/** table header fixed **/
//$("#list-components").tableHeadFixer();
//$("#list-amended-material").tableHeadFixer();
//$("#list-materials").tableHeadFixer();	

/** table header fixed end**/




});