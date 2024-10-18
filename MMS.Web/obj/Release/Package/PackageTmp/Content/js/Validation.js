// Toast function
function toast({ title = "", message = "", type = "info", duration = 3000 }) {
    const main = document.getElementById("toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRemoveId);
            }
        };

        const icons = {
            success: "fas fa-check-circle",
            info: "fas fa-info-circle",
            warning: "fas fa-exclamation-circle",
            error: "fas fa-exclamation-circle"
        };
        const icon = icons[type];
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add("toast", `toast--${type}`);
        toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s ${delay}s forwards`;

        toast.innerHTML = `
                    <div class="toast__icon">
                        <i class="${icon}"></i>
                    </div>
                    <div class="toast__body">
                        <h3 class="toast__title">${title}</h3>
                        <p class="toast__msg">${message}</p>
                    </div>
                    <div class="toast__close">
                        <i class="fas fa-times"></i>
                    </div>
                `;
        main.appendChild(toast);
    }
}

function showSuccessToast() {
    toast({
        title: "Success!",
        message: "Added Successfully.",
        type: "success",
        duration: 5000
    });
}
function showProductionProcessesToast(value) {
    toast({
        title: "Success!",
        message: "Production " + value + " Successfully.",
        type: "success",
        duration: 5000
    });
}
function showProductionProcesses1Toast(value) {
    toast({
        title: "Success!",
        message: "Product Moved to " + value,
        type: "success",
        duration: 5000
    });
}
function showDCToast() {
    toast({
        title: "Success!",
        message: "DC Applied successfully.",
        type: "success",
        duration: 5000
    });
}
function InprogressToast() {
    toast({
        title: "Success!",
        message: "Product Moved to Inprogress and Material Stock decreased",
        type: "success",
        duration: 5000
    });
}
function showprodcomplToast() {
    toast({
        title: "error!",
        message: "Production has been completed for this product.",
        type: "error",
        duration: 5000
    });
}
function showInvoiceToast() {
    toast({
        title: "Success!",
        message: "Invoice Raised successfully.",
        type: "success",
        duration: 5000
    });
}
function showdeletedToast() {
    toast({
        title: "Deleted!",
        message: "Deleted Successfully.",
        type: "error",
        duration: 5000
    });
}
function showdNOSTOCKToast() {
    toast({
        title: "error!",
        message: "There is no stock.",
        type: "error",
        duration: 5000
    });
}
function showupdateToast() {
    toast({
        title: "Updated!",
        message: "Updated Successfully.",
        type: "success",
        duration: 5000,
    });
}

function showErrorToast() {
    toast({
        title: "Error!",
        message: "Entered Code already exists, Please enter another code",
        type: "error",
        duration: 5000
    });
}
function showpssworderrorToast() {
    toast({
        title: "Error!",
        message: "Incorrect password. Please try again",
        type: "error",
        duration: 5000
    });
}
function showpssswordemailerrorToast() {
    toast({
        title: "Error!",
        message: "The email and password you entered are incorrect. Please check your credentials and try again",
        type: "error",
        duration: 5000
    });
}
function showaddToast() {
    toast({
        title: "Error!",
        message: "Already Exist Single Address",
        type: "error",
        duration: 5000
    });
}
function showsaveprocessToast() {
    toast({
        title: "Error!",
        message: "Operation failed. Please try again",
        type: "error",
        duration: 5000
    });
}
function showdeleteprocessToast() {
    toast({
        title: "Error!",
        message: "Delete process Failed",
        type: "error",
        duration: 5000
    });
}
function showsalreadyexistToast() {
    toast({
        title: "Error!",
        message: "Already Exist in the database",
        type: "error",
        duration: 5000
    });
}
function showsdeactivatedToast() {
    toast({
        title: "Error!",
        message: "Deactivated Successfully",
        type: "error",
        duration: 5000
    });
}
function showactivatedToast() {
    toast({
        title: "Success!",
        message: "Activated Successfully",
        type: "success",
        duration: 5000
    });
}
function showsbomnoToast() {
    toast({
        title: "Error!",
        message: "Entered BOM No already exists, Please enter another No",
        type: "error",
        duration: 5000
    });
}
function showsproductthereToast() {
    toast({
        title: "Error!",
        message: "Entered Product already exists, Please enter another Product",
        type: "error",
        duration: 5000
    });
}
function notaddedToast() {
    toast({
        title: "Error!",
        message: "Production details not added",
        type: "error",
        duration: 5000
    });
}
function showsSOCfToast() {
    toast({
        title: "Error!",
        message: "You must add the product before confirming",
        type: "error",
        duration: 5000
    });
}
function showsSOaddproToast() {
    toast({
        title: "Error!",
        message: "Please select an item before proceeding with the purchase",
        type: "error",
        duration: 5000
    });
}
function showsgrnsToast() {
    toast({
        title: "Error!",
        message: "Please select an item before proceeding with add GRN",
        type: "error",
        duration: 5000
    });
}
function showsbuyernotToast() {
    toast({
        title: "Error!",
        message: "Buyer not selected",
        type: "error",
        duration: 5000
    });
}
function showsproductToast() {
    toast({
        title: "Error!",
        message: "This product has already been added",
        type: "error",
        duration: 5000
    });
}
function showsdiscountlessToast() {
    toast({
        title: "Error!",
        message: "Discount must be less than 100",
        type: "error",
        duration: 5000
    });
}
function showsbuyeraddToast() {
    toast({
        title: "Error!",
        message: "You need to provide a buyer address",
        type: "error",
        duration: 5000
    });
}
function showsfullpaymentToast() {
    toast({
        title: "Error!",
        message: "Full payment done already",
        type: "error",
        duration: 5000
    });
}
function showscrtpaymentToast() {
    toast({
        title: "Error!",
        message: "Give a correct payment",
        type: "error",
        duration: 5000
    });
}
function showsvalidnumToast() {
    toast({
        title: "Error!",
        message: "Please enter a valid number",
        type: "error",
        duration: 5000
    });
}
//salesorder Validation
function quantityvalidate() {
    var number = $('#quantity').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error1').text("Please Enter the Qty");

    }
    else if (filter.test(number)) {
        $('#error1').text("");
        $('#quantity').css('border', '#fff');
    }
    else {
        $('#quantity').css('border', '3px solid red');
        $('#error1').text("Please enter valid number");
    }
}
function disvalidate() {
    var number = $('#discountperid').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (filter.test(number) || number === "") {
        $('#error2').text("");
        $('#discountperid').css('border', '#fff');
    }
    else {
        $('#discountperid').css("border", "3px solid red");
        $('#error2').text("Please enter valid number");
    }
}
function BuyerNamefun() {
    var City = $('#BuyerName').val();
    if (City === "" || City === null) {
        $('#berror').text("Please select Buyer");
    } else {
        $('#berror').text("");
    }
}
function ProductNamefun() {
    var City = $('#ProductName').val();
    if (City === "" || City === null) {
        $('#perror').text("Please select Product");
    } else {
        $('#perror').text("");
    }
}
function order() {
    quantityvalidate();
    BuyerNamefun();
    ProductNamefun();
    disvalidate();
    CurrencyConversionfun();
}
function conforder() {
    BuyerNamefun();
}
//Indent Validation
function matfun() {
    var City = $('#MaterialId').val();
    if (City === "" || City === null) {
        $('#materror').text("Please select Raw Material");
    } else {
        $('#materror').text("");

    }
}
function quantityINvalidate() {
    var number = $('#RequiredQty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#inerror').text("Please Enter the Qty");
        $('#RequiredQty').css('border', '2px solid red');
    }
    else if (filter.test(number)) {
        $('#inerror').text("");
        $('#RequiredQty').css('border', '#fff');
    }
    else {
        $('#RequiredQty').css('border', '2px solid red');
        $('#inerror').text("Please enter valid number");
    }
}
function addindent() {
    quantityINvalidate();
    matfun();
}
//BOM Validation 
function quantitybom() {
    var number = $('#Requiredqty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#bomerror').text("Please Enter the Qty");
    }
    else if (filter.test(number)) {
        $('#bomerror').text("");
    }
    else {
        $('#bomerror').text("Please enter valid number");
    }
}
function subquantitybom() {
    var number = $('#subRequiredqty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#subbomerror').text("Please Enter the Qty");
    }
    else if (filter.test(number)) {
        $('#subbomerror').text("");
    }
    else {
        $('#subbomerror').text("Please enter valid number");
    }
}
function materialfun() {
    var City = $('#Productid').val();
    if (City === "" || City === null) {
        $('#materror').text("Please select Raw Material");
    } else {
        $('#materror').text("");
    }
}
function submaterialfun() {
    var City = $('#ProductSUBid').val();
    if (City === "" || City === null) {
        $('#suberror').text("Please select Sub Assembly Product");
    } else {
        $('#suberror').text("");

    }
}
function addbom() {
    quantitybom();
    materialfun();
}
function addsubbom() {
    subquantitybom();
    submaterialfun();
}
//GRN Validxation
function GRNUnitPrice() {
    var number = $('#UnitPrice').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        alert("Please Enter the UnitPrice");
    }
    else if (filter.test(number)) {
    }
    else {
        alert("Please enter valid number");

    }
}
function supplierfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#sperror').text("select Raw Material");
    } else {
        $('#sperror').text("");
    }
}
function ponofun() {
    var City = $('#PoHeaderId').val();
    if (City === "" || City === null) {
        $('#poheerror').text("select PO No");
    } else {
        $('#poheerror').text("");
    }
}
function Inreffun() {
    var City = $('#RefInvoiceNumber').val();
    if (City === "" || City === null) {
        $('#reinerror').text("Enter Ref-Invoice no ");
    } else {
        $('#reinerror').text("");
    }
}
function indatefun() {
    var City = $('#RefInvoiceDate').val();
    if (City === "" || City === null) {
        $('#indateerror').text("Select Invoiced Date");
    } else {
        $('#indateerror').text("");
    }
}


function confirsmGRN() {
    indatefun();
    Inreffun();
    ponofun();
    supplierfun();
    CurrencyConversionfun();
}

function poinfun() {
    var City = $('#IndentNumber').val();
    if (City === "" || City === null) {
        $('#inerror').text("Please select Indent No ");
    } else {
        $('#inerror').text("");
    }
}
function suppofun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#sperror').text("Please select Supplier Name");
    } else {
        $('#sperror').text("");
    }
}
function profun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#proerror').text("Please select Product Name ");
    } else {
        $('#proerror').text("");
    }
}

function pocon() {
    poinfun();
    suppofun();
    profun();
}
//product
function ProductTypes() {
    var Prod = $('#ProductType').val();
    if (Prod === "") {
        $('#error1').text("Please select Product Type ");
    } else {
        $('#error1').text("");
    }
}
function Category() {
    var City1 = $('#MaterialCategoryMasterId').val();
    if (City1 === "" || City1 === null) {
        $('#error2').text("Please select Category");
    } else {
        $('#error2').text("");
    }
} function ProdCod() {
    var City2 = $('#ProductCode').val();
    if (City2 === "" || City2 === null) {
        $('#error3').text("Please Enter Product Code");
    } else {
        $('#error3').text("");
    }
} function ProdName() {
    var City = $('#ProductName').val();
    if (City === "" || City === null) {
        $('#error4').text("Please Enter Product Name ");
    } else {
        $('#error4').text("");
    }
} function ProdDes() {
    var City = $('#ProductDesc').val();
    if (City === "" || City === null) {
        $('#error5').text("Please Enter Product Description ");
    } else {
        $('#error5').text("");
    }
}
function Costs() {
    var number = $('#cost').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error6').text("Please Enter Cost");
    }
    else if (filter.test(number)) {
        $('#error6').text("");
    }
    else {
        $('#error6').text("Please enter valid number");

    }
} function Prices() {
    var number = $('#Price').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error7').text("Please Enter Price");
    }
    else if (filter.test(number)) {
        $('#error7').text("");
    }
    else {
        $('#error7').text("Please enter valid number");

    }
} function proweights() {
    var number = $('#weight').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error8').text("Please Enter the Weight");
    }
    else if (filter.test(number)) {
        $('#error8').text("");
    }
    else {
        $('#error8').text("Please enter valid number");

    }
}
function Uom() {
    var City = $('#UomMasterId').val();
    if (City === "" || City === null) {
        $('#error9').text("Please Select UOM");
    } else {
        $('#error9').text("");
    }
} function Tax() {
    var City = $('#TaxMasterId').val();
    if (City === "" || City === null) {
        $('#error10').text("Please Select Tax Type");
    } else {
        $('#error10').text("");
    }
} function Store() {
    var City = $('#StoreId').val();
    if (City === "" || City === null) {
        $('#error12').text("Please Select Store Name");
    } else {
        $('#error12').text("");
    }
}
function productions() {
    var City = $('#productionperday').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (City === "" || City === null) {
        $('#error13').text("Please Enter Production Per-day");
    }
    else if (filter.test(City)) {
        $('#error13').text("");
    } else {
        $('#error13').text("Please enter valid number");
    }
}
function MaxSt() {
    var number = $('#MaxStock').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;

    if (number === "") {
        $('#error15').text("Please Enter Max Stock");
    } else if (filter.test(number)) {
        var numericValue = parseFloat(number);

        if (numericValue >= 1) {
            $('#error15').text("");
        } else {
            $('#error15').text("At least max stock starts from 1");
        }
    } else {
        $('#error15').text("Please enter a valid number");
    }
}

function MinSt() {
    var number = $('#MinStock').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error14').text("Please Enter Min Stock");
    }
    else if (filter.test(number)) {
        $('#error14').text("");
    }
    else {
        $('#error14').text("Please enter valid number");

    }
}


function productfun() {
    Store();
    Tax();
    Uom();
    Prices();
    ProductTypes();
    Category();
    ProdCod();
    ProdName();
}


// Account Receivable

function Cashfun() {
    var number = $('#Cash').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#cerror').text("Please Enter Cash");
    }
    else if (filter.test(number)) {
        $('#cerror').text("");
    }
    else {
        $('#cerror').text("Please enter valid number");

    }
}
function Debitnotedatefun() {
    var number = $('#Debitnotedate').val();
    if (number == "") {
        $('#Debitdateerror').text("Please Click Debitnotedate");
    }
    else {
        $('#Debitdateerror').text("");

    }
} function Debitnotereffun() {
    var number = $('#Debitnoteref').val();
    if (number == "") {
        $('#debitnoteerror').text("Please Enter Ref no");
    }
    else {
        $('#debitnoteerror').text("");

    }
}
function Debitnotevaluefun() {
    var number = $('#Debitnotevalue').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#Debitvalerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#Debitvalerror').text("");
    }
    else {
        $('#Debitvalerror').text("Please enter valid number");

    }
}
function payfun() {
    var number = $('#PaymentAmount').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#payerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#payerror').text("");
    }
    else {
        $('#payerror').text("Please enter valid number");

    }
}
function payreffun() {
    var number = $('#PaymentRefNo').val();
    if (number == "") {
        $('#payreferror').text("Please Enter Ref no");
    }
    else {
        $('#payreferror').text("");
    }
}

function debitreffun() {
    Debitnotedatefun();
    Debitnotereffun();
    Debitnotevaluefun();
}
function paysfun() {
    payfun();
    payreffun();
}


// account payable

function PAYCashfun() {
    var number = $('#Cash').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#pcerror').text("Please Enter Cash");
    }
    else if (filter.test(number)) {
        $('#pcerror').text("");
    }
    else {
        $('#pcerror').text("Please enter valid number");

    }
}
function creditnotedatefun() {
    var number = $('#CreditNoteDate').val();
    if (number == "") {
        $('#Creditdateerror').text("Please Click  Creditnotedate");
    }
    else {
        $('#Creditdateerror').text("");

    }
}
function creditnotereffun() {
    var number = $('#CreditNoteRefNo').val();
    if (number == "") {
        $('#Creditnoteerror').text("Please Enter Ref no");
    }
    else {
        $('#Creditnoteerror').text("");
    }
}
function creditnotevaluefun() {
    var number = $('#CreditNoteValue').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#Creditvalerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#Creditvalerror').text("");
    }
    else {
        $('#Creditvalerror').text("Please enter valid number");

    }
}
function Payablepayfun() {
    var number = $('#PaymentAmount').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#Payablepayerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#Payablepayerror').text("");
    }
    else {
        $('#Payablepayerror').text("Please enter valid number");

    }
}
function Payablepayreffun() {
    var number = $('#PaymentRefNo').val();
    if (number == "") {
        $('#Payablepayreferror').text("Please Enter Ref no");
    }
    else {
        $('#Payablepayreferror').text("");
    }
}

function Payablecreditfun() {
    creditnotedatefun();
    creditnotereffun();
    creditnotevaluefun();
}
function Payablepaysfun() {
    Payablepayfun();
    Payablepayreffun();
}

function ProductionQtyfun() {
    var number = $('#ProductionQty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#proderror').text("Please Enter ProductionQty");
    }
    else if (filter.test(number)) {
        $('#proderror').text("");
    }
    else {
        $('#proderror').text("Please enter valid number");

    }
}

function prodproductfun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#prodproerror').text("Please select Product");
    } else {
        $('#prodproerror').text("");
    }
}
function prodproductsubfun() {
    var City = $('#ProductSubId').val();
    if (City === "" || City === null) {
        $('#prodsuberror').text("Please select Sub_Product");
    } else {
        $('#prodsuberror').text("");
    }
}
function storefun() {
    var City = $('#StoreCode').val();
    if (City === "" || City === null) {
        $('#storeerror').text("Please select Store");
    } else {
        $('#storeerror').text("");
    }
}

function savevalidate() {
    prodproductfun();
    ProductionQtyfun();
    storefun();
}
function subvalidate() {
    prodproductsubfun();
    ProductionQtyfun();
    storefun();
}

// supplier material

function supplfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#serror').text("Please select Supplier");
    } else {
        $('#serror').text("");
    }
}
function accountypefun() {
    var City = $('#AccountTypeId').val();
    if (City === "" || City === null) {
        $('#AccountTypResult').text("Please select AccountType");
    } else {
        $('#AccountTypResult').text("");
    }
}
function Categoryfun() {
    var City = $('#category').val();
    if (City === "" || City === null) {
        $('#cerror').text("Please select category");
    } else {
        $('#cerror').text("");
    }
} function Producfun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#perror').text("Please select Product");
    } else {
        $('#perror').text("");
    }
} function UomIdfun() {
    var City = $('#UomId').val();
    if (City === "" || City === null) {
        $('#uomerror').text("Please select UOM");
    } else {
        $('#uomerror').text("");
    }
} function TaxIdfun() {
    var City = $('#TaxId').val();
    if (City === "" || City === null) {
        $('#taxerror').text("Please select Tax");
    } else {
        $('#taxerror').text("");
    }
}


function savesupplierfun() {
    TaxIdfun();
    UomIdfun();
    Producfun();
    Categoryfun();
    supplfun();
}



// customer details


function BuyerIdfun() {
    var City = $('#BuyerId').val();
    if (City === "" || City === null) {
        $('#BuyerIderror').text("Please select Buyer Name");
    } else {
        $('#BuyerIderror').text("");
    }
}
function SupplierIdfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#suppIderror').text("Please select Supllier Name");
    } else {
        $('#suppIderror').text("");
    }
}
function IsDefaultfun() {
    var City = $('#addressvarient').val();
    if (City === "" || City === null) {
        $('#IsDefaulterror').text("Please select Type of Address");
    } else {
        $('#IsDefaulterror').text("");
    }
}
function AddressTypefun() {
    var City = $('#AddressType').val();
    var addressvarient = $("#addressvarient").val();
    if (addressvarient != 0 || addressvarient == "") {
        if (City === "" || City === null) {
            $('#AddressTypeerror').text("Please select AddressType");
        } else {
            $('#AddressTypeerror').text("");
        }
    }

}
function Add1fun() {
    var City = $('#Add1').val();
    if (City === "" || City === null) {
        $('#Add1error').text("Please enter an address.");
    } else if (City.length > 100) {
        $('#Add1error').text("Address must be 100 characters or less.");
    } else {
        $('#Add1error').text("");
    }
}
function Add2fun() {
    var City = $('#Add2').val();
    if (City === "" || City === null) {
        $('#Add2error').text("Please enter an address.");
    } else if (City.length > 100) {
        $('#Add2error').text("Address must be 100 characters or less.");
    } else {
        $('#Add2error').text("");
    }
}
function Add3fun() {
    var City = $('#Add3').val();
    if (City === "" || City === null) {
        $('#Add3error').text("Please enter an address.");
    } else if (City.length > 100) {
        $('#Add3error').text("Address must be 100 characters or less.");
    } else {
        $('#Add3error').text("");
    }
}
function Countryfun() {
    var City = $('#Country').val();
    if (City === "" || City === null) {
        $('#Countryerror').text("Please select Country");
    } else {
        $('#Countryerror').text("");
    }
}
function Statefun() {
    var City = $('#State').val();
    if (City === "" || City === null) {
        $('#Stateerror').text("Please select State");
    } else {
        $('#Stateerror').text("");
    }
}
function Cityfun() {
    var City = $('#City').val();
    if (City === "" || City === null) {
        $('#Cityerror').text("Please select City");
    } else {
        $('#Cityerror').text("");
    }
} function ZipCodefun() {
    var Mobilenumber = $('#ZipCode').val();
    var filter = /^[0-9]{6}$/;
    if (Mobilenumber == "") {
        $('#Ziperror').text("Please enter the number");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Ziperror').text("");
    }
    else {
        $('#Ziperror').text("Please enter valid number");
    }
} function ContactNamefun() {
    var City = $('#ContactName').val();
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    if (City === "" || City === null) {
        $('#ContactNameerror').text("Please select Contact Name");
    } else if (phoneregx.test(City)) {
        $('#ContactNameerror').text("");
    } else {
        $('#ContactNameerror').text("Please enter valid Name");
    }
}
function Notesfun() {
    var City = $('#Notes').val();
    if (City === "" || City === null) {
        $('#Noteserror').text("Please Enter Notes");
    } else {
        $('#Noteserror').text("");
    }
}
function numbervalidatefun() {

    var Mobilenumber = $('#Phone').val();

    var filter = /^[0-9]{10}$/;
    if (Mobilenumber == "") {
        $('#Pherror').text("Please enter the number");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Pherror').text("");
    }
    else {
        $('#Pherror').text("Please enter valid number");
    }
}
function emailfun() {

    var email = $('#Email').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#emailerror').text("Please enter the email");
    }
    else if (phoneregx.test(email)) {
        $('#emailerror').text("");
    }
    else {
        $('#emailerror').text("Please enter valid Email");
    }
}
function savecustomerdetails() {
    Notesfun();
    emailfun();
    Cityfun();
    ContactNamefun();
    ZipCodefun();
    Statefun();
    Countryfun();
    numbervalidatefun();
    Add3fun();
    vatnumfun();
    Add1fun(); Add2fun();
    BuyerIdfun();
    IsDefaultfun();
    SupplierIdfun();
    AddressTypefun();


}



//Supplier Screen


function Suppliernamefun() {
    var City = $('#Suppliername').val();
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    if (City === "" || City === null) {
        $('#SuppliernameResult').text("Please Enter Supplier Name");
    } else if (phoneregx.test(City)) {
        $('#SuppliernameResult').text("");
    } else {
        $('#SuppliernameResult').text("Please enter valid Name");
    }
}
function SupplierCodefun() {
    var City = $('#suppliercode').val();
    if (City === "" || City === null) {
        $('#suppCodetResult').text("Please select Supplier Code");
    } else {
        $('#suppCodetResult').text("");
    }
} function Accountfun() {
    var z1 = /^[0-9]*$/;
    var City = $('#Account').val();
    if (City === "" || City === null) {
        $('#AccountResult').text("Please select Account Number");
    } else if (z1.test(City)) {
        $('#AccountResult').text("");
    } else {
        $('#AccountResult').text("Please Enter Valid Number");
    }
} function AccountNamefun() {
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    var City = $('#AccountName').val();
    if (City === "" || City === null) {
        $('#AccountNameResult').text("Please select Account Name");
    } else if (phoneregx.test(City)) {
        $('#AccountNameResult').text("");
    } else {
        $('#AccountNameResult').text("Please enter valid Name");
    }
}
function AccountDescriptionfun() {
    var City = $('#AccountDescription').val();
    if (City === "" || City === null) {
        $('#AccountDescriptionNameResult').text("Please Enter Account Description");
    } else {
        $('#AccountDescriptionNameResult').text("");
    }
}
function EmailContactfun() {

    var email = $('#EmailContact').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailResult').text("Please enter the Email Contact");
    }
    else if (phoneregx.test(email)) {
        $('#EmailResult').text("");
    }
    else {
        $('#EmailResult').text("Please enter valid Email");
    }
}
function Physical2fun() {
    var City = $('#Physical2').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical2Reult').text("Please enter an Physical Address 2.");
    } else if (City.length > 100) {
        $('#Physical2Reult').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical2Reult').text("");
    }
} function Physical5fun() {
    var City = $('#Physical5').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical5Result').text("Please enter an Physical Address 5.");
    } else if (City.length > 100) {
        $('#Physical5Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical5Result').text("");
    }
} function Physical4fun() {
    var City = $('#Physical4').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical4Result').text("Please enter an Physical Address 4.");
    } else if (City.length > 100) {
        $('#Physical4Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical4Result').text("");
    }
} function Physical3fun() {
    var City = $('#Physical3').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical3Result').text("Please enter an Physical Address 3.");
    } else if (City.length > 100) {
        $('#Physical3Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical3Result').text("");
    }
} function Physical1fun() {
    var City = $('#Physical1').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical1Result').text("Please enter an Physical Address 1.");
    } else if (City.length > 100) {
        $('#Physical1Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical1Result').text("");
    }
}
function PhysicalCodefun() {
    var Mobilenumber = $('#PhysicalCode').val();
    var filter = /^[0-9]{6}$/;
    if (Mobilenumber == "") {
        $('#PhysicalCodeResult').text("Please Enter the Physical Code");
    }
    else if (filter.test(Mobilenumber)) {
        $('#PhysicalCodeResult').text("");
    }
    else {
        $('#PhysicalCodeResult').text("Please Enter valid number");
    }
}

function Postal1fun() {
    var City = $('#Postal1').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal1Result').text("Please enter an Postal Address 1.");
    } else if (City.length > 100) {
        $('#Postal1Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal1Result').text("");
    }
}
function Postal2fun() {
    var City = $('#Postal2').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal2Result').text("Please enter an Postal Address 2.");
    } else if (City.length > 100) {
        $('#Postal2Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal2Result').text("");
    }
}
function Postal3fun() {
    var City = $('#Postal3').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal3Result').text("Please enter an Postal Address 3.");
    } else if (City.length > 100) {
        $('#Postal3Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal3Result').text("");
    }
}
function Postal4fun() {
    var City = $('#Postal4').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal4Result').text("Please enter an Postal Address 4.");
    } else if (City.length > 100) {
        $('#Postal4Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal4Result').text("");
    }
}
function Postal5fun() {
    var City = $('#Postal5').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal5Result').text("Please enter an Postal Address 4.");
    } else if (City.length > 100) {
        $('#Postal5Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal5Result').text("");
    }
}
function PostalCodefun() {
    var Mobilenumber = $('#PostalCode').val();
    var filter = /^[0-9]{6}$/;
    if (Mobilenumber == "") {
        $('#PostalCodeResult').text("Please Enter the Postal Code");
    }
    else if (filter.test(Mobilenumber)) {
        $('#PostalCodeResult').text("");
    }
    else {
        $('#PostalCodeResult').text("Please Enter valid number");
    }
}
function Telephone1fun() {
    var Mobilenumber = $('#Telephone1').val();
    var filter = /(?:\(?\+\d{2}\)?\s*)?\d+(?:[ -]*\d+)*$/;
    if (Mobilenumber == "") {
        $('#Telephone1Result').text("Please Enter the Telephone1");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Telephone1Result').text("");
    }
    else {
        $('#Telephone1Result').text("Please Enter valid number");
    }
}
function Telephone2fun() {
    var Mobilenumber = $('#Telephone2').val();
    var filter = /(?:\(?\+\d{2}\)?\s*)?\d+(?:[ -]*\d+)*$/;
    if (Mobilenumber == "") {
        $('#Telephone2Result').text("Please Enter the Telephone2");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Telephone2Result').text("");
    }
    else {
        $('#Telephone2Result').text("Please Enter valid number");
    }
}
function EmailAccountsfun() {

    var email = $('#EmailAccounts').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailAccountsResult').text("Please enter the Email Account");
    }
    else if (phoneregx.test(email)) {
        $('#EmailAccountsResult').text("");
    }
    else {
        $('#EmailAccountsResult').text("Please enter valid Email");
    }
} function EmailEmergencyfun() {

    var email = $('#EmailEmergency').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmergencyNameResult').text("Please enter the Emergency Email");
    }
    else if (phoneregx.test(email)) {
        $('#EmergencyNameResult').text("");
    }
    else {
        $('#EmergencyNameResult').text("Please enter valid Email");
    }
}
function VatNumberfun() {
    var City = $('#VatNumber').val();
    if (City === "" || City === null) {
        $('#VatResult').text("Please Enter VatNumber");
    } else {
        $('#VatResult').text("");
    }
} function RegNumberfun() {
    var City = $('#RegNumber').val();
    if (City === "" || City === null) {
        $('#RegResult').text("Please Enter RegNumber");
    } else {
        $('#RegResult').text("");
    }
}
function DcBalancebom() {
    var number = $('#DcBalance').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#DcBalanceNameResult').text("Please Enter the DcBalance");
    }
    else if (filter.test(number)) {
        $('#DcBalanceNameResult').text("");
    }
    else {
        $('#DcBalanceNameResult').text("Please enter valid number");
    }
}
function CreditLimitfun() {
    var number = $('#CreditLimit').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#CreditResult').text("Please Enter the CreditLimit");
    }
    else if (filter.test(number)) {
        $('#CreditResult').text("");
    }
    else {
        $('#CreditResult').text("Please enter valid number");
    }
} function Interestfun() {
    var number = $('#Interest').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#inResult').text("Please Enter the Interest");
    }
    else if (filter.test(number)) {
        $('#inResult').text("");
    }
    else {
        $('#inResult').text("Please enter valid number");
    }
}

function TaxTypeIdfun() {
    var City = $('#TaxTypeId').val();
    if (City === "" || City === null) {
        $('#TaxTypeIdResult').text("Please Enter TaxTypeId");
    } else {
        $('#TaxTypeIdResult').text("");
    }
}
function vatnumfun() {
    var City = $('#vatnumber').val();
    if (City === "" || City === null) {
        $('#vaterror').text("Please Enter VAT Number");
    } else {
        $('#vaterror').text("");
    }
}
function CurrencyIDfun() {
    var City = $('#CurrencyID').val();
    if (City === "" || City === null) {
        $('#CurrencyIDResult').text("Please Enter Currency");
    } else {
        $('#CurrencyIDResult').text("");
    }
}
function ForeignCurrencyfun() {
    var City = $('#ForeignCurrency').val();
    if (City === "" || City === null) {
        $('#ForeignCurrencyNameResult').text("Please Enter Foreign_Currency");
    } else {
        $('#ForeignCurrencyNameResult').text("");
    }
}
function CurrencyConversionfun() {
    var City = $('#currencyOption').val();
    if (City === "" || City === null) {
        $('#Currencyerror').text("Please Select Currency");
    } else {
        $('#Currencyerror').text("");
    }
}

function ChargeinResultfun() {

    var s = $('input[name="yes"]:checked').val();
    if (s != undefined) {
        $('#ChargeinResult').text("");
    } else {
        $('#ChargeinResult').text("Please Click the Interest Need or Not");
    }
}

function confirmsupplier() {
    ChargeinResultfun();
    Suppliernamefun();
    SupplierCodefun();
    Accountfun();
    AccountNamefun();
    AccountDescriptionfun();
    EmailContactfun();
    Physical1fun();
    PhysicalCodefun();
    accountypefun();
    Telephone1fun();
    Telephone2fun();
    EmailAccountsfun();
    EmailEmergencyfun();
    VatNumberfun();
    RegNumberfun();
    DcBalancebom();
    CreditLimitfun();
    Interestfun();
    TaxTypeIdfun();
    CurrencyIDfun();
    ForeignCurrencyfun();
}

function loginEmailfun() {
    var email = $('#Email').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#Emailpass').text("Please enter the Email");
    }
    else if (phoneregx.test(email)) {
        $('#Emailpass').text("");
    }
    else {
        $('#Emailpass').text("Please enter valid Email");
    }
}

function passwordfun() {

    var Password = $('#Password').val();
    var phoneregx = /^(?=.*[0-5])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$/;

    if (Password == "") {
        $('#passerror').text("Please fill the password");
    }
    else if (phoneregx.test(Password)) {
        $('#passerror').text("");
    }
    else {
        $('#passerror').text("Please enter a valid Passsword");
    }
}

function confirmlogin() {
    passwordfun();
    loginEmailfun();
}

// category



function categoryfun() {
    var City = $('#CategoryName').val();
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    if (City === "" || City === null) {
        $('#error31').text("Please Enter Category Name");
    } else if (phoneregx.test(City)) {
        $('#error31').text("");
    } else {
        $('#error31').text("Please enter valid Name");
    }
}

function categoryCod() {
    var City2 = $('#CategoryCode').val();
    if (City2 === "" || City2 === null) {
        $('#error').text("Please Enter Category Code");
    } else {
        $('#error').text("");
    }
}

function categorytypeCod() {
    var City2 = $('#Categorytype').val();
    if (City2 === "" || City2 === null) {
        $('#error3').text("Please Select Category Type");
    } else {
        $('#error3').text("");
    }
}

function categorysave() {
    categorytypeCod();
    categoryCod();
    categoryfun();
}



function buyernamefun() {
    var City = $('#CustomerName').val();
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    if (City === "" || City === null) {
        $('#CustomerNameResult').text("Please Enter Buyer Name");
    } else if (phoneregx.test(City)) {
        $('#CustomerNameResult').text("");
    } else {
        $('#CustomerNameResult').text("Please enter valid Name");
    }
}
function BuyerCodefun() {
    var City2 = $('#BuyerCode').val();
    if (City2 === "" || City2 === null) {
        $('#BuyerCodeResult').text("Please Enter Buyer Code");
    } else {
        $('#BuyerCodeResult').text("");
    }
}
function BuyerShortNamefun() {
    var City2 = $('#BuyerShortName').val();
    if (City2 === "" || City2 === null) {
        $('#NameResult').text("Please Enter Buyer Short Name");
    } else {
        $('#NameResult').text("");
    }
}

function EmailContactfun() {

    var email = $('#EmailContact').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailResult').text("Please enter the Email");
    }
    else if (phoneregx.test(email)) {
        $('#EmailResult').text("");
    }
    else {
        $('#EmailResult').text("Please enter valid Email");
    }
}

function Accountfun() {
    var City2 = $('#Account').val();
    if (City2 === "" || City2 === null) {
        $('#AccountResult').text("Please Enter Account No");
    } else {
        $('#AccountResult').text("");
    }
}
function AccountNamefun() {
    var City2 = $('#AccountName').val();
    if (City2 === "" || City2 === null) {
        $('#AccountNameResult').text("Please Enter Account Name");
    } else {
        $('#AccountNameResult').text("");
    }
}
function AccountDescriptionfun() {
    var City2 = $('#AccountDescription').val();
    if (City2 === "" || City2 === null) {
        $('#AccountDescriptionResult').text("Please Enter Account Description");
    } else {
        $('#AccountDescriptionResult').text("");
    }
}
function SwiftCodefun() {
    var City2 = $('#SwiftCode').val();
    if (City2 === "" || City2 === null) {
        $('#SwiftCodeResult').text("Please Enter Swift Code");
    } else {
        $('#SwiftCodeResult').text("");
    }
}
function Physical1fun() {
    var City2 = $('#Physical1').val();
    if (City2 === "" || City2 === null) {
        $('#Physical1Result').text("Please Enter Address");
    } else {
        $('#Physical1Result').text("");
    }
}
function PhysicalCodefun() {
    var City2 = $('#PhysicalCode').val();
    if (City2 === "" || City2 === null) {
        $('#PhysicalCodeResult').text("Please Enter Zip Code");
    } else {
        $('#PhysicalCodeResult').text("");
    }
}
function CurrencyIdfun() {
    var City2 = $('#CurrencyId').val();
    if (City2 === "" || City2 === null) {
        $('#CurrencyIdResult').text("Please Select Currency Type");
    } else {
        $('#CurrencyIdResult').text("");
    }
}
function Telephone1fun() {
    var City2 = $('#Telephone1').val();
    if (City2 === "" || City2 === null) {
        $('#Telephone1Result').text("Please Enter Telephone1");
    } else {
        $('#Telephone1Result').text("");
    }
}
function Telephone2fun() {
    var City2 = $('#Telephone2').val();
    if (City2 === "" || City2 === null) {
        $('#Telephone2Result').text("Please Enter Telephone2");
    } else {
        $('#Telephone2Result').text("");
    }
}

function EmailAccountsfun() {

    var email = $('#EmailAccounts').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailAccountsResult').text("Please enter the Email");
    }
    else if (phoneregx.test(email)) {
        $('#EmailAccountsResult').text("");
    }
    else {
        $('#EmailAccountsResult').text("Please enter valid Email");
    }
}
function EmailEmergencysfun() {

    var email = $('#EmailEmergency').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailEmergencyResult').text("Please enter the Email");
    }
    else if (phoneregx.test(email)) {
        $('#EmailEmergencyResult').text("");
    }
    else {
        $('#EmailEmergencyResult').text("Please enter valid Email");
    }
}

function AccountTypeIdfun() {
    var City2 = $('#AccountTypeId').val();
    if (City2 === "" || City2 === null) {
        $('#AccountTypeIdResult').text("Please select Account Type");
    } else {
        $('#AccountTypeIdResult').text("");
    }
} function VatNumberfun() {
    var City2 = $('#VatNumber').val();
    if (City2 === "" || City2 === null) {
        $('#VatNumberResult').text("Please Enter VatNumber");
    } else {
        $('#VatNumberResult').text("");
    }
} function RegNumberfun() {
    var City2 = $('#RegNumber').val();
    if (City2 === "" || City2 === null) {
        $('#RegNumberResult').text("Please Enter RegNumber");
    } else {
        $('#RegNumberResult').text("");
    }
}

function CreditLimitResultfun() {
    var number = $('#CreditLimit').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#CreditLimitResult').text("Please Enter the CreditLimit");
    }
    else if (filter.test(number)) {
        $('#CreditLimitResult').text("");
    }
    else {
        $('#CreditLimitResult').text("Please enter valid number");
    }
}
function InterestResultfun() {
    var number = $('#Interest').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#InterestResult').text("Please Enter the Interest");
    }
    else if (filter.test(number)) {
        $('#InterestResult').text("");
    }
    else {
        $('#InterestResult').text("Please enter valid number");
    }
}
function TaxTypeIdfun() {
    var City2 = $('#TaxTypeId').val();
    if (City2 === "" || City2 === null) {
        $('#TaxTypeIdResult').text("Please Select Tax-Type");
    } else {
        $('#TaxTypeIdResult').text("");
    }
}
function ForeignCurrencyfun() {
    var City2 = $('#ForeignCurrency').val();
    if (City2 === "" || City2 === null) {
        $('#ForeignCurrencyResult').text("Please Select Foreign-Currency");
    } else {
        $('#ForeignCurrencyResult').text("");
    }
}
function DcBalancefun() {
    var number = $('#DcBalance').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#DcBalanceResult').text("Please Enter the Dc-Balance");
    }
    else if (filter.test(number)) {
        $('#DcBalanceResult').text("");
    }
    else {
        $('#DcBalanceResult').text("Please enter valid number");
    }
}
function WebsiteResultfun() {
    var City2 = $('#Website').val();
    if (City2 === "" || City2 === null) {
        $('#WebsiteResult').text("Please Enter Website");
    } else {
        $('#WebsiteResult').text("");
    }
}

function buyerformsave() {
    WebsiteResultfun();
    DcBalancefun();
    ForeignCurrencyfun();
    TaxTypeIdfun();
    InterestResultfun();
    CreditLimitResultfun();
    RegNumberfun();
    VatNumberfun();
    AccountTypeIdfun();
    EmailAccountsfun();
    EmailEmergencysfun();
    Physical1fun();
    PhysicalCodefun();
    CurrencyIdfun();
    Telephone1fun();
    Telephone2fun();
    EmailContactfun();
    Accountfun();
    AccountNamefun();
    AccountDescriptionfun();
    SwiftCodefun();
    buyernamefun();
    BuyerCodefun();
    BuyerShortNamefun();
}

